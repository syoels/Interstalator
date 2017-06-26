using System;
using UnityEngine;

namespace Interstalator {
public class NewGraphicalGardenHose : NewGraphicalShipComponent{

    private int direction = 0;
    private int directionParamId;
    private int directions = 3; 

    protected override NewShipComponentOutput[] InnerProcess() {
        throw new NotImplementedException();
    }

    protected override void SetAnimationParameterIds() {
        base.SetAnimationParameterIds();
        directionParamId = Animator.StringToHash("direction");
    }

    public void Toggle(){
        direction = (direction + 1) % directions;
        GameManager.instance.flowManager.Flow();
    }
}
}

