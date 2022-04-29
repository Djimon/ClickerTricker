using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoard : MonoBehaviour
{
    public GameObject Cube;

    private GameObject[] places;

    [SerializeField]
    private float fBasePrice = 0;
    private float fNextPrice = 0;

    private int iBoardLevel = 0;

    void Start()
    {
        places = GameObject.FindGameObjectsWithTag("Place");
        fNextPrice = fBasePrice;
        foreach( var x in places)
        {
            x.gameObject.GetComponent<GroundPlace>().SetPrice(fNextPrice);
            x.gameObject.GetComponent<GroundPlace>().SetSpawnCube(Cube);
        }
        iBoardLevel = 0;
        fNextPrice = 0;
    }

    // Update is called once per frame
    void Update()
    {
     
        
    }

    public void UpdatePlaces(float points)
    {
        Debug.Log("Update Places");
        if (points >= fNextPrice)
        { 
            foreach( var x in places)
            {
                var go = x.gameObject.GetComponent<GroundPlace>();
                if ( go.IsVisible() && !go.IsPlacable() && !go.IsPlaced())
                {
                    go.SetPlaceability(true);
                }    
            }
        }
        else if (points < fNextPrice)
        {
            foreach (var x in places)
            {
                var go = x.gameObject.GetComponent<GroundPlace>();
                if (go.IsPlacable() && !go.IsPlaced())
                    go.SetPlaceability(false);
            }
        }

    }

    public int IncreasePlaceLevel()
    {
        if(iBoardLevel>=GameStats.imaxBoardLevel)
        {
            //nichts passiert
        }
        else
        {
            iBoardLevel+=1;
            UpdateBoardLevel();            
        }

        return iBoardLevel;
    }

    internal void IncreasePrice(float price)
    {
        if(fNextPrice==GameStats.fFirstGroundPrice)
            fNextPrice = GameStats.fSecondGroundPrice;
        else
            fNextPrice = (int)Math.Round(price * GameStats.fGroundPriceMultiplier);        
        
        Debug.Log("Preise erhöht auf " + fNextPrice);
        foreach (var x in places)
        {
            x.gameObject.GetComponent<GroundPlace>().SetPrice(fNextPrice);
        }

    }

    private void UpdateBoardLevel()
    {
        Debug.Log("Boardlevel: "+ iBoardLevel);
        switch(iBoardLevel)
        {
            case 1:
                ActivatePlace(1);
                ActivatePlace(2);
                ActivatePlace(3);
                ActivatePlace(4);
                break;
            case 2:
                ActivatePlace(5);
                ActivatePlace(6);
                ActivatePlace(7);
                ActivatePlace(8);
                break;
            case 3: 
                ActivatePlace(9);
                ActivatePlace(10);
                ActivatePlace(11);
                ActivatePlace(12);
                break;
            case 4:
                ActivatePlace(13);
                ActivatePlace(14);
                ActivatePlace(15);
                ActivatePlace(16);
                break;
            case 5:
                ActivatePlace(17);
                ActivatePlace(18);
                ActivatePlace(19);
                ActivatePlace(20);
                ActivatePlace(21);
                ActivatePlace(22);
                ActivatePlace(23);
                ActivatePlace(24);
                break;
        }
    }

    //TODO: umschreiben, und die Liste der Ids mitgeben, so muss die foreach nur eimal durhclaufen werden
    public void ActivatePlace(int id)
    {
        foreach (var x in places)
        {
            var go = x.gameObject.GetComponent<GroundPlace>();
            if (go.ID() == id)
            {
                Debug.Log("Actiavte ID " +id);
                x.gameObject.SetActive(true);
                go.SetVisibilty(true);
            }
                
        }
    }

}
