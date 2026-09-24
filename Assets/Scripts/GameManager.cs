using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int addEnemyHealth = 0;
    public float addEnemySpeed = 0;
    public int addEasyEnemy = 0;
    public int addHardEnemy = 0;
    public int currentWaypoit = 0;
    public bool waveActive = false;
    public List<Enemy> enemyWaypointIndexes = new List<Enemy>();

    private void Awake()
    {
        Enemy.currentWaypoitLocation += CurrentWaypointOfEnemys;
        Projectile.Check += checkcurrentIndexOfEnemys;
        if (instance == null)
            instance = this;
        else
            Destroy(instance);



    }
    private void Update()
    {
        if(enemyWaypointIndexes.Count < 1)
        {
            waveActive = false;
        }
        else
        {
            waveActive = true;
        }
    }
    private void OnDisable()
    {
        Enemy.currentWaypoitLocation -= CurrentWaypointOfEnemys;
        Projectile.Check -= checkcurrentIndexOfEnemys;
    }
    void CurrentWaypointOfEnemys(int currentindex) // get waypoint index of the enemy that has goten the fardest
    {
        if (currentWaypoit >= currentindex)
        {
            return;
        }
        else
        {
            currentWaypoit = currentindex;
        }
    }
    void checkcurrentIndexOfEnemys()
    {
        int temp = 0;
        foreach (Enemy enemy in enemyWaypointIndexes)
        {
            if (enemy.currentWayPoint > temp)
            {
                temp = enemy.currentWayPoint;

            }
        }
        currentWaypoit = temp;
    }


}
