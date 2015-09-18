//
//  VisorManager.cs
//
//  Author:
//       Luis Alejandro Vieira <lavz24@gmail.com>
//
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class VisorManager : Singleton<VisorManager>
{

    public Image FrontBG;
    public Image Obj;

    public List<Sprite> meshes = new List<Sprite> ();

    void Start ()
    {
    
        ManagerDoors.Instance.OnOpenFirst += FirstOpenShow;
        ManagerDoors.Instance.OnCloseFirst += FirstOpenClose;

        ManagerDoors.Instance.OnCheckPair += CheckPair;

    }

    void FirstOpenShow (int id, string name)
    {
        TweenAlpha_2.Begin (FrontBG.gameObject, 0.3f, 0);
        if (meshes.Count > 0) {
            Obj.enabled = true;
            Obj.sprite = meshes [id];
        }
    }

    void FirstOpenClose (int id, string name)
    {
        TweenAlpha_2.Begin (FrontBG.gameObject, 0.3f, 1);

        if (meshes.Count > 0) {
            Obj.enabled = false;
            Obj.sprite = null;
        }
    }

    void CheckPair (bool value)
    {
        if (value) {
        } else {
        }

    }

}
