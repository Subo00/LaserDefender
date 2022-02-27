using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class UIDisplay : MonoBehaviour
{

    [SerializeField] private TMP_Text _healthText;
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private TMP_Text _shieldText;
    [SerializeField] private TMP_Text _powerText;

    [SerializeField] private GameObject _shieldImage;
    [SerializeField] private GameObject _powerImage;

    private Score _score;
    private Player _player;
    void Start()
    {
        _player = GameObject.FindWithTag("Player").GetComponent<Player>();  //There is only one player on the scene 
        _score = GameObject.FindObjectOfType<Score>();  //There is only one score on the scene 
        _shieldImage.SetActive(false);
        _powerImage.SetActive(false);
    }


    void Update()
    {
        _scoreText.text = _score.GetScore().ToString();
        _healthText.text = _player.GetHealth().ToString();
        UpdateShield();
        UpdatePower();
    }

    void UpdateShield()
    {
        if(_player.GetShieldTime() > 0f && !_shieldImage.activeSelf)
        {
            _shieldImage.SetActive(true);
        }
        else if(_shieldImage.activeSelf)
        {
            var tempFloat = _player.GetShieldTime();
            var tempInt = Mathf.RoundToInt(tempFloat);
            _shieldText.text = tempInt.ToString();
            
            if(_player.GetShieldTime() <= 0f)
            _shieldImage.SetActive(false);
        }
    }

    void UpdatePower()
    {
        if(_player.GetPowerTime() > 0f && !_powerImage.activeSelf)
        {
            _powerImage.SetActive(true);
        }
        else if(_powerImage.activeSelf)
        {
            var tempFloat = _player.GetPowerTime();
            var tempInt = Mathf.RoundToInt(tempFloat);
            _powerText.text = tempInt.ToString();
            
            if(_player.GetPowerTime() <= 0f)
            _powerImage.SetActive(false);
        }
    }
}
