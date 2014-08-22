//
//  NameFirstSelectedGUI.cs
//
//  Author:
//       Luis Alejandro Vieira <lavz24@gmail.com>
//
using UnityEngine;
using System.Collections;

[RequireComponent (typeof (UILabel))]
public class NameFirstSelectedGUI : MonoBehaviour {

    protected UILabel label;
    // Use this for initialization
    void Awake  () {
        label = GetComponent<UILabel> ();
    }

    void LateUpdate(){

        label.text = ManagerDoors.Instance.getFisrtDoorName();
    }
}
