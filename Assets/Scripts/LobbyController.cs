using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyController : MonoBehaviour
{
    public Button singleplayerButton;
    public Button multiplayerButton;
    public Button exitButton;

    private void Awake()
    {
        singleplayerButton.onClick.AddListener(StartSinglePlayer);
        multiplayerButton.onClick.AddListener(StartMultiPlayer);
        exitButton.onClick.AddListener(Exit);
    }

    private void StartSinglePlayer()
    {
        SceneManager.LoadScene("SinglePlayer");
    }
    private void StartMultiPlayer()
    {
        SceneManager.LoadScene("MultiPlayer");
    }
    private void Exit()
    {
        Application.Quit();
    }
}
