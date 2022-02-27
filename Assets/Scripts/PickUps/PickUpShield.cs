using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpShield : PickUp
{
    [SerializeField] private float _shieldTime = 5f;
    protected override void GetPower(Player player)
    {
        player.ActivateShield(_shieldTime);
    }
}
