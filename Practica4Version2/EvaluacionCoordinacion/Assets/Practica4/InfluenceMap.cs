using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace es.ucm.fdi.iav.rts
{
    public class InfluenceMap : MonoBehaviour
    {
        public GameObject node;
        private List<Node> nodes;
        
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
      
        void OnEnable()
        {
        int dimX= -dimensionX/2;
        int dimZ= -dimensionZ/2;

        for(int i=dimX ; i<dimensionX;i++){
            for(int j=dimZ ; j<dimensionZ;j++){
                GameObject g= Instantiate(node, this.transform);
                g.transform.position=new Vector3( i, dimensionY,j);
                nodes.Add(g.GetComponent<Node>());
                Debug.Log(i + " " + j);
            }
        }

        }   
      
        void UpdateNodes(){
            if(!hasReferences_){
            
            
            }
            else{

                for(int i=0;i<nodes.Count;i++){
                
                }

            }
        }
    }
}