using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetUpBlockJoints : MonoBehaviour {



    public Transform root;
    List<StickJoint> joints = new List<StickJoint>();
    public GameObject usePrefab;
    public bool rigidBodies = false;
    public Material mat;
    public int density = 2;
    public int useExistingLayer = -1;
    public CreationType[] ctypes;

    public CreationType creationType;
    public int creationIndex = 1;

    public void SetUp()
    {

        foreach(Transform t in gameObject.GetComponentsInChildren<Transform>())
        {
                if (t.GetComponent<SkinnedMeshRenderer>() != null)
                {
                    t.GetComponent<SkinnedMeshRenderer>().enabled = false;
                }
        }


        Transform[] transforms = root.GetComponentsInChildren<Transform>();
        foreach(Transform t in transforms)
        {
            if(t.GetComponent<StickJoint>() == null && t != root)
            {
              joints.Add(t.gameObject.AddComponent<StickJoint>());
            }
            if (t.GetComponent<SkinnedMeshRenderer>() != null)
            {
                t.GetComponent<SkinnedMeshRenderer>().enabled = false;
            }
        }

        foreach(StickJoint stick in joints)
        {
            stick.FindLink();
        }
        if (GetComponent<Animator>() != null)
        {
            GetComponent<Animator>().cullingMode = AnimatorCullingMode.AlwaysAnimate;
        }
        if (gameObject.GetComponent<Gravitator>() != null)
        {
            Destroy(gameObject.GetComponent<Gravitator>());

        }
        Gravitator grav = gameObject.AddComponent<Gravitator>();
        grav.root = root;
        grav.Init(creationType,density);
        DestroyImmediate(this);

    }

    private void Start()
    {
      //  SetUp();
    }

}
