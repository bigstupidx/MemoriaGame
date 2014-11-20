using UnityEngine;
using System.Collections;

public class SetPhotoMarco : MonoBehaviour {

    public UICenterOnChild Marco;
    public UI2DSprite TargetMarco;
	// Update is called once per frame
	public void setPhotoMarco () {
        TargetMarco.sprite2D = Marco.centeredObject.GetComponent<UI2DSprite>().sprite2D;
	}
}
