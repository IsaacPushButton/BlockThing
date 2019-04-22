using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public abstract class CreationType : ScriptableObject {

    public abstract Floater MakeBit(StickJoint limb, StickJoint linked, float point);
}
