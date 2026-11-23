using UnityEngine;
using UnityEngine.InputSystem;

public class CambioCamara : MonoBehaviour
{
    // Referencias a las cámaras que asignaremos desde el Inspector
    public GameObject camara1;
    public GameObject camara2;

    void Start()
    {
        // Al iniciar, aseguramos que la cámara 1 esté encendida y la 2 apagada
        camara1.SetActive(true);
        camara2.SetActive(false);
    }

    void Update()
    {
        // Detecta el instante en que se presiona la tecla C
        if (Keyboard.current.lKey.wasPressedThisFrame)
        {
            // Invierte el estado actual de ambas cámaras
            bool estadoActual = camara1.activeSelf;

            camara1.SetActive(!estadoActual);
            camara2.SetActive(estadoActual);
        }
    }
}
