using UnityEngine;
using System.Collections;

public class MergeTexture : MonoBehaviour {



    public static Texture2D Merge (Texture2D texToDraw ,Texture2D marco, int x, int y){


        int width = (marco.width);
        int height =(marco.height);

        if ( y + height> texToDraw.height) {
            y = texToDraw.height - (y+ height);

        }
        if (y < 0)
            y = 0;
        if ( x + width> texToDraw.width) {
            x = texToDraw.width - (x+ width);

        }
        if (x < 0)
            x = 0;

        Color[] pix = texToDraw.GetPixels(x, y, width, height);
        Color32[] pix1 = marco.GetPixels32();
        //Color32[] pix2 = texToDraw.GetPixels32();


        for (int j = 0; j < marco.height; j++){
            for (int i = 0; i < marco.width; i++) {
                if(pix1[j*marco.width + i].a==0  )
                    pix1[ j*marco.width + i] = pix[j*marco.width + i];
            }
        }
        Texture2D newphoto = new Texture2D(marco.width, marco.height);
        newphoto.SetPixels32(pix1);
        newphoto.Apply();
        return newphoto;
    }
}
