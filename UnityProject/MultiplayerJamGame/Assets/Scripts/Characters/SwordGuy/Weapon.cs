using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    public GameObject bulletPref;
    public Transform shootingPoint;
    public Image ammoBar;
    public int damage;
    public int maxAmmo = 50;
    public int initialAmmo = 10;
    private int actualAmmo = 0;
    public float fireRate = 4f;
    private float fireCooldown = 0f;
    public bool isAvaiable = false;
    public bool isPicked = false;

    private void Awake()
    {
        isAvaiable = true;
        shootingPoint.gameObject.SetActive(isAvaiable);
        actualAmmo = initialAmmo;
        AmmoUpdate();
    }
    void Update()
    {
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
                if (Random.Range(0, 1) == 1)
                    GetComponent<AudioManager>().Play("Shoot1");
                else
                    GetComponent<AudioManager>().Play("Shoot2");
            }
            else
            {
                Debug.Log("No ammo");
                GetComponent<AudioManager>().Play("NoAmmo");
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
            GetComponent<AudioManager>().Play("PickAmmo");
            return true;
        }
        return false;
    }
    private void AmmoUpdate()
    {
        ammoBar.fillAmount = (float)actualAmmo / maxAmmo;
    }
}
