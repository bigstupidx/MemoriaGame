using UnityEngine;
using System.Collections;

public class TouchPlantilla : MonoBehaviour {

    public UIButton NextButton;
    public UIButton PlantillaButton;
    public UICenterOnChild Manager;
    void Start(){
        PlantillaButton.onClick.Add(new EventDelegate(this,"GoNextCurrent"));
        PlantillaButton.onClick.AddRange (NextButton.onClick);
    }

    public void GoNextCurrent(){
        Manager.CenterOn (transform);
    }
      
}
