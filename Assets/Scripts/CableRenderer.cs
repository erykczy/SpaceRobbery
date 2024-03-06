using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CableRenderer : MonoBehaviour
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
    public WireSegment Cable { get; private set; }
    private RestrainedLineRenderer FillLine;

    private void Awake()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
        Cable = GetComponentInParent<WireSegment>();
        FillLine = GetComponentInChildren<RestrainedLineRenderer>();
    }

    private void Update()
    {
#if UNITY_EDITOR
        SpriteRenderer = GetComponent<SpriteRenderer>();
        Cable = GetComponentInParent<WireSegment>();
        FillLine = GetComponentInChildren<RestrainedLineRenderer>();

        if (Cable.InputDirection == DirectionMath.Direction.Up)
        {
            if (Cable.OutputDirection == DirectionMath.Direction.Right)
            {
                SpriteRenderer.sprite = TextureUpRight;
            }

            if (Cable.OutputDirection == DirectionMath.Direction.Left)
            {
                SpriteRenderer.sprite = TextureUpLeft;
            }

            if (Cable.OutputDirection == DirectionMath.Direction.Down)
            {
                SpriteRenderer.sprite = TextureDownUp;
            }
        }

        if (Cable.InputDirection == DirectionMath.Direction.Down)
        {

            if (Cable.OutputDirection == DirectionMath.Direction.Up)
            {
                SpriteRenderer.sprite = TextureDownUp;
            }

            if (Cable.OutputDirection == DirectionMath.Direction.Right)
            {
                SpriteRenderer.sprite = TextureDownRight;
            }

            if (Cable.OutputDirection == DirectionMath.Direction.Left)
            {
                SpriteRenderer.sprite = TextureDownLeft;
            }
        }

        if (Cable.InputDirection == DirectionMath.Direction.Right)
        {

            if (Cable.OutputDirection == DirectionMath.Direction.Left)
            {
                SpriteRenderer.sprite = TextureLeftRight;
            }

            if (Cable.OutputDirection == DirectionMath.Direction.Up)
            {
                SpriteRenderer.sprite = TextureUpRight;
            }

            if (Cable.OutputDirection == DirectionMath.Direction.Down)
            {
                SpriteRenderer.sprite = TextureDownRight;
            }
        }

        if (Cable.InputDirection == DirectionMath.Direction.Left)
        {

            if (Cable.OutputDirection == DirectionMath.Direction.Up)
            {
                SpriteRenderer.sprite = TextureUpLeft;
            }

            if (Cable.OutputDirection == DirectionMath.Direction.Down)
            {
                SpriteRenderer.sprite = TextureDownLeft;
            }

            if (Cable.OutputDirection == DirectionMath.Direction.Right)
            {
                SpriteRenderer.sprite = TextureLeftRight;
            }
        }
#endif

        if (!Application.isPlaying) return;

        var progress = Cable.SignalForwardingTime == 0f ? (Cable.ThisDevice.Activated ? 1f : 0f) : (Cable.SignalForwardingProgress / Cable.SignalForwardingTime);
        Vector2 firstPoint = CableSegmentLength / 2f * DirectionMath.DirectionToVector(Cable.InputDirection);
        Vector2 lastPoint = CableSegmentLength / 2f * DirectionMath.DirectionToVector(Cable.OutputDirection);

        if (Cable.IsStraight())
        {
            FillLine.RequestedPoints = new List<Vector3> { firstPoint, lastPoint };
        }
        else
        {
            Vector2 halfPoint = Vector2.zero;
            FillLine.RequestedPoints = new List<Vector3> { firstPoint, halfPoint, lastPoint };
        }

        FillLine.Filament = progress * CableSegmentLength;
    }
}
