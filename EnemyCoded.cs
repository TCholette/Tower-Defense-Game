using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCoded : MonoBehaviour {
    public GameObject Player;

    [SerializeField] private float speed = 2f;

    public float life;
    public float maxLife;

    private bool moving = true;

    private Vector3 position;

    public int moneyDrop;

    private bool canHitPlayer = true;




    private void Update() {

        if (Vector2.Distance(Player.transform.position, transform.position) < 0.5) {
            moving = false;
        }


        StartCoroutine(HitPlayer());

        if (moving) {
            transform.position = Vector2.MoveTowards(transform.position, Player.transform.position, Time.deltaTime * speed);
        }
        

        if (life <= 0) {
            RenderInert();
            Player.GetComponent<levelsCode>().money = Player.GetComponent<levelsCode>().money + moneyDrop;
            position = gameObject.transform.position;
            life = maxLife;
            StopExisting();

        }
    }



    private void Start() {
       
        maxLife = life;

    }

    private IEnumerator HitPlayer() {

        if (Vector2.Distance(Player.transform.position, transform.position) < 0.5f && Player.GetComponent<levelsCode>().health > 0 && canHitPlayer) {
            canHitPlayer = false;

            yield return new WaitForSeconds(1f);
            Player.GetComponent<levelsCode>().health--;
            canHitPlayer = true;

        }
      
    }

    public void Disappear(string action) {

        if (action == "break") {

        
            RenderInert();

            position = gameObject.transform.position;
      
            StopExisting();



        }


        if (action == "getHit") {
        
            life = life - Player.GetComponent<Shooting>().damage;
            
        }
        
    }

   

    public void Spawn() {
        Appearance();
        moving = true;
        int rand = Random.Range(1, 100);
        gameObject.transform.position = Player.transform.position + new Vector3(10f * Mathf.Cos(Mathf.PI * 200 / rand), 10f * Mathf.Sin(Mathf.PI *200 / rand), 0f);
        gameObject.transform.eulerAngles = new Vector3(gameObject.transform.eulerAngles.x, gameObject.transform.eulerAngles.y, (360 *100 / rand)+90);
    }

    public void Appearance() {
        this.gameObject.SetActive(true);
        if (gameObject.GetComponent<CircleCollider2D>() != null) {
            gameObject.GetComponent<CircleCollider2D>().enabled = true;
        }
        if (gameObject.GetComponent<BoxCollider2D>() != null) {
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
        }
        if (gameObject.GetComponent<CapsuleCollider2D>() != null) {
            gameObject.GetComponent<CapsuleCollider2D>().enabled = true;
        }
        if (gameObject.GetComponent<Rigidbody2D>() != null) {
            gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        }

        
    }

    

    private void RenderInert() {
        if (gameObject.GetComponent<CircleCollider2D>() != null) {
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
        }
        if (gameObject.GetComponent<BoxCollider2D>() != null) {
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
        if (gameObject.GetComponent<CapsuleCollider2D>() != null) {
            gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
        }
        if (gameObject.GetComponent<Rigidbody2D>() != null) {
            gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        }
    }

    public void StopExisting() {
        gameObject.SetActive(false);

    }

}



