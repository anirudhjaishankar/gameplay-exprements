using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController player;
    public Transform cam;
    public GameObject ghostTrail;
    public List<GameObject> cloneList;
    public GameObject playerObject;
    public float walkingSpeed = 6f;
    public float runningSpeed = 10f;
    public float turnSmoothTime = 0.1f;
    public float dashSpeed = 20f;
    public float currentSmoothVelocity;
    float firstDashTapTime = 0.0f;
    public bool isGhosting = false;

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref currentSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            float speed = walkingSpeed;
            if (Input.GetKey(KeyCode.LeftShift))
            {
                speed = runningSpeed;

                if (Input.GetKeyDown(KeyCode.LeftControl))
                {
                    ghostTrail.SetActive(true);
                }
                if (Input.GetKey(KeyCode.LeftControl))
                {
                    firstDashTapTime += Time.deltaTime;

                    if (!(firstDashTapTime > 0.3f))
                    {
                        speed = dashSpeed;
                        if(cloneList.Count < 30 && firstDashTapTime > 0.05)
                        {
                            CreateClones();
                        }
                    }
                }
            }
            if (Input.GetKeyUp(KeyCode.LeftControl))
            {
                ghostTrail.SetActive(false);
                DestroyAllClones();
            }


            if (Input.GetKeyUp(KeyCode.LeftControl))
            {
                firstDashTapTime = 0.0f;
            }
            player.Move(moveDir.normalized * speed * Time.deltaTime);
        }

    }

    void CreateClones()
    {
        GameObject clone = Instantiate(playerObject);
        clone.SetActive(true);
        clone.transform.position = player.transform.position;
        clone.transform.rotation = player.transform.rotation;
        clone.transform.localScale = player.transform.localScale;

        cloneList.Add(clone);
    }

    void DestroyAllClones()
    {
        if (cloneList.Any())
        {
            foreach (GameObject clone in cloneList)
            {
                new WaitForSeconds(1);
                Destroy(clone);
            }
            cloneList.Clear();
        }
    }

    private void OnDestroy()
    {
        foreach (GameObject clone in cloneList)
        {
            Destroy(clone);
        }
    }
}
