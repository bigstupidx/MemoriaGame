using UnityEngine;
using System.Collections;

public class ScoreSumEnd : MonoBehaviour {

    public UILabel textNum;
    int sum = 0;
    bool isCounting = false;

    public UI2DSpriteAnimation[] coins;
    public int[] ScoreCoin;
    int currentCoin = 0;
    static int countSecond = 300;
    static float secondToChangue = 0.5f;

    bool changued = false;


  
    bool sumTime = false;
    float currentTime = 0;
    static int countSecondTime = 50;
    public ClockTimer clock;
    public UIWidget clockWid;
    public UIWidget timer;
    public UIWidget texteffect;
    TweenPosition tweenPos;

    TweenScale scalesT;
    #if UNITY_IPHONE
    static bool isIphone4 = false;
    void Awake(){

        // door = GetComponent<Door> ();
        if( iPhone.generation == iPhoneGeneration.iPhone4)
        {
            isIphone4 = true;
            //Its an iPod Touch, third generation
        }

    }
    #endif
    void Start(){
        switch (ManagerDoors.numberOfPair) {

        case NumberOfPair.CincoXSeis:


            break;
        case NumberOfPair.CincoXSeisNormal:
            for (int i = 0; i < ScoreCoin.Length; ++i) {
                ScoreCoin [i] /= 3;
            }

            break;
        case NumberOfPair.CuatroXCuatro:
            for (int i = 0; i < ScoreCoin.Length; ++i) {
                ScoreCoin [i] /= 2;
            }
            break;
        }
    }
	// Update is called once per frame
	void Update () {
	
        if (isCounting) {
            if (!sumTime) {
                sum += (int)(countSecond * Time.deltaTime);
                ++sum;
                if (sum > ManagerScore.Instance.CurrentScore) {
                    //isCounting = false;
                    sum = ManagerScore.Instance.CurrentScore;
                    CheckCoins ();
                    textNum.text = sum.ToString ();
                    //enabled = false;
                    sumTime = true;
                    currentTime = ManagerTime.Instance.getCurrentTimeOfGame;

                    //Showtime

                    clock.transform.parent = transform;
                    clockWid.depth = 41;
                    timer.depth = 42;

                    scalesT = TweenScale.Begin (clock.gameObject, 0.3f,new Vector3( 1.3f, 1.3f, 1.3f)) ;
                    scalesT.style = UITweener.Style.PingPong;

                    clock.PlayFinalClock (  currentTime / countSecondTime);
                    texteffect.alpha = 1;

                    tweenPos = TweenPosition.Begin<TweenPosition> (texteffect.gameObject, 0.1f);
                    tweenPos.from = clockWid.transform.localPosition;
                    tweenPos.to = textNum.transform.localPosition;
                    tweenPos.style = UITweener.Style.Loop;
                } else {
               
                    if (changued) {
                    } else {
                        CheckCoins ();
                        textNum.text = sum.ToString ();
                        changued = true;
                        StartCoroutine ("ChangueText", (secondToChangue));
                    }
                    //
                }
            } else {
                float aux = (countSecondTime * Time.deltaTime);
                currentTime -= aux;

                if (currentTime <= 0) {
                
                    currentTime += aux;

                    sum += (int)(currentTime * ManagerScore.Instance.TimeBySecondScore);
                    CheckCoins ();

                    textNum.text = sum.ToString ();
                    tweenPos.enabled = false;
                    texteffect.alpha = 0;
                    scalesT.enabled = false;
                    clockWid.alpha = 0;
                    timer.alpha = 0;
                    scalesT = TweenScale.Begin (textNum.gameObject, 0.3f,new Vector3( 1.3f, 1.3f, 1.3f)) ;
                    scalesT.style = UITweener.Style.Once;

                    currentTime = 0;
                    isCounting = false;
                    enabled = false;
                } else {
                    sum += (int)(aux*ManagerScore.Instance.TimeBySecondScore) ;
                    CheckCoins ();
                    textNum.text = sum.ToString ();

                }

              

            }
  
        }
	}
    IEnumerator ChangueText(float time){
    
        yield return Wait (time);
        changued = false;
    }
    public void StartCounting(){
        enabled = true;
        isCounting = true;

        #if UNITY_IPHONE
        if(isIphone4){
            sum = ManagerScore.Instance.CurrentScore;

            sum+=  (int)(ManagerTime.Instance.getCurrentTimeOfGame * ManagerScore.Instance.TimeBySecondScore);
            textNum.text = sum.ToString();
            scalesT = TweenScale.Begin (textNum.gameObject, 0.3f,new Vector3( 1.3f, 1.3f, 1.3f)) ;
            scalesT.style = UITweener.Style.Once;

            isCounting = false;
            enabled = false;
            CheckCoins ();
            CheckCoins ();
            CheckCoins ();
        }
        #endif
    }
    void CheckCoins(){

        if (currentCoin <ScoreCoin.Length &&  sum >= ScoreCoin [currentCoin]) {
        
            coins [currentCoin].Play ();
            ++currentCoin;
        }
    }

    //Our wait function
    IEnumerator Wait(float duration)
    {
        for (float timer = 0; timer < duration; timer += Time.deltaTime)
            yield return null;
    }
}
