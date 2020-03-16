using UnityEngine;
using System.Collections;

public class window : MonoBehaviour {
	int nRotateDirection;
	// Use this for initialization
	Transform pivotobjTransform;
	bool bPlayerIn;
	int nAutodirection;

	public AudioClip windSound;
	public AudioClip gajiSound;
	public AudioClip windowcloseSound;
	public AudioSource ASwind;
	public AudioSource ASgaji;

	void Start () {
		
		bPlayerIn = false;
		nRotateDirection = 0;
		nAutodirection = 1;
		Transform[] father = GetComponentsInChildren<Transform>() ;  
		foreach (var child in father) {
			if (child.name == "pivot") {
				pivotobjTransform = child;
				break;
			}
		}

		//ASwind.clip = windSound;
		//ASwind.Play ();

		//ASgaji.clip = gajiSound;
		//ASgaji.Play ();


		//
		//AudioSource.PlayClipAtPoint(gajiSound, transform.localPosition,1.0f);


	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyUp ("e") && bPlayerIn) {
			//this.gameObject.transform.Rotate (0,1, 0,Space.World);
			if (nRotateDirection == 0) {
				nRotateDirection = -1;
			} else 
			{
				nRotateDirection = -nRotateDirection;
			}

			if (nRotateDirection == -1) {
				if (ASwind) {
					ASwind.volume = 0.1f;
					//ASwind.Pause ();
					ASgaji.Pause ();
					AudioSource.PlayClipAtPoint(windowcloseSound, this.transform.localPosition,1.0f);
				}

			} else {
				if (ASwind) {
					ASwind.volume = 1.0f;
					//ASwind.Play ();
					ASgaji.Play ();
				}
			}
		}

		if (nRotateDirection == 0) {
			//自动摇摆状态
			//播放音效
			float yangel = pivotobjTransform.localEulerAngles.y;

			if (nAutodirection == 1 && yangel>110  && yangel < 225) {
				nAutodirection = -1;
			}

			if (nAutodirection == -1 &&  (yangel >= 225 || yangel<=70) ) {
				nAutodirection = 1;
			}

			float randomAngle = Random.Range(2,5);
			pivotobjTransform.transform.Rotate (0,3.0f*nAutodirection, 0,Space.World);


			return;
		}

		float tyangel = pivotobjTransform.localEulerAngles.y;

		if (nRotateDirection == 1 && tyangel>90  && tyangel < 225) {
			return;
		}

		if (nRotateDirection == -1 &&  (tyangel >= 225 || tyangel<=0) ) {
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
