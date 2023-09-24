using UnityEngine;

public class PlayerAnimationEvent : MonoBehaviour
{
    [SerializeField] private GameObject weaponLogic;
    public GameObject iceArrow;
    private Transform arrowStart;

    private void Start()
    {
        arrowStart = GameObject.Find("bowStart").transform;
    }
    void Shoot()
    {
        Instantiate(iceArrow, arrowStart.position, transform.rotation);
        iceArrow.transform.forward = transform.forward;
    }

    public void EnableWeapon()
    {
        weaponLogic.SetActive(true);
    }

    public void DisableWeapon()
    {
        weaponLogic.SetActive(false);
    }
}