using UnityEngine;
using System.Collections;

public class MoveForward : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
	
	}

	public float speed = 1f;

	void Update ()
	{
		if (LoadObjects.hasRun) {
			float translation = Time.deltaTime * 2;
			transform.Translate (0, 0, translation);
		}
	}
}
