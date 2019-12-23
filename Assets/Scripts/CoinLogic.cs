using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinLogic : MonoBehaviour
{
	public float range = 20f;
	private float startX = 10f;

	private new Collider2D collider2D;
	private SpriteRenderer spriteRenderer;
	private AudioSource audioSource;

	private void Start()
	{
		collider2D = GetComponent<Collider2D>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		audioSource = GetComponent<AudioSource>();
	}

	void Update()
	{
		float speed = GameLogic.speed;
		gameObject.transform.Translate(new Vector3(-1 * Time.deltaTime * speed, 0, 0));

		if (Mathf.Abs(startX - gameObject.transform.position.x) > range)
		{
			float newY = Random.Range(-4f, 0f);
			gameObject.transform.position = new Vector2(startX, newY);
			spriteRenderer.enabled = true;
			collider2D.enabled = true;
		}
	}

	public void Eat()
	{
		spriteRenderer.enabled = false;
		collider2D.enabled = false;
		//Play Coin Sound
		if (AudioLogic.Enable) audioSource.PlayOneShot(audioSource.clip);
	}

}
