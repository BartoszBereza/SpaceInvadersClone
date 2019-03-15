using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : SpaceShip {

    public int lifes;
    public int score;
    public float speed;
    public GameObject laserBullet;
    public GameObject engine;
    public float laserBulletFireRate;
    public int laserBulletDamage;
    public Vector3 laserBulletVelocity;
    public int fireNegativeScore = 0;
    public float respawnTime;
    public float immuneTime;
    public Vector3 startPosition;
    public float minX;
    public float maxX;

    private GameObject engineObject;
    private SpriteRenderer spriteRenderer;
    private SpriteRenderer engineSpriteRenderer;
    private Color alpha0;
    private Color alpha1;
    private float t;
    private bool immune = false;
    private float immunePulseRate = 0.3f;
    private float immuneStartTime;
    private float laserBulletNextFire = 0.0f;

    void Start () {
        transform.position = startPosition;
        spriteRenderer = GetComponent<SpriteRenderer> ();
        CreateEngine ();
    }

    void Update () {
        if (immune) {
            Pulse ();
            CheckImmuneEnd ();
        }
        CheckInput ();
    }

    void OnTriggerEnter2D (Collider2D other) {
        if (immune) return;

        var otherBulletComponent = other.GetComponent<Bullet> ();
        if (IsEnemySource (otherBulletComponent)) {
            DealDamage (otherBulletComponent);
            Destroy (other.gameObject);
            CheckIfDestroyed ();
        }
    }

    void CreateEngine () {
        engineObject = Instantiate<GameObject> (engine);
        engineObject.SetActive (false);
        engineObject.transform.SetParent (transform);
        engineObject.transform.position = transform.position + new Vector3 (0.024f, -0.75f, 0.0f);
        engineSpriteRenderer = engineObject.GetComponent<SpriteRenderer> ();
    }

    void Pulse () {
        spriteRenderer.color = Color.Lerp (Color.clear,
            Color.white,
            Mathf.PingPong (Time.time / immunePulseRate, 1.0f));

        engineSpriteRenderer.color = Color.Lerp (Color.clear,
            Color.white,
            Mathf.PingPong (Time.time / immunePulseRate, 1.0f));
    }

    void CheckImmuneEnd () {
        if (immuneStartTime + immuneTime < Time.time) {
            immune = false;
            spriteRenderer.color = Color.white;
            engineSpriteRenderer.color = Color.white;
        }
    }

    void CheckInput () {
        if (Input.GetKey (KeyCode.LeftArrow)) {
            engineObject.SetActive (true);
            Move (new Vector3 (-speed, 0, 0));
        }
        if (Input.GetKey (KeyCode.RightArrow)) {
            engineObject.SetActive (true);
            Move (new Vector3 (speed, 0, 0));
        }
        if (Input.anyKey == false) {
            engineObject.SetActive (false);
        }
        if (Input.GetKey (KeyCode.Space) && Time.time > laserBulletNextFire) {
            Fire ();
        }
    }

    void Move (Vector3 velocity) {
        if (IsInside (transform.position + velocity)) transform.position += velocity;
    }

    bool IsInside (Vector3 nextPosition) {
        if (nextPosition.x > minX &&
            nextPosition.x < maxX) return true;
        return false;
    }

    void Fire () {
        if (score > 0) Score (fireNegativeScore);
        laserBulletNextFire = Time.time + laserBulletFireRate;

        var bulletObject = Instantiate<GameObject> (laserBullet);

        var bulletComponent = bulletObject.GetComponent<Bullet> ();
        bulletComponent.source = gameObject;
        bulletComponent.damage = laserBulletDamage;
        bulletComponent.velocity = laserBulletVelocity;

        bulletObject.transform.position = transform.position - new Vector3 (0.0f, -1.0f, 0.0f);
    }

    bool IsEnemySource (Bullet bulletComponent) {
        if (bulletComponent != null &&
            bulletComponent.source != null &&
            bulletComponent.source.GetComponent<Enemy> () != null)
            return true;
        return false;
    }

    void CheckIfDestroyed () {
        if (health <= 0) {
            gameObject.SetActive (false);
            CreateExplosion ();
            RemoveLife ();
            CheckLifes ();
        }
    }

    void CheckLifes () {
        if (lifes > 0) {
            Invoke ("Spawn", respawnTime);
        }
    }

    void Spawn () {
        immune = true;
        immuneStartTime = Time.time;
        transform.position = startPosition;
        gameObject.SetActive (true);
    }

    public void RemoveLife () {
        lifes--;
    }

    public void AddLife () {
        lifes++;
    }

    public void Score (int value) {
        score += value;
        if (score < 0) score = 0;
    }

    public void GameOver () {
        health = 0;
        lifes = 0;
        CheckIfDestroyed ();
    }
}