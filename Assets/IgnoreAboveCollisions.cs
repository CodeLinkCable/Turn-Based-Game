using UnityEngine;

public class IgnoreAboveCollisions : MonoBehaviour
{
    private Collider2D[] aboveColliders;

    void Start()
    {
        // Find all colliders on objects in the "Above" sorting layer
        SpriteRenderer[] renderers = FindObjectsOfType<SpriteRenderer>();
        var collidersList = new System.Collections.Generic.List<Collider2D>();

        foreach (var renderer in renderers)
        {
            if (renderer.sortingLayerName == "Above")
            {
                Collider2D col = renderer.GetComponent<Collider2D>();
                if (col != null)
                {
                    collidersList.Add(col);
                }
            }
        }

        aboveColliders = collidersList.ToArray();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ToggleAboveColliders(false); // Disable collision
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            ToggleAboveColliders(true); // Re-enable collision
        }
    }

    void ToggleAboveColliders(bool enabled)
    {
        foreach (var col in aboveColliders)
        {
            if (col != null)
                col.enabled = enabled;
        }
        // if (enabled)
        // {
        //     this.gameObject.GetComponent<SpriteRenderer>().sortingOrder = this.gameObject.GetComponent<SpriteRenderer>().sortingOrder - 20;
        // }
        // else
        // {
        //     this.gameObject.GetComponent<SpriteRenderer>().sortingOrder = this.gameObject.GetComponent<SpriteRenderer>().sortingOrder + 20;
        // }
    }
}
