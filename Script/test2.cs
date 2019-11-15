using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
public class test2 : MonoBehaviour {
	public Rigidbody rb;
	// Use this for initialization
	bool breset;
	public int Player;
	bool bresetAdJust;
	Vector3 originposition;
	Quaternion originrotation;
	Transform origintranform;
	int test = 1;
	public GameObject cubetrack;
	public GameObject head;
	bool InBengChunagRange() {
		float BCRange = 2;
		GameObject  plane = GameObject.Find("Cube (1)");
		float distance =transform.position.y - plane.transform.position.y ;
		return distance < BCRange;
	}	

	void OnClick(){
		
		rb.transform.position = originposition;
		rb.transform.rotation = originrotation;
		rb.velocity = Vector3.zero;

	}

	void Start () {
		//transform.position.
		rb = GetComponent<Rigidbody>();
		rb.maxAngularVelocity = 1;
		breset = false;
		bresetAdJust = false;
		originposition = rb.transform.position;
		originrotation = rb.transform.rotation;

		GameObject btnObj = GameObject.Find ("ButtonReset");
		head = GameObject.Find("head");
		Button btn = btnObj.GetComponent<Button> ();
		btn.onClick.AddListener (delegate() {
			this.OnClick ();
		});


		cubetrack = GameObject.Find ("Cubetrack");

	}
	
	// Update is called once per frame
	void Update () {}


	bool IsTouchGround(){
		Vector3 fwd = Vector3.down;//transform.TransformDirection(-Vector3.up);
		 bool grounded =  Physics.Raycast(transform.position,fwd, 3 );
		return grounded;
	}

	void FixedUpdate () {
			


		float maxspeed = 10;
		float maxspeedY = 40;
		float speed = rb.velocity.magnitude;
		 
		bool bTouchGround = IsTouchGround ();
		if (bTouchGround) {
			test += 1;
			//Debug.Log ("touchground:"+test);
		}

		float step = 0.5f;//越

		if (bTouchGround) {
			step =  0.1f;
		}

		string keyR = "";
		string keyL = "";
		string keyF = "";
		string keyB = "";

		if (Player == 1) {
			keyR = "d";
			keyL = "a";
			keyF = "w";
			keyB = "s";
		} else {
			keyR = "l";
			keyL = "j";
			keyF = "i";
			keyB = "k";
		}


		if (true){//< BCRange) {
			//Input.GetKey(KeyCode.
			if (Input.GetKey (keyR)) {
				rb.velocity +=  0.1f*cubetrack.transform.right;//new Vector3(step,0,0);

			}
			if (Input.GetKey (keyL)) {
				rb.velocity += -0.1f*cubetrack.transform.right ;//new Vector3(-step,0,0);

			}
			if (Input.GetKey (keyF)) {
				rb.velocity += step*cubetrack.transform.forward; //new Vector3(0,0,step);
			}
			if (Input.GetKey (keyB)) {
				rb.velocity += -step*cubetrack.transform.forward;//new Vector3(0,0,-step);
			}
			//限制平面移动速度sssssssssssssss
			Vector2  tmp = new Vector2(rb.velocity.x , rb.velocity.z );
			float Mo = tmp.magnitude;
			if (Mo > maxspeed) {
				float bili = maxspeed/Mo;
				tmp *= bili;
				rb.velocity = new Vector3 (tmp.x, rb.velocity.y, tmp.y);
			}
			float newspeedy = Mathf.Max (Mathf.Min (maxspeedY, rb.velocity.y), -maxspeedY);
			rb.velocity = new Vector3 (rb.velocity.x, newspeedy, rb.velocity.z);

		}



		if (Input.GetKeyDown ("h")) {
			//rb.angularVelocity  +=  new Vector3(0f, 600f, 0f);
			//rb.angularVelocity = this.transform.forward * 100;
			//this.transform.Rotate(Vector3.up*100);
			Vector3 Torque= Vector3.up;

			Vector3 offset = new Vector3(50,0,0);
			Vector3 Xoffset=new Vector3(offset.x,0,0);
			Vector3 test = Vector3.Cross (Xoffset, Vector3.forward).normalized;
			rb.angularVelocity = Vector3.zero;
			rb.AddTorque(transform.up *1000);


		
			//Vector3 offset = new Vector3(50,0,0);
			//Vector3 Xoffset=new Vector3(offset.x,0,0);
			//transform.Rotate(Vector3.Cross(Xoffset, Vector3.forward).normalized, offset.magnitude, Space.World);

		}	 

		if (Input.GetKey ("j")) {
			rb.AddTorque(transform.right *15);

		}	 

		if (Input.GetKey ("k")) {
			rb.AddTorque(-transform.right *15);

		}	

		/*
		if (breset) {
			if (Input.GetKeyDown ("space")) {
				//rb.AddForce (0, 200, 0);


				if (InBengChunagRange ()) {
					float t = (2f); 
					float newy = 0;
					if (rb.velocity.y >= 0) {
						newy = rb.velocity.y + power;
					} else if (rb.velocity.y < 0) {
						newy = rb.velocity.y - power;
					}

					rb.velocity = new Vector3 (rb.velocity.x, newy, rb.velocity.z);
					//speed
					breset = false;

				}

			}
		}
		*/

		AdJustPose ();
	}



	void OnCollisionEnter(Collision other){
		return;
		/*
		breset = true;
		bresetAdJust = true;
		float max = 0.5f;
		float power = 1500;
		float angelMax = 85.0f;

		Vector3 vSpeed =rb.velocity ;

		float angle = Vector3.Angle(vSpeed,rb.rotation*Vector3.down);
		Debug.Log("angle:"+angle);
		if (angle > angelMax) {
			
		} else {
			rb.AddForce (Random.Range (-max, max), power, Random.Range (-max, max));
		}

		*/
	}


	void AdJustPose (){
		
		float[] mList = {90.0f,180.0f,270.0f,360.0f};

		float mindiff = 999.0f;
		float setang = 0.0f;
		foreach (var ang in mList){
			float diff = Mathf.Abs(transform.eulerAngles.z - ang);
			if (mindiff < Mathf.Abs(transform.eulerAngles.z - ang) ) {
				mindiff = diff;
				setang = ang;
			}
		}


		transform.eulerAngles =new Vector3( rb.transform.eulerAngles.x , rb.transform.eulerAngles.y, setang);
	}

		

}
