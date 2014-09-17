using UnityEngine;
using System.Collections;

public class ManagerScore : Singleton<ManagerScore> {

    protected int score = 0;
    protected int highScore = 0;

    public int CurrentScore{ get { return score; } }

    public int ScoreBaseToSum = 10;

    [HideInInspector]
    protected int plusScore = 0;

    public void SetPlusScore(int plus){
    
        plusScore += plus;
    }
    protected override void Awake(){
        base.Awake ();
        highScore = PlayerPrefs.GetInt ("HighScore");
    }

    public void AddScore(int sum){
        if (sum <= 0)
            return;

        score += sum + ManagerCombo.Instance.GetCombo*sum + sum*plusScore;
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
