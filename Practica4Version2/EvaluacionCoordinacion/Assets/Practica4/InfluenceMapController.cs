using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace  es.ucm.fdi.iav.rts{
public class InfluenceMapController : MonoBehaviour
{
        public Transform bottomLeftPoint;
        public Transform topRightPoint;

        public float gridDim;
        public float decay=0.3f;
        public float momentum=0.1f; 
    
        InfluenceMap mMap;


        void CreateMap()
	{
		int width = (int)(Mathf.Abs(topRightPoint.position.x - bottomLeftPoint.position.x) / gridDim);
		int height = (int)(Mathf.Abs(topRightPoint.position.z - bottomLeftPoint.position.z) / gridDim);

		mMap = new InfluenceMap(width, height, decay, momentum);
	}

    public void UpdateMap() {
        CreateMap();
        mMap.UpdateMap();

    }
    }
}