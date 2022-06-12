using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class LootBox : MonoBehaviour
{

    public List<LootObject> loot;
    public GameObject GOinventory;
    private DisplayInventory inventory;

    public LootObject[] lootBoxContent;
    public ELootBoxLevel lootboxLevel;

    private float SumSpawnRate = 0;

    // Start is called before the first frame update
    void Start()
    {
        inventory = GOinventory.gameObject.GetComponent<DisplayInventory>();

        InitializeSpawnProbabilities();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitializeSpawnProbabilities()
    {
        Debug.Log("Init Probas!");
        for (int i = 0; i < lootBoxContent.Length; i++)
        {
            SumSpawnRate += lootBoxContent[i].DropChanceWithinRarity;

            for (int j = 0; j < lootBoxContent.Length; j++)
            {
                if (j == 0)
                {
                    lootBoxContent[j].minSpawnProbability = 0;
                    // "/SumSpawnSpawnRate *100" to normalize over any given Spawnrate -> doesn't have to sum up to 100
                    lootBoxContent[j].maxSpawnProbability = ((lootBoxContent[j].DropChanceWithinRarity / SumSpawnRate) * 100) - 1;
                }
                else
                {
                    lootBoxContent[j].minSpawnProbability = lootBoxContent[j - 1].maxSpawnProbability + 1;
                    lootBoxContent[j].maxSpawnProbability = lootBoxContent[j].minSpawnProbability + ((lootBoxContent[j].DropChanceWithinRarity / SumSpawnRate) * 100) - 1;
                }

                Debug.Log("Loot: "+ lootBoxContent[j].name + " -> minP:" + lootBoxContent[j].minSpawnProbability + " | maxP:" + lootBoxContent[j].maxSpawnProbability);
            }
        }
    }

    public void AleaIactaEst()
    {
        int size = Random.Range(0, 3) + 3;
        Debug.Log("Size of LootBox" + size);
        string LootThisRound = "";

        for (int i=0; i < size; i++)
        {
            LootThisRound += GenerateLoot() +" | ";
        }

        Debug.Log(size+"x LOOT: " + LootThisRound);
    }

    private string GenerateLoot()
    {
        float randNum = Random.Range(0, 101);
        Debug.Log("radnom num = " + randNum);
        bool unlucky = true;
      
        for (int i = 0; i < lootBoxContent.Length; i++)
        {
            if (randNum >= lootBoxContent[i].minSpawnProbability && randNum <= lootBoxContent[i].maxSpawnProbability)
            {
                //ToDO: Berechnen wieviele
                int iStack = 1;

                //TODO: Zuerst in einer Vorschau die Karten an zeigen, dann die Karten ins Inventory legen
                // quasi genau das gleiche system wie bei inventory, nur in der Mitte des Bildschirms und statt der icons kommen die Cards
                inventory.Inventory.AddLoot(lootBoxContent[i],iStack);
                //Instantiate(, Vector3.zero, Quaternion.identity, inventory.transform);
                unlucky = false;
                return lootBoxContent[i].itemPrefab.name;
            }
        }

        if (unlucky)
        {
            Debug.Log("LOL - You didn't get loot at all. You are the unlucky one!");
            return "nichts- LOL!";

        }

        return "Shouldn't reach this point?";

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
