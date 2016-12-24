using System;
using UnityEngine;
using UnityEngine.UI;

public class PrefsManager : MonoBehaviour
{

    public Transform PrefBoxes;

	// Use this for initialization
	void Start () {

	    if (!PlayerPrefs.HasKey("Forward"))
	    {
	        //Initialize default playerprefs
	        PlayerPrefs.SetInt("Forward", (int) KeyCode.W);
	        PlayerPrefs.SetInt("Left", (int) KeyCode.A);
	        PlayerPrefs.SetInt("Back", (int) KeyCode.S);
	        PlayerPrefs.SetInt("Right", (int) KeyCode.D);
            PlayerPrefs.SetInt("Shoot", (int) KeyCode.Mouse0);
	        PlayerPrefs.SetInt("Pause", (int) KeyCode.P);
	        PlayerPrefs.SetString("Name", "");
	        PlayerPrefs.Save();
	    }
	}

    public static KeyCode getKeyCode(string key)
    {
        return (KeyCode) PlayerPrefs.GetInt(key);
    }

    public void SaveOptions()
    {

        Debug.Log(PlayerPrefs.GetString("Name"));
        Debug.Log("saving prefernces...");
        for (int i = 0; i < PrefBoxes.childCount; i++)
        {
            if (PrefBoxes.GetChild(i).GetChild(0).GetChild(0).childCount == 3)
            {
                Text text = PrefBoxes.GetChild(i).GetChild(0).GetChild(0).GetChild(2).GetComponent<Text>();
                Debug.Log("setting: " + text.name + " to " + text.text);
                if (text.name != "Name")
                {
                    PlayerPrefs.SetInt(text.name, (int) EnumUtils.ParseEnum<KeyCode>(text.text));
                }
                else
                {
                    PlayerPrefs.SetString(text.name, text.text);
                }
            }
        }
        Debug.Log(PlayerPrefs.GetString("Name"));
        PlayerPrefs.Save();
    }

    public class EnumUtils
    {
        public static T ParseEnum<T>(string value)
        {
            return (T) Enum.Parse(typeof(T), value, true);
        }
    }
}
