using UnityEngine;
using UnityEngine.UI;

namespace Assets.Resources.Gamestrap.UI.Helper_Scripts
{
    [RequireComponent(typeof(Image))]
    [RequireComponent(typeof(Toggle))]
    public class ToggleButtonIcon : MonoBehaviour {

        public Sprite OnIcon;
        public Sprite OffIcon;
        private Toggle _toggleButton;

        void Start()
        {
            // This automatically registers the event click on the button component
            _toggleButton = GetComponent<Toggle>();
            _toggleButton.onValueChanged.AddListener(Click);
            SetIcon();
        }

        public void Click(bool newValue)
        {
            SetIcon();
        }

        private void SetIcon()
        {
            if (_toggleButton.isOn)
            {
                GetComponent<Image>().sprite = OnIcon;
            }
            else
            {
                GetComponent<Image>().sprite = OffIcon;
            }
        }
    }
}
