using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallMove : MonoBehaviour
{
    [SerializeField] private float _initialSpeed = 10;
    [SerializeField] private float _increaseSpeed = 0.25f;
    [SerializeField] private Text _playerScoreText;
    [SerializeField] private Text _AIText;

    private int hitCounter;
    private Rigidbody2D _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        Invoke("StartBall", 1.5f);
    }

    private void FixedUpdate()
    {
        _rb.velocity = Vector2.ClampMagnitude(_rb.velocity, _initialSpeed + (_increaseSpeed * hitCounter));
    }

    void StartBall()
    {
        _rb.velocity = new Vector2(-1, 0) * (_initialSpeed + _increaseSpeed * hitCounter);
    }

    void ResetBall()
    {
        _rb.velocity = new Vector2(0,0);
        transform.position = new Vector2(0, 0);
        hitCounter = 0;
        Invoke("StartBall",1.5f);
    }

    void PlayerBounce(Transform myObject)
    {
        hitCounter++;
        Vector2 ballPos = transform.position;
        Vector2 playerPos = myObject.position;

        float xDir, yDir;
        if (transform.position.x > 0)
        {
            xDir = -1;
        }
        else
        {
            xDir = 1;
        }
        yDir = (ballPos.y - playerPos.y) / myObject.GetComponent<Collider2D>().bounds.size.y;
        if (yDir == 0)
        {
            yDir = 0.25f;
        }
        _rb.velocity = new Vector2(xDir, yDir) * (_initialSpeed + (_increaseSpeed * hitCounter));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player" || collision.gameObject.name == "EnmeyAI")
        {
            PlayerBounce(collision.transform);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (transform.position.x > 0)
        {
            ResetBall();
            _playerScoreText.text = (int.Parse(_playerScoreText.text) +1).ToString();
        }
        else if (transform.position.x < 0)
        {
            ResetBall();
            _AIText.text = (int.Parse(_AIText.text) + 1).ToString();
        }
    }
}
