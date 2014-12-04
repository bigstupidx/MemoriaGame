//
//  AnimationMesh.cs
//
//  Author:
//       Luis Alejandro Vieira <lavz24@gmail.com>
//
using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Animator))]

public class AnimationMesh : MonoBehaviour
{
    public Door door = null;
    public Animator anim = null;

    static float timeShutDown = 0.35f;

    void Start(){


        gameObject.SetActive (false);
        door.OnOpenDoor += onOpen;
        door.onOpenQuickly += onOpenQuickly;
        door.onClose += onClose;


        door.onCheckTruePair.Add (new Signal("onClose",gameObject));

       
    }
    void onOpen(){

        StopCoroutine (ShutDown(timeShutDown));

        gameObject.SetActive (true);

        anim.SetBool("Open",true);
    }
    void onOpenQuickly(){
        StopCoroutine (ShutDown(timeShutDown));

        gameObject.SetActive (true);

        anim.SetBool("OpenQuickly",true);
    }
    void onClose(){
        StopCoroutine (ShutDown(timeShutDown));

        anim.SetBool("Open",false);
        anim.SetBool("OpenQuickly",false);

        StartCoroutine( ShutDown (timeShutDown));

    }
        
    IEnumerator ShutDown(float time)
    {
        yield return StartCoroutine(Wait(time));

        gameObject.SetActive (false);

    }
    //Our wait function
    IEnumerator Wait(float duration)
    {
        for (float timer = 0; timer < duration; timer += Time.deltaTime)
            yield return null;
    } 
}



