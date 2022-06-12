using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

public class TowerTargeting 
{

    public static Enemy GetTarget(TowerBehaviour CurrentTower,ETargetType targettype)
    {
        Collider[] EnemiesinRange = Physics.OverlapSphere(CurrentTower.transform.position, CurrentTower.Range, CurrentTower.EnemiesMask);

        Debug.Log($"Enemies in Range {EnemiesinRange.Length} for tower {CurrentTower.name} (Range: {CurrentTower.Range})");

        if (EnemiesinRange.Length > 0)
        {
            NativeArray<EnemyHolder> EnemiesToCalc = new NativeArray<EnemyHolder>(EnemiesinRange.Length, Allocator.TempJob);
            NativeArray<int> EnemyToIndex = new NativeArray<int>(new int[] { -1 }, Allocator.TempJob);

            int EnemyIndexReturn = 0;

            for (int i = 0; i < EnemiesToCalc.Length; i++)
            {
                Enemy e = EnemiesinRange[i].GetComponent<Enemy>();
                int enemyIndexInList = EnemySpawner.EnemiesAlive.FindIndex(x => x == e);

                EnemiesToCalc[i] = new EnemyHolder(e.transform.position, e.target, e.health, enemyIndexInList);
            }

            SearchForEnemy EnemySearchJob = new SearchForEnemy
            {
                _EnemiesToCalc = EnemiesToCalc,
                _EnemyToIndex = EnemyToIndex,
                targetingType = (int)targettype,
                TowersPosition = CurrentTower.transform.position,

            };

            switch (targettype)
            {
                case ETargetType.First:
                    EnemySearchJob.CompareValue = Mathf.Infinity;
                    break;
                case ETargetType.Last:
                    EnemySearchJob.CompareValue = Mathf.NegativeInfinity;
                    break;
                case ETargetType.Close:
                    goto case ETargetType.First;
            }

            JobHandle dependency = new JobHandle();
            JobHandle SearchJobHandle = EnemySearchJob.Schedule(EnemiesToCalc.Length, dependency);
            SearchJobHandle.Complete();

            //Debug.Log("enemy Return index: " + EnemyToIndex[0]);

            if (EnemyToIndex[0] == -1)
            {
                Debug.LogWarning("Some unexpected Error Occurs");
                EnemiesToCalc.Dispose();
                EnemyToIndex.Dispose();
                return null;
            }
            EnemyIndexReturn = EnemiesToCalc[EnemyToIndex[0]].EnemyIndex;

            EnemiesToCalc.Dispose();
            EnemyToIndex.Dispose();


            return EnemySpawner.EnemiesAlive[EnemyIndexReturn];
        }

        return null;
    }

    struct EnemyHolder
    {
        public EnemyHolder(Vector3 pos, Vector3 target, float hp, int enemyIx)
        {
            EnemyPosition = pos;
            targetPosition = target;
            Health = hp;
            EnemyIndex = enemyIx;
        }

        public Vector3 targetPosition;
        public Vector3 EnemyPosition;
        public float Health;
        public int EnemyIndex;
    }

    struct SearchForEnemy : IJobFor
    {
        public NativeArray<EnemyHolder> _EnemiesToCalc;
        public NativeArray<int> _EnemyToIndex;

        public Vector3 TowersPosition;
        public float CompareValue;
        public int targetingType;

        void IJobFor.Execute(int index)
        {
            Debug.Log("Start Job for Enmy " + index);
            float currdistance = 0;
            float distanceToenemy = 0;
            switch (targetingType)
            {
                case 0: //first
                    currdistance = GetDistancetotarget(_EnemiesToCalc[index]);
                    if (currdistance < CompareValue)
                    {
                        _EnemyToIndex[0] = index;
                        CompareValue = currdistance;
                    }
                    break;
                case 1: //Last
                    currdistance = GetDistancetotarget(_EnemiesToCalc[index]);
                    if (currdistance > CompareValue)
                    {
                        _EnemyToIndex[0] = index;
                        CompareValue = currdistance;
                    }
                    break;
                case 2: //close
                    distanceToenemy = Vector3.Distance(TowersPosition,_EnemiesToCalc[index].EnemyPosition);
                    if (distanceToenemy < CompareValue)
                    {
                        _EnemyToIndex[0] = index;
                        CompareValue = distanceToenemy;
                    }
                    break;
            }
        }

        private float GetDistancetotarget(EnemyHolder EnemyToEvaluate)
        {
            float finalDistance = Vector3.Distance(EnemyToEvaluate.EnemyPosition, EnemyToEvaluate.targetPosition);
            Debug.Log($"Distacne to Target for {EnemyToEvaluate.EnemyIndex} :" + finalDistance);
            return finalDistance;
        }
    }

}
