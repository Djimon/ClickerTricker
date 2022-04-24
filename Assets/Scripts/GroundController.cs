using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundController : MonoBehaviour
{

    public GameObject Growable;

    [SerializeField]
    private int iPoints = 0;
    private bool hasChild = false;

    private GUIManager GUI;
  

    // Start is called before the first frame update
    void Start()
    {

        SpawnNewChild();
        GUI = GameObject.FindGameObjectWithTag("GUI").GetComponent<GUIManager>();
        if (GUI == null)
            Debug.LogError("No GUI instance found.");
        //transform.position = new Vector3(0, 0, 0);

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!hasChild)
        {
            SpawnNewChild();
        }
    }

    public void SpawnNewChild()
    {
        var newGO = Instantiate(Growable, new Vector3(0, 0, 0), Quaternion.identity);
        newGO.transform.SetParent(this.transform);
        newGO.GetComponent<GrowEntity>().SetParentGround(gameObject);
        newGO.transform.localPosition = new Vector3(0, 0.75f, 0);
        hasChild = true;
    }

    public void AddPoints(int points)
    {
        iPoints += points;
        GUI.SendPointsToGUI(points);
        hasChild = false;
    }
}
