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



    public List<GameObject> meshes = new List<GameObject>();

    void Start(){
    
        ManagerDoors.Instance.OnOpenFirst += FirstOpenShow;
        ManagerDoors.Instance.OnCloseFirst += FirstOpenClose;
    }
    void FirstOpenShow(int id,string name){
    
        if (meshes.Count > 0)
            meshes [id].SetActive (true);
    }

     void FirstOpenClose(int id, string name){
        if (meshes.Count > 0)
            meshes [id].SetActive (false);
    }
}
