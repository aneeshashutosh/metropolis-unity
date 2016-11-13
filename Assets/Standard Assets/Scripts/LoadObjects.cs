using UnityEngine;
using System.Collections;
using SimpleJSON;

public class LoadObjects : MonoBehaviour
{
	PriorityQueue<GameObject> priorityQueue;
	Vector3 origin = new Vector3 (30, 10, 15);
	static int offset = 32;
	static float multiplier = 1;
	public AudioClip entrance;
	public GameObject platform;
	public GameObject plane;
	public GameObject camera;
	public GameObject dummy;
	public static string jsonString;
	float originalY;
	public static bool hasRun = false;

	// Use this for initialization
	void Start ()
	{
		if (!hasRun) {
			originalY = plane.transform.position.y;
			// plane.transform.position = new Vector3 (plane.transform.position.x, plane.transform.position.y - 2000, plane.transform.position.z);
		}
	}

	void Update ()
	{
		if (jsonString != null && !hasRun) {
			hasRun = true;
			plane.transform.position = new Vector3 (plane.transform.position.x, originalY, plane.transform.position.z);

			priorityQueue = new PriorityQueue<GameObject> ();

			// jsonString = "[\n\t{\n\t\t\\\"shape\\\": \\\"cube\\\",\n\t\t\\\"x\\\": 4,\n\t\t\\\"y\\\": 5,\n\t\t\\\"z\\\": 3,\n\t\t\\\"l\\\": 1.2,\n\t\t\\\"h\\\": 3,\n\t\t\\\"w\\\": 2,\n\t\t\\\"xRot\\\": 70,\n\t\t\\\"yRot\\\": 20,\n\t\t\\\"zRot\\\": 10,\n\t\t\\\"r\\\": 0.7,\n\t\t\\\"g\\\": 0.2,\n\t\t\\\"b\\\": 0.9\n\t}, \n\t{\n\t\t\\\"shape\\\": \\\"cube\\\",\n\t\t\\\"x\\\": -5,\n\t\t\\\"y\\\": 4,\n\t\t\\\"z\\\": -2,\n\t\t\\\"l\\\": 3,\n\t\t\\\"h\\\": 0.8,\n\t\t\\\"w\\\": 1.2,\n\t\t\\\"xRot\\\": 30,\n\t\t\\\"yRot\\\": 50,\n\t\t\\\"zRot\\\": 0,\n\t\t\\\"r\\\": 0.9,\n\t\t\\\"g\\\": 0.0,\n\t\t\\\"b\\\": 0.3\n\t}\n]";
			// jsonString = "[ { \"shape\": \"cube\", \"x\": 10, \"y\": 0, \"z\": 0, \"l\": 1, \"h\": 20, \"w\": 20, \"xRot\": 0, \"yRot\": 0, \"zRot\": 0, \"r\": 1.0, \"g\": 0.0, \"b\": 0.0 }, { \"shape\": \"cube\", \"x\": -10, \"y\": 0, \"z\": 0, \"l\": 1, \"h\": 20, \"w\": 20, \"xRot\": 0, \"yRot\": 0, \"zRot\": 0, \"r\": 0.0, \"g\": 1.0, \"b\": 0.0 }, { \"shape\": \"cube\", \"x\": 0, \"y\": 0, \"z\": 10, \"l\": 20, \"h\": 20, \"w\": 1, \"xRot\": 0, \"yRot\": 0, \"zRot\": 0, \"r\": 0.0, \"g\": 0.0, \"b\": 1.0 }, { \"shape\": \"cube\", \"x\": 0, \"y\": 0, \"z\": -10, \"l\": 20, \"h\": 20, \"w\": 1, \"xRot\": 0, \"yRot\": 0, \"zRot\": 0, \"r\": 0.0, \"g\": 0.0, \"b\": 0.0 }, { \"shape\": \"cube\", \"x\": 4, \"y\": 5, \"z\": 3, \"l\": 1.2, \"h\": 3, \"w\": 2, \"xRot\": 70, \"yRot\": 20, \"zRot\": 10, \"r\": 0.7, \"g\": 0.2, \"b\": 0.9 },  { \"shape\": \"cube\", \"x\": -5, \"y\": 4, \"z\": -2, \"l\": 3, \"h\": 0.8, \"w\": 1.2, \"xRot\": 30, \"yRot\": 50, \"zRot\": 0, \"r\": 0.9, \"g\": 0.0, \"b\": 0.3 } ]";
			// jsonString = "[{\"shape\": \"cube\", \"b\": 0.0, \"zRot\": 0, \"z\": 14.987523777973578, \"g\": 0.0, \"h\": 7.8389844010126772, \"l\": 6.5822720125090584, \"r\": 1.0, \"w\": 4.7693756667541729, \"y\": 0, \"x\": 15.006755015776694, \"xRot\": 0, \"yRot\": 0}, {\"shape\": \"cube\", \"b\": 0.0, \"zRot\": 0, \"z\": 6.5845791670206522, \"g\": 0.0, \"h\": 8, \"l\": 7.9760696058649216, \"r\": 1.0, \"w\": 5.422768113854775, \"y\": 0, \"x\": 5.6210168816383144, \"xRot\": 0, \"yRot\": 0}]";
			var json_array = JSON.Parse (jsonString);
			for (int i = 0; i < json_array.AsArray.Count; i++) {
				print ("Cube #" + (i + 1));
				var json_obj = json_array.AsArray [i];
				float x = json_obj ["z"].AsFloat;
				float y = json_obj ["y"].AsFloat;
				float z = json_obj ["x"].AsFloat;
				float length = json_obj ["w"].AsFloat;
				float height = json_obj ["h"].AsFloat;
				float width = json_obj ["l"].AsFloat;
				float xRot = json_obj ["xRot"].AsFloat;
				float yRot = json_obj ["yRot"].AsFloat;
				float zRot = json_obj ["zRot"].AsFloat;
				float r = json_obj ["r"].AsFloat;
				float g = json_obj ["g"].AsFloat;
				float b = json_obj ["b"].AsFloat;
				print ("x: " + x);
				print ("y: " + y);
				print ("z: " + z);
				print ("length: " + length);
				print ("height: " + height);
				print ("width: " + width);
				print ("Adjusted x: " + (x + length / 2));
				print ("Adjusted y: " + (y + height / 2));
				print ("Adjusted z: " + (z + width / 2));

				Color color = new Color (r, g, b);

				GameObject cube = GameObject.CreatePrimitive (PrimitiveType.Cube);
				cube.transform.position = SetPosition (x, y, z, length, width, height);
				cube.transform.localScale = new Vector3 (length, height, width);
				cube.transform.Rotate (new Vector3 (xRot, yRot, zRot));
				cube.transform.parent = dummy.transform;
				cube.GetComponent<MeshRenderer> ().material.color = color;

				priorityQueue.Add ((int)Vector3.Distance (cube.transform.position, origin), cube);
			}

			DrawBounds ();

			AudioSource.PlayClipAtPoint (entrance, transform.position);
			hasRun = true;
		}
		if (hasRun) {
			if (priorityQueue.Count > 0) {
				GameObject cube = priorityQueue.RemoveMin ();
				Vector3 firstPosition = cube.transform.position;
				Vector3 secondPosition = new Vector3 (firstPosition.x, firstPosition.y + offset, firstPosition.z);
				iTween.MoveTo (cube, secondPosition, 1.6F * multiplier);
				multiplier *= 1.4F;
			} else {
				camera.GetComponent<Renderer>().enabled = false;
			}
		}
	}

	private Vector3 SetPosition (float x, float y, float z, float length, float height, float width)
	{
		int Z_MAX = 21;
		int Z_MIN = 0;

		float newX = x + length / 2;
		float newY = y + height / 2;
		float newZ = z + width / 2;

		if (newZ + width / 2 > Z_MAX)
			newZ = Z_MAX - width / 2;
		if (newZ - width / 2 < Z_MIN)
			newZ = Z_MIN + width / 2;

		return new Vector3 (newX, newY - offset, newZ);
	}

	private void DrawBounds ()
	{
		GameObject wall1 = GameObject.CreatePrimitive (PrimitiveType.Cube);
		wall1.transform.position = new Vector3 (-1, 10 - offset, 10);
		wall1.transform.localScale = new Vector3 (23, 20, 1);
		wall1.transform.Rotate (new Vector3 (0, 90, 0));
		wall1.transform.parent = dummy.transform;
		wall1.GetComponent<MeshRenderer> ().material.color = Color.white;

		priorityQueue.Add ((int)Vector3.Distance (wall1.transform.position, origin), wall1);

		GameObject wall2 = GameObject.CreatePrimitive (PrimitiveType.Cube);
		wall2.transform.position = new Vector3 (10, 10 - offset, -1);
		wall2.transform.localScale = new Vector3 (21, 20, 1);
		wall2.transform.Rotate (new Vector3 (0, 0, 0));
		wall2.transform.parent = dummy.transform;
		wall2.GetComponent<MeshRenderer> ().material.color = Color.white;

		priorityQueue.Add ((int)Vector3.Distance (wall2.transform.position, origin), wall2);

		GameObject wall3 = GameObject.CreatePrimitive (PrimitiveType.Cube);
		wall3.transform.position = new Vector3 (10, 10 - offset, 21);
		wall3.transform.localScale = new Vector3 (21, 20, 1);
		wall3.transform.Rotate (new Vector3 (0, 0, 0));
		wall3.transform.parent = dummy.transform;
		wall3.GetComponent<MeshRenderer> ().material.color = Color.white;

		priorityQueue.Add ((int)Vector3.Distance (wall3.transform.position, origin), wall3);
	}

	public static void Reset ()
	{
		multiplier = 1;
		print ("resetting...");
		GameObject cubeContainer = GameObject.FindWithTag ("cubeHolder");
		GameObject player = GameObject.FindWithTag ("Player");
		player.transform.position = new Vector3 (30, 10, 15);
		Component[] allChildren = cubeContainer.GetComponentsInChildren<Transform>();
		for (var ac = 0; ac < allChildren.Length; ac++) {
			var cube = allChildren [ac].gameObject;
			Vector3 firstPosition = cube.transform.position;
			Vector3 secondPosition = new Vector3 (firstPosition.x, firstPosition.y - (float) offset, firstPosition.z);
			iTween.MoveTo (cube, secondPosition, 1.6F * multiplier);
			multiplier *= 1.2F;
		}

		for (var ac = 0; ac < allChildren.Length; ac++) {
			Destroy (allChildren [ac].gameObject);
		}

		GameObject ccamera = GameObject.FindWithTag ("webcam");
		ccamera.GetComponent<Renderer>().enabled = true;

		CameraController.isPhotoTaken = false;
		CameraController.hasStarted = false;
		HTTPManager.hasRun = false;
		LoadObjects.jsonString = null;
		LoadObjects.hasRun = false;
		multiplier = 1;
		CameraController[] others = FindObjectsOfType(typeof(CameraController)) as CameraController[];
		foreach(CameraController other in others)
		{
			Destroy(other);
		}

		CameraController sc = ccamera.AddComponent( typeof(CameraController) ) as CameraController;
	}
}
