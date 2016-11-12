using UnityEngine;
using System.Collections;
using System;

public class CameraController : MonoBehaviour
{
	public static WebCamTexture mCamera = null;
	public GameObject plane;
	public static bool isPhotoTaken = false;

	// Use this for initialization
	void Start ()
	{
		plane = GameObject.FindWithTag ("webcam");

		mCamera = new WebCamTexture ();
		plane.GetComponent<MeshRenderer> ().material.mainTexture = mCamera;
		mCamera.Play ();
	}

	// Update is called once per frame
	void Update ()
	{

	}

	public static void TakeSnapshot()
	{
		Texture2D snap = new Texture2D (mCamera.width, mCamera.height);
		snap.SetPixels (mCamera.GetPixels ());
		snap.Apply ();
		isPhotoTaken = true;

		HTTPManager httpManager = new HTTPManager (snap.EncodeToPNG ());
		httpManager.run ();
	}
}
