using UnityEngine;
using System.Collections;

public class FpsController : MonoBehaviour {
	Vector3 StartPosition;  //左键按下时鼠标的位置
	Vector3 previousPosition;  //上一帧鼠标的位置。
	Vector3 offset; 
	Vector3 finalOffset;
	public GameObject cameraobj;
	public AudioClip walksound1;
	public AudioClip walksound2;
	public bool bMouseAble;
	Vector3 v3lastfootprint;
	Rigidbody rb;
	public bool bIsClimbing;
	int cdStair;
	public ghostball ballscript;
	// Use this for initialization
	void Start () {
		bMouseAble = true;
		nFootNum = 1;
		previousPosition = Vector3.zero;
		rb = this.GetComponent<Rigidbody> ();
		v3lastfootprint = this.transform.position;
		bIsClimbing = false;
	}

	public void resetStair()
	{
		cdStair = 100;

		if (ballscript) 
		{
			ballscript.bstart = true;
		}
	}

	public void AddPosition(Vector3 addv3){
		this.transform.position += addv3;
	}
	
	// Update is called once per frame

	void Update () {
		
		if (cdStair > 0) {
			cdStair--;
			if (cdStair <= 0) 
			{
				bIsClimbing = false;
			}
		} 

	}

	int nFootNum;
	void makeWalkSound (){
		
		Vector3  v3diff= (v3lastfootprint - this.transform.position);
		if (v3diff.magnitude >= 3.0f) {
			nFootNum++;
			v3lastfootprint = this.transform.position;
			if (nFootNum % 2 == 1) {
				AudioSource.PlayClipAtPoint (walksound1, transform.localPosition, 2.0f);
			}else{
				AudioSource.PlayClipAtPoint (walksound2, transform.localPosition, 2.0f);
			}
		}
	}

	void FixedUpdate () {
		makeWalkSound ();
		float maxspeed = 5;
		float maxspeedY = 25;

		float step = 0.5f;//越

		string keyR = "";
		string keyL = "";
		string keyF = "";
		string keyB = "";


			keyR = "d";
			keyL = "a";
			keyF = "w";
			keyB = "s";


		if (true){//< BCRange) {
			//Input.GetKey(KeyCode.
			if (Input.GetKey (keyR)) {
				rb.velocity +=  step*this.gameObject.transform.right;//new Vector3(step,0,0);

			}
			if (Input.GetKey (keyL)) {
				rb.velocity += -step*this.gameObject.transform.right ;//new Vector3(-step,0,0);

			}
			if (Input.GetKey (keyF)) {
				rb.velocity += step*this.gameObject.transform.forward; //new Vector3(0,0,step);
			}
			if (Input.GetKey (keyB)) {
				rb.velocity += -step*this.gameObject.transform.forward;//new Vector3(0,0,-step);
			}

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




			if (previousPosition == Vector3.zero) {
				previousPosition = Input.mousePosition;
				return;
			}



				StartPosition = Input.mousePosition;  //记录鼠标按下的时候的鼠标位置
				//previousPosition = Input.mousePosition;  //记录下当前这一帧的鼠标位置


				offset = Input.mousePosition - previousPosition; //这一帧鼠标的位置减去上一帧鼠标的位置就是鼠标的偏移量 
				previousPosition = Input.mousePosition; //再次记录当前鼠标的位置，以备下一帧求offset使用。
				//Vector3 Xoffset=new Vector3((offset.x),0,0);//过滤掉鼠标在Y轴方向上的偏移量，只保留X轴的
				//transform.Rotate(Vector3.Cross(Xoffset, Vector3.forward).normalized, (offset.magnitude)/4, Space.World);  //旋转
				
				//Vector3 Yoffset=new Vector3((offset.y),0,0);//过滤掉鼠标在Y轴方向上的偏移量，只保留X轴的
				//transform.Rotate(Vector3.Cross(Yoffset, Vector3.forward).normalized, (offset.magnitude)/4, Space.World);  //旋转
		if (bMouseAble) {
			this.gameObject.transform.Rotate (0,0.3f*offset.x, 0,Space.World);// , (offset.magnitude)/4, Space.World);  //旋转
			cameraobj.transform.Rotate (-0.3f*offset.y,0, 0,Space.Self);// , (offset.magnitude)/4, Space.World);  //旋转

		}

	}  


	void OnCollisionEnter(Collision other){
		

			var obj = other.gameObject;
		if (obj.name != "Plane") 
		{
			int test = 1;
			test++;
			return;
		}


		}
	}

