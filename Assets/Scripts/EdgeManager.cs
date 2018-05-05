using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeManager : MonoBehaviour {

    public List<List<Vector2>> ExtractEdges(Color32[] pixels, int width, int height) {
        List<List<Vector2>> edges = new List<List<Vector2>>();
        List<Vector2> newEdge1 = new List<Vector2>();
        List<Vector2> newEdge2 = new List<Vector2>();
        for (int x = 0; x < 500; x++) {
            newEdge1.Add(new Vector2(x, 250));
            newEdge2.Add(new Vector2(x, 800));
        }
        edges.Add(newEdge1);
        edges.Add(newEdge2);

        return edges;
    }

    public void DoMagic(Color32[] pixels, int width, int height) {

        byte[] data = new byte[width * height * 3];

        for(int i = 0; i < pixels.Length; i++) {
            data[i*3] = pixels[i].r;
            data[i * 3 + 1] = pixels[i].g;
            data[i * 3 + 2] = pixels[i].b;
        }

        //byte[] bytes = magicFunction(data, width, height, 1);
        //captureImageBox.Image = img;

    }
}
