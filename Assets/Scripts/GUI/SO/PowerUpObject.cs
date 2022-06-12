using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New PowerUp", menuName ="Loot/PowerUps")]
public class PowerUpObject : LootObject
{
    public int ChangeAmount = 10;  // = +10 = +10%;
    public EEffectTarget effectTarget;
    [Range(0,360)]
    public int coolDowninSec;
    //ToDO: Art des OPoweriups deifnieren
    // Auf einzelne Grounds, auf alle Grounds
    //andere Powerups

    private void Awake()
    {
        lootType = ELootType.PowerUp;
    }

}

