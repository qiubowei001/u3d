using UnityEngine;
using System.Collections;

public class flystart : MonoBehaviour {
	public Transform nextFlyNodeTransform;
	Vector3 targetpos;
	Vector3 dir;
	bool bStart;
	// Use this for initialization
	void Start () {
		if (nextFlyNodeTransform) {
			targetpos = nextFlyNodeTransform.position;
			//dir = targetpos - transform.position;
			//dir = dir.normalized;
			dir = Vector3.zero;
		}
		bStart = false;
	}
	
	// Update is called once per frame
	void Update () {
		
		if (nextFlyNodeTransform && bStart) {
			if(dir == Vector3.zero){
				dir = targetpos - transform.position;
				dir = dir.normalized;
			}


			transform.position = transform.position + dir*0.1f; 
		}
	}


	void OnTriggerEnter(Collider other){
		Vector3 finalpos = Vector3.zero;
		if (nextFlyNodeTransform) {
			finalpos =  nextFlyNodeTransform.position;
		}
		flyknot flyknotscrpt = other.gameObject.GetComponent<flyknot> ();
		if (flyknotscrpt) {
			nextFlyNodeTransform = flyknotscrpt.nextFlyNodeTransform;
			if (nextFlyNodeTransform) {
				targetpos = nextFlyNodeTransform.position;
				dir = targetpos - transform.position;
				dir = dir.normalized;
			} else {
				transform.position = finalpos;
			}

		}


		prop_set prop_setscrpt = other.gameObject.GetComponent<prop_set> ();
		if (prop_setscrpt) {
			prop_setscrpt.AddPiece ();
		}

		if (other.gameObject.name == "FPSController") {
			bStart = true;
		}
	}

	void OnCollisionEnter(Collision other){
		Vector3 finalpos = Vector3.zero;
		if (nextFlyNodeTransform) {
			finalpos =  nextFlyNodeTransform.position;
		}
		flyknot flyknotscrpt = other.gameObject.GetComponent<flyknot> ();
		if (flyknotscrpt) {
			nextFlyNodeTransform = flyknotscrpt.nextFlyNodeTransform;
			if (nextFlyNodeTransform) {
				targetpos = nextFlyNodeTransform.position;
				dir = targetpos - transform.position;
				dir = dir.normalized;
			} else {
				transform.position = finalpos;
			}

		}


		prop_set prop_setscrpt = other.gameObject.GetComponent<prop_set> ();
		if (prop_setscrpt) {
			prop_setscrpt.AddPiece ();
		}

		if (other.gameObject.name == "FPSController") {
			var rb = this.GetComponent<Rigidbody> ();
			rb.useGravity = false;
			rb.isKinematic = true;

			var cd = this.GetComponent<SphereCollider> ();
			cd.isTrigger = true;

			bStart = true;
		}


	}
}
