using UnityEngine;
using System.Collections;

[RequireComponent (typeof (UILabel))]

public class ComboGUI : MonoBehaviour {

    public string baseNameScore = "Combo \n X";
    protected UILabel label;
    // Use this for initialization
    void Awake  () {
        label = GetComponent<UILabel> ();
    }

    void LateUpdate(){

        label.text = baseNameScore + ManagerCombo.Instance.GetCombo.ToString ();
    }
}
