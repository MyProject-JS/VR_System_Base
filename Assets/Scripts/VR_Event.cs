using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VR_Event : MonoBehaviour
{
    public Camera MainCamera;


    void Start()
    {
        MainCamera = GetComponent<Camera>();
    }

    public void OnPointerEnter(Transform OBJ)
    {
        OBJ.GetComponent<Renderer>().material.color = Color.green;
    }
    public void OnPointerExit(Transform OBJ)
    {
        OBJ.GetComponent<Renderer>().material.color = Color.white;
    }
    void Update()
    {
        Ray ray = MainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);

        Debug.DrawRay(ray.origin,ray.direction * 100, Color.red);
        RaycastHit2D raycast = Physics2D.Raycast(MainCamera.gameObject.transform.position,  Vector3.forward *-3f);
    }
}
