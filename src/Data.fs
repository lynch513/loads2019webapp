module Data

open Loads2019.Types

let testStationDefault =
    Station.create "3600" [
        Section.create [
            Line.create "ф.20-210"  [  50;   70]
            Line.create "ф.20-210"  [  50;   70] 
            Line.create "ф.20-207"  [  50;   70] 
            Line.create "3643А"     [ -30;  -45] 
            Line.create "3661А"     [ -10;  -10] 
            Line.create "3647Б"     [ -20;  -25] 
            Line.create "3673А"     [ -40;  -45] 
            Line.create "3749"      [ -18;  -15]
            ]
        Section.create [ 
            Line.create "ф.20-506"  [  50;   70] 
            Line.create "ф.20-513"  [  50;   70] 
            Line.create "3643Б"     [ -30;  -45] 
            Line.create "3661Б"     [ -10;  -10] 
            Line.create "3647А"     [ -20;  -25] 
            Line.create "3673Б"     [ -40;  -45] 
            Line.create "8630"      [ -18;  -15] 
            Line.create "Т"         [ -18;  -15] 
        ]
    ]

let testStation1 =
    Station.create "3750" [
        Section.create [
            Line.create "ф.160-30"  [ 168;   97]
            Line.create "3702Б"     [ -50;  -50]
            Line.create "3703Б"     [ -30;  -10]
            Line.create "8630"      [ -10;    0]
            Line.create "3735"      [ -20;   -8]
            Line.create "8641"      [ -40;  -19]
            Line.create "Т"         [ -18;  -10]
        ]
        Section.create [
            Line.create "ф.20-807"  [  40;   47]
            Line.create "ф.20-813"  [  40;   47]
            Line.create "3702А"     [ -30;  -28]
            Line.create "8082"      [ -30;  -35]
            Line.create "3703А"     [ -20;  -30]
            Line.create "3670"      [   0;    0]
            Line.create "8519"      [   0;    0]
        ]
    ]

let testStations = [
    Station.create "3600" [
        Section.create [
            Line.create "ф.20-210"  [  50;   70]
            Line.create "ф.20-210"  [  50;   70] 
            Line.create "ф.20-207"  [  50;   70] 
            Line.create "3643А"     [ -30;  -45] 
            Line.create "3661А"     [ -10;  -10] 
            Line.create "3647Б"     [ -20;  -25] 
            Line.create "3673А"     [ -40;  -45] 
            Line.create "3749"      [ -18;  -15]
            ]
        Section.create [ 
            Line.create "ф.20-506"  [  50;   70] 
            Line.create "ф.20-513"  [  50;   70] 
            Line.create "3643Б"     [ -30;  -45] 
            Line.create "3661Б"     [ -10;  -10] 
            Line.create "3647А"     [ -20;  -25] 
            Line.create "3673Б"     [ -40;  -45] 
            Line.create "8630"      [ -18;  -15] 
            Line.create "Т"         [ -18;  -15] 
        ]
    ]

    Station.create "3735" [
        Section.create [
            Line.create "ф.160-50"  [  35;   25]
            Line.create "3750"      [   0;    0]
        ]
    ] 

    Station.create "3750" [
        Section.create [
            Line.create "ф.160-30"  [ 168;   97]
            Line.create "3702Б"     [ -50;  -50]
            Line.create "3703Б"     [ -30;  -10]
            Line.create "8630"      [ -10;    0]
            Line.create "3735"      [ -20;   -8]
            Line.create "8641"      [ -40;  -19]
            Line.create "Т"         [ -18;  -10]
        ]
        Section.create [
            Line.create "ф.20-807"  [  40;   47]
            Line.create "ф.20-813"  [  40;   47]
            Line.create "3702А"     [ -30;  -28]
            Line.create "8082"      [ -30;  -35]
            Line.create "3703А"     [ -20;  -30]
            Line.create "3670"      [   0;    0]
            Line.create "8519"      [   0;    0]
        ]
    ]

    Station.create "3770" [
        Section.create [
            Line.create "ф.160-06"  [  20;   20]
            Line.create "ф.160-105" [   0;    0]
            Line.create "8515"      [   0;    0]
            Line.create "8779А"     [  -6;   -8]
            Line.create "Т1"        [ -20;   -8]
            Line.create "Т5"        [ -20;  -19]
            Line.create "Т4"        [ -18;  -10]
            ]
        Section.create [
            Line.create "ф.160-31"  [  30;   20]
            Line.create "3640"      [   0;    0]
            Line.create "8799Б"     [   0;    0]
            Line.create "Т2"        [  -5;   -5]
            Line.create "Т3"        [  -9;   -6]
            ]
        ]
 
    Station.create "3780" [
        Section.create [
            Line.create "ф.20-609"  [  35;   35]
            Line.create "3700"      [   0;    0]
            ]
        ]

    Station.create "3785" [
        Section.create [
            Line.create "ф.160-29"  [  80;   60]
            Line.create "ф.160-149" [   0;    0]
            Line.create "Т1 ТП6"    [   0;    0]
            Line.create "Т2 ТП6"    [   0;    0]
            Line.create "ТП1"       [   0;    0]
            Line.create "ТП4"       [   0;    0]
            ]
        ]
]
