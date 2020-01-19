using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveManager
{
	private static MoveManager instance;
	private GameObject selectedObject;

	private MoveManager() { }

	public static MoveManager Instance
	{
		get 
		{
			if (instance == null)
			{
				instance = new MoveManager();
			}

			return instance;
		}
	}

	

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		
    }

	public void RegisterSelectedObject(GameObject obj)
	{
		if (obj != selectedObject)
		{
			if (selectedObject != null)
			{
				selectedObject.GetComponent<MoveableObject>().changeActiveState();
				
			}
			selectedObject = obj;
			obj.GetComponent<MoveableObject>().changeActiveState();
			Debug.Log("Now selected" + obj.transform.position);

		}

		
	}
}
