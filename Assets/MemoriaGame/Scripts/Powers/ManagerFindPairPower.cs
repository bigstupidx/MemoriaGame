//
//  ManagerFindPairPower.cs
//
//  Author:
//       Luis Alejandro Vieira <lavz24@gmail.com>
//
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ManagerFindPairPower : Singleton<ManagerStopTimePower> {

    bool usedPower = false;
    Door primera = null;
    Door segunda = null;

    void Awake(){

        ManagerPause.Instance.onGamePaused.Add (new Signal ("onPaused", gameObject));
        ManagerPause.Instance.onGameResumed.Add (new Signal ("onResume", gameObject));

    }

    public void ActivePower(){
        if (isPaused || usedPower){
            return;
        }
        FindPair ();
        ManagerDoors.Instance.CloseFirstOpen ();
    
        ManagerDoors.Instance.TouchMe(primera);
        Invoke ("DeActivePower", 0.1f);

    }
     void DeActivePower(){


        usedPower = true;
        ManagerDoors.Instance.TouchMe(segunda);

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

        foreach (List < Door> lD in allD) {
            foreach ( Door door in lD) {
                if (door != null) {
                    if (primera == null) {
                        //primera puerta
                        primera = door;
                    } else if (primera.IDpair == door.IDpair){
                        //segunda puerta
                        segunda =door;
                        return;
                    }
                }
            }
        }
    }
}
