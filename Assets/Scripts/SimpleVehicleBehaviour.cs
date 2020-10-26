using UnityEngine;

public class SimpleVehicleBehaviour : IVehicleBehaviour
{

    [SerializeField] private AnimationCurve turnInputCurve = AnimationCurve.Linear(-1.0f, -1.0f, 1.0f, 1.0f);

    [Header("Wheels")]
    [SerializeField] private WheelCollider[] poweredWheels;
    [SerializeField] private WheelCollider[] turningWheels;


    [Header("Motor")]
    /*
    *  Motor torque represent the torque sent to the wheels by the motor with x: speed in km/h and y: torque
    *  The curve should start at x=0 and y>0 and should end with x>topspeed and y<0
    *  The higher the torque the faster it accelerate
    *  the longer the curve the faster it gets
    */
    [SerializeField] AnimationCurve motorTorque = new AnimationCurve(new Keyframe(0, 200), new Keyframe(50, 300), new Keyframe(200, 0));

    [Range(2, 16)]
    [SerializeField] private float diffGearing = 4.0f;
    [SerializeField] private float brakeForce = 1500f;

    /// <summary>
    /// maximum steering angle of the vehicle.
    /// </summary>
    [Range(0f, 50f)]
    [SerializeField] private float steerAngle = 30.0f;

    [Range(0.001f, 1f)]
    [SerializeField] private float steerSpeed = 0.2f;
    [SerializeField] private Transform centerofMass;
    [Range(0.5f, 10f)]
    [SerializeField] private float downForce = 1f;

    [Header("Aerodynamics")]
    [SerializeField] private float rotateAcceleration = 40f;



    private Rigidbody rb;
    private WheelCollider[] wheels;
    private float currentSpeed;
    private Vector2 currentRotationSpeed;




    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        rb.centerOfMass = centerofMass.localPosition;
        wheels = GetComponentsInChildren<WheelCollider>();

        // Set the motor torque to a non null value because 0 means the wheels won't turn no matter what
        foreach (var wheel in wheels)
        {
            wheel.motorTorque = 0.001f;
        }
        currentRotationSpeed = Vector2.zero;

    }

    private void FixedUpdate()
    {

        // Getting current speed
        // NOTE: why is it 3.6?
        currentSpeed = transform.InverseTransformDirection(rb.velocity).z * 3.6f;


        if (IsGrounded())
        {
            // apply steering
            var steering = turnInputCurve.Evaluate(turnValue) * steerAngle;

            foreach (var wheel in turningWheels)
            {
                wheel.steerAngle = Mathf.Lerp(wheel.steerAngle, steering, steerSpeed);
            }

            foreach (var wheel in wheels)
            {
                wheel.brakeTorque = 0;
            }

            // apply handbrake
            if (isBraking)
            {
                foreach (var wheel in wheels)
                {
                    // NOTE: try if this value becomes 0.
                    // It is said that it will lock up but idk.
                    wheel.motorTorque = 0.001f;
                    wheel.brakeTorque = brakeForce;
                }
            }
            else if (Mathf.Abs(currentSpeed) < 4 || Mathf.Sign(currentSpeed) == Mathf.Sign(forwardValue))
            {
                foreach (WheelCollider wheel in poweredWheels)
                {
                    wheel.motorTorque = forwardValue * motorTorque.Evaluate(currentSpeed) * diffGearing / poweredWheels.Length;
                }
            }
            else
            {
                foreach (WheelCollider wheel in wheels)
                {
                    wheel.brakeTorque = Mathf.Abs(forwardValue) * brakeForce;
                }
            }
        }

        else
        {
            if (isBraking)
            {
                rb.AddTorque(transform.forward * rotateAcceleration * -turnValue + transform.right * rotateAcceleration * forwardValue, ForceMode.Acceleration);
            }
            else
            {
                if (rb.angularVelocity.magnitude > Time.fixedDeltaTime)
                {
                    rb.AddTorque(-rb.angularVelocity.normalized * rotateAcceleration * 2, ForceMode.Acceleration);
                }
                else
                {
                    rb.angularVelocity = Vector3.zero;
                }
            }
        }

        // Downforce
        rb.AddForce(-transform.up * currentSpeed * downForce);
    }

    internal override bool IsGrounded()
    {
        foreach (WheelCollider wheel in wheels)
        {
            if (wheel.isGrounded) return true;
        }
        return false;
    }
}
