using UnityEngine;
using System.Collections;


public class PowerOffClue : PowerOff {


 

    void Update(){

        if (Locked == false) {

            if (ManagerDoors.Instance.isFirstOpen ) {
                if (ManagerCluesPower.Instance.isPowerUsed == false) {
                    if (!button.isEnabled && anotherUsing==false)
                        button.isEnabled = true;
                } else {
                    button.isEnabled = false;
             //       enabled = false;
                }
            } else {
            
                if(button.isEnabled)
                    button.isEnabled = false;

            }
        }
    }
  

}
