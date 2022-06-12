using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TDGameLoop : MonoBehaviour
{
    private static Queue<int> EnemyIDsToSpawn;
    private static Queue<Enemy> EnemysToRemove;

    public static float TargetDistances;
    public static List<TowerBehaviour> TowersInGame;

    public Transform TargetLocation;
    public Transform[] SpawnLocations;
    private int SpawnLocationCount;
    private int LastSpawnLocation;

    public bool doGameLoop = true;

    // Start is called before the first frame update
    void Start()
    {
        EnemySpawner.Initialize();
        EnemyIDsToSpawn = new Queue<int>();
        EnemysToRemove = new Queue<Enemy>();
        TowersInGame = new List<TowerBehaviour>();

        SpawnLocationCount = SpawnLocations.Length;
        LastSpawnLocation = 0;

        StartCoroutine(GameLoop());
        //Just Testing
        InvokeRepeating("SummonTest", 0f, 2f);
        InvokeRepeating("RemoveTest", 8.1f, 2f);
    }

    void RemoveTest()
    {
        if(EnemySpawner.EnemiesAlive.Count > 0)
        {
            EnqueueEnemyToRemove(EnemySpawner.EnemiesAlive[0]);
        }
    }

    void SummonTest()
    {
        EnqueueEnemyToSpawn(1);
    }

    IEnumerator GameLoop()
    {
        while (doGameLoop)
        {
            Debug.Log("Enter GamerLoop");
            // Spawn enemies

            if(EnemyIDsToSpawn.Count>0)
            {
                for( int i=0; i < EnemyIDsToSpawn.Count; i++)
                {
                    EnemySpawner.SpawnEnemy(EnemyIDsToSpawn.Dequeue(), GetNextSpawnPsoition());
                }
            }

            // Spawn Towers

            // Move Enemies
            if(EnemySpawner.EnemiesAlive.Count>0)
            {
                for(int i =0; i<EnemySpawner.EnemiesAlive.Count;i++)
                {
                    EnemySpawner.EnemiesAlive[i].agent.SetDestination(TargetLocation.position);
                    EnemySpawner.EnemiesAlive[i].target = TargetLocation.position;

                    if(Vector3.Distance(EnemySpawner.EnemiesAlive[i].transform.position,TargetLocation.position)<0.5f)
                    {
                        Debug.Log("reached: " + EnemySpawner.EnemiesAlive[i].transform.position);
                        //Deal Damage
                    }
                }

            }


            // Tick Towers

            foreach(TowerBehaviour tower in TowersInGame)
            {
                tower.currentTarget = TowerTargeting.GetTarget(tower, tower.targetType);
                tower.Tick();
            }

            // Apply Effects

            // Damage Enemies
            if (EnemySpawner.EnemiesAlive.Count > 0)
            {
                for(int i = 0; i < EnemySpawner.EnemiesAlive.Count; i++)
                {
                    if (EnemySpawner.EnemiesAlive[i].health == 0)
                        EnqueueEnemyToRemove(EnemySpawner.EnemiesAlive[i]);
                }
            }


                // remove Enemies
                if (EnemysToRemove.Count > 0)
            {
                for (int i=0; i< EnemysToRemove.Count; i++)
                {
                    EnemySpawner.RemoveEnemy(EnemysToRemove.Dequeue());
                }
            }

            yield return null;
        }
    }

    public static void EnqueueEnemyToSpawn(int EnemyID)
    {
        EnemyIDsToSpawn.Enqueue(EnemyID);
        //Debug.Log($"Enqueued Enemey ID {EnemyID} to spawn");
    }

    public static void EnqueueEnemyToRemove(Enemy enemyToRemove)
    {
        EnemysToRemove.Enqueue(enemyToRemove);
    }
 

    private Vector3 GetNextSpawnPsoition()
    {
        Vector3 Pos = SpawnLocations[LastSpawnLocation%SpawnLocationCount].position;
        //Debug.Log("Spawnlocation: " + LastSpawnLocation % SpawnLocationCount + " pos:" +Pos);
        LastSpawnLocation++;
        return Pos;
    }
}
