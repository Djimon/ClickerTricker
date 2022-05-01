using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New PowerUp", menuName ="Loot/PowerUps")]
public class PowerUp : ScriptableObject
{
    public string LootName = "Name";
    public string Description = "Description text here...";
    public Color baseColor = Color.white;
    public Sprite ArtworkImage;
    public ERarity Rarity = ERarity.Common;
    public int basePower = 1; //  = HP = Dmg
    public int ChangeAmount = 10;  // = +10 = +10%


    [Range(0, 1)]
    public float DropChanceWithinRarity = 0.01f;
    public int StackLimit = 10;
}

