  using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interstalator {
public class GraphManager : MonoBehaviour {
    // Used to access the graph manager globally
    public static GraphManager instance;

    // Used by components to display different ship status
    // TODO: Make methods in graph/game manager instead of components
    // accessing statusController
    public ShipStatusController statusController;
    public GameObject itemsContainer;

    private ItemType heldItem = ItemType.None;
    public ItemType HeldItem {
        get {
            return heldItem;
        }
        set {
            heldItem = value;
            displayController.SetItem(heldItem.ToString().Replace('_', ' '));
        }
    }

    private ItemDisplayController displayController;
    private bool runningFlow = false;
    private GameObject heldItemObject;


    void Awake() {
        instance = this;
        statusController = GetComponent<ShipStatusController>();
        displayController = GetComponent<ItemDisplayController>();
    }

    void Start() {
        statusController.SetProblem(ShipStatusController.ShipSystem.Air, "No air!");
        Flow();
    }

    public void GrabItem(TextualItem item) {
        if (heldItemObject != null) {
            heldItemObject.SetActive(true);
        }
        HeldItem = item.itemType;
        heldItemObject = item.gameObject;
        heldItemObject.SetActive(false);
    }

    public void DropItem() {
        // Can't drop an item if not holding anything
        Debug.Assert(HeldItem != ItemType.None);
        HeldItem = ItemType.None;
        if (heldItemObject != null) {
            heldItemObject.SetActive(true);
        }
    }

    public void SetInteractionDescription(string interaction) {
        displayController.SetInteraction(interaction);
    }

    public void CancelInteractionDescription() {
        displayController.SetInteraction(null);
    }

    /// <summary>
    /// Runs the flow and updates the state of every component
    /// </summary>
    public void Flow() {
        StartCoroutine(FlowRoutine());
    }

    /// <summary>
    /// The main logic of the flow. Runs as a coroutine to allow adding delays
    /// to play animations
    /// </summary>
    private IEnumerator FlowRoutine() {
        // Avoids running two flows at once
        yield return new WaitUntil(() => !runningFlow);

        runningFlow = true;
        ShipComponent[] allComponents = FindObjectsOfType<ShipComponent>();
        Queue<ShipComponent.Output> queue = new Queue<ShipComponent.Output>(); 
//        int maxIterations = allComponents.Length + 1;
//        int iterations = 0;

        // Start with origins
        foreach (ShipComponent c in allComponents) {
//            maxIterations += c.GetRemainingIncoming();
            c.ResetIncoming();
            if (c.IsOrigin()) {
                ShipComponent.Output originComponent = new ShipComponent.Output(c, ElementTypes.None, 0f);
                queue.Enqueue(originComponent);
            }
        }

//        Debug.Log("Starting flow - max iterations: " + maxIterations);
//        while (queue.Count > 0 && iterations <= maxIterations) {
        while(queue.Count > 0) {
//            iterations += 1;
            ShipComponent.Output transmission = queue.Dequeue();
            ShipComponent curr = transmission.component;
            float delay = curr.UpdateInput(transmission.type, transmission.amount);
            yield return new WaitForSeconds(delay);
            int remainingIncoming = curr.GetRemainingIncoming();
            if (remainingIncoming > 0) {
                queue.Enqueue(transmission);
                continue;
            }

            List<ShipComponent.Output> children = curr.Process();
            foreach (ShipComponent.Output t in children) {
                queue.Enqueue(t);
            }
        }
        if (queue.Count > 0) {
            Debug.Log("Reached max iterations");
        }

        runningFlow = false;
    }
}
}
