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


    public UILabel timeNum;
    float TimeBySecondScore = 20.5f;
    bool sumTime = false;
    float currentTime = 0;
    static int countSecondTime = 50;

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
                    timeNum.text = currentTime.ToString ();
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

                    sum += (int)(currentTime*TimeBySecondScore);
                    CheckCoins ();
                    textNum.text = sum.ToString ();

                    timeNum.text = "00:00";

                    isCounting = false;
                    enabled = false;
                    return;
                }
                sum += (int)(aux*TimeBySecondScore) ;
                CheckCoins ();
                textNum.text = sum.ToString ();

              

                timeNum.text =  Mathf.Floor((currentTime / 60) % 60).ToString()+":"+ ( Mathf.Floor(currentTime%60.0f)).ToString();
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
        timeNum.text =  Mathf.Floor((ManagerTime.Instance.getCurrentTimeOfGame / 60) % 60).ToString()+":"+ ( Mathf.Floor(ManagerTime.Instance.getCurrentTimeOfGame%60.0f)).ToString();

        #if UNITY_IPHONE
        if(isIphone4){
            sum = ManagerScore.Instance.CurrentScore;

            sum+=  (int)(ManagerTime.Instance.getCurrentTimeOfGame * TimeBySecondScore);
            textNum.text = ManagerScore.Instance.CurrentScore.ToString();

            timeNum.text = "00:00";
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
