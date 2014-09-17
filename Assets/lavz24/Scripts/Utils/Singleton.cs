//
// PersistanSingleton.cs
//
// Author:
//       Luis Alejandro Vieira <lavz24@gmail.com>
//
// Copyright (c) 2014 
//
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{

	private static T _instance;
	
	private static object _lock = new object();
	
	public static T Instance
	{
		get
        {
            if (_instance ==  null && applicationIsQuitting) {
                /*Debug.LogWarning("[Singleton] Instance '"+ typeof(T) +
                    "' already destroyed on application quit." +
                    " Won't create again - returning null.");*/
                return null;
            }

			lock(_lock)
			{
				if (_instance == null)
				{
					_instance = (T) FindObjectOfType(typeof(T));
					
					if ( FindObjectsOfType(typeof(T)).Length > 1 )
					{
						Debug.LogError("[Singleton] Something went really wrong " +
						               " - there should never be more than 1 singleton!" +
						               " Reopenning the scene might fix it.");
						return _instance;
					}
					
					if (_instance == null)
					{
						GameObject singleton = new GameObject();
						_instance = singleton.AddComponent<T>();
						singleton.name = "(singleton) "+ typeof(T).ToString();
                        						
						Debug.Log("[Singleton] An instance of " + typeof(T) + 
						          " is needed in the scene, so '" + singleton);
					} else {
						Debug.Log("[Singleton] Using instance already created: " +
						          _instance.gameObject.name);
					}
				}
				
				return _instance;
			}
		}
	}

    protected static bool applicationIsQuitting = false;
    /// <summary>
    /// When Unity quits, it destroys objects in a random order.
    /// In principle, a Singleton is only destroyed when application quits.
    /// If any script calls Instance after it have been destroyed, 
    ///   it will create a buggy ghost object that will stay on the Editor scene
    ///   even after stopping playing the Application. Really bad!
    /// So, this was made to be sure we're not creating that buggy ghost object.
    /// </summary>
    void OnDestroy () {

        applicationIsQuitting = true;
        OnDestroyChild ();
    }
    protected virtual void OnDestroyChild (){}

    /// <summary>
    /// Awake this instance
    /// </summary>
    void Awake(){
        applicationIsQuitting = false;
        AwakeChild ();

    }
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    protected virtual void AwakeChild (){}

}
