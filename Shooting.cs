using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shooting : MonoBehaviour {
    public Text damageText;
    public float damage;
    public Text speedText;
    public float speed;
    public Text rangeText;
    public float range;
    public GameObject rangeZone;
    public Text critChanceText;
    public float critChance;
    public Text critDamageText;
    public float critDamage;
    public Text doubleChanceText;
    public float doubleChance;
    public Text bounceChanceText;
    public float bounceChance;
    public Text multiChanceText;
    public float multiChance;
    public Text multiNumberText;
    public float multiNumber;
    public float shotSpeed;

    public GameObject[] pellet;
    private int pelletLength;
    private int pelletIndex;

    private bool canShoot = true;



    private void Start() {
        pelletLength = pellet.Length;

    }

    void Update() {

        damage = float.Parse(damageText.text);
        speed = float.Parse(speedText.text)/10;
        range = float.Parse(rangeText.text)/10;
        critChance = float.Parse(critChanceText.text)/100;
        critDamage = float.Parse(critDamageText.text)/100 * damage + damage;
        doubleChance = float.Parse(doubleChanceText.text); //1% chance each
        bounceChance = float.Parse(bounceChanceText.text)/100;
        multiChance = float.Parse(multiChanceText.text)/100;
        multiNumber = float.Parse(multiNumberText.text);


        if (canShoot && gameObject.GetComponent<LocateEnemy>().target != null && Random.Range(1, 100) <= doubleChance) {
            StartCoroutine(DoubleShoot());
        }
        else if (canShoot && gameObject.GetComponent<LocateEnemy>().target != null) {
            StartCoroutine(Shoot());
            
        } 
        rangeZone.transform.localScale = new Vector2(range * 2, range*2);



    }

    private IEnumerator DoubleShoot() {
        Appearance(pellet[pelletIndex]);

        while (pellet[pelletIndex].activeInHierarchy == true) {
            pelletIndex = Random.Range(0, pelletLength - 1);
        }
        canShoot = false;
        yield return new WaitForSeconds(0.2f);

        Appearance(pellet[pelletIndex]);

        while (pellet[pelletIndex].activeInHierarchy == true) {
            pelletIndex = Random.Range(0, pelletLength - 1);
        }
        yield return new WaitForSeconds(( 1 / speed ) - 0.2f);
        canShoot = true;
    }
    private IEnumerator Shoot() {

            Appearance(pellet[pelletIndex]);

            while (pellet[pelletIndex].activeInHierarchy == true) {
                pelletIndex = Random.Range(0, pelletLength-1);
            }

            canShoot = false;
            yield return new WaitForSeconds(1 / speed);
            canShoot = true;
        





    }

    public void Appearance(GameObject appeared) {
        appeared.SetActive(true);
        if (appeared.GetComponent<CircleCollider2D>() != null) {
            appeared.GetComponent<CircleCollider2D>().enabled = true;
        }
        if (appeared.GetComponent<BoxCollider2D>() != null) {
            appeared.GetComponent<BoxCollider2D>().enabled = true;
        }
        if (appeared.GetComponent<CapsuleCollider2D>() != null) {
            appeared.GetComponent<CapsuleCollider2D>().enabled = true;
        }
        if (appeared.GetComponent<Rigidbody2D>() != null) {
            appeared.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        }

    }

    



        
}
