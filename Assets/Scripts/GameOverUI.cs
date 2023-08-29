using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private Button newGameButton;

    private void Awake()
    {
        playerHealth.OnPlayerDied += PlayerHealth_OnPlayerDied;
        Hide();
    }

    private void PlayerHealth_OnPlayerDied(object sender, System.EventArgs e)
    {
        Show();
    }

    private void Start()
    {
        newGameButton.onClick.AddListener(() => {
            SceneManager.LoadScene(0);
        });

    }
    private void Show()
    {
        gameObject.SetActive(true);
    }
    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
