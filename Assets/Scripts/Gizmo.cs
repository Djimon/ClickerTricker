using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Gizmo : MonoBehaviour
{
    public float meleeRadius;


    public Color GizmoColor = Color.white;

    public void Awake()
    {
        meleeRadius = gameObject.GetComponent<TowerBehaviour>().Range;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = GizmoColor;
        float theta = 0;
        float x = meleeRadius * Mathf.Cos(theta);
        float y = meleeRadius * Mathf.Sin(theta);
        Vector3 pos = transform.position + new Vector3(x, 0, y);
        Vector3 newPos = pos;
        Vector3 lastPos = pos;
        for (theta = 0.1f; theta < Mathf.PI * 2; theta += 0.1f)
        {
            x = meleeRadius * Mathf.Cos(theta);
            y = meleeRadius * Mathf.Sin(theta);
            newPos = transform.position + new Vector3(x, 0, y);
            Gizmos.DrawLine(pos, newPos);
            pos = newPos;
        }
        Gizmos.DrawLine(pos, lastPos);
    }
}
