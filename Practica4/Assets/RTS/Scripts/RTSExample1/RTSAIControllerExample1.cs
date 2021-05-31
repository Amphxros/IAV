using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace es.ucm.fdi.iav.rts
{
    public class RTSAIControllerExample1 : RTSAIController
    {
        // El estilo para las etiquetas de la interfaz
        private GUIStyle _labelStyle;
        private GUIStyle _labelSmallStyle;

        // Todos los posibles objetivos que puede poner a sus movimientos este IA
        public enum PosibleObjective
        { // Tama�o = 38
            // Ninguno
            RandomObjective,
            // Edificios Enemigos
            FurthestEnemyBase, FurthestEnemyProcessinFacility, ClosestEnemyBase, ClosestEnemyProcessingFacility,
            // Edificios Amigos
            FurthestBase, FurthestProcessingFacility, ClosestBase, ClosestProcessingFacility,
            // Zonas de extraccion
            FurthestResourceZone, ClosestResourceZone,
            // Unidades Amigas
            ClosestExtractor,  ClosestExplorer, ClosestDestroyer, ClosestRandomFriendlyUnit,
            LastExtractor, LastExplorer, LastDestroyer, LastRandomFriendlyUnit,
            FurthestExtractor, FurthestExplorer, FurthestDestroyer, FurthestRandomFriendlyUnit,
            // Unidades Enemigas
            ClosestEnemyExtractor, ClosestEnemyExplorer, ClosestEnemyDestroyer, ClosestRandomEnemyUnity,
            LastEnemyExtractor, LastEnemyExplorer, LastEnemyDestroyer, LastRandomEnemyUnit,
            FurthestEnemyExtractor, FurthestEnemyExplorer, FurthesEnemytDestroyer, FurthestRandomEnemyUnit,
            // Torres
            FurthestTorret, ClosestTorret, RandomTorret
        }

        private List<PosibleObjective> objetivos;
        private int nextObjective = 0;

        public int PersonalMaxExtractor;
        public int PersonalMaxExplorer;
        public int PersonalMaxDestroyer;

        public enum PosibleMovement
        {
            MoveRandomExtraction, MoveAllExtraction, MoveLastExtraction,
            MoveRandomExplorer, MoveAllExplorer, MoveLastExplorer,
            MoveRandomDestroyer, MoveAllDestroyer, MoveLastDestroyer
        }

        // Los movimientos que estaran disponibles de verdad
        private List<PosibleMovement> Moves;
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
            Name = "Player-G10-IAV";
            Author = "Amparo Rubio Bell�n";
            ThinkStepNumber = 0;

            objetivos = new List<PosibleObjective>();
            PosibleObjective[] objs = (PosibleObjective[])PosibleObjective.GetValues(typeof(PosibleObjective));
            for (int i = 1; i < objs.Length; i++)
            {
                objetivos.Add(objs[i]);
            }

            Moves = new List<PosibleMovement>();
            PosibleMovement[] moves = (PosibleMovement[])PosibleMovement.GetValues(typeof(PosibleMovement));
            for (int i = 0; i < moves.Length; i++)
            {
                Moves.Add(moves[i]);
            }

            // Aumenta el tama�o y cambia el color de fuente de letra para OnGUI (amarillo para las IAs)
            _labelStyle = new GUIStyle();
            _labelStyle.fontSize = 16;
            _labelStyle.normal.textColor = Color.yellow;

            _labelSmallStyle = new GUIStyle();
            _labelSmallStyle.fontSize = 11;
            _labelSmallStyle.normal.textColor = Color.yellow;
        }

        protected override void Think()
        {
            // inicio de la partida
            if (ThinkStepNumber == 0)
            {
                // mis referencias
                MyIndex = RTSGameManager.Instance.GetIndex(this);
                MyFirstBaseFacility = RTSGameManager.Instance.GetBaseFacilities(MyIndex)[0];
                MyFirstProcessingFacility = RTSGameManager.Instance.GetProcessingFacilities(MyIndex)[0];

                // mapa de influencia

            }
            else
            {
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
                resourcesList = RTSScenarioManager.Instance.LimitedAccesses;

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
                            // ademas en este caso no contar�amos con dinero para construirlas
                            // es decir tendr�amos edificios pero ni unidades ni dinero para construirlas
                            Debug.Log("no hay unidades ni dinero para construirlas");
                        }

                        // en este caso como contamos con unidades pero no dinero debemos mover las que tenemos
                        else
                        {
                            MoveUnits();
                        }
                    }

                    // como podemos crear unidades podemos decidir si crear o mover las que tenemos
                    else
                    {
                        // Se da prioridad a construir las unidades
                        CreateUnits();
                        // Luego elige un tipo de movimiento al azar y lo ejecuta
                        MoveUnits();
                    }
                }
            }
            ThinkStepNumber++;
        }

        private void CreateUnits()
        {
            // Prioridad a los extractores, luego exploradores, finalmente destructores
            if (RTSGameManager.Instance.GetMoney(MyIndex) > RTSGameManager.Instance.ExtractionUnitCost && UnitsExtractList.Count < RTSGameManager.Instance.ExtractionUnitsMax)
            {
                // Aqui crea un extractor y lo mueve a la zona de recursos mas cercanas
                RTSGameManager.Instance.CreateUnit(this, MyFirstBaseFacility, RTSGameManager.UnitType.EXTRACTION);
                RTSGameManager.Instance.MoveUnit(this, UnitsExtractList[UnitsExtractList.Count - 1], ChooseObjective(UnitsExtractList[UnitsExtractList.Count - 1].transform, PosibleObjective.ClosestResourceZone).transform);
            }
            if (RTSGameManager.Instance.GetMoney(MyIndex) > RTSGameManager.Instance.ExplorationUnitCost && UnitsExploreList.Count < RTSGameManager.Instance.ExplorationUnitsMax)
            {
                //Debug.Log("Creando un nuevo explorador");
                RTSGameManager.Instance.CreateUnit(this, MyFirstBaseFacility, RTSGameManager.UnitType.EXPLORATION);
            }
            if (RTSGameManager.Instance.GetMoney(MyIndex) > RTSGameManager.Instance.DestructionUnitCost && UnitsDestroyerList.Count < RTSGameManager.Instance.DestructionUnitsMax)
            {
                //Debug.Log("Creando un nuevo destructor");
                RTSGameManager.Instance.CreateUnit(this, MyFirstBaseFacility, RTSGameManager.UnitType.DESTRUCTION);
            }
        }

        private void MoveUnits()
        {
            switch (Moves[nextMove])
            {
                case PosibleMovement.MoveRandomExtraction:
                    int extractorId = Random.Range(0, UnitsExtractList.Count);

                    // Solo mueve un extractor si no esta del todo lleno
                    if (UnitsExtractList[extractorId].Resources < UnitsExtractList[extractorId].ExtractableAmmount)
                    {
                        // Hacer que la posibilidad sea peque�a para que no haga este movimiento muchas veces
                        if (Posibilidad(25) > 24)
                        {
                            // Si entra, lo mueve a la zona de extraccion mas cercana
                            RTSGameManager.Instance.MoveUnit(this, UnitsExtractList[extractorId], ChooseObjective(UnitsExtractList[extractorId].transform, PosibleObjective.ClosestResourceZone).transform);
                        }
                    }
                    break;
                case PosibleMovement.MoveRandomExplorer:
                    // Solo mueve exploradores si al menos hay 2 exploradores mas que mantendran su posicion defensiva
                    if (UnitsExploreList.Count > 2)
                    {
                        int exploradorId = Random.Range(0, UnitsExploreList.Count);
                        RTSGameManager.Instance.MoveUnit(this, UnitsExploreList[exploradorId], ChooseObjective(UnitsExtractList[exploradorId].transform, PosibleObjective.RandomObjective));
                    }
                    break;
                case PosibleMovement.MoveRandomDestroyer:
                    // Solo mueve destructores si hay al menos 1 destructor mas que se quedan en su posicion actual
                    if (UnitsDestroyerList.Count > 1)
                    {
                        int destructorID = Random.Range(0, UnitsDestroyerList.Count);
                        RTSGameManager.Instance.MoveUnit(this, UnitsDestroyerList[destructorID], ChooseObjective(UnitsExtractList[destructorID].transform, PosibleObjective.RandomObjective));
                    }
                    break;
                case PosibleMovement.MoveAllExtraction:
                    // Posibilidad extremadamente peque�a
                    if (Posibilidad(100) > 99)
                    {
                        foreach (ExtractionUnit extractor in UnitsExtractList)
                        {
                            // Solo si no estan llenos ya
                            if (extractor.Resources < extractor.ExtractableAmmount)
                            {
                                // Lo mueve a la zona de extraccion mas cercana
                                RTSGameManager.Instance.MoveUnit(this, extractor, ChooseObjective(extractor.transform, PosibleObjective.ClosestResourceZone));
                            }
                        }
                    }
                    break;
                case PosibleMovement.MoveAllExplorer:
                    // Mueve todos los exploradores que existan excepto dos que deja a la retaguardia
                    for (int i = 0; i < UnitsExploreList.Count - 2; i++)
                    {
                        RTSGameManager.Instance.MoveUnit(this, UnitsExploreList[i], ChooseObjective(UnitsExploreList[i].transform, PosibleObjective.RandomObjective));
                    }
                    break;
                case PosibleMovement.MoveAllDestroyer:
                    // Mueve todos los destructores que existan excepto uno que deja a la retaguardia
                    for (int i = 0; i < UnitsDestroyerList.Count - 1; i++)
                    {
                        RTSGameManager.Instance.MoveUnit(this, UnitsDestroyerList[i], ChooseObjective(UnitsDestroyerList[i].transform, PosibleObjective.RandomObjective));
                    }
                    break;
                case PosibleMovement.MoveLastExtraction:
                    // Mueve el ultimo extractor a la zona de extraccion mas lejana
                    RTSGameManager.Instance.MoveUnit(this, UnitsExtractList[UnitsExtractList.Count - 1], ChooseObjective(UnitsExtractList[UnitsExtractList.Count - 1].transform, PosibleObjective.FurthestResourceZone));
                    break;
                case PosibleMovement.MoveLastExplorer:
                    RTSGameManager.Instance.MoveUnit(this, UnitsExploreList[UnitsExploreList.Count - 1], ChooseObjective(UnitsExploreList[UnitsExploreList.Count - 1].transform, PosibleObjective.RandomObjective));
                    break;
                case PosibleMovement.MoveLastDestroyer:
                    RTSGameManager.Instance.MoveUnit(this, UnitsDestroyerList[UnitsDestroyerList.Count - 1], ChooseObjective(UnitsDestroyerList[UnitsDestroyerList.Count - 1].transform, PosibleObjective.RandomObjective));
                    break;
            }

            // Selecciona un valor arbitrario del enum
            // Evita realizar el mismo movimiento dos veces consecutivas
            int move = nextMove;
            while (move == nextMove)
            {
                nextMove = Random.Range(0, PosibleMovement.GetNames(typeof(PosibleMovement)).Length);
            }
        }

        private int Posibilidad(int n)
        {
            int posibilidad = Random.Range(0, n);

            return posibilidad;
        }

        // Recibe un origen y devuelve un objetivo en una posicion relativa a dicho origen
        private Transform ChooseObjective(Transform origen, PosibleObjective obj)
        {
            Transform objetivo = origen;
            int rand = -1;

            PosibleObjective val;
            // Puede recibir un objetivo en especifico
            if (obj != PosibleObjective.RandomObjective)
            {
                val = obj;
            }
            // o no
            else {
                nextObjective = Random.Range(0, PosibleObjective.GetNames(typeof(PosibleObjective)).Length);
                val = objetivos[nextObjective];
            }
            switch (val)
            {
                case PosibleObjective.FurthestEnemyBase:
                    if (EnemyFacilities != null && EnemyFacilities.Count > 0)
                    {
                        objetivo = GetNewObjective(EnemyFacilities.ToArray(), origen, false).transform;
                    }
                    break;
                case PosibleObjective.FurthestEnemyProcessinFacility:
                    if (EnemyPFacilities != null && EnemyPFacilities.Count > 0)
                    {
                        objetivo = GetNewObjective(EnemyPFacilities.ToArray(), origen, false).transform;
                    }
                    break;
                case PosibleObjective.ClosestEnemyBase:
                    if (EnemyFacilities != null && EnemyFacilities.Count > 0)
                    {
                        objetivo = GetNewObjective(EnemyFacilities.ToArray(), origen, true).transform;
                    }
                    break;
                case PosibleObjective.ClosestEnemyProcessingFacility:
                    if (EnemyPFacilities != null && EnemyPFacilities.Count > 0)
                    {
                        objetivo = GetNewObjective(EnemyPFacilities.ToArray(), origen, true).transform;
                    }
                    break;
                case PosibleObjective.FurthestBase:
                    if (Facilities != null && Facilities.Count > 0)
                    {
                        objetivo = GetNewObjective(Facilities.ToArray(), origen, false).transform;
                    }
                    break;
                case PosibleObjective.FurthestProcessingFacility:
                    if (PFacilities != null && PFacilities.Count > 0)
                    {
                        objetivo = GetNewObjective(PFacilities.ToArray(), origen, false).transform;
                    }
                    break;
                case PosibleObjective.ClosestBase:
                    if (Facilities != null && Facilities.Count > 0)
                    {
                        objetivo = GetNewObjective(Facilities.ToArray(), origen, true).transform;
                    }
                    break;
                case PosibleObjective.ClosestProcessingFacility:
                    if (PFacilities != null && PFacilities.Count > 0)
                    {
                        objetivo = GetNewObjective(PFacilities.ToArray(), origen, true).transform;
                    }
                    break;
                case PosibleObjective.FurthestResourceZone:
                    if (resourcesList != null && resourcesList.Count > 0)
                    {
                        objetivo = GetNewObjective(resourcesList.ToArray(), origen, false).transform;
                    }
                    break;
                case PosibleObjective.ClosestResourceZone:
                    if (resourcesList != null && resourcesList.Count > 0)
                    {
                        objetivo = GetNewObjective(resourcesList.ToArray(), origen, true).transform;
                        Debug.Log("Devolviendo " + objetivo);
                    }
                    break;
                case PosibleObjective.ClosestExtractor:
                    if (UnitsExtractList != null && UnitsExtractList.Count > 0)
                    {
                        objetivo = GetNewObjective(UnitsExtractList.ToArray(), origen, true).transform;
                    }
                    break;
                case PosibleObjective.ClosestExplorer:
                    if (UnitsExploreList != null && UnitsExploreList.Count > 0)
                    {
                        objetivo = GetNewObjective(UnitsExploreList.ToArray(), origen, true).transform;
                    }
                    break;
                case PosibleObjective.ClosestDestroyer:
                    if (UnitsDestroyerList != null && UnitsDestroyerList.Count > 0)
                    {
                        objetivo = GetNewObjective(UnitsDestroyerList.ToArray(), origen, true).transform;
                    }
                    break;
                case PosibleObjective.ClosestRandomFriendlyUnit:
                    rand = Random.Range(0, 3);
                    switch (rand)
                    {
                        case 0:
                            // Extractor
                            if (UnitsExtractList != null && UnitsExtractList.Count > 0)
                            {
                                objetivo = GetNewObjective(UnitsExtractList.ToArray(), origen, false).transform;
                            }
                            break;
                        case 1:
                            // Explorador
                            if (UnitsExploreList != null && UnitsExploreList.Count > 0)
                            {
                                objetivo = GetNewObjective(UnitsExploreList.ToArray(), origen, false).transform;
                            }
                            break;
                        case 2:
                            // Destructor
                            if (UnitsDestroyerList != null && UnitsDestroyerList.Count > 0)
                            {
                                objetivo = GetNewObjective(UnitsDestroyerList.ToArray(), origen, false).transform;
                            }
                            break;
                        default:
                            // No se mueve
                            break;
                    }
                    break;
                case PosibleObjective.LastExtractor:
                    if (UnitsExtractList != null && UnitsExtractList.Count > 0)
                        objetivo = UnitsExtractList[UnitsExtractList.Count - 1].transform;
                    break;
                case PosibleObjective.LastExplorer:
                    if (UnitsExploreList != null && UnitsExploreList.Count > 0)
                        objetivo = UnitsExploreList[UnitsExploreList.Count - 1].transform;
                    break;
                case PosibleObjective.LastDestroyer:
                    if (UnitsDestroyerList != null && UnitsDestroyerList.Count > 0)
                        objetivo = UnitsDestroyerList[UnitsDestroyerList.Count - 1].transform;
                    break;
                case PosibleObjective.LastRandomFriendlyUnit:
                    rand = Random.Range(0, 3);
                    switch (rand)
                    {
                        case 0:
                            // Extractor
                            if (UnitsExtractList != null && UnitsExtractList.Count > 0)
                            {
                                objetivo = UnitsExtractList[UnitsExtractList.Count - 1].transform;
                            }
                            break;
                        case 1:
                            // Explorador
                            if (UnitsExploreList != null && UnitsExploreList.Count > 0)
                            {
                                objetivo = UnitsExploreList[UnitsExploreList.Count - 1].transform;
                            }
                            break;
                        case 2:
                            // Destructor
                            if (UnitsDestroyerList != null && UnitsDestroyerList.Count > 0)
                            {
                                objetivo = UnitsDestroyerList[UnitsDestroyerList.Count - 1].transform;
                            }
                            break;
                        default:
                            // No se mueve
                            break;
                    }
                    break;
                case PosibleObjective.FurthestExtractor:
                    if (UnitsExtractList != null && UnitsExtractList.Count > 0)
                    {
                        objetivo = GetNewObjective(UnitsExtractList.ToArray(), origen, false).transform;
                    }
                    break;
                case PosibleObjective.FurthestExplorer:
                    if (UnitsExploreList != null && UnitsExploreList.Count > 0)
                    {
                        objetivo = GetNewObjective(UnitsExploreList.ToArray(), origen, false).transform;
                    }
                    break;
                case PosibleObjective.FurthestDestroyer:
                    if (UnitsDestroyerList != null && UnitsDestroyerList.Count > 0)
                    {
                        objetivo = GetNewObjective(UnitsDestroyerList.ToArray(), origen, false).transform;
                    }
                    break;
                case PosibleObjective.FurthestRandomFriendlyUnit:
                    rand = Random.Range(0, 3);
                    switch (rand)
                    {
                        case 0:
                            // Extractor
                            if (UnitsExtractList != null && UnitsExtractList.Count > 0)
                            {
                                objetivo = GetNewObjective(UnitsExtractList.ToArray(), origen, false).transform;
                            }
                            break;
                        case 1:
                            // Explorador
                            if (UnitsExploreList != null && UnitsExploreList.Count > 0)
                            {
                                objetivo = GetNewObjective(UnitsExploreList.ToArray(), origen, false).transform;
                            }
                            break;
                        case 2:
                            // Destructor
                            if (UnitsDestroyerList != null && UnitsDestroyerList.Count > 0)
                            {
                                objetivo = GetNewObjective(UnitsDestroyerList.ToArray(), origen, false).transform;
                            }
                            break;
                        default:
                            // No se mueve
                            break;
                    }
                    break;
                case PosibleObjective.ClosestEnemyExtractor:
                    if (EnemyUnitsExtractList != null && EnemyUnitsExtractList.Count > 0)
                    {
                        objetivo = GetNewObjective(EnemyUnitsExtractList.ToArray(), origen, true).transform;
                    }
                    break;
                case PosibleObjective.ClosestEnemyExplorer:
                    if (EnemyUnitsExploreList != null && EnemyUnitsExploreList.Count > 0)
                    {
                        objetivo = GetNewObjective(EnemyUnitsExploreList.ToArray(), origen, true).transform;
                    }
                    break;
                case PosibleObjective.ClosestEnemyDestroyer:
                    if (EnemyUnitsDestroyerList != null && EnemyUnitsDestroyerList.Count > 0)
                    {
                        objetivo = GetNewObjective(EnemyUnitsDestroyerList.ToArray(), origen, true).transform;
                    }
                    break;
                case PosibleObjective.ClosestRandomEnemyUnity:
                    rand = Random.Range(0, 3);
                    switch (rand)
                    {
                        case 0:
                            // Extractor
                            if (EnemyUnitsExtractList != null && EnemyUnitsExtractList.Count > 0)
                            {
                                objetivo = GetNewObjective(EnemyUnitsExtractList.ToArray(), origen, true).transform;
                            }
                            break;
                        case 1:
                            // Explorador
                            if (EnemyUnitsExploreList != null && EnemyUnitsExploreList.Count > 0)
                            {
                                objetivo = GetNewObjective(EnemyUnitsExploreList.ToArray(), origen, true).transform;
                            }
                            break;
                        case 2:
                            // Destructor
                            if (EnemyUnitsDestroyerList != null && EnemyUnitsDestroyerList.Count > 0)
                            {
                                objetivo = GetNewObjective(EnemyUnitsDestroyerList.ToArray(), origen, true).transform;
                            }
                            break;
                        default:
                            // No se mueve
                            break;
                    }
                    break;
                case PosibleObjective.LastEnemyExtractor:
                    if (EnemyUnitsExtractList != null && EnemyUnitsExtractList.Count > 0)
                    {
                        objetivo = EnemyUnitsExtractList[EnemyUnitsExtractList.Count - 1].transform;
                    }
                    break;
                case PosibleObjective.LastEnemyExplorer:
                    if (EnemyUnitsExploreList != null && EnemyUnitsExploreList.Count > 0)
                    {
                        objetivo = EnemyUnitsExploreList[EnemyUnitsExploreList.Count - 1].transform;
                    }
                    break;
                case PosibleObjective.LastEnemyDestroyer:
                    if (EnemyUnitsDestroyerList != null && EnemyUnitsDestroyerList.Count > 0)
                    {
                        objetivo = EnemyUnitsDestroyerList[EnemyUnitsDestroyerList.Count - 1].transform;
                    }
                    break;
                case PosibleObjective.LastRandomEnemyUnit:
                    rand = Random.Range(0, 3);
                    switch(rand) {
                        case 0:
                            if (EnemyUnitsExtractList != null && EnemyUnitsExtractList.Count > 0)
                            {
                                objetivo = EnemyUnitsExtractList[EnemyUnitsExtractList.Count - 1].transform;
                            }
                            break;
                        case 1:
                            if (EnemyUnitsExploreList != null && EnemyUnitsExploreList.Count > 0)
                            {
                                objetivo = EnemyUnitsExploreList[EnemyUnitsExploreList.Count - 1].transform;
                            }
                            break;
                        case 2:
                            if (EnemyUnitsDestroyerList != null && EnemyUnitsDestroyerList.Count > 0)
                            {
                                objetivo = EnemyUnitsDestroyerList[EnemyUnitsDestroyerList.Count - 1].transform;
                            }
                            break;
                        default:
                            break;
                    }
                    break;
                case PosibleObjective.FurthestEnemyExtractor:
                    if (EnemyUnitsExtractList != null && EnemyUnitsExtractList.Count > 0)
                    {
                        objetivo = GetNewObjective(EnemyUnitsExtractList.ToArray(), origen, false).transform;
                    }
                    break;
                case PosibleObjective.FurthestEnemyExplorer:
                    if (EnemyUnitsExploreList != null && EnemyUnitsExploreList.Count > 0)
                    {
                        objetivo = GetNewObjective(EnemyUnitsExploreList.ToArray(), origen, false).transform;
                    }
                    break;
                case PosibleObjective.FurthesEnemytDestroyer:
                    if (EnemyUnitsDestroyerList != null && EnemyUnitsDestroyerList.Count > 0)
                    {
                        objetivo = GetNewObjective(EnemyUnitsDestroyerList.ToArray(), origen, false).transform;
                    }
                    break;
                case PosibleObjective.FurthestRandomEnemyUnit:
                    rand = Random.Range(0, 3);
                    switch(rand)
                    {
                        case 0:
                            // Extractor
                            if (EnemyUnitsExtractList != null && EnemyUnitsExtractList.Count > 0)
                            {
                                objetivo = GetNewObjective(EnemyUnitsExtractList.ToArray(), origen, false).transform;
                            }
                            break;
                        case 1:
                            // Explorador
                            if (EnemyUnitsExploreList != null && EnemyUnitsExploreList.Count > 0)
                            {
                                objetivo = GetNewObjective(EnemyUnitsExploreList.ToArray(), origen, false).transform;
                            }
                            break;
                        case 2:
                            // Destructor
                            if (EnemyUnitsDestroyerList != null && EnemyUnitsDestroyerList.Count > 0)
                            {
                                objetivo = GetNewObjective(EnemyUnitsDestroyerList.ToArray(), origen, false).transform;
                            }
                            break;
                        default:
                            // No se mueve
                            break;
                    }
                    break;
                default:
                    break;
            }

            return objetivo;
        }

        // Metodo que selecciona un nuevo objetivo cercano o lejano, relativo a la posicion de origen que se le da
        protected GameObject GetNewObjective(MonoBehaviour[] list, Transform from, bool close)
        {
            int it = 1;
            int maxIt = 0;
            int minIt = 0;
            float maxDistance = Vector3.Distance(list[0].transform.position, from.position);
            float minDistance = Vector3.Distance(list[0].transform.position, from.position);

            while (it < list.Length)
            {
                float distance = Vector3.Distance(list[it].transform.position, from.position);
                if (distance > maxDistance)
                {
                    maxIt = it;
                    maxDistance = distance;
                }
                if (distance < minDistance)
                {
                    minIt = it;
                    minDistance = distance;
                }
                it++;
            }
            if (close)
                return list[minIt].gameObject;
            else
                return list[maxIt].gameObject;
        }

        // Dibuja la interfaz gr�fica de usuario para que se vea la informaci�n en pantalla
        private void OnGUI()
        {
            // Abrimos un �rea de distribuci�n arriba y a la izquierda (si el �ndice del controlador es par) o a la derecha (si el �ndice es impar), con contenido en vertical
            float areaWidth = 150;
            float areaHeight = 250;
            if (MyIndex % 2 == 0)
                GUILayout.BeginArea(new Rect(0, 0, areaWidth, areaHeight));
            else
                GUILayout.BeginArea(new Rect(Screen.width - areaWidth, 0, Screen.width, areaHeight));
            GUILayout.BeginVertical();

            // Lista las variables importantes como el �ndice del jugador y su cantidad de dinero
            GUILayout.Label("[ C" + MyIndex + " ] " + RTSGameManager.Instance.GetMoney(MyIndex) + " solaris", _labelStyle);

            // Aunque no exista el concepto de unidad seleccionada, podr�amos mostrar cual ha sido la �ltima en moverse
            if (movedUnit != null)
                // Una etiqueta para indicar la �ltima unidad movida, si la hay
                GUILayout.Label(movedUnit.gameObject.name + " moved", _labelSmallStyle);

            // Cerramos el �rea de distribuci�n con contenido en vertical
            GUILayout.EndVertical();
            GUILayout.EndArea();
        }
    }
}
