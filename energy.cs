using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class energy : MonoBehaviour {
	public bool bWorking;
	//BoxCollider boxcolid;
	// Use this for initialization
	public bool bturn;
	public int energypower;
	int WaitFrameToCalc; //等待N帧
	int MaxWaitFrameToCalc; //等待N帧
	int xCord;
	int yCord;
	int zCord;
	public Vector3 parentCord;
	public GameObject GameMainCube;
	Stack<energy> _stackenergy;
	Vector3 hidepoint;
	gameoflife g_scrp;
	energy[][][] _EnergyTable;
	Behaviour behavior;
	void Start () {
		hidepoint = new Vector3 (-10000,-10000,-10000);
	}

	public void DestroySelf(){
		energypower = 0;
		SetWorking (false);
		_stackenergy.Push (this);
		this.gameObject.transform.position = hidepoint + _stackenergy.Count*Vector3.down;

		_EnergyTable[xCord][yCord][zCord] = null;
	}


	void SetWorking(bool b)
	{	
		bWorking = b;
		//boxcolid.enabled = b;
		behavior.enabled = b;
	}

	public void Init (int x, int y,int z , GameObject gob,Stack<energy> stackenergy , bool b ,gameoflife scpt){
		behavior = this.gameObject.GetComponent<Behaviour> ();
		_stackenergy = stackenergy;
		xCord = x;
		yCord = y;
		zCord = z;
		GameMainCube = gob;
		energypower = 1;
		WaitFrameToCalc = 0;
		MaxWaitFrameToCalc = 1;
		//boxcolid = this.gameObject.GetComponent<BoxCollider> ();
		SetWorking (b);
		g_scrp = scpt;
		_EnergyTable = g_scrp.g_EnergyTable;
		parentCord = new Vector3(-10,-10,-10);
	}


	public void SetInfo (int x, int y,int z ,Vector3 v3) {
		xCord = x;
		yCord = y;
		zCord = z;

		energypower = 1;
		WaitFrameToCalc = 0;

		SetWorking (true);
		parentCord = v3;
	}



	// Update is called once per frame
	void Update () {
		
		if (bWorking == false) {
			return;
		}

		if (g_scrp.g_Reset) {
			DestroySelf ();
			return;
		}	

		WaitFrameToCalc++;

		if (WaitFrameToCalc < MaxWaitFrameToCalc) {
			return;
		}

		if (energypower >= 8 && energypower <=10) {
			GameMainCube.GetComponent<gameoflife> ().PutACube (xCord, yCord, zCord);
		}
		DestroySelf ();
	}

	public void AddEnergy()
	{
		energypower += 1;
	}


	/*
	void OnTriggerEnter(Collider other){
		if (bWorking == false) {
			return;
		}

		energy scrp = other.gameObject.GetComponent<energy> ();

		if(scrp == null ){
			return;
		}

		if(scrp.bWorking == false) 
		{
			return;
		}
		

		if (energypower > 0) {
			
			if (scrp) {
				energypower += scrp.energypower;
				scrp.DestroySelf();

				return;
			}

		}
	}
	*/
}
