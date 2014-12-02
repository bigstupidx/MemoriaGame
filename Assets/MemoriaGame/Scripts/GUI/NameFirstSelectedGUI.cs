//
//  NameFirstSelectedGUI.cs
//
//  Author:
//       Luis Alejandro Vieira <lavz24@gmail.com>
//
using UnityEngine;
using System.Collections;
using SmartLocalization;
[RequireComponent (typeof (UILabel))]
public class NameFirstSelectedGUI : MonoBehaviour {

    protected UILabel label;
    // Use this for initialization
    void Awake  () {
        label = GetComponent<UILabel> ();
    }
    void Start(){
        label.text = "";
        ManagerDoors.Instance.OnOpenFirst += FirstOpenShow;
        ManagerDoors.Instance.OnCloseFirst += FirstOpenClose;
    }

    void FirstOpenShow(int id, string name){
        label.text = LanguageManager.Instance.GetTextValue(name);
       
    }

    void FirstOpenClose(int id,  string name){

        label.text = "";
    }
}
