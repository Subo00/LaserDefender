using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthDisplay : MonoBehaviour
{
    private TMP_Text _healthText;
    private Player _player;
    void Start()
    {
        _player = GameObject.FindWithTag("Player").GetComponent<Player>();  //There is only one player on the scene 
        _healthText = gameObject.GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        _healthText.text = _player.GetHealth().ToString();
    }
}
