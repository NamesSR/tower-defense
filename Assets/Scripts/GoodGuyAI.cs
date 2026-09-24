using NUnit.Framework;
using System;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;
public class GoodGuyAI : MonoBehaviour
{
    public List<Vector3Int> posses = new List<Vector3Int>();
    public Transform[] wayPoints = new Transform[] { };
    public GameObject archerprefab;
    public GameObject mageprefab;
    public List<GameObject> towesPlaced = new List<GameObject>();

   

    public void Start()
    {

        int s = UnityEngine.Random.Range(0, 2);

        if (s == 0)
        {
            GameObject sd1 = Instantiate(archerprefab, posses[0], Quaternion.identity);
            towesPlaced.Add(sd1);
            posses.RemoveAt(0);

        }
        else
        {
            GameObject sd2 = Instantiate(mageprefab, posses[0], Quaternion.identity);
            towesPlaced.Add(sd2);
            posses.RemoveAt(0);

        }




        StartCoroutine(PlaceTowerChecker());

        // func for upgrade and cost

    }

    


    IEnumerator PlaceTowerChecker()
    {
        while (true)
        {

            if (!CanPlaceTower())
            {

                yield return new WaitForSeconds(0.5f);

            }
            else
            {
                int posIndex = ClosestPosToWaypoint();

                if (CoinManager.instance.goodGuyCoins >= 10)
                {

                    int s = UnityEngine.Random.Range(0, 2);
                    if (s == 0)
                    {
                        CoinManager.instance.UpdateGoodGuyCoins(-5);
                        GameObject tower = Instantiate(archerprefab, posses[posIndex], Quaternion.identity);
                        towesPlaced.Add(tower);
                        posses.RemoveAt(posIndex);
                    }
                    else
                    {
                        CoinManager.instance.UpdateGoodGuyCoins(-10);
                        GameObject tower = Instantiate(mageprefab, posses[posIndex], Quaternion.identity);
                        towesPlaced.Add(tower);
                        posses.RemoveAt(posIndex);
                    }

                }
                else if (CoinManager.instance.goodGuyCoins >= 5)
                {
                    CoinManager.instance.UpdateGoodGuyCoins(-5);
                    GameObject tower = Instantiate(archerprefab, posses[posIndex], Quaternion.identity);
                    towesPlaced.Add(tower);
                    posses.RemoveAt(posIndex);
                }
                else
                {
                    GameObject tower = Instantiate(archerprefab, posses[posIndex], Quaternion.identity);
                    towesPlaced.Add(tower);
                    posses.RemoveAt(posIndex);
                }
            }



            yield return new WaitForSeconds(0.5f);
        }
    }

    bool CanPlaceTower()
    {
        if (!GameManager.instance.waveActive)
        {
            return false;
        }
        else
        {
            foreach (GameObject t in towesPlaced)
            {
                var s = t.GetComponent<Tower>();
                if (s.enemyInRange)
                {
                    return false;
                }

            }

            if (GameManager.instance.currentWaypoit > 0 && posses.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


    }


    int ClosestPosToWaypoint() // calculats the tower place pos that is the closest to the waypoint
    {
        Transform wayt = wayPoints[GameManager.instance.currentWaypoit];
        float smalestDist = 0f;
        int i = 0;
        int returnIndex = 0;
        foreach (Vector3Int pos in posses)
        {
            float dist = Vector3.Distance(wayt.position, pos);
            if (dist < smalestDist)
            {

                smalestDist = dist;
                returnIndex = i;
            }
            if (i == 0)
            {
                smalestDist = dist;
            }
            i++;

        }


        return returnIndex;
    }

}
