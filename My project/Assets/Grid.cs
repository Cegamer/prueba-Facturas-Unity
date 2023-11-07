using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public GridSquare[,] grid = new GridSquare[20, 20];
    public GameObject gridObject;
    public cameraController cameraController;

    // Start is called before the first frame update
    void Start()
    {
        generarGrid();
        cargarPredeterminados();
    }

    void generarGrid () {
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

    void cargarPredeterminados() {
        GridSquare[] inicial = new GridSquare[] { grid[0, 0], grid[0, 1], grid[0, 2], grid[1, 0], grid[1, 1], grid[1, 2] };
        actualizarCuadrosOcupados(inicial);
        IConstruible soporteDefault1 = new soporteLargo();
        soporteDefault1.setGameObject(Instantiate(cameraController.construiblesGO[0]));
        soporteDefault1.rotar(1);
        grid[3, 1].preview(soporteDefault1);
        grid[3, 1].colocarObjetoConstruible();
        cameraController.construibles.Add(soporteDefault1);

        IConstruible soporteDefault2 = new soporteLargo();
        soporteDefault2.setGameObject(Instantiate(cameraController.construiblesGO[0]));
        soporteDefault2.rotar(1);

        grid[3, 3].preview(soporteDefault2);
        grid[3, 3].colocarObjetoConstruible();
        cameraController.construibles.Add(soporteDefault2);

        IConstruible soporteDefault3 = new soporteLargo();
        soporteDefault3.setGameObject(Instantiate(cameraController.construiblesGO[0]));
        soporteDefault3.rotar(1);
        grid[3, 5].preview(soporteDefault3);
        grid[3, 5].colocarObjetoConstruible();
        cameraController.construibles.Add(soporteDefault3);
    }

    public void actualizarCuadrosOcupados(GridSquare[] cuadrosAOcupar)
    {
        foreach (var cuadro in cuadrosAOcupar)
        {
            cuadro.gridObject.GetComponent<MeshRenderer>().material.color = Color.red;
            cuadro.ocupado = true;
        }
    }
    public void setVisible() { foreach (var a in grid) { a.gridObject.GetComponent<MeshRenderer>().enabled = true; } }
    public void setInvisible() { foreach (var a in grid) { a.gridObject.GetComponent<MeshRenderer>().enabled = false; } }
}




