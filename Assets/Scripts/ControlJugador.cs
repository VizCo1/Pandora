using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class ControlJugador : MonoBehaviour
{
    
    private Animator anim;
    private CharacterController control;

    [SerializeField]
    private float velocidad = 5.0f;

    private Vector3 playerVelocity;
    
    private Vector2 movimientoInput = Vector2.zero;

    private void Start()
    {
        anim = GetComponent<Animator>();
        control = gameObject.GetComponent<CharacterController>();
    }

    public void EnMovimiento(InputAction.CallbackContext context)
    {
        Debug.Log("Moviendo");
        movimientoInput = context.ReadValue<Vector2>();

    }

    void Update()
    {

        Vector3 move = new Vector3(movimientoInput.x, 0, movimientoInput.y);
        control.Move(move * Time.deltaTime* velocidad);
        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }

        control.Move(playerVelocity * Time.deltaTime);

    }
}
