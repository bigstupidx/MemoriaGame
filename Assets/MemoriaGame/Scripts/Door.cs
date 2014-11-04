//
// Door.cs
//
// Author:
//       Luis Alejandro Vieira <lavz24@gmail.com>
//
// Copyright (c) 2014 
//
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Door : MonoBehaviour
{
    public int IDpair = -1;
    public string NameDoor = "Hola";
    public PowerDoor character = PowerDoor.Any;


    public bool isPairOpen { get; private set;}

    [HideInInspector]
    public Vector2 posMaxtrix = new Vector2(-1,-1);

    #region Signal list

    public delegate void onOpenBroadcast();
    public event onOpenBroadcast OnOpenDoor;

    public delegate void onOpenQuicklyBroadcast();
    public event onOpenQuicklyBroadcast onOpenQuickly;

    public delegate void onCloseBroadcast();
    public event onCloseBroadcast onClose;

    [HideInInspector]
    public List<Signal> onCheckTruePair = new List<Signal>();

    public delegate void onShakeTrueBroadcast();
    public event onShakeTrueBroadcast onShakeTrue;
    [HideInInspector]
    public delegate void onShakeFalseBroadcast();
    public event onShakeFalseBroadcast onShakeFalse;
    #endregion
   
    void Awake(){
    
        NameDoor = gameObject.name.Substring(0,gameObject.name.Length-7);
    }

    public void Touch(){
        if (isPairOpen)
            return;
        ManagerDoors.Instance.TouchMe(this);
    }
    public void OpenQuickly(){
        if (isPairOpen)
            return;
        onOpenQuickly ();
    }
    public void Open(){
        if (isPairOpen)
            return;
        OnOpenDoor();
    }
    public void Close(){
        if (isPairOpen)
            return;
        onClose ();
    }
    /// <summary>
    /// Si los dos pares  fueron iguales
    /// </summary>
    public void CheckTruePair(){
        isPairOpen = true;
        GetComponent<InputDoor> ().NotInput();
        foreach (Signal sig in onCheckTruePair) {

            sig.Invoke ();
        }
    }
    public void ShakeTrue(){
        if (isPairOpen)
            return;
        onShakeTrue ();
    }
    public void ShakeFalse(){
        if (isPairOpen)
            return;
        onShakeFalse ();
    }
}

