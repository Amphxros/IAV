using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace es.ucm.fdi.iav.rts
{
    public class InfluenceMap : MonoBehaviour
    {
        public int dimensionX, dimensionY, dimensionZ;
        private Vector3[,] points; 

        private List<BaseFacility> TeamAFacilities;
        private List<ProcessingFacility> TeamAPFacilities;
        private List<ExtractionUnit> TeamAUnitsExtractList;
        private List<ExplorationUnit> TeamAUnitsExploreList;
        private List<DestructionUnit> TeamAUnitsDestroyerList;

        // Las listas completas de instalaciones y unidades del enemigo
        private List<BaseFacility> TeamBFacilities;
        private List<ProcessingFacility> TeamBPFacilities;
        private List<ExtractionUnit> TeamBUnitsExtractList;
        private List<ExplorationUnit> TeamBUnitsExploreList;
        private List<DestructionUnit> TeamBUnitsDestroyerList;
    
        bool hasReferences_=false;
        void Start()
        {
        int dimX= -dimensionX/2;
        int dimZ= -dimensionZ/2;

        points = new Vector3[dimensionX, dimensionZ];

        for(int i=0;i<dimensionX;i++){
            for(int j=0;j<dimensionZ;j++){
                points[i,j]=new Vector3(dimX + i, dimensionY, dimZ+ j);
            }
        }

        }   

        void Update()
        {
            if(!hasReferences_){
            // pillar todas las referencias del GM
        

            hasReferences_=true;
            }
        }

        void UpdateNodes(){
            if(!hasReferences_){
                return;
            }
            else{
                
            }
        }
    }
}