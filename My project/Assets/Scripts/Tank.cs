using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tank : MonoBehaviour
{
    public float Health;
    private float MaxHealth;
    [Range (0f, 1f)]
    public float Protection;
    
    public float Speed;
    public float RotateSpeed;

    private Weapon mainWeapon;
    private float Cooldown = 0;
    private int indexWeapon = 0;

    public Weapon[] Weapons;

    private Rigidbody rb;

    public Slider Reload;
    public Slider HP;

    public ParticleSystem ParticleSystem;
    
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        mainWeapon = Weapons[indexWeapon];

        MaxHealth = Health;
    }


    void FixedUpdate()
    {
        if(GameManager.isGame)
        {
            if(Input.anyKey)
            {
                if(Input.GetKey(KeyCode.Space) && Cooldown <= 0)
                {
                    Shoot();
                }
            

                if(Input.GetKey(KeyCode.UpArrow))
                {
                    Move(Speed);
                }
                else if(Input.GetKey(KeyCode.DownArrow))
                {
                    Move(-Speed);
                }
            
                if(Input.GetKey(KeyCode.LeftArrow))
                {
                    TankRotate(-RotateSpeed);
                }
                else if(Input.GetKey(KeyCode.RightArrow))
                {
                    TankRotate(RotateSpeed);
                }
            }
        }
    }

    void Update()
    {
        if(GameManager.isGame)
        {
            if(Cooldown >0)
            {
                Cooldown -= Time.deltaTime;
                Reload.value = 1 - Cooldown / mainWeapon.Cooldown;
            }
        

            if(Input.GetKeyDown(KeyCode.V))
            {
                ChangeWeapon(-1);
            }
            else if(Input.GetKeyDown(KeyCode.B))
            {
                ChangeWeapon(1);
            }
        }
        
    }

    private void TankRotate(float degrees)
    {
        var rotation = rb.rotation.eulerAngles;
        rotation.y += degrees;
        rb.MoveRotation(Quaternion.Euler(rotation));
    }

    private void Move(float speed)
    {
        rb.AddForce(transform.forward * speed);
    }

    private void ChangeWeapon(int i)
    {
        indexWeapon = (indexWeapon + i + Weapons.Length) % Weapons.Length;
        mainWeapon = Weapons[indexWeapon];
        Cooldown = mainWeapon.Cooldown;
    }

    private void Shoot()
    {
        Cooldown = mainWeapon.Cooldown;

        ParticleSystem.startColor = mainWeapon.ShootColor;
        ParticleSystem.Play();

        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);
        
        if(Physics.Raycast(ray, out hit))
        {
            if(hit.transform.CompareTag("Enemy"))
            {
                hit.transform.GetComponent<Enemy>().Damage(mainWeapon.AttackPower);
            }
        }
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.CompareTag("Enemy"))
        {
            Health -= other.GetComponent<Enemy>().AttackPower*(1 - Protection);
            HP.value = Health/MaxHealth; 
            Destroy(other.gameObject);
            if(Health <=0)
            {
                GameManager.GameOver();
            }
        }
    }
}
