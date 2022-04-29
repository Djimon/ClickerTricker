using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour
{
    public GameObject LevelUpBtn;
    public GameObject IncrGainBtn;
    public GameObject IncrGrowthBtn;
    public GameObject UpgrBaseBtn;
    public GameObject AnzahlText;
    public GameObject GameBoard;
    public GameObject NotifyPanel;
    public Camera cam;


    private float fglobalPoints = 0;
    private int iGrounds = 0;

    private float fLevelUpPrice = 10;
    private float fIncreaseGainPrice = 100;
    private int iGainLevelUp = 0;
    private float fIncreaseGrowthRatePrice = 25;
    private float fUpgradeBaseGroundPrice = 100;
    private float fAutomateGroundPrice = 100;

    private float fNotifybaseHeight = 0;

    private Text TM_Points;
    private Text TM_LevelUp;

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
        TM_Points.text = ""+fglobalPoints;
        GB = GameBoard.gameObject.GetComponent<GameBoard>();
        TM_LevelUp = LevelUpBtn.GetComponentInChildren<Text>();
        TM_LevelUp.text = "Ground+ (Price: " + fLevelUpPrice + ")";
        GB.UpdatePlaces(fglobalPoints);;
        UpdatePoints();
        fNotifybaseHeight = NotifyPanel.transform.localPosition.y;
        fLevelUpPrice = GameStats.flevelUpStartPrice;
        fIncreaseGainPrice = GameStats.fIncreaseGlobalGainPrice;
        fIncreaseGrowthRatePrice = GameStats.fIncreaseGrowthRatePrice;
        fUpgradeBaseGroundPrice = GameStats.fUpgradeBaseGroundPrice;
        fAutomateGroundPrice = GameStats.fAutomateGroundPrice;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void UpdatePoints()
    {
        TM_Points.text = "" + fglobalPoints;
        collect.Play();
        GB.UpdatePlaces(fglobalPoints);
        if(fglobalPoints < fLevelUpPrice)
        {
            var c = TM_LevelUp.GetComponentInParent<Image>().color;
            TM_LevelUp.GetComponentInParent<Image>().color = new Color(c.r, c.g, c.b, 0.3f);
        }
        else if (fglobalPoints >= fLevelUpPrice)
        {
            var c = TM_LevelUp.GetComponentInParent<Image>().color;
            TM_LevelUp.GetComponentInParent<Image>().color = new Color(c.r, c.g, c.b, 1f);
        }

        if (GameStats.IsAutomationPreview)
            CheckAutomationPrice();
    }

    public void UpdatePointsToGUI(float points)
    {
        fglobalPoints += points;
        UpdatePoints();
    }

    public float GetGlobalPoints()
    {
        return fglobalPoints;
    }

    //GUI-BUtton-ACtion
    public void clk_LevelUp()
    {
        //CheckLevelUp();
        if (fglobalPoints >= fLevelUpPrice)
        {
            int x = GB.IncreasePlaceLevel();
            if (x < GameStats.imaxBoardLevel)
            {
                UpdatePointsToGUI(-fLevelUpPrice);
                fLevelUpPrice *= GameStats.fLevelUpPriceMultiplier;
                TM_LevelUp.text = "Ground+ (Price: " + fLevelUpPrice + ")";
                StartCoroutine(CameraZoomOut(cam));
                Debug.Log("LevelUp");
            }
            else
                Debug.Log("Max level");            
        }
        else
            Debug.Log("No LevelUp possible");                
    }

    public void clk_IncreaseGrowthGlobally()
    {
        if(fglobalPoints >= fIncreaseGrowthRatePrice)
        {
            UpdatePointsToGUI(-fIncreaseGrowthRatePrice);
            fIncreaseGrowthRatePrice *= GameStats.fIncreaseGrowthPriceMultiplier;
            IncrGrowthBtn.GetComponentInChildren<Text>().text = "Growth +"+ 100*(GameStats.fGrowthRateMultiplier - 1) +"% (" + fIncreaseGrowthRatePrice + ")";
            GameObject[] Grounds = GameObject.FindGameObjectsWithTag("Ground");
            foreach (GameObject g in Grounds)
            {
                g.gameObject.GetComponent<GroundController>().UpdateGrowthSpeed(GameStats.fGrowthRateMultiplier);
            }
            Debug.Log(Grounds.Length + " Grounds found");
        }       
    }

    public void clk_IncreaseGainGlobally()
    {
        if (fglobalPoints >= fIncreaseGainPrice)
        {
            if (iGainLevelUp % 5 == 0)
                GameStats.iIncreasePointsglobally *= GameStats.iInCreaseGainLevelUpMultiplier;
            UpdatePointsToGUI(-fIncreaseGainPrice);
            fIncreaseGainPrice *= GameStats.fIncreaseGainPriceMultiplier;
            IncrGainBtn.GetComponentInChildren<Text>().text = "Profit +"+ GameStats.iIncreasePointsglobally +" (" + fIncreaseGainPrice + ")";
            GameObject[] Grounds = GameObject.FindGameObjectsWithTag("Ground");
            foreach (GameObject g in Grounds)
            {
                g.gameObject.GetComponent<GroundController>().UpdatePoints(GameStats.iIncreasePointsglobally);
                //Debug.Log("Points Updated: " + i);
            }
            Debug.Log(Grounds.Length + " Grounds found");
            iGainLevelUp++;
        }
    }

    public void clk_UpgradeBaseGround()
    {
        if (fglobalPoints >= fUpgradeBaseGroundPrice)
        {
            UpdatePointsToGUI(-fUpgradeBaseGroundPrice);
            fUpgradeBaseGroundPrice = Mathf.Floor( fUpgradeBaseGroundPrice * GameStats.fUpgradeBaseGroundPriceMultiplier);
            UpgrBaseBtn.GetComponentInChildren<Text>().text = "Base +" + GameStats.iUpgradeBaseGroundPoints + " (" + fUpgradeBaseGroundPrice + ")";
            GameObject[] Grounds = GameObject.FindGameObjectsWithTag("Ground");
            foreach (GameObject g in Grounds)
            {
                g.gameObject.GetComponent<GroundController>().UpdatePointsBaseGround(GameStats.iUpgradeBaseGroundPoints);
            }
        }
    }

    public void clk_AutomateGround()
    {
        CheckAutomationPrice();
    }

    private void CheckAutomationPrice()
    {
        if (fglobalPoints >= fAutomateGroundPrice)
        {
            GameStats.IsAutomationPreview = true;
        }
        else
        {
            GameStats.IsAutomationPreview = false;
        }
    }

    internal void NotifyPurchase(float price)
    {
        UpdatePointsToGUI(-price);
        GB.IncreasePrice(price);
        GB.UpdatePlaces(fglobalPoints);
    }

    public void IncreaseGrounds()
    {
        iGrounds++;
        //CheckLevelUp();
    }


    public void ShowText(String text)
    {
        NotifyPanel.gameObject.transform.localPosition= new Vector3(0,fNotifybaseHeight,0);
        NotifyPanel.gameObject.GetComponentInChildren<Text>().text = text;
        float y0 = NotifyPanel.transform.localPosition.y;
        StartCoroutine(ShowNotifyPanel(NotifyPanel.transform, y0));
    }

    public void HideText(float wait=0.3f)
    {
        float y0 = NotifyPanel.transform.localPosition.y;
        StartCoroutine(HideNotifyPanel(NotifyPanel.transform,y0,wait));
    }

    IEnumerator CameraZoomOut(Camera cam)
    {
        var y = cam.transform.position.y;
        Debug.Log("old Y: " +y);
        for (float alpha = y; alpha <= y+2.5F; alpha += 0.02f)
        {
            cam.transform.position = new Vector3(0, alpha, 0);
            Debug.Log("new Y: " + alpha);
            yield return null;
        }
    }

    IEnumerator ShowNotifyPanel(Transform panel, float y0)
    {     

        for (float a = y0; a< y0+100; a+=1f)
        {
            panel.transform.localPosition = new Vector3(0, a, 0);
            yield return null;
        }
    }

    IEnumerator HideNotifyPanel(Transform panel, float y0, float wait=0.3f)
    {
        yield return new WaitForSeconds(wait);
        for (float a = y0; a >= y0-100; a -= 1f)
        {
            Debug.Log("from "+ panel.transform.localPosition.y +" to " +a);
            panel.transform.localPosition = new Vector3(0, a, 0);
            yield return null;
        }
    }

}
