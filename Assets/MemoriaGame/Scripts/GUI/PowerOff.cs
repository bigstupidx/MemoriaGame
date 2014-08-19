using UnityEngine;
using System.Collections;

public class PowerOff : MonoBehaviour {

    UIButton button;
    void Awake(){
    
        button = GetComponent<UIButton> ();
    }

    public void setOffPower(){
        button.isEnabled = false;
    }
}
