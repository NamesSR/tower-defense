using UnityEngine;

public class BadGuyUpgrader : MonoBehaviour
{
    public int cost = 5;
    public float speed = 0;
    public int health = 0;
    public int easyEnemy = 0;
    public int hardEnemy = 0;


    public void upgrade()
    {
        if (cost <= CoinManager.instance.badGuyCoins)
        {
            CoinManager.instance.UpdateBadGuyCoins(-cost);
            GameManager.instance.addEnemySpeed += speed;
            GameManager.instance.addEnemyHealth += health;
            GameManager.instance.addEasyEnemy += easyEnemy;
            GameManager.instance.addHardEnemy += hardEnemy;


        }

    }
}
