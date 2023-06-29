# IBProgramacionAvanzada_Final_Grupo4
Repositorio para el trabajo fina de la materia. 
Funciones para el clásico juego de Tetris, implementadas en F#.

Integrantes del grupo: Camila Soria, Roberto Alfredo Chaparro


# Tetris

En el Tetris el jugador intenta completar líneas horizontales, mientras mueve piezas de formas diferentes (todas de cuatro celdas), las cuales van descendiendo en el tablero y se van acumulando en la parte inferior. 
Las líneas completadas desaparecen otorgando un puntaje al jugador. Las vacantes dejadas por las filas (líneas horizontales) anteriores son aprovechadas por el jugador para seguir sumando puntaje. 
El juego termina cuando las líneas incompletas alcanzan el tope del tablero. Cuanto más tiempo logre el jugador demorar este evento, más puntaje habrá sumado.  


## Guía para entender los archivos

A continuación se describen los 5 archivos principales del proyecto. Siguiendo las consignas del trabajo final, los mismos solo constan de código en F#.


### EstructuraDeDatosTetris.fs

Este archivo tiene la declaración de los tipos necesarios para modelar el dominio del juego. El estado del juego queda representado por el tipo Tablero.


### FuncionesTetris.fs

Acá es donde se declaran las funciones que permiten la evolución del estado del juego. Se declaran las funciones para accionar sobre las piezas (rotar, mover y lanzar). Se implementan funciones para verificar la posición relativa entre piezas y vigilar los márgenes del tablero. Tambien están las funciones que verifican cuándo hay líneas para borrar y asignar puntaje al jugador. 


### InitTetris.fs

En este lugar se declaran constantes para usar en el resto de la solución de .NET. También se inicializa el Tablero.


### Program.fs

De aplicarse el GameLoop, es en este archivo donde se instanciaría la llamada a las funciones asíncronas correspondientes. Por consigna del trabajo, no se aplican esas funciones en este caso.


### UnitTest1.fs

Como lo indica el nombre, acá es donde se hacen los tests unitarios sobre las funciones declaradas antes.

