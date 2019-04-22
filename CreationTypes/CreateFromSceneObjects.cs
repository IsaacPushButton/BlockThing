using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CreateFromSceneObjects : CreationType {

    public int collectFromLayer = 8;
    public override Floater MakeBit(StickJoint limb, StickJoint linked, float point)
    {

        //Queue<Transform> existing = new Queue<Transform>(GameObject.FindObjectsOfType<Transform>().Where(o => o.gameObject.layer == 8));
        //GameObject sort = new GameObject();
        //foreach (Transform t in existing)
       // {
          //  t.SetParent(sort.transform);
        //}
        GameObject newFloater = new GameObject("Scaler");
        GameObject child = GameObject.FindObjectsOfType<Transform>().FirstOrDefault(o => o.gameObject.layer == collectFromLayer && o.transform.childCount == 0)?.gameObject; //existing.Dequeue().gameObject;
        if(child != null)
        {
            newFloater.transform.position = child.transform.position;
            child.layer = 0;
            child.transform.SetParent(newFloater.transform);
        }
     



        return new Floater(limb,linked,newFloater.transform,point);
    }
}
