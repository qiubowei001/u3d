using UnityEngine;
using System.Collections;

public class stairrecept : MonoBehaviour {
	public FpsController fpsscrpt;
	// Use this for initialization
	public stairdown sendstair;

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject.name != "FPSController") {
			return;
		}
		if (fpsscrpt) {

			other.gameObject.GetComponent<Rigidbody> ().isKinematic = false;
			var v3Adj =  new Vector3 (0, 0.28f, 0);
			other.gameObject.transform.position = this.transform.position +v3Adj;
		}

	}


	void OnTriggerExit(Collider other){
		if (other.gameObject.name != "FPSController") {
			return;
		}

		if (fpsscrpt) {
			//fpsscrpt.bIsClimbing  =false;
			fpsscrpt.resetStair ();
			sendstair.ReachTarget ();
			fpsscrpt = null;
		}


	}
}
