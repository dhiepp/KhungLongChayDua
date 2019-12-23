using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioLogic : MonoBehaviour
{
	public static bool Enable = true;
	private AudioSource music;

    void Start()
    {
		music = GetComponent<AudioSource>();
		StartBGM();
    }

	public void StartBGM()
	{
		if (Enable) music.Play();
	}

	public void StopBGM()
	{
		if (Enable) music.Stop();
	}
}
