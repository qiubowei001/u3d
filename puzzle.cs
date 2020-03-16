using UnityEngine;
using System.Collections;

public class puzzle : MonoBehaviour {
	public GameObject obj_set_1;
	public GameObject obj_set_2;
	public GameObject obj_set_3;
	public dancer dancer1;
	bool bPlayerIn;

	int state ;
	// Use this for initialization
	void Start () {
		state = 0;
	}
	
	// Update is called once per frame
	void Update () {
		var set_1 = obj_set_1.GetComponent<prop_set> ();
		var set_2 = obj_set_2.GetComponent<prop_set> ();
		var set_3 = obj_set_3.GetComponent<prop_set> ();

		if (state == 0) {//全部放入 显示碎片
			if (set_1.bIsSet && set_2.bIsSet && set_3.bIsSet) {
				//动画播放
				set_1.ShowPiece ();
				set_2.ShowPiece ();
				set_3.ShowPiece ();
				state = 1;
			}

		}
		else if (state == 1) //一条直线 开始蹦达
		{
			if (bPlayerIn) {
				set_1.HidePiece ();
				set_2.HidePiece ();
				set_3.HidePiece ();
			}

			dancer1.startDance ();

		}


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
