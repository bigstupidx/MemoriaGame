using UnityEngine;
using System.Collections;

public class AS_Sound : MonoBehaviour {

    public Door door = null;
    public AS_SpawnFX fx = null;
    public AudioClip clip = null;
    public AudioClip clipClose = null;
    public AudioClip clipShake = null;
    AS_SpawnFX fxAuxShake;
    void Start(){
        if (door == null)
            door = GetComponent<Door> ();

        door.OnOpenDoor += onOpen;
        door.onClose += onClose;
        door.onShakeTrue += onShakeTrue;
        door.onShakeFalse += onShakeFalse;
        door.onOpenQuickly += onOpenQuickly;
    }
    void onOpen(){
        AS_SpawnFX fxAux =  fx.Spawn (transform.position, transform.rotation);
        fxAux.fx = clip;
    }
    void onOpenQuickly(){
        if (clipClose != null && clip != null) {
            AS_SpawnFX fxAux =  fx.Spawn (transform.position, transform.rotation);
            fxAux.fx = clip;
        }
    }
    void onClose(){
        if (clipClose != null) {
            AS_SpawnFX fxAux = fx.Spawn (transform.position, transform.rotation);
            fxAux.fx = clipClose;
        }
    }
    void onShakeTrue(){
        if (clipShake != null) {
            fxAuxShake = fx.Spawn (transform.position, transform.rotation);
            fxAuxShake.fx = clipShake;
            fxAuxShake.loop = true;


        }
    }
    void onShakeFalse(){
        if (clipShake != null) {
            if(fxAuxShake!= null)
                fxAuxShake.DestroySound ();
        }
    }
}
