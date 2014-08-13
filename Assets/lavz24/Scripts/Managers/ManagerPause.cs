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

public class ManagerPause : Singleton<ManagerPause> {

	private static bool paused = false;
	public static bool Pause {
	
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
	protected static void OnPauseGame(){
		Object[] objects = FindObjectsOfType (typeof(GameObject));
		foreach (GameObject go in objects) {
			go.SendMessage ("OnPauseGame", SendMessageOptions.DontRequireReceiver);
		}
	}
	protected static void OnResumeGame(){
		Object[] objects = FindObjectsOfType (typeof(GameObject));
		foreach (GameObject go in objects) {
			go.SendMessage ("OnResumeGame", SendMessageOptions.DontRequireReceiver);
		}
	}

	public static void PauseGame(){

		Pause = !Pause;
	}
	public static void PauseGame(bool value){
		
		Pause = value;
	}
}
