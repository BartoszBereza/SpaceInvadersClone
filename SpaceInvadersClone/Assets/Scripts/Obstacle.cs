using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour {

    public int maxDurability;
    public int currentDurability;

    private SpriteRenderer spriteRenderer;

    void Start () {
        spriteRenderer = GetComponent<SpriteRenderer> ();
    }

    void OnTriggerEnter2D (Collider2D other) {
        if (!IsEnemy (other)) Destroy (other.gameObject);
        Hit ();
        CheckIfDestroyed ();
    }

    bool IsEnemy (Collider2D other) {
        var enemyComponent = other.GetComponent<Enemy> ();
        if (enemyComponent != null) return true;
        return false;
    }

    void Hit () {
        currentDurability--;
        spriteRenderer.color = new Color (1.0f, 1.0f, 1.0f,
            (float) currentDurability / (float) maxDurability * 1.0f);
    }

    void CheckIfDestroyed () {
        if (currentDurability <= 0) {
            gameObject.SetActive (false);
        }
    }
}