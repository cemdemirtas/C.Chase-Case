using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StackableObject : MonoBehaviour, IStackable
{
    private const float SCALE_MULTIPLIER = 0.5f;
    private const float SCALE_DURATION = 0.5f;
    private const float JUMP_RADIUS = 1f;

    private new Collider collider;
    public Collider Collider { get { return collider == null ? collider = GetComponentInChildren<Collider>() : collider; } }

    private Tween scaleTween;

    public void OnStacked()
    {
        if (scaleTween != null)
            scaleTween.Kill(true);

        scaleTween = transform.DOPunchScale(Vector3.one * SCALE_MULTIPLIER, SCALE_DURATION, 2);
    }

    public void OnUnstacked(Transform goCriminalPerson)
    {
        Collider.enabled = false;
        transform.SetParent(null);

        Vector3 jumpPosition = goCriminalPerson.position +Vector3.up * 5 + Vector3.down * 7;
        transform.GetComponent<Animation>().Play();
        transform.DOJump(jumpPosition, 1f, 1, 1f).OnComplete(()=> Collider.enabled = true);
    }
}
