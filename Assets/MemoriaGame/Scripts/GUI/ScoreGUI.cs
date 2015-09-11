using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent (typeof(Text))]
public class ScoreGUI : MonoBehaviour
{

    public string baseNameScore = "Score: ";
    Text _label;

    public Text label {
    
        get {
            if (_label == null)
                _label = GetComponent<Text> ();
            return _label;
        }
    }

    void LateUpdate ()
    {

        label.text = baseNameScore + ManagerScore.Instance.CurrentScore.ToString ();
    }
}
