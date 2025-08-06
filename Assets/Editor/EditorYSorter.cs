using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;

[InitializeOnLoad]
public static class EditorYSorter
{
    static EditorYSorter()
    {
        // Called on load, and every time the editor updates
        EditorApplication.hierarchyChanged += OnHierarchyChanged;
        EditorApplication.update += AutoSortVisibleObjects;
        
    }

    [MenuItem("Tools/Sort Selected by Y Position")]
    static void SortSelectedByY()
    {
        GameObject[] selected = Selection.gameObjects;

        if (selected.Length == 0)
        {
            Debug.LogWarning("No GameObjects selected.");
            return;
        }

        Undo.RecordObjects(selected, "Sort by Y Position");

        int updatedCount = 0;

        foreach (GameObject go in selected)
        {
            updatedCount += ApplyYSorting(go);
        }

        Debug.Log($"[Manual] Updated sortingOrder for {updatedCount} SpriteRenderers.");
    }

    static void AutoSortVisibleObjects()
    {
        if (Selection.transforms.Length == 0) return;

        foreach (Transform t in Selection.transforms)
        {
            if (t == null) continue;
            ApplyYSorting(t.gameObject);
        }
    }

    static void OnHierarchyChanged()
    {
        GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();
        foreach (GameObject go in allObjects)
        {

            if (!go.activeInHierarchy) continue;

            ApplyYSorting(go);
        }
    }

    static int ApplyYSorting(GameObject go)
{
    // Skip GameObjects tagged as "Player"
    if (go.CompareTag("Player"))
        return 0;

    int updated = 0;
    SpriteRenderer sr = go.GetComponent<SpriteRenderer>();
    if (sr != null)
    {
        int order = Mathf.RoundToInt(-go.transform.position.y * 100);
        if (sr.sortingOrder != order)
        {
            sr.sortingOrder = order;
            EditorUtility.SetDirty(sr);
            updated++;
        }
    }

    foreach (Transform child in go.transform)
    {
        updated += ApplyYSorting(child.gameObject);
    }

    return updated;
}

}
