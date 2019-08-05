module App

open Browser.Dom

let minus = document.getElementById "minus"
let plus = document.getElementById "plus"

let mutable count = 0

let textcount = document.getElementById "textcount"
textcount.innerHTML <- (string count)

minus.addEventListener(
    "click",
    fun _ -> 
        count <- count - 1
        textcount.innerHTML <- (string count)
)

plus.addEventListener(
    "click",
    fun _ -> 
        count <- count + 1
        textcount.innerHTML <- (string count)
)

