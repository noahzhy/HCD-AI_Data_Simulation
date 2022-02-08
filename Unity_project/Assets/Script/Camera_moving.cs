using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_moving : MonoBehaviour
{
    private float y0 = 0f;
    public float speed = 100;
    public float offset = 1.0f;
    public float amplitude = 0.5f;

    public float fov_init = 50f;
    public float fov_scope = 25f;

    public float sin_speed = 1.0f;
    public float cos_speed = 3.0f;
    const float step = Mathf.PI / 10;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        y0 = amplitude * Mathf.Sin(Time.time * sin_speed);
        this.GetComponent<Camera>().fieldOfView = fov_init + (Mathf.Cos(Time.time * cos_speed))*fov_scope;
        // this.transform.RotateAround(new Vector3(0, 0, 0), Vector3.up, speed * Time.deltaTime);
        this.transform.localPosition = new Vector3(
            this.transform.localPosition.x,
            y0 + offset,
            this.transform.localPosition.z);
    }

    public static float easeInSine(float start, float end, float value)
    {
        end -= start;
        return -end * Mathf.Cos(value / 1 * (Mathf.PI / 2)) + end + start;
    }
}
