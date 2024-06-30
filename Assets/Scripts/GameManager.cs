using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private const float LevelAppearingDuration = 1f;
    private const float PauseFadingDuration = 0.5f;

    [SerializeField]
    private FadeManager fadeManager;

    [SerializeField]
    private GameObject pauseMenu;

    public void ReloadLevel()
    {
        fadeManager.FadeOut(LevelAppearingDuration);

        Invoke(nameof(ReloadScene), LevelAppearingDuration);
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        fadeManager.FadeOut(PauseFadingDuration);

        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(true);
        fadeManager.FadeIn(PauseFadingDuration);

        Time.timeScale = 1f;
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    private void ReloadScene()
    {
        var currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }

    private void Start()
    {
        fadeManager.FadeIn(LevelAppearingDuration);
        StartCoroutine(GenerateLevel());
    }

    private IEnumerator GenerateLevel()
    {
        yield return GetComponent<MazeGenerator>().GeneratLevel();
    }
}
