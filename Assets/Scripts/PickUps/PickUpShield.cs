using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpShield : PickUp
{
    [SerializeField] private float _shiledTime = 5f;
    protected override void GetPower(Player player)
    {
        player.ActivateShiled(_shiledTime);
    }
}
