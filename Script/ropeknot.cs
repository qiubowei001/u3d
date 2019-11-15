using UnityEngine;
using System.Collections;

public class ropeknot : MonoBehaviour {
	public Vector3 Mousestartpos;
	public Vector3 knotstartpos;
	internal Rigidbody RBody;
	public Transform phone_transform;//跟踪的对象

	public bool bIsEnd;
	bool bWeakJoint;

	// Use this for initialization
	public int MatchTargetWeightMask;
	public GameObject nextRopeKnot;
	GameObject parentRope;

	void Start () {
		Mousestartpos =  Vector3.zero;
		knotstartpos = Vector3.zero;
		MatchTargetWeightMask = LayerMask.GetMask("floor");
		this.RBody = this.gameObject.GetComponent<Rigidbody>();

		phone_transform = GameObject.Find("phoneplug").transform;
	}

	public void SetIsEnd(bool b)
	{
		bIsEnd = b;
	}

	public void SetParentRope(GameObject o)
	{
		parentRope = o;
	}


	public void SetWeakKnot(bool b)
	{
		bWeakJoint = b;
	}


	Vector3 GetHitFloorPos() {
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit,100f,MatchTargetWeightMask)) {
			//GameObject floor = hit.collider.gameObject;
			return hit.point;
		}

		return Vector3.zero;
	}


	// Update is called once per frame
	void FixedUpdate ()
	{
		var pos = this.gameObject.transform.position;
		if (pos.y < 0)
			pos.y = 0;
		this.gameObject.transform.position = pos;


		var vx = this.RBody.velocity.x;
		var vy = this.RBody.velocity.y;
		var vz = this.RBody.velocity.z;
		float speedmax = 0.1f;

		this.RBody.velocity = new Vector3 (Mathf.Max (Mathf.Min (vx, speedmax), -speedmax), Mathf.Max (Mathf.Min (vy, speedmax), -speedmax), Mathf.Max (Mathf.Min (vz, speedmax), -speedmax));

		//Debug.Log ("mo:"+this.RBody.velocity.sqrMagnitude);

		if (Input.GetMouseButton(0)){//Input.GetMouseButtonDown (0)) {
			if (Mousestartpos == Vector3.zero) {
				//从摄像机发出射线的点
				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				RaycastHit hit;
				if (Physics.Raycast (ray, out hit)) {
					GameObject go = hit.collider.gameObject;
					if (go == this.gameObject) {
						Mousestartpos = GetHitFloorPos ();//Input.mousePosition;
						knotstartpos = this.transform.position;
					}
				}
			} else {
				if (Mousestartpos != Vector3.zero) {	
					var diff = GetHitFloorPos () - Mousestartpos;
					//Debug.Log (diff.x + " " + diff.y + " " + diff.z);
					var nowpos = knotstartpos + diff;
					this.transform.position = nowpos;
				}
			}
		}
		else if (Input.GetMouseButtonUp (0)) {
				Mousestartpos = Vector3.zero;
				knotstartpos = Vector3.zero;
				return;
			}
	}


	void OnCollisionEnter(Collision other){
		if (bIsEnd){
			
			var phoneobj = other.gameObject.GetComponent<phone> ();
			if (phoneobj != null) {
				this.transform.position = phone_transform.position;

				this.transform.eulerAngles =new Vector3(270,0,0);
				this.RBody.isKinematic = true;

				phoneobj.SetConnectedRope (parentRope);
			}

		}
	}



	float angle_360(Vector3 from_, Vector3 to_){

		Vector3 v3 = Vector3.Cross(from_,to_);

		if(v3.z > 0)

			return Vector3.Angle(from_,to_);

		else

			return 360-Vector3.Angle(from_,to_);

	}


	public bool IsConnected()
	{
		if (bWeakJoint && nextRopeKnot) {
			//纵向夹角
			float fMaxAngleJiaJiao = 1.0f;
			float fMinAngleJiaJiao = 1.0f;
			//平面夹角
			float fMaxAngleFlat = 1.0f;
			float fMinAngleFlat = 1.0f;
			Vector3 v3flat = transform.forward;

			Vector3 v3Next = nextRopeKnot.transform.up;
			Vector3 v3This = transform.up;
			float anglesJiajiao = angle_360 (v3Next,v3This);
			Vector3 v3Touying = Vector3.Cross(v3Next,v3This);
			float anglesflat = angle_360(v3flat,v3Touying);

			//Debug.Log ("anglesJiajiao："+anglesJiajiao +"   anglesflat:"+anglesflat);

			bool b1 = (anglesJiajiao < 30 || anglesJiajiao > 330 );
			bool b2 = (anglesflat < 100 || anglesflat < 260 );

			if (b1  && b2) {
				return true;
			}else{
				return false;	
			}
				




		} else {
			return true;
		}
	}
}


