//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class RaycastItemAligner : MonoBehaviour
//{
//    public float raycastDistance = 100f;
//    public GameObject objectToSpawn;

//    // Start is called before the first frame update
//    void Start()
//    {
//        PositionRaycast();
//    }

//    void PositionRaycast()
//    {
//        RaycastHit hit;

//        if (Physics.Raycast(transform.position, Vector3.down, out hit, raycastDistance))
//        {
//            Quaternion spawnRotation = Quaternion.FromToRotation(Vector3.up, hit.normal);

//            GameObject clone = Instantiate(objectToSpawn, hit.point, spawnRotation);
//        }
//    }
//}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class RaycastItemAligner : MonoBehaviour
{
    public GameObject[] itemsToPickFrom;
    public float raycastDistance = 100f;
    public float overlapTestBoxSize = 1f;
    public LayerMask spawnedObjectLayer;
    public bool constantSpawn = false;
    public float spawnTime = 0f;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        PositionRaycast();
    }

	private void Update()
	{
        if (constantSpawn)
        {
            if (timer <= 0)
            {
                PositionRaycast();
                timer = spawnTime;
            }

            timer -= Time.deltaTime;
        }
	}

	void PositionRaycast()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.down, out hit, raycastDistance))
        {

            Quaternion spawnRotation = Quaternion.FromToRotation(Vector3.up, hit.normal);

            Vector3 overlapTestBoxScale = new Vector3(overlapTestBoxSize, overlapTestBoxSize, overlapTestBoxSize);
            Collider[] collidersInsideOverlapBox = new Collider[1];
            int numberOfCollidersFound = Physics.OverlapBoxNonAlloc(hit.point, overlapTestBoxScale, collidersInsideOverlapBox, spawnRotation, spawnedObjectLayer);

            Debug.Log("number of colliders found " + numberOfCollidersFound);

            if (numberOfCollidersFound == 0)
            {
                Debug.Log("spawned robot");
                Pick(hit.point, spawnRotation);
            }
            else
            {
                Debug.Log("name of collider 0 found " + collidersInsideOverlapBox[0].name);
            }
        }
    }

    void Pick(Vector3 positionToSpawn, Quaternion rotationToSpawn)
    {
        int randomIndex = Random.Range(0, itemsToPickFrom.Length);
        GameObject clone = Instantiate(itemsToPickFrom[randomIndex], positionToSpawn, rotationToSpawn);
    }
}