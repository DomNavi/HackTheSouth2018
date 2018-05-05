using UnityEngine;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;
using System;
using ZedGraph;

public class ImageHandler : MonoBehaviour {

	// Use this for initialization
	void Start () {

        int[] data = new int[] { 0, 0, 255, 0, 0, 255, 0, 0, 255, 0, 0, 255 };
        Mat img = new Mat(2,2, CV_8UC3, data);

        CvInvoke.CvtColor(img, img, ColorConversion.Bgr2Gray);

        CvInvoke.PyrDown(img, img);

        CvInvoke.PyrUp(img, img);

        CvInvoke.Canny(img, img, 100, 60);

        //captureImageBox.Image = img;

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}


