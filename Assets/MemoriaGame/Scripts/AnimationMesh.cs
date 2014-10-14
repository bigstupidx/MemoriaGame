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

    const float timeShutDown = 0.45f;

    void Start(){
        // door = GetComponent<Door> ();


        door.OnOpenDoor += onOpen;
        door.onOpenQuickly += onOpenQuickly;

        door.onClose.Add (new Signal("onClose",gameObject));

        door.onCheckTruePair.Add (new Signal("onCheckTruePair",gameObject));

        gameObject.SetActive (false);
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
    [Signal]
    void onClose(){
        anim.SetBool("Open",false);
        anim.SetBool("OpenQuickly",false);

        StartCoroutine( ShutDown (timeShutDown));

    }

    [Signal]
    void onCheckTruePair(){
        //anim.SetBool("Open",false);
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



