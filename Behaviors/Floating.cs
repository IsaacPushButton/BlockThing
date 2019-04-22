using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Floating : LimbBehavior {
    [Range(0,100)]
    public float noise = 0;
    [Range(0.001f,1)]
    public float power = .1f;
    public override void UpdatePosition(StickJoint limb, List<Floater> floaters, float globalScale)
    {
        foreach (Floater fl in floaters)
        {
            if (fl.floater != null)
            {
                Vector3 heading = fl.start.transform.position - fl.end.transform.position;
                Vector3 desiredPosition = fl.start.transform.position - heading * fl.point;

                Vector3 slerped = Vector3.Slerp(fl.floater.transform.position, desiredPosition + (fl.randomOffset * (noise + 0.01f)), (heading.magnitude * power));
                fl.floater.transform.localScale = Vector3.one * globalScale;
                //                   Debug.Log(heading + " " + desiredPosition + " " + slerped);
                if (!float.IsNaN(slerped.x))
                {
                    if (fl.rb != null)
                    {
                        fl.rb.MovePosition(slerped);
                    }
                    else
                    {
                        fl.floater.position = slerped;
                    }
                }
                fl.floater.rotation = Quaternion.Lerp(fl.floater.rotation, limb.transform.rotation, .66f);
            }
        }
    }
}
