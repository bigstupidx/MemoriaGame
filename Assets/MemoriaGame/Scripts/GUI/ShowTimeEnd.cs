using UnityEngine;
using System.Collections;

public class ShowTimeEnd : MonoBehaviour {
    public bool isForWin = false;
    public TweenAlpha alphaT;
    public AudioClip Win;
    public AudioClip Lose;
    void OnEnable(){
        if (isForWin) {
            ManagerDoors.SubscribeOnVictory (StartAlpha);
            ManagerDoors.SubscribeOnVictory (Winer);


        } else {
            ManagerTime.SubscribeOnTimeGameEnd (StartAlpha);
            ManagerTime.SubscribeOnTimeGameEnd (Loser);

        }
    }
    void OnDisable(){
        if (isForWin) {
            ManagerDoors.UnSubscribeOnVictory (StartAlpha);
            ManagerDoors.UnSubscribeOnVictory (Winer);

        } else {
            ManagerTime.UnSubscribeOnTimeGameEnd (StartAlpha);
            ManagerTime.UnSubscribeOnTimeGameEnd (Loser);

        }

    }

    void StartAlpha(){
       
        alphaT.PlayForward();

    }
    void Winer(){
        GetComponent<AudioSource>().volume = ManagerSound.Instance.fxVolume;

        GetComponent<AudioSource>().clip = Win;
        GetComponent<AudioSource>().Play ();
    }
    void Loser(){
        Invoke ("PlayAhh",alphaT.delay+2.5f);

    }

    void PlayAhh(){

        GetComponent<AudioSource>().volume = ManagerSound.Instance.fxVolume;
        GetComponent<AudioSource>().clip = Lose;
        GetComponent<AudioSource>().Play ();
    }
}
