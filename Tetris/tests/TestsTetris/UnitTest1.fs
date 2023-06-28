module TestsTetris

open NUnit.Framework
open Tetris.Modelo
open Tetris.Control

[<SetUp>]
let Setup () =
    ()

[<Test>]
let Test1 () =
    Assert.Pass()

[<Test>]
let ``test de rotacion Rojo`` () =
    let formaInicial = [ [ X; X; X; X ] ]
    let expected = [ [X]; [X]; [X]; [X] ]                                  
    let actual = formaInicial |> rotar
    
    Assert.AreEqual(expected, actual)