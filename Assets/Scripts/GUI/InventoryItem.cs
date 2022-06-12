using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class InventoryItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject MouseOverDisplay;
    private LootObject loot;
    public int stack = 1;

    private GameObject hoverLayer;
    private GameObject hoverText;
    private bool isMouseoverActive = false;
    [SerializeField]
    private HoverText hoverScript;

    private TextMeshProUGUI stackText;

    public void Awake()
    {
        
    }

    public void Initialize(LootObject _loot, GameObject _hoverLayer)
    {
        hoverLayer = _hoverLayer;
        loot = _loot;
        hoverText = Instantiate(MouseOverDisplay);
        hoverText.transform.SetParent(hoverLayer.transform);
        hoverScript = hoverText.gameObject.GetComponent<HoverText>();
        stackText = gameObject.GetComponentInChildren<TextMeshProUGUI>();
        stackText.text = stack.ToString();


        string stats = "";
        foreach (string s in loot.stats)
        {
            stats += s;
        }
        Debug.Log(loot.LootName + "|" + loot.Description + "|" + loot.Rarity.ToString()+ "|"+ stats);


        //TODO: Hover-text austuaschen zu Cards
        hoverScript.Initialize(loot.LootName, loot.Description, loot.Rarity, loot.stats);
        hoverText.SetActive(false);
    }

    public int UpdateStack(int value)
    {
        stack += value;
        stackText.text = stack.ToString();
        return stack;
    }

    private void FixedUpdate()
    {
        if (isMouseoverActive)
            hoverText.transform.position = Input.mousePosition;
    }


    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        isMouseoverActive = true;
        hoverText.transform.position = Input.mousePosition;
        hoverText.SetActive(true);
        Debug.Log("Mouseover: " + loot.name);
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        isMouseoverActive = false;
        hoverText.SetActive(false);
        Debug.Log("Leave Mouseover");
    }
}
