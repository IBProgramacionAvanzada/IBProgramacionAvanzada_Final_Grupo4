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

[<Test>]
let ``Traslacion de bloque verde`` () =
    let formaInicial = (Azul, [[X; X; O]; [O; X; X]])
    let esperada = [(0, 0); (1, 0); (1, 1); (2, 1)]
    let calculada = traslacionBloques (0, 0) (snd formaInicial)
    
    Assert.AreEqual(esperada, calculada)

[<Test>]
let ``Bloques se superponen en el tablero`` () =
    (* let unTablero = {

    }
    let bloques = 
    let resultado = seSuperponen bloques unTablero

    assert (resultado = true) *)
    Assert.Pass()

[<Test>]
let ``Procesamiento de un comando`` () = 
    Assert.Pass()

[<Test>]
let ``Lanzamiento de una ficha hacia el fondo`` () = 
    Assert.Pass()

[<Test>]
let ``Actualiza tablero y cual sera la proxima pieza`` () = 
    Assert.Pass()

[<Test>]
let ``Averigua las lineas que pueden ser dadas de baja`` () = 
    Assert.Pass()

[<Test>]
let ``Elimina las lineas completadas del tablero y suma puntos`` () = 
    Assert.Pass()

[<Test>]
let ``Avanza el estado del juego`` () = 
    Assert.Pass()