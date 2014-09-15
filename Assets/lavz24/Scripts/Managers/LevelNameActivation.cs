using UnityEngine;
using System.Collections;

public class LevelNameActivation : MonoBehaviour {

    public string LevelToLoad = "";
    public bool ManualActivation = false;


    public void LoadLevel(){
        LoadLevelManager.Instance.LoadLevelWithLoadingScene (LevelToLoad,ManualActivation);
    }


}
