//
// ManagerDoors.cs
//
// Author:
//       Luis Alejandro Vieira <lavz24@gmail.com>
//
// Copyright (c) 2014 
//
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;

public enum NumberOfPair
{
    CuatroXDos = 4, 
    CuatroXCuatro = 8,
    CincoXSeis = 15,
}


class ManagerDoors : Singleton<ManagerDoors>
{
    public NumberOfPair numberOfPair = NumberOfPair.CincoXSeis;
  
    /// <summary>
    /// Contiene los prefabs de las diferentes puertas del juego. En total 15.
    /// </summary>
    public List<Door> prefabDoorsWithoutPair;

    List< List < Door> > doors =  new List< List<Door>> ();
    Door firstOpen;
    Door SecondOpen;

    public static float offSetX = 0.20f;
    public static float offSetZ = 0.20f;

    /// <summary>
    /// Tiempo para hacer el check de los pares
    /// Se utiliza para dar chance a que se abra la puerta
    /// </summary>
    public static float TimeToCheck = 0.4f;
    /// <summary>
    /// Si se esta corriendo el tiempo de checkeo.
    /// </summary>
    bool isChecking = false;


    public bool CanTouch = false;
    void Awake(){
        setDoors();
    }

    #region Seteado Aleatorio:
    void setDoors(){
    
        switch (numberOfPair) {

        case NumberOfPair.CincoXSeis:
            setDoorBy (5, 6,-0.5f,-10f);
            break;
        case NumberOfPair.CuatroXCuatro:
            setDoorBy (4,4,-0.3f,-10f);
            break;
        case NumberOfPair.CuatroXDos:
            setDoorBy (4, 2,-0.1f,-10f);
            break;

        }
    }
    void setDoorBy(int countX,int countZ,float posIniX,float posIniZ){
       

        #region Crear Lista de todas las puertas a usar: 
        List<Door> allDoors = new  List<Door> ();
        for (int i = 0; i < (int)numberOfPair; ++i) {
        
            allDoors.Add (prefabDoorsWithoutPair[i]);
            allDoors.Add (prefabDoorsWithoutPair[i]);

        }
        allDoors.Shuffle ();
        int posMatrix = 0;
        #endregion

   
        float currentX = posIniX;
        float currentZ = posIniZ;
        for (int i = 0; i < countX; ++i) {
            doors.Add (new List<Door> ());

            for (int j = 0; j < countZ; j++) {

                doors[i].Add (allDoors[posMatrix++].Spawn(new Vector3(currentX,0,currentZ),Quaternion.identity));
                doors [i] [j].posMaxtrix = new Vector2 (i, j);
                currentX += offSetX;
            }

            currentZ += offSetZ;
            currentX = posIniX;
        }

        allDoors.Clear ();
    }
   #endregion

    public void TouchMe(Door door){
        //Si no se esta esperando el tiempo de CheckPair
        if (!isChecking && CanTouch) {
            if (firstOpen == null) {
        
                firstOpen = door;
                door.Open ();

            } else if(firstOpen != door) {
        
                SecondOpen = door;
                door.Open ();

                isChecking = true;
                Invoke ("CheckPairs", TimeToCheck);
            }
        }
    }

    /// <summary>
    /// Se revisa si las dos puertas abiertas son las mismas.
    /// </summary>
    void CheckPairs(){
        //Hago check de que el par sea igual
        if (firstOpen.IDpair == SecondOpen.IDpair) {

            firstOpen.CheckTruePair ();
            SecondOpen.CheckTruePair ();
        } else {
            firstOpen.Close ();
            SecondOpen.Close ();

        }
        firstOpen = null;
        SecondOpen = null;
        isChecking = false;
    }
}

