using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpaceShip : SpaceShip {

    public Vector3 velocity;
    public int scoreValue;

    protected NPCSpaceShipsSet NPCSpaceShipsSet;

    protected void ScorePlayer (Bullet bulletComponent) {
        var playerComponent = bulletComponent.source.GetComponent<Player> ();
        playerComponent.Score (scoreValue);
    }

    protected bool IsPlayerSource (Bullet bulletComponent) {
        if (bulletComponent != null &&
            bulletComponent.source != null &&
            bulletComponent.source.GetComponent<Player> () != null)
            return true;
        return false;
    }

    protected bool IsDestroyed () {
        if (health <= 0) {
            return true;
        }
        return false;
    }
}