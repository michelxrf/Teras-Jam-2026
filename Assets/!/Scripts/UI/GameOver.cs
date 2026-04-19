using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField] TMP_Text _gameOverText;
    [SerializeField] CanvasGroup _canvasGroup;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        Hide();
    }

    public void Gameover(bool goodEnding)
    {
        if (goodEnding)
        {
            _gameOverText.text = "You win! All the cats are safe!";
        }
        else
        {
            _gameOverText.text = "You lose! Those lovely thing tore you to pieces! Who's gonna feed them now?!";
        }
        DisablePlayer();
        Show();
    }

    void DisablePlayer()
    {
        PlayerController playerController = FindFirstObjectByType<PlayerController>();
        playerController._isDead = true;
    }

    void Show()
    {
        _canvasGroup.alpha = 1f;
        _canvasGroup.interactable = true;
        _canvasGroup.blocksRaycasts = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void Hide()
    {
        _canvasGroup.alpha = 0f;
        _canvasGroup.interactable = false;
        _canvasGroup.blocksRaycasts = false;
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
