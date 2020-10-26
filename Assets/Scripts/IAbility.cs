using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IAbility : MonoBehaviour
{

    internal bool isActive;
    // Start is called before the first frame update
    internal virtual void Apply()
    {

        isActive = true;
    }
    internal virtual void Disapply()
    {

        isActive = false;
    }
}
