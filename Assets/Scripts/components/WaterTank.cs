using System;
using System.Linq;

namespace Interstalator {
public class WaterTank : Component {

    private float _pressure;

    public float pressure {
        set { 
            _pressure = value; 
        }
        get { 
            return _pressure;
        }
    }


    public override void Process(params TransferredElement[] inputs) {

        // Transfer water to all children with "water connection". 
        // TODO: This is just an example for a possible flow of elements throughout the ship's graph, it can be run through a manager script. 
        foreach (Connection connection in outgoing.Where(n => n.type == ElementTypes.Water)) {
            Component child = connection.connectedComponent;
            TransferredElement water = new TransferredElement(ElementTypes.Water, _pressure); 
            child.Process(water);
        }

        return; 
    }

}
}

