using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//生命游戏细胞
public class cell : MonoBehaviour {

	int  g_nLife;
	bool bEdgeCell;
	cell[] tCellsAround;
	int xCord;
	int yCord;
	int zCord;
	int myframe;

	Dictionary<int,int[][][]> TFrameLiveState;
	int[][][] tframeStateMine;
	GameObject g_gameObject;
	public int[] TCelltest;
	Renderer myrederer;
	// Use this for initialization
	void Start () {
		

	}


	void SetEdgeCell(bool b)
	{
		bEdgeCell = b;
		if(b)SetLife (0);
	}

	public void Init (int x, int y,int z ,int Index , GameObject[] tCells, int[,,] indexTCells ,int length ,int frame ,GameObject gameObject) {
		xCord = x;
		yCord = y;
		zCord = z;
		myframe = frame;
		myrederer = this.gameObject.GetComponent<Renderer> ();
		g_scrp = gameObject.GetComponent<gameinit> ();
		int indexAround = 0;
		tCellsAround = new cell[26];

		TFrameLiveState =  gameObject.GetComponent<gameinit> ().TFrameLiveState;
		SetFrameBuffer ();

		for (int ix = -1; ix <= 1; ix++) {
			for (int iy = -1; iy <= 1; iy++) {
				for (int iz = -1; iz <= 1; iz++) {

					if (ix == 0 && iy == 0 && iz == 0)
						continue;

					if ((x + ix) < 0 || (x + ix) >= (length))
						continue;

					if ((y + iy) < 0 || (y + iy) >= (length))
						continue;

					if ((z + iz) < 0 || (z + iz) >= (length))
						continue;


					int index = indexTCells [x + ix,y + iy,z + iz];
					var cellt = tCells [index];

					tCellsAround [indexAround++] = cellt.GetComponent<cell>();
				}
			}
		}

		SetGameMainObj(gameObject);

		if (x <= 0 || x >= (length-1) || y <= 0 || y >= (length-1) || z <= 0 || z >= (length-1)) {
			SetEdgeCell (true);
		} else {
			SetEdgeCell (false);
		}
	}

	void SetGameMainObj(GameObject gameObject){
		g_gameObject =gameObject;
	}

	delegate bool testDelegate(bool b);

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
		//return TFrameLiveState [myframe] [xCord][ yCord][ zCord] ;
		return tframeStateMine[xCord][ yCord][ zCord] ;
	}
	

	int ComputerAliveNew()
	{
		
		int testNum = 0;
		int[][][] tframeState = tframeStateMine;//TFrameLiveState [myframe];

		//tframeStateMine = TFrameLiveState [myframe];

		//int* p = &tframeState [xCord - 1, yCord - 1, zCord - 1];



		testNum = 
		tframeState[xCord-1][yCord-1][zCord-1] +
		tframeState[xCord-1][yCord-1][zCord] +
		tframeState[xCord-1][yCord-1][zCord+1] +
		tframeState[xCord-1][yCord][zCord-1] +
		tframeState[xCord-1][yCord][zCord] +
		tframeState[xCord-1][yCord][zCord+1]+
		tframeState[xCord-1][yCord+1][zCord-1] +
		tframeState[xCord-1][yCord+1][zCord] +
		tframeState[xCord-1][yCord+1][zCord+1]+

		tframeState[xCord][yCord-1][zCord-1] +
		tframeState[xCord][yCord-1][zCord] +
		tframeState[xCord][yCord-1][zCord+1] +
		tframeState[xCord][yCord][zCord-1] +
		tframeState[xCord][yCord][zCord+1]+
		tframeState[xCord][yCord+1][zCord-1] +
		tframeState[xCord][yCord+1][zCord] +
		tframeState[xCord][yCord+1][zCord+1]+

		tframeState[xCord+1][yCord-1][zCord-1] +
		tframeState[xCord+1][yCord-1][zCord] +
		tframeState[xCord+1][yCord-1][zCord+1] +
		tframeState[xCord+1][yCord][zCord-1] +
		tframeState[xCord+1][yCord][zCord] +
		tframeState[xCord+1][yCord][zCord+1]+
		tframeState[xCord+1][yCord+1][zCord-1] +
		tframeState[xCord+1][yCord+1][zCord] +
		tframeState[xCord+1][yCord+1][zCord+1];


		int bNewLife;
		if (testNum >= 5 && testNum <=7) {
			bNewLife = Stay();
		}else if (testNum >= 8 && testNum <=10) {
			bNewLife = ToLive();
		}else
		{
			bNewLife = ToDead();
		}




		return bNewLife;
	}
	gameinit  g_scrp;
	// Update is called once per frame
	void Update(){
		if (bEdgeCell) {
			return;
		}

		/*
		if (!(g_scrp.ifCanCalThisUpdate())){
			return;
		}
*/
		int frameNow = g_scrp.getFrame();



		if(myframe!= frameNow){

			int liveret  = ComputerAliveNew ();
			//周围都计算完毕
			myframe = frameNow;

			SetFrameBuffer ();
			SetLife (liveret);
			g_gameObject.GetComponent<gameinit> ().CellCalcDone();

		}

	}

	void SetFrameBuffer()
	{
		tframeStateMine = TFrameLiveState [myframe];
	}

	public void SetLife(int n)
	{
		bool borgin = myrederer.enabled;
		myrederer.enabled = (n==1);

		//new Color(,,)
		if (!borgin && myrederer.enabled)myrederer.material.color= g_scrp.getCubeColor();

		tframeStateMine [xCord][ yCord][ zCord] = n;

	}



}
