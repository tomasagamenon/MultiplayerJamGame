using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Pool;

public abstract class Gun : Movement
{
    [SerializeField] private Transform gunPoint;
    [SerializeField] private Transform[] gunDirections = new Transform[5];
    [SerializeField] private GameObject bulletPrefab;
    private Vector2 direction;
    private bool drawnWeapon = false;
    private float cooldown;
    private float actualAmmo;
    private ObjectPool<Bullet> pool;

    private new void Awake()
    {
        Debug.Log("Awake gun");
        base.Awake();
        pool = new ObjectPool<Bullet>(CreateBullet, OnTakeBulletFromPool, OnReturnBulletToPool);
    }
    private new void Start()
    {
        Debug.Log("Start gun");
        base.Start();
        actualAmmo = Data.initialAmmo;
        AmmoUpdate();
    }
    public void OnAim(InputAction.CallbackContext input)
    {
        direction = input.ReadValue<Vector2>();
        //Debug.Log("Aim value= " + direction);
    }
    public void OnFire(InputAction.CallbackContext input)
    {
        if (input.performed)
            Shoot();
    }
    public void OnGunDraw(InputAction.CallbackContext input)
    {
        if (input.performed)
            DrawGun();
    }

    private void DrawGun()
    {
        drawnWeapon = !drawnWeapon;
        Animator.SetBool("Gun", drawnWeapon);
    }
    private void Shoot()
    {
        if (drawnWeapon && Time.time >= cooldown)
        {
            if (actualAmmo > 0)
            {
                //Instantiate<GameObject>(bulletPrefab, gunPoint.position, gunPoint.rotation);
                var bullet = pool.Get();
                bullet.transform.SetPositionAndRotation(gunPoint.position, gunPoint.rotation);
                bullet.SetVelocity();
                actualAmmo--;
                AmmoUpdate();
                cooldown = Time.time + 1f / Data.fireRate;
                if (Random.Range(0, 1) == 1)
                    audioManager.Play("Shoot1");
                else
                    audioManager.Play("Shoot2");
            }
            else
            {
                Debug.Log("No ammo");
                audioManager.Play("OutOfAmmo");
                //audio sin municion
            }
        }
    }

    protected override void Update()
    {
        base.Update();
        //Debug.Log("Update Gun");
        RotateGun();
    }
    private void RotateGun()
    {
        if (direction == new Vector2(0f, 0f))
            return;
        Vector2 newPosition = gunDirections[4].position;
        Quaternion newRotation = gunDirections[4].rotation;
        if (!drawnWeapon)
        {
            Debug.Log("no esta el arma");
            return;
        }
        if (direction.x > 0.1f)
        {
            if (direction.y > 0.1f)
            {
                newPosition = gunDirections[3].position;
                newRotation = gunDirections[3].rotation;
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
                newPosition = gunDirections[4].position;
                newRotation = gunDirections[4].rotation;
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
                newPosition = gunDirections[1].position;
                newRotation = gunDirections[1].rotation;
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
                newPosition = gunDirections[0].position;
                newRotation = gunDirections[0].rotation;
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
            newPosition = gunDirections[2].position;
            newRotation = gunDirections[2].rotation;
            Animator.SetInteger("AimState", 2);
            //apunta hacia arriba
        }
        if ((Vector2)gunPoint.position == newPosition)
        {
            Debug.Log("La posicion es la misma");
            return;
        }
        gunPoint.SetPositionAndRotation(newPosition, newRotation);
    }
    public bool AddAmmo(int ammo)
    {
        if (actualAmmo != Data.totalAmmo)
        {
            actualAmmo = Mathf.Clamp(ammo + actualAmmo, 0, Data.totalAmmo);
            AmmoUpdate();
            Debug.Log("municion= " + actualAmmo);
            audioManager.Play("PickUpAmmo");
            //audio de agarrar municion
            return true;
        }
        Debug.Log("municion llena");
        return false;
    }
    private void AmmoUpdate()
    {
        //lenar barra de municion
    }
    private Bullet CreateBullet()
    {
        Debug.Log("createbullet");
        var bullet = Instantiate(bulletPrefab).GetComponent<Bullet>();
        bullet.SetPool(pool);
        bullet.damage = Data.bulletDamage;
        bullet.speed = Data.bulletSpeed;
        bullet.lifeTime = Data.bulletDuration;
        bullet.ResetLifeLeft();
        return bullet;
    }
    private void OnTakeBulletFromPool(Bullet bullet)
    {
        Debug.Log("ontakebulletfrompool");
        bullet.gameObject.SetActive(true);
        //bullet.lifeTime = Data.bulletDuration;
        bullet.ResetLifeLeft();
    }
    private void OnReturnBulletToPool(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
    }
}
