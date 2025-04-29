using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour {
    [Header("References")]
    public GameObject ClawAttack;
    [Header("Stats")]
    public float SwingDistance = 1.0f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))  // Left click
        {
            PerformAttack();
        }
    }

    void PerformAttack()
    {
        Vector3 MouseWorldPosition = GetMouseWorldPosition3D();
        Vector3 Direction = (MouseWorldPosition - transform.position);
        Direction.y = 0; 
        Direction.Normalize();

        GameObject Claw = Instantiate(ClawAttack);

        Claw.transform.position = (Vector3)transform.position + Direction * SwingDistance;

        float angle = Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg;
        Claw.transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    Vector3 GetMouseWorldPosition3D()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);

        if (groundPlane.Raycast(ray, out float enter))
        {
            return ray.GetPoint(enter);
        }
        return Vector3.zero;
    }

}
