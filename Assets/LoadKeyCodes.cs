using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets
{
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
}
