using UnityEngine;
using System.Collections;

[RequireComponent (typeof (UILabel))]
public class ScoreGUI : MonoBehaviour {

    public string baseNameScore = "Score: ";
    protected UILabel label;
	// Use this for initialization
	void Awake  () {
        label = GetComponent<UILabel> ();
	}
	
    void LateUpdate(){

        label.text = baseNameScore + ManagerScore.Instance.CurrentScore.ToString ();
    }
}
