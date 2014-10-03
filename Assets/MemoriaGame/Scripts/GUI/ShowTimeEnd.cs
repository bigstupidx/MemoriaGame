using UnityEngine;
using System.Collections;

public class ShowTimeEnd : MonoBehaviour {
    public bool isForWin = false;
    public TweenAlpha alphaT;
    void OnEnable(){
        if (isForWin) {
            ManagerDoors.SubscribeOnVictory (StartAlpha);
        } else {
            ManagerTime.SubscribeOnTimeGameEnd (StartAlpha);
        }
    }
    void OnDisable(){
        if (isForWin) {
            ManagerDoors.UnSubscribeOnVictory (StartAlpha);

        } else {
            ManagerTime.UnSubscribeOnTimeGameEnd (StartAlpha);
        }

    }

    void StartAlpha(){
    
        alphaT.PlayForward();
    }
}
