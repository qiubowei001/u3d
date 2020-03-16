using UnityEngine;
using System.Collections;

public class stairdown : MonoBehaviour {
	public stairrecept upstair;
	bool bPlayerIn;
	FpsController fpsscrpt;

	// Use this for initialization
	void Start () {
		bPlayerIn = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (bPlayerIn) {
			if (fpsscrpt.bIsClimbing) {
				var v3Adj =  new Vector3 (0, 0.28f, 0);
				Vector3 v3Direction = upstair.transform.position+v3Adj - this.transform.position;
				fpsscrpt.AddPosition (0.1f*v3Direction.normalized);

			} 
		}
	}


	public void ReachTarget (){
		bPlayerIn = false;

	}



	void OnTriggerEnter(Collider other){
		if (other.gameObject.name != "FPSController") {
			return;
		}
		fpsscrpt = other.gameObject.GetComponent<FpsController> ();
		if (fpsscrpt.bIsClimbing)
			return;
		upstair.fpsscrpt = fpsscrpt;
		fpsscrpt.bIsClimbing  =true;
		bPlayerIn = true;

		other.gameObject.GetComponent<Rigidbody> ().isKinematic = true;
	}


	void OnTriggerExit(Collider other){
		if (other.gameObject.name != "FPSController") {
			return;
		}

	}
}
