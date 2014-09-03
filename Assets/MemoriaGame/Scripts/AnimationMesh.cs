//
//  AnimationMesh.cs
//
//  Author:
//       Luis Alejandro Vieira <lavz24@gmail.com>
//
using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Animator))]

class AnimationMesh : MonoBehaviour
{
    public Door door = null;
    Animator anim = null;
    void Awake(){
        // door = GetComponent<Door> ();

        anim = GetComponent<Animator> ();

        door.onOpen.Add (new Signal("onOpen",gameObject));
        door.onOpenQuickly.Add (new Signal("onOpenQuickly",gameObject));

        door.onClose.Add (new Signal("onClose",gameObject));

        door.onCheckTruePair.Add (new Signal("onCheckTruePair",gameObject));

    }
    [Signal]
    void onOpen(){
        anim.SetBool("Open",true);
    }
    [Signal]
    void onOpenQuickly(){
        anim.SetBool("OpenQuickly",true);
    }
    [Signal]
    void onClose(){
        anim.SetBool("Open",false);
        anim.SetBool("OpenQuickly",false);

    }

    [Signal]
    void onCheckTruePair(){
        //anim.SetBool("Open",false);
    }
        
}



