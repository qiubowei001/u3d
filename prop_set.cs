using UnityEngine;
using System.Collections;

public class prop_set : MonoBehaviour {
	public bool bIsSet;
	public GameObject Piece; 
	public GameObject prop; 
	// Use this for initialization
	void Start () {
		if (Piece) {
			var meshrender = Piece.GetComponent<SpriteRenderer> ();
			meshrender.enabled = false;
		}
		bIsSet = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ShowPiece () {
		if (bIsSet && Piece ) {
			var meshrender = Piece.GetComponent<SpriteRenderer> ();
			meshrender.enabled = true;
			var meshrender2 = prop.GetComponent<MeshRenderer> ();
			meshrender2.enabled = false;
		}
	}

	public void HidePiece () {
		if (Piece) {
			var meshrender = Piece.GetComponent<SpriteRenderer> ();
			meshrender.enabled = false;
			var meshrender2 = prop.GetComponent<MeshRenderer> ();
			meshrender2.enabled = false;
		}
	}

	public void AddPiece () {
		bIsSet = true;
	}

}
