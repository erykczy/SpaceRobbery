using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Unit : MonoBehaviour
{
    public float RotationSpeed;
    public float Speed;
    public Vector2 Destination { get; set; }
    private Tile _targetTile = null;
    public Tile TargetTile {
        get => _targetTile;
        set
        {
            _targetTile = value;
            shouldAttackTargetTile = _targetTile != null && !_targetTile.TryGetComponent<InteractableTile>(out var _);
        } 
    }
    public float Radius { get => GetComponentInChildren<Collider2D>().bounds.extents.y; }
    public Rigidbody2D Rigidbody { get; private set; }
    public Shooter Shooter { get; private set; }
    public UnityEvent WhenDestroyed = new();
    private bool shouldAttackTargetTile = false;

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        Shooter = GetComponent<Shooter>();
    }

    private void Start()
    {
        Destination = transform.position;
    }

    private void Update()
    {
        MoveTowardsDestination();
        if(TargetTile != null)
        {
            RotateTowardsTargetTile();
            if (shouldAttackTargetTile) AttackTargetTile();
        }
    }

    private void OnDestroy()
    {
        WhenDestroyed.Invoke();
    }

    private void AttackTargetTile()
    {

    }

    private void RotateTowardsTargetTile()
    {
        var deltaAngle = Vector2.SignedAngle(transform.up, (TargetTile.transform.position - transform.position).normalized);

        var newAngle = transform.eulerAngles.z + Mathf.Clamp(deltaAngle, -RotationSpeed * Time.deltaTime, RotationSpeed * Time.deltaTime);
        transform.eulerAngles = new Vector3(0, 0, newAngle);
    }

    private void MoveTowardsDestination()
    {
        var distance = Destination - (Vector2)transform.position;
        Rigidbody.velocity = Mathf.Clamp01(distance.magnitude) * Speed * distance.normalized;
    }
}
