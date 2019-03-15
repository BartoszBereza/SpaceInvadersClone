using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : NPCSpaceShip {

    public GameObject bullet;
    public int bulletDamage;
    public Vector3 bulletVelocity;
    public float minFireRate;
    public float maxFireRate;
    public float directionChangeRate;
    public bool isFiring;
    public int positionRow;
    public int positionCol;

    private float nextDirectionChange;
    private float nextFire;

    void Start () {
        NPCSpaceShipsSet = transform.parent.GetComponent<NPCSpaceShipsSet> ();
        nextDirectionChange = Time.time + directionChangeRate / 2;
        nextFire = Time.time + Random.Range (minFireRate, maxFireRate);
    }

    void Update () {
        if (isFiring) Fire ();
        Move ();
    }

    void OnTriggerEnter2D (Collider2D other) {
        Bullet otherBulletComponent = other.GetComponent<Bullet> ();
        if (IsPlayerSource (otherBulletComponent)) {
            DealDamage (otherBulletComponent);
            if (IsDestroyed ()) {
                NPCSpaceShipsSet.DeleteEnemy (gameObject);
                CreateExplosion ();
                ScorePlayer (otherBulletComponent);
            }
            Destroy (other.gameObject);
        }
    }

    void Fire () {
        if (Time.time > nextFire) {
            nextFire = Time.time + Random.Range (minFireRate, maxFireRate);

            var bulletObject = Instantiate<GameObject> (bullet);

            var bulletComponent = bullet.GetComponent<Bullet> ();
            bulletComponent.source = gameObject;
            bulletComponent.damage = bulletDamage;
            bulletComponent.velocity = bulletVelocity;

            bulletObject.transform.position = transform.position + new Vector3 (0.0f, -1.0f, 0.0f);
        }
    }

    void Move () {
        DirectionChangeTimeCheck ();
        transform.position += velocity;
    }

    void DirectionChangeTimeCheck () {
        if (Time.time > nextDirectionChange) {
            nextDirectionChange = Time.time + directionChangeRate;
            velocity = -velocity;
        }
    }
}