using UnityEngine;
using System.Collections;

public class ghostwindow : MonoBehaviour {
	public GameObject ghostobj;
	int cdtime;
	bool bPlayerIn;
	public AudioSource ASsuddensound;
	bool bClipPlaying;
	public GameObject head;
	public FpsController fpsctrlscrp;
	// Use this for initialization
	void Start () {
		bPlayerIn = false;
		bClipPlaying = false;
		cdtime = 0;
		ghostobj.GetComponent<MeshRenderer> ().enabled = false;
		ASsuddensound.Stop ();
	}
	
	// Update is called once per frame
	void Update () {
		if (bPlayerIn) {
			cdtime++;



			if (cdtime >= 0 ) {
				if ((bClipPlaying == false)) {
					ASsuddensound.Play ();
					bClipPlaying = true;
					fpsctrlscrp.bMouseAble = false;
					//head.transform.LookAt (ghostobj.transform);
				} else {
					if (fpsctrlscrp.bMouseAble == false) {
						var v3direction = ghostobj.transform.position - head.transform.position;
						v3direction.y = 0;
						var qua = Quaternion.FromToRotation (Vector3.forward, v3direction);
						head.transform.rotation = Quaternion.Lerp (head.transform.rotation, qua, Time.time * 0.1f);	
					}
				}
			}

			if (cdtime > 25) {
				ghostobj.GetComponent<MeshRenderer> ().enabled = false;
				fpsctrlscrp.bMouseAble = true;
				bPlayerIn = false;
				bClipPlaying = false;
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

	}

	void OnTriggerExit(Collider other){
		if (other.gameObject.name != "FPSController") {
			return;
		}


	}

}
