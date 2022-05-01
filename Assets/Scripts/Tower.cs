using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Tower", menuName = "Loot/Tower")]
public class Tower : ScriptableObject
{
    public string LootName = "Name";
    public string Description = "Description text here...";
    public Color baseColor = Color.white;
    public Sprite ArtworkImage;
    public ERarity Rarity = ERarity.Common;
    public int basePower = 1; //  = HP = Dmg

    public GameObject TowerPrefab;
    public ETowerType TowerType = ETowerType.none;

    [Range(0, 1)]
    public float DropChanceWithinRarity = 0.01f;
    public int StackLimit = 10;
}
