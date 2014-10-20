using UnityEngine;
using System.Collections;

public class ManagerScore : Singleton<ManagerScore> {

    protected int score = 0;
    protected int highScore = 0;

    public int CurrentScore{ get { return score; } }

    public int ScoreBaseToSum = 50;

    [HideInInspector]
    protected int plusScore = 1;

    public void SetPlusScore(int plus){
    
        plusScore += plus;
        if (plusScore < 1)
            plusScore = 1;
    }
    protected override void AwakeChild(){
        highScore = PlayerPrefs.GetInt ("HighScore");
    }

    public void AddScore(int sum){
        if (sum <= 0)
            return;

        int value = sum * ( 1*plusScore +   ManagerCombo.Instance.GetCombo);

        score += value*plusScore;


    }
    public void AddScore(){

        AddScore (ScoreBaseToSum);
    }

    public void SaveHighScore(){
    
        if (score > highScore) {
            PlayerPrefs.SetInt ("HighScore", score);
            highScore = score;
        }

    }

}
