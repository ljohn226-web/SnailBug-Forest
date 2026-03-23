using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCam : MonoBehaviour
{

    public float sensitiveX;
    public float sensitiveY;

    public Transform orientation;

    float xRoto;
    float yRoto;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //lock cursor to screen center to start and make invis
        Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "LoseScene")
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        } else
        {
            //get mouse input
            float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensitiveX;
            float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensitiveY;

            //cuz unity is weird
            yRoto += mouseX;
            xRoto -= mouseY;
            xRoto = Mathf.Clamp(xRoto, -90F, 90F); //clamp so head cant look 360 degrees

            //rotate cam and player orientation
            transform.rotation = Quaternion.Euler(xRoto, yRoto, 0); //on both axes
            orientation.rotation = Quaternion.Euler(0, yRoto, 0); // on y axis only
        }
            
    }
}
