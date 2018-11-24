using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{

    [SerializeField] float deathDelay = 2.0f;

    public void LoadGameOver()
    {
        StartCoroutine(WaitAndLoad());
    }
    IEnumerator WaitAndLoad()
    {
        yield return new WaitForSeconds(deathDelay);
        SceneManager.LoadScene("GameOver");
    }
    public void LoadMainGame()
    {
        SceneManager.LoadScene("Game");
        FindObjectOfType<GameSession>().ResetGame();
    }
    public void LoadStartMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void QuitGame()
    {
        Application.Quit();
    }


}
