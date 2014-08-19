using UnityEngine;
using System.Collections;

public class ManagerScore : Singleton<ManagerScore> {

    protected int score = 0;
    protected int highScore = 0;

    public int CurrentScore{ get { return score; } }

    public int ScoreBaseToSum = 10;

    void Awake(){
    
        highScore = PlayerPrefs.GetInt ("HighScore");
    }

    public void AddScore(int sum){
        if (sum <= 0)
            return;

        score += sum + ManagerCombo.Instance.GetCombo*sum;
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
