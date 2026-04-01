using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Vector2 mousePos;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            ClickTarget();
        }
    }

    private void ClickTarget()
    {
        Collider2D hit = Physics2D.OverlapPoint(mousePos);
        
        if (hit == null)
        {
            Debug.Log("Empty target clicked");
            return;
        }

        if (hit.CompareTag("Target"))
        {
            Debug.Log("Target " + hit.name + " clicked");
            Destroy(hit.gameObject);
        }
    }
}
