namespace Tetris

open System

module Say =
    let hello name =
        printfn "Hello %s" name

module Modelo =
    let aleatorio = new System.Random ()

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
    
    

