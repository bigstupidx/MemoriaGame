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

    [HideInInspector]
    public List<Signal> onClose = new List<Signal>();
    [HideInInspector]
    public List<Signal> onCheckTruePair = new List<Signal>();
    [HideInInspector]
    public List<Signal> onShakeTrue = new List<Signal>();
    [HideInInspector]
    public List<Signal> onShakeFalse = new List<Signal>();
    #endregion
   
    void Awake(){
    
        NameDoor = gameObject.name.Substring(0,gameObject.name.Length-7);
    }

    public void Touch(){

        ManagerDoors.Instance.TouchMe(this);
    }
    public void OpenQuickly(){

        onOpenQuickly ();
    }
    public void Open(){
        OnOpenDoor();
    }
    public void Close(){

        foreach (Signal sig in onClose) {

            sig.Invoke ();
        }
    }
    /// <summary>
    /// Si los dos pares  fueron iguales
    /// </summary>
    public void CheckTruePair(){
        isPairOpen = true;
        foreach (Signal sig in onCheckTruePair) {

            sig.Invoke ();
        }
    }
    public void ShakeTrue(){
        foreach (Signal sig in onShakeTrue) {

            sig.Invoke ();
        }
    }
    public void ShakeFalse(){
        foreach (Signal sig in onShakeFalse) {

            sig.Invoke ();
        }
    }
}

