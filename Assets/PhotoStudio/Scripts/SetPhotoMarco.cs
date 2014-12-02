using UnityEngine;
using System.Collections;

public class SetPhotoMarco : MonoBehaviour {

    public UICenterOnChild Marco;
    public UISprite TargetMarco;
	// Update is called once per frame
	public void setPhotoMarco () {
        TargetMarco.spriteName = Marco.centeredObject.GetComponent<UISprite>().spriteName;
	}
}
