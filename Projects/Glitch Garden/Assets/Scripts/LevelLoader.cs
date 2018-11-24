using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{

    [SerializeField] float loadDelay = 4.0f;
    int currentSceneIndex = 0;


    IEnumerator WaitAndLoad()
    {
        yield return new WaitForSeconds(loadDelay);
        LoadNextScene();
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    // Use this for initialization
    void Start()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentSceneIndex == 0)
        {
            StartCoroutine(WaitAndLoad());
        }
    }

    // Update is called once per frame
    void Update()
    {

    }




}
