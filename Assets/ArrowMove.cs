using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowMove : MonoBehaviour
{
	private Vector3 mOffset;
	private float mZCoord;
    // Start is called before the first frame update


	private void OnMouseDown()
	{
		
		transform.gameObject.transform.position += new Vector3(0f, 10f, 0f);

		mZCoord = Camera.main.WorldToScreenPoint(transform.gameObject.transform.position).z;
		mOffset = transform.gameObject.transform.position - GetMouseWorldPos();
	}

	private Vector3 GetMouseWorldPos()
	{
		Vector3 mousePoint = Input.mousePosition;

		mousePoint.z = mZCoord;

		return Camera.main.ScreenToWorldPoint(mousePoint);
	}

	private void OnMouseDrag()
	{
		GameObject.Find("MovableBlock").transform.position += new Vector3(0, 0, 1f);
		
	}

}
