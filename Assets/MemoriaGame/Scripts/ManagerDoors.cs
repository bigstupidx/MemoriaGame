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
    Vector2 firstOpen = new Vector2 (-1, -1);

    public static float offSetX = 0.20f;
    public static float offSetZ = 0.20f;

    void Awake(){
        setDoors();
    }

    void setDoors(){
    
        switch (numberOfPair) {

        case NumberOfPair.CincoXSeis:
            setDoorBy (5, 6,-0.5f,-10f);
            break;
        case NumberOfPair.CuatroXCuatro:
            setDoorBy (4,4,-0.6f,-10.5f);
            break;
        case NumberOfPair.CuatroXDos:
            setDoorBy (4, 2,-0.6f,-10.5f);
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
   
    void Start(){

    }

   public Door getDoor(Vector2 pos){
    
        return doors [(int)pos.x] [(int)pos.y];
    }

    public void TouchMe(Door door){

        if (firstOpen.x == -1) {
        
            firstOpen = door.posMaxtrix;
           
            door.Open ();
            //Aqui hago lo de la animacion de la puerta
        } else {
        
            //Hago check de que el par sea igual

            ///
            firstOpen = new Vector2 (-1, -1);
        }
    }
}

