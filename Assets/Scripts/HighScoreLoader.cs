using UnityEngine;
using Assets.Scripts.Managers;
using UnityEngine.UI;

public class HighScoreLoader : MonoBehaviour
{

    public Transform Content;
    public GameObject Text;

    public void LoadScores()
    {
        ClearScores();

        StartCoroutine(DBInterface.Connect());
        foreach (string[] highScore in DBInterface.GetHighScores())
        {
            string name = highScore[0];
            string score = highScore[1];
            string time = highScore[2];

            GameObject nameText = (GameObject) Instantiate(Text, Content.FindChild("Name"));
            GameObject scoreText = (GameObject) Instantiate(Text, Content.FindChild("Score"));
            GameObject timeText = (GameObject) Instantiate(Text, Content.FindChild("Time"));

            nameText.GetComponent<Text>().text = name;
            scoreText.GetComponent<Text>().text = score;
            timeText.GetComponent<Text>().text = time;
        }
        StartCoroutine(DBInterface.CloseConnection());
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
