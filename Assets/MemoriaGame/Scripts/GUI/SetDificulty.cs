//
//  SetDificulty.cs
//
//  Author:
//       Luis Alejandro Vieira <lavz24@gmail.com>
//
using UnityEngine;
using System.Collections;

public class SetDificulty : MonoBehaviour
{
        
    public string level = "GameScene";
    public float timeForChangue = 1.5f;

    public GameObject test;


    public void facil(){
        ManagerDoors.numberOfPair = NumberOfPair.CuatroXCuatro;
        //LoadLevel ();
        test.SetActive (true);
        StartCoroutine (LoadLevelAfter(timeForChangue));
    }

    public void normal(){
        ManagerDoors.numberOfPair = NumberOfPair.CincoXSeisNormal;
        //LoadLevel ();
        test.SetActive (true);

        StartCoroutine (LoadLevelAfter(timeForChangue));

    }
    public void dificil(){
        ManagerDoors.numberOfPair = NumberOfPair.CincoXSeis;
        //LoadLevel ();
        test.SetActive (true);

        StartCoroutine (LoadLevelAfter(timeForChangue));

    }

    void LoadLevel(){
        PlayerPrefs.Save ();
        LoadLevelManager.Instance.LoadLevelImmediate (level);
       

    }
    public void GoToIntro(){
        LoadLevelManager.Instance.LoadLevelImmediate ("Intro");

    }
    //Our wait function //Se usa este porque pdes cambiar el el time
    IEnumerator Wait(float duration)
    {
        for (float timer = 0; timer < duration; timer += Time.deltaTime)
            yield return null;
    }

    IEnumerator LoadLevelAfter(float waitTime) {

        yield return StartCoroutine( Wait(waitTime));
        LoadLevel ();
    }
}

