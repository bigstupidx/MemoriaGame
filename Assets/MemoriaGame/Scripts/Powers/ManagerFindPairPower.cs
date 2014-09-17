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

    void OnEnable(){
        ManagerPause.SubscribeOnPauseGame(onPaused);
        ManagerPause.SubscribeOnResumeGame( onResume);
    }
    void OnDisable(){

        ManagerPause.UnSubscribeOnPauseGame(onPaused);
        ManagerPause.UnSubscribeOnResumeGame(onResume);
    }

    public void ActivePower(){
        if (isPaused || usedPower){
            return;
        }
        ManagerPowers.Instance.UsingPower = true;

        FindPair ();
        ManagerDoors.Instance.CloseFirstOpen ();
    
        ManagerDoors.Instance.TouchMe(primera);
        Invoke ("DeActivePower", 0.1f);

    }
     void DeActivePower(){


        usedPower = true;
        ManagerDoors.Instance.TouchMe(segunda);

        ManagerPowers.Instance.UsingPower = false;
    }

    #region Paused
    bool isPaused = false;
    void onPaused(){
        isPaused = true;

    }
    void onResume(){
        isPaused = false;

    }
    #endregion

    void FindPair(){
    
        List< List < Door> > allD = ManagerDoors.Instance.GetAllDoors;

        int z= Random.Range(0,allD.Count);
        int x= Random.Range(0,allD[z].Count);

        bool find = false;
        for (int zP = z; zP < allD.Count; ++zP) {

            for (int  xP = x; xP < allD[zP].Count; ++xP) {
                if (allD [zP] [xP] != null ) {

                    primera = allD [zP] [xP];
                    find = true;
                    break;
                }

            }
            if (find)
                break;
        }

        if (!find) {
            for (int zP = z; zP >=0; --zP) {

                for (int  xP = x; xP >=0; --xP) {
                    if (allD [zP] [xP] != null ) {

                        primera = allD [zP] [xP];
                        find = true;
                        break;
                    }

                }
                if (find)
                    break;
            }
        }

        find = false;
        for (int zP = 0; zP < allD.Count; ++zP) {

            for ( int xP = 0; xP < allD[zP].Count; ++xP) {
                if (allD [zP] [xP] != null 
                    && primera != allD [zP] [xP]
                    && primera.IDpair == allD [zP] [xP].IDpair) {

                    segunda =allD [zP] [xP];
                    find = true;
                    break;
                }

            }
            if (find)
                break;
        }
        /*
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
        }*/
    }
}
