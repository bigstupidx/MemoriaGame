using UnityEngine;
using System.Collections;

public class AS_WinPair : MonoBehaviour {
    public AS_SpawnFX fx;
    public AudioClip clip;
    public AudioClip clipPlus;
    bool plusPoint = false;
    void Start(){
        ManagerDoors.Instance.OnCheckPair += onCheck;
        ManagerDoublePoints.Instance.OnActivePower += ActivatePlus;

    }

    void ActivatePlus(bool value){
        plusPoint = value;
    }
    void onCheck(bool value){
        if (value) {
            AS_SpawnFX fxAux = fx.Spawn (transform.position, transform.rotation);
            fxAux.fx = clip;

            if(plusPoint)
                Invoke ("SoundPlus", 0.1f);
        }
    }

    void SoundPlus(){
        AS_SpawnFX fxAux = fx.Spawn (transform.position, transform.rotation);
        fxAux.fx = clipPlus;
    }

}
