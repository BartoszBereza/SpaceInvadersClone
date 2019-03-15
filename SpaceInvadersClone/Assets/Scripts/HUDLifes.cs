using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDLifes : MonoBehaviour {

    public int startLifesNumber;
    public int maxLifesNumber;
    public GameObject HUDLife;
    public GameObject[] lifesArray;

    void Start () {
        CreateLifes ();
        SetLifes (startLifesNumber);
    }

    void CreateLifes () {
        lifesArray = new GameObject[maxLifesNumber];
        var lifeRectTransform = HUDLife.GetComponent<RectTransform> ();
        var width = lifeRectTransform.rect.width;
        var height = lifeRectTransform.rect.height;

        for (int i = 0; i < maxLifesNumber; ++i) {
            GameObject lifeObject = Instantiate<GameObject> (HUDLife);
            lifeObject.transform.SetParent (transform);
            lifeObject.SetActive (false);
            lifeObject.transform.position = new Vector3 (width / 2 + i * width, height / 2, 0.0f);
            lifesArray[i] = lifeObject;
        }
    }

    public void SetLifes (int lifes) {
        for (int i = 0; i < maxLifesNumber; ++i) {
            GameObject lifeObject = lifesArray[i];
            if (lifes > 0) {
                lifeObject.SetActive (true);
                lifes--;
            } else {
                lifeObject.SetActive (false);
            }
        }
    }
}