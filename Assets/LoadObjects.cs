using UnityEngine;
using System.Collections;
using SimpleJSON;

public class LoadObjects : MonoBehaviour {

	// Use this for initialization
	void Start () {
		string jsonString = "[\n\t{\n\t\t\\\"shape\\\": \\\"cube\\\",\n\t\t\\\"x\\\": 0,\n\t\t\\\"y\\\": 0,\n\t\t\\\"z\\\": 0,\n\t\t\\\"l\\\": 1.2,\n\t\t\\\"h\\\": 3,\n\t\t\\\"w\\\": 2,\n\t\t\\\"xRot\\\": 70,\n\t\t\\\"yRot\\\": 20,\n\t\t\\\"zRot\\\": 10,\n\t\t\\\"r\\\": 0.7,\n\t\t\\\"g\\\": 0.2,\n\t\t\\\"b\\\": 0.9\n\t}, \n\t{\n\t\t\\\"shape\\\": \\\"cube\\\",\n\t\t\\\"x\\\": -5,\n\t\t\\\"y\\\": -1,\n\t\t\\\"z\\\": -2,\n\t\t\\\"l\\\": 3,\n\t\t\\\"h\\\": 0.8,\n\t\t\\\"w\\\": 1.2,\n\t\t\\\"xRot\\\": 30,\n\t\t\\\"yRot\\\": 50,\n\t\t\\\"zRot\\\": 0,\n\t\t\\\"r\\\": 0.9,\n\t\t\\\"g\\\": 0.0,\n\t\t\\\"b\\\": 0.3\n\t}\n]";
		var json_array = JSON.Parse (jsonString);
		for (int i = 0; i < json_array.AsArray.Count; i++) {
			var json_obj = json_array.AsArray [i];
			float x = json_obj["x"].AsFloat;
			float y = json_obj["y"].AsFloat;
			float z = json_obj["z"].AsFloat;
			float length = json_obj["l"].AsFloat;
			float height = json_obj["h"].AsFloat;
			float width = json_obj["w"].AsFloat;
			float xRot = json_obj["xRot"].AsFloat;
			float yRot = json_obj["yRot"].AsFloat;
			float zRot = json_obj["zRot"].AsFloat;
			float r = json_obj ["r"].AsFloat;
			float g = json_obj ["g"].AsFloat;
			float b = json_obj ["b"].AsFloat;
			Color color = new Color (r, g, b);

			GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
			cube.transform.position = new Vector3(x, y, z);
			cube.transform.localScale = new Vector3 (length, height, width);
			cube.transform.Rotate(new Vector3(xRot, yRot, zRot));
			cube.GetComponent<MeshRenderer> ().material.color = color;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
