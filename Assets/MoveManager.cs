using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveManager : MonoBehaviour
{
	private static MoveManager instance;
	public GameObject[] movableObjects;

	private int selectedIndex = 0;
	GameObject selectedObj;

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
		if (GameManager.instance.currentStage == GameManager.GameStage.Building)
		{
			foreach (var _obj in movableObjects)
			{
				_obj.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
			}

			if (Input.GetKeyDown(KeyCode.Tab))
			{
				if (selectedObj == null)
				{
					selectedObj = movableObjects[selectedIndex];
					selectedObj.GetComponent<MoveableObject>().ChangeActiveState();
				} else
				{
					selectedObj.GetComponent<MoveableObject>().ChangeActiveState();
					selectedIndex = (selectedIndex+1) % movableObjects.Length;
					movableObjects[selectedIndex].GetComponent<MoveableObject>().ChangeActiveState(); ;
					selectedObj = movableObjects[selectedIndex];
				}
			} 
		} else
		{
			foreach (var _obj in movableObjects)
			{

				_obj.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
			}
		}
    }

	private void FixedUpdate()
	{
		if (selectedObj != null && GameManager.instance.currentStage == GameManager.GameStage.Building) {
			if (Input.GetKey(KeyCode.W))
			{
				selectedObj.GetComponent<Rigidbody>().MovePosition(selectedObj.transform.position + Vector3.forward * 0.15f);
			}
			else if (Input.GetKey(KeyCode.S))
			{
				selectedObj.GetComponent<Rigidbody>().MovePosition(selectedObj.transform.position + Vector3.back * 0.15f);

			}
			else if (Input.GetKey(KeyCode.A))
			{
				selectedObj.GetComponent<Rigidbody>().MovePosition(selectedObj.transform.position + Vector3.left * 0.15f);
			}
			else if (Input.GetKey(KeyCode.D))
			{
				selectedObj.GetComponent<Rigidbody>().MovePosition(selectedObj.transform.position + Vector3.right * 0.15f);
			}
			selectedObj.GetComponent<Rigidbody>().velocity = Vector3.zero;
		}

		

	}

}
