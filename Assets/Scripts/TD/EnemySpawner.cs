using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static List<Enemy> EnemiesAlive;

    public static Dictionary<int, GameObject> EnemyPrefabs;
    public static Dictionary<int, Queue<Enemy>> EnemyObjectPools;

    private static bool isInitialized = false;

    // Start is called before the first frame update
    public static void Initialize()
    {
        if(!isInitialized)
        {
            EnemyPrefabs = new Dictionary<int, GameObject>();
            EnemyObjectPools = new Dictionary<int, Queue<Enemy>>();
            EnemiesAlive = new List<Enemy>();

            EnemyData[] Enemies = Resources.LoadAll<EnemyData>("Enemies");
            //Debug.Log(Enemies[0].name);

            foreach (EnemyData ED in Enemies)
            {
                EnemyPrefabs.Add(ED.EnemyID, ED.EnemyPrefab);
                EnemyObjectPools.Add(ED.EnemyID, new Queue<Enemy>());
            }

            isInitialized = true;
        }
        else
        {
            Debug.Log("EnemySpawner: Already Initialoized");
        }
        

    }

    public static Enemy SpawnEnemy(int EnemyID, Vector3 Pos)
    {
        Enemy SpawnedEnemy = null;

        if(EnemyPrefabs.ContainsKey(EnemyID))
        {
            Queue<Enemy> ReferenceQueue = EnemyObjectPools[EnemyID];
            if(ReferenceQueue.Count >0)
            {
                //Dequeue an Enemy
                //Debug.Log("Reuse Enemy");
                SpawnedEnemy = ReferenceQueue.Dequeue();
                SpawnedEnemy.Initialize(Pos);
                SpawnedEnemy.gameObject.SetActive(true);
            }
            else
            {
                //Instantiate new one
                //Debug.Log("Spawn new Enemy");
                GameObject newEnemy = Instantiate(EnemyPrefabs[EnemyID], Vector3.zero, Quaternion.identity);
                SpawnedEnemy = newEnemy.GetComponentInChildren<Enemy>();
                SpawnedEnemy.Initialize(Pos);
            }
        }
        else
        {
            Debug.LogWarning($"EnemySpawner: Enemy with ID {EnemyID} does not exist!");
            return null;
        }

        SpawnedEnemy.ID = EnemyID;

        EnemiesAlive.Add(SpawnedEnemy);
        return SpawnedEnemy;
    }

    public static void RemoveEnemy(Enemy enemy)
    {
        EnemiesAlive.Remove(enemy);
        EnemyObjectPools[enemy.ID].Enqueue(enemy);
        enemy.gameObject.SetActive(false);
       // Debug.Log("Removed Enemy "+enemy.ID);
    }


}
