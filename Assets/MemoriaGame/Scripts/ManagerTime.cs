//
// ManagerTime.cs
//
// Author:
//       Luis Alejandro Vieira <lavz24@gmail.com>
//
// Copyright (c) 2014 
//
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

class ManagerTime : Singleton<ManagerTime>
{
    public static float TimeToStart = 5.0f;
    float currentTimeToStart = 0;
    public float getCurrentTimeToStart { get { return currentTimeToStart; }}
    public float TimeOfGame = 120.0f;
    float currentTimeOfGame = 0;

    void Awake(){
        Invoke("setTimeToStart",0.1f);
    }
    void setTimeToStart(){
        currentTimeToStart = TimeToStart;

    }
    void Update(){

        if (currentTimeOfGame > 0) {
            currentTimeOfGame -= Time.deltaTime;
            if (currentTimeOfGame <= 0) {
               //Aqui hago lo q pasa cuando se pierde.

                ManagerDoors.Instance.CanTouch = false;

            }

        }else if (currentTimeToStart > 0) {
        
            currentTimeToStart -= Time.deltaTime;
            if (currentTimeToStart <= 0) {
                currentTimeOfGame = TimeOfGame;
                ManagerDoors.Instance.CanTouch = true;

            }
        }
    }

}

