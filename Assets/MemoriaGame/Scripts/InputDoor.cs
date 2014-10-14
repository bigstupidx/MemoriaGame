//
// InputDoor.cs
//
// Author:
//       Luis Alejandro Vieira <lavz24@gmail.com>
//
// Copyright (c) 2014 
//
using UnityEngine;
using System.Collections;
using TouchScript.Gestures;
using System;

[RequireComponent (typeof(Door))]
[RequireComponent (typeof(TapGesture))]

public class InputDoor : MonoBehaviour
{
    Door door;

    void Awake(){
        GetComponent<TapGesture>().Tapped += TappedHandler;
        door = GetComponent<Door> ();
    }

    void TappedHandler(object sender, EventArgs e){

        door.Touch();
    }

    public void NotInput(){
        GetComponent<TapGesture>().Tapped -= TappedHandler;
    }

}

