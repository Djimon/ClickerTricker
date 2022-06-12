using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundController : MonoBehaviour
{

    public GameObject Growable;
    public GameObject AutomationGhostBlock;
    public Material AutomatedMaterial;

    [SerializeField]
    private int iPoints = 0;
    private bool hasChild = false;
    public float fgrowthSpeed = 1f;
    public int iGivePoints = 1;
    public bool autoCollect = false;

    private bool isReady = false;
    private bool isBaseGround = false;
    private bool isAutomationActivated = false;

    private bool hasLeft = true;

    private GUIManager GUI;
  

    // Start is called before the first frame update
    void Start()
    {

        //SpawnNewChild();
        GUI = GameObject.FindGameObjectWithTag("GUI").GetComponent<GUIManager>();
        if (GUI == null)
            Debug.LogError("No GUI instance found.");
        //transform.position = new Vector3(0, 0, 0);
        fgrowthSpeed = GameStats.fGrowthStartSpeed;
        if(! GameStats.hasBaseGround)
        {
            GameStats.GO_BaseGround = gameObject;
            GameStats.hasBaseGround = true;
            isBaseGround = true;
            fgrowthSpeed = GameStats.iBaseGroundGrowthSpeed;
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!hasChild && isReady)
        {
            SpawnNewChild();
        }
    }

    public void Initialize(float speed, int points)
    {
        fgrowthSpeed = speed;
        Debug.Log("growth speed set to "+speed);
        iGivePoints = points;
        isReady = true;
        Debug.Log("Start Spawning growables");
    }

    public void SpawnNewChild()
    {
        var newGO = Instantiate(Growable, new Vector3(0, 0, 0), Quaternion.identity);
        newGO.transform.SetParent(this.transform);
        newGO.GetComponent<GrowEntity>().Initialize(gameObject, fgrowthSpeed, iGivePoints, autoCollect);
        newGO.transform.localPosition = new Vector3(0, 0.75f, 0);
        hasChild = true;
    }

    public void AddPoints(int points)
    {
        iPoints += points;
        GUI.UpdatePointsToGUI(points);
        hasChild = false;
    }

    public void UpdatePoints(int increase)
    {
        if(!isBaseGround)
            iGivePoints += increase;
    }

    public void UpdateGrowthSpeed(float multiplier)
    {
        if (!isBaseGround)
            fgrowthSpeed *= multiplier;
    }

    public void UpdatePointsBaseGround(int increase)
    {
        if (isBaseGround)
            iGivePoints += increase;
    }

    public void UpdateGrowthSpeedBaseGround(float multiplier)
    {
        if (isBaseGround)
            fgrowthSpeed *= multiplier;
    }


    private void OnMouseOver()
    {
        if (GameStats.IsAutomationPreview && !isBaseGround && !isAutomationActivated && hasLeft)
        {
            var gb = Instantiate(AutomationGhostBlock);
            gb.transform.SetParent(transform);
            gb.transform.localPosition = new Vector3(0, 0, 0);
            GUI.ShowText("Activate Automation for this Ground (cost: " + GameStats.fAutomateGroundPrice + ")");
            hasLeft = false;
        }
        else if(GameStats.IsAutomationPreview && isBaseGround)
        {
            GUI.ShowText("Base Ground can't be automated");
        
        }
    }

    private void OnMouseExit()
    {
        if (GameStats.IsAutomationPreview && !isAutomationActivated)
            DestroyGhost();
    }

    public void DestroyGhost()
    {
        if (!hasLeft)
        {
            Destroy(GameObject.FindGameObjectWithTag("Ghostblock"));
            hasLeft = true;
            GUI.HideText();
        }
    }

    private void OnMouseDown()
    {
        if (!isBaseGround && GameStats.IsAutomationPreview && !isAutomationActivated && GUI.GetGlobalPoints()>=GameStats.fAutomateGroundPrice )
        {
            DestroyGhost();
            GameStats.IsAutomationPreview = false;
            GUI.ShowText("Purchased Automation for this Ground " + GameStats.fAutomateGroundPrice);
            GUI.NotifyPurchase(GameStats.fAutomateGroundPrice);
            autoCollect = true;
            isAutomationActivated = true;
            gameObject.GetComponent<MeshRenderer>().material = AutomatedMaterial;
            GUI.HideText(2f);
        }
    }

}
