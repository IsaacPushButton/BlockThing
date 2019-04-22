using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateFromPrefab : CreationType {
    public GameObject prefab;
    public override Floater MakeBit(StickJoint limb, StickJoint linked, float point)
    {



        Floater fl = new Floater(limb, linked, Instantiate(prefab).transform, point);

        Vector3 heading = fl.start.transform.position - fl.end.transform.position;
        fl.floater.transform.position = fl.start.transform.position - heading * fl.point;


        return fl;

    }
}
