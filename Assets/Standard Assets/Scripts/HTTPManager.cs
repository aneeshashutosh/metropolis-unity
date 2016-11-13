using UnityEngine;
using System;
using System.Collections;

public class HTTPManager : MonoBehaviour
{
	static string imageAsString;
	public static bool hasRun = false;

	public static void setImage (byte[] imageData)
	{
		imageAsString = Convert.ToBase64String (imageData);
	}

	void Start() {

	}

	void Update ()
	{
		if (imageAsString != null && !hasRun) {
			hasRun = true;
			string url = "metropolis.black/infer";
			print (imageAsString);

			WWWForm form = new WWWForm ();
			form.AddField ("img", imageAsString);
			form.AddField ("num_boxes", 1);
			WWW www = new WWW ("https://" + url, form);
			StartCoroutine (WaitForRequest (www));
		}
	}

	IEnumerator WaitForRequest (WWW www)
	{
		yield return www;

		// check for errors
		if (www.error == null) {
			LoadObjects.jsonString = www.text;
		} else {
			Debug.Log ("WWW Error: " + www.error);
		}
	}
}
