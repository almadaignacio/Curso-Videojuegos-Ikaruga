using UnityEngine;

public class GuiaTransforms : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // ---------------------------------------------------------
        // 1. ESPACIO GLOBAL (WORLD) - Respecto al centro del mundo (0,0,0)
        // ---------------------------------------------------------

        // Posición exacta en todo el mapa
        Vector3 posGlobal = transform.position;
        Debug.Log("transform.position (Global): " + posGlobal);

        // Rotación exacta en el mundo (en formato Quaternion)
        Quaternion rotGlobal = transform.rotation;
        Debug.Log("transform.rotation (Global Quaternion): " + rotGlobal);

        // Rotación exacta en el mundo expresada en grados (0 a 360)
        Vector3 gradosGlobales = transform.eulerAngles;
        Debug.Log("transform.eulerAngles (Global en Grados): " + gradosGlobales);


        // ---------------------------------------------------------
        // 2. ESPACIO LOCAL - Respecto al objeto Padre
        // ---------------------------------------------------------

        // Posición relativa al objeto padre (si no tiene padre, es igual a position)
        Vector3 posLocal = transform.localPosition;
        Debug.Log("transform.localPosition (Respecto al Padre): " + posLocal);

        // Rotación relativa al objeto padre (en formato Quaternion)
        Quaternion rotLocal = transform.localRotation;
        Debug.Log("transform.localRotation (Respecto al Padre Quaternion): " + rotLocal);

        // Rotación relativa al objeto padre expresada en grados (0 a 360)
        Vector3 gradosLocales = transform.localEulerAngles;
        Debug.Log("transform.localEulerAngles (Respecto al Padre en Grados): " + gradosLocales);


        // ---------------------------------------------------------
        // 3. ESCALA (TAMAÑO)
        // ---------------------------------------------------------

        // Tamaño del objeto respecto a su padre (1,1,1 es el tamaño original)
        Vector3 escalaLocal = transform.localScale;
        Debug.Log("transform.localScale (Tamaño Local): " + escalaLocal);

        // Tamaño real en el mundo tras multiplicarse por el tamaño de sus padres (Solo lectura)
        Vector3 escalaGlobal = transform.lossyScale;
        Debug.Log("transform.lossyScale (Tamaño Global Real): " + escalaGlobal);


        // ---------------------------------------------------------
        // 4. DIRECCIONES LOCALES EN EL MUNDO (Vectores unitarios)
        // ---------------------------------------------------------

        // Eje Z (Azul): Hacia dónde mira el frente del objeto
        Debug.Log("transform.forward (Frente / Eje Azul): " + transform.forward);

        // Eje X (Rojo): Hacia dónde apunta la derecha del objeto
        Debug.Log("transform.right (Derecha / Eje Rojo): " + transform.right);

        // Eje Y (Verde): Hacia dónde apunta la parte superior del objeto
        Debug.Log("transform.up (Arriba / Eje Verde): " + transform.up);

        Debug.Log("=== FIN DE LA GUÍA ===");

        transform.rotation = Quaternion.Euler(0, 90, 0);
    }
}
