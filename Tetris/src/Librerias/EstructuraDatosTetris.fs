namespace EstructuraDatosTetris

module TiposTetris =
    
    type FormaBloque = 
        | X 
        | O

    type Color =
        | Rojo 
        | Magenta 
        | Amarillo
        | Cian 
        | Azul 
        | Plateado 
        | Verde

    type Evento = 
        | Movido 
        | Rotado 
        | Bloqueado 
        | Caido 
        | Linea 
        | FinJuego

    type Comando = 
        | Izquierda 
        | Derecha 
        | Rotar

    type Tablero = {
        puntuacion: int
        juegoTerminado: bool

        comandoAnteriorTiempo: float
        caidaAnteriorTiempo: float
        lineaAnteriorTiempo: float
        lineasAEliminar: (Color * int * int) list option

        bloquesEstaticos: (Color * int * int) list
        pos: int * int
        forma: (Color * FormaBloque list list) option
        proxForma: (Color * FormaBloque list list)
        eventos: Evento list
    }     
    let formas = [
        Cian, [[X;X]
               [X;X]]
        Rojo, [[X;X;X;X]]
        Verde, [[X;X;O]
                [O;X;X]]
        Azul, [[O;X;X]
               [X;X;O]]
        Amarillo, [[X;X;X]
                   [X;O;O]]
        Magenta, [[X;X;X]
                  [O;O;X]]
        Plateado, [[X;X;X]
                   [O;X;O]]
    ]
