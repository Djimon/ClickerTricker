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

    [SerializeField]
    private int iPrice = 5;

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

    public void SetPrice(int price)
    {
        iPrice = price;
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
            hasLeft = false;

        }
        else if (isVisible && !isPlaceable && hasLeft)
        {
            // Outline Red 
            Debug.Log("This place is too expensive: " + id);
            var rb = Instantiate(RedBlink);
            rb.transform.SetParent(transform);
            rb.transform.localPosition = new Vector3(0, 0, 0);
            hasLeft = false;
        }
    }

    private void OnMouseExit()
    {
        var go = transform.GetChild(0);
        Destroy(go.gameObject);

        hasLeft = true;
        
    }

    private void OnMouseDown()
    {
        if(isVisible && isPlaceable)
        {
            SpawnGround();
            GUI.NotifyPurchase(iPrice);
        }
    }

    public void SpawnGround()
    {
        var sc = Instantiate(SpawnCube);
        sc.transform.SetParent(transform);
        sc.transform.localPosition = new Vector3(0, 0, 0);

        GUI.IncreaseGrounds();
        
        isPlaceable = false;
        isVisible = false;
    }
}
