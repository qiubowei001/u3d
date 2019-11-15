using UnityEngine;
using System.Collections;


public class rope : MonoBehaviour {
	//存储RopeParent的Rigibody组件
	internal Rigidbody RBody;
	public GameObject prefabObj;
	public GameObject prefabObjjietou;

	public GameObject oWeaknot;
	int nWeakKnot = 22;
	// Use this for initialization
	internal void Start () {
		//给RopeParent添加Rigibody组件
		//this.gameObject.AddComponent<Rigidbody>();
		//获取RopeParent的Rigibody组件并赋值给RBody
		this.RBody = this.gameObject.GetComponent<Rigidbody>();
		this.RBody.isKinematic = true;
	
		//RopeParent中子物体的数量
		//给每一个子物体都加上Hinge Joint组件
		int childcount = 25;//this.transform.childCount;
		var lastobj = this.gameObject;
		GameObject lastknot = null;
		for (int i = 0; i < childcount;i++) {
			//Transform t = this.transform.GetChild(i);
			var vec3 = this.gameObject.transform.position +   new Vector3(0, 0, 0.8f*i);//new Vector3(0, -1.0f*i,0 ); // 

			GameObject table;

			if (i == childcount - 1) {
				table = GameObject.Instantiate (prefabObjjietou)as GameObject;
			} else {
				table = GameObject.Instantiate (prefabObj)as GameObject;
			}
			//
			if (lastknot != null) {
				lastknot.GetComponent<ropeknot> ().nextRopeKnot = table;
				lastknot = table;
			} else {
				lastknot = table;
			}



			table.GetComponent<ropeknot> ().SetIsEnd (i == (childcount-1));
			table.GetComponent<ropeknot> ().SetParentRope (this.gameObject);
			table.GetComponent<ropeknot> ().SetWeakKnot ( nWeakKnot == i);
			if (nWeakKnot == i) {
				oWeaknot = table;
				//table.GetComponent<Renderer> ().material.color=Color.red;

			}


			table.transform.position = vec3;
			//table.GetComponent<CharacterJoint>(); // AddComponent<CharacterJoint>();
			table.transform.parent = this.transform;
				


			CharacterJoint hinge = table.GetComponent<CharacterJoint>();
			hinge.connectedBody = i == 0 ? this.RBody : lastobj.GetComponent<Rigidbody>();

			var thisrbody = table.GetComponent<Rigidbody>();
			thisrbody.maxDepenetrationVelocity = 1.0f;
			//hinge.anchor
			hinge.enablePreprocessing = true;
			lastobj = table;
		}


	}	


	public bool IsConnected()
	{
		return oWeaknot.GetComponent<ropeknot> ().IsConnected();

		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
