using UnityEngine;

namespace Interstalator {
/// <summary>
/// A singleton that holds references to other important general game objects
/// like the FlowManager, ShipStatus and other stuff
/// </summary>
public class GameManager : MonoSingleton<GameManager> {

    public FlowManager flowManager;
    // Will probably be replaced by player controller
    public ItemManager itemManager;
    public InteractionDisplay interactionDisplay;
    public ShipStatusController shipStatus;
    
    public override void Init() {
        if (flowManager == null) {
            flowManager = GetComponent<FlowManager>();
        }

        if (itemManager == null) {
            itemManager = GetComponent<ItemManager>();
        }

        if (interactionDisplay == null) {
            interactionDisplay = GetComponent<InteractionDisplay>();
        }

        if (shipStatus == null) {
            shipStatus = GetComponent<ShipStatusController>();
        }
    }

    // A series of helper methods to easier writing of common actions

    public void Flow() {
        flowManager.Flow();
    }
}
}
