using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class WireRenderer : MonoBehaviour
{
    public float CableSegmentWidth;
    public float CableSegmentLength;
    public Sprite TextureDownLeft;
    public Sprite TextureDownRight;
    public Sprite TextureDownUp;
    public Sprite TextureLeftRight;
    public Sprite TextureUpLeft;
    public Sprite TextureUpRight;
    public SpriteRenderer SpriteRenderer { get; private set; }
    public WireSegment Wire { get; private set; }
    public ChildrenCountRegulator ChildrenCountRegulator { get; private set; }
    private List<Vector3> signalLinePoints;

    private void Awake()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
        ChildrenCountRegulator = GetComponent<ChildrenCountRegulator>();
        Wire = GetComponentInParent<WireSegment>();
    }

    private void Start()
    {
        signalLinePoints = FindSignalLinePoints();
    }

    private void Update()
    {
#if UNITY_EDITOR
        SpriteRenderer = GetComponent<SpriteRenderer>();
        Wire = GetComponentInParent<WireSegment>();
        signalLinePoints = FindSignalLinePoints();

        if (Wire.InputDirection == DirectionMath.Direction.Up)
        {
            if (Wire.OutputDirection == DirectionMath.Direction.Right)
            {
                SpriteRenderer.sprite = TextureUpRight;
            }

            if (Wire.OutputDirection == DirectionMath.Direction.Left)
            {
                SpriteRenderer.sprite = TextureUpLeft;
            }

            if (Wire.OutputDirection == DirectionMath.Direction.Down)
            {
                SpriteRenderer.sprite = TextureDownUp;
            }
        }

        if (Wire.InputDirection == DirectionMath.Direction.Down)
        {

            if (Wire.OutputDirection == DirectionMath.Direction.Up)
            {
                SpriteRenderer.sprite = TextureDownUp;
            }

            if (Wire.OutputDirection == DirectionMath.Direction.Right)
            {
                SpriteRenderer.sprite = TextureDownRight;
            }

            if (Wire.OutputDirection == DirectionMath.Direction.Left)
            {
                SpriteRenderer.sprite = TextureDownLeft;
            }
        }

        if (Wire.InputDirection == DirectionMath.Direction.Right)
        {

            if (Wire.OutputDirection == DirectionMath.Direction.Left)
            {
                SpriteRenderer.sprite = TextureLeftRight;
            }

            if (Wire.OutputDirection == DirectionMath.Direction.Up)
            {
                SpriteRenderer.sprite = TextureUpRight;
            }

            if (Wire.OutputDirection == DirectionMath.Direction.Down)
            {
                SpriteRenderer.sprite = TextureDownRight;
            }
        }

        if (Wire.InputDirection == DirectionMath.Direction.Left)
        {

            if (Wire.OutputDirection == DirectionMath.Direction.Up)
            {
                SpriteRenderer.sprite = TextureUpLeft;
            }

            if (Wire.OutputDirection == DirectionMath.Direction.Down)
            {
                SpriteRenderer.sprite = TextureDownLeft;
            }

            if (Wire.OutputDirection == DirectionMath.Direction.Right)
            {
                SpriteRenderer.sprite = TextureLeftRight;
            }
        }
        if (!Application.isPlaying) return;
#endif

        ChildrenCountRegulator.Count = Wire.Signals.Count;

        for(int i = 0; i < Wire.Signals.Count; i++)
        {
            var signal = Wire.Signals[i];
            var lineRenderer = transform.GetChild(i).GetComponent<RestrainedLineRenderer>();
            lineRenderer.RequestedPoints = signalLinePoints;
            lineRenderer.Min = signal.Start * CableSegmentLength;
            lineRenderer.Max = signal.End * CableSegmentLength;
        }
    }

    private List<Vector3> FindSignalLinePoints()
    {
        List<Vector3> list;
        Vector3 firstPoint = CableSegmentLength / 2f * DirectionMath.DirectionToVector(Wire.InputDirection);
        Vector3 lastPoint = CableSegmentLength / 2f * DirectionMath.DirectionToVector(Wire.OutputDirection);
        if (Wire.IsStraight())
        {
            list = new List<Vector3> { firstPoint, lastPoint };
        }
        else
        {
            Vector2 halfPoint = Vector2.zero;
            list = new List<Vector3> { firstPoint, halfPoint, lastPoint };
        }
        return list;
    }
}
