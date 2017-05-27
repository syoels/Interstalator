using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Interstalator {
[RequireComponent(typeof(TextualComponentController))]
public abstract class ShipComponent : MonoBehaviour {

    // Used in flow manager to manage flow
    public class Transmission {
        public ShipComponent component;
        public ElementTypes type;
        public float amount;
        public Transmission(ShipComponent c, ElementTypes t, float a){
            this.component = c; 
            this.type = t; 
            this.amount = a;
        }
    }

    public class RequiredInput {
        public ElementTypes type;
        public bool isReceived;

        public RequiredInput(ElementTypes t, bool state) {
            this.type = t; 
            this.isReceived = state;
        }

        public void Received(bool state) {
            this.isReceived = state;
        }
    }

    // Reuired incoming resources, and have they been received yet
    protected List<RequiredInput> _incoming;
    private TextualComponentController _txtControl;
    private string componentName = "Unnamed";
    protected bool _isOrigin = false;
    public ShipComponent[] children;


    void Awake(){
        _txtControl = GetComponent<TextualComponentController>();
        componentName = getComponentName();
        _incoming = new List<RequiredInput>();
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
        _incoming.Add(new RequiredInput(type, false));
    }
        
    // Reset all incoming to false. Should be used by manager after every "run" is finished
    public void ResetIncoming() {
        foreach (RequiredInput incoming in _incoming) {
            incoming.Received(false); //TODO: make sure this actually changes by reference.
        }
    }

    // Get all incoming that aren't "false"
    public List<ElementTypes> GetRemainingIncoming() {
        List<ElementTypes> remainingInputs = new List<ElementTypes>();
        foreach (RequiredInput incoming in _incoming) {
            if (!incoming.isReceived) {
                remainingInputs.Add(incoming.type);
            }
        }
        return remainingInputs;
    }   

    // Main functions, should be overriden by any inhereting component.
    // Returns a list of child-element-amount for manager to keep traversing the ship.
    public List<Transmission> Process() {
        _txtControl.SetStatus(" Processing...");
        return InnerProcess();
    }

    //TODO: delete after textual level is finished
    public void SetStatus(string status){
        _txtControl.SetStatus(status);
    }

    // Actual process, decided by each sub-class
    protected abstract List<Transmission> InnerProcess();

    // In case component requires some action upon input
    protected abstract void InnerUpdateInput(ElementTypes type, float amount);

    // Update inner variables ("current water" etc.)
    // Returns true iff some input was updated
    public bool UpdateInput(ElementTypes type, float amount) {

        _txtControl.SetStatus("Trying to add " + amount.ToString() + " " + type.ToString());

        foreach (RequiredInput incoming in _incoming) {
            if (!incoming.isReceived && incoming.type == type) {
                InnerUpdateInput(type, amount); // In case component requires some action upon input
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
