using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject objectPrefab;
    private float Speed = 5.0f;
    private string EndMessage;
    private AudioSource AudioSource;
    public AudioClip gameOverSound;
    public AudioClip winSound;
    public GameObject mainMenu;
    public GameObject howToPlayMenu;

    private void Start()
    {
        AudioSource = GetComponent<AudioSource>();
    }

    public void EndGame(bool win, string message)
    {
        AudioSource.PlayOneShot(win ? winSound : gameOverSound, 1f);
        EndMessage = message;
        Time.timeScale = 0;
        StartCoroutine(ChangeSceneInSecs(0.6f, "GameFinish"));
    }

    IEnumerator ChangeSceneInSecs(float nSecs, string scene)
    {
        yield return new WaitForSecondsRealtime(nSecs);
        Time.timeScale = 1;
        SceneManager.LoadScene(scene);
    }

    public string GetMessage()
    {
        return EndMessage;
    }

    public void StartGame(float speedArg)
    {
        Speed = speedArg;
        StartCoroutine(ChangeSceneInSecs(0.3f, "Minigame"));
    }

    public float GetSpeed()
    {
        return Speed;
    }

    public void ReturnMainMenu()
    {
        StartCoroutine(ChangeSceneInSecs(0.3f, "MainMenu"));
    }

    public void ShowHowToPlayMenu()
    {
        mainMenu.SetActive(false);
        howToPlayMenu.SetActive(true);
    }

    public void HideHowToPlayMenu()
    {
        mainMenu.SetActive(true);
        howToPlayMenu.SetActive(false);
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
