using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour
{

    public GameObject AnzahlText;
    public GameObject GameBoard;

    private int iglobalPoints = 0;
    private int iGrounds = 0;
    private bool canLevelUp = false;

    private Text TM_Points;

    private AudioSource collect;

    private GameBoard GB;

    private void Awake()
    {
        collect = gameObject.GetComponent<AudioSource>();
        Debug.Log("Initialized GUI");
    }

    // Start is called before the first frame update
    void Start()
    {
        TM_Points = AnzahlText.GetComponent<Text>();
        TM_Points.text = "Anzahl Mana: " + iglobalPoints;
        GB = GameBoard.gameObject.GetComponent<GameBoard>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void UpdatePoints()
    {
        TM_Points.text = "Anzahl Mana: " + iglobalPoints;
        collect.Play();
        GB.UpdatePlaces(iglobalPoints);

    }

    public void SendPointsToGUI(int points)
    {
        iglobalPoints += points;
        UpdatePoints();
    }

    public int GetGlobalPoints()
    {
        return iglobalPoints;
    }

    public void LevelUp()
    {
        if (canLevelUp)
        {
            GB.IncreasePlaceLevel();
            canLevelUp = false;
            Debug.Log("LevelUp");
        }
        else
            Debug.Log("No LevelUp possible");
                
    }

    internal void NotifyPurchase(int price)
    {
        SendPointsToGUI(-price);
        GB.IncreasePrice(price);
    }

    public void IncreaseGrounds()
    {
        iGrounds++;
        Debug.Log("Anzahl Groundd:" + iGrounds + " mod: " + (iGrounds%4) );
        if (iGrounds % 4 == 0 )
            canLevelUp = true;

    }

}
