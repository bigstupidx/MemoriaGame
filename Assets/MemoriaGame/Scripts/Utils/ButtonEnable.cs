//
//  ButtonEnable.cs
//
//  Author:
//       Luis Alejandro Vieira <lavz24@gmail.com>
//
using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof(Button))]
public class ButtonEnable : MonoBehaviour
{

    Button _button;

    public Button button {
        get {
            if (_button == null)
                _button = GetComponent<Button> ();
            return _button;
        }
    }

    void OnEnable ()
    {

        button.interactable = false;

    }

    void Start ()
    {
        ManagerTime.Instance.onTimeGameStart.Add (new Signal ("setOnPower", gameObject));
    }

    [Signal]
    public void setOnPower ()
    {
        button.interactable = true;
    }

    public void setOffPower ()
    {
        button.interactable = false;
    }
        

}

