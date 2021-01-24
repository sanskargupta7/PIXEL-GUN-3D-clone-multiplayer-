using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField]           //used if we want to kepp the variables private and also want them to show on inspector window.
    float speed = 5f;

    [SerializeField]
    private float lookSensitivity = 3f;                 //initialised the sensitivity of rotation

    [SerializeField]
    GameObject fpsCamera;

    private Vector3 velocity = Vector3.zero;             //initial velocity of the player........will be changed in move() method
    private Vector3 rotation = Vector3.zero;             //rotation vector .....will be changed in rotate() method
    private float CameraUpAndDownRotation = 0f;
    private float CurrentCameraUpAndDownRotation = 0f;

    private Rigidbody rb;
    
    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody>();                              //we will use this rigid body of the player to move
    }

    // Update is called once per frame
    private void Update()
    {
        //Calculate movement velocity as a 3d vector
        float _xMovement = Input.GetAxis("Horizontal");              //horizontal is moving about right or left
        float _zMovement = Input.GetAxis("Vertical");                //vertical for moving about forward


        Vector3 _movementHorizontal = transform.right * _xMovement;
        Vector3 _movementVertical = transform.forward * _zMovement;

        //Final movement velocity
        Vector3 _movementVelocity = (_movementHorizontal + _movementVertical).normalized * speed;


        //Apply movement
        Move(_movementVelocity);



        //calculate rotation as a 3d vector for turning around
        float _yRotation = Input.GetAxis("Mouse X");
        Vector3 _rotationVector = new Vector3(0, _yRotation, 0) * lookSensitivity;

        //Apply Rotation
        Rotate(_rotationVector);

        //Calculate look up and down camera rotation
        float _cameraUpDownRotation = Input.GetAxis("Mouse Y") * lookSensitivity;


        //Apply Rotation
        RotateCamera(_cameraUpDownRotation);

    }


    //runs per physics iteration
    private void FixedUpdate()
    {
        if(velocity!=Vector3.zero)
        {
            rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);        //and so we move the rigid body of the player with which the player will move too
        }                                                                         //all physics and collisions checks are taken care automatically

        rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));

        if(fpsCamera!=null)
        {

            CurrentCameraUpAndDownRotation -= CameraUpAndDownRotation;
            CurrentCameraUpAndDownRotation = Mathf.Clamp(CurrentCameraUpAndDownRotation, -85, 85);


            fpsCamera.transform.localEulerAngles = new Vector3(CurrentCameraUpAndDownRotation, 0, 0);

        }

    }                                                                             //initial position is increased by velocity*time i.e the distance of the movement 


    void Move(Vector3 movementVelocity)
    {
        velocity = movementVelocity;
    }


    void Rotate(Vector3 rotationVector)
    {
        rotation = rotationVector;
    }

    void RotateCamera(float cameraUpAndDownRotation)
    {
        CameraUpAndDownRotation = cameraUpAndDownRotation;

    }

}
