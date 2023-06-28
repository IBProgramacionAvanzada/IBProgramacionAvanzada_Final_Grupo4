namespace Tetris

open Tetris.Modelo

module Control =

    let formaAleatoria () = formas.[aleatorio.Next(formas.Length)]
    
    let tableroInicial = {
        puntaje = 0
        nivel = 0
        juegoFinalizado = false

        tiempoUltimoComando = 0.
        tiempoUltimaCaida = 0.
        tiempoUltimaLinea = 0.
        lineasParaQuitar = None

        bloquesEstaticos = []
        pos = posInicial
        forma = formaAleatoria () |> Some
        siguiente = formaAleatoria ()
        eventos = []
    }
    
    let rec rotar = function
        | (_::_)::_ as m -> 
            (List.map List.head m |> List.rev)::(List.map List.tail m |> rotar) 
        | _ -> []

    let dibujar (tlx, tly) =
        List.mapi (fun y -> 
            List.mapi (fun x -> function
                                        | X -> (x + tlx, y + tly) |> Some
                                        | O -> None) >> List.choose id) >> List.concat
    
    let fueraDelTablero bloques =
        bloques |> List.exists (fun (x,y) -> x < 0 || x >= ancho || y < 0 || y >= alto)
    
    