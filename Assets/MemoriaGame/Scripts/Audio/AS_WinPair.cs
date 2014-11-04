using UnityEngine;
using System.Collections;

public class AS_WinPair : MonoBehaviour {
    public AS_SpawnFX fx;
    public AudioClip clip;
    void Start(){
        ManagerDoors.Instance.OnCheckPair += onCheck;

    }

    void onCheck(bool value){
        if (value) {
            AS_SpawnFX fxAux = fx.Spawn (transform.position, transform.rotation);
            fxAux.fx = clip;
        }
    }


}
