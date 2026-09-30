using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class TowerUpgradeStage
{
    public float range;
    public float fireRate;
    public Sprite sprite;
    public int price;
}


public class Tower : MonoBehaviour
{
    public float range = 3f;
    public float fireRate = 1f;
    public GameObject projectilePrefab;
    public Transform firePoint;

    public TowerUpgradeStage[] upgradeStages;
    public int upgradeStage = 0;

    public SpriteRenderer sr;
    public int towerprice = 1;

    public bool enemyInRange = false;

    private float fireCooldown = 0f;

    void Start()
    {
        //sr.GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        fireCooldown -= Time.deltaTime;
        Enemy target = FindBestTarget();

        if (target != null && fireCooldown <= 0f)
        {
            enemyInRange = true;
            Shoot(target);
            fireCooldown = 1f / fireRate;
        }
        else
        {
            enemyInRange = false;
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
            if (dist <= range)
            {
                if (e.currentWayPoint > bestprogress)
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

    public bool CanUpgrade()
    {
        if (upgradeStage >= upgradeStages.Length) return false;

        if (CoinManager.instance.goodGuyCoins < upgradeStages[upgradeStage].price) return false;


        return true;
    }

    public void Upgrade()
    {
        TowerUpgradeStage currentUpgradeStage = upgradeStages[upgradeStage];

        range = currentUpgradeStage.range;
        fireRate = currentUpgradeStage.fireRate;
        sr.sprite = currentUpgradeStage.sprite;
        CoinManager.instance.UpdateGoodGuyCoins(-currentUpgradeStage.price);
        upgradeStage += 1;
    }


}
