using UnityEngine;
using System.Collections;

public class GUI_PhotoMenu : MonoBehaviour {
    public UIWidget selectHome;
    public UIWidget selectPlantilla;
	
    protected UIWidget selectCurrent;

    public void SeleccionarPlantilla(){
        selectCurrent.alpha = 0;
        selectPlantilla.alpha = 1;
        selectCurrent = selectPlantilla;
    }

    public void SeleccionarHome(){
        selectCurrent.alpha = 0;
        selectHome.alpha = 1;
        selectCurrent = selectHome;
    }
}
