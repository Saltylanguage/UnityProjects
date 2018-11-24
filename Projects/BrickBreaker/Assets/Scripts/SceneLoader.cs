using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class SceneLoader : MonoBehaviour
{
    // config params
    [SerializeField] Level currentLevel;

    private void Start()
    {
        currentLevel = GetComponent<Level>();
    }
    public void LoadStartScene()
    {
        currentLevel = FindObjectOfType<Level>();
        if (currentLevel) { Destroy(currentLevel.gameObject); }
        SceneManager.LoadScene(0);
    }

    public void LoadNextScene()
    { 
        if (currentLevel) { currentLevel.levelComplete = false; }
        int currentBuildIndex = SceneManager.GetActiveScene().buildIndex;
        Debug.Log(FindObjectOfType<Button>().gameObject.name);
        FindObjectOfType<Button>().gameObject.SetActive(false);
        FindObjectOfType<GameSession>().scoreText.gameObject.SetActive(true);
        SceneManager.LoadScene(currentBuildIndex + 1);
    }

}
