//
//  ManagerCluesPower.cs
//
//  Author:
//       Luis Alejandro Vieira <lavz24@gmail.com>
//
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ManagerCluesPower: Singleton<ManagerCluesPower> {

    public float TimeOfPower = 3.0f;
    bool usedPower = false;
    public bool isPowerUsed { get { return usedPower; }}
    Door primera = null;
    Door segunda = null;
    Door tercera = null;

    void Awake(){

        ManagerPause.Instance.onGamePaused.Add (new Signal ("onPaused", gameObject));
        ManagerPause.Instance.onGameResumed.Add (new Signal ("onResume", gameObject));

    }

    public void ActivePower(){
        if (isPaused || usedPower || !ManagerDoors.Instance.isFirstOpen){
            return;
        }
        Debug.Log ("se llama aaqui");
        usedPower = true;

        FindPair ();


        if (primera != null)
            primera.ShakeTrue ();

        if (segunda != null)
            segunda.ShakeTrue ();

        if (tercera != null)
            tercera.ShakeTrue ();


        Invoke ("DeActivePower", TimeOfPower);

    }
    void DeActivePower(){



        if (primera != null)
            primera.ShakeFalse ();

        if (segunda != null)
            segunda.ShakeFalse ();

        if (tercera != null)
            tercera.ShakeFalse ();

    }

    #region Paused
    bool isPaused = false;
    [Signal]
    void onPaused(){
        isPaused = true;

    }
    [Signal]
    void onResume(){
        isPaused = false;

    }
    #endregion

    void FindPair(){

        List< List < Door> > allD = ManagerDoors.Instance.GetAllDoors;

        int z=0;
        int x=0;
        bool find = false;
        for (z = 0; z < allD.Count; ++z) {
        
            for ( x = 0; x < allD[z].Count; ++x) {
                if (allD [z] [x] != null
                    &&  !ManagerDoors.Instance.isFirstOpenEqual(allD [z] [x])) {
                
                    if(ManagerDoors.Instance.isPair(allD [z] [x])){
                        find = true;
                        primera = allD [z] [x];
                        break;
                    }
                }

            }
            if (find)
                break;
        }

        if (!find)
            return;
        //Aqui busco a los otros dos;
        #region Buscar PrimeraPista Izquirda y luego derecha
        bool findL = false;
        //Busco el primero haci ala izquierda
        for (int zL = z-1; zL >= 0; --zL) {

            for (int xL = x-1; xL>= 0; --xL) {
                if (allD [zL] [xL] != null
                    &&  !ManagerDoors.Instance.isFirstOpenEqual(allD [zL] [xL])) {

                    if(!ManagerDoors.Instance.isPair(allD [zL] [xL])){
                        findL = true;
                        segunda = allD [zL] [xL];
                        break;
                    }
                }
            }
            if (findL)
                break;
        }
        bool findLR = false;
        //Si no encontre en la izquierda voy a la derecha
        if (!findL) {
           
            //Aqui busco a los otros dos;
            for (int zL = z+1; zL < allD.Count; ++zL) {

                for (int xL = x+1; xL< allD[zL].Count; ++xL) {
                    if (allD [zL] [xL] != null
                        &&  !ManagerDoors.Instance.isFirstOpenEqual(allD [zL] [xL])) {

                        if(!ManagerDoors.Instance.isPair(allD [zL] [xL])){
                            findLR = true;
                            segunda = allD [zL] [xL];
                            break;
                        }
                    }
                }
                if (findLR)
                    break;
            }

            if (!findLR)
                return;
        }

        #endregion
        #region Buscar PrimeraPista Derecha y luego izquierda
        bool findR = false;
        //Busco el segundo a la derecha
        for (int zL = z+1; zL < allD.Count; ++zL) {

            for (int xL = x+1; xL< allD[zL].Count; ++xL) {
                if (allD [zL] [xL] != null
                    &&  !ManagerDoors.Instance.isFirstOpenEqual(allD [zL] [xL])
                    && segunda != allD [zL] [xL]  ) {

                    if(!ManagerDoors.Instance.isPair(allD [zL] [xL])){
                        findR = true;
                        tercera = allD [zL] [xL];
                        break;
                    }
                }
            }
            if (findR)
                break;
        }
        bool findRL = false;
        //Si no hay a la derecha lo busco haci ala izquierda
        if(!findR && findL){
            for (int zL = z-1; zL >= 0; --zL) {

                for (int xL = x-1; xL>= 0; --xL) {
                    if (allD [zL] [xL] != null
                        &&  !ManagerDoors.Instance.isFirstOpenEqual(allD [zL] [xL])
                        && segunda != allD [zL] [xL] ) {

                        if(!ManagerDoors.Instance.isPair(allD [zL] [xL])){
                            findRL = true;
                            tercera = allD [zL] [xL];
                            break;
                        }
                    }
                }
                if (findRL)
                    break;
            }
        }


        #endregion


    }
}

