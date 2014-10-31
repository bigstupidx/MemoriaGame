//
//  ManagerPowers.cs
//
//  Author:
//       Luis Alejandro Vieira <lavz24@gmail.com>
//
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ManagerPowers : Singleton<ManagerPowers>
{

    bool usingPow = false;

    public bool UsingPower{
    
        get{
            return usingPow;
        }
        set{
            usingPow = value;
            if (value) {

                usingPowerIsTrue ();
            } else {
                usingPowerIsFalse ();
            }
        }
    }
        
    #region Signal list
    [HideInInspector]
    public List<Signal> onPowerTrue= new List<Signal>();
    [HideInInspector]
    public List<Signal> onPowerFalse = new List<Signal>();
    #endregion
    void usingPowerIsTrue(){
        foreach (Signal sig in onPowerTrue) {

            sig.Invoke ();
        }
    }
    void usingPowerIsFalse(){
        foreach (Signal sig in onPowerFalse) {

            sig.Invoke ();
        }
    }
}