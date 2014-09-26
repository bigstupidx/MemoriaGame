using UnityEngine;
using System.Collections;

public class SetTweenForResolution : MonoBehaviour {


    public Transform StartPos;
    public Transform EndPos;

    public Sprite normal;
    public Sprite touch;

    public UI2DSprite widget;
	// Use this for initialization
	void Awake () {
        GetComponent<TweenPosition> ().from = StartPos.localPosition;
        GetComponent<TweenPosition> ().to = EndPos.localPosition;
    }
	

    public void ChangueSpriteToTouch(){
        widget.sprite2D = touch;
    }


    public void ChangueSpriteToNormal(){
        widget.sprite2D = normal;

    }
}
