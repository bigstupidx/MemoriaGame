using UnityEngine;
using System.Collections;

public class AnimationPage : MonoBehaviour {

    public string spriteName;
    public UISprite sprite;
    public UISpriteAnimationLimit left;
    public UISpriteAnimationLimit right;


    public UISpriteAnimation Title;
	// Update is called once per frame
    public void ToLeft () {
        Title.gameObject.SetActive(false);


        left.PlayForward();

	}
    public void ToRight () {
        Title.gameObject.SetActive(false);

        right.PlayForward();


    }

    public void ShutDown(){
        gameObject.SetActive(false);

        sprite.spriteName = spriteName;
    }

    public void TurnOn(){
        sprite.spriteName = spriteName;
        gameObject.SetActive(true);

        Title.gameObject.SetActive(true);
        Title.enabled = true;
        Title.ResetToBeginning();
    }
}
