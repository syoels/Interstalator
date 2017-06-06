using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Used to check incoming input
using System.Linq;


namespace Interstalator {
[RequireComponent(typeof(TextualComponentController))]
public abstract class ShipComponent : MonoBehaviour {

    /// <summary>
    /// Holds details that a components outputs to its neighbor
    /// </summary>
    public class Output {
        public ShipComponent component;
        public ElementTypes type;
        public float amount;

        public Output(ShipComponent comp, ElementTypes type, float amount) {
            this.component = comp; 
            this.type = type; 
            this.amount = amount;
        }
    }

    /// <summary>
    /// Information about the inputs a component expects to receive
    /// </summary>
    public class Input {
        public ElementTypes[] type;
        public bool isReceived;
        public float amount;

        public Input(ElementTypes[] type) {
            this.type = type; 
            this.isReceived = true;
        }

        public void Received(float amount) {
            this.isReceived = true;
            this.amount = amount;
        }
    }

    public ShipComponent[] children;

    protected abstract string ComponentName { get; }

    // Reuired incoming resources, and have they been received yet
    // TODO: Maybe change to array
    protected List<Input> incoming;

    private TextualComponentController txtControl;
    private bool isOrigin = false;

    void Awake() {
        txtControl = GetComponent<TextualComponentController>();
        incoming = new List<Input>();
        SetRequiredInputs();
        isOrigin = SetIsOrigin();
    }

    protected void Start() {
        txtControl.SetName(ComponentName);
        txtControl.SetStatus("Awake");
    }

    /// <summary>
    /// Used in Graph Manager to check whether to start running the flow from
    /// this node
    /// </summary>
    public bool IsOrigin() {
        return isOrigin;
    }

    /// <summary>
    /// Checks if the player can interact and change this component. Must be
    /// overriden by components with interactability
    /// </summary>
    public virtual bool IsInteractable() {
        return false;
    }

    /// <summary>
    /// Apply some sort of change to this instance or bring something to the
    /// player.
    /// </summary>
    public virtual void Interact() {
        return;
    }

    /// <summary>
    /// Helper method to print to the player what interacting in this specific
    /// component will do
    /// </summary>
    /// <value>The interaction description.</value>
    public virtual string InteractionDescription { get; }

    /// <summary>
    /// Components should override this method to mark what inputs they have.
    /// Origin components that require no input don't need to override it.
    /// </summary>
    protected virtual void SetRequiredInputs() {
    }

    /// <summary>
    /// Components which require no inputs should override this method to mark
    /// themselves as such
    /// </summary>
    protected virtual bool SetIsOrigin() {
        return incoming.Count == 0;
    }

    /// <summary>
    /// Adds the given type as a required input and marks it as 'not received'
    /// </summary>
    protected void AddRequiredInput(params ElementTypes[] type) {
        incoming.Add(new Input(type));
    }

    /// <summary>
    /// Reset all incoming inputs to false.
    /// Should be used by manager after every "run" is finished
    /// </summary>
    public void ResetIncoming() {
        foreach (Input incoming in incoming) {
            incoming.amount = 0f; 
            incoming.isReceived = false;
        }
    }

    // Returns the number of elements waiting to be recieved
    public int GetRemainingIncoming() {
        int remainingInputs = 0;
        foreach (Input incoming in incoming) {
            if (!incoming.isReceived) {
                remainingInputs++;
            }
        }
        return remainingInputs;
    }

    /// <summary>
    /// Main functions, should be overriden by any inhereting component.
    /// Returns a list of child-element-amount for manager to keep traversing
    /// the ship.
    /// </summary>
    // TODO: Examine if we want to know which child should get what output
    public List<Output> Process() {
        txtControl.SetStatus("Start Processing");
        return InnerProcess();
    }

    //TODO: delete after textual level is finished
    public void SetStatus(string status) {
        txtControl.SetStatus(status);
    }

    /// <summary>
    /// Actual process, decided by each sub-class
    /// </summary>
    /// <returns>List of outputs to transfer to the children</returns>
    protected abstract List<Output> InnerProcess();

    // Update inner variables ("current water" etc.)
    /// <summary>
    /// Updates inner varribles. Returns true if some input was updated
    /// </summary>
    public bool UpdateInput(ElementTypes type, float amount) {

        txtControl.SetStatus("Trying to add " + amount.ToString() + " " + type.ToString());

        // This can be a problem if shipComponent is waiting for 2 transmissions of the same 
        // type and has to diffrentiate between them. Possible solution: requestSlotNumber from child
        foreach (Input incoming in incoming) {
            if (!incoming.isReceived && incoming.type.Contains(type)) {
                incoming.Received(amount);
                txtControl.SetStatus("Added" + amount.ToString() + " " + type.ToString());
                return true;
            }
        }

        txtControl.SetStatus("No " + type.ToString() + "required");

        // No input was updated
        return false;
    }
}
}
