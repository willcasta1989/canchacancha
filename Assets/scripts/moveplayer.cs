using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveplayer : MonoBehaviour
{
    public float velocidad = 5f;

    // Fuerza de salto del jugador
    public float fuerzaSalto = 10f;

    // Referencia al componente Rigidbody del jugador
    private Rigidbody rb;
    private Animator animator;

    void Start()
    {
        // Obtener el componente Rigidbody del jugador
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Obtener la entrada del teclado para el movimiento horizontal y vertical
        float movimientoHorizontal = Input.GetAxis("Horizontal");
        float movimientoVertical = Input.GetAxis("Vertical");

        // Calcular la velocidad de movimiento del jugador
        Vector3 movimiento = new Vector3(movimientoHorizontal, 0f, movimientoVertical) * velocidad;

        // Si el jugador salta, permitir el movimiento en el eje Y
        if (Input.GetKeyDown(KeyCode.Space))
        {
            movimiento.y = fuerzaSalto;
        }

        // Aplicar el movimiento al Rigidbody del jugador
        rb.velocity = movimiento;

        if (movimientoVertical > 0)
        {
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }

    }
    void OnCollisionEnter(Collision collision)
    {
        // Obtener la masa del objeto con el que colisiona el jugador
        float masaObjeto = collision.rigidbody ? collision.rigidbody.mass : Mathf.Infinity;

        // Verificar si la masa del objeto es mayor o igual a la del jugador
        if (masaObjeto >= rb.mass)
        {
            // Calcular la fuerza de rebote reducida a la mitad
            Vector3 direccionRebote = Vector3.Reflect(rb.velocity.normalized, collision.contacts[0].normal);
            float fuerzaRebote = rb.velocity.magnitude * 0.5f;

            // Aplicar la fuerza de rebote al jugador en dirección contraria
            rb.velocity = direccionRebote * fuerzaRebote;
        }
    }
}
