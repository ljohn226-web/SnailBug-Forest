using UnityEngine;

public class Player : MonoBehaviour
{
    public Camera cam;
    public RiverBehavior river; //ref to river script
    public KittyAI kitty;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
      kitty = GetComponent<KittyAI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("River"))
                {
                    kitty.StartFishing(hit.point);
                }
            }
        }
    }
}
