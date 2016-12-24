using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using UnityEngine.UI;

public class LoadKeyCodes : MonoBehaviour {

	// Use this for initialization
	void Start ()
	{
	    Dropdown dropdown = GetComponent<Dropdown>();

	    string[] enumNames = Enum.GetNames(typeof(KeyCode));
	    List<string> names = new List<string>(enumNames);
	    dropdown.AddOptions(names);

	    transform.FindChild("Template").FindChild("Scrollbar").GetComponent<Scrollbar>().numberOfSteps = names.Count;
	}
}
