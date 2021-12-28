using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{

    [SerializeField] private float _time = 1f;

    private int _currentSceneIndex;
    void Start(){ _currentSceneIndex = SceneManager.GetActiveScene().buildIndex;  }
    public void LoadNextScene(){ SceneManager.LoadScene(_currentSceneIndex + 1); }
    public void ReloadScene() { SceneManager.LoadScene(_currentSceneIndex); }
    public void LoadStartScene(){ SceneManager.LoadScene(0); }
    public void LoadGameScene()
    { 
        SceneManager.LoadScene(1);
        FindObjectOfType<Score>().ResetScore();
    }
    public void LoadGameOver()
    { 
        StartCoroutine(WaitAndLoad(_time));
    }
    IEnumerator WaitAndLoad(float time)
    {
        yield return new WaitForSeconds(time);
        
        SceneManager.LoadScene("GameOverScene");
    }
    public void QuitGame() { Application.Quit();}
}
