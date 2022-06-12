using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LootObject : ScriptableObject
{
    [System.NonSerialized]
    public  ELootType lootType;
    public string LootName = "Name";
    public string Description = "Description text here...";
    public Color baseColor = Color.white;
    public Sprite ArtworkImage;
    public ERarity Rarity = ERarity.Common;
    [Range(1, 100)]
    public int DropChanceWithinRarity =10;
    public int StackLimit = 10;

    public float minSpawnProbability = 0;
    public float maxSpawnProbability = 0;

    public List<string> stats;

    public GameObject itemPrefab;
}
