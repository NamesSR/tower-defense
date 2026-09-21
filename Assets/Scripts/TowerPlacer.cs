using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;
using UnityEngine.EventSystems;
public class TowerPlacer : MonoBehaviour
{
    public Tilemap placementMap;
    public Tilemap nonPlacebleMap;

    public GameObject ghostPrefab;

    private HashSet<Vector3Int> occupiedTiles = new HashSet<Vector3Int>();
    private GameObject ghostInstance;

    // Update is called once per frame
    void Update()
    {
        HandlePlacementHover();
        HandlePlacementClick();
    }

    void HandlePlacementHover()
    {
        if (towerSelectionUI.SelectedTowerPrefab == null)
        {
            if (ghostInstance != null)
                Destroy(ghostInstance);
            return;

        }
        if (ghostInstance == null)
            ghostInstance = Instantiate(ghostPrefab);




        ghostInstance.GetComponent<SpriteRenderer>().sprite = towerSelectionUI.SelectedTowerPrefab.GetComponent<SpriteRenderer>().sprite;

        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0;

        Vector3Int cellpos = placementMap.WorldToCell(mouseWorldPos);

        Vector3 worldCenter = placementMap.GetCellCenterWorld(cellpos);
        worldCenter.z = 0;
        ghostInstance.transform.position = worldCenter + new Vector3(0, placementMap.cellSize.y * 0.25f);

        bool valid = placementMap.HasTile(cellpos) && !occupiedTiles.Contains(cellpos);

        ghostInstance.GetComponent<GhostTower>().SetValid(valid);


    }
    void HandlePlacementClick()
    {
        if (!Input.GetMouseButtonDown(0)) return;
        if (towerSelectionUI.SelectedTowerPrefab == null) return;

        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
            return;

        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0;

        Vector3Int cellpos = placementMap.WorldToCell(mouseWorldPos);
        if (!placementMap.HasTile(cellpos)) return;
        if (occupiedTiles.Contains(cellpos)) return;

        Instantiate(towerSelectionUI.SelectedTowerPrefab, ghostInstance.transform.position, Quaternion.identity);

        CoinManager.instance.UpdateCoins(-towerSelectionUI.SelectedTowerPrefab.GetComponent<Tower>().towerprice);

        towerSelectionUI.SelectedTowerPrefab = null;

        occupiedTiles.Add(cellpos);
    }
}
