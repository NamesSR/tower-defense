using UnityEngine;

public class towerSelectionUI : MonoBehaviour
{
    public static GameObject SelectedTowerPrefab;

    public void SelectTower(GameObject TowerPrefab)
    {

        if(TowerPrefab == SelectedTowerPrefab)
        {
            SelectedTowerPrefab = null;
            return;
        }
        if (TowerPrefab.GetComponent<Tower>().towerprice <= CoinManager.instance.coins)
        {
            SelectedTowerPrefab = TowerPrefab;
        }
        
    }
}
