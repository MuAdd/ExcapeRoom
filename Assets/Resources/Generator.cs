using UnityEngine;
using System.Collections;

public class Generator : MonoBehaviour {

	public GameObject prefab;
    public float spawnTime = 1f;

	
	void Start()
	{
		InvokeRepeating ("SpawnObject", spawnTime, spawnTime);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void SpawnObject()
	{
		var newObj = GameObject.Instantiate(prefab, gameObject.transform.position, Quaternion.identity);
	}
}
