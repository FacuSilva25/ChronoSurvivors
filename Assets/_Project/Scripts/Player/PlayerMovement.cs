using UnityEngine;
using UnityEngine.InputSystem; // Importamos el nuevo sistema

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Estadísticas de Movimiento")]
    public float moveSpeed = 5f;

    private PlayerStatController statController;
    private Rigidbody2D rb;
    private Vector2 movement;
    private InputAction moveAction;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        // 1. Creamos la acción de movimiento que leerá un Vector2 (X, Y)
        moveAction = new InputAction("Move", InputActionType.Value);

        // 2. Asignamos las teclas usando un Composite "2DVector"
        // El mode=2 asegura que responda de forma digital (como el viejo GetAxisRaw)
        moveAction.AddCompositeBinding("2DVector(mode=2)")
            .With("Up", "<Keyboard>/w")
            .With("Down", "<Keyboard>/s")
            .With("Left", "<Keyboard>/a")
            .With("Right", "<Keyboard>/d")
            .With("Up", "<Keyboard>/upArrow")
            .With("Down", "<Keyboard>/downArrow")
            .With("Left", "<Keyboard>/leftArrow")
            .With("Right", "<Keyboard>/rightArrow");

        // 3. Añadimos soporte para el Joystick analógico de los mandos o Android
        moveAction.AddBinding("<Gamepad>/leftStick");
    }

    void Start()
    {
        // Buscamos el componente automáticamente
        statController = GetComponent<PlayerStatController>();
    }

    void OnEnable()
    {
        // El nuevo sistema requiere activar explícitamente las acciones
        moveAction.Enable();
    }

    void OnDisable()
    {
        // Y desactivarlas cuando el objeto se destruye o apaga
        moveAction.Disable();
    }

    void Update()
    {
        // Leemos los valores del input en tiempo real
        movement = moveAction.ReadValue<Vector2>();

        // Normalizamos para no ir más rápido al moverse en diagonal
        if (movement.magnitude > 1f)
        {
            movement.Normalize();
        }
    }

    void FixedUpdate()
    {
        // Aplicamos el movimiento físico
        rb.MovePosition(rb.position + movement * (moveSpeed * statController.moveSpeedMultiplier) * Time.fixedDeltaTime);
    }
}