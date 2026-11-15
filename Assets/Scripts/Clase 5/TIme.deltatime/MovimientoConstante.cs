using UnityEngine;

public class MovimientoConstante : MonoBehaviour
{
    // Velocidad en unidades (metros) por segundo
    public float velocidad = 5.0f;
    
    void Update()
    {
        // Movemos el objeto hacia adelante (eje Z)
        // Multiplicar por Time.deltaTime convierte el movimiento de "por fotograma" a "por segundo"
        transform.Translate(Vector3.forward * velocidad * Time.deltaTime);
    }
}

