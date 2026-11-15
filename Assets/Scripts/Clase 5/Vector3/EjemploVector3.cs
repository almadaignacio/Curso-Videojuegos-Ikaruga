using UnityEngine;
using UnityEngine.InputSystem;

public class EjemploVector3 : MonoBehaviour
{
    public float velocidad = 5f;

    public float velocidadRotacion = 10f; // Controla qué tan rápido gira el personaje


    private void Start()
    {
        Normalizado();
    }

    void Update()
    {
        // Verificamos que haya un teclado conectado para evitar errores
        if (Keyboard.current == null) return;

        // 2. Creamos un Vector3 vacío (0, 0, 0) donde guardaremos la dirección
        Vector3 direccion = Vector3.zero;

        // 3. Leemos las teclas físicas directamente y sumamos direcciones
        if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed)
        {
            direccion += Vector3.forward; // Suma (0, 0, 1)
        }
        if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed)
        {
            direccion += Vector3.back;    // Suma (0, 0, -1)
        }
        if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
        {
            direccion += Vector3.left;    // Suma (-1, 0, 0)
        }
        if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed)
        {
            direccion += Vector3.right;   // Suma (1, 0, 0)
        }

        // 4. Normalizamos para que en diagonal no corra más rápido
        direccion = direccion.normalized;

        // 5. Aplicamos el movimiento final
        transform.position += direccion * velocidad * Time.deltaTime;

        // 6. Aplicamos la rotación hacia la dirección de movimiento
        if (direccion != Vector3.zero)
        {
            // Calculamos la rotación deseada mirando hacia la dirección
            Quaternion rotacionObjetivo = Quaternion.LookRotation(direccion);

            // Interpolamos suavemente desde la rotación actual hasta la objetivo
            transform.rotation = Quaternion.Slerp(transform.rotation, rotacionObjetivo, velocidadRotacion * Time.deltaTime);
        }
    }

    private void Normalizado()
    {
        //Armamos un vector3
        Vector3 vector = new Vector3(10, 3, 5);
        //Si llamamos a vector, nos devuelve 10,3,5
        Debug.Log(vector + " es vector");

        //Cambiemos su valor para normalizarlo
        vector = vector.normalized;
        //Ahora su magnitud es 1, pero su dirección se mantiene
        Debug.Log(vector + " es vector normalizado");
    }
}
