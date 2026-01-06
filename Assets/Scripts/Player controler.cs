using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playercontroler : MonoBehaviour
{
    public float velocidad = 5f;

    public float jumpForce = 10f;
    public float longitudRaycast = 0.1f;
    public LayerMask capaSuelo; 
    private bool estaEnSuelo;
    private Rigidbody2D rb;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        float velocidadX = Input.GetAxis("Horizontal")*Time.deltaTime*velocidad;

        animator.SetFloat("movimiento", velocidadX*velocidad);
        if (velocidadX > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (velocidadX < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        Vector3 posicion = transform.position;

        transform.position = new Vector3(velocidadX + posicion.x, posicion.y, posicion.z);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, longitudRaycast, capaSuelo);
        estaEnSuelo= hit.collider != null;
        if (Input.GetKeyDown(KeyCode.Space) && estaEnSuelo)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
        animator.SetBool("estaEnSuelo", estaEnSuelo);
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y - longitudRaycast, transform.position.z));
    }
}
