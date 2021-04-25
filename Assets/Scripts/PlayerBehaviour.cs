using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public GameObject[] pathNode;
    public GameObject player;
    public float moveSpeed;
    public UnityEngine.UI.Text countText;

    private float _timer;
    private static Vector3 _nextPositionHolder;
    private int _currentNode;
    private Vector3 _startposition;
    private int _score;

    // Start is called before the first frame update
    void Start()
    {
        CheckNode();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        _timer += Time.deltaTime * moveSpeed;
        _nextPositionHolder.y = _nextPositionHolder.y <= 0.01f ? _nextPositionHolder.y = 0 : _nextPositionHolder.y += -0.01f;

        if (Input.GetKeyDown("space"))
        {
            print("space key was pressed");
            Vector3 jump = new Vector3(0.0f, 30f, 0.0f);
            _nextPositionHolder.y += 1f;

        }// Touch support
        foreach (var touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                Vector3 jump = new Vector3(0.0f, 30f, 0.0f);
                _nextPositionHolder.y += 1f;
            }
        }


        if (player.transform.position.x != _nextPositionHolder.x && player.transform.position.z != _nextPositionHolder.z)
        {
            player.transform.position = Vector3.Lerp(_startposition, _nextPositionHolder, _timer);
            player.transform.LookAt(_nextPositionHolder);

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
        _nextPositionHolder = pathNode[_currentNode].transform.position;
        
    }

    void OnTriggerEnter (Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "CrashObject":
                print("crashed");
                break;
            case "Floor":
                print("landed");
                _nextPositionHolder.y += 1f;
                break;
            case "ScoreObject":
                _score++;
                print($"Scored, points: {_score}");
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
}
