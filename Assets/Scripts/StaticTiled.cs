using UnityEngine;

public class StaticTiled : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
            GetComponent<SpriteRenderer>().sortingOrder = -(int)(transform.position.y * 100);
            
    }

    // Update is called once per frame
void Update() {
}

}
