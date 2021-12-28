using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ScoreDisplay : MonoBehaviour
{
    private TMP_Text _scoreText;
    private Score _score;
    void Start()
    {
        _score = GameObject.FindObjectOfType<Score>();  //There is only one score on the scene 
        _scoreText = gameObject.GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        _scoreText.text = _score.GetScore().ToString();
    }
}
