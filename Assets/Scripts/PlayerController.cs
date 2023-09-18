using UnityEngine;

// Include the namespace required to use Unity UI and Input System
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour {
	
	// Create public variables for player speed, and for the Text UI game objects
	public float speed;
	private int RemainingTime;
	private int Lives;
	public TextMeshProUGUI countText;
	public TextMeshProUGUI livesText;
	public TextMeshProUGUI timeText;
	
    private AudioSource AudioSource;
    public AudioClip getPoint;
    public AudioClip getHit;

	private GameManager Gm;

    private float MovementX;
    private float MovementY;

	private Rigidbody Rb;
	private int Count;
	private float Time;

	// At the start of the game..
	void Start ()
	{
        Gm = FindObjectOfType<GameManager>();
		// Assign the Rigidbody component to our private rb variable
		Rb = GetComponent<Rigidbody>();
		AudioSource = GetComponent<AudioSource>();

		// Set the count to zero 
		Count = 0;
		RemainingTime = 40;
		Lives = 3;

		SetCountText();
		SetLivesText();
	}

	void Update()
	{
		Time += UnityEngine.Time.deltaTime;
		// Run every 1 sec.
		if (Time >= 1.0) 
		{
			Time = 0.0f;
			RemainingTime--;
			if (RemainingTime <= 0) 
			{
				Gm.EndGame(false, "Time is up, you lose");
			}
			timeText.text = "Time: " + RemainingTime.ToString() + "s";
		}
	}

	void FixedUpdate()
	{
		// Create a Vector3 variable, and assign X and Z to feature the horizontal and vertical float variables above
		Vector3 movement = new Vector3 (MovementX, 0.0f, MovementY);

		Rb.AddForce(movement * speed);

		if (transform.position.y < -1)
		{
			Gm.StartGame(Gm.GetSpeed());
		}
	}

	void OnTriggerEnter(Collider other) 
	{
		// ..and if the GameObject you intersect has the tag 'Pick Up' assigned to it..
		if (other.gameObject.CompareTag ("PickUp"))
		{
			// Play the point sound
			AudioSource.PlayOneShot(getPoint, 0.5f);
			other.gameObject.SetActive(false);

			// Add one to the score variable 'count'
			Count++;

			// Run the 'SetCountText()' function (see below)
			SetCountText();
		}
		if (other.gameObject.CompareTag("Enemy"))
		{
			// Play the hit sound
			AudioSource.PlayOneShot(getHit, 0.8f);
			Lives--;
			// Teleports the enemy to a place near, to give time to the player to run from it.
			Vector3 enemyPos = other.gameObject.transform.position;
			float movX = UnityEngine.Random.Range(6f, 8f);
			float movZ = UnityEngine.Random.Range(6f, 8f);

			if (enemyPos.x >= 0)
			{
				movX = -movX;
			}
			if (enemyPos.z >= 0)
			{
				movZ = -movZ;
			}
			Vector3 movement = new Vector3(movX, 0.0f, movZ);
			other.gameObject.transform.Translate(movement);
			
			// If the player is out of lives, the game ends.
			if (Lives <= 0)
			{
				Gm.EndGame(false , "Out of lives, you lose");
			}
			SetLivesText();
		}
	}

    void OnMove(InputValue value)
    {
        Vector2 v = value.Get<Vector2>();

        MovementX = v.x;
        MovementY = v.y;
    }

    void SetCountText()
	{
		countText.text = "Count: " + Count.ToString();

		if (Count >= 12) 
		{
			Gm.EndGame(true, "You got all pickups, you win!!");
		}
	}

	void SetLivesText()
	{
		livesText.text = "Lives: " + Lives.ToString();
	}
}