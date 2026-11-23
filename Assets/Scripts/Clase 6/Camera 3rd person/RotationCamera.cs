using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

public class RotationCamera : MonoBehaviour
{
    private Transform cameraTarget;
    public CinemachineVirtualCamera virtualCamera;

    public float sensibilidad = 0.2f;
    public float velocidadCambioHombro = 5f;

    private float rotacionX = 0f;
    private float rotacionY = 0f;

    private Cinemachine3rdPersonFollow thirdPersonFollow;
    private float hombroObjetivoX;

    void Start()
    {
        cameraTarget = GetComponent<Transform>();
        if (virtualCamera != null)
        {
            thirdPersonFollow = virtualCamera.GetCinemachineComponent<Cinemachine3rdPersonFollow>();

            // Solo guarda el hombro si esa cámara realmente usa 3rd Person Follow
            if (thirdPersonFollow != null)
            {
                hombroObjetivoX = thirdPersonFollow.ShoulderOffset.x;
            }
        }
    }

    void LateUpdate()
    {
        RotarCamara();

        if (thirdPersonFollow != null)
        {
            DetectarCambioDeHombro();
            SuavizarCambioDeHombro();
        }
    }

    void RotarCamara()
    {
        if (Mouse.current == null) return;

        Vector2 deltaMouse = Mouse.current.delta.ReadValue();

        rotacionY += deltaMouse.x * sensibilidad;
        rotacionX -= deltaMouse.y * sensibilidad;
        rotacionX = Mathf.Clamp(rotacionX, -40f, 70f);

        cameraTarget.rotation = Quaternion.Euler(rotacionX, rotacionY, 0f);
    }

    void DetectarCambioDeHombro()
    {
        if (Keyboard.current != null && Keyboard.current.qKey.wasPressedThisFrame)
        {
            hombroObjetivoX = -hombroObjetivoX;
        }
    }

    void SuavizarCambioDeHombro()
    {
        Vector3 offsetActual = thirdPersonFollow.ShoulderOffset;
        offsetActual.x = Mathf.Lerp(offsetActual.x, hombroObjetivoX, Time.deltaTime * velocidadCambioHombro);
        thirdPersonFollow.ShoulderOffset = offsetActual;
    }
}
