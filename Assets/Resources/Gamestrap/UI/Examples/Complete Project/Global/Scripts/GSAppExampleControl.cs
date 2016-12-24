using UnityEngine;
#if !(UNITY_4_6 || UNITY_5_0 || UNITY_5_1 || UNITY_5_2)
using UnityEngine.SceneManagement;
#endif
using System.Collections;

namespace Gamestrap
{
    /// <summary>
    /// Main Control that handles all of the global Application logic, specially the scene transitions
    /// </summary>
    public class GSAppExampleControl : MonoBehaviour
    {
        public static GSAppExampleControl Instance;
        private static int _visibleVariable = Animator.StringToHash("Visible");

        public Animator FaderAnimator;
        public AnimationClip FadingClip;

        void Awake()
        {
            if (Instance != null)
            {
                //ApplicationControl already exists
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public void LoadScene(ESceneNames sceneName)
        {
            Time.timeScale = 1;
            StartCoroutine(LoadSceneTransitions(sceneName));
        }

        private IEnumerator LoadSceneTransitions(ESceneNames sceneName)
        {
            FaderAnimator.SetBool(_visibleVariable, true);
            yield return new WaitForEndOfFrame();

            yield return new WaitForSeconds(FadingClip.length);
#if UNITY_4_6
//LoadLevelAsync wasn't in the free version of Unity in 4.6 so did this for compatibility reasons
        Application.LoadLevel(sceneName.ToString());
        while (Application.isLoadingLevel)
        {
            yield return new WaitForEndOfFrame();
        }
#elif UNITY_5_0 || UNITY_5_1 || UNITY_5_2
            yield return Application.LoadLevelAsync(sceneName.ToString());
#else
            yield return SceneManager.LoadSceneAsync(sceneName.ToString());
#endif
            FaderAnimator.SetBool(_visibleVariable, false);
        }
    }
}