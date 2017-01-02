using UnityEngine;
using UnityEngine.UI;

namespace Assets
{
    public class LoadPref : MonoBehaviour {

        // Use this for initialization
        void Start ()
        {
            if (GetComponent<InputField>() != null)
            {
                Debug.Log("loading pref for: " + transform.GetChild(1).name);
                InputField inputField = GetComponent<InputField>();

                Debug.Log("pref is: " + PlayerPrefs.GetString(transform.GetChild(1).transform.name));

                string getStr = PlayerPrefs.GetString(transform.GetChild(1).transform.name);
                int getInt = PlayerPrefs.GetInt(transform.GetChild(1).transform.name);

                inputField.text = getStr == "" ? ((KeyCode)getInt).ToString() : getStr;
            }else if (GetComponent<Toggle>() != null)
            {
                Debug.Log("loading pref for: " + transform.name);
                Toggle toggle = GetComponent<Toggle>();

                Debug.Log("pref is: " + PlayerPrefs.GetString(transform.name));

                int getInt = PlayerPrefs.GetInt(transform.name);

                toggle.isOn = getInt == 1 ;
            }
        }
    }
}
