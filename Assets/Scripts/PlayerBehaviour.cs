using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerBehaviour : MonoBehaviour
{
    public GameObject[] pathNode;
    public GameObject player;
    public float moveSpeed;
    public Text countText;

    private float _timer;
    private static Vector3 _nextPositionHolder;
    private int _currentNode;
    private Vector3 _startposition;
    private int _score;

    private float _height;
    private Rigidbody _rigid;

    // Start is called before the first frame update
    void Start()
    {
        CheckNode();
        _height = player.transform.position.y;
        _rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        _timer += Time.deltaTime * moveSpeed;

        //_nextPositionHolder.y = _nextPositionHolder.y <= 0.01f ? _nextPositionHolder.y = 0 : _nextPositionHolder.y += -0.01f;
        var currentPosition = transform.position;
        currentPosition.y += -0.001f;
        //print(currentPosition.y);
        transform.position = currentPosition;

        if (Input.GetKeyDown("space"))
        {
            currentPosition.y += 10f;
            print(currentPosition.y);
            transform.position = currentPosition;

        }// Touch support
        foreach (var touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                currentPosition.y += 1f;
                transform.position = currentPosition;
            }
        }
        

        if (player.transform.position.x != _nextPositionHolder.x && player.transform.position.z != _nextPositionHolder.z)
        {
            //var next = _nextPositionHolder;
            //next.y = _height;
            //player.transform.position = Vector3.Lerp(_startposition, next, 0.5f);


            _nextPositionHolder.y = _startposition.y;
            player.transform.position = Vector3.Lerp(_startposition, _nextPositionHolder, _timer);


            //player.transform.LookAt(_nextPositionHolder);

            //var targetRotation = Quaternion.LookRotation(_nextPositionHolder - transform.position);
            //var str = Mathf.Min(0.5f * Time.deltaTime, 1);
            //transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, str);


        }
        else
        {
            if (_currentNode < pathNode.Length - 1)
            {
                _currentNode++;
                CheckNode();
            }
            else
            {
                _currentNode = 0;
                CheckNode();
            }
        }
    }

    void CheckNode()
    {
        _timer = 0;
        _startposition = player.transform.position;
        var next = pathNode[_currentNode];
        _nextPositionHolder = next.transform.position;
        //_nextPositionHolder.y = _height;
        
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
                //print("landed");
                _nextPositionHolder.y += 1f;
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

        if (PlayerPrefs.GetInt("highscore") != 0)
        {
            var currentHighScore = PlayerPrefs.GetInt("highscore");
            currentHighScore = currentHighScore < _score ? _score : currentHighScore;
            PlayerPrefs.SetInt("highscore", currentHighScore);
        }
        else PlayerPrefs.SetInt("highscore", _score);
    }
}
