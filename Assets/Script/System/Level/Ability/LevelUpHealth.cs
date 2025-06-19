using Unity.VisualScripting;
using UnityEngine;

public class LevelUpHealth :  LevelUps
{
    [SerializeField] private int _valueHP = 1;
    [SerializeField] private int _minLevel = 3;

    private PlayerCharecterV1_3 player;
    public override void LevelUp(SystemLevel systemLevel)
    {
        if (systemLevel.Level >= _minLevel)
        {
            player = player ?? systemLevel.GetComponent<PlayerCharecterV1_3>();

            if (player == null) return;

            HPUP(player);
        }
    }

    private void HPUP(PlayerCharecterV1_3 playerCharecter)
    {
        playerCharecter._playerHealth.health += _valueHP;
    }
}
