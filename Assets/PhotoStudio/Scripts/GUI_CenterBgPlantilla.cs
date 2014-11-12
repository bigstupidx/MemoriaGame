﻿using UnityEngine;
using System.Collections;

public class GUI_CenterBgPlantilla : MonoBehaviour {

    public UICenterOnChild center;
	// Use this for initialization
	void Start () {
        center.onCenter += CenterObject;
	}
	
    void CenterObject ( GameObject centerObj) {
        transform.parent = centerObj.transform;
        transform.localPosition = Vector3.zero;
	}

    public string SpriteNameCenter{
    
        get{
        
            return transform.parent.GetComponent<UISprite>().spriteName;
        }
    }
}
