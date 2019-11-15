using UnityEngine;
using System.Collections;

public class phone : MonoBehaviour {
	GameObject plug;
	bool isCharge;
	public AudioClip AC;
	public AudioClip ACBlood;
	public GameObject blood;

	public GameObject imgCharging;
	GameObject parentRope;
	AudioClip[] tclips;

	GameObject[] tPics;
	public AudioSource xx;

	int CDChargeing;
	// Use this for initialization
	int nBattery;
	int nBatteryGrade;
	int[] tBatteryGrade;

	public GameObject pic1;
	public GameObject pic2;
	public GameObject pic3;
	public GameObject pic4;
	public GameObject pic0;


	bool bHigh;
	void Start () {
		isCharge = false;
		nBattery = 0;
		nBatteryGrade = 0;
		bHigh = false;

		tclips = new AudioClip[17];
		for(int i=0;i<=16;i++){
			tclips[i] = (AudioClip)Resources.Load ("sound/van/van"+i) ;
		}



		tPics = new GameObject[5];
		///tPics = {pic1,pic2,pic3,pic4,pic5};
		tPics[0] = pic0;
		tPics[1] = pic1;
		tPics[2] = pic2;
		tPics[3] = pic3;
		tPics[4] = pic4;

		pic1.GetComponent<Renderer> ().enabled = false;
		pic2.GetComponent<Renderer> ().enabled = false;
		pic3.GetComponent<Renderer> ().enabled = false;
		pic4.GetComponent<Renderer> ().enabled = false;
		pic0.GetComponent<Renderer> ().enabled = false;

		blood.GetComponent<EllipsoidParticleEmitter> ().emit = false;


		tBatteryGrade = new int[17];//{ 100,250,500,750};
		for(int i=0;i<=16;i++){
			tBatteryGrade[i] = i*100 + 100;

		}


	}


	public float GetBatteryPercent(){
		int maxBattery = tBatteryGrade [tBatteryGrade.Length-1];
		float test = 0.0025f *(float)(nBattery);

		float  ret = Mathf.Atan (test);
		return ret;
	}

	void SetShowPic(){
		
		//if (nBatteryGrade<3) {
			
		int[] tPicGrade = new int[5];
		tPicGrade[0] = 4;
		tPicGrade[1] = 8;
		tPicGrade[2] = 12;
		tPicGrade[3] = 16;
		tPicGrade[4] = 20;
		pic1.GetComponent<Renderer> ().enabled = false;
		pic2.GetComponent<Renderer> ().enabled = false;
		pic3.GetComponent<Renderer> ().enabled = false;
		pic4.GetComponent<Renderer> ().enabled = false;
		pic0.GetComponent<Renderer> ().enabled = false;


		for(int i=0;i<5;i++){
			if (nBatteryGrade < tPicGrade [i]) {
				tPics[i].GetComponent<Renderer> ().enabled = true;
			}
		}
				

	}


	// Update is called once per frame
	void Update () {
	
	}

	void OnPlugIn (GameObject obj) {
		plug = obj;
	}

	bool IsConnected()
	{
		if (parentRope == null)
			return false;
		
		bool b = parentRope.GetComponent<rope>().IsConnected();
		return b;
	}

	void Connected(bool bIsConnect)
	{
		isCharge = bIsConnect;
		AudioSource.PlayClipAtPoint(AC, transform.localPosition,1.0f);

		if (bIsConnect && !bHigh ) {
			imgCharging.GetComponent<Renderer> ().enabled = bIsConnect;
			CDChargeing = 60;
		}
	}


	void FixedUpdate ()
	{
		CDChargeing -= 1;
		if (CDChargeing <= 0) {
			imgCharging.GetComponent<Renderer> ().enabled = false;
		}


		bool bIsConnected = IsConnected();
		if (bIsConnected &&  !isCharge  ) {
			//连接
			Connected(bIsConnected);
			
		} else if(!bIsConnected &&  isCharge  ) {
			//断开
			Connected(bIsConnected);
		}






		bool bUp = true;
		int FazhiStep = 50;
		int Fazhi = FazhiStep;
		if (bIsConnected) {
			nBattery += 2;
			bUp = true;
			Fazhi = FazhiStep;
		} else {
			nBattery -= 0;
			bUp = false;
			Fazhi = -FazhiStep;
		}

		if (nBattery < 0) {
			nBattery = 0;
		}





		int maxBattery = tBatteryGrade [tBatteryGrade.Length-1];
		if (nBattery > maxBattery +100) {
				nBattery = maxBattery + 100;
		}


		int nBoundery = 0;
		if (bUp &&  nBatteryGrade < (tBatteryGrade.Length-1) ) {
			nBoundery = tBatteryGrade [nBatteryGrade + 1] + Fazhi;
		} else if( !bUp &&  nBatteryGrade > 0 ) {
			nBoundery = tBatteryGrade [nBatteryGrade - 1] + Fazhi;
		}

		if (bUp && nBattery > nBoundery &&  nBatteryGrade < (tBatteryGrade.Length-1) ) {
			nBatteryGrade += 1;
			if(nBatteryGrade == 16){
				SetHigh ();

			}
			var AC = tclips[nBatteryGrade];
			//AudioSource.PlayClipAtPoint(AC, transform.localPosition,0.1f+nBatteryGrade*0.05f);
		} else if (!bUp && nBattery < nBoundery &&  nBatteryGrade > 0 ) {

			nBatteryGrade -= 1;
			//SetShowPic ();
			var AC = tclips[nBatteryGrade];

			//AudioSource.PlayClipAtPoint(AC, transform.localPosition,0.1f+nBatteryGrade*0.05f);
		}

		//Debug.Log ("nBattery："+nBattery +"   nBatteryGrade:"+nBatteryGrade);
	}

	void SetHigh(){
		bHigh = true;
		SetShowPic ();
		blood.GetComponent<EllipsoidParticleEmitter> ().emit = true;
		AudioSource.PlayClipAtPoint(ACBlood, transform.localPosition,1.0f);
		var AC = tclips[10];
		//AudioSource.PlayClipAtPoint(AC, transform.localPosition,1.0f);

		//xx.clip = AC;
		//xx.loop = true;
		//xx.Play();
	}

	public void SetConnectedRope(GameObject o)
	{
		parentRope = o;
	}


}
