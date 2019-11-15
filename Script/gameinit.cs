using UnityEngine;

using System.Collections;
using System.Collections.Generic;
public class gameinit : MonoBehaviour {
	public GameObject prefabObj;
	// Use this for initialization
	GameObject[] tCells;
	int[,,] indexTCells;
	int g_Frame;
	int gLength;//正方体长度个数
	int nCalcuCellNum;
	int gCellAllNum;
	//GameObject camera;
	public GameObject cameracube;
	public Dictionary<int,int[][][]> TFrameLiveState;
	GameObject cellcenter;
	int MaxCalPerFrame;//每帧计算生命数最大值
	int thisFrameCalNum;//该帧计算生命数
	int nPerFrameUpdatesNum;//帧UPDATA数
	int nMaxPerFrameUpdatesNum;//每帧需要UPDATA数最大值

	void InitFrameBuffer (){
		TFrameLiveState = new Dictionary<int, int[][][]> ();

		//TFrameLiveState [0] = new int[gLength][][];// gLength, gLength];

		for (int k = 0; k < 2; k++) {
			TFrameLiveState [k] = new int[gLength][][];
			for (int i = 0; i < gLength; i++) {
				TFrameLiveState [k] [i] = new int[gLength][];
				for (int j = 0; j < gLength; j++) {
					TFrameLiveState [k] [i] [j] = new int[gLength];
				}
			}
		}
	

		//TFrameLiveState [1] = new int[gLength, gLength, gLength];
	}




	public int getFrame()
	{
		return g_Frame;
	}


	public Color getCubeColor()
	{
		int colorindex = (g_Frame*8) % 256;
		return ColorTable[colorindex];
	}

	Color[] ColorTable;

	void Start () {
		//UnityEngine.Profiler.maxNumberOfSamplesPerFrame = -1;

		ColorTable = new Color[256];
		for (int i = 0; i < 85; i++) {
			ColorTable [i] = new Color ( 1-(i/84.0f), i/84.0f ,0 );
		}
		for (int i = 0; i < 85; i++) {
			ColorTable [i+85] = new Color ( 0,1-(i/84.0f), i/84.0f );
		}
		for (int i = 0; i < 86; i++) {
			ColorTable [i+170] = new Color ( i/85.0f  , 0,1-(i/85.0f) );
		}



		MaxCalPerFrame = 90000;
		nMaxPerFrameUpdatesNum = 10;
		nPerFrameUpdatesNum = 0;

		test = 0;
		nCalcuCellNum = 0;
		g_Frame = 0;
		//gLength = 65;
		//gLength = 35;
		gLength = 78;
		int gLengthmin1 = gLength - 2;
		gCellAllNum = gLengthmin1*gLengthmin1*gLengthmin1;
		int RealAllNum = gLength * gLength * gLength;

		tCells = new GameObject[RealAllNum];
		indexTCells = new int[gLength,gLength,gLength];

		InitFrameBuffer ();


			int length = gLength;
			int nIndex = 0;
			for (int x = 0; x < length;x++) {
				for (int y = 0; y < length;y++){
						for (int z = 0; z < length;z++){
							var vec3 = this.gameObject.transform.position +   new Vector3(x,y,z);
							GameObject table;
							table = GameObject.Instantiate (prefabObj)as GameObject;
							table.transform.position = vec3;
							tCells [nIndex] = table;
							indexTCells[x,y,z] = nIndex;
							nIndex++;
						}
				}
			}	



			for (int x = 0; x < length;x++) {
				for (int y = 0; y < length;y++){
					for (int z = 0; z < length;z++){
						int Index = indexTCells[x,y,z];
						var _cell = tCells [Index] ;
						
						_cell.GetComponent<cell> ().Init(x,y,z ,Index ,tCells,indexTCells,length,g_Frame ,this.gameObject);


						if (x > 10 && x < length-10 && y > 10 && y < length-10 && z > 10 && z < length-10) {
							_cell.GetComponent<cell> ().SetLife (1);
						} else {
							_cell.GetComponent<cell> ().SetLife (0);
						}
										
					}
				}
			}	




		//摄像机跟踪最中心方块

		var index = indexTCells [(gLength / 2), (gLength / 2), (gLength / 2)];
		cellcenter = tCells [index];
		cameracube.transform.position = cellcenter.transform.position ;
		//camera.transform.LookAt (cameracube);

	}

	public void CellCalcDone(){
		nCalcuCellNum++;
		thisFrameCalNum++;
	}

	int test;

	//该update可否计算
	public bool ifCanCalThisUpdate(){
		return thisFrameCalNum < MaxCalPerFrame;
	}

	void FixedUpdate(){// Update() {


		test++;


		//|| test > 300
		if (test < 5 ) {
			return;
		} else if (test == 5) {
			g_Frame++;
		}

		thisFrameCalNum = 0;
		nPerFrameUpdatesNum++;

		//if (thisFrameCalNum > MaxCalPerFrame) {
		//	return;
		//}

		//是否全部计算完毕  且到了可跳帧update计数
		if (nCalcuCellNum >= gCellAllNum ){//&& nPerFrameUpdatesNum>nMaxPerFrameUpdatesNum ) {
			TFrameLiveState [g_Frame + 1] = TFrameLiveState [g_Frame - 1];
			TFrameLiveState.Remove (g_Frame - 1);
			g_Frame++;
			nCalcuCellNum = 0;
			nPerFrameUpdatesNum = 0;


		}


	}
}
