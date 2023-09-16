using UnityEngine;
using TMPro;

public class MenuManager : MonoBehaviour
{
    private GameManager GameManager;
    public TextMeshProUGUI endMessage;
    // Start is called before the first frame update
    void Start()
    {
        GameManager = FindObjectOfType<GameManager>();
        endMessage.text = GameManager.GetMessage();
    }

    public void GmStartGame(float speed) {
        GameManager.StartGame(speed);
    }

}
