/*    
   Copyright (C) 2020 Federico Peinado
   http://www.federicopeinado.com

   Este fichero forma parte del material de la asignatura Inteligencia Artificial para Videojuegos.
   Esta asignatura se imparte en la Facultad de Informática de la Universidad Complutense de Madrid (España).

   Autores originales: Opsive (Behavior Designer Samples)
   Revisión: Federico Peinado 
   Contacto: email@federicopeinado.com
*/

using UnityEngine;
using System.Collections.Generic;

namespace es.ucm.fdi.iav.rts.example1
{
    // El controlador táctico que proporciono de ejemplo... simplemente manda órdenes RANDOM, y no hace ninguna interpretación (localizar puntos de ruta bien, análisis táctico, acción coordinada...) 
    public class RTSAIControllerExample1 : RTSAIController
    {
        // No necesita guardar mucha información porque puede consultar la que desee por sondeo, incluida toda la información de instalaciones y unidades, tanto propias como ajenas

        // Mi índice de controlador y un par de instalaciones para referenciar
        private int MyIndex { get; set; }
        private BaseFacility MyBaseFacility { get; set; }
        private BaseFacility enemyBaseFacility { get; set; }
        private ProcessingFacility MyProcessingFacility { get; set; }

        // Número de paso de pensamiento 
        private int ThinkStepNumber { get; set; } = 0;

        // Última unidad creada
        private Unit LastUnit { get; set; }

        // Todos los movimientos que la IA puede hacer en cualquier jugada
        public enum MovementType
        {
            MoveRandomExtractor, MoveRandomExplorer, MoveRandomDestroyer,
            MoveAllExtractors, MoveAllExplorers, MoveAllDestroyer,
            MoveLastExtractor, MoveLastExplorer, MoveLastDestroyer
        }
        public List<MovementType> moves;
        private int currentMove = 0;

        private int MaxNumberOfExtractors;
        private int MaxNumberOfExplorers;
        private int MaxNumberOfDestroyers;

        // Lista de objetos en el juego
        // Ejercito de uno mismo
        // Unidades
        private List<ExtractionUnit> extractionUnits;
        private List<ExplorationUnit> explorationUnits;
        private List<DestructionUnit> destructionUnits;

        // Edificios
        private List<BaseFacility> baseFacilities;
        private List<ProcessingFacility> processingFacilities;

        // Ejercito
        // Unidades
        private List<ExtractionUnit> enemyExtractionUnits;
        private List<ExplorationUnit> enemyExplorationUnits;
        private List<DestructionUnit> enemyDestructionUnits;

        // Edificios
        private List<BaseFacility> enemyBaseFacilities;
        private List<ProcessingFacility> enemyProcessingFacilities;

        // Enemy Index
        private int EnemyIndex { get; set; }
        private ProcessingFacility enemyProcessingFacility;

        // Lista de accesos limitados
        private List<LimitedAccess> resources;
        // Lista de torretas
        private List<Tower> towers;

        // Despierta el controlado y configura toda estructura interna que sea necesaria
        private void Awake()
        {
            Name = "Example 1";
            Author = "Grupo 10";
        }

        // El método de pensar que sobreescribe e implementa el controlador, para percibir (hacer mapas de influencia, etc.) y luego actuar.
        protected override void Think()
        {
            // Actualizo el mapa de influencia 
            // ...

            // Para las órdenes aquí estoy asumiendo que tengo dinero de sobra y que se dan las condiciones de todas las cosas...
            // (Ojo: esto no debería hacerse porque si me equivoco, causaré fallos en el juego... hay que comprobar que cada llamada tiene sentido y es posible hacerla)

            // Aquí lo suyo sería elegir bien la acción a realizar. 
            // En este caso como es para probar, voy dando a cada vez una orden de cada tipo, todo de seguido y muy aleatorio...

            switch (ThinkStepNumber)
            {
                case 0: // El primer contacto, un paso especial
                    // Lo primer es conocer el índice que me ha asignado el gestor del juego
                    MyIndex = RTSGameManager.Instance.GetIndex(this);

                    // Obtengo referencias a mis cosas
                    MyBaseFacility = RTSGameManager.Instance.GetBaseFacilities(MyIndex)[0];
                    MyProcessingFacility = RTSGameManager.Instance.GetProcessingFacilities(MyIndex)[0];

                    // Obtengo referencias a las cosas de mi enemigo
                    var indexList = RTSGameManager.Instance.GetIndexes();
                    indexList.Remove(MyIndex);
                    EnemyIndex = indexList[0];
                    enemyBaseFacility = RTSGameManager.Instance.GetBaseFacilities(EnemyIndex)[0];
                    enemyProcessingFacility = RTSGameManager.Instance.GetProcessingFacilities(EnemyIndex)[0];

                    // Lista de accesos limitados
                    resources = RTSScenarioManager.Instance.LimitedAccesses;

                    ThinkStepNumber++;
                    // Construyo por primera vez el mapa de influencia (con las 'capas' que necesite)
                    // ...
                    break;

                case 1:
                    // Aqui elige si construir una nueva unidad
                    // My Army
                    // Unidades
                    extractionUnits = RTSGameManager.Instance.GetExtractionUnits(MyIndex);
                    explorationUnits = RTSGameManager.Instance.GetExplorationUnits(MyIndex);
                    destructionUnits = RTSGameManager.Instance.GetDestructionUnits(MyIndex);
                    // Edificios
                    baseFacilities = RTSGameManager.Instance.GetBaseFacilities(MyIndex);
                    processingFacilities = RTSGameManager.Instance.GetProcessingFacilities(MyIndex);
                    // Dinero
                    int MyMoney = RTSGameManager.Instance.GetMoney(MyIndex);

                    /*// Enemy Army
                    // Unidades
                    enemyExtractionUnits = RTSGameManager.Instance.GetExtractionUnits(EnemyIndex);
                    enemyExplorationUnits = RTSGameManager.Instance.GetExplorationUnits(EnemyIndex);
                    enemyDestructionUnits = RTSGameManager.Instance.GetDestructionUnits(EnemyIndex);
                    // Edificios
                    enemyBaseFacilities = RTSGameManager.Instance.GetBaseFacilities(EnemyIndex);
                    enemyProcessingFacilities = RTSGameManager.Instance.GetProcessingFacilities(EnemyIndex);

                    // Torretas
                    towers = RTSScenarioManager.Instance.Towers;*/


                    // Si hay al menos una base
                    if (baseFacilities.Count > 0)
                    {
                        // Tienen prioridad los extractores
                        if (extractionUnits.Count < RTSGameManager.Instance.ExtractionUnitsMax && RTSGameManager.Instance.GetMoney(MyIndex) > RTSGameManager.Instance.ExtractionUnitCost)
                        {
                            LastUnit = RTSGameManager.Instance.CreateUnit(this, MyBaseFacility, RTSGameManager.UnitType.EXTRACTION);
                        }
                        // Luego los exploradores
                        if (explorationUnits.Count < RTSGameManager.Instance.ExplorationUnitsMax && RTSGameManager.Instance.GetMoney(MyIndex) > RTSGameManager.Instance.ExplorationUnitCost)
                        {
                            LastUnit = RTSGameManager.Instance.CreateUnit(this, MyBaseFacility, RTSGameManager.UnitType.EXPLORATION);
                        }
                        // Luego los destructores
                        if (destructionUnits.Count < RTSGameManager.Instance.DestructionUnitsMax && RTSGameManager.Instance.GetMoney(MyIndex) > RTSGameManager.Instance.DestructionUnitCost)
                        {
                            LastUnit = RTSGameManager.Instance.CreateUnit(this, MyBaseFacility, RTSGameManager.UnitType.DESTRUCTION);
                        }
                    }
                    ThinkStepNumber++;
                    Debug.Log("Creando");
                    break;

                case 2:
                    // Aqui mueve unidades
                    // My Army
                    // Unidades
                    extractionUnits = RTSGameManager.Instance.GetExtractionUnits(MyIndex);
                    explorationUnits = RTSGameManager.Instance.GetExplorationUnits(MyIndex);
                    destructionUnits = RTSGameManager.Instance.GetDestructionUnits(MyIndex);
                    // Edificios
                    baseFacilities = RTSGameManager.Instance.GetBaseFacilities(MyIndex);
                    processingFacilities = RTSGameManager.Instance.GetProcessingFacilities(MyIndex);

                    /*// Enemy Army
                    // Unidades
                    enemyExtractionUnits = RTSGameManager.Instance.GetExtractionUnits(EnemyIndex);
                    enemyExplorationUnits = RTSGameManager.Instance.GetExplorationUnits(EnemyIndex);
                    enemyDestructionUnits = RTSGameManager.Instance.GetDestructionUnits(EnemyIndex);
                    // Edificios
                    enemyBaseFacilities = RTSGameManager.Instance.GetBaseFacilities(EnemyIndex);
                    enemyProcessingFacilities = RTSGameManager.Instance.GetProcessingFacilities(EnemyIndex);

                    // Torretas
                    towers = RTSScenarioManager.Instance.Towers;*/

                    switch (moves[currentMove])
                    {
                        case MovementType.MoveRandomExtractor:
                            break;
                        case MovementType.MoveRandomExplorer:
                            if (explorationUnits != null && explorationUnits.Count > 0)
                            {
                                // Selecciona uno al azar y lo mueve a la posicion
                                int ind = Random.Range(0, explorationUnits.Count);
                                RTSGameManager.Instance.MoveUnit(this, explorationUnits[ind], enemyBaseFacility.transform);
                            }
                            break;
                        case MovementType.MoveRandomDestroyer:
                            if (destructionUnits != null && destructionUnits.Count > 0)
                            {
                                // Selecciona uno al azar y lo mueve a la posicion
                                int ind = Random.Range(0, destructionUnits.Count);
                                RTSGameManager.Instance.MoveUnit(this, destructionUnits[ind], enemyBaseFacility.transform);
                            }
                            break;
                        case MovementType.MoveAllExtractors:
                            break;
                        case MovementType.MoveAllExplorers:
                            if (explorationUnits != null && explorationUnits.Count > 0)
                            {
                                // Recorre la lista y mueve todos a la posicion
                                foreach (Unit exploration in explorationUnits)
                                {
                                    RTSGameManager.Instance.MoveUnit(this, exploration, enemyBaseFacility.transform);
                                }
                            }
                            break;
                        case MovementType.MoveAllDestroyer:
                            if (destructionUnits != null && destructionUnits.Count > 0)
                            {
                                // Recorre la lista y mueve todos a la posicion
                                foreach (Unit destroyer in destructionUnits)
                                {
                                    RTSGameManager.Instance.MoveUnit(this, destroyer, enemyBaseFacility.transform);
                                }
                            }
                            break;
                        case MovementType.MoveLastExtractor:
                            break;
                        case MovementType.MoveLastExplorer:
                            if (explorationUnits != null && explorationUnits.Count > 0)
                            {
                                // Mueve la ultima unidad a la posicion
                                RTSGameManager.Instance.MoveUnit(this, explorationUnits[explorationUnits.Count - 1], enemyBaseFacility.transform);
                            }
                            break;
                        case MovementType.MoveLastDestroyer:
                            if (destructionUnits != null && destructionUnits.Count > 0)
                            {
                                // Mueve la ultima unidad a la posicion
                                RTSGameManager.Instance.MoveUnit(this, destructionUnits[destructionUnits.Count - 1], enemyBaseFacility.transform);
                            }
                            break;
                    }
                    currentMove = (currentMove + 1) % moves.Count;
                    ThinkStepNumber++;
                    Debug.Log("Moviendo");
                    break;

                case 3:
                    // Hace un análisis de la situación
                    // Enemy Army
                    // Unidades
                    enemyExtractionUnits = RTSGameManager.Instance.GetExtractionUnits(EnemyIndex);
                    enemyExplorationUnits = RTSGameManager.Instance.GetExplorationUnits(EnemyIndex);
                    enemyDestructionUnits = RTSGameManager.Instance.GetDestructionUnits(EnemyIndex);
                    // Edificios
                    enemyBaseFacilities = RTSGameManager.Instance.GetBaseFacilities(EnemyIndex);
                    enemyProcessingFacilities = RTSGameManager.Instance.GetProcessingFacilities(EnemyIndex);

                    bool noEnemyFacilities = enemyBaseFacilities == null || enemyBaseFacilities.Count <= 0;
                    bool noEnemyProcessingFacilities = enemyProcessingFacilities == null || enemyProcessingFacilities.Count <= 0;
                    bool noEnemyExtractors = enemyExtractionUnits == null || enemyExtractionUnits.Count <= 0;
                    bool noEnemyExplorers = enemyExplorationUnits == null || enemyExplorationUnits.Count <= 0;
                    bool noEnemyDestructors = enemyDestructionUnits == null || enemyDestructionUnits.Count <= 0;

                    if (noEnemyFacilities && noEnemyProcessingFacilities &&
                        noEnemyExtractors && noEnemyExplorers && noEnemyDestructors)
                    {
                        // El juego ha acabado y avisa
                        Stop = true;
                    }
                    else ThinkStepNumber++;

                    Debug.Log("Analizando");
                    break;
                case 4:
                    ThinkStepNumber = 0;

                    Debug.Log("Reiniciando");
                    break;
            }
        }
    }
}