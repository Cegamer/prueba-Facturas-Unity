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
    public bool ocupado = false;
    IConstruible objeto = null;
    public Grid parent;
    public void build(GameObject gridObject, int gridIndexX, int gridIndexY, Grid parent)
    {
        this.gridObject = gridObject;
        this.gridIndexX = gridIndexX;
        this.gridIndexY = gridIndexY;
        this.parent = parent;
    }

    void actualizarOcupado(GridSquare[] cuadrosAOcupar)
    {
        foreach(var cuadro in cuadrosAOcupar) { 
            cuadro.gridObject.GetComponent<MeshRenderer>().material.color = Color.red;
            cuadro.ocupado = true;
        }
    }

    public int[] getGridPos()
    {
        return new int[] { gridIndexX, gridIndexY };
    }
    public bool colocar()
    {
        var a = checkAvailability ();
        if (a != null)
        {
            actualizarOcupado(a);
            return true;
        }
        return false;
    }
    public void preview(IConstruible objetoe)
    {
        objetoe.getGameObject().transform.SetParent(gridObject.transform);
        objetoe.getGameObject().transform.localPosition = Vector3.zero;
        objeto = objetoe;
        checkAvailability();
    }


    public GridSquare[] checkAvailability()
    {
        if (objeto != null)
        {
            int width = objeto.getCuadrosHorizontal();
            int height = objeto.getCuadrosVertical();
            int rotation = objeto.getEstadoRotacion();

            // Coordenadas del cuadro principal
            int mainX = gridIndexX;
            int mainY = gridIndexY;

            // Ajustar la posición del cuadro principal según la rotación
            if (rotation == 1)
            {
                mainX -= width - 1;
            }
            else if (rotation == 2)
            {
                mainX -= width - 1;
                mainY -= height - 1;
            }
            else if (rotation == 3)
            {
                mainY -= height - 1;
            }

            // Crear un array de cuadros que ocupará el objeto
            GridSquare[] cuadrosAOcupar = new GridSquare[width * height];
            int index = 0;
            for (int x = mainX; x < mainX + width; x++)
            {
                for (int y = mainY; y < mainY + height; y++)
                {
                    if (x >= 0 && x < parent.grid.GetLength(0) && y >= 0 && y < parent.grid.GetLength(1))
                    {
                        cuadrosAOcupar[index] = parent.grid[x, y];
                        index++;
                    }
                }
            }

            // Verificar si el objeto puede colocarse en los cuadros especificados
            bool puedeColocarse = objeto.puedeColocarse(cuadrosAOcupar);

            // Cambiar el color del objeto en consecuencia
            if (puedeColocarse)
            {
                objeto.getGameObject().GetComponent<MeshRenderer>().material.color = Color.green;
                return cuadrosAOcupar;
            }
            else
            {
                objeto.getGameObject().GetComponent<MeshRenderer>().material.color = Color.red;
            }
            
        }
        return null;
    }

}


