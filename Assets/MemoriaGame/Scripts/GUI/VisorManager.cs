//
//  VisorManager.cs
//
//  Author:
//       Luis Alejandro Vieira <lavz24@gmail.com>
//
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VisorManager : Singleton<VisorManager> {

    public UI2DSprite FrontBG;
    public UI2DSprite Obj;

    public List<Sprite> meshes = new List<Sprite>();

    void Start(){
    
        ManagerDoors.Instance.OnOpenFirst += FirstOpenShow;
        ManagerDoors.Instance.OnCloseFirst += FirstOpenClose;

        ManagerDoors.Instance.OnCheckPair += CheckPair;

    }
    void FirstOpenShow(int id,string name){
        TweenAlpha.Begin (FrontBG.gameObject, 0.3f, 0);
        if (meshes.Count > 0) {
            Obj.sprite2D = meshes [id];
        }
    }

     void FirstOpenClose(int id, string name){
        TweenAlpha.Begin (FrontBG.gameObject, 0.3f, 1);

        if (meshes.Count > 0) {

            Obj.sprite2D = null;
        }
    }

    void CheckPair(bool value){
        if (value) {
        } else {
        }

    }

}
