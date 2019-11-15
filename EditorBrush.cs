using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
public class EditorBrush : MonoBehaviour {
	public gameoflife g_scrpt;
	public EditorGOL editor_scrp;
	Mesh mymesh;
	public bool bReset;
	List<Vector3> listChoosed;
	public int MatchTargetWeightMask;
	// Use this for initialization
	public void Init () {
		MatchTargetWeightMask = LayerMask.GetMask("editorbrush");
		bReset = false;
		mymesh = this.gameObject.GetComponent<MeshFilter> ().mesh;
		listChoosed = new List<Vector3>();
		this.GetComponent<Renderer> ().material.color = new Color (1.0f, 1.0f, 1.0f, 0.5f);
	}

	public void ClearListChoosed()
	{
		foreach (Vector3 v3 in listChoosed) {
			editor_scrp.RemoveACube ((int)v3.x, (int)v3.y, (int)v3.z);
		}
		listChoosed.Clear ();
	}

	void Start(){
		
	}

	// Update is called once per frame
	void Update () {
		//BoxCollider 
		if (g_scrpt.bStart) {
			return;
		}

		if (bReset) {
			ClearListChoosed ();
			mymesh = this.gameObject.GetComponent<MeshFilter> ().mesh;
			var bounds = mymesh.bounds;

			Vector3 startpoint = this.gameObject.transform.TransformPoint(bounds.center- new Vector3(bounds.size.x,bounds.size.y,bounds.size.z)*0.5f);
			Vector3 endpoint = this.gameObject.transform.TransformPoint(bounds.center+ new Vector3(bounds.size.x,bounds.size.y,bounds.size.z)*0.5f);
			int Sx = (int)startpoint.x;
			int Sy= (int)startpoint.y;;
			int Sz= (int)startpoint.z;
			int Ex= (int)endpoint.x;
			int Ey= (int)endpoint.y;
			int Ez= (int)endpoint.z;

			for (int i = Sx; i <= Ex; i++) {
				for (int j = Sy; j <= Ey; j++) {
					for (int k = Sz; k <= Ez; k++) {
						var v3detec = new Vector3 (i, j, k);

						var v3scale = this.gameObject.transform.localScale;
						var v3Local = v3detec;


						bool bisin = IsPointInside (mymesh, v3detec );
						if (bisin) {
							editor_scrp.PlaceACube (i, j, k);
							listChoosed.Add (v3detec);
						}
					}
				}
			}
			bReset = false;
		}


	}


	 bool PointIsWithinBoundingSphere(Mesh mesh, Vector3 point)
	{
		Ray ray = new Ray ( point, Vector3.up);
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit,100.0f,MatchTargetWeightMask))//Physics.Raycast (ray, out hit,0.001f,MatchTargetWeightMask))
		{
			return true;
		}
		return false;
	}


	 bool IsPointInside(Mesh aMesh, Vector3 aWorldPoint)
     {
		
		Vector3	aLocalPoint = this.gameObject.transform.InverseTransformPoint(aWorldPoint);

         var verts = aMesh.vertices;
         var tris = aMesh.triangles;
         int triangleCount = tris.Length / 3;
         for (int i = 0; i < triangleCount; i++)
         {
             var V1 = verts[ tris[ i*3     ] ];
             var V2 = verts[ tris[ i*3 + 1 ] ];
             var V3 = verts[ tris[ i*3 + 2 ] ];
             var P = new Plane(V1,V2,V3);
             if (P.GetSide(aLocalPoint))
                 return false;
         }
         return true;
     }

}
