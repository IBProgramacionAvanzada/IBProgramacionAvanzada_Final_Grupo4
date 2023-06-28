namespace Tetris

open System

module Say =
    let hello name =
        printfn "Hello %s" name

module Modelo =
    let aleatorio = new System.Random ()

    let ancho, alto = 10, 20
    let posInicial = (ancho / 2 - 1, 0)
    let puntajePorLinea = 100

    type Color = 
        | Rojo
        | Magenta
        | Amarillo
        | Cian
        | Azul
        | Plateado
        | Verde

    type Marca = 
        |X
        |O

    let formas = [
            Cian, [
                [X;X]
                [X;X]
            ]
            Rojo, [
                [X;X;X;X]
            ]
            Verde, [
                [X;X;O]
                [O;X;X]
            ]
            Azul, [
                [O;X;X]
                [X;X;O]
            ]
            Amarillo, [
                [X;X;X]
                [X;O;O]
            ]
            Magenta, [
                [X;X;X]
                [O;O;X]
            ]
            Plateado, [
                [X;X;X]
                [O;X;O]
            ]
        ]
    
    type Comando =
        | Derecha
        | Izquierda
        | Rotar

    type Evento =
        | Movido 
        | Rotado 
        | Bloqueado 
        | Lanzado 
        | Linea 
    //    | IncrementoNivel 
        | FinDelJuego

    type Tablero = {
        puntaje: int
        nivel: int
        juegoFinalizado: bool

        tiempoUltimoComando: float
        tiempoUltimaCaida: float
        tiempoUltimaLinea: float
        lineasParaQuitar: (Color * int * int) list option

        bloquesEstaticos: (Color * int * int) list
        pos: int * int
        forma: (Color * Marca list list) option
        siguiente: Color * Marca list list
        eventos: Evento list
    } 
