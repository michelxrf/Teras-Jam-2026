using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuPanel;

    CanvasGroup _canvasGroup;
    PlayerController _playerController;
    bool _isShowing = false;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _playerController = FindFirstObjectByType<PlayerController>();
        Hide();
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    void Show()
    {
        Cat[] cats = FindObjectsByType<Cat>(FindObjectsSortMode.None);
        foreach (Cat c in cats)
        {
            c.GetComponent<NavMeshAgent>().isStopped = true;
        }

        _playerController.enabled = false;
        _canvasGroup.alpha = 1f;
        _canvasGroup.interactable = true;
        _canvasGroup.blocksRaycasts = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        _isShowing = true;
    }

    public void Hide()
    {
        Cat[] cats = FindObjectsByType<Cat>(FindObjectsSortMode.None);
        foreach (Cat c in cats)
        {
            c.GetComponent<NavMeshAgent>().isStopped = false;
        }

        _playerController.enabled = true;
        _canvasGroup.alpha = 0f;
        _canvasGroup.interactable = false;
        _canvasGroup.blocksRaycasts = false;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
        _isShowing = false;
    }

    public void OnPause(InputValue value)
    {
        if (_playerController._isDead)
        {
            Hide();
            return;
        }

        if(_isShowing)
            Hide();
        else
            Show();

    }
}
