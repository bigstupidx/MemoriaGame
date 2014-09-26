using UnityEngine;
using System.Collections;

public class SetTweenForResolution : MonoBehaviour {


    public Transform StartPos;
    public Transform EndPos;
	// Use this for initialization
	void Awake () {
        GetComponent<TweenPosition> ().from = StartPos.localPosition;
        GetComponent<TweenPosition> ().to = EndPos.localPosition;
    }
	
}
