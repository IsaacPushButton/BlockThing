using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateFromPrimitive : CreationType {

    public PrimitiveType primitive;
    public Material material;
    public Vector3 scale = Vector3.one * .3f;//new Vector3(.05f, .05f, .05f);
    public override Floater MakeBit(StickJoint limb, StickJoint linked, float point)
    {
     GameObject child = GameObject.CreatePrimitive(primitive);
        child.transform.localScale = scale;

        if(material != null)
        {
            child.GetComponent<MeshRenderer>().sharedMaterial = material;
        }
        GameObject newFloater = new GameObject("Scaler");
        newFloater.transform.position = child.transform.position;
        child.transform.SetParent(newFloater.transform);

        Floater fl = new Floater(limb, linked, newFloater.transform, point);

        Vector3 heading = fl.start.transform.position - fl.end.transform.position;
        fl.floater.transform.position = fl.start.transform.position - heading * fl.point;


        return fl;
    }
}
