using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public interface GridNode
{
    int Width{get;}
    int Height{get;}
    float GetValue(int x, int y);
}


public class Grid : MonoBehaviour
{

    MeshRenderer mMeshRenderer_;

    MeshFilter mMeshFilter_;
    Mesh mMesh;

    GridNode [] mData_;
    

     
    [SerializeField]
	Vector3 LeftBottomStartPoint;
    [SerializeField]
	Material mMaterial;
	
	[SerializeField]
	Color mNeutralColor = Color.white;
	
	[SerializeField]
	Color mColorP1 = Color.red;
	
	[SerializeField]
	Color mColorP2 = Color.blue;

	
	Color[] _colors;
        
    public void setGridNodes(GridNode[] g){
        mData_=g;
    }
    public void createMesh(Vector3 bottomLeftPosition, float dimGrid){
        mMesh= new Mesh();
        mMesh.name=name;
        mMeshFilter_ = gameObject.AddComponent<MeshFilter>();
		mMeshRenderer_ = gameObject.AddComponent<MeshRenderer>();
        mMeshFilter_.mesh=mMesh;
        mMeshRenderer_.material=mMaterial;

        // components of our mesh 
        List<Vector3> vertex= new List<Vector3>();
		List<Color> colors = new List<Color>();
		List<Vector3> norms = new List<Vector3>();
		List<Vector2> uvs = new List<Vector2>();
		List<int> tris = new List<int>();

        for(int i=0;i<mData_[0].Height; i++){
            for(int j=0;j<mData_[0].Width; j++){
            //    vertices
                Vector3 a= new Vector3(bottomLeftPosition.x + (j* dimGrid), bottomLeftPosition.y, bottomLeftPosition.z + (i* dimGrid));
                Vector3 b= new Vector3(bottomLeftPosition.x + ((j+1)* dimGrid), bottomLeftPosition.y, bottomLeftPosition.z + (i* dimGrid));
                Vector3 c= new Vector3(bottomLeftPosition.x + (j* dimGrid), bottomLeftPosition.y, bottomLeftPosition.z + ((i+1)* dimGrid));
                Vector3 d= new Vector3(bottomLeftPosition.x + ((j+1)* dimGrid), bottomLeftPosition.y, bottomLeftPosition.z + ((i+1)* dimGrid));
                vertex.Add(a);
                vertex.Add(b);
                vertex.Add(c);
                vertex.Add(d);

            // colores

                colors.Add(mNeutralColor);
                colors.Add(mNeutralColor);
                colors.Add(mNeutralColor);
                colors.Add(mNeutralColor);

            // normales
                norms.Add(Vector3.up);
                norms.Add(Vector3.up);
                norms.Add(Vector3.up);
                norms.Add(Vector3.up);
            // UVs
                uvs.Add(new Vector2(0,0));
                uvs.Add(new Vector2(1,0));
                uvs.Add(new Vector2(0,1));
                uvs.Add(new Vector2(1,1));

            }     
        }

        for(int k=0; k<vertex.Count; k=k+4){
            int a1=k;
            int b2=k+1;
            int c3=k+2;
            int d4=k+3;

            tris.Add(a1);
			tris.Add(c3);
			tris.Add(b2);
			
			tris.Add(c3);
			tris.Add(d4);
			tris.Add(b2);

        }

        _colors=colors.ToArray();

		mMesh.vertices = vertex.ToArray();
		mMesh.normals = norms.ToArray();
		mMesh.uv = uvs.ToArray();
		mMesh.colors = _colors;
		mMesh.triangles = tris.ToArray();

    }

    void SetColor(int x, int y, Color c) {
		int idx = ((y * mData_[0].Width) + x) * 4;
		_colors[idx] = c;
		_colors[idx+1] = c;
		_colors[idx+2] = c;
		_colors[idx+3] = c;
	}

    // Update is called once per frame
    void Update()
    {
        if(mData_!=null){
            for(int i=0;i<mData_[0].Height; i++){
                for(int j=0;j<mData_[0].Width; j++){
                    float val=0, maxValue=0;
                    for(int k=0; k<mData_.Length; k++){
                        if (Mathf.Abs(mData_[k].GetValue (j, i)) > maxValue) {
							maxValue = Mathf.Abs(mData_[k].GetValue (j, i));
							val = mData_[k].GetValue (j, i);
						}
                    }

                    Color c= mNeutralColor;
                    if(val>0.5f){
                        c= mColorP1;
                    }
                    else if(val>0){
                        c=Color.Lerp(mNeutralColor, mColorP1,(val-0.5f)/0.5f);

                    }
                    else if(val<0.5){

                        c= mColorP2;
                    }
                    else{
                        c=Color.Lerp(mNeutralColor, mColorP2,-(val-0.5f)/0.5f);
                        
                    }
                    SetColor(j,i,c);


                }   
            }

            mMesh.colors=_colors;
        }
        
    }
}
