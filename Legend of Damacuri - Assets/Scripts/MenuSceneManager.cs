using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuSceneManager : MonoBehaviour
{
    [SerializeField] private Button startGameButton;
    [SerializeField] private Button creditsButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private GameObject creditsPanel;
    [SerializeField] private GameObject pressAnyButtonTextObj;
    [SerializeField] private string gameSceneName;

    private bool canCloseCredits;

    private void Awake()
    {
        startGameButton.onClick.AddListener(StartGame);
        creditsButton.onClick.AddListener(OpenCredits);
        quitButton.onClick.AddListener(QuitGame);
    }

    private void OnDestroy()
    {
        startGameButton.onClick.RemoveListener(StartGame);
        creditsButton.onClick.RemoveListener(OpenCredits);
        quitButton.onClick.RemoveListener(QuitGame);
    }

    private void Update()
    {
        if (canCloseCredits && Input.anyKeyDown)
        {
            creditsPanel.SetActive(false);
            canCloseCredits = false;
            pressAnyButtonTextObj.SetActive(false);
        }
    }

    private void StartGame ()
    {
        SceneManager.LoadScene(gameSceneName);
    }

    private void OpenCredits ()
    {
        creditsPanel.SetActive(true);
        pressAnyButtonTextObj.SetActive(false);
        Invoke(nameof(SetCanCloseCredits), 1);
    }

    private void QuitGame ()
    {
        Application.Quit();
    }

    private void SetCanCloseCredits ()
    {
        canCloseCredits = true;
        pressAnyButtonTextObj.SetActive(true);
    }
}
