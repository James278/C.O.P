
using UnityEngine;

//Camera Controller
//Task: Purpose moving around the map and zooming in. 

public class CamController : MonoBehaviour
{
    // speed that you can left to right or up or down
    [SerializeField]
    private float panSpeed = 20f;
    // px range when the mouse hit the edge of the screen to move cam position
    [SerializeField]
    private float panBorderThinkness = 5f;
    // scroll speed zoom out or in
    [SerializeField]
    private float scrollSpeed = 20f;
    //camera x y limit before it stops
    public float negXLimit, negYLimit;
    public float posXLimit, posYLimit;
    // Y limit for scroll 
    public float minY = 20f;
    public float maxY = 100f;
    // Cam vector position
    private Vector3 pos;


    private void Update()
    {
        // set the position of the camera trasform
        pos = transform.position;

        // see methods for comment
        Foreward();
        Backwards();
        Right();
        Left();
        RotateCamera();
        CameraScroll();
        CameraLimits();
        
        // update the camera position
        transform.position = pos;
    }

    private void CameraLimits()
    {
        // track position of the camera and restrict camera movment for x,z,y axis
        pos.y = Mathf.Clamp(pos.y, minY, maxY);

        pos.x = Mathf.Clamp(pos.x, -negXLimit, posXLimit);
        pos.z = Mathf.Clamp(pos.z, -negYLimit, posYLimit);
    }

    private void CameraScroll()
    {
        // scroll wheel of the mouse allow the to zoom in or out
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        pos.y -= scroll * scrollSpeed * 100 * Time.deltaTime;
    }

    private void Foreward()
    {
        // when the w key held down the player will move in + z axis and if the mouse hit the of the screen than will move in the + z axis
        if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panBorderThinkness)
        {

            pos.z += panSpeed * Time.deltaTime;
        }
    }

    private void Backwards()
    {
        //when the w key held down the player will move in + z axis and if the mouse hit the of the screen than will move in the - z axis
        if (Input.GetKey("s") || Input.mousePosition.y <= panBorderThinkness)
        {

            pos.z -= panSpeed * Time.deltaTime;
        }
    }

    private void Right()
    {
        //when the w key held down the player will move in + x axis and if the mouse hit the of the screen than will move in the + x axis
        if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBorderThinkness)
        {

            pos.x += panSpeed * Time.deltaTime;
        }
    }

    private void Left()
    {
        //when the w key held down the player will move in - x axis and if the mouse hit the of the screen than will move in the - x axis
        if (Input.GetKey("a") || Input.mousePosition.x <= panBorderThinkness)
        {

            pos.x -= panSpeed * Time.deltaTime;
        }
    }

    private void RotateCamera()
    {
        // rotate the camera on y axis

        // set the rotation on the y axis
        int YRot = 15;

        // set the restrict movement into the y axis
        Vector3 rotationValue;

        //press key down the rotate 15 degree in the + y axis
        if (Input.GetKeyDown(KeyCode.Q))
        {
            rotationValue = new Vector3(0, YRot,0);
            transform.eulerAngles = transform.eulerAngles + rotationValue;
        }

        //press key down the rotate 15 degree in the - y axis
        if (Input.GetKeyDown(KeyCode.E))
        {
            rotationValue = new Vector3(0, YRot, 0);
            transform.eulerAngles = transform.eulerAngles - rotationValue;
        }
    }

}
