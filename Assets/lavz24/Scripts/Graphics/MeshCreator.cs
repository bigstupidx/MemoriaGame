//
// MeshCreator.cs
//
// Author:
//       Luis Alejandro Vieira <lavz24@gmail.com>
//
// Copyright (c) 2014 
//
using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public struct GridInfo{

	public Mesh mesh;
	public Dictionary<int,Vector3> centers;
};

public class MeshCreator : MonoBehaviour
{
	/// <summary>
	/// Creates a plane in axis XZ and uvs
	/// </summary>
	/// <returns>The plane X zwith U.</returns>
	/// <param name="parentM">Parent m.</param>
	/// <param name="sizeRec">Size rec.</param>
	/// <param name="numX">Number x.</param>
	/// <param name="numZ">Number z.</param>
	public static GridInfo CreatePlaneXZwithUV(Transform parentM, int sizeRec, int numX, int numZ)
    {
        Vector3 iniPos = new Vector3(-0.5f * sizeRec * numX, 0, -0.5f * sizeRec*numZ);

		GridInfo info = new GridInfo();
		int indexCenter = 0;
		Dictionary<int,Vector3> centers = new Dictionary<int,Vector3> ();
        Mesh ml = new Mesh();
        ml.name = "GridXZ";

        int hCount = numX + 1;
        int vCount = numZ + 1;
        int numTriangles = numX * numZ * 6;
        int numVertices = hCount * vCount;

        Vector3[] vertices = new Vector3[numVertices];
        Vector2[] uvs = new Vector2[numVertices];
        int[] triangles = new int[numTriangles];

        int index = 0;
        float uvFactorX = 1.0f / numX;
        float uvFactorZ = 1.0f / numZ;

        for (float z = 0.0f; z < vCount; z++)
        {
            for (float x = 0.0f; x < hCount; x++)
            {
                vertices[index] = (new Vector3(x * sizeRec + iniPos.x, 0, z * sizeRec  + iniPos.z));

                uvs[index++] = new Vector2(x * uvFactorX, z * uvFactorZ);
            }
        }
        index = 0;
		for (int z = 0; z < numZ; z++)
        {
            for (int x = 0; x < numX; x++)
            {
				triangles[index] = (z * hCount) + x;
				triangles[index + 1] = ((z + 1) * hCount) + x;
				triangles[index + 2] = (z * hCount) + x + 1;

				triangles[index + 3] = ((z + 1) * hCount) + x;
				triangles[index + 4] = ((z + 1) * hCount) + x + 1;
				triangles[index + 5] = (z * hCount) + x + 1;

                index += 6;
            }

        }
		for (float z = 0.0f; z < vCount-1; ++z)
		{
			for (float x = 0.0f; x < hCount-1; ++x)
			{
				centers.Add (indexCenter++, new Vector3 ((x+0.5f) * sizeRec + iniPos.x, 0, (z+0.5f) * sizeRec  + iniPos.z));
			}
		}

        ml.vertices = vertices;
        ml.uv = uvs;
        ml.triangles = triangles;

        ml.RecalculateNormals();
        ml.RecalculateBounds();
        ml.Optimize();

		info.mesh = ml;
		info.centers = centers;
		return info;
    }


	public static Mesh CreatePlaneXZ(Transform parentM,int sizeRec, int numX, int numZ){

		Vector3 iniPos = new Vector3(-numX*0.5f*sizeRec,0,-numZ*0.5f*sizeRec);

		Mesh ml = new Mesh();

		int acumZ = 0;

		Vector3 first =parentM.InverseTransformPoint(iniPos);
		Vector3 second =parentM.InverseTransformPoint(first+new Vector3(sizeRec,0,sizeRec));
        
        Vector3 moveX = new Vector3(sizeRec,0,0);

		for(int j=0;j<numZ;++j){
			for(int i=0;i<numX;++i){

                AddLine(ml, MakeQuad(parentM, first, second), false); 
				second += moveX;
				first += moveX;
			}
			acumZ +=sizeRec;
			first = parentM.InverseTransformPoint(iniPos+new Vector3(0,0,acumZ));
			second =  parentM.InverseTransformPoint(first+new Vector3(sizeRec,0,sizeRec ));
		}

  

		return ml;
	}
    
    static Vector3[] MakeQuad(Transform parentM,Vector3 s, Vector3 e) {

		Vector3[] q = new Vector3[4];


		q[0] = parentM.InverseTransformPoint(s  );
		q[1] = parentM.InverseTransformPoint(new Vector3(s.x,0,e.z));
		q[2] = parentM.InverseTransformPoint(new Vector3(e.x,0,s.z));
		q[3] = parentM.InverseTransformPoint(e );

		return q;
	}

    static void AddLine(Mesh m, Vector3[] quad, bool tmp)
    {
        
		int vl = m.vertices.Length;
		
		Vector3[] vs = m.vertices;
		if(!tmp || vl == 0) vs = resizeVertices(vs, 4);
		else vl -= 4;
		
		vs[vl] = quad[0];
		vs[vl+1] = quad[1];
		vs[vl+2] = quad[2];
		vs[vl+3] = quad[3];
		
		int tl = m.triangles.Length;
		
		int[] ts = m.triangles;
		if(!tmp || tl == 0) ts = resizeTraingles(ts, 6);
		else tl -= 6;
        ts[tl] = vl;
        ts[tl+1] = vl+1;
        ts[tl+2] = vl+2;
        ts[tl+3] = vl+1;
        ts[tl+4] = vl+3;
        ts[tl+5] = vl+2;
        

        m.vertices = vs;
        m.triangles = ts;
     
        m.RecalculateNormals();
        m.RecalculateBounds();
        m.Optimize();

		m.name = "GridXZ";
    }

	static Vector3[] resizeVertices(Vector3[] ovs, int ns) {
		Vector3[] nvs = new Vector3[ovs.Length + ns];
		for(int i = 0; i < ovs.Length; i++) nvs[i] = ovs[i];
		return nvs;
	}
	static Vector2[] resizeUV(Vector2[] ovs, int ns) {
		Vector2[] nvs = new Vector2[ovs.Length + ns];
		for(int i = 0; i < ovs.Length; i++) nvs[i] = ovs[i];
		return nvs;
    }
    static int[] resizeTraingles(int[] ovs, int ns) {
		int[] nvs = new int[ovs.Length + ns];
		for(int i = 0; i < ovs.Length; i++) nvs[i] = ovs[i];
		return nvs;
    }
}

