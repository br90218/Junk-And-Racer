using UnityEngine;

public abstract class IVehicleBehaviour : MonoBehaviour
{

    protected bool isBraking;
    protected float forwardValue;
    protected float turnValue;
    protected float energy;
    protected float damage = 0f;
    protected IAbility[] abilities = new IAbility[3];

    //protected bool isGrounded;

    internal virtual void SetBrake(bool value)
    {
        isBraking = value;
    }
    internal virtual void SetForwardValue(float value)
    {
        forwardValue = value;
    }
    internal virtual void SetTurnValue(float value)
    {
        turnValue = value;
    }

    internal abstract bool IsGrounded();

    internal virtual void TriggerAbility(int index, bool onOrOff)
    {
        if(abilities[index].isActive)
        {
            abilities[index].Apply();
        }
        else
        {
            abilities[index].Disapply();
        }
    }
}
