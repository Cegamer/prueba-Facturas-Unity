using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class cameraController : MonoBehaviour

{
    public float sensitivity = 3f;
    public Transform selected = null;
    // Start is called before the first frame update
    void Start()

    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        this.transform.rotation = new Quaternion(0,0,0,0);   

    }

    // Update is called once per frame
    void Update()
    {
        var c = Camera.main.transform;
        c.eulerAngles += sensitivity * new Vector3(-Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0);


        moveItem();
    }
    public void moveItem() {
        if (Input.GetMouseButtonDown(0))
            if(selection() != null)
            selected =  selection().transform;
        
        if(Input.GetMouseButtonUp(0)) 
            selected = null;

        if (selected != null && selected.GetComponent<Producto>() != null)
            selected.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 5));
    }

    public GameObject selection() { 
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 100)) {
            Debug.DrawLine(Camera.main.ScreenToWorldPoint(Input.mousePosition),hit.transform.position);
            return hit.transform.gameObject;
        }
        return null;
    }
}
