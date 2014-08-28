﻿//
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
        if (isPaused || usedPower || !ManagerDoors.Instance.isFirstOpen || ManagerPowers.UsingPower ){
            return;
        }
        ManagerPowers.UsingPower = true;

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

        ManagerPowers.UsingPower = false;

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

        int zP=0;
        int xP=0;
        bool find = false;
        for (zP = 0; zP < allD.Count; ++zP) {
        
            for ( xP = 0; xP < allD[zP].Count; ++xP) {
                if (allD [zP] [xP] != null
                    &&  !ManagerDoors.Instance.isFirstOpenEqual(allD [zP] [xP])) {
                
                    if(ManagerDoors.Instance.isPair(allD [zP] [xP])){
                        find = true;
                        primera = allD [zP] [xP];
                        break;
                    }
                }

            }
            if (find)
                break;
        }

        if (!find)
            return;

        int z = Random.Range (0,zP);
        int x = Random.Range (0,xP);
        //Aqui busco a los otros dos;
        #region Buscar PrimeraPista Izquirda y luego derecha
        bool findL = false;
        //Busco el primero haci ala izquierda
        for (int zL = z; zL >= 0; --zL) {

            for (int xL = x; xL>= 0; --xL) {
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
            for (int zL = z; zL < allD.Count; ++zL) {

                for (int xL = x; xL< allD[zL].Count; ++xL) {
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

        }

        #endregion

        if (zP + 1 < allD.Count) {
            z = Random.Range (zP+1, allD.Count);
        }
        if (xP + 1 < allD [zP].Count) {
            x = Random.Range (xP+1, allD.Count);
        }
      
        Debug.Log ("Aqui "+z+" "+x);
        #region Buscar PrimeraPista Derecha y luego izquierda
        bool findR = false;
        //Busco el segundo a la derecha
        for (int zL = z; zL < allD.Count; ++zL) {

            for (int xL = x; xL< allD[zL].Count; ++xL) {
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
        if(!findR ){
            for (int zL = z; zL >= 0; --zL) {

                for (int xL = x; xL>= 0; --xL) {
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

