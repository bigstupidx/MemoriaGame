using UnityEngine;
using System.Collections;

public class ChanguerLevel : MonoBehaviour {


    public void ChangueToMemoria(){
        LoadLevelManager.Instance.LoadLevelImmediate ("HomeScene");
    }
    public void ChangueToPhoto(){
        LoadLevelManager.Instance.LoadLevelImmediate ("MenuPhotoStudio");
    }
    public void ChangueToIntro(){
        LoadLevelManager.Instance.LoadLevelImmediate ("Intro");
    }
    public void ChangueToCredits(){
        LoadLevelManager.Instance.LoadLevelImmediate ("Credits");
    }
}
