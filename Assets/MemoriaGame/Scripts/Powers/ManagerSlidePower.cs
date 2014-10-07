using UnityEngine;
using System.Collections;

public class ManagerSlidePower : Singleton<ManagerSlidePower> {

    bool usedPower = false;
    SlidePower[] powers;
    public delegate void onActiveBroadcast(bool activate);
    public event onActiveBroadcast OnActivePower;

    void OnEnable(){
        ManagerPause.SubscribeOnPauseGame(onPaused);
        ManagerPause.SubscribeOnResumeGame( onResume);
    }
    void OnDisable(){

        ManagerPause.UnSubscribeOnPauseGame(onPaused);
        ManagerPause.UnSubscribeOnResumeGame(onResume);
    }

    void Start(){
    
        powers =  GameObject.FindObjectsOfType<SlidePower> ();
    }

    public void ActivePower(){
        if (isPaused || stopTime || usedPower)
            return;

        ManagerPowers.Instance.UsingPower = true;

        foreach (SlidePower power in powers) {
        
            if (power != null && power.gameObject.activeSelf) {
                power.activePower = true;

            }
        }
        //Si quiero q no pueda abrir ninguno activo aaqui q no se puede hacer touch
        ManagerDoors.Instance.CanTouch = false;

        //Mostras GUI del feedbackvisual
        if (OnActivePower != null)
            OnActivePower (true);
    }
    public bool DeActivePower(){
        if (isPaused || stopTime)
            return false;

        foreach (SlidePower power in powers) {

            if (power != null && power.gameObject.activeSelf) {
                power.activePower = false;

            }
        }
        //Si quiero q no pueda abrir ninguno activo aaqui q  se peude hacer touch
        ManagerDoors.Instance.CanTouch = true;
        usedPower = true;

        ManagerPowers.Instance.UsingPower = false;

        //apagar GUI de feedbackvisual 
        if (OnActivePower != null)
        OnActivePower (false);
        return true;
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

    #region StopTime: 
    /// <summary>
    /// Si el tiempo esta parado
    /// </summary>
    bool stopTime = false;
    /// <summary>
    /// Cuando se quiere parar el tiempo
    /// </summary>
    public void onStop(){
        stopTime = true;
    }
    /// <summary>
    /// Cuando se quiere reanudar el tiempo
    /// </summary>
    public void onPlay(){
        stopTime = false;
    }
    #endregion
}
