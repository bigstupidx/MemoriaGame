using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class GUI_GaleriaManager : MonoBehaviour {


    public UICenterOnChild father;
    public UITexture texturePrefab;
    public UIButton NextButton;

    const float offsetX = 550;
    int number = 0;

    ArrayList list;

    public GUI_CenterBgPlantilla backgroundSelected;
    public UIButton buttonBack;
	// Use this for initialization
	void Awake () {
        number = PlayerPrefs.GetInt("CountImages");
        list = new ArrayList();
        father.enabled = false;
	}
	
    public Texture2D getTextureOfCenter(){
    

        if (father.centeredObject != null)
        {
            return (Texture2D)father.centeredObject.GetComponent<UITexture>().mainTexture;
        }
        return null;

    }
    public void DeleteCurrentSelect(){
        if (father.centeredObject != null) {
            if (File.Exists (getPathFile ("TommyPlayground_" + father.centeredObject.name + ".png"))) {

                father.enabled = false;

                UITexture current = father.centeredObject.GetComponent<UITexture> ();
              //  Erase (current);
                System.IO.File.Delete (getPathFile ("TommyPlayground_" + father.centeredObject.name + ".png"));
              //  EventDelegate.Execute(buttonBack.onClick);
                father.CenterOn (null);
                current.gameObject.SetActive (false);
                father.enabled = true;
              //  EraseAll();
              //  EnableAll();
                //backgroundSelected.AddFromOnCenter ();

                //father.enabled = true;
            //    father.Recenter ();
                //  --number;
                //  PlayerPrefs.SetInt("CountImages",number);
            }
        }
    }

    public void EnableAll(){

        Vector3 off = Vector3.right * offsetX;
        for (int i = 0; i < number; ++i)
        {
            Texture2D texture = LoadInsideUnity("TommyPlayground_" + i + ".png");
            if (texture != null)
            {
                if(list == null)
                    list = new ArrayList();
                list.Add( texturePrefab.Spawn(father.transform,texturePrefab.transform.position+off));
                off += Vector3.right * offsetX;
                ((UITexture)list[list.Count - 1]).gameObject.name = i.ToString();
                ((UITexture)list[list.Count - 1]).mainTexture = texture;
                ((UITexture)list[list.Count - 1]).transform.localScale = new Vector3(1, 1, 1);
                TouchPlantilla tPlan = ((UITexture)list [list.Count - 1]).GetComponent<TouchPlantilla> ();
                tPlan.NextButton = NextButton;
                tPlan.Manager = father;

            }
        }
        backgroundSelected.AddFromOnCenter ();
        father.enabled = true;
        father.Recenter();
    }

    public void Erase(UITexture toErase){
        backgroundSelected.RemoveFromOnCenter ();

        for (int i = 0; i < list.Count; ++i)
        {
            if(toErase == ((UITexture)list[i])){

              //  DestroyImmediate(((UITexture)list[i]).mainTexture,true);
             //   ((UITexture)list[i]).Recycle();
            //    list.RemoveAt (i);
            }
           
        }

    }
    public void EraseAll(){
        backgroundSelected.RemoveFromOnCenter ();
        Texture2D texture = getTextureOfCenter();
        for (int i = 0; i < list.Count; ++i)
        {
            if(texture != ((Texture2D)((UITexture)list[i]).mainTexture)){
                DestroyImmediate(((UITexture)list[i]).mainTexture,true);
            }
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
