using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interstalator{
public class GraphManager : MonoBehaviour {


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Flow(){
        ShipComponent[] allComponents = FindObjectsOfType<ShipComponent>();
        Queue<ShipComponent.Transmission> queue = new Queue<ShipComponent.Transmission>(); 

        // Start with origins
        foreach(ShipComponent c in allComponents){
            if (c.IsOrigin()) {
                ShipComponent.Transmission originComponent = new ShipComponent.Transmission(c, ElementTypes.None, 0f);
                queue.Enqueue(originComponent);
            }
        }

        while (queue.Count > 0) {
            // Use queue.Peek to check inputs
            ShipComponent.Transmission transmission = queue.Dequeue();
            ShipComponent curr = transmission.component; 
            curr.UpdateInput(transmission.type, transmission.amount);
            List<ShipComponent.Transmission> children = curr.Process();
            foreach (ShipComponent.Transmission t in children){
                queue.Enqueue(t);
            }
        }

    }


}
}
