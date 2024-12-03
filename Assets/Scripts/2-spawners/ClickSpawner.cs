using UnityEngine;
using UnityEngine.InputSystem;

/**
 * This component spawns the given object whenever the player clicks a given key.
 */
public class ClickSpawner: MonoBehaviour {
    [SerializeField] protected InputAction spawnAction = new InputAction(type: InputActionType.Button);
    [SerializeField] protected GameObject prefabToSpawn;
    [SerializeField] protected Vector3 velocityOfSpawnedObject;

    void OnEnable()  {
        spawnAction.Enable();
    }

    void OnDisable()  {
        spawnAction.Disable();
    }
    
    protected virtual GameObject spawnObject() {
        //Debug.Log("Spawning a new object");

        // Step 1: spawn the new object.
        Vector3 positionOfSpawnedObject = transform.position;  // span at the containing object position.
        Quaternion rotationOfSpawnedObject = Quaternion.identity;  // no rotation.
        GameObject newObject = Instantiate(prefabToSpawn, positionOfSpawnedObject, rotationOfSpawnedObject);

        // Step 2: modify the velocity of the new object.
        Mover newObjectMover = newObject.GetComponent<Mover>();
        if (newObjectMover) {
            newObjectMover.SetVelocity(velocityOfSpawnedObject);
        }

        return newObject;
    }
    protected virtual GameObject SpawnObjectAtPlayerPosition(Vector2 throwDirection, float throwForce)
    {
        // Step 1: spawn the new object.
        Vector3 positionOfSpawnedObject = transform.position;  // spawn at the player's position.
        Quaternion rotationOfSpawnedObject = Quaternion.identity;  // no rotation.
        GameObject newObject = Instantiate(prefabToSpawn, positionOfSpawnedObject, rotationOfSpawnedObject);

        // Step 2: Add force to simulate a throw (using Rigidbody2D).
        Rigidbody2D rb = newObject.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;  // Ensure there is no previous velocity
            rb.AddForce(throwDirection * throwForce, ForceMode2D.Impulse);  // Apply the throw force
        }

        return newObject;
    }
    private void Update() {
        if (spawnAction.WasPressedThisFrame()) {
            spawnObject();
        }
    }
}
