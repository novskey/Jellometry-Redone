﻿using UnityEngine;
using UnityEngine.UI;

namespace Assets.Resources.Gamestrap.UI.Examples.Complete_Project.Global.Scripts
{
    /// <summary>
    /// Script registers itself to the Buttons event on click and then calls the GSApplExample to do the transition automatically
    /// </summary>
    [RequireComponent(typeof(Button))]
    public class SceneTransitionsButton : MonoBehaviour
    {

        public ESceneNames SceneName;

        void Start()
        {
            //Register a Unity action to the button component
            GetComponent<Button>().onClick.AddListener(ButtonClick);
        }

        void ButtonClick()
        {
            GSAppExampleControl.Instance.LoadScene(SceneName);
        }
    }
}