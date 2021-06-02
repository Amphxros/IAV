using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace  es.ucm.fdi.iav.rts
{
    public class InfluenceMap : Grid
    {   

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

        float[,] influences;
        
        float[,] influenceBuffer;

        public float Decay { get; set; }
	    public float Momentum { get; set; }

	    public int Width { get{ return influences.GetLength(0); } }
	    public int Height { get{ return influences.GetLength(1); } }
	    public float GetValue(int x, int y) {
		    return influences [x, y];
	    }


        public InfluenceMap(int size, float decay, float momentum){
            influences= new float[size, size];
            influenceBuffer= new float[size, size];
            Decay=decay;
            Momentum=momentum;
        }

        public InfluenceMap(int w, int h, float decay, float momentum) {
		influences = new float[w, h];
		influenceBuffer = new float[w, h];
		Decay = decay;
		Momentum = momentum;
	    }

        public void SetInfluence(int x, int y, float value) {
        if (x < Width && y < Height) {

            influences[x,y]=value;
            influenceBuffer[x,y]=value;
        }
        
        }

        public void SetInfluence(Vector2 pos, float value) {
            if (pos.x < Width && pos.y < Height) {
            influences[(int)pos.x,(int)pos.y]=value;
            influenceBuffer[(int)pos.x,(int)pos.y]=value;

            }
        }


        void UpdateInfluenceBuffer() {
		for (int i = 0; i< influences.GetLength(0); i++) 
			for (int j = 0; j < influences.GetLength(1); j++) 
				influenceBuffer[i, j] = influences[i, j];
    	}


        public void UpdateMap() {
                
            var indexList = RTSGameManager.Instance.GetIndexes();
            var MyIndex =RTSGameManager.Instance.GetIndex(RTSGameManager.Instance.Controllers[indexList[0]]);
            Facilities = RTSGameManager.Instance.GetBaseFacilities(MyIndex);
            PFacilities = RTSGameManager.Instance.GetProcessingFacilities(MyIndex);
            UnitsExtractList = RTSGameManager.Instance.GetExtractionUnits(MyIndex);
            UnitsExploreList = RTSGameManager.Instance.GetExplorationUnits(MyIndex);
            UnitsDestroyerList = RTSGameManager.Instance.GetDestructionUnits(MyIndex);
                
            foreach(Facility f in Facilities){
                SetInfluence((int)f.transform.position.x, (int)f.transform.position.z, 1.0f);
            }
                
            foreach(var f in PFacilities){
                SetInfluence((int)f.transform.position.x, (int)f.transform.position.z, 1.0f);
            }
                
            foreach(var f in UnitsExtractList){
                SetInfluence((int)f.transform.position.x, (int)f.transform.position.z, 1.0f);
            }
                
            foreach(var f in UnitsExploreList){
                SetInfluence((int)f.transform.position.x, (int)f.transform.position.z, 1.0f);
            }
            foreach(var f in UnitsDestroyerList){
                SetInfluence((int)f.transform.position.x, (int)f.transform.position.z, 1.0f);
            }


            indexList.Remove(MyIndex);
            var FirstEnemyIndex = indexList[0];
            EnemyFacilities = RTSGameManager.Instance.GetBaseFacilities(FirstEnemyIndex);
            EnemyPFacilities = RTSGameManager.Instance.GetProcessingFacilities(FirstEnemyIndex);
            EnemyUnitsExtractList = RTSGameManager.Instance.GetExtractionUnits(FirstEnemyIndex);
            EnemyUnitsExploreList = RTSGameManager.Instance.GetExplorationUnits(FirstEnemyIndex);
            EnemyUnitsDestroyerList = RTSGameManager.Instance.GetDestructionUnits(FirstEnemyIndex);


            foreach(Facility f in EnemyFacilities){
                SetInfluence((int)f.transform.position.x, (int)f.transform.position.z, -1.0f);
            }
                
            foreach(var f in EnemyPFacilities){
                SetInfluence((int)f.transform.position.x, (int)f.transform.position.z, -1.0f);
            }
                
            foreach(var f in EnemyUnitsExtractList){
                SetInfluence((int)f.transform.position.x, (int)f.transform.position.z, -1.0f);
            }
                
            foreach(var f in EnemyUnitsExploreList){
                SetInfluence((int)f.transform.position.x, (int)f.transform.position.z, -1.0f);
            }
            foreach(var f in EnemyUnitsDestroyerList){
                SetInfluence((int)f.transform.position.x, (int)f.transform.position.z, -1.0f);
            }

        }


    }
}
