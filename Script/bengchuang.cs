﻿using UnityEngine;
using System.Collections;

public class bengchuang : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter(Collision other){
		float max = 0.5f;
		float power = 70;
		float angelMax = 150.0f;

		Vector3 vSpeed =other.rigidbody.velocity ;


		other.rigidbody.AddForce (Random.Range (-max, max), power, Random.Range (-max, max));



	}

}
