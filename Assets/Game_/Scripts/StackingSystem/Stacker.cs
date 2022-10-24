using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class Stacker : MonoBehaviour, IStacker
{
    [field: SerializeField] public Transform StackHolder { get; set; }
    [field: SerializeField] public Transform FollowTarget { get; set; }
    public List<IStackable> Stacks { get; set; } = new List<IStackable>();

    private const float Z_OFFSET = 1f;
    [SerializeField] List<Transform> stackedCriminalPerson;
    private bool seal;

    private void OnEnable()
    {
        Events.OnObstacleCollision.AddListener(Unstack);
    }

    private void OnDisable()
    {
        Events.OnObstacleCollision.RemoveListener(Unstack);
    }

    private void Update()
    {
        HandleStackMovement();
    }

    public void Stack(IStackable stackable)
    {
        if (Stacks.Contains(stackable)) return;

        Stacks.Add(stackable);
        stackable.transform.SetParent(StackHolder);
    }

    public void Unstack(IStackable stackable)
    {
        if (!Stacks.Contains(stackable)) return;

        int stackIndex = GetStackIndex(stackable);
        for (int i = stackIndex; i < Stacks.Count; i++)
        {
            //Stacks[Stacks.Count - 1].OnUnstacked();
            //Stacks.Remove(Stacks[Stacks.Count - 1]);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        IStackable stackable = other.GetComponent<IStackable>();

        if (stackable != null)
        {
            Stack(stackable);
            Vector3 jumpPosition = transform.position + Vector3.up * 5 + Vector3.right * 5;
            other.GetComponent<Animation>().Play();
            stackable.transform.DOJump(jumpPosition, 1f, 1, 0.5f).OnComplete(() => Debug.Log("spring okey"));
            
        }

        if (other.gameObject.tag == "Criminal" && Stacks.Count > 0)
        {
            stackedCriminalPerson.Add(other.transform);
            other.gameObject.tag = "ToArrested";
            int stackIndex = GetStackIndex(stackable);
            Stacks[Stacks.Count - 1].OnUnstacked(other.transform);
            Stacks.Remove(Stacks[Stacks.Count - 1]);
            HandleStackScale(Stacks[Stacks.Count - 1].gameObject, other.transform);

        }


    }

    private int GetStackIndex(IStackable stack)
    {
        int index = -1;
        for (int i = 0; i < Stacks.Count; i++)
        {
            if (Stacks[i] == stack)
            {
                index = i;
                break;
            }
        }

        return index;
    }

    public void HandleStackMovement()
    {
        if (Stacks.Count <= 0) return;

        for (int i = 0; i < Stacks.Count; i++)
        {
            if (i - 1 < 0)
                Stacks[i].transform.position = Vector3.Lerp(Stacks[i].transform.position, FollowTarget.position, Time.deltaTime * 15);
            else
                Stacks[i].transform.position = Vector3.Lerp(Stacks[i].transform.position, Stacks[i - 1].transform.position + Vector3.up / 1.5f, Time.deltaTime * 15);
            Stacks[i].transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.identity, 0.1f);
        }
    }

    private void HandleStackScale(GameObject DecreasedHandcuff, Transform collided)
    {
        StartCoroutine(HandleStackScaleCo(DecreasedHandcuff, collided));
    }

    private IEnumerator HandleStackScaleCo(GameObject DecreasedHandcuff, Transform collided)
    {
        Stacks[Stacks.Count - 1].transform.GetComponent<Animation>().Play();
        yield return new WaitForSeconds(0.1f);
        DecreasedHandcuff.transform.GetComponent<Collider>().enabled = false;
    }
}
