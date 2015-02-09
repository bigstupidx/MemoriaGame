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
    public AudioClip score01;
    public AudioClip score02;
    public AudioClip score03;

    public AudioSource audioSum;
    public AudioSource audioCoins;
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
                    SetTextNum();
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
                        SetTextNum();
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

                    SetTextNum();
                    tweenPos.enabled = false;
                    texteffect.alpha = 0;
                    scalesT.enabled = false;
                    clockWid.alpha = 0;
                    timer.alpha = 0;
                    scalesT = TweenScale.Begin (textNum.gameObject, 0.3f,new Vector3( 1.3f, 1.3f, 1.3f)) ;
                    scalesT.style = UITweener.Style.Once;

                    currentTime = 0;
                    isCounting = false;
                    audioSum.Stop ();
                    audioSum.clip = score03;
                    audioSum.loop = false;
                    audioSum.Play ();
                    Invoke ("DisableAudio",score03.length);
                    enabled = false;

                  
                } else {
                    sum += (int)(aux*ManagerScore.Instance.TimeBySecondScore) ;
                    CheckCoins ();
                    SetTextNum();
                }

              

            }
  
        }
	}
    IEnumerator ChangueText(float time){
    
        yield return Wait (time);
        changued = false;
    }
    void DisableAudio(){
        audioSum.enabled = false;
    }

    void PlayScoreMidle(){
        audioSum.Stop ();
        audioSum.clip = score02;
        audioSum.loop = true;
        audioSum.Play ();
    }
    public void StartCounting(){
        if (sum == 0) {
            enabled = true;
            isCounting = true;

            audioSum.volume = ManagerSound.Instance.fxVolume;
            audioSum.Stop ();
            audioSum.clip = score01;
            audioSum.loop = false;
            audioSum.Play ();
            Invoke ("PlayScoreMidle", score01.length);
            #if UNITY_IPHONE
            if (isIphone4) {
                sum = ManagerScore.Instance.CurrentScore;

                sum += (int)(ManagerTime.Instance.getCurrentTimeOfGame * ManagerScore.Instance.TimeBySecondScore);
            SetTextNum();
                scalesT = TweenScale.Begin (textNum.gameObject, 0.3f, new Vector3 (1.3f, 1.3f, 1.3f));
                scalesT.style = UITweener.Style.Once;

                isCounting = false;
                enabled = false;
                CheckCoins ();
                CheckCoins ();
                CheckCoins ();
                audioSum.Stop ();
                CancelInvoke ("PlayScoreMidle");
            }
            #endif
        }
    }
    void CheckCoins(){

        if (currentCoin <ScoreCoin.Length &&  sum >= ScoreCoin [currentCoin]) {
            audioCoins.Play ();
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

    void SetTextNum(){
     //   textNum.text = sum.ToString ();

        if (!isChanguin)
            StartCoroutine (UpdateText());
    }
    bool isChanguin = false;
    IEnumerator UpdateText(){
        isChanguin = true;
        yield return new WaitForSeconds (0.01f);
        isChanguin = false;
        textNum.text = sum.ToString ();

    }
}
