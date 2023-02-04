using UnityEngine;
using UnityEngine.InputSystem;

public class ControlJugador : MonoBehaviour
{
    
    private Animator anim;
    [SerializeField] private Rigidbody2D rb;

    [SerializeField]
    private float velocidad = 5.0f;

    [SerializeField]
    private float smoothing = 0.2f;

    private Vector3 playerVelocity;
    
    private Vector2 movimientoInput = Vector2.zero;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void EnMovimiento(InputAction.CallbackContext context)
    {
        Debug.Log("Moviendo");
        movimientoInput = context.ReadValue<Vector2>();

    }

    void Update()
    {
        Vector3 move = new Vector2(movimientoInput.x, movimientoInput.y);

        move = move.normalized;
        //rb.MovePosition(rb.transform.position + move * velocidad * Time.deltaTime);

        rb.velocity = Vector2.Lerp(rb.velocity, move * velocidad, smoothing);
    }
}
