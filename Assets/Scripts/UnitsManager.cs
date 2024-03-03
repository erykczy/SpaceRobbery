using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class UnitsManager : MonoBehaviour
{
    public LayerMask TilesLayer;
    public Selection CurrentSelection = null;
    public List<Unit> SelectedUnits = new();
    public DestinationPlan DestinationPlan;

    private void Awake()
    {
        UserInput.Config.UnitManagement.Select.started += OnSelectionInputStarted;
        UserInput.Config.UnitManagement.Select.canceled += OnSelectionInputStopped;

        UserInput.Config.UnitManagement.PlanTargets.started += OnPlanTargetsInputStarted;
        UserInput.Config.UnitManagement.PlanTargets.canceled += OnPlanTargetsInputStopped;
    }

    private void Update()
    {
        UpdateSelection();
        UpdateDestinationPlan();
    }

    private void OnSelectionInputStarted(CallbackContext context)
    {
        var mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        CurrentSelection = new();
        CurrentSelection.StartPos = mouseWorldPos;
        CurrentSelection.EndPos = mouseWorldPos;
    }

    private void OnSelectionInputStopped(CallbackContext context)
    {
        if (CurrentSelection == null) return;
        SelectedUnits = CurrentSelection.FetchSelectedUnits();
        CurrentSelection = null;
    }

    private void UpdateSelection()
    {
        if (CurrentSelection == null) return;
        var mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        CurrentSelection.EndPos = mouseWorldPos;
    }

    private void OnPlanTargetsInputStarted(CallbackContext context)
    {
        var mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var colliders = Physics2D.OverlapPointAll(mouseWorld, TilesLayer);
        Tile pressedTile = colliders.Length == 0 ? null : colliders.First().GetComponentInParent<Tile>();

        if(pressedTile == null)
        {
            DestinationPlan = new();
        }
        else
        {
            foreach (var unit in SelectedUnits)
            {
                unit.TargetTile = pressedTile;
            }
        }
    }

    private void OnPlanTargetsInputStopped(CallbackContext context)
    {
        if (DestinationPlan == null) return;
        for (int i = 0; i < DestinationPlan.Destinations.Count; i++)
        {
            SelectedUnits[i].Destination = DestinationPlan.Destinations[i];
        }
        DestinationPlan = null;
    }

    private void UpdateDestinationPlan()
    {
        var mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (DestinationPlan != null)
        {
            if (DestinationPlan.Destinations.Count < SelectedUnits.Count)
            {
                // destinations collision check
                bool collides = false;
                for (int i = 0; i < DestinationPlan.Destinations.Count; i++)
                {
                    var distance = Vector2.Distance(DestinationPlan.Destinations[i], mouseWorldPos);
                    if (distance < SelectedUnits[i].Radius + SelectedUnits[DestinationPlan.CurrentUnitIndex].Radius)
                    {
                        collides = true;
                        break;
                    }
                }
                if (!collides)
                {
                    DestinationPlan.Destinations.Add(mouseWorldPos);
                    DestinationPlan.CurrentUnitIndex++;
                }
            }
        }
    }
}
