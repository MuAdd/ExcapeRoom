using UnityEngine;
using System.Collections;

public class GrabAndDrop : MonoBehaviour {

    Material hoverMaterial;
    Material defaultMaterial;

    // Use this for initialization
    void Start()
    {
        hoverMaterial = Resources.Load("Highlight", typeof(Material)) as Material;
        defaultMaterial = Resources.Load("Default-Material", typeof(Material)) as Material;
        Debug.Log(hoverMaterial);
        Debug.Log(defaultMaterial);
    }

    GameObject grabbedObject;
    GameObject[] checkNewObject = new GameObject[2] { null, null };
	float grabbedObjectSize;
    float lineOfSightRange = 5;

	GameObject GetMouseHoverObject(float range)
	{
		Vector3 position = gameObject.GetComponentInChildren<Camera>().transform.position;
		RaycastHit raycastHit;
		Vector3 target = position + Camera.main.transform.forward * range;
        
		if (Physics.Linecast (position, target, out raycastHit))
        {
            return raycastHit.collider.gameObject;
        }
		return null;
	
	}

	void TryGrabObject(GameObject grabObject)
	{
		if (grabObject == null || !CanGrab(grabObject))
			return;

		grabbedObject = grabObject;
		grabbedObjectSize = grabbedObject.GetComponent<Renderer>().bounds.size.magnitude;
        //Remove Rigidbody Constraints imposed by FuseBehavior.cs
        grabbedObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;

    }

    bool CanGrab(GameObject candidate)
    {
        if (candidate == null) return false;
        return candidate.GetComponent<Rigidbody>() != null;
    }

	void DropObject()
	{
		if (grabbedObject == null)
			return;

		if (grabbedObject.GetComponent<Rigidbody> () != null)
			grabbedObject.GetComponent<Rigidbody> ().velocity = Vector3.zero;
		grabbedObject = null;
	}

    void changeMaterial(Vector3 position, Material defaultMaterial, Material hoverMaterial)
    {
        //currently grabbing an object unhighlights the object
        if (grabbedObject != null)
        {
            grabbedObject.GetComponent<Renderer>().material = defaultMaterial;
            return;
        }

        //scan line of sight for object
        RaycastHit raycastHit;
        if (Physics.Linecast(position, position + Camera.main.transform.forward * lineOfSightRange, out raycastHit))
        {
            //same object will return; new object change materials
            checkNewObject[0] = raycastHit.collider.gameObject;
            if (checkNewObject[0] == checkNewObject[1])
            {
                return;
            }
            else
            {
                if (CanGrab(checkNewObject[1]))
                    {
                        //unHighlights old object
                        checkNewObject[1].GetComponent<Renderer>().material = defaultMaterial;
                    }
                if (CanGrab(checkNewObject[0]))
                    {
                        //Highlights new object in line of sight
                        raycastHit.collider.GetComponent<Renderer>().material = hoverMaterial;
                    }
                //stores the current object in line of sight    
                checkNewObject[1] = raycastHit.collider.gameObject;
            }
        }
    }


    public float armLengthModifier = 4f;
    public float shoulderHeightModifier= 0.6f;
	// Update is called once per frame
	void Update () 
	{
        Vector3 position = gameObject.GetComponentInChildren<Camera>().transform.position;
        //Draws line of sight
        Debug.DrawRay(position, Camera.main.transform.forward * lineOfSightRange, Color.green);
        changeMaterial(position, defaultMaterial, hoverMaterial);

        if (Input.GetMouseButtonDown (1)) {
            if (grabbedObject == null)
            {
                TryGrabObject(GetMouseHoverObject(lineOfSightRange));
            }
            else
                DropObject();
		}



		if (grabbedObject != null) {
			Vector3 newPosition =  gameObject.transform.position + Camera.main.transform.forward * grabbedObjectSize * armLengthModifier;
			grabbedObject.transform.position = newPosition + Vector3.up * shoulderHeightModifier;
		}
	}
}
