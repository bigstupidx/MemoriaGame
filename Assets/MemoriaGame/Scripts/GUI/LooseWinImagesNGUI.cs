using UnityEngine;
using System.Collections;

public class LooseWinImagesNGUI : MonoBehaviour {

    public UI2DSprite win;
    public Sprite[] Winners;
    public UI2DSprite loser;
    public Sprite[] Loosers;
	// Use this for initialization
	void Awake () {
        win.sprite2D = Winners [Random.Range (0, Winners.Length)];
        loser.sprite2D = Loosers [Random.Range (0, Loosers.Length)];

	}
	
}
