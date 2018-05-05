using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CameraController : MonoBehaviour {
    //public WebCamTexture mCamera = null;
    public GameObject plane;
    public Text debugText;
    //public SpriteRenderer cameraPane;
    private WebCamTexture webcamTexture = null;
    public SpriteRenderer backgroundSprite;

    // Use this for initialization
    void Start() {
        Debug.Log("Script has been started");
        plane = GameObject.FindWithTag("CameraPlane");

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
        Color32[] imagePixels = webcamTexture.GetPixels32();
        Texture2D texture = new Texture2D(webcamTexture.width, webcamTexture.height);
        texture.SetPixels32(imagePixels);
        texture.Apply();
        Sprite sprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100.0f);
        backgroundSprite.sprite = sprite;
    }

    // Update is called once per frame
    void Update() {

    }
}