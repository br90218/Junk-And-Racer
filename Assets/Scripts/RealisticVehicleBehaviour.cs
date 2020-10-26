using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealisticVehicleBehaviour : IVehicleBehaviour
{


    [SerializeField] private List<WheelInfo> wheelSets;

    
    [SerializeField] protected float torque = 400;
    [SerializeField] protected float steeringAngle = 30;

    // Start is called before the first frame update
    void Start()
    {
        
    }


    private void FixedUpdate() 
    {
        foreach(var wheelSet in wheelSets)
        {
            wheelSet.Accelerate(torque * forwardValue);
            wheelSet.Steer(steeringAngle * turnValue);
        }
    }

    internal override bool IsGrounded()
    {
        throw new System.NotImplementedException();
    }

    [System.Serializable]
    protected class WheelInfo
    {
        [SerializeField] private WheelCollider leftWheel;
        [SerializeField] private WheelCollider rightWheel;

        [SerializeField] private bool motor, canSteer;

        internal void Accelerate(float value)
        {
            if (!motor) return;
            leftWheel.motorTorque = value;
            rightWheel.motorTorque = value;
        }

        internal void Steer(float value)
        {
            if (!canSteer) return;
            leftWheel.steerAngle = value;
            rightWheel.steerAngle = value;
        }
    }


}
