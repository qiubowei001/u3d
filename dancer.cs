using UnityEngine;
using System.Collections;

public class dancer : MonoBehaviour {

	// Use this for initialization
	void Start () {
		var anim = GetComponent<Animation> ();
		var animt = GetComponent<Animator> ();
		var spr = GetComponent<SpriteRenderer> ();
		animt.speed = 0.2f;
		animt.enabled = false;
		spr.enabled = false;
		/*
		foreach (AnimationState state in anim) {
			state.speed = 0.5f;
		}
		*/
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void startDance(){
		var animt = GetComponent<Animator> ();
		animt.enabled = true;

		var audiosource = GetComponent<AudioSource> ();
		audiosource.enabled = true;

		var spr = GetComponent<SpriteRenderer> ();
		spr.enabled = true;
	}
}
