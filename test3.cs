using UnityEngine;
using System.Collections;

using System.Linq;
using System.Runtime.InteropServices;

using System.Collections;
using System.Collections.Generic;

public class test3 : MonoBehaviour {
	GameObject g_gameObject;
	int testNum;
	int type;
	int[] t;
	int[] tMapLifeType;
	byte[] tbin;
	int testtime;
	// Use this for initialization
	int[,,] tframeState;
	int[][][] tframeState2;
	void Start () {
		
		tbin = new byte[26];;
		testtime = 2000;
		int nseed = System.DateTime.Now.Second;
		Random.seed = nseed; 
		cd = 0;
		//tMapLifeType = new int[2,2,2,2,2,2,2,2,2];

		//for

		int length = 2;
		tframeState = new int[2, 2, 2];

		tframeState2 = new int[2][][];//[2][2];
		tframeState2[0] = new int[2][];
			tframeState2[0][0] = new int[2];
			tframeState2[0][1] = new int[2];
		tframeState2[1] = new int[2][];
			tframeState2[1][0] = new int[2];
			tframeState2[1][1] = new int[2];

		int n = 0; 
	//	int* pt = stackalloc int[8];

		for (int x = 0; x < length; x++) {
			for (int y = 0; y < length; y++) {
				for (int z = 0; z < length; z++) {
					tframeState [x, y, z] = n ;
					tframeState2[x][y][z] =n;
					//pt[n] = n;
					n++;
				}
			}
		}

		int nlength = 100000;
		_list = new ArrayList();
		for (int i = 0; i < nlength; i++) 
		{
			_list.Add(1);
		}


		dic = new Dictionary<int,int> ();
		for (int i = 0; i < nlength; i++) 
		{
			dic [i] = 1;
		}


	
		//int* p = &tframeState [xCord - 1, yCord - 1, zCord - 1];
		//int * _xt  = tframeState2[1][1][1];//tframeState [1, 1, 1];// &(tframeState2[1][1][1]);
		//int p = t[3];//&(tframeState [1, 1, 1];

		int gLength = 100;
		GameObject[][] g_gameObject =new GameObject[gLength][];
		for (int i = 0; i < gLength; i++) {
			g_gameObject [i] = new GameObject[gLength];
		}

		g_gameObject [2][2] = new GameObject();

	}

	void GenerateT()
	{
		for(int i=0;i<26;i++){
			t[i] = Random.Range(0,1);
		}

		for(int i=0;i<26;i++){
			tbin[i] =(byte) Random.Range(0,1);
		}
	}



	void test2(){
		for (int j = 0; j < testtime; j++) {
			testNum = t [0] + t [1] + t [2] + t [3] + t [4] + t [5] + t [6] + t [7] + t [8] + t [9] + t [10] + t [11] + t [12] + t [13] + t [14] + t [15] + t [16] + t [17] + t [18] + t [19] + t [20] + t [21] + t [22] + t [23] + t [24] + t [25];
			if (testNum < 5) {
				type = 1;
			} else if (testNum < 15 || testNum > 10) {
				type = 2;
			} else {
				type = 3;
			}
		}
	}




	ArrayList  _list;
	public Dictionary<int,int> dic;

	int test1(){
		
		int i = 1;
		for (int j = 0; j < t.Length; j++) {
				i +=  t [j];
			}

		return i;
	}


	int test33(){
		int i = 1;
		foreach (KeyValuePair<int,int> item in dic) 
		{
			i += item.Value;
		}

		return i;

	}

	int test44(){
		int i = 1;
		foreach (int item in _list) 
		{
			i += item ;
		}

		return i;

	}

	int cd;
	// Update is called once per frame
	void Update () {
		if (cd < 5) {
			
			cd++;
			return;
		}
		
		//GenerateT ();

			test1 ();
			//test2 ();
			test44 ();
			test33 ();
	}
}
