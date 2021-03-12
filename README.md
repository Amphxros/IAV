Bienvenido a la documentación de IAV.  
Este repositorio contiene todas las practicas agrupadas en diferentes proyectos, así como la documentación de cada una de ellas organizada en diferentes páginas.   

Está página contiene un resumen de las prácticas, para documentación detallada sobre el funcionamiento y su implementación abra la página lateral correspondiente.

Proyecto realizado por el grupo 10 de la asignatura de IAV del año 2020/2021 compuesto por:
|Amparo Rubio Bellón | Marcos Llinares Montes | Alejandro Plázer Arnaz | Liyuan Li |Jorge Zurdo Izquierdo|
|--|--|--|--|--|

# P1 Flautista de Hamelin

El prototipo muestra una demo de comportamientos de IA de movimiento:

* El Flautista de Hamelín contará con controles de dirección en manos del usuario y podrá tocar la flauta, que resultará en 2 comportamientos, el perro dejará de seguir al Flautista para huir de él, y las ratas comenzarán a seguirle.

* El Perro tendrá un comportamiento de seguimiento (Arrive) al flautista excepto cuando toque la flauta, que huirá de él (Leave).

* Las Ratas camparán a sus anchas hasta que el flautista toque la flauta, en ese caso se dirigirán hacia el flautista manteniendo un comportamiento de bandada (Steering Behavior) en torno a un puto medio (Boid) manteniendo una distancia para simular la cohesión
