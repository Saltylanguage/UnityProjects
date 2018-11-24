using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;



public class GameSession : MonoBehaviour
{

    [Range(0, 10)]
    [SerializeField] float gameSpeed;
    [SerializeField] int pointsPerBlock = 75;
    [SerializeField] public TextMeshProUGUI scoreText;
    [SerializeField] bool autoplay;

    [SerializeField] int currentScore = 0;

    private void Awake()
    {
        int gameManagerCount = FindObjectsOfType<GameSession>().Length;
        if(gameManagerCount > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject); 
        }
    }

    private void Start()
    {
        scoreText.text = currentScore.ToString();
    }

    private void Update()
    {
        Time.timeScale = gameSpeed;
    }

    public void AddToScore()
    {
        currentScore += pointsPerBlock;
        scoreText.text = currentScore.ToString();
    }

    public void ResetGameManager()
    {
        currentScore = 0;
        scoreText.text = currentScore.ToString();
    }

    public bool IsAutoPlayEnabled()
    {
        return autoplay;
    }

    
}
