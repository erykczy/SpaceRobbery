using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Size")]
    public float SizeSmoothTime;
    public float SizeChange;
    public float MaxSize;
    public float MinSize;

    [Header("Movement")]
    public float MovementSpeed;
    public float MovementSmoothTime;

    public Camera Camera { get; private set; }
    private Vector3 movementVelocity;
    private float sizeVelocity;
    private float targetSize;

    private void Awake()
    {
        Camera = GetComponent<Camera>();
    }

    private void Start()
    {
        targetSize = Camera.orthographicSize;
    }

    private void Update()
    {
        Vector2 direction = Vector2.zero;
        if (Input.GetKey(KeyCode.W)) direction += Vector2.up;
        if (Input.GetKey(KeyCode.S)) direction += Vector2.down;
        if (Input.GetKey(KeyCode.D)) direction += Vector2.right;
        if (Input.GetKey(KeyCode.A)) direction += Vector2.left;
        if (Input.mouseScrollDelta.y > 0) targetSize = Mathf.Max(targetSize - SizeChange * Time.unscaledDeltaTime, MinSize);
        if (Input.mouseScrollDelta.y < 0) targetSize = Mathf.Min(targetSize + SizeChange * Time.unscaledDeltaTime, MaxSize);

        transform.position = Vector3.SmoothDamp(transform.position, transform.position + (Vector3)direction.normalized * MovementSpeed, ref movementVelocity, MovementSmoothTime, float.PositiveInfinity, Time.unscaledDeltaTime);

        Camera.orthographicSize = Mathf.SmoothDamp(Camera.orthographicSize, targetSize, ref sizeVelocity, SizeSmoothTime, float.PositiveInfinity, Time.unscaledDeltaTime);
    }
}
