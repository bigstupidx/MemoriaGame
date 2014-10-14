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
    public Door door = null;
    public Animator anim = null;
    void Awake(){
       // door = GetComponent<Door> ();

       

        door.OnOpenDoor += onOpen;
        door.onOpenQuickly += onOpenQuickly;

        door.onClose.Add (new Signal("onClose",gameObject));
        door.onShakeTrue.Add (new Signal("onShakeTrue",gameObject));
        door.onShakeFalse.Add (new Signal("onShakeFalse",gameObject));
        door.onCheckTruePair.Add (new Signal("onCheckTruePair",gameObject));

    }
    void onOpen(){
        anim.SetBool("Open",true);
        anim.SetBool("Shake",false);

    }
    void onOpenQuickly(){
        anim.SetBool("OpenQuickly",true);
        anim.SetBool("Shake",false);
    }

    [Signal]
    void onClose(){
        anim.SetBool("Open",false);
        anim.SetBool("OpenQuickly",false);

    }

    [Signal]
    void onCheckTruePair(){
        //anim.SetBool("Open",false);
        //TweenScale.Begin(door.gameObject,0.7f,new Vector3(0.01f,0.01f,0.01f)).AddOnFinished(new EventDelegate(this,"ReleaseDoor"));
        onClose ();
        InvokeStarPoof ();
        Invoke ("InvokeStar",0.5f);
    }
    [Signal]
    void onShakeTrue(){
        anim.SetBool("Shake",true);
    }
    [Signal]
    void onShakeFalse(){
        anim.SetBool("Shake",false);
    }

    public void ReleaseDoor(){
       // ManagerDoors.Instance.getStar ((int)door.posMaxtrix.x, (int)door.posMaxtrix.y).GetComponent<Animator> ().SetBool ("Changue", true);
       // door.Recycle ();
    }

    void InvokeStar(){
        ManagerDoors.Instance.getStar((int)door.posMaxtrix.x,(int)door.posMaxtrix.y).SetActive(true);

    }
    void InvokeStarPoof(){
        ManagerDoors.Instance.getStarPoof((int)door.posMaxtrix.x,(int)door.posMaxtrix.y).SetActive(true);

    }
}

