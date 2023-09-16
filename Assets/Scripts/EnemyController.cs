using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject player;
    private float Speed;
    private GameManager GameManager;

    // Start is called before the first frame update
    void Start()
    {
        GameManager = FindObjectOfType<GameManager>();
        Speed = GameManager.GetSpeed();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 localPosition = player.transform.position - transform.position;
        localPosition = localPosition.normalized;
        Vector3 movement = new Vector3(localPosition.x, localPosition.y, localPosition.z) * (Time.deltaTime * Speed);
        transform.Translate(movement);
    }
}
