using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;
using UnityEditor;

[System.Serializable]
public struct Floater
{

   public StickJoint start;
    public StickJoint end;
    public Collider col;

   public Transform floater;
    public Rigidbody rb;

   public Vector3 randomOffset;
    public float point;
  
   public Floater(StickJoint start, StickJoint end, Transform floater, float point)
    {
        this.start = start;
                this.end = end;
        
            this.floater = floater;
        this.col = floater.GetComponent<Collider>();
        this.randomOffset = Random.insideUnitSphere;
        this.point = Mathf.Clamp(point,0,1);
        this.rb = floater.GetComponent<Rigidbody>();
    }
        
}
public class Gravitator : MonoBehaviour {

    public Transform root;
    [SerializeField]
    List<StickJoint> limbs;// = new List<StickJoint>();
    public LimbBehavior behavior;
    public float globalScale = 0.5f;
  public LimbBehavior[]  behaviors;
    public int behaviorIndex = 2;
    void FindLimbs()
    {
        if (limbs == null)
        {
            limbs = new List<StickJoint>();
        }
        foreach (Transform child in root.GetComponentsInChildren<Transform>())
        {
            if (child.GetComponent<StickJoint>() != null)
            {
                limbs.Add(child.GetComponent<StickJoint>());
            }
      
        }

    }
   

    void CreateBits(CreationType ct, int density)
    {
      
         Transform   container = new GameObject(gameObject.name + " container").transform;

        int bits = density;//qty;
   
        foreach (StickJoint limb in limbs)
        {
            bool terminating = limb.linked == null;
          
            for (int i = 0; i < bits; i++)
            {


                float point = (float)i / (float)bits;

            StickJoint linked = limb.linked;
            if(linked == null)
            {
                linked = limb;

            }
            Floater fl = ct.MakeBit(limb,linked,point);//new Floater(limb, limb.linked ?? limb, newFloater.transform, point);
                limb.floaters.Add(fl);
                fl.floater.transform.SetParent(container);
                fl.floater.gameObject.layer = 2;

                if (terminating)
                {
                    i = bits;
                }
            }
        }
    }

    public void Init(CreationType ct,int density)
    {
       // behavior = new Floating();

        FindLimbs();
        CreateBits(ct,density);
    }


    void UpdatePositions()
    {

        foreach (StickJoint sj in limbs)
        {
            if(sj == null)
            {
                Debug.LogError("sj was null");
            }else if(sj.floaters == null)
            {
                Debug.LogError("sj.floaters wa snull");
            }
            behavior?.UpdatePosition(sj,sj.floaters,globalScale);
        }

        
    }


    private void FixedUpdate()
    {
 
            UpdatePositions();
     
    }
    

}
