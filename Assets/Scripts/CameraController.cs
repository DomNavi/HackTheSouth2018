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
    public int Threshold = 100;

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

        Color32[] colors = eManager.ApplyOperator(texture);
        Color32[] colorsBefore = texture.GetPixels32();

        for(int i = 0; i < colors.Length; i++) {
            if(colors[i].r > Threshold) {
                colorsBefore[i].r = 255;
                colorsBefore[i].g = 0;
                colorsBefore[i].b = 0;
            }
        }
        texture.SetPixels32(colorsBefore);
        texture.Apply();

        // Extracting edges.
        eManager.ExtractEdges(imagePixels, webcamTexture.width, webcamTexture.height);

    }

    // Update is called once per frame
    void Update() {

    }
}