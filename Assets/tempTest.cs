using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tempTest : MonoBehaviour
{

    public List<LootObject> loot;
    public GameObject GOinventory;
    private DisplayInventory inventory;

    // Start is called before the first frame update
    void Start()
    {
        inventory = GOinventory.gameObject.GetComponent<DisplayInventory>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddLootToInventory()
    {
        for (int i = 0; i < loot.Count; i++)
        {
            inventory.Inventory.AddLoot(loot[i], 1);
            //inventory.UpdateDisplay();
        }
       
    }
}
