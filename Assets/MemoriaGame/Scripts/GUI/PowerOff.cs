using UnityEngine;
using System.Collections;

public class PowerOff : MonoBehaviour {

    UIButton button;
    void Awake(){
    
        button = GetComponent<UIButton> ();
        button.isEnabled = false;
        ManagerTime.Instance.onTimeGameStart.Add (new Signal ("setOnPower", gameObject));
    }
    [Signal]
    public void setOnPower(){
        button.isEnabled = true;
    }
    public void setOffPower(){
        button.isEnabled = false;
    }
}
