using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToEnemy : MonoBehaviour {

    public bool activate;
    public GameObject Player;

    private void Start() {

    }

    // Update is called once per frame
    void Update() {
        if (gameObject.activeInHierarchy && Player.GetComponent<LocateEnemy>().target != null) {
            if (gameObject.activeInHierarchy) {
                gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, Player.GetComponent<LocateEnemy>().target.transform.position, Time.deltaTime * Player.GetComponent<Shooting>().shotSpeed);
       
            }


            if (Vector2.Distance(Player.GetComponent<LocateEnemy>().target.transform.position, gameObject.transform.position) < .1f) {
                Player.GetComponent<LocateEnemy>().target.GetComponent<EnemyCoded>().Disappear("getHit");

                gameObject.transform.position = Player.transform.position;
                gameObject.SetActive(false);
            }

           
        }
        if (Player.GetComponent<LocateEnemy>().target == null || Player.GetComponent<LocateEnemy>().target.activeInHierarchy == false) {
            gameObject.transform.position = Player.transform.position;
            gameObject.SetActive(false);

        }

    }
    
}

