using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName ="TD/Enemy")]
public class EnemyData : ScriptableObject
{
    public GameObject EnemyPrefab;
    public int EnemyID;

}
