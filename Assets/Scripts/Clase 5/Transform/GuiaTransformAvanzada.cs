using UnityEngine;

public class GuiaTransformAvanzada : MonoBehaviour
{
    public Transform objetivoParaMirar;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // ---------------------------------------------------------
        // 1. JERARQUÍA Y RELACIONES
        // ---------------------------------------------------------

        // Padre y Raíz 
        Debug.Log("Padre directo: " + (transform.parent != null ? transform.parent.name : "Ninguno"));
        Debug.Log("Objeto Raíz (Root): " + transform.root.name);

        // Conteo de hijos y bandera de cambio
        Debug.Log("Cantidad de hijos directos: " + transform.childCount);
        Debug.Log("¿El transform cambió recientemente?: " + transform.hasChanged);
        transform.hasChanged = false; // Siempre se reinicia a false manualmente

        // ---------------------------------------------------------
        // 2. MATRICES Y ESCALAS CALCULADAS
        // ---------------------------------------------------------

        Debug.Log("Escala global real (lossyScale): " + transform.lossyScale);
        Debug.Log("Matriz Local a Global:\n" + transform.localToWorldMatrix);
        Debug.Log("Matriz Global a Local:\n" + transform.worldToLocalMatrix);

        // ---------------------------------------------------------
        // 3. BÚSQUEDA E HIJOS
        // ---------------------------------------------------------

        // Buscar hijo por índice (si tiene al menos 1 hijo)
        if (transform.childCount > 0)
        {
            Transform primerHijo = transform.GetChild(0);
            Debug.Log("Primer hijo por índice (0): " + primerHijo.name);

            // Buscar hijo por nombre
            Transform hijoBuscado = transform.Find(primerHijo.name);
            Debug.Log("Hijo encontrado por nombre: " + hijoBuscado.name);
        }

        // Ejemplo de cómo desvincular hijos (comentado para no romper tu jerarquía al dar Play)
        // transform.DetachChildren();

        // ---------------------------------------------------------
        // 4. CONVERSIÓN DE ESPACIOS
        // ---------------------------------------------------------

        // Convierte un punto 2 metros delante del objeto (local) a su coordenada real en el mapa (global)
        Vector3 puntoLocal = new Vector3(0, 0, 2);
        Vector3 puntoEnElMundo = transform.TransformPoint(puntoLocal);
        Debug.Log("2m delante de mí en el mundo es: " + puntoEnElMundo);

        // Proceso inverso: convierte una coordenada del mundo a local respecto a este objeto
        Vector3 deVueltaALocal = transform.InverseTransformPoint(puntoEnElMundo);
        Debug.Log("Ese punto respecto a mí es: " + deVueltaALocal);

        // Conversión de direcciones (solo importa hacia dónde mira el objeto, no su posición ni escala)
        Vector3 dirGlobal = transform.TransformDirection(Vector3.forward);
        Vector3 dirLocal = transform.InverseTransformDirection(dirGlobal);
        Debug.Log("Dirección frente en global: " + dirGlobal + " | En local: " + dirLocal);
    }

    void Update()
    {
        // ---------------------------------------------------------
        // 5. MOVIMIENTO Y ROTACIÓN CONTINUA
        // ---------------------------------------------------------
        // === 5. MOVIMIENTO Y ROTACIÓN CONTINUA ===

        // Mueve el objeto suavemente hacia su propio frente
        transform.Translate(Vector3.forward * 1f * Time.deltaTime);

        // Rota el objeto 30 grados por segundo sobre su eje Y
        transform.Rotate(new Vector3(0, 30f * Time.deltaTime, 0));

        // Si asignaste un objetivo en el Inspector, lo mirará constantemente
        if (objetivoParaMirar != null)
        {
            transform.LookAt(objetivoParaMirar);
        }
    }
}
