using UnityEngine;
using UnityEngine.InputSystem;

public class MovimientoJugador : MonoBehaviour
{
    [Header("Configuración de Movimiento")]
    public float velocidad = 6.0f;
    public float velocidadRotacion = 10.0f;

    [Header("Configuración de Salto y Gravedad")]
    public float alturaSalto = 1.5f;
    public float gravedad = -19.62f;

    private UnityEngine.CharacterController controller;
    private Vector3 velocidadVertical;
    private bool estaEnElSuelo;

    void Start()
    {
        controller = GetComponent<UnityEngine.CharacterController>();
    }

    void Update()
    {
        // 1. Verificar suelo
        estaEnElSuelo = controller.isGrounded;
        if (estaEnElSuelo && velocidadVertical.y < 0)
        {
            velocidadVertical.y = -2f;
        }

        // 2. Leer teclado con el nuevo Input System
        Vector2 inputTeclado = Vector2.zero;
        if (Keyboard.current != null)
        {
            if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed) inputTeclado.y += 1;
            if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed) inputTeclado.y -= 1;
            if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed) inputTeclado.x += 1;
            if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed) inputTeclado.x -= 1;
        }

        Vector3 direccion = new Vector3(inputTeclado.x, 0f, inputTeclado.y).normalized;

        // 3. Mover y rotar
        if (direccion.magnitude >= 0.1f)
        {
            Quaternion rotacionObjetivo = Quaternion.LookRotation(direccion);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotacionObjetivo, velocidadRotacion * Time.deltaTime);
            controller.Move(direccion * velocidad * Time.deltaTime);
        }

        // 4. Salto con barra espaciadora
        if (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame && estaEnElSuelo)
        {
            velocidadVertical.y = Mathf.Sqrt(alturaSalto * -2f * gravedad);
        }

        // 5. Aplicar Gravedad
        velocidadVertical.y += gravedad * Time.deltaTime;
        controller.Move(velocidadVertical * Time.deltaTime);
    }
}
