using UnityEngine;
using System.Collections;

public class SetMountainTween : MonoBehaviour {

    public UIWidget widget;
    public TweenPosition tween;
    public Vector3 offset = new Vector3(0,1,0);

	// Use this for initialization
	void Start () {
        tween.to = widget.transform.localPosition;
        tween.from = widget.transform.localPosition + new Vector3(offset.x * widget.transform.localPosition.x,
            offset.y * widget.transform.localPosition.y,offset.z * widget.transform.localPosition.z);

        SetPosFrom();
	}
	
    public void SetPosFrom(){
        widget.transform.localPosition = tween.from;

    }
}
