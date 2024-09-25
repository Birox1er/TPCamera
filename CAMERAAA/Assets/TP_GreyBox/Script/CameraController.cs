using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private CameraConfig config;
    [SerializeField] private Camera cam;
    // Update is called once per frame
    private static CameraController instance = null;
    public static CameraController Instance => instance;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);

        // Initialisation du Game Manager...
    }
    void Update()
    {
        ApplyConfig();
    }
    private void ApplyConfig()
    {
        transform.localPosition = config.GetPosition();
        transform.rotation = config.GetRotation();
        cam.fieldOfView = config.fov;
    }
    private void OnDrawGizmos()
    {
        config.DrawGizmos(Color.red);
    }
}
[Serializable]
struct CameraConfig
{
    public float yaw;
    public float pitch;
    public float roll;
    public Vector3 pivot;
    public float distance;
    public float fov;

    public Quaternion GetRotation()
    {
        return Quaternion.Euler(yaw, pitch, roll);
    }

    public Vector3 GetPosition()
    {
        return pivot+(GetRotation()*(Vector3.back * distance));
    }
    public void DrawGizmos(Color color)
    {
        Gizmos.color = color;
        Gizmos.DrawSphere(pivot, 0.25f);
        Vector3 position = GetPosition();
        Gizmos.DrawLine(pivot, position);
        Gizmos.matrix = Matrix4x4.TRS(position, GetRotation(), Vector3.one);
        Gizmos.DrawFrustum(Vector3.zero, fov, 0.5f, 0f, Camera.main.aspect);
        Gizmos.matrix = Matrix4x4.identity;
    }
}
