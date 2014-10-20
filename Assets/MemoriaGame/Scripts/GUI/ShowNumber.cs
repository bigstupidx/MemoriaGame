using UnityEngine;
using System.Collections;

public class ShowNumber : MonoBehaviour {
    static bool DebugMode = true;
    public Door door = null;
    MeshRenderer mesh;
    void Awake(){
        if (!DebugMode) {
            //DestroyImmediate (gameObject);
            return;
        }
        mesh = GetComponent<MeshRenderer> ();
        mesh.enabled = false;

        door.OnOpenDoor += onOpen;
        door.onOpenQuickly += onOpenQuickly;
        door.onCheckTruePair.Add (new Signal("onClose",gameObject));

        door.onClose += onClose;
    }
    void onOpen(){

        mesh.enabled = true;
    }
    void onOpenQuickly(){
        mesh.enabled = true;

    }

    void onClose(){
        mesh.enabled = false;


    }
}
