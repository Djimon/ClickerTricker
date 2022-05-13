using System.Collections;
using System.Collections.Generic;
using UnityEngine;




[CreateAssetMenu(fileName = "New Inventory", menuName ="InventorySystem/Inventory")]
public class InventoryObject : ScriptableObject
{
    public string inventoryName;
    public List<InventorySlot> Container = new List<InventorySlot>();
    public int iMaxSize = 24;

    public bool AddLoot(LootObject _loot, int stack)
    {
        bool hasLoot = false;
        for(int i=0; i< Container.Count; i++)
        {
            if(Container[i].loot == _loot)
            {
                if(Container[i].istackAmount < Container[i].loot.StackLimit)
                {
                    Container[i].AddAmount(stack);
                    Debug.Log("Added "+ stack+ " "+_loot.name + " to inventory");
                    hasLoot = true;
                    return true;
                }
                else
                {
                    Debug.Log("Max StackLimit reached for "+ _loot.LootName);
                    return false;
                }
            }
            
        }

        if(!hasLoot)
        {
            //if(Container.Count >=24)
            //{
            //    Debug.Log("Max Slots reached, loot not added to the inventory");
            //    return false;
            //}
            //else
            {
                Container.Add(new InventorySlot(_loot, stack));
                Debug.Log("Added " + stack + " "+ _loot.name + " to inventory");
                return true;
            }   
        }

        //Shouldn't reach this one, just for compiler
        Debug.LogWarning("Shouldn't reach this one");
        return false;
    }
}

[System.Serializable]
public class InventorySlot
{
    public LootObject loot;
    public int istackAmount;

    public InventorySlot(LootObject _loot, int _stack)
    {
        loot = _loot;
        istackAmount = _stack;
    }

    public void AddAmount(int value)
    {
        istackAmount += value;
    }

}
