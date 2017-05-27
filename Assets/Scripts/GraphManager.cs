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
        Queue<ShipComponent.Output> queue = new Queue<ShipComponent.Output>(); 

        // Start with origins
        foreach(ShipComponent c in allComponents){
            if (c.IsOrigin()) {
                ShipComponent.Output originComponent = new ShipComponent.Output(c, ElementTypes.None, 0f);
                queue.Enqueue(originComponent);
            }
        }

        while (queue.Count > 0) {
            yield return new WaitForSeconds(1f);
            Debug.Log("Dequeuing");
            ShipComponent.Output transmission = queue.Dequeue();
            ShipComponent curr = transmission.component; 
            curr.UpdateInput(transmission.type, transmission.amount);
            if (curr.GetRemainingIncoming().Count > 0) {
                curr.SetStatus("Waiting for " + curr.GetRemainingIncoming().Count.ToString() + " more inputs");
                queue.Enqueue(transmission);
                continue;
            }

            List<ShipComponent.Output> children = curr.Process();
            foreach (ShipComponent.Output t in children){
                queue.Enqueue(t);
            }
        }

    }


}
}
