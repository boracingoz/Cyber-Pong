using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private bool _isAI;
    [SerializeField] private GameObject _ball;

    private Rigidbody2D _rb;
    private Vector2 _playerMove;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (_isAI)
        {
            AIController();
        }
        else
        {
            PlayerController();
        }
    }

    void PlayerController()
    {
        _playerMove = new Vector2(0, Input.GetAxisRaw("Vertical"));
    }

    void AIController()
    {
        if (_ball.transform.position.y > transform.position.y + 0.5f)
        {
            _playerMove = new Vector2(0, 1);
        }
        else if(_ball.transform.position.y < transform.position.y - 0.5f)
        {
            _playerMove = new Vector2(0, -1);
        }
        else
        {
            _playerMove = new Vector2(0, 0);
        }
    }

    private void FixedUpdate()
    {
        _rb.velocity = _playerMove * _moveSpeed;
    }
}
