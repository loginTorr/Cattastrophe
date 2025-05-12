using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;

public class RangeAttack : MonoBehaviour {
    [Header("References")]
    public GameObject YarnAttack;
    [Header("Stats")]
    public float SwingDistance = 1.0f;
    public float Speed = 50.0f;
    public float HeightOffset = 0.5f;

    void Update()
    {
        if (Input.GetMouseButtonDown(1)) // right click
        {
            PerformAttack();
        }
    }

    void PerformAttack()
    {
        Vector3 MouseWorldPosition = GetMouseWorldPosition3D();
        Vector3 Direction = (MouseWorldPosition - transform.position).normalized;
        Direction.y = 0; 

        GameObject Yarn = Instantiate(YarnAttack, transform.position, Quaternion.identity);

        Rigidbody rb = Yarn.GetComponent<Rigidbody>();
        rb.velocity = Direction * Speed;

        Yarn.transform.position = (Vector3)transform.position + Direction * SwingDistance + new Vector3(0, HeightOffset, 0);

        float Angle = Mathf.Atan2(Direction.z, Direction.x) * Mathf.Rad2Deg;
        Yarn.transform.rotation = Quaternion.Euler(0, -Angle, 0);
    }

    Vector3 GetMouseWorldPosition3D()
    {
        Ray Ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane GroundPlane = new Plane(Vector3.up, Vector3.zero);

        if (GroundPlane.Raycast(Ray, out float enter))
        {
            return Ray.GetPoint(enter);
        }
        return Vector3.zero;
    }

}
