using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public GridSquare[,] grid = new GridSquare[20, 20];
    public GameObject gridObject;

    // Start is called before the first frame update
    void Start()
    {
        int x = 0, y = 0;
        for (int i = 0; i < 20; i++)
        {
            for (int j = 0; j < 20; j++)
            {
                var obj = Instantiate(gridObject, new Vector3(x, 0, y), Quaternion.identity);
                obj.AddComponent<GridSquare>();
                obj.GetComponent<GridSquare>().build(obj, i, j, this);
                grid[i, j] = obj.GetComponent<GridSquare>();
                x += 5;
            }
            x = 0;
            y += 5;
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}

public class GridSquare : MonoBehaviour
{
    GameObject gridObject = null;
    int gridIndexX, gridIndexY;
    bool ocupado = false;
    IConstruible objeto = null;
    public Grid parent;
    public void build(GameObject gridObject, int gridIndexX, int gridIndexY, Grid parent)
    {
        this.gridObject = gridObject;
        this.gridIndexX = gridIndexX;
        this.gridIndexY = gridIndexY;
        this.parent = parent;
    }

    void actualizarOcupado()
    {
        if (!ocupado)
        {
            gridObject.GetComponent<MeshRenderer>().material.color = Color.red;
        }
    }

    public int[] getGridPos()
    {
        return new int[] { gridIndexX, gridIndexY };
    }
    public void colocar()
    {
        if (!ocupado)
        {
            actualizarOcupado();
        }
    }
    public void preview(IConstruible objetoe)
    {
        objetoe.getGameObject().transform.SetParent(gridObject.transform);
        objetoe.getGameObject().transform.localPosition = Vector3.zero;
        objeto = objetoe;
        Debug.Log(objetoe.getEstadoRotacion());
        checkAvailability();
    }


    public void checkAvailability()
    {
      //Implementar
    }
}


