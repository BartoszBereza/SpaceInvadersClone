using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpaceShipsSet : MonoBehaviour {

    [HideInInspector]
    public int enemiesLeft;

    public GameObject[] enemies;
    public int enemiesNumberInRow;
    public float distanceBetweenEnemies;
    public GameObject spaceDeer;
    public float spaceDeerSpawnMaxTime;
    public float spaceDeerSpawnMinTime;
    public Vector3 velocity;
    public float gameOverOnY;

    private float spaceDeerSpawnNextTime;
    private SpaceDeer spaceDeerComponent;
    private GameObject[, ] enemiesArray;
    private float leftEdgeSet;
    private GameObject spaceDeerObject;
    private Vector3 spaceDeerSpawn = new Vector3 (20.0f, 4.0f, 0.0f);

    void Start () {
        CreateSpaceDeer ();
        CreateEnemiesArray ();
        SetupFiringShips ();
    }

    void Update () {
        SpaceDeerTimeCheck ();
        Move ();
    }

    void Move () {
        transform.position += velocity;
    }

    void CreateSpaceDeer () {
        spaceDeerObject = Instantiate<GameObject> (spaceDeer);
        spaceDeerObject.SetActive (false);
        spaceDeerObject.transform.SetParent (transform);
        spaceDeerComponent = spaceDeerObject.GetComponent<SpaceDeer> ();
        spaceDeerSpawnNextTime = Time.time + Random.Range (spaceDeerSpawnMinTime, spaceDeerSpawnMaxTime);
    }

    void SpaceDeerTimeCheck () {
        if (Time.time > spaceDeerSpawnNextTime) {
            spaceDeerSpawnNextTime = Time.time + Random.Range (spaceDeerSpawnMinTime, spaceDeerSpawnMaxTime);
            SpawnSpaceDeer ();
        }
    }

    void SpawnSpaceDeer () {
        spaceDeerObject.transform.position = spaceDeerSpawn;
        spaceDeerObject.SetActive (true);
        spaceDeerComponent.ResetScore ();
        spaceDeerComponent.SetDecreaseTime (Time.time);
    }

    void CreateEnemiesArray () {
        enemiesLeft = enemies.Length * enemiesNumberInRow;
        enemiesArray = new GameObject[enemies.Length, enemiesNumberInRow];
        leftEdgeSet = transform.position.y - enemiesNumberInRow / 2;
        for (int i = 0; i < enemies.Length; ++i) {
            CreateEnemiesRow (enemies[i], enemiesNumberInRow, i);
        }
    }

    void CreateEnemiesRow (GameObject enemy, int enemiesNumber, int rowNumber) {
        for (int i = 0; i < enemiesNumber; ++i) {
            var enemyObject = Instantiate<GameObject> (enemy);
            enemiesArray[rowNumber, i] = enemyObject;
            enemyObject.transform.SetParent (transform);
            enemyObject.transform.position = new Vector3 (
                (float) i + leftEdgeSet,
                rowNumber * distanceBetweenEnemies,
                0.0f
            );
            var enemyComponent = enemyObject.GetComponent<Enemy> ();
            enemyComponent.positionRow = rowNumber;
            enemyComponent.positionCol = i;
        }
    }

    public void DeleteEnemy (GameObject enemy) {
        if (enemy.GetComponent<Enemy> () != null) {
            enemiesLeft--;
        }
        enemy.SetActive (false);
        SetupFiringShips ();
    }

    public void SetupFiringShips () {
        for (int col = 0; col < enemiesArray.GetLength (1); ++col) {
            for (int row = 0; row < enemiesArray.GetLength (0); ++row) {
                var enemyObject = enemiesArray[row, col];
                var enemyComponent = enemyObject.GetComponent<Enemy> ();
                enemyComponent.isFiring = true;
                for (int rowBelow = 0; rowBelow < row; ++rowBelow) {
                    if (enemiesArray[rowBelow, col].gameObject.activeSelf) {
                        enemyComponent.isFiring = false;
                        break;
                    }
                }
            }
        }
    }
}