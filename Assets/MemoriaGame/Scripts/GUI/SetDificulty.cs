//
//  SetDificulty.cs
//
//  Author:
//       Luis Alejandro Vieira <lavz24@gmail.com>
//
using UnityEngine;

public class SetDificulty : MonoBehaviour
{
        
    public string level = "GameScene";

    public void facil(){
        ManagerDoors.numberOfPair = NumberOfPair.CuatroXDos;
        LoadLevel ();
    }

    public void normal(){
        ManagerDoors.numberOfPair = NumberOfPair.CuatroXCuatro;
        LoadLevel ();
    }
    public void dificil(){
        ManagerDoors.numberOfPair = NumberOfPair.CincoXSeis;
        LoadLevel ();
    }

    void LoadLevel(){
        PlayerPrefs.Save ();
        LoadLevelManager.Instance.LoadLevelImmediate (level);
       

    }
}

