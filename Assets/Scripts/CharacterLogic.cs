using System.Collections;
using UnityEngine;
using static GemLogic;

public class CharacterLogic : MonoBehaviour
{
	public GameLogic gameLogic;
	public GameObject HeartsIndicator;

    private new Rigidbody2D rigidbody2D;
	private new Collider2D collider2D;
	private SpriteRenderer spriteRenderer;
	private AudioSource[] audioSources;
	private Animator anim;

	//Có đang ở mặt đất ko?
	private bool isGrounded = false;
	//Có đang bất tử ko?
    private bool isInvincible = false;
	//Số mạng
	private int hearts = 3;
	//Chiều cao nhảy
	private float jump = 15f;

    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
		collider2D = GetComponent<Collider2D>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		audioSources = GetComponents<AudioSource>();
		anim = GetComponent<Animator>();
	}

	void Update()
    {
		//Tắt collision với enemy khi đang bất tử
        if (isInvincible == true)
        {
            Physics2D.IgnoreLayerCollision(9, 10, true);
        }
        else
        {
            Physics2D.IgnoreLayerCollision(9, 10, false);
        }

		//Kiểm tra xem có đang chạm đất và xử lí nhảy
		isGrounded = Physics2D.Raycast(collider2D.bounds.center, Vector2.down, 
			collider2D.bounds.extents.y + 0.1f, 9);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(isGrounded)
			{
				//Play Jump Sound
				if (AudioLogic.Enable) audioSources[0].PlayOneShot(audioSources[0].clip);
				rigidbody2D.velocity = new Vector2(0, jump);
			}
        }

		//Animation
        anim.SetBool("isGrounded", isGrounded);
        anim.SetBool("isRunning", true);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
		//Va chạm với kẻ thù 
		if (collision.gameObject.tag == "Enemy")
        {
			//Play Hurt Sound
			if (AudioLogic.Enable) audioSources[1].PlayOneShot(audioSources[1].clip);
			hearts--;
			HeartsIndicator.transform.GetChild(hearts).gameObject.SetActive(false);
			if (hearts > 0)
			{
				StartCoroutine(InvincibleTime(0f,0.1f));
			}
			else gameLogic.Lose();
        }
	}

    void OnTriggerEnter2D(Collider2D other)
    {
		//Va chạm với Coin
		if (other.gameObject.tag == "Coin")
		{
			gameLogic.EatCoin();
			other.gameObject.GetComponent<CoinLogic>().Eat();
		}
		//Va chạm với Gem
		if (other.gameObject.tag == "gem")
		{
			GemLogic gem = other.gameObject.GetComponent<GemLogic>();
			gem.Eat();
			GemType type = gem.type;
			switch (type)
			{
				//RED: bất tử 5s
				case GemType.RED:
					{
						StopAllCoroutines();
						StartCoroutine(InvincibleTime(5f, 0.2f));
						break;
					}
				//GREEN: thêm mạng
				case GemType.GREEN:
					{
						if (hearts < 3)
						{
							HeartsIndicator.transform.GetChild(hearts).gameObject.SetActive(true);
							hearts++;
						}
						else
						{
							gameLogic.EatGem();
						}
						break;
					}
				//YELLOW: thêm điểm
				case GemType.YELLOW:
					{
						gameLogic.EatGem();
						break;
					}
			}
		}
	}

    private IEnumerator InvincibleTime(float duration, float flicker)
    {
		isInvincible = true;
		GetComponent<SpriteRenderer>().color = Color.red;
		float time = flicker;
        yield return new WaitForSeconds(duration);
        while (true)
        {
            time -= Time.deltaTime;
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.color = Color.red;
            if (time <= 0)
            {
                break;
            }
        }
        isInvincible = false;
        GetComponent<SpriteRenderer>().color = Color.white;
    }

	public void Restart()
	{
		//Reset vị trí và tốc độ
		gameObject.transform.position = new Vector3(-7f, 2f, 0);
		rigidbody2D.velocity = new Vector2(0, 0);
		//Reset số mạng
		hearts = 3;
		foreach (Transform child in HeartsIndicator.transform)
		{
			child.gameObject.SetActive(true);
		}
	}
}
