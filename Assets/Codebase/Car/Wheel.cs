using UnityEngine;


public class Wheel : MonoBehaviour
{
    [SerializeField] private WheelCollider _wheelCollider;
    [SerializeField] private Transform _visual;
    [SerializeField] private Vector3 _baseVisual = Vector3.up;

    private void FixedUpdate()
    {
        _wheelCollider.GetWorldPose(out _, out var quaternion);
        _visual.rotation = quaternion;
        _visual.Rotate(_baseVisual);
    }

    public void Steer(float angle)
    { 
        _wheelCollider.steerAngle = angle;
    }

    public void Torque(float power)
    {
        _wheelCollider.motorTorque = power;
    }
}