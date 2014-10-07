using UnityEngine;
using System.Collections;

public class DoublePointsGUI : MonoBehaviour {

    public TweenAlpha _alpha;
	// Use this for initialization
	void Start () {
        ManagerDoublePoints.Instance.OnActivePower += OnActivePower;
	}
	
	// Update is called once per frame
    void OnActivePower (bool active) {
        if (active) {
            _alpha.PlayForward ();
        } else {
            _alpha.PlayReverse ();
        }
	
	}
}
