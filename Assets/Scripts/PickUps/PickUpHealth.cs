
using UnityEngine;
public class PickUpHealth : PickUp
{
    [SerializeField] private int _healthToAdd = 50;
    
    protected override void GetPower(Player player)
    {
        player.ProcessHeal(_healthToAdd);
    }


}
