using UnityEngine;
using UnityEngine.UI;

namespace Gamestrap
{
    public class MainMenuControl : MonoBehaviour
    {
        private static int _visibleVariable = Animator.StringToHash("Visible");
        private static int _notifyVariable = Animator.StringToHash("Notify");

        public GameObject SettingsPanel, AboutPanel, LeaderboardPanel;

        public Toggle SoundToggle, MusicToggle;

        public Text NotificationText;
        private Animator _notificationAnimator;
        public void Start()
        {
            //Adds events to the Toggle buttons through code since
            //doing it through the inspector wouldn't will give the value of the button dynamically
            SoundToggle.onValueChanged.AddListener(ToggleSound);
            MusicToggle.onValueChanged.AddListener(ToggleMusic);

            _notificationAnimator = NotificationText.GetComponent<Animator>();
        }

        #region Event Methods Called from the UI
        public void PlayClick()
        {
            GSAppExampleControl.Instance.LoadScene(ESceneNames.Arena);
        }

        public void AchievementsClick()
        {
            NotificationText.text = "Achievements Clicked...";
            _notificationAnimator.SetTrigger(_notifyVariable);
        }

        public void ToggleLeaderboard()
        {
            TogglePanel(LeaderboardPanel.GetComponent<Animator>());


        }

        public void RateClick()
        {
        }

        #region Settings Events
        public void ToggleSettingsPanel()
        {
            TogglePanel(SettingsPanel.GetComponent<Animator>());
        }

        public void ToggleSound(bool on)
        {
            // Change the sound
        }

        public void ToggleMusic(bool on)
        {
            // Change the music
        }

        #endregion

        #region About Events

        public void FacebookClick()
        {
            Application.OpenURL("https://www.facebook.com/gamestrapui/");
        }

        public void TwitterClick()
        {
            Application.OpenURL("https://twitter.com/EmeralDigEnt");

        }

        public void YoutubeClick()
        {
            Application.OpenURL("https://www.youtube.com/channel/UC8b_9eMveC6W0hl5RJkCvyQ");
        }

        public void WebsiteClick()
        {
            Application.OpenURL("http://www.gamestrap.info");
        }
        #endregion

        public void ToggleAboutPanel()
        {
            TogglePanel(AboutPanel.GetComponent<Animator>());
        }

        private void TogglePanel(Animator panelAnimator)
        {
            panelAnimator.SetBool(_visibleVariable, !panelAnimator.GetBool(_visibleVariable));
        }
        #endregion

        public void Quit()
        {
            Application.Quit();
        }
    }
}