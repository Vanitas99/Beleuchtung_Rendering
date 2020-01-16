using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap_Star : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
		transform.RotateAroundLocal(Vector3.back, 1f);

	}
}
