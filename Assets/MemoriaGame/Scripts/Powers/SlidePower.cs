using UnityEngine;
using System.Collections;
using TouchScript.Gestures;
using System;

[RequireComponent (typeof(Door))]
public class SlidePower : MonoBehaviour {

    public FlickGesture horizontal;
    public FlickGesture vertical;


    Door door;

    [HideInInspector]
    public bool activePower = false;



    void Awake(){
        door = GetComponent<Door> ();

        horizontal.Flicked += FlickHorizontalHandler;
        vertical.Flicked += FlickVerticalHandler;

    }

    void FlickHorizontalHandler(object sender, EventArgs e){
        if (activePower &&  ManagerSlidePower.Instance.DeActivePower ()) {
            ManagerDoors.Instance.OpensDoors ((int)door.posMaxtrix.x, true);
           
        }
    }
    void FlickVerticalHandler(object sender, EventArgs e){
        if (activePower &&  ManagerSlidePower.Instance.DeActivePower ()) {
            ManagerDoors.Instance.OpensDoors ((int)door.posMaxtrix.y, false);

        }
    }
}
