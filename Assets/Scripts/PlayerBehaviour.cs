using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerBehaviour : MonoBehaviour
{
    public GameObject player;
    public float moveSpeed;
    public Text countText;

    private int _score;

    private float _angle = 0;

    private float _x;
    private float _y;
    private float _z;
    public float radius = 40;
    public float gravity = 0.01f;
    public float jump = 3f;

    // Start is called before the first frame update
    void Start()
    {
        _x = player.transform.position.x;
        _y = player.transform.position.y;
        _z = player.transform.position.z;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        _y = _y > gravity ? _y - gravity : 0f;

        if (_y == 0) GameOver();

        if (Input.GetKeyDown("space"))
        {
            _y += jump;

        }// Touch support
        foreach (var touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                _y += jump;
            }
        }         

        _x = radius * Mathf.Cos(_angle);
        _z = radius * Mathf.Sin(_angle);

        Vector3 vector3 = new Vector3(_x, _y, _z);
        transform.position = vector3;
        _angle += moveSpeed * Mathf.Deg2Rad * Time.deltaTime;  


    }

    void OnTriggerEnter (Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "CrashObject":
                //print("crashed");
                GameOver();
                break;
            case "Floor":
                print("landed");
                //_nextPositionHolder.y += 1f;
                GameOver();
                break;
            case "ScoreObject":
                _score++;
                //print($"Scored, points: {_score}");
                CountText();
                break;
            default:
                break;
        }
       
    }

    void CountText()
    {
        countText.text = $"Score: {_score.ToString()}";
    }

    void GameOver()
    {
        SetScore();
        SceneManager.LoadScene("EndScreen");        
    }

    void SetScore()
    {
        print("ScoreSetting");
        print(PlayerPrefs.GetInt("highscore"));

        PlayerPrefs.SetInt("score", _score);
        print($"score set: {PlayerPrefs.GetInt("score")}");
                
        var currentHighScore = PlayerPrefs.GetInt("highscore");
        currentHighScore = currentHighScore < _score ? _score : currentHighScore;
        PlayerPrefs.SetInt("highscore", currentHighScore);
        
    }
}
