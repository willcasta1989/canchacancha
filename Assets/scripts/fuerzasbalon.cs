using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fuerzasbalon : MonoBehaviour
{
    // Start is called before the first frame update
    // Velocidad inicial del balón al ser pateado
     public float velocidadInicial = 10f;

    // Factor de aumento de la fuerza de rebote
    public float factorAumentoFuerza = 2f;

    // Referencia al componente Rigidbody del balón
    private Rigidbody rb;

    void Start()
    {
        // Obtener el componente Rigidbody del balón
        rb = GetComponent<Rigidbody>();

        // Aplicar una fuerza inicial al balón para simular una patada
        Vector3 direccionInicial = new Vector3(1f, 0.5f, 0f).normalized; // Dirección inicial de la patada
        rb.AddForce(direccionInicial * velocidadInicial, ForceMode.Impulse);
    }

    void OnCollisionEnter(Collision collision)
    {
        // Verificar si el balón ha colisionado con otro objeto
        if (collision.gameObject.tag == "Jugador") // Cambia "Jugador" al tag del objeto con el que el balón colisiona
        {
            // Calcular la fuerza de rebote aumentada
            Vector3 direccionRebote = Vector3.Reflect(rb.velocity.normalized, collision.contacts[0].normal);
            float fuerzaRebote = rb.velocity.magnitude * factorAumentoFuerza;

            // Aplicar la fuerza de rebote aumentada al balón
            rb.velocity = direccionRebote * fuerzaRebote;
        }
    }
}


