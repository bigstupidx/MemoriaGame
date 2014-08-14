//
// AnimationDoor.cs
//
// Author:
//       Luis Alejandro Vieira <lavz24@gmail.com>
//
// Copyright (c) 2014 
//
using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Animator))]

class AnimationDoor : MonoBehaviour
{
    public Door door;
    Animator anim;
    void Awake(){
       // door = GetComponent<Door> ();

        anim = GetComponent<Animator> ();

        door.onOpen.Add (new Signal("onOpen",gameObject));
    }
    [Signal]
    void onOpen(){
        anim.SetBool("Open",true);
    }

}

