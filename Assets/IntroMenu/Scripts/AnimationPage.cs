using UnityEngine;
using System.Collections;

public class AnimationPage : MonoBehaviour {

    public string spriteName;
    public UISprite sprite;
    public UISpriteAnimationLimit left;
    public UISpriteAnimationLimit right;

	// Update is called once per frame
    public void ToLeft () {


        left.PlayForward();

	}
    public void ToRight () {

        right.PlayForward();


    }

    public void ShutDown(){

        gameObject.SetActive(false);

        sprite.spriteName = spriteName;
    }

    public void TurnOn(){
        sprite.spriteName = spriteName;
        gameObject.SetActive(true);
    }
}
