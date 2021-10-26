using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField] float inputDelayTime = 3f;
    [SerializeField] float loadSceneDelayTime = 3f;

    ScoreDisplay scoreDisplay;

    float timer = 0;
    int currentSceneIndex;

    private void Start()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        
        scoreDisplay = FindObjectOfType<ScoreDisplay>();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if(timer < inputDelayTime)
        {
            return;
        }
        
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (Input.anyKey && currentSceneIndex == 0)
        {
            SceneManager.LoadScene(1);
        }

        if (Input.anyKey && (currentSceneIndex == 4 || currentSceneIndex == 5))
        {
            Destroy(scoreDisplay.gameObject);
            SceneManager.LoadScene(0);
        }
    }

    public void loadLoseScreen()
    {
        StartCoroutine(PauseGameAndLoadSeneWithDelay(5));
    }


    public IEnumerator PauseGameAndLoadSeneWithDelay(int index)
    {
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(loadSceneDelayTime);
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        Time.timeScale = 1;
        SceneManager.LoadScene(index);
    }
}
