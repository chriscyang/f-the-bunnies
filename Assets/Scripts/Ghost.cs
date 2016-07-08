using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Ghost : MonoBehaviour
{
	public Transform player;
	public GameObject gameManager;
	public AudioSource audioSource;

	public bool staredAt = false;
	public bool isMoving;
	public int moveSpeed = 12;

	void Start ()
	{
		player = GameObject.FindWithTag ("Player").transform;
		gameManager = GameObject.FindWithTag ("GameManager");
		audioSource = GetComponent<AudioSource> ();

		// Increase moveSpeed for every wave cleared.
		if (gameManager.GetComponent<LevelManager> ().getGhostCount () % 2 == 0) {
			moveSpeed += 2;
			Debug.Log ("moveSpeed increased.");
		}

		// Face the player.
		// Done by rotating an additional 180 degrees because the model is fucked up.
		transform.LookAt (player);
		transform.rotation *= Quaternion.Euler (0, 180f, 0);

		// Start moving.
		isMoving = true;
	}

	void Update ()
	{
		// Keep moving towards the player when not being looked at.
		// Done by decrementing the position because the model is fucked up.
		if (!staredAt && isMoving) {
			transform.position -= transform.forward * moveSpeed * Time.deltaTime;
		}

		if (Input.GetKey (KeyCode.A)) {
			JumpScare ();
		}
	}

	void OnBecameVisible ()
	{
		staredAt = true;
	}

	void OnBecameInvisible ()
	{
		staredAt = false;
	}

	void OnTriggerEnter (Collider other)
	{
		// Scare the player on collide.
		if (other.gameObject.tag == "Player") {
			JumpScare ();
		}
	}

	void OnDestroy ()
	{
		gameManager.GetComponent<LevelManager> ().incKills ();
	}

	void JumpScare ()
	{
		// Jump right in front of the player.
		transform.position = player.position + player.forward;

		// Reorientate to face the player again.
		transform.LookAt (player.position);
		transform.rotation *= Quaternion.Euler (0, 180f, 0);

		// Stay in front of the player.
		isMoving = false;

		audioSource.Play ();

		Invoke ("GameOver", 0.5f);
	}

	void GameOver ()
	{
		SceneManager.LoadScene ("Finish");
		ScoreManager.UpdateHighScore ();
	}
}
