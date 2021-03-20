using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputController : MonoBehaviour
{
    [SerializeField] private IVehicleBehaviour vehicle;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    // Bad Optimization right here perhaps
    private void Update()
    {
        if(Input.GetButtonDown("Jump"))
        {
            vehicle.SetBrake(true);
        }
        if(Input.GetButtonUp("Jump"))
        {
            vehicle.SetBrake(false);
        }


        vehicle.SetForwardValue(Input.GetAxis("Vertical"));
        vehicle.SetTurnValue(Input.GetAxis("Horizontal"));

    }
}
