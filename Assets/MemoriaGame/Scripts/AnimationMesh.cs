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

    #if UNITY_IPHONE
    static bool isIphone4 = false;
 
    void Awake(){
        // door = GetComponent<Door> ();
        if( iPhone.generation == iPhoneGeneration.iPhone4)
        {
            isIphone4 = true;
            //Its an iPod Touch, third generation
        }

    }
    #endif
    void Start(){

        #if UNITY_IPHONE
        if (isIphone4) {
            gameObject.SetActive (false);

        }
        #endif
        door.OnOpenDoor += onOpen;
        door.onOpenQuickly += onOpenQuickly;
        door.onClose += onClose;


        door.onCheckTruePair.Add (new Signal("onClose",gameObject));

       
    }
    void onOpen(){

        StopCoroutine (ShutDown(timeShutDown));
        #if UNITY_IPHONE

        if (isIphone4) {
            gameObject.SetActive (true);
        }
        #endif

        anim.SetBool("Open",true);
    }
    void onOpenQuickly(){
        StopCoroutine (ShutDown(timeShutDown));
        #if UNITY_IPHONE

        if (isIphone4) {
            gameObject.SetActive (true);
        }
        #endif
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
        #if UNITY_IPHONE

        if (isIphone4) {
            gameObject.SetActive (false);
        }
        #endif
    }
    //Our wait function
    IEnumerator Wait(float duration)
    {
        for (float timer = 0; timer < duration; timer += Time.deltaTime)
            yield return null;
    } 
}



