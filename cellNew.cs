using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class cellNew : MonoBehaviour {
	int EnergyNum;//能量数
	int nMyState;
	int myframe;
	Renderer myrederer;
	public int xCord;
	public int yCord;
	public int zCord;
	public GameObject prefabObj;
	int energypower;
	int WaitFrameToCalc; //等待N帧
	int MaxWaitFrameToCalc; //等待N帧
	gameoflife  g_scrp;
	Stack<energy> _stackenergy;
	Stack<cellNew> _stackcell;
	cellNew[][][] _cellsTable;
	energy[][][] _EnergyTable;
	bool bWorking;
	Vector3 startpoint;
	Behaviour behavior;
	void Start () {
		
	}


	void SetWorking(bool b)
	{	
		bWorking = b;
		behavior.enabled = b;
	}



	GameObject MainCube;

	public void init(GameObject gameObject,int x, int y,int z,int frame ,Stack<energy> stackenergy, Stack<cellNew> stackcell ){
		behavior = this.gameObject.GetComponent<Behaviour> ();
		startpoint = new Vector3 (10000,10000,10000);
		_stackenergy = stackenergy;
		_stackcell = stackcell;
		nMyState = 1;
		xCord = x;
		yCord = y;
		zCord = z;
		myframe = frame;
		MainCube = gameObject;
		g_scrp = gameObject.GetComponent<gameoflife> ();
		myrederer = this.gameObject.GetComponent<Renderer> ();
		WaitFrameToCalc = 0;
		MaxWaitFrameToCalc = 1;
		energypower = 0;
		_cellsTable = g_scrp.g_cellsTable;
		_EnergyTable = g_scrp.g_EnergyTable;
		//SetWorking (false);
		SetLife (0);
	}

	public void SetInfo(int x, int y,int z,int frame){
		nMyState = 1;
		xCord = x;
		yCord = y;
		zCord = z;
		myframe = frame;

		WaitFrameToCalc = 0;
		energypower = 0;
		SetWorking (true);

		if (g_scrp.g_cellsTable [x] [y] [z]) {
			throw new UnityException ();
		}

		g_scrp.g_cellsTable [x] [y] [z] = this;

		myrederer.material.color = g_scrp.getCubeColor ();

	}


	int ToDead()
	{
		return 0;//死亡
	}

	int ToLive()
	{
		return 1;//复活
	}

	int Stay()
	{
		return nMyState ;
	}

	int CalcLive(){
		int nNewLife;
		if (energypower >= 5 && energypower <=7) {
			nNewLife = Stay();
		}else if (energypower >= 8 && energypower <=10) {
			nNewLife = ToLive();
		}else
		{
			nNewLife = ToDead();
		}
		return nNewLife;
	}

	public void AddEnergy()
	{ 
		energypower+= 1;
	}

	cellNew getCell(int newx,int newy,int newz)
	{
		return _cellsTable[newx] [newy] [newz];
	}

	energy getEnergy(int newx,int newy,int newz)
	{
		return _EnergyTable[newx] [newy] [newz];
	}

	void setEnergy(int newx,int newy,int newz , energy newenergy)
	{
		_EnergyTable[newx] [newy] [newz]  = newenergy;
	}

	void EmitEnergy (){
		for (int ix = -1; ix <= 1; ix++) {
			for (int iy = -1; iy <= 1; iy++) {
				for (int iz = -1; iz <= 1; iz++) 
				{
					if(ix==0&&iy==0&&iz==0)continue;
					int newx = xCord + ix;
					int newy = yCord + iy;
					int newz = zCord + iz;

					var cell = getCell (newx, newy, newz); 
						if(cell)
						{
							cell.AddEnergy ();
							continue;
						}
							
					var energy = getEnergy (newx, newy, newz); 
					if (energy) {
						energy.AddEnergy ();
					} else {
						var newenergy = _stackenergy.Pop ();
						setEnergy (newx, newy, newz,newenergy);					
						newenergy.SetInfo (newx, newy, newz,new Vector3(xCord,yCord,zCord));
					}
				}
			}
		}
	}


	// Update is called once per frame
	void Update () {
		if (!bWorking) {
			return;
		}

		if (g_scrp.g_Reset) {
			SetLifeAndCellTable (0);
			return;
		}

		int frameNow = g_scrp.getFrame();

		if (myframe != frameNow) {
			//向四周发射

			if  (WaitFrameToCalc == 0) {
				EmitEnergy ();
				WaitFrameToCalc++;
				return;
				}
			else if (WaitFrameToCalc < MaxWaitFrameToCalc) {
				WaitFrameToCalc++;
				return;
			}


			int nstate = CalcLive ();
			energypower = 0;
			WaitFrameToCalc = 0;
			myframe = frameNow;
			SetLifeAndCellTable (nstate);
		}
	}

	void SetLife(int n)
	{
		nMyState = n;


		if (n == 0) {

			SetWorking (false);
			int nowcount = _stackcell.Count;


			_stackcell.Push (this);
			this.gameObject.transform.position = startpoint  + nowcount *Vector3.up;
		}


	}

	public void SetLifeAndCellTable(int n)
	{
		nMyState = n;
	
		if (n == 0) {
			SetWorking (false);
			int nowcount = _stackcell.Count;

			_stackcell.Push (this);
			this.gameObject.transform.position = startpoint  + nowcount *Vector3.up;
			g_scrp.g_cellsTable [xCord] [yCord] [zCord] = null;
		}

	}

}
