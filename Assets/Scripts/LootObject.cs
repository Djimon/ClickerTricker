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
    [Range(0, 1)]
    public float DropChanceWithinRarity = 0.01f;
    public int StackLimit = 10;

    public List<string> stats;

    public GameObject itemPrefab;
}
