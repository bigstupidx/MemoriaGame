using UnityEngine;
using System.Collections;

public class AS_Sound : MonoBehaviour {

    public Door door = null;
    public AS_SpawnFX fx = null;
    public AudioClip clip = null;
    void Start(){


        door.OnOpenDoor += onOpen;



    }
    void onOpen(){
        AS_SpawnFX fxAux =  fx.Spawn (transform.position, transform.rotation);
        fxAux.fx = clip;
    }
}
