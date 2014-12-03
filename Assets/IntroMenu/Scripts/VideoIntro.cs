using UnityEngine;
using System.Collections;

public class VideoIntro : MonoBehaviour {

    public UISpriteAnimationLimit anim01;
    public UISpriteAnimationLimit anim02;

    public static bool startIntro = true;
    bool firstRun = true;

	// Use this for initialization
	void Update () {
        if (firstRun) {
//            Debug.Log (startIntro);
            if (startIntro) {

                anim01.PlayForward ();

                startIntro = false;
            } else {

                anim02.FinishedCall ();

                anim01.gameObject.SetActive (false);
            }
            firstRun = false;
        }
    }

    public void ChangueAtlas(){
        anim01.gameObject.SetActive (false);
        anim02.gameObject.SetActive (true);
        anim02.PlayForward ();
    }

	
}
