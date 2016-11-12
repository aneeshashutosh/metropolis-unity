using UnityEngine;
using System.Collections;
using SimpleJSON;

public class LoadObjects : MonoBehaviour
{
	PriorityQueue<GameObject> priorityQueue;
	Vector3 origin = new Vector3 (0, 0, 0);
	int offset = 32;
	float multiplier = 1;
	public AudioClip entrance;
	public GameObject platform;
	public GameObject plane;
	public GameObject camera;
	public GameObject dummy;
	public static string jsonString;
	float originalY;
	static bool hasRun = false;

	// Use this for initialization
	void Start ()
	{
		if (!hasRun) {
			originalY = plane.transform.position.y;
			// plane.transform.position = new Vector3 (plane.transform.position.x, plane.transform.position.y - 2000, plane.transform.position.z);
		}
	}

	public static void Run (string jsonString)
	{
		
	}

	void Update ()
	{
		if (jsonString != null && !hasRun) {
			hasRun = true;
			plane.transform.position = new Vector3(plane.transform.position.x, originalY, plane.transform.position.z);

			priorityQueue = new PriorityQueue<GameObject> ();

			// jsonString = "[\n\t{\n\t\t\\\"shape\\\": \\\"cube\\\",\n\t\t\\\"x\\\": 4,\n\t\t\\\"y\\\": 5,\n\t\t\\\"z\\\": 3,\n\t\t\\\"l\\\": 1.2,\n\t\t\\\"h\\\": 3,\n\t\t\\\"w\\\": 2,\n\t\t\\\"xRot\\\": 70,\n\t\t\\\"yRot\\\": 20,\n\t\t\\\"zRot\\\": 10,\n\t\t\\\"r\\\": 0.7,\n\t\t\\\"g\\\": 0.2,\n\t\t\\\"b\\\": 0.9\n\t}, \n\t{\n\t\t\\\"shape\\\": \\\"cube\\\",\n\t\t\\\"x\\\": -5,\n\t\t\\\"y\\\": 4,\n\t\t\\\"z\\\": -2,\n\t\t\\\"l\\\": 3,\n\t\t\\\"h\\\": 0.8,\n\t\t\\\"w\\\": 1.2,\n\t\t\\\"xRot\\\": 30,\n\t\t\\\"yRot\\\": 50,\n\t\t\\\"zRot\\\": 0,\n\t\t\\\"r\\\": 0.9,\n\t\t\\\"g\\\": 0.0,\n\t\t\\\"b\\\": 0.3\n\t}\n]";
			jsonString = "[ { \"shape\": \"cube\", \"x\": 10, \"y\": 0, \"z\": 0, \"l\": 1, \"h\": 20, \"w\": 20, \"xRot\": 0, \"yRot\": 0, \"zRot\": 0, \"r\": 1.0, \"g\": 0.0, \"b\": 0.0 }, { \"shape\": \"cube\", \"x\": -10, \"y\": 0, \"z\": 0, \"l\": 1, \"h\": 20, \"w\": 20, \"xRot\": 0, \"yRot\": 0, \"zRot\": 0, \"r\": 0.0, \"g\": 1.0, \"b\": 0.0 }, { \"shape\": \"cube\", \"x\": 0, \"y\": 0, \"z\": 10, \"l\": 20, \"h\": 20, \"w\": 1, \"xRot\": 0, \"yRot\": 0, \"zRot\": 0, \"r\": 0.0, \"g\": 0.0, \"b\": 1.0 }, { \"shape\": \"cube\", \"x\": 0, \"y\": 0, \"z\": -10, \"l\": 20, \"h\": 20, \"w\": 1, \"xRot\": 0, \"yRot\": 0, \"zRot\": 0, \"r\": 0.0, \"g\": 0.0, \"b\": 0.0 }, { \"shape\": \"cube\", \"x\": 4, \"y\": 5, \"z\": 3, \"l\": 1.2, \"h\": 3, \"w\": 2, \"xRot\": 70, \"yRot\": 20, \"zRot\": 10, \"r\": 0.7, \"g\": 0.2, \"b\": 0.9 },  { \"shape\": \"cube\", \"x\": -5, \"y\": 4, \"z\": -2, \"l\": 3, \"h\": 0.8, \"w\": 1.2, \"xRot\": 30, \"yRot\": 50, \"zRot\": 0, \"r\": 0.9, \"g\": 0.0, \"b\": 0.3 } ]";
			var json_array = JSON.Parse (jsonString);
			for (int i = 0; i < json_array.AsArray.Count; i++) {
				var json_obj = json_array.AsArray [i];
				float x = json_obj ["x"].AsFloat;
				float y = json_obj ["y"].AsFloat;
				float z = json_obj ["z"].AsFloat;
				float length = json_obj ["l"].AsFloat;
				float height = json_obj ["h"].AsFloat;
				float width = json_obj ["w"].AsFloat;
				float xRot = json_obj ["xRot"].AsFloat;
				float yRot = json_obj ["yRot"].AsFloat;
				float zRot = json_obj ["zRot"].AsFloat;
				float r = json_obj ["r"].AsFloat;
				float g = json_obj ["g"].AsFloat;
				float b = json_obj ["b"].AsFloat;
				Color color = new Color (r, g, b);

				GameObject cube = GameObject.CreatePrimitive (PrimitiveType.Cube);
				cube.transform.position = new Vector3 (x, y - offset, z);
				cube.transform.localScale = new Vector3 (length, height, width);
				cube.transform.Rotate (new Vector3 (xRot, yRot, zRot));
				cube.GetComponent<MeshRenderer> ().material.color = color;

				priorityQueue.Add ((int)Vector3.Distance (cube.transform.position, origin), cube);
			}
			AudioSource.PlayClipAtPoint (entrance, transform.position);
			hasRun = true;
		}
		if (hasRun) {
			if (priorityQueue.Count > 0) {
				GameObject cube = priorityQueue.RemoveMin ();
				Vector3 firstPosition = cube.transform.position;
				Vector3 secondPosition = new Vector3 (firstPosition.x, firstPosition.y + offset, firstPosition.z);
				iTween.MoveTo (cube, secondPosition, 1.2F * multiplier);
				multiplier *= 2;
			} else {
				camera.SetActive (false);
				Destroy (platform, 5);
			}
		}
	}
}
