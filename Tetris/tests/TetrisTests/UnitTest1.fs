module TetrisTests

open NUnit.Framework
open EstructuraDatosTetris.TiposTetris
open InitTetris.Inicializar
open FuncionesTetris.Funciones

[<SetUp>]
let Setup () =
    ()

[<Test>]
let ``Rotacion de bloque`` () = 
    let formaInicial = (Azul, [[O; X; X]; [X; X; O]])
    let formaRotada = rotar (snd formaInicial)
    let formaEsperada = snd (Azul, [[X; O]; [X; X]; [O; X]])
    
    assert (formaRotada = formaEsperada)

[<Test>]
let ``Bloque fuera de rango`` () =
    let bloques = [(9, 5); (10, 5); (5, 21); (-1, 10)]
    let resultado = fueraRango bloques

    assert (resultado = true)

