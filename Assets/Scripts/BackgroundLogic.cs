using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundLogic : MonoBehaviour
{
    public float range;
    private Vector3 oldPosition;

	void Start()
    {
        oldPosition = gameObject.transform.position; 
    }

    void Update()
    {
		float speed = GameLogic.speed;
        gameObject.transform.Translate(new Vector3(-1 * Time.deltaTime * speed, 0, 0));

        if(Vector3.Distance(oldPosition,gameObject.transform.position) > range)
        {
            gameObject.transform.position = oldPosition;
        }
    }
}
