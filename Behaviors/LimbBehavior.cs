using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public abstract class LimbBehavior : ScriptableObject  {

    public abstract void UpdatePosition(StickJoint limb, List<Floater> floaters, float globalScale);
}
