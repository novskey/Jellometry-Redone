using Assets.Scripts.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class HighScoreLoader : MonoBehaviour
    {

        public Transform Content;
        public GameObject Text;

        private HSController _hsController;

        void Start()
        {
            _hsController = GameObject.Find("HSController").GetComponent<HSController>();
        }

        public void LoadScores()
        {
            StartCoroutine(_hsController.ReturnScores());
        }

        public void DisplayScores(string[][] highScores)
        {
            ClearScores();

            foreach (string[] highScore in highScores)
            {
                string name = highScore[0];
                //Debug.Log(name);

                string score = highScore[1];
                string time = highScore[2].Substring(0,highScore[2].IndexOf(" "));

                GameObject nameText = (GameObject) Instantiate(Text, Content.FindChild("Name"));
                GameObject scoreText = (GameObject) Instantiate(Text, Content.FindChild("Score"));
                GameObject timeText = (GameObject) Instantiate(Text, Content.FindChild("Time"));

                nameText.GetComponent<Text>().text = name;
                scoreText.GetComponent<Text>().text = score;
                timeText.GetComponent<Text>().text = time;
            }
        }

        private void ClearScores()
        {
            for (int i = 0; i < Content.childCount; i++)
            {
                for (int j = 0; j < Content.GetChild(i).childCount; j++)
                {
                    Destroy(Content.GetChild(i).GetChild(j).gameObject);
                }

                Content.GetChild(i).DetachChildren();
            }
        }
    }
}
