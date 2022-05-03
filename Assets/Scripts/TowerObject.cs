using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Tower", menuName = "Loot/Tower")]
public class TowerObject : LootObject
{
    public int basePower = 1; //  = HP = Dmg

    public GameObject TowerPrefab;
    public ETowerType TowerType = ETowerType.none;
    public float fireRate = 1;
    public float range = 2;

    private void Awake()
    {
        lootType = ELootType.Tower;
    }

}
