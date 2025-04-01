using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class Play_VRInteraction : MonoBehaviour
{
    public MochiManager mochiManager;
    public Transform rayOrigin;
    public float raycastDistance = 10f;
    public LayerMask interactableLayer;

    [Header("Input Action")]
    public InputActionReference interactAction;

    [Header("Laser Pointer")]
    public LineRenderer lineRenderer;

    [Header("Highlighting")]
    private GameObject lastHovered;
    private Material originalMaterial;
    public Material highlightMaterial;

    void OnEnable()
    {
        interactAction.action.Enable();
    }

    void OnDisable()
    {
        interactAction.action.Disable();
    }

    void Update()
    {
        Ray ray = new Ray(rayOrigin.position, rayOrigin.forward);
        RaycastHit hit;
        Vector3 endPosition = rayOrigin.position + rayOrigin.forward * raycastDistance;

        if (Physics.Raycast(ray, out hit, raycastDistance, interactableLayer))
        {
            endPosition = hit.point;

            GameObject hitObject = hit.collider.gameObject;

            // Highlighting
            if (hitObject.CompareTag("interactable"))
            {
                if (lastHovered != hitObject)
                {
                    ClearHighlight();
                    lastHovered = hitObject;

                    Renderer rend = lastHovered.GetComponent<Renderer>();
                    if (rend != null)
                    {
                        originalMaterial = rend.material;
                        rend.material = highlightMaterial;
                    }
                }

                if (interactAction.action.triggered)
                {
                    mochiManager.ChangeInteractable(hitObject);
                }
            }
            else
            {
                ClearHighlight();
            }
        }
        else
        {
            ClearHighlight();
        }

        // Update laser positions
        if (lineRenderer)
        {
            lineRenderer.SetPosition(0, rayOrigin.position);
            lineRenderer.SetPosition(1, endPosition);
        }
    }

    void ClearHighlight()
    {
        if (lastHovered != null)
        {
            Renderer rend = lastHovered.GetComponent<Renderer>();
            if (rend != null && originalMaterial != null)
            {
                rend.material = originalMaterial;
            }
            lastHovered = null;
        }
    }
}
