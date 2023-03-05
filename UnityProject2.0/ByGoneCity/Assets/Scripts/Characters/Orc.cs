using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Orc : Movement
{
    [SerializeField] private Transform[] shieldDirections = new Transform[5];
    [SerializeField] private Transform shield;
    [SerializeField] private Collider2D pushCollider;
    private bool drawnShield;
    private float cooldown;
    private Vector2 direction;
    private new void Awake()
    {
        base.Awake();
    }
    private new void Start()
    {
        base.Start();
    }
    public void OnAim(InputAction.CallbackContext input)
    {
        direction = input.ReadValue<Vector2>();
    }
    public void OnPush(InputAction.CallbackContext input)
    {
        if (input.performed)
            Push();
    }
    public void OnDraw(InputAction.CallbackContext input)
    {
        if (input.performed)
            DrawShield();
    }
    private new void Update()
    {
        base.Update();
        RotateShield();
    }
    private void Push()
    {
        if(drawnShield && Time.time >= cooldown)
        {
            if (EnoughStamina(Data.pushCost))
            {
                SpendStamina(Data.pushCost);
                cooldown = Time.time + 1f / Data.pushRate;
                List<Collider2D> colliders = new();
                ContactFilter2D filter = new ContactFilter2D().NoFilter();
                filter.SetLayerMask(Data.pushLayers);
                Physics2D.OverlapCollider(pushCollider, filter, colliders);
                if(colliders.Count == 0)
                {
                    //ejecutar audio "cast"
                    return;
                }
                foreach (Collider2D collider2D in colliders)
                {
                    if (collider2D.gameObject.CompareTag("Lever"))
                    {
                        //codigo activar lever
                        //audio de empuje secundario
                        return;
                    }
                    Vector2 direction = (Vector2)collider2D.transform.position - (Vector2)transform.position;
                    direction = direction.normalized;
                    if (collider2D.gameObject.CompareTag("Player"))
                    {
                        Debug.Log("se empujo al jugador");
                        //inmovilizar gnomo
                    }
                    collider2D.GetComponent<Rigidbody2D>().AddForce(direction * Data.pushKnockback, ForceMode2D.Impulse);
                    //audio de empuje
                }
            }
            else
            {
                Debug.Log("No hay stamina");
                //audio sin stamina
            }
        }
    }
    private void RotateShield()
    {
        if (direction == new Vector2(0f, 0f))
            return;
        Vector2 newPosition = shieldDirections[4].position;
        Quaternion newRotation = shieldDirections[4].rotation;
        if (!drawnShield)
        {
            Debug.Log("no esta el escudo");
            return;
        }
        if (direction.x > 0.1f)
        {
            if (direction.y > 0.1f)
            {
                newPosition = shieldDirections[3].position;
                newRotation = shieldDirections[3].rotation;
                if (_moveInput.x == 0)
                    Animator.SetInteger("AimState", 3);
                else if (_moveInput.x > 0.1)
                    Animator.SetInteger("AimState", 3);
                else
                    Animator.SetInteger("AimState", 1);
                //apunta en diagonal derecha caminando a la derecha
            }
            else
            {
                newPosition = shieldDirections[4].position;
                newRotation = shieldDirections[4].rotation;
                if (_moveInput.x == 0)
                    Animator.SetInteger("AimState", 4);
                else if (_moveInput.x > 0.1)
                    Animator.SetInteger("AimState", 4);
                else
                    Animator.SetInteger("AimState", 0);
                //apunta a la derecha
            }
        }
        else if (direction.x < -0.1f)
        {
            if (direction.y > 0.1f)
            {
                newPosition = shieldDirections[1].position;
                newRotation = shieldDirections[1].rotation;
                if (_moveInput.x == 0)
                    Animator.SetInteger("AimState", 1);
                else if (_moveInput.x > 0.1)
                    Animator.SetInteger("AimState", 1);
                else
                    Animator.SetInteger("AimState", 3);
                //apunta en diagonal izquierda
            }
            else
            {
                newPosition = shieldDirections[0].position;
                newRotation = shieldDirections[0].rotation;
                if (_moveInput.x == 0)
                    Animator.SetInteger("AimState", 0);
                else if (_moveInput.x > 0.1)
                    Animator.SetInteger("AimState", 0);
                else
                    Animator.SetInteger("AimState", 4);
                //apunta a la izquierda
            }
        }
        else if (direction.y > 0.1f)
        {
            newPosition = shieldDirections[2].position;
            newRotation = shieldDirections[2].rotation;
            Animator.SetInteger("AimState", 2);
            //apunta hacia arriba
        }
        if ((Vector2)shield.position == newPosition)
        {
            Debug.Log("La posicion es la misma");
            return;
        }
        shield.SetPositionAndRotation(newPosition, newRotation);
    }
    private void DrawShield()
    {
        drawnShield = !drawnShield;
        shield.gameObject.SetActive(drawnShield);
        isSlowed = drawnShield;
        //Animator.SetBool("Shield", drawnShield);
    }
}
