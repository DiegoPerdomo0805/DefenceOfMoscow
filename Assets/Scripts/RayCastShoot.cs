using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastShoot : MonoBehaviour
{
    // Health
    private float maxHealth = 20f;
    public float Health;


    // Apuntar
    public Vector3 Aim;

    // Variables de arma
    public float shootCooldown = 0.25f;
    private float shootTimer;
    public int Ammo;
    public int Magazines; // Cantidad de cartuchos, sujeto a balanceo
    public int MagSize = 25; // Cartucho para una PPD-34
    public int maxMag = 4; // Máxima cantidad de cartuchos 

    // Variables de granadas y explosivos

    public float expCooldown = 3f;
    private float expTimer;
    public int Grenades;
    public int GCarry = 4; // valor arbitrario, sujeto a balanceo
    public GameObject grenadePrefab;
    public int throwForce = 5;

    // TextMeshes
    public TMPro.TextMeshProUGUI municiones;
    public TMPro.TextMeshProUGUI cartuchos;
    public TMPro.TextMeshProUGUI granadas;

    void Start () 
    {
        shootTimer = 0;
        expTimer = 0;
        Ammo = MagSize;
        Grenades = GCarry;
        Magazines = maxMag;
        Health = maxHealth;

        municiones.text = "Balas: " + Ammo;
        cartuchos.text = "Cartuchos: " + Magazines;
        granadas.text = "Granadas: " + Grenades;
    }

    public Transform RespawnPoint;

    void Respawn(){
        Magazines = maxMag;
        Ammo = MagSize;
        Grenades = GCarry;
        Health = maxHealth;
        transform.position = RespawnPoint.position;
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
            municiones.text = "Balas: " + Ammo;
        }
        
        // Recargar 
        if(Input.GetKeyDown(KeyCode.R) && Magazines > 0){
            Ammo = MagSize;
            Magazines--;
            municiones.text = "Balas: " + Ammo;
            cartuchos.text = "Cartuchos: " + Magazines;
        }

        // Granadas-
        if (Input.GetButtonDown("Fire2") && Grenades > 0){
            Grenade(Aim);
            expTimer = expCooldown;
            Grenades--;
            granadas.text = "Granadas: " + Grenades;
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