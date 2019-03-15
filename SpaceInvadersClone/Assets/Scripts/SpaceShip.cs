using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShip : MonoBehaviour {

    public int health;
    public GameObject explosion;

    protected void DealDamage (Bullet bulletComponent) {
        health -= bulletComponent.damage;
    }

    protected void CreateExplosion () {
        var explosionObject = Instantiate<GameObject> (explosion);
        explosionObject.transform.position = transform.position;
    }
}