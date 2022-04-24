using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoard : MonoBehaviour
{
    public GameObject Cube;

    private GameObject[] places;

    [SerializeField]
    private int iBasePrice = 1;
    private int iNextPrice = 5;

    private int iBoardLevel = 1;

    void Start()
    {
        places = GameObject.FindGameObjectsWithTag("Place");
        iNextPrice = iBasePrice;
        foreach( var x in places)
        {
            x.gameObject.GetComponent<GroundPlace>().SetPrice(iNextPrice);
            x.gameObject.GetComponent<GroundPlace>().SetSpawnCube(Cube);
        }
        iBoardLevel = 1;
    }

    // Update is called once per frame
    void Update()
    {
     
        
    }

    public void UpdatePlaces(int points)
    {
        Debug.Log("Update Board");
        if (points >= iNextPrice)
        { 
            foreach( var x in places)
            {
                var go = x.gameObject.GetComponent<GroundPlace>();
                if ( go.IsVisible() && !go.IsPlacable())
                {
                    go.SetPlaceability(true);
                }    
            }
        }
        else if (points < iNextPrice)
        {
            foreach (var x in places)
            {
                var go = x.gameObject.GetComponent<GroundPlace>();
                if (go.IsPlacable())
                    go.SetPlaceability(false);
            }
        }

    }

    public void IncreasePlaceLevel()
    {
        if(iBoardLevel>=4)
        {
            //nichts passiert
        }
        else
        {
            iBoardLevel+=1;
            UpdateBoardLevel();            
        }

    }

    internal void IncreasePrice(int price)
    {
        iNextPrice = (int)Math.Round(price * 1.66);
        Debug.Log("Preise erhöht auf " + iNextPrice);
        foreach (var x in places)
        {
            var go = x.gameObject.GetComponent<GroundPlace>();
            if(go.IsVisible())
                go.SetPrice(iNextPrice);
        }

    }

    private void UpdateBoardLevel()
    {
        Debug.Log("Boardlevel: "+ iBoardLevel);
        switch(iBoardLevel)
        {
            case 2: ActivatePlace(5);
                    ActivatePlace(6);
                    ActivatePlace(7);
                    ActivatePlace(8);
                break;
        }
    }

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
