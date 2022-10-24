using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

public class CriminalPersonController : MonoBehaviour
{
    NavMeshAgent nav;
    [SerializeField] Transform player;
    [SerializeField] GameObject handcuffPrefab;
    private bool sealCharacter;
    private void OnEnable()
    {
        nav = GetComponent<NavMeshAgent>();
        nav.enabled = false;
    }
    private void Update()
    {
        if (!nav.enabled) return;
        nav.SetDestination(player.position);
    }
    private void OnTriggerEnter(Collider other)
    {
        IStacker stacker = other.GetComponent<IStacker>();
        IStackable stackable = other.GetComponent<IStackable>();
        if (stacker != null && nav)
        {
            transform.GetComponent<CapsuleCollider>().enabled = false;
            transform.GetComponentInChildren<Renderer>().material.color = Color.green;
            nav.enabled = true;
        }

        IPrisonable prisonable = other.GetComponent<IPrisonable>();
        if (other.transform.TryGetComponent(out PrisonTrigger prison))
        {
            other.transform.GetComponent<BoxCollider>().enabled = true;
            transform.GetComponentInChildren<Renderer>().material.color = Color.yellow; // To aware arrasted.
            prison.Arrest(nav);
            GameObject handcuff = Instantiate(handcuffPrefab, other.transform.position, Quaternion.identity);
            handcuff.transform.DOJump(player.position, 1f, 1, 0.01f);
        }



    }
}
