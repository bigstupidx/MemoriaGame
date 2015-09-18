//
//  ManagerPowers.cs
//
//  Author:
//       Luis Alejandro Vieira <lavz24@gmail.com>
//
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ManagerPowers : Singleton<ManagerPowers>
{

    bool usingPow = false;

    public bool UsingPower {
    
        get {
            return usingPow;
        }
        set {
            usingPow = value;
            if (value) {

                usingPowerIsTrue ();
            } else {
                usingPowerIsFalse ();
            }
        }
    }

    #region Signal list

    public Action onPowerTrue;
    public Action onPowerFalse;

    #endregion

    void usingPowerIsTrue ()
    {
        if (onPowerTrue != null)
            onPowerTrue ();
    }

    void usingPowerIsFalse ()
    {
        if (onPowerFalse != null)
            onPowerFalse ();
    }
}