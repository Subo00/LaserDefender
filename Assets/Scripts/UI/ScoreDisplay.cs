using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreDisplay : MonoBehaviour
{
    private Score _score;
    private TMP_Text _scoreText;
    void Start()
    {
        _score = GameObject.FindObjectOfType<Score>();  //There is only one score on the scene 
        _scoreText = GetComponent<TMP_Text>();
        _scoreText.text = _score.GetScore().ToString();
    }

}
