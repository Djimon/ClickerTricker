using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TowerBehaviour : MonoBehaviour
{
    public LayerMask EnemiesMask;

    private List<MeshRenderer> prefabRenderers;
    public Dictionary<MeshRenderer, Material> MeshToMaterial;
    public Material baseMaterial;
    public bool isPlaceable = false;

    public ETargetType targetType = ETargetType.Close;
    public float Damage = 1f;
    public float Firerate = 1f;
    public float Range = 25f;
    public Transform TowerPivot;
    public Enemy currentTarget;


    private float Delay;
    
    // Start is called before the first frame update
    void Start()
    {
        Delay = 1 / Firerate;

        prefabRenderers = new List<MeshRenderer>(gameObject.GetComponentsInChildren<MeshRenderer>());
        prefabRenderers.Add(gameObject.GetComponent<MeshRenderer>());
        MeshToMaterial = new Dictionary<MeshRenderer, Material>();

        if (MeshToMaterial.Count == 0)
        {
           
            for (int i = 0; i < prefabRenderers.Count; i++)
            {
                MeshToMaterial.Add(prefabRenderers[i], prefabRenderers[i].material);
            }
        }

    }

    public void Tick()
    {
        if(currentTarget != null)
        {
            TowerPivot.transform.rotation = Quaternion.LookRotation(currentTarget.transform.position - transform.position);
            Debug.Log("Rotate Canon");
        }
    }

}
