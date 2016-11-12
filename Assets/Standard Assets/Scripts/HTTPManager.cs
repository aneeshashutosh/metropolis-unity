using UnityEngine;
using System.Collections;

public class HTTPManager : MonoBehaviour
{
	string imageAsString;

	public HTTPManager (byte[] imageData)
	{
		imageAsString = System.Text.Encoding.UTF8.GetString(imageData);
	}

	public void run ()
	{
		string url = "http://example.com/script.php";

		WWWForm form = new WWWForm ();
		form.AddField ("img", imageAsString);
//		WWW www = new WWW (url, form);
//		StartCoroutine (WaitForRequest (www));
		LoadObjects.jsonString = "hi";
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
