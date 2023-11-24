using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Lançarbola : MonoBehaviour
{
      public float forceAmount = 5f;
    private Rigidbody rb;
    private InputAction applyForceAction;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Create an InputAction for applying force
        applyForceAction = new InputAction(binding: "<Keyboard>/space", interactions: "press");
        applyForceAction.performed += _ => ApplyForce();
        applyForceAction.Enable();
    }

    private void ApplyForce()
    {
        rb.AddForce(-transform.right * forceAmount, ForceMode.Impulse);
    }

    private void OnDisable()
    {
        applyForceAction.Disable();
    }
}
