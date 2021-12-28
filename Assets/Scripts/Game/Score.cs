using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    private int _score = 0;
    private void Awake(){   SetUpSingleton(); }
    public void AddScore(int score){ _score += score; }
    public int GetScore(){ return _score; }
    public void ResetScore(){ Destroy(gameObject); }

    private void SetUpSingleton()
    {
        int numGameSessions = FindObjectsOfType<Score>().Length;
        if(numGameSessions > 1 )
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

}
