using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public enum PowerDoor
{
    Any = 0,
    // Use this transition to represent a non-existing transition in your system
    Tati = 1,
    Tica = 2,
    Tico = 3,
    Timi = 4,
    Topi = 5,
}

public class PowerOff : MonoBehaviour
{

    public PowerDoor character = PowerDoor.Any;


    protected bool Locked = true;
    [HideInInspector]
    public bool used = false;

    public bool isEnabled {
    
        get {
        
            return !Locked && !used && button.interactable;
        }
        set {
            button.interactable = value;
        }
    }

    Button _button;

    public Button button {
    
        get {
        
            if (_button == null)
                _button = GetComponent<Button> ();
            return _button;
        }
    }

    Door[] doors;
    public AudioSource audio;

    bool firstRun = true;

    void Start ()
    {
        isEnabled = false;

        if (ManagerDoors.numberOfPair == NumberOfPair.CuatroXCuatro) {
            return;
        }

        doors = GameObject.FindObjectsOfType<Door> ();


        for (int i = 0; i < doors.Length; ++i) {
            if (character == doors [i].character) {
                doors [i].onCheckTruePair.Add (new Signal ("setOnPower", gameObject));

                break;
            }
        }

        ManagerPowers.Instance.onPowerTrue += onNotUse;
        ManagerPowers.Instance.onPowerFalse += onUse;

        ManagerPause.SubscribeOnPauseGame (onPaused);
        ManagerPause.SubscribeOnResumeGame (onResume);

        firstRun = false;
    }

    void OnEnable ()
    {
        if (!firstRun) {

            ManagerPause.SubscribeOnPauseGame (onPaused);
            ManagerPause.SubscribeOnResumeGame (onResume);
        }
    }

    void OnDisable ()
    {

        ManagerPause.UnSubscribeOnPauseGame (onPaused);
        ManagerPause.UnSubscribeOnResumeGame (onResume);
    }

    [Signal]
    public void setOnPower ()
    {
        Locked = false;
        if (anotherUsing == false) {
            isEnabled = true;

            audio.volume = ManagerSound.Instance.fxVolume;
            audio.Play ();
            
        }
    }

    public void setOffPower ()
    {

        if (!isPaused) {
            isEnabled = false;
            used = true;
        }


    }

    protected bool anotherUsing = false;

    #region Used

    [Signal]
    void onUse ()
    {
        if (!Locked && !used)
            isEnabled = true;

        anotherUsing = false;

    }

    [Signal]
    void onNotUse ()
    {
        isEnabled = false;
        anotherUsing = true;

    }

    #endregion

    #region Paused

    bool isPaused = false;

    void onPaused ()
    {
        isPaused = true;

    }

    void onResume ()
    {
        isPaused = false;

    }

    #endregion

}
