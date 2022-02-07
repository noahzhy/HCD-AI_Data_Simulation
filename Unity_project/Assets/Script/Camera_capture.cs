using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Camera_capture : MonoBehaviour
{
    public float capture_holding = 1.0f;
    public int width = 1280;
    public int height = 720;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Capture", 0f, capture_holding);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Capture() {
        string filename = "test";
        
        camCapture(filename);
        labelCapture(filename);
    }

    void camCapture(string filename) {
        RenderTexture rt = new RenderTexture(width, height, -1);
        Camera camera = this.GetComponent<Camera>();
        camera.targetTexture = rt;
        camera.Render();

        RenderTexture.active = rt;
        Texture2D screenShot = new Texture2D(width, height, TextureFormat.RGB24, false);
        screenShot.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        screenShot.Apply();

        camera.targetTexture = null;
        RenderTexture.active = null;
        GameObject.Destroy(rt);

        byte[] bytes = screenShot.EncodeToPNG();
        string savePath = Application.dataPath + "/" + filename + ".jpg";
        System.IO.File.WriteAllBytes(savePath, bytes);
        Debug.Log(string.Format("Save Path: {0}", savePath));
    }

    void labelCapture(string filename) {

    }
}
