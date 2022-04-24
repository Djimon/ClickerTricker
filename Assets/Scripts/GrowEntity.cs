using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowEntity : MonoBehaviour
{

    [Range(0.1f, 1f)]
    public float fgrowth = 0.1f;

    public float fgrowthSpeed = 0.1f;
    public int iPoints = 1;


    private bool isReady = false;

    [SerializeField]
    private GameObject ParentGround;


    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.localScale = new Vector3(fgrowth, fgrowth, fgrowth);
        gameObject.transform.localPosition = new Vector3(0, 0.75F, 0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!isReady)
        {
            fgrowth = fgrowth * ((100 + fgrowthSpeed) / 100);
            //Debug.Log("Wachstumsrate: "+ fgrowth);
            gameObject.transform.localScale = new Vector3(fgrowth, fgrowth, fgrowth);

            if (fgrowth >= 1f)
                isReady = true;                
        }
    }

    private void OnMouseDown()
    {
        if (!isReady)
            Debug.Log("Not Ready");
        else
        {
            Debug.Log("GaaaiinZ!!!");
            ParentGround.GetComponent<GroundController>().AddPoints(iPoints);
            Destroy(gameObject);
        }
            
    }

    public void SetParentGround(GameObject parent)
    {
        ParentGround = parent;
    }

}
