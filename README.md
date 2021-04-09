# Practica 2: El hilo de Ariadna

Proyecto realizado por el grupo 10 de la asignatura de IAV del año 2020/2021 compuesto por:
|Amparo Rubio Bellón | Marcos Llinares Montes | Alejandro Plázer Arnaz | Liyuan Li |Jorge Zurdo Izquierdo|
|--|--|--|--|--|

El prototipo muestra la implementación del algoritmo de pathfinding conocido como A* (A-estrella o A-star)

* Teseo contará con controles de dirección en manos del usuario y podrá usar el hilo de Ariadna, cuando el jugador mantenga pulsado el espacio Teseo activará su pathfinding hasta la salida representando el camino visualmente como si fuese el hilo.
* El Minotauro merodeará por el laberinto de manera lenta hasta que Teseo cruce su línea de visión, en tal momento comenzará a perseguirlo a velocidad constante.
* El laberinto cuenta con múltiples caminos abiertos que permiten al algoritmo demostrar que siempre elige el camino más corto hacia el objetivo.

## Controles
El Jugador puede mover a Teseo con las flechas direccionales (← ↑ ↓ →) y usar el hilo manteniendo el espacio.
Al usar el Hilo el control pasa a ser automatizado por el juego usando A-Star y el Hilo se representa visualmente en el mapa cono un camino en blanco.

## Algoritmo
El algoritmo A-star una una técnica combinada de valores de nodos y heurística para hallar el camino más corto de la manera más eficiente.

Definiendo los nodos como cada uno de los puntos a explorar que tiene un valor asignado con un coste diferente (Terrenos mas dificiles de traversar),
el algoritmo busca la casilla más cercana a la que le cuesta menos desplazarse y que según su heurística es el más cercano.

El coste es un valor asociado al nodo, sirve para diferencia.

La Geometría Manhattan es una geometría que reemplaza la euclídea y nos permite calcular la distancia entre dos puntos expresado como el valor de sus coordenadas.
Como no podemos usar el camino más corto (Verde) al no poder atravesar un camino que no existe, nos vemos forzados a usar cualquier otra opción (Rojo, Azul o Amarillo) con una distancia total idéntica (Todos los caminos tienen 6 desplazamientos verticales y 6 horizontales)
![image](https://user-images.githubusercontent.com/18735746/113914108-6f8cd800-97dd-11eb-92db-8194a1eb986d.png)


La Heurística es un valor que define la distancia entre el agente y su meta, se usa como auxiliar para cuando tiene que elegir entre dos casillas con mismo coste para priorizar la solución más prometedora. Su valor se corresponde al coste manhattan total de llegar a ese nodo.

A la hora de calcular el valor de un nodo sumaremos su heurística y su coste para hallar el valor total del nodo.

En este ejemplo el desplazamiento a un nodo en diagonal tiene un coste de 14 y en vertical u horizontal de 10. Si sumamos el coste de 14 del nodo superior izquierdo con su heurística de 38 obtenemos un valor total de 42 dejándonos este valor como el más prometedor de los cercanos.

![image](https://user-images.githubusercontent.com/18735746/113913694-e9709180-97dc-11eb-84c5-565e5087638b.png)

Como podemos ver en el siguiente ejemple el algoritmo priorizará menores valores de heurística siempre que tenga que elegir entre nodos con valores totales iguales. (Tenía 3 opciones con valor total de 48 y ha elegido el de heurística 24)

![image](https://user-images.githubusercontent.com/18735746/113915767-774d7c00-97df-11eb-8de4-bb41006c0d3f.png)

## Implementación
Para la implementacion del algoritmo tenemos varias clases:

Vertex: Unidad mínima de representación en el laberinto, corresponde a cada uno de los puntos que forman el grafo.

Edge: "Vertice" especial que se está observando en busca de la salida más corta

Graph: Clase que maneja estos vértices y edges y que calcula mediante el algoritmo deseado el camino mas optimo(A star, Dijkstra...) y lo puede suavizar.

GraphGrid: Implementa el propio laberinto leído mediante archivo

BinaryHeap: Estructura que maneja estos Vertices

Después para manejar a cada uno de los personajes contamos con la clase Minotauro que aprovecha este algoritmo buscando una meta aleatoria siempre y cuando no vea a Teseo.
Teseo sin embargo, es controlado por el jugador, a menos que calcule el hilo que entonces seguira el camino automáticamente.

## Pruebas
Para probar esto hemos recurrido a una serie de archivos (mapa2.map para caminos rectos caso minimo,test1.map para un caso medio pero bastante basico y mapa3.map para un caso mas enreversado)

### Caso mínimo
![image](https://user-images.githubusercontent.com/37449976/114242544-5cb80600-998b-11eb-8e50-b6f05521bdf6.png)

### Caso medio
![image](https://user-images.githubusercontent.com/37449976/114242394-1d89b500-998b-11eb-9aa2-1c9b3142485a.png)

### Caso mas enreversado
![image](https://user-images.githubusercontent.com/37449976/114242636-86712d00-998b-11eb-969a-924f4a978670.png)

## Referencias 

Unity 2018 Artificial Intelligence Cookbook, Second Edition (Repositorio)
https://github.com/PacktPublishing/Unity-2018-Artificial-Intelligence-Cookbook-Second-Edition

A* Pathfinding (E01: algorithm explanation)
https://www.youtube.com/watch?v=-L-WgKMFuhE
