using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static event Action<int> currentWaypoitLocation;
    public float speed = 2f;
    public int health = 1;
    public Transform[] wayPoints;

    public int currentWayPoint = 0;

    private void Awake()
    {
        speed  += GameManager.instance.addEnemySpeed;
        health += GameManager.instance.addEnemyHealth;
        GameManager.instance.enemyWaypointIndexes.Add(this.gameObject.GetComponent<Enemy>());
    }
    void Update()
    {
      if(wayPoints == null || wayPoints.Length == 0) return;
      
      Transform target = wayPoints[currentWayPoint];
        Vector3 dir = (target.position - transform.position).normalized;

        transform.position += dir * speed * Time.deltaTime;

        if(Vector3.Distance(transform.position, target.position) < 0.05f)
        {
            currentWayPoint++;
            currentWaypoitLocation?.Invoke(currentWayPoint);
            CoinManager.instance.UpdateBadGuyCoins(1);

            if (currentWayPoint >= wayPoints.Length)
            {
                HealthManager.Instance.UpDatehealth(-health);
                GameManager.instance.enemyWaypointIndexes.Remove(this.gameObject.GetComponent<Enemy>());
                Destroy(gameObject);
            }
        }
    }
}
