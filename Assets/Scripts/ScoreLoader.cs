using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreLoader : MonoBehaviour
{
    public Text score;
    public Text highscore;

    // Start is called before the first frame update
    void Start()
    {
        GetScores();
    }

    private void GetScores()
    {
        //print(PlayerPrefs.GetInt("score"));
        //print(PlayerPrefs.GetInt("highscore"));

        if (score != null)
        {
            score.text = PlayerPrefs.GetInt("score") == 0 ? "Score: 0" : $"Score: {PlayerPrefs.GetInt("score")}";

        }
        if (highscore != null)
        {
            highscore.text = PlayerPrefs.GetInt("highscore") == 0 ? "HighScore: 0" : $"HighScore: {PlayerPrefs.GetInt("highscore")}";

        }
    }
}
