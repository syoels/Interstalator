using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interstalator{
public class GraphManager : MonoBehaviour {

    public void Start(){
        StartCoroutine(Flow());
    }

    public IEnumerator Flow(){
        yield return new WaitForSeconds(1f);
        Debug.Log("Begin Flow");
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
            yield return new WaitForSeconds(1f);
            Debug.Log("Dequeuing");
            ShipComponent.Transmission transmission = queue.Dequeue();
            ShipComponent curr = transmission.component; 
            curr.UpdateInput(transmission.type, transmission.amount);
            if (curr.GetRemainingIncoming().Count > 0) {
                curr.SetStatus("Waiting for " + curr.GetRemainingIncoming().Count.ToString() + " more inputs");
                queue.Enqueue(transmission);
                continue;
            }

            List<ShipComponent.Transmission> children = curr.Process();
            foreach (ShipComponent.Transmission t in children){
                queue.Enqueue(t);
            }
        }

    }


}
}
