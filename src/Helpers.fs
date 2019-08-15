module Helpers

open Fable.Core
open Fable.Core.JsInterop
open Fetch.Types
open Thoth.Json

let errorString (response: Response) =
    string response.Status + " " + response.StatusText + " for URL " + response.Url

let inline requestHeaders (headers: HttpRequestHeaders list) =
    RequestProperties.Headers(keyValueList CaseRules.None headers :?> IHttpRequestHeaders)

let fetchWithDecoder<'T> (url: string) (decoder: Decoder<'T>) (init: RequestProperties list) =
    GlobalFetch.fetch(RequestInfo.Url url, Fetch.requestProps init)
    |> Promise.bind (fun response ->
        if not response.Ok then
            errorString response |> Error |> Promise.lift
        else
            response.text() |> Promise.map (Decode.fromString decoder))

let inline fetchAs<'T> (url: string) (init: RequestProperties list) =
    let decoder = Decode.Auto.generateDecoderCached<'T>()
    fetchWithDecoder url decoder init

let inline httpGet<'T> (url: string) =
    fetchAs<'T> url [
        RequestProperties.Method HttpMethod.GET
        requestHeaders [ ContentType "application/json" ]
        RequestProperties.Credentials RequestCredentials.Sameorigin
    ]