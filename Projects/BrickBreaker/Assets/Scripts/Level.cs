using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level : MonoBehaviour
{

    // config params
    [SerializeField] GameObject nextLevelButton;

    // State Variables
    public bool levelComplete = true;
    private bool firstBlockCounted = false;
    int numBlocks;

	public void CountBlocks()
    {
        
        numBlocks++;
        firstBlockCounted = true;
    }

    public void BreakBlock()
    {
        numBlocks--;
    }

    public void ResetLevel()
    {
        numBlocks = 0;
        firstBlockCounted = false;
        FindObjectOfType<GameSession>().ResetGameManager();
    }

    private void Update()
    {
        Debug.Log(numBlocks);
        if(!levelComplete)
        {
            if (firstBlockCounted && numBlocks == 0)
            {
                levelComplete = true;
                firstBlockCounted = false;
                Ball ball = FindObjectOfType<Ball>();
                if (ball) { ball.gameObject.SetActive(false); }
                Paddle paddle = FindObjectOfType<Paddle>();
                if (paddle) { paddle.gameObject.SetActive(false); }
                nextLevelButton.SetActive(true);
            }
        }
        
    }

}
