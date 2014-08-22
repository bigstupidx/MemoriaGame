//
//  PairsGUI.cs
//
//  Author:
//       Luis Alejandro Vieira <lavz24@gmail.com>
//
using UnityEngine;
using System.Collections;

[RequireComponent (typeof (UILabel))]
public class PairsGUI : MonoBehaviour {

    public string baseNameScore = " / ";
    protected UILabel label;
    // Use this for initialization
    void Awake  () {
        label = GetComponent<UILabel> ();
    }

    void LateUpdate(){

        label.text = ManagerDoors.Instance.GetCurrentPairs+ baseNameScore + ManagerDoors.Instance.GetMaxPairs;
    }
}

