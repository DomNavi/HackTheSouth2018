using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class CameraController : MonoBehaviour {
    //public WebCamTexture mCamera = null;
    public GameObject plane;
    public Text debugText;
    //public SpriteRenderer cameraPane;
    private WebCamTexture webcamTexture = null;
    public SpriteRenderer backgroundSprite;
    private EdgeManager eManager;

    // Use this for initialization
    void Start() {
        // Get relevant objects.
        eManager = GetComponent<EdgeManager>();
        plane = GameObject.FindWithTag("CameraPlane");

        // Setup camera devices.
        WebCamDevice[] devices = WebCamTexture.devices;
        webcamTexture = new WebCamTexture(Screen.width, Screen.height);

        if (devices.Length > 0) {
            webcamTexture.deviceName = devices[0].name;
            webcamTexture.Play();
        }

        plane.GetComponent<Renderer>().material.mainTexture = webcamTexture;
        //debugText.text = devices[0].name;
    }

    public void TakePicture() {

        // Set background.
        Color32[] imagePixels = webcamTexture.GetPixels32();
        Texture2D texture = new Texture2D(webcamTexture.width, webcamTexture.height);
        texture.SetPixels32(imagePixels);
        texture.Apply();
        Sprite sprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100.0f);
        backgroundSprite.sprite = sprite;

        eManager.DoMagic(imagePixels, webcamTexture.width, webcamTexture.height);

        // Extracting edges.
        List<List<Vector2>> edges = eManager.ExtractEdges(imagePixels, webcamTexture.width, webcamTexture.height);

    }

    // Update is called once per frame
    void Update() {

    }
}