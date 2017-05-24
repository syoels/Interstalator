using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Interstalator {
public abstract class Component : MonoBehaviour {

    public class Connection {
        public Component connectedComponent;
        public ElementTypes type;
    }

    public struct TransferredElement {
        ElementTypes type;
        float quantitiy;

        public TransferredElement(ElementTypes t, float amount) {
            type = t; 
            quantitiy = amount;
        }
    }


    public List<Connection> incoming;
    public List<Connection> outgoing;

    // Use this for initialization
    void Start() {
			
    }
		
    // Update is called once per frame
    void Update() {
			
    }

    public abstract void Process(params TransferredElement[] inputs);



}
}
