using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class levelsCode : MonoBehaviour
{

    //Level stats
    public int levelNumber = 0;
    public float waveSpeed;
    private float baseWaveSpeed;
    public float waveAcc;
    private int waveIndex = 0;
    public Text waveIndexText;

    //shop stats
    public GameObject[] shop;
    public int shopLength;
    public int shopIndex;
    private bool canOpenShop = true;
    private int lastShopIndex;
    public float scale;

    //enemy stats
    public GameObject[] enemies;
    public int enemiesLength;
    public int baseEnemiesLength;
    public int enemiesIndex;
    public int[] waveEnemyAmount;
    public int[] waveEnemyTypes;
    public GameObject[] bossEnemies;
    public int[] bossWaves;



    //player stats
    //health
    public float maxHealth = 5;
    public Text maxHealthText;
    public float health;
    public float regen = 0;
    public Text regenText;
        //economy
    public int money = 0;
    public Text moneyText;
    public float coinsPer20Seconds;
    public Text coinsPer20SecondsText;
    public int moneyPerRound;
    public Text moneyPerRoundText;
    public int gold = 0;
    public int gems = 0;
    public int emeralds = 0;

    //Private variables
    private bool canGiveMoney = true;
    private bool canHeal = true;

    private int spawned = 0;
    private bool canSpawn = true;

    private int numberOfWaves;

    private bool canContinueWave = true;
    public bool canStartWave = false;
    public int waveSeparationTime;

    private bool bossSpawned = false;
    private int bossWaveIndex = 1;

    private GameObject lastEnemy;

    void Start()
    {
        maxHealth = float.Parse(maxHealthText.text);
        health = maxHealth;
        waveIndexText.text = "1";
        baseWaveSpeed = waveSpeed;
        enemiesLength = enemies.Length;
        baseEnemiesLength = enemiesLength;
        //red = tank, blue = mage, yellow = healer, orange = bomb, green = speed, gray = shield, black = camo, brown = normal, purple = boss.
        shopLength = shop.Length;

        numberOfWaves = waveEnemyAmount.Length;
    }

    void Update() {

        coinsPer20Seconds = float.Parse(coinsPer20SecondsText.text);
        moneyPerRound = int.Parse(moneyPerRoundText.text);
        maxHealth = float.Parse(maxHealthText.text);
        regen = float.Parse(regenText.text) / 100;
        moneyText.text = money + "";
        

        if (canOpenShop) {
            shop[shopIndex].transform.position = new Vector3(565f + 960, -344f+540, 0f);
            canOpenShop = false;
            lastShopIndex = shopIndex;
        }
        if (canOpenShop == false) {
            Vector3 pos = shop[shopIndex].transform.position;
            pos.y += Input.mouseScrollDelta.y * -1 * scale;
            shop[shopIndex].transform.position = pos;
        }

        if (shopIndex == 0) {
            shop[lastShopIndex].transform.position = new Vector3(1321f + 960, -344f+540, 0f);
            canOpenShop = true;
        }



        if (canContinueWave == true && spawned < waveEnemyAmount[waveIndex]) {
            StartCoroutine(Spawnings());
            Debug.Log(spawned);
        } else if (canContinueWave == true && spawned >= waveEnemyAmount[waveIndex] && bossWaves[bossWaveIndex - 1] == waveIndex + 1) {
            if (bossSpawned == false) {
                Debug.Log("boss spawned");
                bossEnemies[bossWaveIndex-1].GetComponent<EnemyCoded>().Spawn();
                bossSpawned = true;
                canContinueWave = false;

            }

            
        } else if (bossSpawned == true && bossEnemies[bossWaveIndex - 1].activeInHierarchy == false) {
            Debug.Log("boss fight finished");
            canStartWave = true;
            bossWaveIndex++;
            if (canStartWave && spawned >= waveEnemyAmount[waveIndex]) {
                StartCoroutine(NewWave());
            }

        } else if (spawned >= waveEnemyAmount[waveIndex] && canContinueWave == true) {
            Debug.Log("wave without boss finished");
            canStartWave = true;
            canContinueWave = false;
            if (canStartWave && spawned >= waveEnemyAmount[waveIndex]) {
                StartCoroutine(NewWave());
            }

        }
      
        StartCoroutine(CoinsPer20Seconds());
        StartCoroutine(Regen());
    }
    private IEnumerator Spawnings() {

        if (canSpawn) {

            enemies[enemiesIndex].GetComponent<EnemyCoded>().Spawn();

            spawned++;
            while (enemies[enemiesIndex].activeInHierarchy == true) {
                enemiesIndex = Random.Range(0, waveEnemyTypes[waveIndex]);
            }



            canSpawn = false;
            yield return new WaitForSeconds(1 / waveSpeed);
            canSpawn = true;
            waveSpeed = waveSpeed + waveAcc;
        }
    }

    private IEnumerator CoinsPer20Seconds() {

        if (canGiveMoney && coinsPer20Seconds != 0) {

            money++;

            canGiveMoney = false;
            yield return new WaitForSeconds(20 / (coinsPer20Seconds));
            canGiveMoney = true;

        }

    }

    private IEnumerator Regen() {
        if (canHeal && regen != 0 && health < maxHealth) {

            health = health + regen;

            canHeal = false;
            yield return new WaitForSeconds(1);
            canHeal = true;
        }
       
    }

    private IEnumerator NewWave() {
        bossSpawned = false;
        spawned = 0;
        canStartWave = false;
        canContinueWave = false;
        Debug.Log("Wave Finished");
        yield return new WaitForSeconds(waveSeparationTime);
        Debug.Log("newWave");
        waveIndex++;
        waveSpeed = baseWaveSpeed;
        waveIndexText.text = waveIndex + 1 + "";
        money = money + moneyPerRound;
        canContinueWave = true;
       
    }
}
