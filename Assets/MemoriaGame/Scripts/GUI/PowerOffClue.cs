using UnityEngine;
using System.Collections;


public class PowerOffClue : PowerOff
{


 

    void Update ()
    {

        if (Locked == false) {

            if (ManagerDoors.Instance.isFirstOpen) {
                if (ManagerCluesPower.Instance.isPowerUsed == false) {
                    if (!isEnabled && anotherUsing == false)
                        isEnabled = true;
                } else {
                    isEnabled = false;
                    //       enabled = false;
                }
            } else {
            
                if (isEnabled)
                    isEnabled = false;

            }
        }
    }
  

}
