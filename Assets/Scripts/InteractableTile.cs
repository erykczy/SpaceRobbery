using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractableTile : MonoBehaviour
{
    public UnityEvent WhenInteracted = new();
    public bool CanBeInteractedOnlyOnce = false;
    public float InteractionRadius;
    public LayerMask UnitsLayer;
    public Device Device { get; private set; }

    private void Awake()
    {
        Device = GetComponent<Device>();
    }

    private void Update()
    {
        var colliders = Physics2D.OverlapCircleAll(transform.position, InteractionRadius, UnitsLayer);
        if (colliders.Length > 0) Interact();
    }

    private void Interact()
    {
        Device.ActivateOutput();
        WhenInteracted.Invoke();
        if (CanBeInteractedOnlyOnce) Destroy(this);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, InteractionRadius);
    }
}
