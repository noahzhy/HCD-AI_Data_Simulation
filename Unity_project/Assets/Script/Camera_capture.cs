using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class Camera_capture : MonoBehaviour
{
    public float capture_holding = 1.0f;
    public int width = 1280;
    public int height = 720;

    public int counter_start = 0;
    public int counter_end = 10;

    public Camera BackgroundCamera;
    private Camera camera;
    protected Texture2D imageShot;
    protected Texture2D labelShot;

    // Start is called before the first frame update
    void Start()
    {
        camera = this.GetComponent<Camera>();
        imageShot = new Texture2D(width, height, TextureFormat.RGB24, false);
        labelShot = new Texture2D(width, height, TextureFormat.RGB24, false);

        InvokeRepeating("Capture", 0f, capture_holding);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Capture() {
        if (counter_start < counter_end) {
            string filename = counter_start.ToString("D6");
            CamCapture(filename);
            counter_start += 1;
        } else {
            EditorApplication.isPlaying = false;
        }
    }

    void SaveScreenShot(Texture2D imageShot, string filename, string format) {
        byte[] bytes = imageShot.EncodeToPNG();
        string savePath = Application.dataPath + "/images/" + filename + format;
        System.IO.File.WriteAllBytes(savePath, bytes);
        Debug.Log(string.Format("Save Path: {0}", savePath));
    }

    void CamCapture(string filename) {
        RenderTexture img_rt = RenderTexture.GetTemporary(width, height, 24);
        RenderTexture lab_rt = RenderTexture.GetTemporary(width, height, 24);

        // merge camera and background
        BackgroundCamera.targetTexture = img_rt;
        BackgroundCamera.Render();
        camera.targetTexture = img_rt;
        camera.Render();

        RenderTexture.active = img_rt;
        imageShot.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        imageShot.Apply();

        // only camera (for labels)
        camera.targetTexture = lab_rt;
        camera.Render();

        RenderTexture.active = lab_rt;
        labelShot.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        labelShot.Apply();

        camera.targetTexture = null;
        BackgroundCamera.targetTexture = null;
        RenderTexture.active = null;

        RenderTexture.ReleaseTemporary(img_rt);
        RenderTexture.ReleaseTemporary(lab_rt);

        SaveScreenShot(imageShot, filename, ".jpg");
        SaveScreenShot(labelShot, filename, ".png");
    }
}
