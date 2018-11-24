using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{

    [SerializeField] float speed;
    [SerializeField] float screenWidthInUnits = 16.0f;
    [SerializeField] float leftBound = 1.0f;
    [SerializeField] float rightBound = 15.0f;

    Ball ball;
    GameSession gameSession;

    private void Start()
    {
        ball = FindObjectOfType<Ball>();
        gameSession = FindObjectOfType<GameSession>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 paddlePosition = new Vector2(transform.position.x, transform.position.y);
        paddlePosition.x = Mathf.Clamp(GetXPos(), leftBound, rightBound);
        transform.position = paddlePosition;
    }

    private float GetXPos()
    {
        if (gameSession.IsAutoPlayEnabled())
        {
            return ball.transform.position.x;
        }
        else
        {
            return Input.mousePosition.x / Screen.width * screenWidthInUnits;
        }
    }
}
