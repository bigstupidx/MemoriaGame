//
// ManagerPause.cs
//
// Modified:
//       Luis Alejandro Vieira <lavz24@gmail.com>
//
// Copyright (c) 2014 
//
using UnityEngine;
using System.Collections;

public class LoadLevelManager : PersistentSingleton<LoadLevelManager> {

    private AsyncOperation m_asop = null;

    private bool m_quitAfterCurrentLoad = false;


    #region HelperWithSceneLoading:
    public int SceneLoading = -1;

    object level = null;
    bool manualActivation = false;

    public void LoadLevelWithLoadingScene(object level, bool manualActivation = false)
    {
        this.level = level;
        this.manualActivation = manualActivation;
        //Aqui deberia cargar la escena de carga.
        LoadLevelImmediate (SceneLoading);
        //
    }
    #endregion
    public void RestartCurrentLevel()
    {
        LoadLevelImmediate (Application.loadedLevel);
    }

    /// <summary>
    /// Pass either the level name as a string or the level index
    /// </summary>
    /// <param name="level">Level.</param>
    /// <param name="manualActivation">If set to <c>true</c> manual activation.</param>
    public void LoadLevel(object level, bool manualActivation = false)
    {
        if (Application.isLoadingLevel) {
            Debug.LogError ("Call attempted to LoadLevel while a level is already in the process of loading; ignoring the load request...");
        } else {

            m_asop = _LoadLevelAsyncProxy (level);
            if (null != m_asop) {
                m_asop.allowSceneActivation = !manualActivation;
                Application.backgroundLoadingPriority = ThreadPriority.Low;
            }
        }
    }
    private void LoadLevelAsyncForLoadingScene()
    {
        LoadLevel (level, manualActivation);

    }
    public void LoadLevelImmediate(object level)
    {
        if (Application.isLoadingLevel) {
            Debug.LogError ("Call attempted to LoadLevel while a level is already in the process of loading; ignoring the load request...");
        } else {
            _LoadLevelImmediateProxy (level);
        }
    }
    /// <summary>
    /// This will immediately activate the currently loaded level if it hasn't been activated.
    /// If the level hasn't finished loading, it will be activated immediately after finishing.
    /// </summary>
    public void ActivateLoadedLevel()
    {

        if (null != m_asop) {
            m_asop.allowSceneActivation = true;
        } else {
            Debug.LogWarning ("SceneMgr::ActivateLoadedLevel was called, but there is no inactive scene to activate!");
        }
    }

    private static AsyncOperation _LoadLevelAsyncProxy(object level)
    {
        if (level.GetType () == typeof(int)) {
            return Application.LoadLevelAsync ((int)level);
        } else if (level.GetType () == typeof(string)) {
            return Application.LoadLevelAsync ((string)level);
        } else {
            Debug.LogError ("SceneMgr.LoadLevel was called with the wrong parameter type " + level.GetType () + "; must be int or string.");
        }
        return null;
    }

    private static void _LoadLevelImmediateProxy(object level)
    {
        if (level.GetType () == typeof(int)) {
            Application.LoadLevel ((int)level);
        } else if (level.GetType () == typeof(string)) {
            Application.LoadLevel ((string)level);
        } else {
            Debug.LogError ("SceneMgr.LoadLevelImmediate was called with the wrong parameter type " + level.GetType () + "; must be int or string.");
        }
    }

    #region Unity Callbacks:

    IEnumerator OnLevelWasLoaded(int level)
    {

        while (null != m_asop &&  !m_asop.allowSceneActivation)
        {
            // Wait until the just-loaded scene is allowed to start

            yield return null;
        }

       
        m_asop = null;

        if (m_quitAfterCurrentLoad)
        {
            Application.Quit();
        }


        if (level == SceneLoading) {
            StartCoroutine (CheckLoadingScene());

        }
    }
    IEnumerator CheckLoadingScene() {
        while (Application.isLoadingLevel) {
            yield return null;
        }
        LoadLevelAsyncForLoadingScene ();

    }
    void OnApplicationQuit()
    {
        if (null != m_asop) {
            m_quitAfterCurrentLoad = true;
            Application.CancelQuit ();
            Debug.Log ("OnApplicationQuit : CancelQuit! Attempting to quit while a scene is loading; quitting after scene load finishes. Called from: " + name);
        } else {
            Debug.Log ("OnApplicationQuit : Shutting down. Called from: " + name);
        }
    }


#endregion

}
