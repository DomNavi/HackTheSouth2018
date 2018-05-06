using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Painter : MonoBehaviour {

    private List<Vector2> drawPoints = new List<Vector2>();
    private EdgeManager eManager;

    void Start() {
        eManager = GetComponent<EdgeManager>();   
    }

    void Update() {
        if (eManager.isReady()) {
            if( Input.touchCount > 0) {
                if (Input.GetTouch(0).phase == TouchPhase.Began) {
                    drawPoints.Clear();
                }
                if (Input.GetTouch(0).phase == TouchPhase.Moved) {
                    // Construct a ray from the current touch coordinates
                    //Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                    // Create a particle if hit
                    //if (Physics.Raycast(ray)) {
                    if (!drawPoints.Contains(Input.GetTouch(0).position)) {
                        drawPoints.Add((Input.GetTouch(0).position));
                    }
                        //Instantiate(particle, transform.position, transform.rotation);
                    //}
                }
                if (Input.GetTouch(0).phase == TouchPhase.Ended && drawPoints.Count > 0) {
                    snapToEdge();
                }
            }
            //if(drawPoints != null)
            //Debug.Log(drawPoints.ToArray()[drawPoints.Count - 1]);
        }
    }

    void snapToEdge() {
        List<List<Vector2>> edges = eManager.GetEdges();
        long[] distances = new long[edges.Count];

        int i = 0;
        foreach(List<Vector2> edge in edges) {
            foreach(Vector2 ePixel in edge) {
                long dist = long.MaxValue;
                long d;
                foreach (Vector2 dPixel in drawPoints) {
                    if((d = Mathf.RoundToInt(Vector2.Distance(dPixel,ePixel))) < dist) {
                        dist = d;
                    }
                }
                if(dist != long.MaxValue) {
                    distances[i] += dist;
                }
            }
        i++;
        }
    }
}
