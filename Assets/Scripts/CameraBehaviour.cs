using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{   
    [SerializeField] private IVehicleBehaviour vehicle;
    [SerializeField] private Transform target;
    [SerializeField] private Transform baseTarget;
    [SerializeField] private float transitionTime = 10f;
    private Vector3 velocity = Vector3.zero;
    private Vector3 vectorDiffWithVehicle;

    




    // Start is called before the first frame update
    void Start()
    {
        //transform.localPosition = -target.transform.forward * defaultDistance + transform.up * height;

    }

    // Update is called once per frame

    private void FixedUpdate()
    {
        if (vehicle.IsGrounded())
        {
            vectorDiffWithVehicle = Vector3.zero;
            transform.position = Vector3.SmoothDamp(transform.position, baseTarget.position, ref velocity, transitionTime * Time.fixedDeltaTime);
            transform.LookAt(target.transform);
        }
        else
        {
            if(vectorDiffWithVehicle == Vector3.zero)
            {
                vectorDiffWithVehicle = vehicle.transform.position - transform.position;
            }
            transform.position = Vector3.SmoothDamp(transform.position, vehicle.transform.position - vectorDiffWithVehicle, ref velocity, transitionTime * Time.fixedDeltaTime);
            //transform.position = vehicle.transform.position - vectorDiffWithVehicle;
            //transform.LookAt(vehicle.transform); 
        }
    }
}
