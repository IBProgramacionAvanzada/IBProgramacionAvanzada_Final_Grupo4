namespace FuncionesTetris

module Funciones =
    open EstructuraDatosTetris.TiposTetris
    open InitTetris.Inicializar

    let rec rotar = function
        | (_::_)::_ as l -> 
            (List.map List.head l |> List.rev)::(List.map List.tail l |> rotar) 
        | _ -> []

    let traslacionBloques (tlx, tly) =
        List.mapi (fun y -> 
            List.mapi (fun x -> function
            | X -> (x + tlx, y + tly) |> Some
            | O -> None) >> List.choose id) >> List.concat

    let fueraRango bloques =
        bloques |> List.exists (fun (x,y) -> x < 0 || x >= ancho || y < 0 || y >= alto)

    let seSuperponen bloques tablero =
        let bloquesActuales = tablero.bloquesEstaticos |> List.map (fun (_,x,y) -> x,y)
        List.except bloquesActuales bloques <> bloques

    let procesoComando comando tablero =
        match comando with
        | None -> tablero
        | Some c ->
            match tablero.forma with
            | None -> tablero
            | Some (color, bloques) ->
                let (x, y) = tablero.pos
                let (nx, ny) = 
                    match c with
                    | Izquierda -> (x - 1, y)
                    | Derecha -> (x + 1, y)
                    | Rotar -> (x, y)
                let nuevaForma = bloques |> match c with | Rotar -> rotar | _ -> id
                let nuevosBloques = traslacionBloques (nx, ny) nuevaForma
                
                if fueraRango nuevosBloques || seSuperponen nuevosBloques tablero then 
                    {tablero with eventos = Bloqueo::tablero.eventos}
                else
                    let evento = 
                        match c with
                        | Rotar -> Rotacion
                        | Izquierda | Derecha -> MovimientoHorizontal
                    {tablero with 
                        forma = Some (color, nuevaForma)
                        pos = (nx, ny)
                        eventos = evento::tablero.eventos}

    let caida tiempo teclaAbajoPresionada tablero = 
        match tablero.forma with
        | None -> tablero
        | Some _ when 
            let tiempoEntreCaidas = 
                if teclaAbajoPresionada then tiempoMinCaida 
                else tiempoEntreCaidas
            tiempo - tablero.caidaAnteriorTiempo < tiempoEntreCaidas -> tablero
        | Some (color, bloques) ->
            let (x, y) = tablero.pos
            let nuevaPos = (x, y + 1)

            let nuevosBloques = traslacionBloques nuevaPos bloques
            if not (fueraRango nuevosBloques) && not (seSuperponen nuevosBloques tablero) then 
                {tablero with pos = nuevaPos; caidaAnteriorTiempo = tiempo; eventos = BloqueEnPosFinal::tablero.eventos}
            else    
                let bloquesActuales = traslacionBloques tablero.pos bloques |> List.map (fun (x,y) -> color, x, y)
                {tablero with bloquesEstaticos = tablero.bloquesEstaticos @ bloquesActuales; forma = None }

    let proxForma tablero =
        match tablero.forma with
        | Some _ -> tablero
        | None ->
            let proxBloques = snd tablero.proxForma |> traslacionBloques posicionInic
            let juegoTerminado = seSuperponen proxBloques tablero
            {tablero with
                eventos = if juegoTerminado then [FinJuego] else tablero.eventos
                juegoTerminado = juegoTerminado
                pos = posicionInic
                forma = Some tablero.proxForma
                proxForma = formaAleatoria ()}
        
    let obtengoLineas tablero = 
        tablero.bloquesEstaticos 
            |> List.groupBy (fun (_,_,y) -> y) 
            |> List.filter (fun r -> List.length (snd r) = ancho)
            |> List.collect snd 

    let eliminoLineas tiempo tablero = 
        match tablero.lineasAEliminar with
        | None -> tablero
        | Some lineas ->
            let nuevaPuntuacion = List.length lineas / ancho * puntuacionPorLinea |> (+) tablero.puntuacion
            let horizontales = lineas |> List.map  (fun (_,_,y) -> y) |> List.distinct
            let nuevosBloques = 
                List.except lineas tablero.bloquesEstaticos
                |> List.map (fun (c, x, y) -> 
                    let ajusto = y::horizontales |> List.sortByDescending id |> List.findIndex ((=) y)
                    c, x, (y + ajusto))

            {tablero with 
                bloquesEstaticos = nuevosBloques
                puntuacion = nuevaPuntuacion
                eventos = tablero.eventos
                caidaAnteriorTiempo = tiempo
                lineasAEliminar = None}

    let avanzar tiempo comando teclaAbajoPresionada tablero =
        let resultado =
            {tablero with eventos = [] } 
            |> eliminoLineas tiempo
            |> proxForma
            |> procesoComando comando
            |> caida tiempo teclaAbajoPresionada
        let lineas = obtengoLineas resultado
        if List.isEmpty lineas then resultado else 
            {resultado with 
                eventos = LineaEliminada::tablero.eventos
                lineasAEliminar = Some lineas}