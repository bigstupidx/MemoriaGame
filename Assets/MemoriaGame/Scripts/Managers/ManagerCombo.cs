using UnityEngine;
using System.Collections;

public class ManagerCombo : Singleton<ManagerCombo> {



    protected int timesInRow = 0;
    public int MaxTimesInRow = 4;

    public int GetCombo {
        get { 
            if (timesInRow < 1)
                return 0;
            return timesInRow;
        }
    }
	// Update is called once per frame
    public void setCombo (bool value) {
        if (value) {
            ++timesInRow;
            /*if (timesInRow > MaxTimesInRow)
                timesInRow = MaxTimesInRow;*/
        } else
            timesInRow = 0;
	}
}
