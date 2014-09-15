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


    [HideInInspector]
    public List<Signal> onGamePaused = new List<Signal>();
    [HideInInspector]
    public List<Signal> onGameResumed = new List<Signal>();

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
	protected  void OnPauseGame(){
        foreach (Signal sig in onGamePaused) {

            sig.Invoke ();
        }
	}
	protected  void OnResumeGame(){
        foreach (Signal sig in onGameResumed) {

            sig.Invoke ();
        }
	}

	public  void PauseGame(){

		Pause = !Pause;
	}
	public  void PauseGame(bool value){
		
		Pause = value;
	}
}
