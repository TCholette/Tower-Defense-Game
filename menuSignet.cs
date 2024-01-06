using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menuSignet : MonoBehaviour
{


    public GameObject Player;

    public int index;

    private Camera mainCamera;

 

    private void Start() {
        mainCamera = Camera.main;

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)) { 
        Vector3 mousePos = Input.mousePosition;
   

            if (mousePos.x <= (transform.position.x + 100) && mousePos.x >= (transform.position.x - 100) && mousePos.y <= (transform.position.y + 50) && mousePos.y >= (transform.position.y - 50)) {

              
                Player.GetComponent<levelsCode>().shopIndex = index;
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Player.GetComponent<levelsCode>().shopIndex = 0;
        }

    }
}
