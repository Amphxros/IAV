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
        
        [SerializeField]
        // Los movimientos que estaran disponibles de verdad
        public List<PosibleMovement> Moves;
        private int nextMove=0; // indice para ir eligiendo enumerados de movimiento
        
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


        private int ThinkStepNumber { get; set; } = 0;
        private Unit LastUnit { get; set; }

        private override void Awake()
        {
        Name="Player-G10-IAV";
        Author= "Amparo Rubio Bellón";
        ThinkStepNumber=0;

        }

        protected override voi Think(){
            // inicio de la partida
            if(ThinkStepNumber==0){
                // mis referencias
                MyIndex= RTSGameManager.Instance.GetIndex(this);
                MyFirstBaseFacility = RTSGameManager.Instance.GetBaseFacilities(MyIndex)[0];
                MyFirstProcessingFacility = RTSGameManager.Instance.GetProcessingFacilities(MyIndex)[0];

                


                // mapa de influencia


            }
            else{

                    Facilities = RTSGameManager.Instance.GetBaseFacilities(MyIndex);
                    PFacilities = RTSGameManager.Instance.GetProcessingFacilities(MyIndex);
                    UnitsExtractList = RTSGameManager.Instance.GetExtractionUnits(MyIndex);
                    UnitsExploreList = RTSGameManager.Instance.GetExplorationUnits(MyIndex);
                    UnitsDestroyerList = RTSGameManager.Instance.GetDestructionUnits(MyIndex);
1
                    EnemyFacilities = RTSGameManager.Instance.GetBaseFacilities(FirstEnemyIndex);
                    EnemyPFacilities = RTSGameManager.Instance.GetProcessingFacilities(FirstEnemyIndex);
                    EnemyUnitsExtractList = RTSGameManager.Instance.GetExtractionUnits(FirstEnemyIndex);
                    EnemyUnitsExploreList = RTSGameManager.Instance.GetExplorationUnits(FirstEnemyIndex);
                    EnemyUnitsDestroyerList = RTSGameManager.Instance.GetDestructionUnits(FirstEnemyIndex);

                    towersList = RTSScenarioManager.Instance.Towers;

                    if (Facilities.Count > 0) // Si hay instalaciones base podemos construir
                    {
                        bool [] canCreateUnits= new bool[3];
                        if(RTSGameManager.Instance.GetMoney(MyIndex)>RTSGameManager.Instance.ExtractionUnitCost){
                            canCreateUnits[0]=true;
                        }
   
                        if(RTSGameManager.Instance.GetMoney(MyIndex)>RTSGameManager.Instance.DestructionUnitCost){
                            canCreateUnits[1]=true;
                        }
                        if(RTSGameManager.Instance.GetMoney(MyIndex)>RTSGameManager.Instance.ExplorationUnitCost){
                            canCreateUnits[2]=true; 
                        }
                        if(!canCreateUnits[0] && !canCreateUnits[1] && !canCreateUnits[2]){ //no hay dinero para crear asi que podemos intentar ver si hay unidades para mmover
                            
                            bool thereAreUnits=false;
                            if(UnitsExtractList.Count>0){
                                thereAreUnits=true;

                            }
                            if(UnitsExploreList.Count>0){
                                thereAreUnits=true;

                            }
                            if(UnitsDestroyerList.Count>0){
                                thereAreUnits=true;

                            }

                            if(!thereAreUnits){
                                // si no hemos podido mover unidades -> no habia unidades
                                // ademas en este caso no contaríamos con dinero para construirlas
                                // es decir tendríamos edificios pero ni unidades ni dinero para construirlas
                                Debug.Log("no hay unidades ni dinero para construirlas");
                            }

                            // en este caso como contamos con unidades pero noo dinero debemos mover las que tenemos
                            else{

                            }
                        }

                        // como podemos crear unidades podemos decidir si crear o mover las que tenemos
                        else{

                        }
  

                    }


        }

    }
}
