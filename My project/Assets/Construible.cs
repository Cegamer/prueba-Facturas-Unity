using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class Construible : MonoBehaviour
{
}

public interface IConstruible
{

    public void colocar(GridSquare[] cuadrosAOcupar) { }
    public void rotar() { }
    public GameObject getGameObject();
    public int getEstadoRotacion();
    public int getCuadrosHorizontal();
    public int getCuadrosVertical();
    public bool puedeColocarse(GridSquare[] cuadrosAOcupar);

}
class soporte : IConstruible
{
    public int cuadrosHorizontal;
    public int cuadrosVertical;
    public int estadoRotacion = 0;
    public GameObject objetoConstruible;

    public int getEstadoRotacion() { return estadoRotacion; }
    public int getCuadrosHorizontal() { return cuadrosHorizontal; }
    public int getCuadrosVertical() { return cuadrosVertical; }

    public bool puedeColocarse(GridSquare[] cuadrosAOcupar)
    {
        foreach (var cuadro in cuadrosAOcupar)
            if (cuadro == null || cuadro.ocupado)
                return false;    
        return true;
    }
    public soporte() { cuadrosHorizontal = 3; cuadrosVertical = 1; }
    public void colocar(GridSquare[] cuadrosAOcupar) { }
    public void rotar(int rotado)
    {
        estadoRotacion = rotado;
        switch (rotado)
        {
            case 0:
                objetoConstruible.transform.rotation = Quaternion.Euler(-90,0,0);
                break;
            case 1:
                objetoConstruible.transform.rotation = Quaternion.Euler(-90, 90, 0);
                break;
            case 2:
                objetoConstruible.transform.rotation = Quaternion.Euler(-90, 180, 0);
                break;
            case 3:
                objetoConstruible.transform.rotation = Quaternion.Euler(-90, 360, 0);
                break;
        }
    }
    public GameObject getGameObject() { return objetoConstruible; }
    public void setGameObject(GameObject go) { objetoConstruible = go; Debug.Log("Definido objeto como " + go); }

}


