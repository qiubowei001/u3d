using UnityEngine;
using System.Collections;

public class control : MonoBehaviour {
	public Rigidbody rb;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
		rb.maxAngularVelocity = 1;
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	void FixedUpdate () {



		float maxspeed = 10;
		float maxspeedY = 40;
		float speed = rb.velocity.magnitude;

		string keyR = "";
		string keyL = "";
		string keyF = "";
		string keyB = "";

		keyR = "d";
		keyL = "a";
		keyF = "w";
		keyB = "s";



		if (true) {//< BCRange) {
			//Input.GetKey(KeyCode.
			if (Input.GetKey (keyR)) {
				rb.AddForce (10.1f * transform.right);
				//rb.velocity +=  0.1f*cubetrack.transform.right;//new Vector3(step,0,0);

			}
			if (Input.GetKey (keyL)) {
				rb.AddForce (-0.1f * transform.right);
				//rb.velocity += -0.1f*cubetrack.transform.right ;//new Vector3(-step,0,0);

			}
			if (Input.GetKey (keyF)) {
				rb.AddForce (0.1f * transform.up);
				//rb.velocity += step*cubetrack.transform.forward; //new Vector3(0,0,step);
			}
			if (Input.GetKey (keyB)) {
				rb.AddForce (-0.1f * transform.up);
				//rb.velocity += -step*cubetrack.transform.forward;//new Vector3(0,0,-step);
			}
		}

	}
}
