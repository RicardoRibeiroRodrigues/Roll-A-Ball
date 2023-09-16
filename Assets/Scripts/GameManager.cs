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

    private void Start()
    {
        AudioSource = GetComponent<AudioSource>();
    }

    public void EndGame(bool win, string message)
    {
        AudioSource.PlayOneShot(win ? winSound : gameOverSound, 1f);
        EndMessage = message;
        StartCoroutine(ChangeSceneInSecs(0.5f, "GameFinish"));
    }

    IEnumerator ChangeSceneInSecs(float nSecs, string scene)
    {
        yield return new WaitForSecondsRealtime(nSecs);
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

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
