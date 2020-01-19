using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveableObject : MonoBehaviour
{
	float t;
	public bool mouseOver = false, selected = false;
	Color color;

	private Transform arrowZ, arrowX;

    // Start is called before the first frame update
    void Start()
    {
		color = GetComponent<Renderer>().material.color;
		arrowZ = transform.Find("zArrow");
		arrowX = transform.Find("xArrow");
		arrowZ.gameObject.SetActive(false);
		arrowX.gameObject.SetActive(false);
	}

	private void OnMouseEnter()
	{
		mouseOver = true;
		Debug.Log("slrt");
	}

	private void OnMouseOver()
	{
		mouseOver = true;
		if (Input.GetMouseButtonDown(0))
		{
			MoveManager.Instance.RegisterSelectedObject(gameObject);
		}
	}

	private void OnMouseExit()
	{
		mouseOver = false;

	}

	// Update is called once per frame
	void Update()
    {
		t += Time.deltaTime;
		if (!mouseOver && !selected)
		{
			GetComponent<Renderer>().material.color = color;
		} else
		{
			GetComponent<Renderer>().material.color = Color.red;
		}

		arrowZ.rotation = arrowZ.rotation;


	}

	public void changeActiveState() 
	{
		selected = !selected;
		if (selected)
		{
			GetComponent<Renderer>().material.color = Color.red;
			arrowZ.gameObject.SetActive(true);
			arrowX.gameObject.SetActive(true);
		} else
		{
			GetComponent<Renderer>().material.color = color;
			arrowZ.gameObject.SetActive(false);
			arrowX.gameObject.SetActive(false);
		}
	}

}
