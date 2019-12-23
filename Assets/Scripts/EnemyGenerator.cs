using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
	public Transform EnemyParent;
    public List<GameObject> EnemyPrefabs;
	public Transform Character;

	private int currentIndex;
	private GameObject currentEnemy;

    void Start()
    {
		foreach (GameObject enemy in EnemyPrefabs)
		{
			GameObject spawn = Instantiate(enemy, EnemyParent);
			spawn.SetActive(false);
		}
		SelectRandom();
    }

    void Update()
    {
		//Nếu enemy hiện tại đi qua character thì spawn enemy mớiz
		if (currentEnemy.transform.position.x <= Character.position.x)
		{
			int ran = currentIndex;
			while (ran == currentIndex)
			{
				ran = Random.Range(0, EnemyParent.childCount);
			}
			currentIndex = ran;
			currentEnemy = EnemyParent.GetChild(ran).gameObject;
			currentEnemy.SetActive(true);
		}
	}

    public void Restart()
    {
		foreach (Transform enemy in EnemyParent)
		{
			enemy.GetComponent<EnemyLogic>().Reset();
		}

		SelectRandom();
	}

	private void SelectRandom()
	{
		int ran = Random.Range(0, EnemyParent.childCount);
		currentIndex = ran;
		currentEnemy = EnemyParent.GetChild(ran).gameObject;
		currentEnemy.SetActive(true);
	}

}
