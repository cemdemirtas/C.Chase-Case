using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public interface IPrisonable
{
    void Arrest(NavMeshAgent nav);
}
