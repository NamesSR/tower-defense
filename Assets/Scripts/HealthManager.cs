using UnityEngine;
using TMPro;
using Unity.VectorGraphics;
using UnityEngine.SceneManagement;
public class HealthManager : MonoBehaviour
{
    public static HealthManager Instance;
    public int health = 100;
    public TextMeshProUGUI healthText;
    private void Awake()
    {
        Instance = this;
        UpDatehealth(0);
    }

    public void UpDatehealth(int changeAmount)
    {
       health += changeAmount;
       healthText.text = health.ToString();
        if(health <= 0)
        {
            Debug.Log("You Dieded");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
