using System.Collections;
using UnityEngine;

public class HoldingObject : MonoBehaviour
{
    public Transform anchorPoint;            // Where the object will follow
    private GameObject holdingObject;        // The instance of the prefab
    private bool isHolding = false;          // Track if object is currently being held

    public void InstantiateGameObject(GameObject newObject)
    {
        if (holdingObject == null)
        {
            holdingObject = Instantiate(newObject, anchorPoint.position, anchorPoint.rotation);
            isHolding = true;
            StartCoroutine(Hold());
        }
        else
        {
            // probably a voiceline saying Mochi's hands are full
            Debug.Log("Mochi's hands are full!");
        }
    }

    IEnumerator Hold()
    {
        while (isHolding && holdingObject != null)
        {
            // Smoothly follow the anchor point
            holdingObject.transform.position = Vector3.Lerp(holdingObject.transform.position, anchorPoint.position, Time.deltaTime * 10f);
            holdingObject.transform.rotation = Quaternion.Slerp(holdingObject.transform.rotation, anchorPoint.rotation, Time.deltaTime * 10f);

            yield return null;
        }
    }

    // Call this when the player interacts in VR (like grabbing the object)
    public void OnPlayerInteract()
    {
        isHolding = false;
    }
}
