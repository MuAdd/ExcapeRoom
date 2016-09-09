using UnityEngine;
using System.Collections;

public class FuseBehavior : MonoBehaviour {

    GameObject[] bin = new GameObject[8];
    Vector3 liftup = Vector3.up * 0.2f; 
    // Use this for initialization
    void Start () {
            bin = GameObject.FindGameObjectsWithTag("FuseBin");
    }
	
	// Update is called once per frame
	void Update () {
        foreach (GameObject fusebin in bin)
        {
            Bounds binBounds = fusebin.transform.GetComponent<MeshCollider>().bounds;
            if (gameObject.GetComponent<CapsuleCollider>().bounds.Intersects(binBounds))
                {
                gameObject.transform.position = fusebin.transform.position + liftup;
                gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
                }
        }
	}
}
