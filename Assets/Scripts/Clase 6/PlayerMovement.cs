using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Referencias")]
    public Transform camaraTransform;

    [Header("Configuración de Movimiento")]
    public float velocidadCaminar = 6.0f;
    public float velocidadSprint = 10.0f;
    public float velocidadAgachado = 3.0f;
    public float velocidadRotacion = 10.0f;

    [Header("Configuración de Agacharse")]
    public float alturaDePie = 2.0f;
    public float alturaAgachado = 1.0f;
    public float velocidadCambioAltura = 10.0f;

    [Header("Configuración de Salto y Gravedad")]
    public float alturaSalto = 1.5f;
    public float gravedad = -19.62f;

    private CharacterController controller;
    private Vector3 velocidadVertical;
    private bool estaEnElSuelo;
    private bool estaAgachado = false;
    private Vector3 centroDePie;
    private float desplazamientoCentroAgachado;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        // Guardamos la configuración exacta que tienes en el Inspector
        alturaDePie = controller.height;
        centroDePie = controller.center;

        // Calculamos cuánto debe bajar el centro para mantener los pies pegados al suelo
        float diferenciaAltura = alturaDePie - alturaAgachado;
        desplazamientoCentroAgachado = centroDePie.y - (diferenciaAltura / 2f);

        if (camaraTransform == null && Camera.main != null)
        {
            camaraTransform = Camera.main.transform;
        }
    }

    void Update()
    {
        VerificarSuelo();
        Vector2 inputTeclado = LeerInputMovimiento();
        ManejarAgacharse();
        MoverYRotar(inputTeclado);
        ManejarSalto();
        AplicarGravedad();
    }

    private void VerificarSuelo()
    {
        estaEnElSuelo = controller.isGrounded;
        if (estaEnElSuelo && velocidadVertical.y < 0)
        {
            velocidadVertical.y = -2f;
        }
    }

    private Vector2 LeerInputMovimiento()
    {
        Vector2 input = Vector2.zero;
        if (Keyboard.current != null)
        {
            if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed) input.y += 1;
            if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed) input.y -= 1;
            if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed) input.x += 1;
            if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed) input.x -= 1;
        }
        return input;
    }

    private void ManejarAgacharse()
    {
        // Alternar estado al presionar C
        if (Keyboard.current != null && Keyboard.current.cKey.wasPressedThisFrame)
        {
            if (estaAgachado)
            {
                if (PuedeLevantarse())
                {
                    estaAgachado = false;
                }
            }
            else
            {
                estaAgachado = true;
            }
        }

        // Interpolamos tanto la altura como el centro hacia sus valores objetivo
        float alturaObjetivo = estaAgachado ? alturaAgachado : alturaDePie;
        float centroYObjetivo = estaAgachado ? desplazamientoCentroAgachado : centroDePie.y;

        controller.height = Mathf.Lerp(controller.height, alturaObjetivo, velocidadCambioAltura * Time.deltaTime);

        Vector3 nuevoCentro = controller.center;
        nuevoCentro.y = Mathf.Lerp(controller.center.y, centroYObjetivo, velocidadCambioAltura * Time.deltaTime);
        controller.center = nuevoCentro;
    }

    private bool PuedeLevantarse()
    {
        // Lanza un rayo hacia arriba desde la base del jugador para comprobar si hay espacio
        float distanciaAComprobar = alturaDePie - controller.height;
        Vector3 origen = transform.position + Vector3.up * controller.height;
        return !Physics.Raycast(origen, Vector3.up, distanciaAComprobar + 0.1f);
    }

    private float ObtenerVelocidadActual()
    {
        if (estaAgachado)
        {
            return velocidadAgachado;
        }

        bool estaSprintando = Keyboard.current != null && Keyboard.current.leftShiftKey.isPressed;
        if (estaSprintando)
        {
            return velocidadSprint;
        }

        return velocidadCaminar;
    }

    private void MoverYRotar(Vector2 inputTeclado)
    {
        Vector3 adelanteCamara = camaraTransform.forward;
        Vector3 derechaCamara = camaraTransform.right;

        adelanteCamara.y = 0f;
        derechaCamara.y = 0f;
        adelanteCamara.Normalize();
        derechaCamara.Normalize();

        Vector3 direccion = (adelanteCamara * inputTeclado.y + derechaCamara * inputTeclado.x).normalized;

        if (direccion.magnitude >= 0.1f)
        {
            Quaternion rotacionObjetivo = Quaternion.LookRotation(direccion);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotacionObjetivo, velocidadRotacion * Time.deltaTime);

            float velocidadActual = ObtenerVelocidadActual();
            controller.Move(direccion * velocidadActual * Time.deltaTime);
        }
    }

    private void ManejarSalto()
    {
        // Evita saltar si está agachado
        if (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame && estaEnElSuelo && !estaAgachado)
        {
            velocidadVertical.y = Mathf.Sqrt(alturaSalto * -2f * gravedad);
        }
    }

    private void AplicarGravedad()
    {
        velocidadVertical.y += gravedad * Time.deltaTime;
        controller.Move(velocidadVertical * Time.deltaTime);
    }
}
