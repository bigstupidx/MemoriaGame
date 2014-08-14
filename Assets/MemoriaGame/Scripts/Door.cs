﻿//
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

class Door : MonoBehaviour
{
    public int IDpair = -1;
    public Vector2 posMaxtrix = new Vector2(-1,-1);

    [SerializeField]
    public List<Signal> onOpen = new List<Signal>();


    public void Touch(){

        ManagerDoors.Instance.TouchMe(this);
    }

    public void Open(){

        foreach (Signal sig in onOpen) {
        
            sig.Invoke ();
        }
    }
}

