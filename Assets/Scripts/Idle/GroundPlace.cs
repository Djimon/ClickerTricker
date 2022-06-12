using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundPlace : MonoBehaviour
{
    public int id;
    public Material BaseMaterial;
    public GameObject GreenBlink;
    public GameObject RedBlink;
    

    private GameObject SpawnCube;

    [SerializeField]
    private bool isVisible = false;
    private bool isPlaceable = false;
    private bool hasLeft = true;
    private bool isPlaced = false;

    [SerializeField]
    private float fPrice = 5;
    [SerializeField]
    private float  fGrowthSpeed = 1f;
    public int points=1;

    private Material mat;
    private GUIManager GUI;

    // Start is called before the first frame update
    void Start()
    {

        BaseMaterial.color = new Color(1, 1, 1, 0.25f);
        if (!isVisible)
            gameObject.GetComponent<MeshRenderer>().enabled = false;
        GUI = GameObject.FindGameObjectWithTag("GUI").GetComponent<GUIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetSpawnCube(GameObject cube)
    {
        SpawnCube = cube;
    }

    internal bool IsPlaced()
    {
        return isPlaced;
    }

    public int ID()
    {
        Debug.Log("ID: "+id);
        return id;
    }
    public bool IsVisible()
    {
        return isVisible;
    }

    public bool IsPlacable()
    {
        return isPlaceable;
    }

    public void SetPlaceability( bool place)
    {
        isPlaceable = place;
    }

    public void SetVisibilty (bool visible)
    {
        isVisible = visible;
        if (isVisible)
            gameObject.GetComponent<MeshRenderer>().enabled = true;
    }

    public void SetPrice(float price)
    {
        fPrice = price;
    }

    private void OnMouseOver()
    {

        if(isVisible && isPlaceable && hasLeft)
        {
            // Outline Green Ghost Groundblock
            Debug.Log("Place new Groundblock is Possible");
            var gb = Instantiate(GreenBlink);
            gb.transform.SetParent(transform);
            gb.transform.localPosition = new Vector3(0, 0, 0);
            GUI.ShowText("Place new Ground-Block (cost: "+fPrice+")");
            hasLeft = false;

        }
        else if (isVisible && !isPlaceable && hasLeft)
        {
            // Outline Red 
            Debug.Log("This place is too expensive: " + id);
            var rb = Instantiate(RedBlink);
            rb.transform.SetParent(transform);
            rb.transform.localPosition = new Vector3(0, 0, 0);
            GUI.ShowText("Can't afford (cost: " + fPrice + ")");
            hasLeft = false;
        }

    }

    private void OnMouseExit()
    {
        if(!isPlaced)
            DestroyGhost();
    }

    public void DestroyGhost()
    {
        if (isVisible && !hasLeft)
        {
            //var go = transform.GetChild(0);
            //Destroy(go.gameObject);
            Destroy(GameObject.FindGameObjectWithTag("Ghostblock"));
            hasLeft = true;
            GUI.HideText();
        }
    }

    private void OnMouseDown()
    {
        if(isVisible && isPlaceable && !isPlaced)
        {
            DestroyGhost();
            SpawnGround();
            GUI.ShowText("Purchased 1 new ground for " + fPrice);
            GUI.NotifyPurchase(fPrice);
            GUI.HideText(2f);
        }
    }

    public void SpawnGround()
    {
        var sc = Instantiate(SpawnCube);
        sc.gameObject.transform.SetParent(transform);
        sc.gameObject.transform.localPosition = new Vector3(0, 0, 0);
        sc.gameObject.GetComponent<GroundController>().Initialize(fGrowthSpeed,points);
        //sc.gameObject.GetComponent<GroundController>().GO();
        GUI.IncreaseGrounds();
        
        isVisible = false;
        isPlaced = true;
    }
}
