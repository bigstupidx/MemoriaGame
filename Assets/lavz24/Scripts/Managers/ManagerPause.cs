//
// ManagerPause.cs
//
// Author:
//       Luis Alejandro Vieira <lavz24@gmail.com>
//
// Copyright (c) 2014 
//
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ManagerPause : Singleton<ManagerPause> {


    public delegate void onPauseGameBroadcast();
    public event onPauseGameBroadcast OnPauseGame;


    public delegate void onResumeGameBroadcast();
    public event onResumeGameBroadcast OnResumeGame;
  /*  [HideInInspector]
    public List<Signal> onGamePaused = new List<Signal>();*/
 /*   [HideInInspector]
    public List<Signal> onGameResumed = new List<Signal>();
    */


	private  bool paused = false;
	public  bool Pause {
	
		get{
			return paused;
		}
		set{
			paused = value;
			if(paused){
                OnPauseGame();
			}else{
                OnResumeGame();
			}
		}
	}

}
