using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public static event Action Check;
    public float speed = 8f;
    public Transform target;
    void Update()
    {
        if(target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = ( target.position - transform.position).normalized;
        transform.position += dir * speed * Time.deltaTime;
        float angle = Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0,0,angle - 90);
        if(Vector2.Distance(transform.position, target.position) < 0.15f)
        {
            Enemy e = target.GetComponent<Enemy>();
            e.health -= 1;
            if(e.health <= 0)
            {
                CoinManager.instance.UpdateGoodGuyCoins(1);
                
                GameManager.instance.enemyWaypointIndexes.Remove(e);
                Check?.Invoke();
                Destroy(target.gameObject);
            }
            Destroy(gameObject);
        }  
    }
}
