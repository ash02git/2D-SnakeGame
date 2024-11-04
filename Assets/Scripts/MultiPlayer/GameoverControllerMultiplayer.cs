using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameoverControllerMultiplayer : MonoBehaviour
{
    public Button restartButton;
    public Button quitButton;
    public TextMeshProUGUI winnerText;

    private void Start()
    {
        restartButton.onClick.AddListener(Restart);
        quitButton.onClick.AddListener(Quit);
    }

    private void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    private void Quit()
    {
        SceneManager.LoadScene("Lobby");
    }

    public void ShowWinner(string winner)
    {
        winnerText.text = winner + "has won";
    }
}
