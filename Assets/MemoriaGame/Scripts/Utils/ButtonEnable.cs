//
//  ButtonEnable.cs
//
//  Author:
//       Luis Alejandro Vieira <lavz24@gmail.com>
//
using System;
using UnityEngine;

public class ButtonEnable : MonoBehaviour {

    UIButton button;


    void Awake(){

        button = GetComponent<UIButton> ();
        button.isEnabled = false;

    }
    void Start(){
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

