using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour
{
    public GameObject LevelUpBtn;
    public GameObject AnzahlText;
    public GameObject GameBoard;
    public GameObject NotifyPanel;
    public Camera cam;

    private int iglobalPoints = 0;
    private int iGrounds = 0;
    private bool canLevelUp = false;

    private int iLevelUpPrice = 10;

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
        TM_Points.text = "Anzahl Mana: " + iglobalPoints;
        GB = GameBoard.gameObject.GetComponent<GameBoard>();
        TM_LevelUp = LevelUpBtn.GetComponentInChildren<Text>();
        TM_LevelUp.text = "Level+ (Price: " + iLevelUpPrice + ")";
        GB.UpdatePlaces(iglobalPoints);;
        UpdatePoints();
        fNotifybaseHeight = NotifyPanel.transform.localPosition.y;
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
        if(canLevelUp && iglobalPoints < iLevelUpPrice)
        {
            var c = TM_LevelUp.GetComponentInParent<Image>().color;
            TM_LevelUp.GetComponentInParent<Image>().color = new Color(c.r, c.g, c.b, 0.3f);
        }
        else if (iglobalPoints >= iLevelUpPrice)
        {
            var c = TM_LevelUp.GetComponentInParent<Image>().color;
            TM_LevelUp.GetComponentInParent<Image>().color = new Color(c.r, c.g, c.b, 1f);
        }

    }

    public void UpdatePointsToGUI(int points)
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
        CheckLevelUp();
        if (canLevelUp && iglobalPoints >= iLevelUpPrice)
        {
            GB.IncreasePlaceLevel();
            UpdatePointsToGUI(-iLevelUpPrice);
            canLevelUp = false;
            iLevelUpPrice *= 2;
            TM_LevelUp.text = "Level+ (Price: " + iLevelUpPrice + ")";
            StartCoroutine(CameraZoomOut(cam));
            Debug.Log("LevelUp");
        }
        else
            Debug.Log("No LevelUp possible");
                
    }

    internal void NotifyPurchase(int price)
    {
        UpdatePointsToGUI(-price);
        GB.IncreasePrice(price);
        GB.UpdatePlaces(iglobalPoints);
    }

    public void IncreaseGrounds()
    {
        iGrounds++;
        CheckLevelUp();
    }

    private void CheckLevelUp()
    {
        Debug.Log("Anzahl Groundd:" + iGrounds + " mod: " + (iGrounds % 4));
        if ((iGrounds-1) % 4 == 0)
            canLevelUp = true;

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
        
        for (float a = y0; a >= y0-100; a -= 1f)
        {
            Debug.Log("from "+ panel.transform.localPosition.y +" to " +a);
            panel.transform.localPosition = new Vector3(0, a, 0);
            yield return null;
        }
    }

}
