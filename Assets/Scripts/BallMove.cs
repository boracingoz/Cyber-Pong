using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallMove : MonoBehaviour
{
    [SerializeField] private float _initialSpeed = 10;
    [SerializeField] private float _increaseSpeed = 0.25f;

    public ScoreManager scoreManager;

    private int hitCounter;
    private Rigidbody2D _rb;
    private bool lastScorerWasPlayer = false;

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
        float direction = lastScorerWasPlayer ? -1 : 1;
        _rb.velocity = new Vector2(direction, 0) * (_initialSpeed + _increaseSpeed * hitCounter);
    }

    void ResetBall()
    {
        _rb.velocity = Vector2.zero;
        transform.position = new Vector2(0, 0); 
        hitCounter = 0;
        Invoke("StartBall", 1.5f); 
    }

    void PlayerBounce(Transform myObject)
    {
        hitCounter++;
        Vector2 ballPos = transform.position;
        Vector2 playerPos = myObject.position;

        float xDir = (transform.position.x > 0) ? -1 : 1; 
        float yDir = (ballPos.y - playerPos.y) / myObject.GetComponent<Collider2D>().bounds.size.y;

        if (yDir == 0) yDir = 0.25f; 

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
            scoreManager.PlayerGoal();
            lastScorerWasPlayer = false; 
        }
        else if (transform.position.x < 0)
        {
            ResetBall();
            scoreManager.AIGoal();
            lastScorerWasPlayer = true; 
        }
    }
}
