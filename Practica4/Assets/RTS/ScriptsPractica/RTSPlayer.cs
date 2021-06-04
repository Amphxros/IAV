using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace es.ucm.fdi.iav.rts{
    public class RTSPlayer : RTSAIController
    {
        public enum PosibleMovement
        {
            MoveRandomExtraction, MoveAllExtraction, MoveLastExtraction,
            MoveRandomExplorer, MoveAllExplorer, MoveLastExplorer,
            MoveRandomDestroyer, MoveAllDestroyer, MoveLastDestroyer
        }

        public int PersonalMaxExtractor;
        public int PersonalMaxExplorer;
        public int PersonalMaxDestroyer;

        public InfluenceMapController myMap;

        [SerializeField]
        // Los movimientos que estaran disponibles de verdad
        public List<PosibleMovement> Moves;
        private int nextMove = 0; // indice para ir eligiendo enumerados de movimiento
        
        private Unit movedUnit;


        // informacion del "tablero"
        private BaseFacility MyFirstBaseFacility { get; set; }
        private ProcessingFacility MyFirstProcessingFacility { get; set; }
        private BaseFacility FirstEnemyFirstBaseFacility { get; set; }
        private ProcessingFacility FirstEnemyFirstProcessingFacility { get; set; }

        // Mis listas completas de instalaciones y unidades
        private List<BaseFacility> Facilities;
        private List<ProcessingFacility> PFacilities;
        private List<ExtractionUnit> UnitsExtractList;
        private List<ExplorationUnit> UnitsExploreList;
        private List<DestructionUnit> UnitsDestroyerList;

        // Las listas completas de instalaciones y unidades del enemigo
        private List<BaseFacility> EnemyFacilities;
        private List<ProcessingFacility> EnemyPFacilities;
        private List<ExtractionUnit> EnemyUnitsExtractList;
        private List<ExplorationUnit> EnemyUnitsExploreList;
        private List<DestructionUnit> EnemyUnitsDestroyerList;

        // Las listas completas de accesos limitados y torretas 
        private List<LimitedAccess> resourcesList;
        private List<Tower> towersList;

        private int MyIndex { get; set; }
        private int FirstEnemyIndex { get; set; }

        private int ThinkStepNumber { get; set; } = 0;
        private Unit LastUnit { get; set; }

        public void Awake()
        {
            Name="Player-G10-IAV";
            Author= "Amparo Rubio Bellón";
            ThinkStepNumber=0;

            Moves = new List<PosibleMovement>();
            Moves.Add(PosibleMovement.MoveRandomExplorer);
        }

        protected override void Think(){
            // inicio de la partida
            if (ThinkStepNumber == 0)
            {
                // mis referencias
                ThinkStepNumber++;
                MyIndex = RTSGameManager.Instance.GetIndex(this);
                MyFirstBaseFacility = RTSGameManager.Instance.GetBaseFacilities(MyIndex)[0];
                MyFirstProcessingFacility = RTSGameManager.Instance.GetProcessingFacilities(MyIndex)[0];
                Debug.Log("referencias Pilladas");

                // mapa de influencia



            }
            else
            {
                Debug.Log("Iniciando Update");
                myMap.UpdateMap(MyIndex, FirstEnemyIndex);

                Facilities = RTSGameManager.Instance.GetBaseFacilities(MyIndex);
                PFacilities = RTSGameManager.Instance.GetProcessingFacilities(MyIndex);
                UnitsExtractList = RTSGameManager.Instance.GetExtractionUnits(MyIndex);
                UnitsExploreList = RTSGameManager.Instance.GetExplorationUnits(MyIndex);
                UnitsDestroyerList = RTSGameManager.Instance.GetDestructionUnits(MyIndex);

                EnemyFacilities = RTSGameManager.Instance.GetBaseFacilities(FirstEnemyIndex);
                EnemyPFacilities = RTSGameManager.Instance.GetProcessingFacilities(FirstEnemyIndex);
                EnemyUnitsExtractList = RTSGameManager.Instance.GetExtractionUnits(FirstEnemyIndex);
                EnemyUnitsExploreList = RTSGameManager.Instance.GetExplorationUnits(FirstEnemyIndex);
                EnemyUnitsDestroyerList = RTSGameManager.Instance.GetDestructionUnits(FirstEnemyIndex);

                towersList = RTSScenarioManager.Instance.Towers;
                Debug.Log("Cosas referenciadas");

                if (Facilities.Count > 0) // Si hay instalaciones base podemos construir
                {
                    bool[] canCreateUnits = new bool[3];
                    if (RTSGameManager.Instance.GetMoney(MyIndex) > RTSGameManager.Instance.ExtractionUnitCost)
                    {
                        canCreateUnits[0] = true;
                    }

                    if (RTSGameManager.Instance.GetMoney(MyIndex) > RTSGameManager.Instance.DestructionUnitCost)
                    {
                        canCreateUnits[1] = true;
                    }
                    if (RTSGameManager.Instance.GetMoney(MyIndex) > RTSGameManager.Instance.ExplorationUnitCost)
                    {
                        canCreateUnits[2] = true;
                    }

                    //no hay dinero para crear asi que podemos intentar ver si hay unidades para mover
                    if (!canCreateUnits[0] && !canCreateUnits[1] && !canCreateUnits[2])
                    {
                        Debug.Log("Sin blanca, vamos a mover");
                        bool thereAreUnits = false;
                        if (UnitsExtractList.Count > 0)
                        {
                            thereAreUnits = true;

                        }
                        if (UnitsExploreList.Count > 0)
                        {
                            thereAreUnits = true;

                        }
                        if (UnitsDestroyerList.Count > 0)
                        {
                            thereAreUnits = true;

                        }

                        if (!thereAreUnits)
                        {
                            // si no hemos podido mover unidades -> no habia unidades
                            // ademas en este caso no contaríamos con dinero para construirlas
                            // es decir tendríamos edificios pero ni unidades ni dinero para construirlas
                            Debug.Log("no hay unidades ni dinero para construirlas");
                        }

                        // en este caso como contamos con unidades pero no dinero debemos mover las que tenemos
                        else
                        {

                        }
                    }

                    // como podemos crear unidades podemos decidir si crear o mover las que tenemos
                    else
                    {
                        Debug.Log("Cosa del else");
                        // Se da prioridad a construir las unidades
                       // if (RTSGameManager.Instance.GetMoney(MyIndex) > RTSGameManager.Instance.ExtractionUnitCost && UnitsExtractList.Count < RTSGameManager.Instance.ExtractionUnitsMax)
                       // {
                       //     Debug.Log("Creando un nuevo extractor");
                       //     RTSGameManager.Instance.CreateUnit(this, MyFirstBaseFacility, RTSGameManager.UnitType.EXTRACTION);
                       // }
                       // if (RTSGameManager.Instance.GetMoney(MyIndex) > RTSGameManager.Instance.ExplorationUnitCost && UnitsExploreList.Count < RTSGameManager.Instance.ExplorationUnitsMax)
                       // {
                       //     Debug.Log("Creando un nuevo explorador");
                       //     RTSGameManager.Instance.CreateUnit(this, MyFirstBaseFacility, RTSGameManager.UnitType.EXPLORATION);
                       // }
                       // if (RTSGameManager.Instance.GetMoney(MyIndex) > RTSGameManager.Instance.DestructionUnitCost && UnitsDestroyerList.Count < RTSGameManager.Instance.DestructionUnitsMax)
                       // {
                       //     Debug.Log("Creando un nuevo destructor");
                       //     RTSGameManager.Instance.CreateUnit(this, MyFirstBaseFacility, RTSGameManager.UnitType.DESTRUCTION);
                       // }

                        Debug.Log("Debajo de los ifs");
                        // Luego elige un tipo de movimiento al azar y lo ejecuta
                       // switch (Moves[nextMove])
                       // {
                       //     case PosibleMovement.MoveRandomExtraction:
                       //         Debug.Log("Mueve un extractor x");
                       //         break;
                       //     case PosibleMovement.MoveRandomExplorer:
                       //         Debug.Log("Mueve un explorador x");
                       //         int exploradorID = Random.Range(0, UnitsExploreList.Count-1);
                       //         RTSGameManager.Instance.MoveUnit(this, UnitsExploreList[exploradorID], EnemyFacilities[0].transform);
                       //         break;
                       //     case PosibleMovement.MoveRandomDestroyer:
                       //         Debug.Log("Mueve un destructor x");
                       //         int destructorID = Random.Range(0, UnitsDestroyerList.Count-1);
                       //         RTSGameManager.Instance.MoveUnit(this, UnitsDestroyerList[destructorID], EnemyFacilities[0].transform);
                       //         break;
                       //     case PosibleMovement.MoveAllExtraction:
                       //         Debug.Log("Mueve todos los extractores");
                       //         break;
                       //     case PosibleMovement.MoveAllExplorer:
                       //         Debug.Log("Mueve todos los exploradores");
                       //         foreach (Unit explorador in UnitsExploreList)
                       //         {
                       //             RTSGameManager.Instance.MoveUnit(this, explorador, EnemyFacilities[0].transform);
                       //         }
                       //         break;
                       //     case PosibleMovement.MoveAllDestroyer:
                       //         Debug.Log("Mueve todos los destructores");
                       //         foreach (Unit destroyer in UnitsDestroyerList)
                       //         {
                       //             RTSGameManager.Instance.MoveUnit(this, destroyer, EnemyFacilities[0].transform);
                       //         }
                       //         break;
                       //     case PosibleMovement.MoveLastExtraction:
                       //         Debug.Log("Mueve al ultimo extractor");
                       //         break;
                       //     case PosibleMovement.MoveLastExplorer:
                       //         Debug.Log("Mueve al ultimo explorador");
                       //         break;
                       //     case PosibleMovement.MoveLastDestroyer:
                       //         Debug.Log("Mueve al ultimo destructor");
                       //         break;
                       //     default:
                       //         break;
                       // }
                        // Selecciona un valor arbitrario del enum
                        nextMove = NextMovement();
                        Debug.Log("Siguiente movimiento es " + nextMove);
                    }
                }
            }
            
        }
        private int NextMovement()
        {

            Debug.Log("Nesmuf");
            int posibleMovements = PosibleMovement.GetNames(typeof(PosibleMovement)).Length;
            return Random.Range(0, posibleMovements-1);
        }

    }
}
