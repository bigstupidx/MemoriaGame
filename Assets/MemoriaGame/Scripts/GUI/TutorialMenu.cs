using UnityEngine;
using System.Collections;
using SmartLocalization;

public class TutorialMenu : MonoBehaviour {

    public UI2DSprite iconPower;
    public UILabel Descripcion;

    public Sprite[] powers;
    public string[] textTutorial;

    protected int currentPos = 0;

	// Use this for initialization
	void Start () {
        for (int i = 0; i < textTutorial.Length; ++i) {

            textTutorial[i] = LanguageManager.Instance.GetTextValue(textTutorial[i]);
        }
        SetCurrent ();
	}
	
	// Update is called once per frame
	public void Left () {
        --currentPos;
        if (currentPos < 0)
            currentPos = powers.Length - 1;

        SetCurrent ();
	}

    public void Right(){
        ++currentPos;
        if (currentPos >= powers.Length)
            currentPos = 0;
        SetCurrent ();
    }

    void SetCurrent(){
        Descripcion.text = textTutorial [currentPos];
        iconPower.sprite2D = powers [currentPos];
    }
}
