using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class cameraController : MonoBehaviour

{
    public float sensitivity = 3f,velocidadMoviminento=5f;
    public Transform selected = null;
    public bool clientMode;
    // Start is called before the first frame update
    void Start()

    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        this.transform.rotation = new Quaternion(0, 0, 0, 0);

    }

    // Update is called once per frame
    void Update()
    {
        var c = Camera.main.transform;
        c.eulerAngles += sensitivity * new Vector3(-Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0);


        moveItem();
        moverEnModoCliente();
    }
    public void moveItem() {
        if (Input.GetMouseButtonDown(0))
            if (selection() != null)
                selected = selection().transform;

        if (Input.GetMouseButtonUp(0))
            selected = null;

        if (selected != null && selected.GetComponent<Producto>() != null)
            selected.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 5));
    }

    public GameObject selection() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 100)) {
            Debug.DrawLine(Camera.main.ScreenToWorldPoint(Input.mousePosition), hit.transform.position);
            if(hit.transform.gameObject.GetComponent<Button>() != null) hit.transform.gameObject.GetComponent<Button>().onClick.Invoke();
            return hit.transform.gameObject;
        }
        return null;
    }

    void moverEnModoCliente() {
        if (clientMode) {
            float movimientoHorizontal = Input.GetAxis("Horizontal");
            float movimientoVertical = Input.GetAxis("Vertical");
            Vector3 movimiento = new Vector3(movimientoHorizontal, 0.0f, movimientoVertical);
            movimiento.Normalize();
            transform.Translate(movimiento * velocidadMoviminento * Time.deltaTime);
            transform.position = new Vector3(transform.position.x, 3.77f, transform.position.z);
        }
    }
}
