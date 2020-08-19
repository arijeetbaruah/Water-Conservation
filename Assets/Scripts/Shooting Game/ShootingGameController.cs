using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingGameController : MonoBehaviour
{

    Camera fpsCam;

    public float weaponRange = 50f;

    public float sensitivityX = 15F;
    public float sensitivityY = 15F;

    public float minimumX = -360F;
    public float maximumX = 360F;
    public float minimumY = -60F;
    public float maximumY = 60F;
    float rotationX = 0F;
    float rotationY = 0F;
    Quaternion originalRotation;

    public Texture2D crosshairImage;
    Animator animator;

    private bool playingAnimation = false;
    public bool PlayAnimation
    {
        set { playingAnimation = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        fpsCam = Camera.main;
        originalRotation = transform.localRotation;
        Cursor.visible = false;

        animator = GetComponentInChildren<Animator>();
    }

    void Shoot()
    {
        RaycastHit hit;
        Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));


        if (Physics.Raycast(new Ray(rayOrigin, fpsCam.transform.forward), out hit, weaponRange, 9))
        {
            float point = (hit.transform.position - hit.point).magnitude;
            playingAnimation = true;

            Debug.Log(point);
        }
    }

    // Update is called once per frame
    void Update()
    {
        rotationX += Input.GetAxis("Mouse X") * sensitivityX;
        rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
        rotationX = Mathf.Clamp(rotationX, minimumX, maximumX);
        rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);
        Quaternion xQuaternion = Quaternion.AngleAxis(rotationX, Vector3.up);
        Quaternion yQuaternion = Quaternion.AngleAxis(rotationY, -Vector3.right);

        transform.localRotation = originalRotation * xQuaternion * yQuaternion;

        if (Input.GetAxis("Fire1") != 0 && !playingAnimation)
        {
            animator.SetTrigger("Fire");
            Shoot();
        }
    }

    

    void OnGUI()
    {
         float xMin = (Screen.width / 2) - (crosshairImage.width / 4);
         float yMin = (Screen.height / 2) - (crosshairImage.height / 4);
         GUI.DrawTexture(new Rect(xMin, yMin, crosshairImage.width / 2, crosshairImage.height / 2), crosshairImage);
    }
}
