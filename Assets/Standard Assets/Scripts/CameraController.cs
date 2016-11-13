using UnityEngine;
using System.Collections;
using System;

public class CameraController : MonoBehaviour
{
	public static WebCamTexture mCamera = null;
	public static GameObject plane;
	public static GameObject canvas;
	public static bool isPhotoTaken = false;
	public static bool hasStarted = false;

	// Use this for initialization
	void Start ()
	{
		
	}

	// Update is called once per frame
	void Update ()
	{
		print ("up");
		if (!hasStarted) {
			print ("started");
			hasStarted = true;
			plane = GameObject.FindWithTag ("webcam");
			canvas = GameObject.FindWithTag ("canvas");

			mCamera = new WebCamTexture ();
			plane.GetComponent<MeshRenderer> ().material.mainTexture = mCamera;
			mCamera.Play ();
		}
	}

	void FixedUpdate ()
	{
		print ("up");
		if (!hasStarted) {
			print ("started");
			hasStarted = true;
			plane = GameObject.FindWithTag ("webcam");
			canvas = GameObject.FindWithTag ("canvas");

			mCamera = new WebCamTexture ();
			plane.GetComponent<MeshRenderer> ().material.mainTexture = mCamera;
			mCamera.Play ();
		}
	}

	public static void TakeSnapshot()
	{
		print ("snapshot");
		Texture2D snap = new Texture2D (mCamera.width, mCamera.height);
		snap.SetPixels (mCamera.GetPixels ());
		snap.Apply ();
		isPhotoTaken = true;

		plane.GetComponent<MeshRenderer> ().material.mainTexture = snap;
		canvas.GetComponent<MeshRenderer> ().material.mainTexture = snap;

		HTTPManager.setImage (snap.EncodeToPNG());
	}
}
