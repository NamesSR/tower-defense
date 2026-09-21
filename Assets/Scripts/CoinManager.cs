using UnityEngine;
using TMPro;
public class CoinManager : MonoBehaviour
{
    public static CoinManager instance;

    public int coins;
    public TextMeshProUGUI CoinTxt;

    private void Awake()
    {
        instance = this;
        UpdateCoins(0);
    }

    public void UpdateCoins(int changeAmount)
    {
        coins += changeAmount;
        CoinTxt.text = coins.ToString();
    }
}
