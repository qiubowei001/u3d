using UnityEngine;
using System.Collections;

public class ghost : MonoBehaviour {
	bool bPlayerIn;
	public GameObject ghostobj;
	public GameObject hat;
	// Use this for initialization
	void Start () {
		bPlayerIn = false;
		ghostobj.GetComponent<MeshRenderer> ().enabled = false;

		if (hat) {
			hat.GetComponent<SpriteRenderer> ().enabled = false;
			hat.GetComponent<CapsuleCollider> ().isTrigger = false;
		}
	}

	int cdtime;
	// Update is called once per frame
	void Update () {
		if (bPlayerIn) {
			//玩家转向ghost 3秒后消失
			cdtime++;

			if (cdtime > 60) {
				ghostobj.GetComponent<MeshRenderer> ().enabled = false;
			} else {
				var g_tran = ghostobj.GetComponent<Transform> ();
				g_tran.position = g_tran.position - Vector3.forward*0.1f; 
			}
		}
	}


	void OnTriggerEnter(Collider other){
		if (other.gameObject.name != "FPSController") {
			return;
		}
		cdtime = 0;
		bPlayerIn = true;

		ghostobj.GetComponent<MeshRenderer> ().enabled = true;

		if (hat) {
			hat.GetComponent<SpriteRenderer> ().enabled = true;
			hat.GetComponent<CapsuleCollider> ().isTrigger = true;
		}

	}

	void OnTriggerExit(Collider other){
		if (other.gameObject.name != "FPSController") {
			return;
		}

		bPlayerIn = false;
		ghostobj.GetComponent<MeshRenderer> ().enabled = false;
	}
}
