using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastShoot : MonoBehaviour
{
    // Apuntar
    public Vector3 Aim;

    // Variables de arma
    public float shootCooldown = 0.25f;
    private float shootTimer;
    public int Ammo;
    public int Magazines = 4; // Cantidad de cartuchos, sujeto a balanceo
    public int MagSize = 25; // Cartucho para una PPD-34

    // Variables de granadas y explosivos

    public float expCooldown = 3f;
    private float expTimer;
    public int Grenades;
    public int GCarry = 4; // valor arbitrario, sujeto a balanceo
    public GameObject grenadePrefab;
    public int throwForce = 5;

    void Start () 
    {
        shootTimer = 0;
        expTimer = 0;
        Ammo = MagSize;
        Grenades = GCarry;
    }


    void Update () 
    {
        Aim = Input.mousePosition;
        // Cool Down de disparo
        if(shootTimer > 0){
            shootTimer -= Time.deltaTime;
        }
        else{
            shootTimer = 0;
        }

        // Cool Down de granada
        if(expTimer > 0){
            expTimer -= Time.deltaTime;
        }
        else{
            expTimer = 0;
        }



        // Disparar
        if (Input.GetButtonDown("Fire1") && shootTimer == 0 && Ammo > 0){
            Shoot(Aim);
            shootTimer = shootCooldown;
            Ammo--;
        }
        
        // Recargar 
        if(Input.GetKeyDown(KeyCode.R) && Magazines > 0){
            Ammo = MagSize;
            Magazines--;
        }

        // Granadas-
        if (Input.GetButtonDown("Fire2") && Grenades > 0){
            Grenade(Aim);
            expTimer = expCooldown;
            Grenades--;
        }
    }

    void Shoot(Vector3 apunta)
    {
        Ray fire = Camera.main.ScreenPointToRay(apunta);
        RaycastHit hit;

        if(Physics.Raycast(fire, out hit)){
            Debug.Log("Bang " + hit.transform.name);
        }
    }

        void Grenade(Vector3 apunta)
    {
        // Instantiate grenade prefab at current position with appropriate rotation
        GameObject grenadeInstance = Instantiate(grenadePrefab, transform.position, Quaternion.identity);

        // Calculate direction towards the target
        Vector3 direction = (apunta - transform.position).normalized;

        // Access grenade's Rigidbody component and apply force in the calculated direction
        Rigidbody grenadeRigidbody = grenadeInstance.GetComponent<Rigidbody>();
        grenadeRigidbody.AddForce(direction * throwForce, ForceMode.Impulse);

    }
}