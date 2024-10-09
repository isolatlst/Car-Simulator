using System.Linq;
using UnityEngine;

namespace Car
{
    enum TypesOfDrivetrains
    {
        FWD,
        AWD,
        RWD
    }

    public class Car : MonoBehaviour
    {
        private bool[] _direction = { false, false, false, false}; //fix me
        
        [Header("Steer")] 
            [SerializeField] private float _maxSteer = 20f;
        [Header("Power")] 
            [SerializeField] private float power = 50f;
        [Header("Wheels")] 
            [SerializeField] private Wheel[] _frontWheels = { };
            [SerializeField] private Wheel[] _backWheels = { };
        [Header("Type of Drivetrains")] 
            [SerializeField] private TypesOfDrivetrains _typesOfDrivetrains;
        [Header("Lights")]
            [SerializeField] private Lights[] _lights;

        private void FixedUpdate()
        {
            Turning();
            Powering(ReturnPoweringWheels());
            Lights();
            CheckDirection();
        }

        private void Turning()
        {
            foreach (var wheelCollider in _frontWheels)
            {
                wheelCollider.Steer(
                    Input.GetAxis("Horizontal") * _maxSteer);
            }
        }

        private Wheel[] ReturnPoweringWheels()
        {
            switch (_typesOfDrivetrains)
            {
                case TypesOfDrivetrains.AWD:
                {
                    return _frontWheels;
                }
                case TypesOfDrivetrains.RWD:
                {
                    return _backWheels;
                }
                case TypesOfDrivetrains.FWD:
                {
                    return _frontWheels.Concat(_backWheels).ToArray();
                }
            }
            return null;
        }

        private void Powering(Wheel[] wheels)
        {
            foreach (var wheelCollider in wheels)
            {
                wheelCollider.Torque(
                    Input.GetAxis("Vertical") * power * Time.deltaTime);
            }
        }

        private void CheckDirection()
        {
            if (Input.GetAxis("Vertical") >= 0)
            {
                _direction[0] = true;
                _direction[2] = false;
            }
            else
            {
                _direction[0] = false;
                _direction[2] = true;
            }
            if (Input.GetAxis("Horizontal") > 0)
            {
                _direction[1] = true;
                _direction[3] = false; 
            }
            else
            {
                _direction[1] = false;
                _direction[3] = true;
            }
        } //#fix me
        
        private void Lights()
        {
            for (uint i = 0; i < _lights.Length; i++)
            {
                _lights[i].FireLights(_direction[i]);
            }
        }
    }
}