using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : LimbBehavior {
    public Vector3 orbitAxis = Vector3.up;
    public float orbitRadius = 10f;
    public float orbitRadiusCorrectionSpeed = .5f;
    public float orbitRotationSpeed = 1f;
    public override void UpdatePosition(StickJoint limb, List<Floater> floaters, float globalScale)
    { 


        foreach (Floater fl in floaters)
        {
            if (fl.floater != null)
            {
                Vector3 heading = fl.start.transform.position - fl.end.transform.position;
                Vector3 desiredPosition = fl.start.transform.position - heading * fl.point;
                fl.floater.transform.localScale = Vector3.one * globalScale;



                fl.floater.RotateAround(desiredPosition, orbitAxis, orbitRotationSpeed * Time.deltaTime);
                Vector3 orbitDesiredPosition = (fl.floater.position - desiredPosition).normalized * orbitRadius + desiredPosition;
                fl.floater.position = Vector3.Slerp(fl.floater.position, orbitDesiredPosition, Time.deltaTime * orbitRadiusCorrectionSpeed);
            }
        }

    }
}
