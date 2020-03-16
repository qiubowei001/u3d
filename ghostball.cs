using UnityEngine;
using System.Collections;

public class ghostball : MonoBehaviour {
	public AudioClip ballsound;
	public bool bstart;
	bool bInit;
	// Use this for initialization
	void Start () {
		bstart = false;
		var rb = this.GetComponent<Rigidbody> ();
		rb.isKinematic = true;
		var startpos = new Vector3(1000 ,1000 ,1000);
		transform.position = startpos;
		bInit = false;

	}
	
	// Update is called once per frame
	void Update () {
		if (bstart && bInit == false) {
			var rb = this.GetComponent<Rigidbody> ();
			rb.isKinematic = false;

			var startpos = new Vector3(-55.245f ,5.622f ,80.472f);
			transform.position = startpos;
			bInit = true;
		}
	}

	void OnCollisionEnter(Collision other){
		var vel = this.GetComponent<Rigidbody> ().velocity;
		//vel.magnitude
		float  sound = vel.magnitude/2.0f;
		AudioSource.PlayClipAtPoint(ballsound, this.transform.localPosition,sound);
		//Debug.Log ("vel.magnitude："+vel.magnitude);



	}
}
