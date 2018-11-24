using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour {

    int score = 0;


	// Use this for initialization
	void Awake ()
    {
        SetUpSingleton();
	}
	private void SetUpSingleton()
    {
        int sessionCount = FindObjectsOfType(GetType()).Length;
        if(sessionCount > 1)
        {
            Destroy(gameObject); 
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public int GetScore()
    {
        return score;
    }

    public void AddToScore(int points)
    {
        score += points;
    }

    public void ResetGame()
    {
        Destroy(gameObject);
    }

	// Update is called once per frame
	void Update () {
		
	}
}
