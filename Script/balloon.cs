using UnityEngine;
using System.Collections;

public class balloon : MonoBehaviour {
	public Rigidbody rb;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate () {
		Vector3 vSpeed = rb.velocity;
		vSpeed = new Vector3(vSpeed.x,0,vSpeed.z);
		Vector3 forceZuNi = -0.05f*vSpeed;

		rb.velocity += forceZuNi;


		//下落速度阀值
		float y= Mathf.Max (-3, rb.velocity.y);
		rb.velocity = new Vector3(rb.velocity.x, y, rb.velocity.z);
	}	
}


