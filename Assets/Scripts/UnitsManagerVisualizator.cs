using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitsManagerVisualizator : MonoBehaviour
{
    public UnitsManager UnitsManager { get; private set; }
    public GameObject SelectionBoxPrefab;
    public GameObject DestinationPlanIndicatorPrefab;
    private List<GameObject> destinationPlanIndicators = new();
    private GameObject selectionBox;

    private void Awake()
    {
        UnitsManager = GetComponentInParent<UnitsManager>();
    }

    private void Update()
    {
        // TODO optimize
        foreach (var unit in FindObjectsByType<Unit>(FindObjectsSortMode.None))
        {
            var effectsContainer = unit.transform.GetChild(1);
            var destinationIndicator = effectsContainer.GetChild(0);
            var selectionIndicator = effectsContainer.GetChild(1);
            destinationIndicator.transform.position = unit.Destination;
            selectionIndicator.gameObject.SetActive(UnitsManager.SelectedUnits.Contains(unit
                ));
            selectionIndicator.GetComponent<CircleRenderer>().Radius = unit.Radius;
        }

        UpdateDestinationPlanVisualization();
        UpdateSelectionBox();
    }

    private void UpdateSelectionBox()
    {
        var selection = UnitsManager.CurrentSelection;
        if (selection == null)
        {
            if (selectionBox != null) Destroy(selectionBox);
        }
        else
        {
            if (selectionBox == null) selectionBox = Instantiate(SelectionBoxPrefab);
            var bottomLeft = selection.GetBottomLeft();
            var topRight = selection.GetTopRight();
            var size = topRight - bottomLeft;

            selectionBox.transform.position = bottomLeft + size / 2f;
            selectionBox.transform.localScale = size;
        }
    }

    private void UpdateDestinationPlanVisualization()
    {
        var destPlan = UnitsManager.DestinationPlan;
        var destCount = destPlan == null ? 0 : destPlan.Destinations.Count;

        if (destinationPlanIndicators.Count > destCount)
        {
            for (int i = destinationPlanIndicators.Count - 1; i >= 0; i--)
            {
                Destroy(destinationPlanIndicators[i]);
                destinationPlanIndicators.RemoveAt(i);
            }
        }
        else if (destinationPlanIndicators.Count < destCount)
        {
            for (int i = 0; i < destCount - destinationPlanIndicators.Count; i++)
            {
                var indicatorObj = Instantiate(DestinationPlanIndicatorPrefab, transform);
                destinationPlanIndicators.Add(indicatorObj);
            }
        }
        for (int i = 0; i < destCount; i++)
        {
            destinationPlanIndicators[i].transform.position = destPlan.Destinations[i];
        }
    }
}
