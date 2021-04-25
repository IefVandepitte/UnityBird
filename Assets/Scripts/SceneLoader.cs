using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{

    public void NextScene(string scene)
    {
        print(scene);
        SceneManager.LoadScene(scene);
    }

    //private void GetScores()
    //{
    //    print(PlayerPrefs.GetInt("score"));
    //    print(PlayerPrefs.GetInt("highscore"));

    //    score.text = PlayerPrefs.GetInt("score") == 0 ? "Score: 0" : $"Score: {PlayerPrefs.GetInt("score")}";
    //    highscore.text = PlayerPrefs.GetInt("highscore") == 0 ? "HighScore: 0" :$"Score: {PlayerPrefs.GetInt("highscore")}";
    //}
}

