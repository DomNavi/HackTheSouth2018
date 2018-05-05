using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Painter : MonoBehaviour {

    private List<Vector2> drawPoints;

    void Update() {
        if( Input.touchCount > 0) {
            if (Input.GetTouch(0).phase == TouchPhase.Began) {
                drawPoints = new List<Vector2>();
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
        }
        //if(drawPoints != null)
        //Debug.Log(drawPoints.ToArray()[drawPoints.Count - 1]);
    }
}
