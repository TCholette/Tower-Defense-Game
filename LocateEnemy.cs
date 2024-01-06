using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocateEnemy : MonoBehaviour
{

    public GameObject target;

    void Start()
    {
        
    }


    void Update()
    {
        FindClosestEnemy();
    }

    private void FindClosestEnemy() {
        target = null;
        float closestDistance = gameObject.GetComponent<Shooting>().range;
        for (int targetedIndex = 0; targetedIndex < gameObject.GetComponent<levelsCode>().baseEnemiesLength; targetedIndex++) {
            float distance = Vector3.Distance(gameObject.GetComponent<levelsCode>().enemies[targetedIndex].transform.position, gameObject.transform.position);
            if (distance < closestDistance && gameObject.GetComponent<levelsCode>().enemies[targetedIndex].activeInHierarchy == true) {
                closestDistance = distance;
                target = gameObject.GetComponent<levelsCode>().enemies[targetedIndex];

            }
        }
        
        
    }
}
