using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveableObject : MonoBehaviour
{
	
	public bool selected = false;
	Color color;


	// Start is called before the first frame update
	void Start()
	{
		color = GetComponent<Renderer>().material.color;

	}

	void Update()
    {
	
		if (!selected)
		{
			GetComponent<Renderer>().material.color = color;
		} else
		{
			GetComponent<Renderer>().material.color = Color.red;
		}



	}

	public void ChangeActiveState() 
	{
		selected = !selected;
		if (selected)
		{
			GetComponent<Renderer>().material.color = Color.red;
			
		} else
		{
			GetComponent<Renderer>().material.color = color;
			
		}
	}

}
