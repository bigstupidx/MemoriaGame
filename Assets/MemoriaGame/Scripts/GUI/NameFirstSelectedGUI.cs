//
//  NameFirstSelectedGUI.cs
//
//  Author:
//       Luis Alejandro Vieira <lavz24@gmail.com>
//
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using SmartLocalization;

[RequireComponent (typeof(Text))]
public class NameFirstSelectedGUI : MonoBehaviour
{

    Text _label;

    public Text label {
    
        get {
            if (_label == null) {
                _label = GetComponent<Text> ();
            }
            return _label;
        }
    }

    void Start ()
    {
        label.text = "";
        ManagerDoors.Instance.OnOpenFirst += FirstOpenShow;
        ManagerDoors.Instance.OnCloseFirst += FirstOpenClose;
    }

    void FirstOpenShow (int id, string name)
    {
        label.text = LanguageManager.Instance.GetTextValue (name);
       
    }

    void FirstOpenClose (int id, string name)
    {
        label.text = "";
    }
}
