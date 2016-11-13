using UnityEngine;

public class ClickDetector:MonoBehaviour
{
	public int button = 0;
	public float clickSize = 20;
	static ClickManager clickManager = new ClickManager();

	void ClickHappened ()
	{
		if (CameraController.isPhotoTaken) {

		} else {
			CameraController.TakeSnapshot ();
		}
	}

	Vector3 pos;

	void Update ()
	{
		if (Input.GetMouseButtonDown (button))
			pos = Input.mousePosition;

		if (Input.GetMouseButtonUp (button)) {
			if (clickManager.DoubleClick()) {
				LoadObjects.Reset ();
			}
			var delta = Input.mousePosition - pos;
			if (delta.sqrMagnitude < clickSize * clickSize)
				ClickHappened ();
		}
	}
}