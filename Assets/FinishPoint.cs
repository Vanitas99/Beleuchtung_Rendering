using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishPoint : MonoBehaviour
{
	Vector3 pos1, pos2;
	float speed;
    // Start is called before the first frame update
    void Start()
    {
		speed = 2.0f;
		pos1 = new Vector3(transform.position.x, transform.position.y + 0.1f, transform.position.z);
		pos2 = new Vector3(transform.position.x, transform.position.y - 0.1f, transform.position.z);
    }


	// Update is called once per frame
	void FixedUpdate()
	{
		transform.position = Vector3.Lerp(pos1, pos2, (Mathf.Sin(speed * Time.time) + 1.0f) / 2.0f) ;
	}
}
