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
    public static float offSetZ = -0.20f;

    /// <summary>
    /// Tiempo para hacer el check de los pares
    /// Se utiliza para dar chance a que se abra la puerta
    /// </summary>
    public static float TimeToCheck = 0.4f;
    /// <summary>
    /// Si se esta corriendo el tiempo de checkeo.
    /// </summary>
    bool isChecking = false;

    [HideInInspector]
    public bool CanTouch = false;

    #region Starts:
    public GameObject Star;
    List< List < GameObject> > stars =  new List< List<GameObject>> ();
    public static float offSetXStar = 1.05f;
    public static float offSetZStart = -1.05f;
    #endregion

    #region ContadorDePares
    int currentPair = 0;

    bool CheckWinGame(){
        return (currentPair >= (int)numberOfPair);
    }
    #endregion

    void Awake(){
        setDoors();
        setStars ();

    }

    #region Seteado Aleatorio:
    void setStars(){
        switch (numberOfPair) {

        case NumberOfPair.CincoXSeis:
            setStartBy (5, 6,-2.65f,-5.9f);
            break;
        case NumberOfPair.CuatroXCuatro:
            setStartBy (4,4,-1.65f,-5.9f);
            break;
        case NumberOfPair.CuatroXDos:
            setStartBy (4, 2,-0.65f,-5.9f);
            break;

        }
    }
    void setStartBy(int countX,int countZ,float posIniX,float posIniZ){

        float currentX = posIniX;
        float currentZ = posIniZ;
        for (int i = 0; i < countX; ++i) {
            stars.Add (new List<GameObject> ());
            for (int j = 0; j < countZ; j++) {

                stars[i].Add (Star.Spawn(new Vector3(currentX,0,currentZ),Quaternion.identity));
                stars [i] [j].SetActive (false);
                currentX += offSetXStar;
            }

            currentZ += offSetZStart;
            currentX = posIniX;
        }
    }
    void setDoors(){
    
        switch (numberOfPair) {

        case NumberOfPair.CincoXSeis:
            setDoorBy (5, 6,-0.5f,-9.2f);
            break;
        case NumberOfPair.CuatroXCuatro:
            setDoorBy (4,4,-0.3f,-9.2f);
            break;
        case NumberOfPair.CuatroXDos:
            setDoorBy (4, 2,-0.1f,-9.2f);
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

    #region GamePlay CHecking:
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

        if (firstOpen.IDpair == SecondOpen.IDpair)//Si los dos pares son iguales
        {
            //LLamo a ManagerCombo
            ManagerCombo.Instance.setCombo (true);
            //Llamo a ManagerScore para
            ManagerScore.Instance.AddScore ();

            firstOpen.CheckTruePair ();
            SecondOpen.CheckTruePair ();


            //Aqui paro el tiempo si desbloquie todo.
            ++currentPair;
            if (CheckWinGame ()) {
                CanTouch = false;
                ManagerTime.Instance.onStop ();
                //Invoco GUI
            }
        } else {
            //LLamo a ManagerCombo
            ManagerCombo.Instance.setCombo (false);

            firstOpen.Close ();
            SecondOpen.Close ();

        }
        firstOpen = null;
        SecondOpen = null;
        isChecking = false;
		
    }
    #endregion

    #region Funciones de acceso:
    public GameObject getStar(int x, int z){
    
        return stars [x] [z];
    }
    #endregion
}

