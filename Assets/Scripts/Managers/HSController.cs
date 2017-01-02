using System;
using UnityEngine;
using System.Collections;
using System.Linq;
using System.Security.Cryptography;
using Assets.Scripts;
using Assets.Scripts.Managers;

public class HSController : MonoBehaviour
{
    private static string secretKey = "miriam"; // Edit this value and make sure it's the same as the one stored on the server
    public static string addScoreURL = "http://noteworthy.nz/addscore.php?"; //be sure to add a ? to your url
    public static string highscoreURL = "http://noteworthy.nz/display.php";

    void Start()
    {
        StartCoroutine(GetScores());
    }

    // remember to use StartCoroutine when calling this function!
    public static IEnumerator PostScores(string name, int score)
    {
//        DateTime time = DateTime.UtcNow;

        //This connects to a server side php script that will add the name and score to a MySQL DB.
        // Supply it with a string representing the players name and the players score.
        string hash = Utilities.Md5Sum(name + score + secretKey);

        string post_url = addScoreURL + "playername=" + WWW.EscapeURL(name) + "&score=" +
                          score + "&hash=" + hash;

        //Debug.Log(post_url);
        // Post the URL to the site and create a download object to get the result.
        WWW hs_post = new WWW(post_url);
        yield return hs_post; // Wait until the download is done

        if (hs_post.error != null)
        {
            print("There was an error posting the high score: " + hs_post.error);
        }
    }

    // Get the scores from the MySQL DB to display in a GUIText.
    // remember to use StartCoroutine when calling this function!
    public static IEnumerator GetScores()
    {
        WWW hs_get = new WWW(highscoreURL);
        yield return hs_get;

        if (hs_get.error != null)
        {
            print("There was an error getting the high score: " + hs_get.error);
        }
        else
        {
            string[] split = hs_get.text.Split(new string[] {"\n" }, StringSplitOptions.None);


            string[][] highScores = new string[split.Length - 1][];

            for (int i = 0; i < split.Length - 1; i++)
            {
                highScores[i] = split[i].Split(',');
            }

            yield return highScores;
        }
    }

    public IEnumerator ReturnScores()
    {
        CoroutineWithData cd = new CoroutineWithData(this, GetScores( ) );
        yield return cd.coroutine;
        GameObject.Find("Score List").GetComponent<HighScoreLoader>().DisplayScores((string[][]) cd.result);
        //Debug.Log("result is " + cd.result);  //  'success' or 'fail'
    }

}