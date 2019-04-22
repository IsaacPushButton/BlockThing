using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickJoint : MonoBehaviour {
    public StickJoint linked;
   // public float scale = 1;
    public List<Floater> floaters = new List<Floater>();
    public void FindLink()
    {
        if (linked == null)
        {
            if (transform.parent.GetComponent<StickJoint>() != null)
            {
                linked = transform.parent.GetComponent<StickJoint>();
            }
            else if (transform.parent.GetComponentInParent<StickJoint>() != null)
            {
                linked = transform.parent.GetComponentInParent<StickJoint>();
            }
        }
    }
    //public void GetUp()
    //{
    //    root.localScale = root.localScale * 2f;
    //    if (completeness <= 0)
    //    {
    //        return;
    //    }

    //    knockout = false;
    //    foreach (Floater fl in floaters)
    //    {
    //        // fl.col.enabled = false;
    //        if (fl.rb != null)
    //        {
    //            fl.rb.useGravity = false;
    //            fl.rb.isKinematic = true;
    //        }
    //    }
    //}

    //public void KnockOut()
    //{

    //    knockout = true;
    //    List<int> remove = new List<int>();
    //    int a = 0;
    //    foreach (Floater fl in floaters)
    //    {
    //        fl.col.enabled = true;
    //        if (fl.rb != null)
    //        {
    //            fl.rb.useGravity = true;
    //            fl.rb.isKinematic = false;
    //            fl.rb.AddExplosionForce(1000f, fl.floater.position + transform.forward, 1f);
    //        }
    //        a++;
    //    }
    //    floaters.RemoveAll(o => o.point > completeness);
    //    Invoke("GetUp", 15f);
    //}
}
