﻿using Assets.Scripts;
using UnityEngine;

namespace Assets.Resources.Gamestrap.UI.Examples.Complete_Project.Screens.Gameplay
{
    public class GameplayUI : MonoBehaviour
    {

        public GameObject PausePanel;

        private static bool _pause;

//        private bool _canUnPause = true;

        /// <summary>
        /// It activates the pause animation in the pause panel
        /// </summary>
        public bool Pause
        {
            get { return _pause; }
            set
            {
                _pause = value;

                if (_pause)
                {
                    Time.timeScale = 0;
                    PausePanel.SetActive(true);
                    PausePanel.GetComponent<Animator>().SetBool("Visible", true);
                }
                else
                {
                    Time.timeScale = 1;
                    PausePanel.SetActive(false);
//                    PausePanel.GetComponent<Animator>().SetBool("Visible", false);
                }
            }
        }

        void Update()
        {
            if (Input.anyKey)
            {
                if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(PrefsManager.GetKeyCode("Pause")))
                {
                    //Debug.Log("game paused?: " + Pause);

                    Pause = !Pause;
                }
            }
        }
    }
}