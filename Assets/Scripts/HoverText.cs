using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HoverText : MonoBehaviour
{
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI desc;
    public TextMeshProUGUI rarity;
    public GameObject StatsContainer;

    public GameObject StatsPrefab;
    private Color rarityColor;

    public void Initialize(string _name, string _desc, ERarity _rarity, List<string> stats)
    {
        rarityColor = C.ColValue((int)_rarity); 
        itemName.text = _name;
        itemName.color = rarityColor;
        desc.text = _desc;
        desc.color = rarityColor;
        rarity.text = _rarity.ToString();
        rarity.color = rarityColor;


        for (int i=0; i<stats.Count; i++)
        {
            AddStats(stats[i]);
        }
    }

    public void AddStats(string statdesc)
    {
        var stat = Instantiate(StatsPrefab);
        stat.gameObject.GetComponent<TextMeshProUGUI>().text = statdesc;
        stat.gameObject.GetComponent<TextMeshProUGUI>().color = rarityColor;
        stat.transform.SetParent(StatsContainer.transform);

    }
}
