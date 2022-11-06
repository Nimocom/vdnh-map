using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraTarget : MonoBehaviour
{
    Transform cameraTransform;
    [SerializeField] float speed;
    // Start is called before the first frame update
    void Start()
    {
        cameraTransform = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (!Input.GetMouseButton(0))
            return;

        float x = Input.GetAxis("Mouse X");
        float y = Input.GetAxis("Mouse Y");

        Vector3 movementVector = new Vector3(-x, 0f, -y);

        Vector3 faceDirection = new Vector3(cameraTransform.forward.x, 0, cameraTransform.forward.z);
        float cameraAngle = Vector3.SignedAngle(Vector3.forward, faceDirection, Vector3.up);
        var cameraRelativeEulers = Quaternion.Euler(0, cameraAngle, 0);

        transform.Translate(cameraRelativeEulers * movementVector * speed * Time.deltaTime);
    } 
}
