using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CamRec : MonoBehaviour
{
	public bool play = false;
	public GameObject screen;
	Renderer screenRenderer;
	Texture2D tex;

	// Use this for initialization
	void Start ()
	{
		screenRenderer = screen.GetComponent<Renderer> ();
		tex = new Texture2D (rt.width, rt.height);
		//tex2 = new Texture2D(rt.width,rt.height,tex.format,false);
	}

	public Camera cam;
	static int count = 0;

	[SerializeField]
	private List<byte[]> framebuffer = new List<byte[]> ();
	int skipFrames = 0;
	int playFrame = 0;

	public RenderTexture rt;
	byte[] testByte;
	Texture2D tex2;
	// Update is called once per frame
	void Update ()
	{

		if (!play)
		{
			// Setup a camera, texture and render texture
			//Texture2D tex = new Texture2D (rt.width, rt.height);
			//RenderTexture rt = new RenderTexture (1280, 1280, 0);

			// Render to RenderTexture
			cam.targetTexture = rt;
			cam.Render ();

			// Read pixels to texture
			RenderTexture.active = rt;
			tex.ReadPixels (new Rect (0, 0, rt.width, rt.height), 0, 0);
			tex.Apply ();
//			screenRenderer.material.mainTexture = tex;
			// Read texture to array
	//		if (skipFrames > 2)
//			{

			testByte = ImageConversion.EncodeToJPG(tex,100);
				framebuffer.Add (testByte);
		



			Debug.Log (framebuffer.Count);

//				skipFrames = 0;
//			}
			skipFrames++;
			cam.targetTexture = null;
			cam.Render ();
		}
		else
		{
			if (count > framebuffer.Count)
			{
				count = 0;
			}
//			if (playFrame > 2)
//			{

			tex.LoadImage(framebuffer[count]);
			screenRenderer.material.mainTexture = tex;//framebuffer [count];
				count++;
				cam.Render ();
				playFrame = 0;
//			}
//			playFrame++;
		}


	}
}
