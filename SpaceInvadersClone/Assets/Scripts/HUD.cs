using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD : MonoBehaviour {

    public HUDLifes HUDLifesOnScene;
    public HUDScore HUDScoreOnScene;
    public GameObject playerOnScene;
    public GameObject NPCSpaceShipsSetOnScene;
    public GameObject returnToMenuButton;

    private Player playerComponent;
    private NPCSpaceShipsSet NPCSpaceShipsSetComponent;
    private HUDControler HUDControlerComponent;

    void Start () {
        playerComponent = playerOnScene.GetComponent<Player> ();
        HUDControlerComponent = GetComponent<HUDControler>();
        NPCSpaceShipsSetComponent = NPCSpaceShipsSetOnScene.GetComponent<NPCSpaceShipsSet> ();
        returnToMenuButton.SetActive (false);
    }

    void OnGUI () {
        CheckInput();
        if (playerComponent.lifes <= 0 || NPCSpaceShipsSetComponent.enemiesLeft <= 0) {
            returnToMenuButton.SetActive (true);
        }
        HUDLifesOnScene.SetLifes (playerComponent.lifes);
        HUDScoreOnScene.SetScore (playerComponent.score);
    }

    void CheckInput () {
        if (Input.GetKey (KeyCode.Escape)) {
            HUDControlerComponent.ChangeScene(0);
        }
    }
}