using UnityEngine;
using UnityEngine.UI;

public class LoadPref : MonoBehaviour {

	// Use this for initialization
	void Start ()
	{
	    Debug.Log("loading pref for: " + transform.GetChild(1).name);
	    InputField inputField = GetComponent<InputField>();
	    Debug.Log("pref is: " + PlayerPrefs.GetString(transform.GetChild(1).transform.name));
	    string getStr = PlayerPrefs.GetString(transform.GetChild(1).transform.name);
	    int getInt = PlayerPrefs.GetInt(transform.GetChild(1).transform.name);
	    inputField.text = getStr == "" ? ((KeyCode)getInt).ToString() : getStr;
	}
}
