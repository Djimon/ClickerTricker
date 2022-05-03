using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayInventory : MonoBehaviour
{
    public InventoryObject Inventory;
    public GameObject HoverLayer;

    public int X_Space_between_Item;
    public int Y_Space_between_Item;
    public int Col_Count;

    public int x_Start = 70;
    public int y_Start = -70;

    Dictionary<InventorySlot, GameObject> itemsDisplayed = new Dictionary<InventorySlot, GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        CreateDisplay();
        UpdateDisplay();
    }

  
    // Update is called once per frame
    void FixedUpdate()
    {
        UpdateDisplay();
    }

    public void UpdateDisplay()
    {
        for (int i = 0; i < Inventory.Container.Count; i++)
        {
            if(itemsDisplayed.ContainsKey(Inventory.Container[i]))
            {
                itemsDisplayed[Inventory.Container[i]].GetComponentInChildren<TextMeshProUGUI>().text = Inventory.Container[i].istackAmount.ToString();
            }
            else
            {
                AddItemToinventory(i);
            }
        }
    }

    private void CreateDisplay()
    {
        for(int i=0; i <Inventory.Container.Count; i++)
        {
            AddItemToinventory(i);
        }
    }

    private void AddItemToinventory(int i)
    {
        var obj = Instantiate(Inventory.Container[i].loot.itemPrefab, Vector3.zero, Quaternion.identity, transform);
        obj.gameObject.GetComponent<RectTransform>().localPosition = GetPosition(i);
        obj.gameObject.GetComponent<InventoryItem>().Initialize(Inventory.Container[i].loot, HoverLayer);
        //obj.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = Inventory.Container[i].istackAmount.ToString();
        obj.gameObject.GetComponent<InventoryItem>().UpdateStack(Inventory.Container[i].istackAmount);
        itemsDisplayed.Add(Inventory.Container[i], obj);
    }

    public Vector3 GetPosition(int i)
    {
        var x = x_Start + (X_Space_between_Item * (i % Col_Count));
        var y = y_Start + (-Y_Space_between_Item * (i / Col_Count));
        Debug.Log("X|Y: " + x + " | " + y);
        return new Vector3(x,y, 0f);
        
    }

    private void OnApplicationQuit()
    {
        //Inventory.Container.Clear();
    }
}

