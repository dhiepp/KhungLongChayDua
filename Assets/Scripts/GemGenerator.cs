using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemGenerator : MonoBehaviour
{
	public Transform GemParent;
	public List<GameObject> GemPrefabs;

	private float time = 0f;
	private float wait = 10f;

	void Start()
	{
		wait = Random.Range(10, 20);
		foreach (GameObject gem in GemPrefabs)
		{
			GameObject spawn = Instantiate(gem, GemParent);
			spawn.SetActive(false);
		}
	}

	// Update is called once per frame
	void Update()
	{
		time += Time.deltaTime;
		if (time >= wait)
		{
			time = 0;
			wait = Random.Range(10, 20);
			int ran = Random.Range(0, GemParent.childCount);
			GameObject selected = GemParent.GetChild(ran).gameObject;
			selected.SetActive(true);
		}
	}

	public void Restart()
	{
		time = 0f;

		foreach (Transform gem in GemParent)
		{
			if (gem.gameObject.activeSelf)
			gem.GetComponent<GemLogic>().Reset();
		}
	}
}
