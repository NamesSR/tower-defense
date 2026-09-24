using Unity.VisualScripting;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public float range = 3f;
    public float fireRate = 1f;
    public GameObject projectilePrefab;
    public Transform firePoint;

    public int towerprice = 1;
    public bool enemyInRange = false;
    private float fireCooldown = 0f;
    void Update()
    {
        fireCooldown -= Time.deltaTime;
        Enemy target = FindBestTarget();

        if(target != null && fireCooldown <= 0f)
        {
            enemyInRange = true;
            Shoot(target);
            fireCooldown = 1f / fireRate;
        }
        else 
        {
            enemyInRange= false;
        }
    }

    Enemy FindBestTarget()
    {
        Enemy[] enemies = GameObject.FindObjectsByType<Enemy>();

        Enemy best = null;
        float bestprogress = -1f;

        foreach (Enemy e in enemies)
        {
            float dist = Vector2.Distance(transform.position, e.transform.position);
            if(dist <= range)
            {
                if(e.currentWayPoint > bestprogress)
                {
                    bestprogress = e.currentWayPoint;
                    best = e;
                }
            }
        }
        return best;
    }

    void Shoot(Enemy target)
    {
        GameObject p = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        Projectile pr = p.GetComponent<Projectile>();
        pr.target = target.transform;
    }
}
