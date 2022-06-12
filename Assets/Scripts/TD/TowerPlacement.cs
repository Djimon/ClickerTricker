using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPlacement : MonoBehaviour
{

    private GameObject currentPlacingTower;
    
    public Material RedGhost;

    [SerializeField]
    private Camera MainCamera;
    [SerializeField]
    private LayerMask PlacementColldierMaks;

    private TowerBehaviour tower;
    private bool isPlaceableMaterial = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(currentPlacingTower != null)
        {
            Transform parentTranform = null;
            Ray CamRay = MainCamera.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(CamRay, out RaycastHit hitInfo, 100f, PlacementColldierMaks))
            {
                currentPlacingTower.transform.position = hitInfo.point;
                //Debug.Log("hit: " + hitInfo.point);
                if (hitInfo.transform.gameObject.CompareTag("Towerplace") && !hitInfo.transform.gameObject.CompareTag("CantPlace"))
                {
                    ChangePlaceability(true);
                    parentTranform = hitInfo.transform;
                }
                else
                {
                    ChangePlaceability(false);
                    parentTranform = null;
                }                
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Destroy(currentPlacingTower);
                tower = null;
                currentPlacingTower = null;
                return;
            }

            if (Input.GetMouseButtonDown(0))
            {
                if(tower.isPlaceable)
                {
                    // tower.MeshToMaterial.Clear();
                    TDGameLoop.TowersInGame.Add(tower);
                    currentPlacingTower.transform.SetParent(parentTranform);
                    tower.gameObject.layer = 3; //3 = Tower
                    tower = null;
                    currentPlacingTower = null;
                }
                else
                {
                    Debug.Log("is not placeable here!");
                }
                    
            }
                        
        }
    }

    public void SetTowerToPlace(GameObject tower)
    {
        currentPlacingTower = Instantiate(tower, Vector3.zero, Quaternion.identity);
        this.tower = currentPlacingTower.GetComponent<TowerBehaviour>();
        ChangePlaceability(false);
    }

    public void ChangePlaceability(bool isPlaceable)
    {
        if(isPlaceable && !isPlaceableMaterial)
        {
            tower.isPlaceable = true;
            foreach (KeyValuePair<MeshRenderer,Material> MR in tower.MeshToMaterial)
            {
                MR.Key.sharedMaterial = MR.Value;
            }
            isPlaceableMaterial = true;
            //Debug.Log("change placeability to TRUE");
        }
        else if(!isPlaceable && isPlaceableMaterial)
        {
            tower.isPlaceable = false;
            foreach(KeyValuePair < MeshRenderer, Material > MR in tower.MeshToMaterial)
            {
                MR.Key.sharedMaterial = RedGhost;
            }
            isPlaceableMaterial = false;
            //Debug.Log("change placeability to FALSE");
        }

        
    }

    

}
