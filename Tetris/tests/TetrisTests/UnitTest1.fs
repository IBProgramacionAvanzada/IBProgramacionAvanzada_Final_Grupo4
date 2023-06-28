module TetrisTests

open NUnit.Framework
open EstructuraDatosTetris.TiposTetris
open InitTetris.Inicializar
open FuncionesTetris.Funciones

[<SetUp>]
let Setup () =
    ()

[<Test>]
let ``Rotacion de bloque azul`` () = 
    let formaInicial = (Azul, [[O; X; X]; [X; X; O]])
    let formaRotada = rotar (snd formaInicial)
    let formaEsperada = snd (Azul, [[X; O]; [X; X]; [O; X]])
    
    assert (formaRotada = formaEsperada)
    
[<Test>]
let ``Rotacion de bloque rojo`` () =
    let formaInicial = [ [ X; X; X; X ] ]
    let expected = [ [X]; [X]; [X]; [X] ]                                  
    let actual = formaInicial |> rotar
    
    Assert.AreEqual(expected, actual)
    
[<Test>]
let ``Bloque fuera de rango`` () =
    let bloques = [(9, 5); (10, 5); (5, 21); (-1, 10)]
    let resultado = fueraRango bloques

    assert (resultado = true)

