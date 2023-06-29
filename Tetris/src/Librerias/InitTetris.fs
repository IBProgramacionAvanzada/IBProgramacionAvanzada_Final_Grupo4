namespace InitTetris

module Inicializar =
    open EstructuraDatosTetris.TiposTetris
    
    let ancho, alto = 10, 20
    let posicionInic = (ancho / 2 - 1, 0)
    let puntuacionPorLinea = 100

    let tiempoEntreCaidas = 1000. // en milisegundos
    let tiempoMinCaida = 100.

    let random = new System.Random ()
    let formaAleatoria() = formas.[random.Next(formas.Length)]

    let initTablero = {
        puntuacion = 0
        juegoTerminado = false

        caidaAnteriorTiempo = 0.
        lineasAEliminar = None
        
        bloquesEstaticos = []
        pos = posicionInic
        forma = formaAleatoria () |> Some
        proxForma = formaAleatoria ()
        eventos = []
    }

    