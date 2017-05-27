using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Interstalator {
[RequireComponent(typeof(TextualComponentController))]
public abstract class ShipComponent : MonoBehaviour {

    // Used in flow manager to manage flow
    public class Output {
        public ShipComponent component;
        public ElementTypes type;
        public float amount;
        public Output(ShipComponent c, ElementTypes t, float a){
            this.component = c; 
            this.type = t; 
            this.amount = a;
        }
    }

    public class Input {
        public ElementTypes type;
        public bool isReceived;
        public float amount;

        public Input(ElementTypes t, bool state, float amount) {
            this.type = t; 
            this.isReceived = state;
            this.amount = amount;
        }

        public void Received(bool state) {
            this.isReceived = state;
        }
    }

    // Reuired incoming resources, and have they been received yet
    protected List<Input> _incoming;
    private TextualComponentController _txtControl;
    private string componentName = "Unnamed";
    protected bool _isOrigin = false;
    public ShipComponent[] children;


    void Awake(){
        _txtControl = GetComponent<TextualComponentController>();
        componentName = getComponentName();
        _incoming = new List<Input>();
        SetRequiredInputs();    
    }

    protected void Start(){
        _txtControl.SetStatus("Awake");
    }

    public bool IsOrigin(){
        return _isOrigin;
    }

    // TODO: probably can br deleted after first simulation
    protected abstract string getComponentName();

    protected abstract void SetRequiredInputs();

    protected void AddRequiredInput(ElementTypes type){
        _incoming.Add(new Input(type, false, 0f));
    }
        
    // Reset all incoming to false. Should be used by manager after every "run" is finished
    public void ResetIncoming() {
        foreach (Input incoming in _incoming) {
            incoming.Received(false); //TODO: make sure this actually changes by reference.
        }
    }

    // Get all incoming that aren't "false"
    public List<ElementTypes> GetRemainingIncoming() {
        List<ElementTypes> remainingInputs = new List<ElementTypes>();
        foreach (Input incoming in _incoming) {
            if (!incoming.isReceived) {
                remainingInputs.Add(incoming.type);
            }
        }
        return remainingInputs;
    }   

    // Main functions, should be overriden by any inhereting component.
    // Returns a list of child-element-amount for manager to keep traversing the ship.
    public List<Output> Process() {
        _txtControl.SetStatus("Start Processing");
        return InnerProcess();
    }

    //TODO: delete after textual level is finished
    public void SetStatus(string status){
        _txtControl.SetStatus(status);
    }

    // Actual process, decided by each sub-class
    protected abstract List<Output> InnerProcess();

    // In case component requires some action upon input
    protected abstract void InnerUpdateInput(ElementTypes type, float amount);

    // Update inner variables ("current water" etc.)
    // Returns true iff some input was updated
    public bool UpdateInput(ElementTypes type, float amount) {

        _txtControl.SetStatus("Trying to add " + amount.ToString() + " " + type.ToString());

        // This can be a problem if shipComponent is waiting for 2 transmissions of the same 
        // type and has to diffrentiate between them. Possible solution: requestSlotNumber from child
        foreach (Input incoming in _incoming) {
            if (!incoming.isReceived && incoming.type == type) {
                InnerUpdateInput(type, amount);
                incoming.Received(true);
                _txtControl.SetStatus("Added" + amount.ToString() + " " + type.ToString());
                return true;
            }
        }
        _txtControl.SetStatus("No " + type.ToString() + "required");
        return false; //no input was updated
    }
	
       

}
}
