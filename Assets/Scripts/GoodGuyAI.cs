using NUnit.Framework;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;
public class GoodGuyAI : MonoBehaviour
{
    public List<Vector3> posses = new List<Vector3>();
    public Transform[] wayPoints = new Transform[] { };
    public GameObject archerprefab;
    public GameObject mageprefab;
    public List<GameObject> towesPlaced = new List<GameObject>();

    public Tilemap placementMap;

    Tower emtyTower;




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
       Debug.Log("AUTOMATIC COMPILE TEST3");
            if (!CanPlaceTower())
            {// test
           
                Debug.Log("test");
                yield return new WaitForSeconds(0.5f);

            }
            else
            {
                int posIndex = ClosestPosToWaypoint();

                
                Vector3 pos = posses[posIndex];
               
                Debug.Log("test");
                Debug.Log("test2");
                Debug.Log("AUTOMATIC COMPILE TEST");
                Debug.Log("AUTOMATIC COMPILE TEST321");



                if (CoinManager.instance.goodGuyCoins >= 10)
                {
                    int s = UnityEngine.Random.Range(0, 2);
                    if (s == 0)
                    {
                        CoinManager.instance.UpdateGoodGuyCoins(-5);
                        GameObject tower = Instantiate(archerprefab, pos, Quaternion.identity);
                        towesPlaced.Add(tower);
                        posses.RemoveAt(posIndex);
                    }
                    else
                    {
                        CoinManager.instance.UpdateGoodGuyCoins(-10);
                        GameObject tower = Instantiate(mageprefab, pos, Quaternion.identity);
                        towesPlaced.Add(tower);
                        posses.RemoveAt(posIndex);
                    }

                }
                else if (CoinManager.instance.goodGuyCoins >= 5)
                {
                    CoinManager.instance.UpdateGoodGuyCoins(-5);
                    GameObject tower = Instantiate(archerprefab, pos, Quaternion.identity);
                    towesPlaced.Add(tower);
                    posses.RemoveAt(posIndex);
                }
                else
                {
                    GameObject tower = Instantiate(archerprefab, pos, Quaternion.identity);
                    towesPlaced.Add(tower);
                    posses.RemoveAt(posIndex);
                }
            }



            yield return new WaitForSeconds(0.5f);
        }
    }


    IEnumerator UpgadeTowerChecker()
    {
        while (true)
        {
            
            if (towesPlaced.Count > 0)
            {

                (bool, Tower) towers = CanUpgradeTower();
                if (towers.Item1)
                {
                    towers.Item2.Upgrade();
                }
                else
                {
                    yield return new WaitForSeconds(0.5f);
                }

            }
            else
            {
                yield return new WaitForSeconds(0.5f);
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

    (bool, Tower) CanUpgradeTower()
    {
        if (towesPlaced.Count > 0)
        {


            foreach (GameObject tower in towesPlaced)
            {
                if (tower.GetComponent<Tower>().CanUpgrade())
                {
                    return (true, tower.GetComponent<Tower>());
                }


            }

            return (false, emtyTower);
        }

        return (false, emtyTower);
    }



    int ClosestPosToWaypoint() // calculats the tower place pos that is the closest to the waypoint
    {
        Transform wayt = wayPoints[GameManager.instance.currentWaypoit];
        float smalestDist = 0f;
        int i = 0;
        int returnIndex = 0;
        foreach (Vector3 pos in posses)
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
