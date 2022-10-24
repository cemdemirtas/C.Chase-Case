using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PrisonTrigger : MonoBehaviour, IPrisonable
{
    public void Arrest(NavMeshAgent nav)
    {
        nav.enabled = false;
    }

}
