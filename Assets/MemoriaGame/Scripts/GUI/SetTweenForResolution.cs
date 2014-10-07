using UnityEngine;
using System.Collections;

public class SetTweenForResolution : MonoBehaviour {

    public TweenAlpha _alpha;
    public TweenPosition _position;

    public Transform StartPos;
    public Transform EndPos;

    public Sprite normal;
    public Sprite touch;

    public UI2DSprite widget;
    public float delay = 0;
    bool deActive = true;
    TimeCallBacks timer = new TimeCallBacks();
	// Use this for initialization
	void Awake () {
        _position.from = StartPos.localPosition;
        _position.to = EndPos.localPosition;
    }
	
    void Start(){
        ManagerSlidePower.Instance.OnActivePower += OnActivePower;

    }
    void OnActivePower (bool active) {


        if (active) {

            deActive = false;
            AddOnAlpha ();
            //_alpha.PlayForward ();
     
        } else {
            deActive = true;
            StopCoroutine ("PlayHand");
            //_position.enabled = false;
           // _alpha.PlayReverse ();
        }

    }
     void AddOnAlpha(){
        transform.localPosition = StartPos.localPosition;

        _alpha = TweenAlpha.Begin (gameObject, 0.6f, 1);
        _alpha.delay = delay;
        _alpha.AddOnFinished(Recall);
        _alpha.AddOnFinished (ChangueSpriteToTouch);
        _alpha.AddOnFinished (StartPositionTween);
    }
    IEnumerator PlayHand(float time){
        yield return StartCoroutine (timer.Wait (time));
        AddOnAlpha ();


    }
    public void Recall(){
        if (!deActive)
            StartCoroutine ("PlayHand", 8.0f);
    }
    void AddOnPostion(){
        _position.onFinished.Clear ();

        _position = TweenPosition.Begin (gameObject, 1, EndPos.localPosition);
        _position.from =  StartPos.localPosition;
        _position.AddOnFinished (ChangueSpriteToNormal);
        _position.AddOnFinished (EndAlpha);

    }
    public void StartPositionTween(){
        AddOnPostion ();

    }
    public void EndAlpha(){
        _alpha = TweenAlpha.Begin (gameObject, 0.6f, 0); 
        _alpha.delay = 0;
        _alpha.onFinished.Clear ();

    }
    public void ChangueSpriteToTouch(){
        widget.sprite2D = touch;
    }


    public void ChangueSpriteToNormal(){
        widget.sprite2D = normal;

    }
}
