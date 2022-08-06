using UnityEngine;
using TMPro;

public class Weapon : MonoBehaviour
{
    public GameObject bulletPref;
    public Transform shootingPoint;
    public TextMeshProUGUI ammoText;
    public int damage;
    public int maxAmmo = 50;
    private int actualAmmo = 0;
    public float fireRate = 4f;
    private float fireCooldown = 0f;
    public bool isAvaiable = false;
    public bool isPicked = false;

    private void Awake()
    {
        shootingPoint.gameObject.SetActive(isAvaiable);
        AmmoUpdate();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
            ToggleWeapon();
        //if(Time.time >= fireCooldown && isAvaiable)
        //{
        //    if (Input.GetKeyDown(KeyCode.F))
        //    {
        //        Fire();
        //        fireCooldown = Time.time + 1f / fireRate;
        //    }
        //}
    }
    public void Fire()
    {
        if(Time.time >= fireCooldown && isAvaiable)
        {
            if(actualAmmo > 0)
            {
                Debug.Log("Disparaste");
                Instantiate(bulletPref, shootingPoint.position, shootingPoint.rotation);
                actualAmmo--;
                AmmoUpdate();
                fireCooldown = Time.time + 1f / fireRate;
                //Sound
            }
            else
            {
                Debug.Log("No ammo");
                //no ammo!
            }
        }
    }
    /*
    public void ReloadClip()
    {
        Debug.Log("Empezaste recarga");
        isReloading = true;
        actualAmmo += actualClip;
        actualClip = 0;
        AmmoUpdate();

        //execute animation
        //sound
        FinishReloading();
    }
    public void FinishReloading()
    {
        Debug.Log("Terminaste recarga");
        actualClip = Mathf.Clamp(actualAmmo, 0, maxClip);
        actualAmmo -= actualClip;
        AmmoUpdate();
        isReloading = false;
        //maybe a sound
    }*/
    public void ToggleWeapon()
    {
        isAvaiable = !isAvaiable;
        shootingPoint.gameObject.SetActive(isAvaiable);
        //sound?
    }
    public bool AddAmmo(int ammo)
    {
        if((actualAmmo != maxAmmo))
        {
            actualAmmo = Mathf.Clamp(ammo + actualAmmo, 0, maxAmmo);
            AmmoUpdate();
            return true;
        }
        return false;
    }
    private void AmmoUpdate()
    {
        ammoText.text = actualAmmo.ToString();
    }
}
