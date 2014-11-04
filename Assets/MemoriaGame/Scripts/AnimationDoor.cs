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
    static float timeIvokingStar = 0.5f;
    void Awake(){
       // door = GetComponent<Door> ();

       

        door.OnOpenDoor += onOpen;
        door.onOpenQuickly += onOpenQuickly;
        door.onClose += onClose;

        door.onShakeTrue += onShakeTrue;
        door.onShakeFalse += onShakeFalse;
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
        Invoke ("InvokeStar",timeIvokingStar);
    }
    void onShakeTrue(){
        anim.SetBool("Shake",true);
    }
 
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

