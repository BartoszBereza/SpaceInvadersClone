using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverField : MonoBehaviour {

    private Player playerComponent;

    void Start () {
        playerComponent = transform.parent.GetComponent<Player> ();
    }

    void OnTriggerEnter2D (Collider2D other) {
        if (IsEnemy (other)) {
            playerComponent.GameOver ();
        }
    }

    bool IsEnemy (Collider2D other) {
        var enemyComponent = other.GetComponent<Enemy> ();
        if (enemyComponent != null) return true;
        return false;
    }
}