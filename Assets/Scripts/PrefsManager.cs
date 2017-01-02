using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
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

        public static KeyCode GetKeyCode(string key)
        {
            return (KeyCode) PlayerPrefs.GetInt(key);
        }

        public static string GetName()
        {
            return PlayerPrefs.GetString("Name");
        }

        public void SaveOptions()
        {

            //Debug.Log(PlayerPrefs.GetString("Name"));
            //Debug.Log("saving prefernces...");
            for (int i = 0; i < PrefBoxes.childCount; i++)
            {
                if (PrefBoxes.GetChild(i).GetChild(0).GetChild(0).childCount == 3)
                {
                    string text = PrefBoxes.GetChild(i).GetChild(0).GetChild(0).GetComponent<InputField>().text;
                    string name = PrefBoxes.GetChild(i).name;
                    //Debug.Log("setting: " + name + " to " + text);
                    if (name != "Name")
                    {
                        PlayerPrefs.SetInt(name, (int) EnumUtils.ParseEnum<KeyCode>(text));
                    }
                    else
                    {
                        PlayerPrefs.SetString(name, text);
                    }
                }
            }
            //Debug.Log(PlayerPrefs.GetString("Name"));
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
}
