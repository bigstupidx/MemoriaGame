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
    public Door door;
    Animator anim;
    void Awake(){
        // door = GetComponent<Door> ();

        anim = GetComponent<Animator> ();

        door.onOpen.Add (new Signal("onOpen",gameObject));
        door.onClose.Add (new Signal("onClose",gameObject));

        door.onCheckTruePair.Add (new Signal("onCheckTruePair",gameObject));

    }
    [Signal]
    void onOpen(){
        anim.SetBool("Open",true);
    }
    [Signal]
    void onClose(){
        anim.SetBool("Open",false);
    }

    [Signal]
    void onCheckTruePair(){
        //anim.SetBool("Open",false);
    }
        
}



