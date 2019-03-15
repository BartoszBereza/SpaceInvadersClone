using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {

    public float explosionTime;

    private SpriteRenderer spriteRenderer;
    private float explosionPhase;

    void Start () {
        spriteRenderer = GetComponent<SpriteRenderer> ();
        explosionPhase = 1.0f;
    }

    void Update () {
        SetExplosionPhase ();
        if (explosionPhase <= 0.0f) Destroy (gameObject);
    }

    void SetExplosionPhase () {
        spriteRenderer.color = Color.Lerp (Color.clear, Color.white, explosionPhase);
        explosionPhase -= Time.deltaTime / explosionTime;
    }
}