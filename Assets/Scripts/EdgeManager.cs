using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;
using System;
using System.Drawing;
using System.IO;

public class EdgeManager : MonoBehaviour {
    List<List<Vector2>> edges = new List<List<Vector2>>();
    private bool ready = false;

    public void ExtractEdges(Color32[] pixels, int width, int height) {
        edges.Clear();
        List<Vector2> newEdge1 = new List<Vector2>();
        List<Vector2> newEdge2 = new List<Vector2>();
        for (int x = 0; x < 500; x++) {
            newEdge1.Add(new Vector2(x, 250));
            newEdge2.Add(new Vector2(x, 800));
        }
        edges.Add(newEdge1);
        edges.Add(newEdge2);
        ready = true;
    }

    public bool isReady() {
        return ready;
    }

    public List<List<Vector2>> GetEdges() {
        return edges;
    }

    public Color32[] ApplyOperator(Texture2D txt) {

        int h = txt.height;
        int w = txt.width;

        Color32[] colorsRaw = txt.GetPixels32();

        Bitmap bitmap = new Bitmap(w, h);
        for(int x = 0; x < w; x++) {
            for (int y = 0; y < h; y++) {
                Color32 tmp = colorsRaw[w * y + x];
                bitmap.SetPixel(x, y, System.Drawing.Color.FromArgb(tmp.r, tmp.g, tmp.b));
            }
        }

        Image<Bgr, Byte> imageCV = new Image<Bgr, byte>(bitmap);
        Mat img = imageCV.Mat;

        CvInvoke.CvtColor(img, img, ColorConversion.Bgr2Gray);
        CvInvoke.PyrDown(img, img);
        CvInvoke.PyrUp(img, img);
        CvInvoke.Canny(img, img, 100, 60);
        byte[] after = img.GetData();

        Color32[] colors = new Color32[after.Length];
        for(int i = 0; i < after.Length; i++) {
            colors[i].r = after[i];
            colors[i].g = after[i];
            colors[i].b = after[i];
            colors[i].a = 255;
        }

        return colors;
        //byte[] bytes = magicFunction(data, width, height, 1);
        //captureImageBox.Image = img;

    }
}
