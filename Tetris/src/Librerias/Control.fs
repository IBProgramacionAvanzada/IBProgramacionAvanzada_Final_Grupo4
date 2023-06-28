namespace Tetris

open Tetris.Modelo

module Control =

    let rec rotar = function
        | (_::_)::_ as m -> 
            (List.map List.head m |> List.rev)::(List.map List.tail m |> rotar) 
        | _ -> []