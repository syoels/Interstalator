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
        Component[] allComponents = FindObjectsOfType<Component>();
        Queue<Component.Transmission> queue = new Queue<Component.Transmission>(); 

        // Start with origins
        foreach(Component c in allComponents){
            if (c.IsOrigin()) {
                Component.Transmission originComponent = new Component.Transmission(c, ElementTypes.None, 0f);
                queue.Enqueue(originComponent);
            }
        }

        while (queue.Count > 0) {
            // Use queue.Peek to check inputs
            Component.Transmission transmission = queue.Dequeue();
            Component curr = transmission.component; 
            curr.UpdateInput(transmission.type, transmission.amount);
            List<Component.Transmission> children = curr.Process();
            foreach (Component.Transmission t in children){
                queue.Enqueue(t);
            }
        }

    }


}
}
