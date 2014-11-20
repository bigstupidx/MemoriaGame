using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class GUI_GaleriaManager : MonoBehaviour {


    public UICenterOnChild father;
    public UITexture texturePrefab;

    const float offsetX = 360;
    int number = 0;

    ArrayList list;

	// Use this for initialization
	void Awake () {
        number = PlayerPrefs.GetInt("CountImages");
        list = new ArrayList();
        father.enabled = false;
	}
	

    public void EnableAll(){
        Vector3 off = Vector3.right * offsetX;
        for (int i = 0; i < number; ++i)
        {
            Texture2D texture = LoadInsideUnity("TommyPlayground_" + i + ".png");
            if (texture != null)
            {
                list.Add( texturePrefab.Spawn(father.transform,texturePrefab.transform.position+off));
                off += Vector3.right * offsetX;
                ((UITexture)list[list.Count - 1]).gameObject.name = i.ToString();
                ((UITexture)list[list.Count - 1]).mainTexture = texture;
                ((UITexture)list[list.Count - 1]).transform.localScale = new Vector3(1, 1, 1);
            }
        }
        father.enabled = true;
    }


    public void EraseAll(){
    
        for (int i = 0; i < list.Count; ++i)
        {

            DestroyImmediate(((UITexture)list[i]).mainTexture,true);
            ((UITexture)list[i]).Recycle();
        }
        list.Clear();

    }
    string getPathFile(string filename){
        string path = "";
        if (Application.platform == RuntimePlatform.IPhonePlayer){
            path = Application.persistentDataPath.Substring( 0, Application.persistentDataPath.Length - 5 );
            path = path.Substring( 0, path.LastIndexOf( '/' ) );
            return Path.Combine( Path.Combine( path, "Documents" ), filename );
        } 
        else if(Application.platform == RuntimePlatform.Android)
        {
            path = Application.persistentDataPath;   
            path = path.Substring(0, path.LastIndexOf( '/' ) ); 
            return Path.Combine (path, filename);
        }   
        else 
        {
            path = Application.dataPath; 
            path = path.Substring(0, path.LastIndexOf( '/' ) );
            return  Path.Combine (path, filename);
        }
    }
    public void SaveInsideUnity(Texture2D texture){
        if (texture != null)
        {
            byte[] val = texture.EncodeToPNG();

            string filename = "TommyPlayground_" + number.ToString() + ".png";
            System.IO.File.WriteAllBytes(getPathFile(filename), val);
            ++number;
            PlayerPrefs.SetInt("CountImages",number);
        }

    }
    public Texture2D LoadInsideUnity(string filename){
  

        if (File.Exists(getPathFile(filename)))
        {
            Texture2D texture = new Texture2D(1, 1);
            byte[] val = File.ReadAllBytes(getPathFile(filename));
            texture.LoadImage(val);
            texture.hideFlags = HideFlags.DontSave;
            return texture;

           
        }
        return null;

    }
}
