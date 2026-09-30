using UnityEngine;
using TMPro;
public class CoinManager : MonoBehaviour
{
    public static CoinManager instance;

    public int goodGuyCoins;
    public int badGuyCoins;
    public TextMeshProUGUI CoinTxt;

    private void Awake()
    {
        instance = this;
      
    }
    
    void Start()
    {
        UpdateGoodGuyCoins(200);
        UpdateBadGuyCoins(0);
    }

    public void UpdateGoodGuyCoins(int changeAmount)
    {
        goodGuyCoins += changeAmount;
        CoinTxt.text = goodGuyCoins.ToString();
    }
    public void UpdateBadGuyCoins(int changeAmount)
    {
        badGuyCoins += changeAmount;
    }
}
