using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
    public LayerMask mask;
    public Interactable focus;

    private Camera cam;
    private PlayerMotor motor;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        motor = GetComponent<PlayerMotor>();
        focus = null;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit, mask))
            {
                motor.MoveToPoint(hit.point);
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Interactable interactable = hit.collider.gameObject.GetComponent<Interactable>();
                if(interactable != null)
                {
                    SetFocus(interactable);
                }
                else if (focus != null)
                {
                    ClearFocus();
                }
            }
        }
    }

    void SetFocus(Interactable focus)
    {
        if(this.focus != null)
        {
            ClearFocus();
        }

        focus.OnFocus(transform);
        this.focus = focus;
        motor.Follow(focus);
    }

    void ClearFocus()
    {
        focus.OnDefocus();
        focus = null;
        motor.StopFollowing();
    }
}
