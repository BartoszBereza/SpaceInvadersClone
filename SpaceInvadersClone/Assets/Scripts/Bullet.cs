using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public int damage;
    public Vector3 velocity;
    public GameObject source;

    void Update () {
        transform.position += velocity;
    }
}