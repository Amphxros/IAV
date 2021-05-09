## P3: El fantasma de la opera

Proyecto realizado por el grupo 10 de la asignatura de IAV del año 2020/2021 compuesto por:
|Amparo Rubio Bellón | Marcos Llinares Montes | Alejandro Plázer Arnaz | Liyuan Li |Jorge Zurdo Izquierdo|
|--|--|--|--|--|

**Version de Unity: 2020.1.4**
[Enlace al repositorio](https://github.com/amprubio/IAV-G10)

Este proyecto muestra la implementación de una IA capaz de tomar decisiones mediante la implementación de arboles de comportamiento y maquinas de estado. Para ello se ha representado la obra del fantasma de la opera en forma de "videojuego".
## Introduccion
![image](https://user-images.githubusercontent.com/18735746/117212182-4e2e0480-adfa-11eb-8660-8c55e7a517a8.png)

Esquema del teatro

El escenario se representa mediante un grafo donde se conectan las estancias tal y como se puede observar en la imagen donde se encuentran los personajes de nuestra obra:

- **El vizconde:**  Representa al jugador, se mueve con los controles
- **La cantante:** Es el personaje que nos toca salvar, sin embargo mientras tiene que hacer su trabajo, cantar en el escenario, cada X tiempo se tomará un descanso en las bambalinas y podrá ser secuestrada por el fantasma mientras no haya público mirando.
- **El fantasma:** Su principal objetivo será secuestrar a la cantante sin embargo para ello tendrá que espantar al público si ella está cantando

## Mecánicas y controles
* **Movimiento [Flechas o WASD]:** moverá el vizconde por el mapa.
* **Interactuar [E]:** interactuará con diferentes objetos, arreglando las lamparas o golpeando los instrumentos del fantasma.

### Sistema de cámaras.
Esta demo tiene una serie de cámaras que podemos cambiar con distintas teclas:
* **Cam Jugador[P]**

![image](https://user-images.githubusercontent.com/18735746/117212294-7453a480-adfa-11eb-96d5-b11627ca317f.png)

* **Cam Cantante[O]**

![image](https://user-images.githubusercontent.com/18735746/117212583-ce546a00-adfa-11eb-9956-564e2cfaa58b.png)

* **Cam Fantasma[I]**

![image](https://user-images.githubusercontent.com/18735746/117212684-ef1cbf80-adfa-11eb-8498-28293acdd9a4.png)

* **Cam General[U]**

![image](https://user-images.githubusercontent.com/18735746/117212516-bd0b5d80-adfa-11eb-896e-e356ebded62a.png)

## Implementación

### Vizconde
Se mueve gracias al componente Avatar, proporcionado por el proyecto base. Además cuenta con un componente interactuar que le permite interactuar con el entorno arreglando lamparas, rompiendo el piano de la sala de música y golpeando al vizconde cuando este esté secuestrando a la cantante.

### Fantasma
Se mueve gracias a la navMesh entre habitaciones mediante un árbol de comportamiento hecho en behaviour designer. Básicamente su objetivo principal es secuestrar a la cantante, para ello si esta se encuentra en el escenario tirará las lamparas del escenario para espantar al público y así raptarla. Sin embargo, en caso de que se encuentre en las bambalinas podrá secuestrarla directamente 


![image](https://user-images.githubusercontent.com/37449976/117587866-459e3c80-b120-11eb-9f93-639a039c84a8.png)

### Cantante
Se mueve gracias a la navMesh entre habitaciones mediante un árbol de comportamiento hecho en behaviour designer. Cuando la secuestran ella sigue al fantasma hasta su objetivo sin embargo si ve al vizconde esta le seguirá hasta reconocer su lugar de trabajo, en caso contrario, rotará entre cantar par el público y descansar en las bambalinas.

![image](https://user-images.githubusercontent.com/37449976/117587832-1f789c80-b120-11eb-8aa5-d8354298e496.png)


### Público
Cuenta con un árbol de comportamiento muy básico, si hay lamparas estos estarán en el patio de butacas observando la función, si no, se irán al vestibulo hasta que estas estén arregladas.



 
### Sistema de barcas
Para conectar las habitaciones donde el fantasma habita con el resto del teatro, éste emplea una serie de barcas que se conectan en los inundados pasos. Estas barcas están implementadas utilizando atajos en la navmesh, que el fantasma utiliza para saltar de habitación a habitación, pero dando la impresión de que está usando las barcas para moverse.
Para que esto sea posible, las barcas se componen cada una de varios componentes:
- Dos GameObjects por barca, situadas a ambos lados del río que conectan, cada una con un script Barca que controla su comportamiento individual.
- Las dos habitaciones que se conectan a través de las barcas tienen cada una un OffMeshLink, para conectarlas a pesar de la falta de un camino
  en la NavMesh.
![imagen](https://user-images.githubusercontent.com/37513637/117562616-91a99c80-b0a0-11eb-9412-8b8adb1924e8.png)

![imagen](https://user-images.githubusercontent.com/37513637/117562624-a71ec680-b0a0-11eb-93bf-e01bef46c847.png)

![imagen](https://user-images.githubusercontent.com/37513637/117562635-b30a8880-b0a0-11eb-86b9-238c55a9021c.png)

Finalmente, todas las barcas y atajos son controlados por un Manager que gestiona sus funciones cuando un objeto (por ejemplo, el fantasma o el vizconde) entran en contacto con ellas. En ambos casos, los que están entrando en contacto con la barca serán transportados al otro lado del río, y la barca por donde llegaron será escondida, mientras la barca más cercana a ellos se hará visible, creando esta impresión de movimiento de la barca.

* El truco visual que se emplea en cada barca

![imagen](https://user-images.githubusercontent.com/37513637/117562645-c9184900-b0a0-11eb-8c0b-97a23754291b.png)

* La navmesh con los atajos incluidos

![imagen](https://user-images.githubusercontent.com/37513637/117562666-fc5ad800-b0a0-11eb-813a-402793edb480.png)

## Pruebas
Primero de todo realizamos una serie de secuencias para probar funciones principales, tales como "el fantasma ve a la cantante" ya que era la base para poder secuestrarla. Después que este la buscara de forma aleatoria entre el teatro hasta que volviendo con el primer punto, la viera. Una vez teniendo en cuenta estos puntos pasamos a que el fantasma tirase las lamparas en caso de que la cantante no estuviera en las bambalinas. A continuación estuvimos en paralelo probando el sistema de barcas de modo que los personajes pudieran moverse, e sistema de cámaras y que el vizconde arreglase las lamparas, además de el comportamiento del público. Después continuamos con el secuestro de la cantante de modo que esta le siguiera una vez fuera secuestrada pudiera ser rescatada por el vizconde y traída de vuelta a su lugar de trabajo.

## Referencias
* Assets : [Kenney's page](https://www.kenney.nl/assets)
* Tools: [Behaviour designer](https://assetstore.unity.com/packages/tools/visual-scripting/behavior-designer-behavior-trees-for-everyone-15277)

