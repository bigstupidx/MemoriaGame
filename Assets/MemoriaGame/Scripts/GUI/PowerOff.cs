using UnityEngine;
using System.Collections;

public class PowerOff : MonoBehaviour {

    UIButton button;
    void Awake(){
    
        button = GetComponent<UIButton> ();
        button.isEnabled = false;
        ManagerTime.Instance.onTimeGameStart.Add (new Signal ("setOnPower", gameObject));

        ManagerPause.Instance.onGamePaused.Add (new Signal ("onPaused", gameObject));
        ManagerPause.Instance.onGameResumed.Add (new Signal ("onResume", gameObject));
    }
    [Signal]
    public void setOnPower(){
        button.isEnabled = true;
    }
    public void setOffPower(){

        if(!isPaused)
            button.isEnabled = false;
    }

    #region Paused
    bool isPaused = false;
    [Signal]
    void onPaused(){
        isPaused = true;

    }
    [Signal]
    void onResume(){
        isPaused = false;

    }
    #endregion

}
