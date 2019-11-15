
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class gameoflife : MonoBehaviour {
	public GameObject prefab;
	public EditorGOL editor_scrp;
	public bool bStart;
	int g_Frame;
	public bool g_Reset;
	// Use this for initialization
	public int length;
	int nTimesUpdate;
	public int nMaxTimesUpdate;
	Stack<energy> g_stackenergy;
	Stack<cellNew> g_stackcells;
	public GameObject cameracube;

	int nInitEnergy;//初始energy数
	public GameObject prefabEnergy;
	int nInitCells;

	public int gLengthTable;//空间大小

	public cellNew[][][] g_cellsTable;
	//public Dictionary<int,int[][][]> TFrameLiveState;
	public energy[][][] g_EnergyTable;

	void InitTable (){
		g_EnergyTable = new energy[gLengthTable][][];

		for (int i = 0; i < gLengthTable; i++) {
			g_EnergyTable[i] = new energy[gLengthTable][];
			for (int j = 0; j < gLengthTable; j++) {
				g_EnergyTable[i] [j] = new energy[gLengthTable];
			}
		}


		g_cellsTable = new cellNew[gLengthTable][][];
		for (int i = 0; i < gLengthTable; i++) {
			g_cellsTable [i] = new cellNew[gLengthTable][];
			for (int j = 0; j < gLengthTable; j++) {
				g_cellsTable[i] [j] = new cellNew[gLengthTable];
			}
		}
	}


	public int getFrame()
	{
		return g_Frame;
	}


	void InitCellStack()
	{
		
		FillStackCell (nInitCells);

	}

	public void FillStackCell (int nNum){

		for (int i = 0; i < nNum; i++) {
			
			var scrp = CreateACube (10000, 10000 + g_stackcells.Count , 10000);
			//g_stackcells.Push (scrp);
		}
	}

	Vector3 hidepoint = new Vector3 (-10000,-10000,-10000);

	public void FillStackEnergy (int nNum){
		for (int i = 0; i < nNum; i++) {
			var energyobj = GameObject.Instantiate (prefabEnergy)as GameObject;	
			var energyscrp = energyobj.GetComponent<energy> ();

			energyscrp.Init (-10000 , -10000, -10000, this.gameObject,g_stackenergy,false,this);

			energyobj.transform.position = hidepoint+ g_stackenergy.Count*Vector3.down;;
			g_stackenergy.Push (energyscrp);
		}
	}

	void InitEnergyStack()
	{
		
		FillStackEnergy (nInitEnergy);

	}



	Color[] ColorTable;
	public Color getCubeColor()
	{
		int colorindex = g_Frame % g_numofColors;
		return ColorTable[colorindex];
	}

	int g_numofColors;
	void InitColorTable(){
		Color[] ColorGradeTable = new Color[3];
		ColorGradeTable[0] = new Color ( 1, 0 ,0 );
		ColorGradeTable[1] = new Color ( 0, 1 ,0 );
		ColorGradeTable[2] = new Color ( 0, 0 ,1 );



		int nFadeStep = 10;
		int nKeepStep = 5;
		g_numofColors = (nFadeStep + nKeepStep) * ColorGradeTable.Length;
		ColorTable = new Color[g_numofColors];
		int index = 0;

		for(int j = 0; j < ColorGradeTable.Length; j++) {
			for (int i = 0; i < nKeepStep; i++) {
				ColorTable [index] = ColorGradeTable[j];
				index++;
			}

			int nextj = j+1;
			if (j == (ColorGradeTable.Length - 1)) {
				nextj = 0;
			}

			var xstep = ColorGradeTable [nextj] - ColorGradeTable [j];

			for (int i = 0; i < nFadeStep; i++) {
				Color tt = xstep*((float)i/nFadeStep);
				ColorTable [index] =  ColorGradeTable [j] + tt ; // new Color ( 1-(i/nFadeStep), i/nFadeStep ,0 );
				index++;
			}
		}

	}

	public GameObject btnObjReset;
	public GameObject btnObjStart;

	void Start () {
		bStart = false;
		UnityEngine.Profiler.maxNumberOfSamplesPerFrame = -1;
		InitTable ();
		InitColorTable ();
		nInitEnergy  = gLengthTable*gLengthTable*gLengthTable/3;
		nInitCells = gLengthTable*gLengthTable*gLengthTable/3;
		nTimesUpdate = 0;
		//nMaxTimesUpdate = 2;
		//length = 3;
		g_Frame = 0;
		g_stackcells = new Stack<cellNew>();
		int test = g_stackcells.Count;
		g_stackenergy = new Stack<energy>();
		InitEnergyStack ();
		InitCellStack ();
		editor_scrp.Init ();

		Button btn1 = btnObjReset.GetComponent<Button> ();
		btn1.onClick.AddListener (delegate() {
			this.OnClickReset ();
		});

		Button btn2 = btnObjStart.GetComponent<Button> ();
		btn2.onClick.AddListener (delegate() {
			this.OnClickStart ();
		});



		/*PutACube (0, 0, 1);
		PutACube (0, 1, 1);
		PutACube (0, 2, 1);

		PutACube (0, 1, 0);
		PutACube (0, 1, 2);
		PutACube (1, 1, 1);
		*/
			

		/*
		int startindex = (gLengthTable -  length )/2;
		for (int x = startindex; x < startindex+length;x++) {
			//for (int y = startindex; y < startindex+length;y++){
			for (int y = startindex; y < startindex+3;y++){
				for (int z = startindex; z < startindex+length;z++){	
						PutACube (x, y, z);
					}
				}
			}
		*/


		//摄像机跟踪最中心方块
		var v3Center = new Vector3((gLengthTable / 2), (gLengthTable / 2), (gLengthTable / 2));
		cameracube.transform.position = v3Center;
		cameracube.GetComponent<selfrotate>().SetMode (0);
		editor_scrp.SetStartPos (v3Center);

	}


	void OnClickStart(){
		editor_scrp.OnStartGame ();
		cameracube.GetComponent<selfrotate>().SetMode (1);
		bStart = true;
	}

	void OnClickReset(){
		cameracube.GetComponent<selfrotate>().SetMode (0);
		g_Reset = true;
		bStart = false;
	}


	public  void RemoveCube(int x,int y,int z)
	{
		if (g_cellsTable [x] [y] [z]) {
			g_cellsTable [x] [y] [z].SetLifeAndCellTable(0);
		}
	}

	public cellNew PutACube(int x,int y,int z){
		if (g_stackcells.Count <= 0) {
			FillStackCell (50);
		}

		var cellscrp = g_stackcells.Pop ();
		cellscrp.SetInfo (x, y, z,g_Frame);
		cellscrp.gameObject.transform.position = new Vector3 (x,y,z);
		return cellscrp; 
	}

	public cellNew CreateACube(int x,int y,int z)
	{
		
		var vec3 = new Vector3(x,y,z);
		GameObject table;
		table = GameObject.Instantiate (prefab)as GameObject;
		var scrpt = table.GetComponent<cellNew> ();
		scrpt.init( this.gameObject,x,y,z,g_Frame,g_stackenergy,g_stackcells);
		//table.transform.position = vec3;
		return scrpt;

	}

	// Update is called once per frame
	void Update () {



		if (g_Reset) {
			if (g_stackcells.Count == nInitCells && g_stackenergy.Count == nInitEnergy) {
				g_Reset = false;
				editor_scrp.OnResetDone ();
			}
		}

		if (bStart == false) {
			return;
		}

		nTimesUpdate++;
		if (nTimesUpdate >= nMaxTimesUpdate )
		{
			nTimesUpdate = 0;
			g_Frame++;
		}

	}
}
