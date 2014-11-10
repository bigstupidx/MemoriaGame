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
    CuatroXCuatro = 8,
    CincoXSeis = 15,
    CincoXSeisNormal = 14,
}


class ManagerDoors : Singleton<ManagerDoors>
{
    public static NumberOfPair numberOfPair = NumberOfPair.CincoXSeis;
  
    /// <summary>
    /// Contiene los prefabs de las diferentes puertas del juego. En total 15.
    /// </summary>
    public List<Door> prefabDoorsWithoutPair = new List<Door>();
    public GameObject Ground = null;
    List< List < Door> > doors =  new List< List<Door>> ();
    public List< List < Door> > GetAllDoors { get { return doors; }}

    Door firstOpen;
    Door SecondOpen;

    public static float offSetX = 0.15f;
    public static float offSetZ = -0.17f;

    /// <summary>
    /// Tiempo para hacer el check de los pares
    /// Se utiliza para dar chance a que se abra la puerta
    /// </summary>
    public static float TimeToCheck = 1.5f;
    /// <summary>
    /// Si se esta corriendo el tiempo de checkeo.
    /// </summary>
    bool isChecking = false;

    [HideInInspector]
    public bool CanTouch = false;

    public delegate void onCheckBroadcast(bool value);
    public event onCheckBroadcast OnCheckPair;

    public delegate void onOpenFirstBroadcast(int id, string name);
    public event onOpenFirstBroadcast OnOpenFirst;

    public delegate void onCloseFirstBroadcast(int id, string name);
    public event onCloseFirstBroadcast OnCloseFirst;
    #region Starts:
    public GameObject Star = null;
    List< List < GameObject> > stars =  new List< List<GameObject>> ();

    #endregion

    #region StartsPoof:
    public GameObject StarPoof = null;
    List< List < GameObject> > starsPoof =  new List< List<GameObject>> ();

    #endregion
    #region ContadorDePares
    int currentPair = 0;

    public bool CheckWinGame(){
        if (numberOfPair == NumberOfPair.CincoXSeisNormal)
            return currentPair >= (int)NumberOfPair.CincoXSeis;

        return (currentPair >= (int)numberOfPair);
    }

    public int GetCurrentPairs { get { return currentPair; }}

    public int GetMaxPairs { 
        get {
            if (numberOfPair == NumberOfPair.CincoXSeisNormal)
                return (int)NumberOfPair.CincoXSeis;

            return (int)numberOfPair; 
        }
    }

    #endregion

    /// <summary>
    /// Funciones para cuando el tiempo se acabe
    /// </summary>
    public delegate void onVictoryBroadcast();
    public event onVictoryBroadcast onVictory;

    /// <summary>
    /// Subscribes funct to OnPauseGame.
    /// </summary>
    /// <param name="funct">Funct.</param>
    public static void SubscribeOnVictory(onVictoryBroadcast funct){
        Instance.onVictory+=funct;
    }
    /// <summary>
    /// Unsubscribe safetely funct Function to OnPauseGameDelegate
    /// </summary>
    /// <param name="funct">Function.</param>
    public static void UnSubscribeOnVictory(onVictoryBroadcast funct){
        if(Instance != null)
            Instance.onVictory-=funct;
    }

    protected override void AwakeChild(){
        setObjects();


    }
    bool firstRun = true;
    void Start(){
        firstRun = false;
        ManagerPause.SubscribeOnPauseGame (onPaused);
        ManagerPause.SubscribeOnResumeGame (onResume);
    }
    void OnEnable(){
        if (!firstRun) {
            ManagerPause.SubscribeOnPauseGame (onPaused);
            ManagerPause.SubscribeOnResumeGame (onResume);
        }
    }
    void OnDisable(){

        ManagerPause.UnSubscribeOnPauseGame(onPaused);
        ManagerPause.UnSubscribeOnResumeGame(onResume);
    }
    #region Seteado Aleatorio (arreglar todas estas funciones):
    void setObjects(){
        switch (numberOfPair) {

        case NumberOfPair.CincoXSeis:
            setDoorBy (5, 6, -0.5f, -9.2f,0);
            setStartBy (5, 6, -0.5f, -9.2f,0.02f);
            setStartPoofBy (5, 6, -0.5f, -9.23f,0.09f);

            break;
        case NumberOfPair.CincoXSeisNormal:
            setDoorBy (5, 6, -0.5f, -9.2f,0);
            setStartBy (5, 6, -0.5f, -9.2f,0.02f);
            setStartPoofBy (5, 6, -0.5f, -9.23f,0.09f);

            break;
        case NumberOfPair.CuatroXCuatro:
            setDoorBy (4, 4, -0.37f, -9.39f,0.2f);
            setStartBy (4, 4, -0.37f, -9.39f,0.22f);
            setStartPoofBy (4, 4, -0.37f, -9.41f,0.29f);

            break;
        }
    }
    void setStartPoofBy(int countX,int countZ,float posIniX,float posIniZ, float posIniY){

        float currentX = posIniX;
        float currentZ = posIniZ;
        for (int i = 0; i < countX; ++i) {
            starsPoof.Add (new List<GameObject> ());
            for (int j = 0; j < countZ; j++) {

                starsPoof[i].Add (StarPoof.Spawn(new Vector3(currentX,posIniY ,currentZ),Quaternion.identity));
                starsPoof [i] [j].SetActive (false);
                currentX += offSetX;
            }

            currentZ += offSetZ +i*0.01f;;
            currentX = posIniX;
        }
    }

    void setStartBy(int countX,int countZ,float posIniX,float posIniZ, float posIniY){

        float currentX = posIniX;
        float currentZ = posIniZ;
        for (int i = 0; i < countX; ++i) {
            stars.Add (new List<GameObject> ());
            for (int j = 0; j < countZ; j++) {
                float aux = 0;
                if (i > 3) {
                    aux = i * 0.008f;
                }
                stars[i].Add (Star.Spawn(new Vector3(currentX + 0.012f, posIniY ,currentZ-(countX-i)*0.015f - aux),Quaternion.identity));
                stars [i] [j].SetActive (false);
                currentX += offSetX;
            }

            currentZ += offSetZ +i*0.01f;
            currentX = posIniX;
        }
    }

    void setDoorBy(int countX,int countZ,float posIniX,float posIniZ, float posIniY){
       

        #region Crear Lista de todas las puertas a usar: 
        List<Door> allDoors = new  List<Door> ();
        for (int i = 0; i < GetMaxPairs; ++i) {
        
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
                float aux = 0;
                if (i > 3) {
                    aux = i * 0.008f;
                }
                doors[i].Add (allDoors[posMatrix++].Spawn(new Vector3(currentX,posIniY,currentZ -(countX-i)*0.015f -aux),Quaternion.identity));
                GameObject g1 =  Ground.Spawn (Vector3.zero, Ground.transform.rotation);
                g1.transform.parent = doors [i] [j].transform;
                g1.transform.localPosition = Vector3.zero;
                g1.transform.parent = null;
                doors [i] [j].posMaxtrix = new Vector2 (i, j);
                currentX += offSetX;


            }

            currentZ += offSetZ +i*0.01f;
            currentX = posIniX;
        }

        allDoors.Clear ();
    }
    #endregion
   
    #region Paused
    bool isPaused = false;
    void onPaused(){
        isPaused = true;

    }
    void onResume(){
       
        Invoke ("setFalseIsPaused", 0.1f);
    }
    void setFalseIsPaused(){
        isPaused = false;
    }
    #endregion

    #region GamePlay CHecking:
    public void TouchMe(Door door){
        //Si no se esta esperando el tiempo de CheckPair
        if (!isChecking && CanTouch && !isPaused) {
            if (firstOpen == null) {
        
                firstOpen = door;
                door.Open ();
                //Aqui deberia poder poner un delegado para el visor
                OnOpenFirst (door.IDpair, door.NameDoor);
            } else if (firstOpen != door) {
        
                SecondOpen = door;
                door.Open ();

                isChecking = true;
                ManagerTime.Instance.onStop (false);
                Invoke ("CheckPairs", TimeToCheck);
            } else {
            
                //Debug.LogWarning ("Problema de logica aqui: "+);
            }
        }
    }

    /// <summary>
    /// Se revisa si las dos puertas abiertas son las mismas.
    /// </summary>
    void CheckPairs(){
        ManagerTime.Instance.onPlay (false);

        if (firstOpen.IDpair == SecondOpen.IDpair)//Si los dos pares son iguales
        {
            //LLamo a ManagerCombo
            ManagerCombo.Instance.setCombo (true);
            //Llamo a ManagerScore para
            ManagerScore.Instance.AddScore ();
            if(OnCheckPair != null)
                OnCheckPair (true);

            firstOpen.CheckTruePair ();
            SecondOpen.CheckTruePair ();
 

            doors [(int)firstOpen.posMaxtrix.x] [(int)firstOpen.posMaxtrix.y] = null;
            doors [(int)SecondOpen.posMaxtrix.x] [(int)SecondOpen.posMaxtrix.y] = null;

            //Aqui paro el tiempo si desbloquie todo.
            ++currentPair;
            if (CheckWinGame ()) {
                CanTouch = false;
                ManagerTime.Instance.onStop (false);
                //Invoco GUI
                onVictory ();
            }
        } else {
            //LLamo a ManagerCombo
            ManagerCombo.Instance.setCombo (false);
            if(OnCheckPair != null)
                OnCheckPair (false);

            firstOpen.Close ();
            SecondOpen.Close ();

        }
        //Aqui llamo a las funciones q dependen  del primer abierto
        OnCloseFirst (firstOpen.IDpair,firstOpen.NameDoor);
        firstOpen = null;
        SecondOpen = null;
        isChecking = false;
    }
    #endregion

    #region Accesing Door:
    int fila;
    int pos;
    int count;
    bool isH;
    bool isPosi;
    const float TimeToNextOpen = 0.2f;

    /// <summary>
    /// Abre las puertas que se encuentre en la fila y la direccion isH
    /// </summary>
    /// <param name="fila">Fila.</param>
    /// <param name="isH">If is Horizontal <c>true</c> is h.</param>
    public void OpensDoors(int fila, bool isH, bool isPosi){
        CanTouch = false;
        int countZ = 0;
        int countX = 0;
        switch (numberOfPair) {

        case NumberOfPair.CincoXSeis:
            countZ = 5;
            countX = 6;
            break;
        case NumberOfPair.CincoXSeisNormal:
                countZ = 5;
                countX = 6;
                break;
        case NumberOfPair.CuatroXCuatro:
            countZ = 4;
            countX = 4;
            break;

        }
        this.isH = isH;
        this.fila = fila;
        this.isPosi = isPosi;

      
        if (isH) {

            count = countX;


        } else {
            count = countZ;

        }

        if (isPosi) {
            pos = 0;
        } else {
            pos = count - 1;
        }
        if (audio != null) {
            audio.volume = ManagerSound.Instance.fxVolume;
            audio.Play ();
        }

        Invoke ("CloseDoors",TimeToNextOpen * count);

        repeatOpensDoors ();
    }

    /// <summary>
    /// Funcion que abre una puerta
    /// </summary>
    void repeatOpensDoors(){

        if (isH) {
            if (doors [fila] [pos] != null && doors [fila] [pos] != firstOpen) {

                doors [fila] [pos].OpenQuickly ();
            }
            if (isPosi) {
                pos++;
                if (pos < count) {

                    Invoke ("repeatOpensDoors", TimeToNextOpen);
                } 
            } else {
                pos--;
                if (pos >= 0) {

                    Invoke ("repeatOpensDoors", TimeToNextOpen);
                } 
            }
           
        } 
        else {
            if (doors [pos] [fila] != null && doors [pos] [fila] != firstOpen) {

                doors [pos] [fila].OpenQuickly ();
            }
            if (isPosi) {
                pos++;
                if (pos < count) {

                    Invoke ("repeatOpensDoors", TimeToNextOpen);
                } 
            } else {
                pos--;
                if (pos >= 0) {

                    Invoke ("repeatOpensDoors", TimeToNextOpen);
                } 
            }
           
        }
    }

    /// <summary>
    /// Cierra las puertas que se abrieron en Opensdoors
    /// </summary>
    void CloseDoors(){

        if (isPosi) {
            pos = 0;
        } else {
            pos = count - 1;
        }
        repeatCloseDoors ();

    }

    /// <summary>
    /// Funcion que cierra una puerta.
    /// </summary>
    void repeatCloseDoors(){

        if (isH) {
            if(doors [fila] [pos] != null && doors [fila] [pos] != firstOpen)
                doors [fila] [pos].Close ();

            if (isPosi) {
                pos++;
                if (pos < count) {

                    Invoke ("repeatCloseDoors", TimeToNextOpen);
                } else {

                    CanTouch = true;

                }
            } else {
                pos--;
                if (pos >=0) {

                    Invoke ("repeatCloseDoors", TimeToNextOpen);
                } else {

                    CanTouch = true;

                }
            }
           
        }
        else {
            if(doors [pos] [fila]!= null && doors [pos] [fila] != firstOpen)
                doors [pos] [fila].Close ();

            if (isPosi) {
                pos++;
                if (pos < count) {

                    Invoke ("repeatCloseDoors",TimeToNextOpen);
                }  else {

                    CanTouch = true;

                }
            } else {
                pos--;
                if (pos >=0) {

                    Invoke ("repeatCloseDoors",TimeToNextOpen);
                }  else {

                    CanTouch = true;

                }
            }
          
        }
    }

    #endregion

    #region Funciones de acceso:
    public GameObject getStar(int x, int z){

        return stars [x] [z];
    }
    public GameObject getStarPoof(int x, int z){

        return starsPoof[x] [z];
    }
    public string getFisrtDoorName(){

        if (firstOpen == null)
            return "";

        return firstOpen.NameDoor;
    }

    public int getFisrtDoorID(){

        if (firstOpen == null)
            return -1;

        return firstOpen.IDpair;
    }

    public bool isFirstOpen{
    

        get{ return firstOpen != null; }
    }
    public void CloseFirstOpen(){


        if (isFirstOpen) {
        
            firstOpen.Close ();
            OnCloseFirst (firstOpen.IDpair,firstOpen.NameDoor);
            firstOpen = null;
            isChecking = false;
        }

    }
    public bool isFirstOpenEqual(Door door){


        if (isFirstOpen) {
        
            return door == firstOpen;
        }

        return false;
    }
    public bool isPair(Door door){

        if(!isFirstOpenEqual(door)){
        
            return door.IDpair == firstOpen.IDpair;
        }

        return false;
    }
    #endregion
}

