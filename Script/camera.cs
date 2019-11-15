using UnityEngine;
using System.Collections;

public class camera : MonoBehaviour {
	private Vector3 offset;
	public Transform playerTransform;//跟踪的对象
	public Transform camareTransform;
	public Transform targetTransform;
	public GameObject pphone;
	Vector3 startposition;
	// Use this for initialization
	void Start () {
		
		offset = transform.position - playerTransform.position;//计算相对距离
		transform.position = playerTransform.position;// + offset; //保持相对距离
		startposition = camareTransform.position;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//transform.position = playerTransform.position;// + offset; //保持相对距离
		//transform.eulerAngles = new Vector3(  transform.eulerAngles.x ,playerTransform.eulerAngles.y ,transform.eulerAngles.z ); //Y旋转跟随主角
		//Debug.Log("eulerAngles:"+ transform.eulerAngles.x +" "+playerTransform.eulerAngles.y+" "+transform.eulerAngles.z);
		Vector3 targetpos = targetTransform.position;
		Vector3 dir =targetpos - startposition ;
		float percent = pphone.GetComponent<phone> ().GetBatteryPercent();
		Debug.Log ("percent:" + percent);

		camareTransform.position = startposition + dir * percent/2.0f;

		return;

	}
}
