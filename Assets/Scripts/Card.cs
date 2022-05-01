using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum ECardType 
{
    PowerUp,
    Tower
}

public class Card : MonoBehaviour
{
    public ECardType CardType;
    public Tower tower;
    public PowerUp powerUp;
    public Image Artwork;
    public Image Rahmen;

    public TextMeshProUGUI tName;
    public TextMeshProUGUI tDesc;
    public TextMeshProUGUI tRarity;
    public TextMeshProUGUI tStack;


    // Start is called before the first frame update
    void Start()
    {
        tName.text = "Test";

        switch (CardType)
        {
            case ECardType.PowerUp: 
                InitializePowerUp();
                break;
            case ECardType.Tower:
                InitializeTower();
                break;
        }
    }

    private void InitializeTower()
    {
        tName.text = tower.name;
        tDesc.text = tower.Description;
        tRarity.text = (tower.Rarity).ToString();
        tStack.text = "1";
        Artwork.sprite = tower.ArtworkImage;
        Rahmen.color = C.ColValue((int)tower.Rarity);
    }

    private void InitializePowerUp()
    {
        tName.text = powerUp.name;
        tDesc.text = powerUp.Description;
        tRarity.text = (powerUp.Rarity).ToString();
        tStack.text = "1";
        Artwork.sprite = powerUp.ArtworkImage;
        Rahmen.color = C.ColValue((int)powerUp.Rarity);
    }

    public void UpdateStack(int count)
    {
        tStack.text = count.ToString();
    }



}

