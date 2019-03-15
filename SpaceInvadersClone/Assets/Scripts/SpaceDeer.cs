using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceDeer : NPCSpaceShip {

    public int decreaseValue;
    public float decreaseRate;
    public int startScoreValue;

    private float decreaseTime;

    void Start () {
        NPCSpaceShipsSet = transform.parent.GetComponent<NPCSpaceShipsSet> ();
    }

    void Update () {
        DecreaseScoreByTime ();
        Move ();
    }

    void OnTriggerEnter2D (Collider2D other) {
        var otherBulletComponent = other.GetComponent<Bullet> ();
        if (IsPlayerSource (otherBulletComponent)) {
            var playerComponent = otherBulletComponent.source.GetComponent<Player> ();
            playerComponent.AddLife ();
            DealDamage (otherBulletComponent);
            if (IsDestroyed ()) {
                NPCSpaceShipsSet.DeleteEnemy (gameObject);
                CreateExplosion ();
                ScorePlayer (otherBulletComponent);
            }
            Destroy (other.gameObject);
        }
    }

    void Move () {
        transform.position += velocity;
    }

    void DecreaseScoreByTime () {
        if (decreaseTime + decreaseRate <= Time.time) {
            decreaseTime = Time.time;
            scoreValue -= decreaseValue;
        }
    }

    public void SetDecreaseTime (float time) {
        decreaseTime = time;
    }

    public void ResetScore () {
        scoreValue = startScoreValue;
    }
}