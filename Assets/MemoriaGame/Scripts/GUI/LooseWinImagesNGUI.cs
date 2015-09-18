using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LooseWinImagesNGUI : MonoBehaviour
{

    public Image win;
    public Sprite[] Winners;
    public Image loser;
    public Sprite[] Loosers;
    // Use this for initialization
    void Awake ()
    {
        win.sprite = Winners [Random.Range (0, Winners.Length)];
        loser.sprite = Loosers [Random.Range (0, Loosers.Length)];
    }
}
