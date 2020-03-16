using UnityEngine;
using System.Collections;

public class pooeater : MonoBehaviour {
	public bool bStart;
	bool bEnd;
	public AudioClip coughsound;
	public door2 door;
	// Use this for initialization
	void Start () {
		soundcd = 0;
		bEnd = false;
	}
	int soundcd;
	// Update is called once per frame
	void Update () {
		if (bStart == false )
			return;
			
		if (soundcd > 100  && !bEnd) {
			AudioSource.PlayClipAtPoint (coughsound, this.transform.localPosition, 1.0f);
			soundcd = 0;
		}
		soundcd++;



		if (door.bIsOpen) {
			//隐藏
			bEnd = true;
			float f_trans = this.GetComponent<Renderer> ().material.color.a;

			if (f_trans > 0) {
				f_trans -= 0.05f;
			}
			this.GetComponent<Renderer> ().material.color = new Color (1.0f, 1.0f, 1.0f, f_trans);
		} else {
			bEnd = false;
			float f_trans = this.GetComponent<Renderer> ().material.color.a;;

			if (f_trans <1.0f) {
				f_trans += 0.05f;
			}
			//显现
			this.GetComponent<Renderer> ().material.color = new Color (1.0f, 1.0f, 1.0f,f_trans);
		}

	}
}
