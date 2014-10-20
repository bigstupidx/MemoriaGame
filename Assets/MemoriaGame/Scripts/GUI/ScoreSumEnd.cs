using UnityEngine;
using System.Collections;

public class ScoreSumEnd : MonoBehaviour {

    public UILabel textNum;
    int sum = 0;
    bool isCounting = false;

    public UI2DSpriteAnimation[] coins;
    public int[] ScoreCoin;
    int currentCoin = 0;
    static int countSecond = 200;
    static float secondToChangue = 0.5f;

    bool changued = false;

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
            sum += (int)(countSecond*Time.deltaTime);
            ++sum;
            if (sum > ManagerScore.Instance.CurrentScore) {
                isCounting = false;
                sum = ManagerScore.Instance.CurrentScore;
                CheckCoins ();
                textNum.text = sum.ToString();
                enabled = false;
            } else {
               
                if (changued) {
                } else {
                    CheckCoins ();
                    textNum.text = sum.ToString();
                    changued = true;
                    StartCoroutine ("ChangueText", (secondToChangue));
                }
                //
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
            textNum.text = ManagerScore.Instance.CurrentScore.ToString();
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
