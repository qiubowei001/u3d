using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.UI;

public class testtest
{
	public List<Vector3> testarray;
}

public class filelist
{
	public List<string> list;
}




public class EditorGOL : MonoBehaviour {
	public gameoflife g_scrpt;
	List<Vector3> startarray;
	List<string> filearray;
	List<cellNew> startcellarray;
	string filelistpathname;
	public GameObject inputui;
	//public EditorBrush edit_brushscrp;
	string pathname;
	string sNowFileName;

	// Use this for initialization
	void Start () {
		this.GetComponent<Renderer> ().material.color = new Color (1.0f, 1.0f, 1.0f, 0.5f);
	}

	public GameObject btnObjClear;
	public GameObject btnObjSave;
	public GameObject btnObjNew;

	GameObject[] t_edit_brushs;

	void ReadFile(string jsonFileName)
	{
		//jsonFileName = "testjson.json";
		pathname = g_savepath + jsonFileName;

		startarray = ReadArray (jsonFileName);
		if (startarray == null) {
			startarray = new List<Vector3> ();
		}
		if (startcellarray == null) {
			startcellarray = new List<cellNew> ();
		}
		InitCellArray ();
	}

	string g_savepath;
	public void Init () 
	{
		g_savepath  = Application.dataPath + "/Resources/savegol/";

		fOrignScale = Vector3.zero;
		scrollitems = new List< GameObject>();
		string jsonFilelistName = "filenamelist.json";
		filelistpathname = g_savepath + jsonFilelistName;
		filearray = ReadFileList (filelistpathname);
		if (filearray == null) {
			filearray = new List<string>();
		}

		string jsonFileName = "testjson.json";
		sNowFileName = jsonFileName;
		ReadFile (jsonFileName);

		t_edit_brushs= GameObject.FindGameObjectsWithTag ("EditBrush");
		foreach(GameObject go in t_edit_brushs)
		{
			var edit_brushscrp = go.GetComponent<EditorBrush> ();
			edit_brushscrp.Init ();
		}

		Button btn2 = btnObjClear.GetComponent<Button> ();
		btn2.onClick.AddListener (delegate() {
			this.OnClickClear ();
		});


		Button btn3 = btnObjSave.GetComponent<Button> ();
		btn3.onClick.AddListener (delegate() {
			this.SaveFile ();
		});


		Button btn4 = btnObjNew.GetComponent<Button> ();
		btn4.onClick.AddListener (delegate() {
			this.NewFile ();
		});





		ShowInputUI (false);

		var inputfield = inputui.GetComponentInChildren<InputField> ();
		inputfield.onEndEdit.AddListener ( OnClickSaveOk);

		RefreshLeftUI ();

	}


	int FileNum;
	void NewFile()
	{
		OnClickClear ();
		sNowFileName =  "testjson.json";

	}

	void AddFileName(string filename)
	{
		string pathname = filelistpathname;
		string fullpath =  pathname;
		filearray.Add (filename);

		filelist tt = new filelist ();
		tt.list = filearray;
		string json = JsonUtility.ToJson (tt);

		if (Directory.Exists(fullpath))
		{
			File.Delete(fullpath);
		}

		File.WriteAllText(fullpath, json);

	}

	void ShowInputUI(bool b)
	{
		if (b) {
			inputui.GetComponent<RectTransform>().position   += 10000*Vector3.up;//  zero ;
		}else
		{
			inputui.GetComponent<RectTransform>().position   -= 10000*Vector3.up ;

		}
	}

	void SaveFile()
	{
		if (sNowFileName == "testjson.json") {
			ShowInputUI (true);
		} else {
			WriteArray (sNowFileName);
		}
	}

	void OnClickSaveOk(string sinput)
	{
		
		ShowInputUI (false);

		string filename = sinput;

		if (filename == "") {
			return;
		}

		sNowFileName = sinput;
			
		WriteArray (sinput);
		AddFileName(filename);
		RefreshLeftUI ();
	}



	void ChooseFile(string filename)
	{
		OnClickClear ();
		sNowFileName = filename;
		ReadFile (filename);
	}



	public GameObject tmpItem;
	public Transform scrolltransform;
	List< GameObject> scrollitems;
	public GameObject horlayoutgroup;
	void RefreshLeftUI ()
	{
		foreach (GameObject go in scrollitems) {
			GameObject.Destroy (go);
		}
		scrollitems.Clear();

		ScrollRect scrollview;
		//scrollview.

		foreach (string filename in filearray) {
			var v3 =  Vector3.zero;
			GameObject button = GameObject.Instantiate(tmpItem,v3,Quaternion.identity )as GameObject;;
			button.transform.parent = horlayoutgroup.transform;
			var label = button.GetComponentInChildren<Text>();
			label.text = filename;
			scrollitems.Add (button);
			//filearray.Add (filename);

			Button btn2 = button.GetComponent<Button> ();
			string stmp = filename;
			btn2.onClick.AddListener (delegate() {
					ChooseFile (stmp);
			});

		}

	}


	public void SetStartPos(Vector3 v3)
	{
		this.transform.position = v3;

		foreach(GameObject go in t_edit_brushs)
		{
			var edit_brushscrp = go.GetComponent<EditorBrush> ();
			edit_brushscrp.gameObject.transform.position = v3+Vector3.up*3;
		}
	}

	// Update is called once per frame
	void Update () {
		string keyR = "";
		string keyL = "";
		string keyF = "";
		string keyB = "";
		string keyU = "";
		string keyD = "";
		string keySpawn = "p";

		keyR = "d";
		keyL = "a";
		keyF = "w";
		keyB = "s";
		keyU = "[";
		keyD = "]";


		if (true) {//< BCRange) {
			//Input.GetKey(KeyCode.
			if (Input.GetKeyDown (keyR)) {
				this.transform.position += Vector3.right;
			}
			if (Input.GetKeyDown (keyL)) {
				this.transform.position -= Vector3.right;
			}
			if (Input.GetKeyDown (keyF)) {
				this.transform.position += Vector3.forward;
			}
			if (Input.GetKeyDown (keyB)) {
				this.transform.position -= Vector3.forward;
			}

			if (Input.GetKeyDown (keyU)) {
				this.transform.position += Vector3.up;
			}
			if (Input.GetKeyDown (keyD)) {
				this.transform.position -= Vector3.up;
			}


			if (Input.GetKeyDown (KeyCode.Space)) {
				Spawn ();
			}


		}
	}


	public void PlaceACube(int x,int y, int z){
		cellNew cellscrp =  g_scrpt.g_cellsTable [x] [y] [z];
		var posv3 = new Vector3 (x, y, z);
		if (cellscrp) {
			return;
		}

		startarray.Add (posv3);
		var cellnew = PutACube(posv3);//g_scrpt.PutACube (x, y, z);
		startcellarray.Add (cellnew);
	}


	public void RemoveACube(int x,int y, int z){
		cellNew cellscrp =  g_scrpt.g_cellsTable [x] [y] [z];
		var posv3 = new Vector3 (x, y, z);
		if (cellscrp) {
			foreach (Vector3 v3 in startarray) {
				if (v3 == posv3) {
					startarray.Remove (v3);
					break;
				}
			}

			foreach (cellNew cell in startcellarray) {
				if (cell.gameObject.transform.position == posv3) {
					startcellarray.Remove (cell);
					break;
				}
			}

			g_scrpt.RemoveCube(x, y, z);
		}
	}

	void OnClickClear(){
		if (g_scrpt.bStart)
			return;
		

		foreach(GameObject go in t_edit_brushs)
		{
			var edit_brushscrp = go.GetComponent<EditorBrush> ();
			edit_brushscrp.ClearListChoosed ();
		}

		foreach (Vector3 v3 in startarray) 
		{
			g_scrpt.RemoveCube((int)v3.x, (int)v3.y, (int)v3.z);
		}

		startcellarray.Clear ();
		startarray.Clear();
	}


	void Spawn()
	{
		int x = (int)(this.transform.position.x);
		int y = (int)(this.transform.position.y);
		int z = (int)(this.transform.position.z);
		cellNew cellscrp =  g_scrpt.g_cellsTable [x] [y] [z];
		var posv3 = new Vector3 (x, y, z);
		if (cellscrp) {
			RemoveACube (x, y, z);
		} else {
			PlaceACube (x, y, z);
		}
	}

	void WriteArray(string pathname)
	{
		string fullpath = g_savepath + pathname;
		testtest tt = new testtest ();
		tt.testarray = startarray;
		string json = JsonUtility.ToJson (tt);

		if (Directory.Exists(fullpath))
		{
			File.Delete(fullpath);
		}

		File.WriteAllText(fullpath, json);
	}


	public void OnStartGame()
	{
		
		WriteArray (sNowFileName);

		//还原
		foreach (cellNew cell in startcellarray) {
			cell.gameObject.transform.localScale = fOrignScale;
		}

		foreach(GameObject go in t_edit_brushs)
		{
			var edit_brushscrp = go.GetComponent<EditorBrush> ();
			edit_brushscrp.gameObject.GetComponent<Renderer> ().enabled = false;
		}

		this.GetComponent<Renderer> ().enabled = false;

	}

	public void OnResetDone()
	{

		startcellarray.Clear();
		InitCellArray ();

		this.GetComponent<Renderer> ().enabled = true;

		foreach(GameObject go in t_edit_brushs)
		{
			var edit_brushscrp = go.GetComponent<EditorBrush> ();
			edit_brushscrp.gameObject.GetComponent<Renderer> ().enabled = true;
		}
	}

	void InitCellArray() 
	{
		foreach (Vector3 v3 in startarray) 
		{
			var cellnew=  PutACube(v3);//g_scrpt.PutACube ((int)v3.x, (int)v3.y, (int)v3.z);
			startcellarray.Add (cellnew);
		}
	}

	Vector3 fOrignScale;
	public float editScale;
	cellNew PutACube(Vector3 v3)
	{
		var cellnew = g_scrpt.PutACube ((int)v3.x, (int)v3.y, (int)v3.z);
		if(fOrignScale ==Vector3.zero )fOrignScale = cellnew.gameObject.transform.localScale;
		cellnew.gameObject.transform.localScale = new Vector3(editScale,editScale,editScale);
		return cellnew; 
	}



	List<string> ReadFileList(string pathname)
	{ 

		if (!File.Exists(pathname))
		{
			return null;
		}

			StreamReader sr = new StreamReader(pathname);
		string json = sr.ReadToEnd();
		sr.Close();



		filelist read;
		if (json.Length > 0)
		{
			read = JsonUtility.FromJson<filelist>(json);
			List<string> xxarray = read.list;
			return xxarray;
		}

		return null;
	}


	List<Vector3> ReadArray(string jsonFileName)
	{ 
		

		if (!File.Exists(g_savepath + jsonFileName))
			        {
				            return null;
			        }

		StreamReader sr = new StreamReader(g_savepath + jsonFileName);
		string json = sr.ReadToEnd();
		sr.Close();



		testtest read;
		if (json.Length > 0)
		{
			read = JsonUtility.FromJson<testtest>(json);
			List<Vector3> xxarray = read.testarray;
			return xxarray;
		}

		return null;

	}

}
