using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemLogic : MonoBehaviour
{
    public float range = 30f;
	public GemType type;
    private Vector3 oldPosition;

	private new Collider2D collider2D;
	private SpriteRenderer spriteRenderer;
	private AudioSource audioSource;

	public enum GemType
	{
		RED, GREEN, YELLOW
	}

	private void Start()
	{
		collider2D = GetComponent<Collider2D>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		audioSource = GetComponent<AudioSource>();
		oldPosition = transform.position;
	}

	void Update()
    {
        float speed = GameLogic.speed;
        gameObject.transform.Translate(new Vector3(-1 * Time.deltaTime * speed, 0, 0));
        if (Vector3.Distance(oldPosition, gameObject.transform.position) > range)
        {
			Reset();
        }
    }

	public void Eat() {
		spriteRenderer.enabled = false;
		GetComponent<Collider2D>().enabled = false;
		//Play Gem Sound
		if (AudioLogic.Enable) audioSource.PlayOneShot(audioSource.clip);
	}

	public void Reset()
	{
		spriteRenderer.enabled = true;
		collider2D.enabled = true;
		gameObject.transform.position = oldPosition;
		gameObject.SetActive(false);
	}
}
