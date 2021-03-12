# P1 Flautista de Hamelin

## Introduccion

Proyecto realizado por el grupo 10 de la asignatura de IAV del año 2020/2021 compuesto por:
|Amparo Rubio | Marcos Llinares | Alejandro | Liyuan Li |Jorge Zurdo Izquierdo|
|--|--|--|--|--|

El prototipo muestra una demo de comportamientos de IA de movimiento:

* El Flautista de Hamelín contará con controles de dirección en manos del usuario y podrá tocar la flauta, que resultará en 2 comportamientos, el perro dejará de seguir al Flautista para huir de él, y las ratas comenzarán a seguirle.

* El Perro tendrá un comportamiento de seguimiento (Arrive) al flautista excepto cuando toque la flauta, que huirá de él (Leave).

* Las Ratas camparán a sus anchas hasta que el flautista toque la flauta, en ese caso se dirigirán hacia el flautista manteniendo un comportamiento de bandada (Steering Behavior) en torno a un puto medio (Boid) manteniendo una distancia para simular la cohesión

## Controles
El jugador puede mover al Flautista con las flechas direccionales (← ↑ ↓ →) y tocar la flauta manteniendo el espacio.  
Tocar la flauta envía un mensaje a todos los oyentes para que alteren sus comportamientos

## Comportamientos

* Seek: Busca la posición del objetivo y hace que el objeto se dirija a el con la mayor aceleración posible. La aceleración y velocidades máximas están limitadas para evitar tender a infinito con el tiempo. Un objetivo en movimiento puede dar resultado en una orbita alrededor del Objetivo.
* Flee: Virtualmente idéntico al Seek pero siempre elige la dirección contraria al Objetivo.  
![image](https://user-images.githubusercontent.com/18735746/110780687-3fe1c300-8265-11eb-97e6-ff766e668bc1.png)
* Arrive: Añade un factor de deceleración respecto a un radio al objetivo para evitar la orbita alrededor.  
![image](https://user-images.githubusercontent.com/18735746/110780787-5e47be80-8265-11eb-8098-2e9e34995d9c.png)

## Comportamientos Adicionales