using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpPower : PickUp
{
    [SerializeField] private float _powerTime = 3f;
    protected override void GetPower(Player player)
    {
        player.PoweredFire(_powerTime);
    }

}
