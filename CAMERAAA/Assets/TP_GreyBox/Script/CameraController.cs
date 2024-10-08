using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEditor;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Camera cam;

    [SerializeField] private CameraConfig targetConfig;
    private CameraConfig currentConfig;

    [SerializeField] private float speed = 1;

    List<AView> activeViews=new();

    private static CameraController instance = null;
    public static CameraController Instance => instance;
    private void Awake()
    {
        currentConfig = ComputeAverage();

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
        targetConfig = ComputeAverage();
        ApplyConfig();
    }
    private void ApplyConfig()
    {
        CalculateCurrentConfig();
        transform.position = currentConfig.GetPosition();
        transform.rotation = currentConfig.GetRotation();
        cam.fieldOfView = currentConfig.fov;
    
    }

    private void CalculateCurrentConfig()
    {
        if (speed * Time.deltaTime >= 1)
        {
            currentConfig = targetConfig;
            return;
        }

        currentConfig.pitch = currentConfig.pitch + (targetConfig.pitch - currentConfig.pitch) * speed * Time.deltaTime;
        currentConfig.roll = currentConfig.roll + (targetConfig.roll -currentConfig.roll) * speed * Time.deltaTime;
        currentConfig.pivot = currentConfig.pivot + (targetConfig.pivot - currentConfig.pivot) * speed * Time.deltaTime;
        currentConfig.distance = currentConfig.distance + (targetConfig.distance - currentConfig.distance) * speed * Time.deltaTime;
        currentConfig.fov = currentConfig.fov + (targetConfig.fov - currentConfig.fov) * speed * Time.deltaTime;

        Vector2 targetYaw = new Vector2(Mathf.Cos(targetConfig.yaw * Mathf.Deg2Rad),
            Mathf.Sin(targetConfig.yaw * Mathf.Deg2Rad));
        Vector2 currentYaw = new Vector2(Mathf.Cos(currentConfig.yaw * Mathf.Deg2Rad),
            Mathf.Sin(currentConfig.yaw * Mathf.Deg2Rad));

        currentYaw = currentYaw + (targetYaw - currentYaw) * speed * Time.deltaTime;
        currentConfig.yaw = Vector2.SignedAngle(Vector2.right, currentYaw);
    }

    private void OnDrawGizmos()
    {
        targetConfig.DrawGizmos(Color.red);
    }

    public void AddView(AView view) { activeViews.Add(view); }
    public void RemoveView(AView view) { activeViews.Remove(view); }

    private CameraConfig ComputeAverage() 
    {
        Vector3 pivotSum = Vector3.zero;
        float distanceSum = 0f;
        float fovSum = 0f;
        float weightSum = 0;
        foreach (AView view in activeViews)
        {
            pivotSum += view.GetConfiguration().pivot * view.weight;
            distanceSum += view.GetConfiguration().distance * view.weight;
            fovSum += view.GetConfiguration().fov * view.weight;
            weightSum += view.weight;
        }
        weightSum = weightSum == 0 ? 1 : weightSum;

            return new CameraConfig(ComputeAverageYaw(), 
                ComputeAveragePitch(), 
                ComputeAverageRoll(), 
                pivotSum/weightSum, 
                distanceSum/weightSum, 
                fovSum/weightSum);
    }

    public float ComputeAverageYaw()
    {
        Vector2 sum = Vector2.zero;
        foreach (AView view in activeViews)
        {
            CameraConfig config = view.GetConfiguration();
            sum += new Vector2(Mathf.Cos(config.yaw * Mathf.Deg2Rad),
            Mathf.Sin(config.yaw * Mathf.Deg2Rad)) * view.weight;
        }
        return Vector2.SignedAngle(Vector2.right, sum);
    }

    public float ComputeAveragePitch()
    {
        Vector2 sum = Vector2.zero;
        foreach (AView view in activeViews)
        {
            CameraConfig config = view.GetConfiguration();
            sum += new Vector2(Mathf.Cos(config.pitch * Mathf.Deg2Rad),
            Mathf.Sin(config.pitch * Mathf.Deg2Rad)) * view.weight;
        }
        return Vector2.SignedAngle(Vector2.right, sum);
    }

    public float ComputeAverageRoll()
    {
        Vector2 sum = Vector2.zero;
        foreach (AView view in activeViews)
        {
            CameraConfig config = view.GetConfiguration();
            sum += new Vector2(Mathf.Cos(config.roll * Mathf.Deg2Rad),
            Mathf.Sin(config.roll * Mathf.Deg2Rad)) * view.weight;
        }
        return Vector2.SignedAngle(Vector2.right, sum);
    }
}
[Serializable]
public struct CameraConfig
{
    public float yaw;
    public float pitch;
    public float roll;
    public Vector3 pivot;
    public float distance;
    public float fov;

    public CameraConfig(float yaw, float pitch, float roll, Vector3 pivot, float distance, float fov)
    {
        this.yaw = yaw;
        this.pitch = pitch;
        this.roll = roll;
        this.pivot = pivot;
        this.distance = distance;
        this.fov = fov;
    }

    public Quaternion GetRotation()
    {
        return Quaternion.Euler( pitch, yaw, roll);
    }

    public Vector3 GetPosition()
    {
        //Debug.Log("the PIVOT is " + pivot);
        return pivot+(GetRotation()*(Vector3.back * distance));
    }
    public void DrawGizmos(Color color)
    {
        Gizmos.color = color;
        Gizmos.DrawSphere(pivot, 0.25f);
        Vector3 position = GetPosition();
        Gizmos.DrawLine(pivot, position);
        Gizmos.matrix = Matrix4x4.TRS(position, GetRotation(), Vector3.one);
        Gizmos.DrawFrustum(Vector3.zero, fov, 5f, 0f, Camera.main.aspect);
        Gizmos.matrix = Matrix4x4.identity;
    }
}
