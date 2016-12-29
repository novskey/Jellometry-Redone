using UnityEngine;
using UnityEngine.UI;

namespace Assets.Resources.Gamestrap.UI.Helper_Scripts
{
    [RequireComponent(typeof(Button))]
    public class ButtonShowHide : MonoBehaviour {

        public bool StartShowGroup;
        public GameObject[] ShowHideGroup;

        private bool _show;

        void Start () {
            // This automatically registers the event click on the button component
            GetComponent<Button>().onClick.AddListener(() => { Click(); });
            _show = StartShowGroup;
            ShowHideUpdate();
        }

        public void Click()
        {
            _show = !_show;
            ShowHideUpdate();
        }

        private void ShowHideUpdate()
        {
            foreach (GameObject go in ShowHideGroup)
            {
                go.SetActive(_show);
            }
        }
    }
}
