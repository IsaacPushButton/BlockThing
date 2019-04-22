using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fixed : LimbBehavior
{

    public override void UpdatePosition(StickJoint limb, List<Floater> floaters,float globalScale)
    {
        foreach (Floater fl in floaters)
        {
            if (fl.floater != null)
            {
                Vector3 heading = fl.start.transform.position - fl.end.transform.position;
                Vector3 desiredPosition = fl.start.transform.position - heading * fl.point;

                fl.floater.transform.localScale = Vector3.one * globalScale;

                if (fl.rb != null)
                {
                    fl.rb.MovePosition(desiredPosition);
                }
                else
                {
                    fl.floater.position = desiredPosition;
                }

                fl.floater.rotation = limb.transform.rotation;
            }
        }
    }
}
