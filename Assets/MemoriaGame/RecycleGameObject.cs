//
//  RecycleGameObject.cs
//
//  Author:
//       Luis Alejandro Vieira <lavz24@gmail.com>
//
using UnityEngine;

public class RecycleGameObject : MonoBehaviour
{
    public GameObject obj;



    public void RecycleObject(){
    
        obj.Recycle();
    }
}


