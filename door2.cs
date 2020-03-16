using UnityEngine;
using System.Collections;


public class door2 : MonoBehaviour {
	int nRotateDirection;
	// Use this for initialization
	Transform pivotobjTransform;
	bool bPlayerIn;
	int nAutodirection;

	public AudioClip door2closeSound;
	public bool bIsOpen;
	void Start () {
		bIsOpen = false;
		bPlayerIn = false;
		nRotateDirection = -1;
		nAutodirection = 1;
		Transform[] father = GetComponentsInChildren<Transform>() ;  
		foreach (var child in father) {
			if (child.name == "pivot") {
				pivotobjTransform = child;
				break;
			}
		}

	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyUp ("e") && bPlayerIn) {
			//this.gameObject.transform.Rotate (0,1, 0,Space.World);
			AudioSource.PlayClipAtPoint(door2closeSound, this.transform.localPosition,1.0f);
			if (nRotateDirection == 0) {
				nRotateDirection = 1;
			} else 
			{
				nRotateDirection = -nRotateDirection;
			}
		}



		float tyangel = pivotobjTransform.localEulerAngles.y;

		if (nRotateDirection == 1 && tyangel>90  && tyangel < 225) {
			bIsOpen = true;
			return;
		}

		if (nRotateDirection == -1 &&  (tyangel >= 225 || tyangel<=0) ) {
			bIsOpen = false;
			return;
		}

		pivotobjTransform.transform.Rotate (0,3.0f*nRotateDirection, 0,Space.World);

	}



	void OnTriggerEnter(Collider other){
		if (other.gameObject.name != "FPSController") {
			return;
		}

		bPlayerIn = true;


	}

	void OnTriggerExit(Collider other){
		if (other.gameObject.name != "FPSController") {
			return;
		}

		bPlayerIn = false;
	}
}

