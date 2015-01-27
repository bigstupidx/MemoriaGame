using UnityEngine;
using System.Collections;

public class GUI_CenterBgPlantilla : MonoBehaviour {

    public UICenterOnChild center;
	// Use this for initialization
	void Start () {
        center.onCenter += CenterObject;
	}
    public void RemoveFromOnCenter(){
       
        center.onCenter -= CenterObject;
        transform.parent = center.transform.parent;
    }
    public void AddFromOnCenter(){
        center.onCenter += CenterObject;
    }
    void CenterObject ( GameObject centerObj) {
        if (centerObj != null) {
            transform.parent = centerObj.transform;
            transform.localPosition = Vector3.zero;
        } else {
            transform.parent = center.transform.parent;
        }

	}

    public string SpriteNameCenter{
    
        get{
        
            return transform.parent.GetComponent<UISprite>().spriteName;
        }
    }
}
