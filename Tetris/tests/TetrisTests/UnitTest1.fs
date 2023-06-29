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
let ``Bloques no se superponen en el tablero`` () =
    // El tablero es de 10x20 celdas

    let formaCian = formas[0]
    let dropCian = traslacionBloques (0, 18) (snd formaCian)
    let formaAmarillo = formas[4]
    let rotAmarillo = snd formaAmarillo
                                                |> rotar
                                                |> rotar
                                                |> rotar
    let dropAmarillo = traslacionBloques (0, 15) rotAmarillo
    let formaGris = formas[6]
    let rotGris = snd formaGris
                                            |> rotar
                                            |> rotar
    let dropGris = traslacionBloques (2, 18) rotGris
    let formaVerde = formas[2]
    let rotVerde = snd formaVerde
                                            |> rotar
    let dropVerde = traslacionBloques (5, 17) rotVerde
    let estaticoCian = dropCian |> List.map (fun (x,y) -> fst formaCian, x, y)
    let estaticoAmarillo = dropAmarillo |> List.map (fun (x,y) -> fst formaAmarillo, x, y)
    let estaticoGris = dropGris |> List.map (fun (x,y) -> fst formaGris, x, y)
    let estaticoVerde = dropVerde |> List.map (fun (x,y) -> fst formaVerde, x, y)

    let unTablero = { initTablero with 
                                    bloquesEstaticos = estaticoCian @ estaticoAmarillo @ 
                                                        estaticoGris @ estaticoVerde 
                            }
    let unBloque = match unTablero.forma with 
                                                    | None -> (SinColor, [[]])
                                                    | Some B -> B
    let nuevoBloque = unBloque
                                        |> snd
                                        |> traslacionBloques unTablero.pos

    let resultado = seSuperponen nuevoBloque unTablero

    assert (resultado = false)

[<Test>]
let ``Bloques se superponen en el tablero`` () =
    // El tablero es de 10x20 celdas

    let formaCian = formas[0]
    let dropCian = traslacionBloques (0, 18) (snd formaCian)
    let formaAmarillo = formas[4]
    let rotAmarillo = snd formaAmarillo
                                                |> rotar
                                                |> rotar
                                                |> rotar
    let dropAmarillo = traslacionBloques (0, 15) rotAmarillo
    let formaGris = formas[6]
    let rotGris = snd formaGris
                                            |> rotar
                                            |> rotar
    let dropGris = traslacionBloques (2, 18) rotGris
    let formaVerde = formas[2]
    let rotVerde = snd formaVerde
                                            |> rotar
    let dropVerde = traslacionBloques (5, 17) rotVerde
    let estaticoCian = dropCian |> List.map (fun (x,y) -> fst formaCian, x, y)
    let estaticoAmarillo = dropAmarillo |> List.map (fun (x,y) -> fst formaAmarillo, x, y)
    let estaticoGris = dropGris |> List.map (fun (x,y) -> fst formaGris, x, y)
    let estaticoVerde = dropVerde |> List.map (fun (x,y) -> fst formaVerde, x, y)

    let unTablero = { initTablero with 
                                    bloquesEstaticos = estaticoCian @ estaticoAmarillo @ 
                                                        estaticoGris @ estaticoVerde 
                            }

    // Proponemos un bloque que se superpone, por una celda, con el estaticoGris                        
    let nuevoBloque = formaCian
                                        |> snd
                                        |> traslacionBloques (3, 18)

    let resultado = seSuperponen nuevoBloque unTablero

    assert (resultado = true)
    
[<Test>]
let ``Procesamiento de un comando: Izquierda`` () = 
    let unTablero = initTablero
    let esperado = { initTablero with 
                                pos = (3, 0)
                                eventos = [ MovimientoHorizontal ]
                         }
    let calculado = procesoComando (Some Izquierda) unTablero

    Assert.AreEqual(esperado, calculado)

[<Test>]
let ``Acelerar la caída de una ficha, paso de tiempo irrelevante`` () = 
    let pasoTiempo = 1000
    let unTablero = initTablero
    let esperado = { initTablero with 
                                pos = (4, 1)
                                caidaAnteriorTiempo = pasoTiempo
                                eventos = Lanzado::unTablero.eventos
                         }
    let calculado = lanzar pasoTiempo true unTablero

    Assert.AreEqual(esperado, calculado)

[<Test>]
let ``Acelerar la caída de una ficha, paso de tiempo 0k`` () = 
    let pasoTiempo = 2010
    let otroTablero = { initTablero with 
                                       puntuacion = 600
                                       caidaAnteriorTiempo = 900
                                       bloquesEstaticos = [(Cian, 0, 18); (Cian, 1, 18); (Cian, 0, 19); (Cian, 1, 19)] @
                                                          [(Amarillo, 0, 15); (Amarillo, 0, 16); (Amarillo, 0, 17); (Amarillo, 1, 17)] @
                                                          [(Gris, 3, 18); (Gris, 2, 19); (Gris, 3, 19); (Gris, 4, 19)] @
                                                          [(Verde, 6, 17); (Verde, 5, 18); (Verde, 6, 18); (Verde, 5, 19)]
                                       forma = Some (Verde, [[O; X]; [X; X]; [X; O]])
                                       pos = 7, 5
                                       eventos = [ MovimientoHorizontal; Rotacion; Rotacion; MovimientoHorizontal ]
                                 }
    let esperado = { otroTablero with 
                                pos = (7, 6)
                                caidaAnteriorTiempo = pasoTiempo
                                eventos = Lanzado::otroTablero.eventos
                             }
    let calculado = lanzar pasoTiempo false otroTablero

    Assert.AreEqual(esperado, calculado)

[<Test>]
let ``Acelerar la caída de una ficha, paso de tiempo insuficiente`` () = 
    let pasoTiempo = 500
    let esperado = initTablero
    let calculado = lanzar pasoTiempo false esperado  // devuelve el tablero sin cambios

    Assert.AreEqual(esperado, calculado)

[<Test>]
let ``Actualiza tablero y cual sera la proxima pieza`` () = 
    Assert.Pass()

[<Test>]
let ``Averigua las lineas que pueden ser dadas de baja, acá ninguna`` () = 
    let otroTablero = { initTablero with 
                                       puntuacion = 600
                                       caidaAnteriorTiempo = 900
                                       bloquesEstaticos = [(Cian, 0, 18); (Cian, 1, 18); (Cian, 0, 19); (Cian, 1, 19)] @
                                                          [(Amarillo, 0, 15); (Amarillo, 0, 16); (Amarillo, 0, 17); (Amarillo, 1, 17)] @
                                                          [(Gris, 3, 18); (Gris, 2, 19); (Gris, 3, 19); (Gris, 4, 19)] @
                                                          [(Verde, 6, 17); (Verde, 5, 18); (Verde, 6, 18); (Verde, 5, 19)]
                                       forma = Some (Verde, [[O; X]; [X; X]; [X; O]])
                                       pos = 7, 5
                                       eventos = [ MovimientoHorizontal; Rotacion; Rotacion; MovimientoHorizontal ]
                                 }
    let esperada: (Color * int * int) list = initTablero.bloquesEstaticos  // usado como una especie de None
    let calculada = obtengoLineas otroTablero

    Assert.AreEqual(esperada, calculada)

[<Test>]
let ``Averigua las lineas que pueden ser dadas de baja, acá hay 2`` () = 
    let tableroCon2Lineas = { initTablero with 
                                          puntuacion = 600
                                          caidaAnteriorTiempo = 1000
                                          bloquesEstaticos = [(Cian, 0, 18); (Cian, 1, 18); (Cian, 0, 19); (Cian, 1, 19)] @
                                                             [(Amarillo, 0, 15); (Amarillo, 0, 16); (Amarillo, 0, 17); (Amarillo, 1, 17)] @
                                                             [(Gris, 3, 18); (Gris, 2, 19); (Gris, 3, 19); (Gris, 4, 19)] @
                                                             [(Verde, 6, 17); (Verde, 5, 18); (Verde, 6, 18); (Verde, 5, 19)] @
                                                             [(Rojo, 7, 19); (Rojo, 7, 18); (Rojo, 7, 17); (Rojo, 7, 16)] @
                                                             [(Cian, 8, 18); (Cian, 9, 18); (Cian, 8, 19); (Cian, 9, 19)] @
                                                             [(Cian, 8, 17); (Cian, 9, 17); (Cian, 8, 16); (Cian, 9, 16)] @
                                                             [(Azul, 3, 16); (Azul, 3, 17); (Azul, 4, 17); (Azul, 4, 18)] @
                                                             [(Gris, 5, 17); (Gris, 4, 16); (Gris, 5, 16); (Gris, 6, 16)] @
                                                             [(Rojo, 2, 18); (Rojo, 2, 17); (Rojo, 2, 16); (Rojo, 2, 15)]
                                          forma = Some (Verde, [[O; X]; [X; X]; [X; O]])
                                          pos = 7, 5
                                          eventos = []
                                      }
    let esperada: (Color * int * int) list = [(Cian, 0, 18); (Cian, 1, 18); (Gris, 3, 18); (Verde, 5, 18); (Verde, 6, 18);
                                              (Rojo, 7, 18); (Cian, 8, 18); (Cian, 9, 18); (Azul, 4, 18); (Rojo, 2, 18);
                                              (Amarillo, 0, 17); (Amarillo, 1, 17); (Verde, 6, 17); (Rojo, 7, 17);
                                              (Cian, 8, 17); (Cian, 9, 17); (Azul, 3, 17); (Azul, 4, 17); (Gris, 5, 17);
                                              (Rojo, 2, 17)]
    let calculada = obtengoLineas tableroCon2Lineas

    Assert.AreEqual(esperada, calculada)

[<Test>]
let ``Elimina las lineas completadas del tablero y suma puntos`` () = 
    let hayTablero = { initTablero with 
                                          puntuacion = 600
                                          caidaAnteriorTiempo = 1000
                                          bloquesEstaticos = [(Cian, 0, 18); (Cian, 1, 18); (Cian, 0, 19); (Cian, 1, 19)] @
                                                             [(Amarillo, 0, 15); (Amarillo, 0, 16); (Amarillo, 0, 17); (Amarillo, 1, 17)] @
                                                             [(Gris, 3, 18); (Gris, 2, 19); (Gris, 3, 19); (Gris, 4, 19)] @
                                                             [(Verde, 6, 17); (Verde, 5, 18); (Verde, 6, 18); (Verde, 5, 19)] @
                                                             [(Rojo, 7, 19); (Rojo, 7, 18); (Rojo, 7, 17); (Rojo, 7, 16)] @
                                                             [(Cian, 8, 18); (Cian, 9, 18); (Cian, 8, 19); (Cian, 9, 19)] @
                                                             [(Cian, 8, 17); (Cian, 9, 17); (Cian, 8, 16); (Cian, 9, 16)] @
                                                             [(Azul, 3, 16); (Azul, 3, 17); (Azul, 4, 17); (Azul, 4, 18)] @
                                                             [(Gris, 5, 17); (Gris, 4, 16); (Gris, 5, 16); (Gris, 6, 16)] @
                                                             [(Rojo, 2, 18); (Rojo, 2, 17); (Rojo, 2, 16); (Rojo, 2, 15)]
                                          forma = Some (Verde, [[O; X]; [X; X]; [X; O]])
                                          pos = 7, 5
                                          lineasAEliminar = Some [(Cian, 0, 18); (Cian, 1, 18); (Gris, 3, 18); (Verde, 5, 18); (Verde, 6, 18);
                                                                 (Rojo, 7, 18); (Cian, 8, 18); (Cian, 9, 18); (Azul, 4, 18); (Rojo, 2, 18);
                                                                 (Amarillo, 0, 17); (Amarillo, 1, 17); (Verde, 6, 17); (Rojo, 7, 17);
                                                                 (Cian, 8, 17); (Cian, 9, 17); (Azul, 3, 17); (Azul, 4, 17); (Gris, 5, 17);
                                                                 (Rojo, 2, 17)]
                                          eventos = []
                                      }
    let tableroLimpiado = { 
                                        puntuacion = 800
                                        juegoTerminado = false
                                        caidaAnteriorTiempo = 1000.0
                                        lineasAEliminar = None
                                        bloquesEstaticos = [(Cian, 0, 19); (Cian, 1, 19); (Amarillo, 0, 17); (Amarillo, 0, 18);
                                                            (Gris, 2, 19); (Gris, 3, 19); (Gris, 4, 19); (Verde, 5, 19); (Rojo, 7, 19);
                                                            (Rojo, 7, 18); (Cian, 8, 19); (Cian, 9, 19); (Cian, 8, 18); (Cian, 9, 18);
                                                            (Azul, 3, 18); (Gris, 4, 18); (Gris, 5, 18); (Gris, 6, 18); (Rojo, 2, 18);
                                                            (Rojo, 2, 17)]
                                        pos = (7, 5)
                                        forma = Some (Verde, [[O; X]; [X; X]; [X; O]])
                                        proxForma = (Rojo, [[X; X; X; X]])
                                        eventos = [] 
                                    }
    let esperado = tableroLimpiado
    let calculado = eliminoLineas 1000 tableroLimpiado
    
    Assert.AreEqual(esperado, calculado)

[<Test>]
let ``Avanza el estado del juego`` () = 
    Assert.Pass()