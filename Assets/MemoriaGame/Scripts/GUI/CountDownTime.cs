//
// CountDownTime.cs
//
// Author:
//       Luis Alejandro Vieira <lavz24@gmail.com>
//
// Copyright (c) 2014 
//
using UnityEngine;
using System.Collections;
[RequireComponent (typeof(UILabel))]
public class CountDownTime : MonoBehaviour {

    UILabel label;
	// Use this for initialization
	void Awake () {
        label = GetComponent<UILabel> ();
	}
	
	// Update is called once per frame
	void LateUpdate () {
	
        label.text = ((int)ManagerTime.Instance.getCurrentTimeToStart).ToString();
        if (ManagerTime.Instance.getCurrentTimeToStart < 0) {
        
            gameObject.SetActive (false);
        }
	}
}
