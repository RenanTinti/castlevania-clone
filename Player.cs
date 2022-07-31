using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Player : MonoBehaviour
{
    public GameObject knife;
    public Rigidbody2D axe;
    public Rigidbody2D holyWater;
    public GameObject cross;
    public Transform SWSpawner;
    private Rigidbody2D rb;
    public Animator anim;
    public Image lifeBarUI;
    public Text hearthText;
    public float hp;
    public float maxHp = 100;
    private float speed = 5;
    public float runningSpeed = 8;
    public float jumpForce = 13;
    public int hearths;
    public int life;
    public int equipped = 1;
    public float hForce = 0;
    public float atkRate;
    float nextAtk;
    public bool isRunning;
    public bool isJumping;
    public bool isCrouched;
    public bool onGround;
    public bool facingRight;
    public bool isAttacking;
    public bool invincibility;
    public bool canMove;
    public bool isDead = false;
    
    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        Physics2D.IgnoreLayerCollision(9, 10, false);
        hp = maxHp;
    }
  
    void Update()
    {
        if(canMove) // Andar para esquerda e direita //
        {    
            hForce = Input.GetAxisRaw("Horizontal");

            rb.velocity = new Vector2(hForce * speed, rb.velocity.y);
            anim.SetFloat("Speed", Mathf.Abs(hForce));

            if(hForce > 0 && !facingRight)
            {
                Flip();
            }
            else if(hForce < 0 && facingRight)
            {
                Flip();
            }
        }
    }

    void FixedUpdate()
    {
        if(invincibility)
        {
            Physics2D.IgnoreLayerCollision(9, 10);
        }
        if(canMove)
        {
            if(Input.GetKeyDown(KeyCode.Space) && onGround) // Input pulo //
            {
                Jump();
            }

            if(Input.GetKey(KeyCode.S) && onGround && rb.velocity.x == 0) // Input agachar //
            {
                anim.SetBool("Run", false);
                isCrouched = true;
                anim.SetBool("Crouch", true);
            }
            else
            {
                anim.SetBool("Crouch", false);
            }

            if(Input.GetKey(KeyCode.L)) // Input corrida //
            {
                rb.velocity = new Vector2(hForce * runningSpeed, rb.velocity.y);
                anim.SetBool("Run", true);
            }
            else
            {
                anim.SetBool("Run", false);
            }

            if(hp <= 0) // Morte //
            {
                print("Dead");
                this.gameObject.SetActive(false);
                Invoke("ReloadScene", 5f);
            }
        }

        if(Input.GetKeyDown(KeyCode.K) && Time.time > nextAtk) // Input atacar //
        {
            nextAtk = Time.time + atkRate;
            StartCoroutine(Attack());
        }

        if(Input.GetKeyDown(KeyCode.I) && Time.time > nextAtk && hearths > 0) // Input usar Sub Weapon //
        {
            nextAtk = Time.time + atkRate;
            anim.SetTrigger("Throw");
            switch(equipped)
            {
                case 1: // Knife //
                    GameObject tempKnife = Instantiate(knife, SWSpawner.position, SWSpawner.rotation);
                    if(!facingRight)
                    {
                        tempKnife.transform.eulerAngles = new Vector3(0, 0, 180);
                    }
                    hearths--;
                    hearthText.text = hearths.ToString();
                break;
                case 2: // Axe //
                    Rigidbody2D tempAxe = Instantiate(axe, SWSpawner.position, SWSpawner.rotation);
                    if(facingRight)
                    {
                        tempAxe.AddForce(new Vector2(5, 15), ForceMode2D.Impulse);
                    }
                    if(!facingRight)
                    {
                        tempAxe.AddForce(new Vector2(-5, 15), ForceMode2D.Impulse);
                    }
                    hearths -= 2;
                    if(hearths <= 0)
                    {
                        hearths = 0;
                    }
                    hearthText.text = hearths.ToString();
                break;
                case 3: // Holy Water //
                    Rigidbody2D tempWater = Instantiate(holyWater, SWSpawner.position, SWSpawner.rotation);
                    if(facingRight)
                    {
                        tempWater.AddForce(new Vector2(5, 5), ForceMode2D.Impulse);
                    }
                    if(!facingRight)
                    {
                        tempWater.AddForce(new Vector2(-5, 5), ForceMode2D.Impulse);
                    }
                    hearths -= 2;
                    if(hearths <= 0)
                    {
                        hearths = 0;
                    }
                    hearthText.text = hearths.ToString();
                break;
                case 4: // Cross //
                    GameObject tempCross = Instantiate(cross, SWSpawner.position, SWSpawner.rotation);
                    if(!facingRight)
                    {
                        tempCross.transform.eulerAngles = new Vector3(0, 0, 180);
                    }
                    hearths -= 4;
                    if(hearths <= 0)
                    {
                        hearths = 0;
                    }
                    hearthText.text = hearths.ToString();
                break;
            }
        }
    }
    
    void Flip() // Inverter sprite quando personagem vira //
    {
        facingRight = !facingRight;

        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    void Jump() // Pulo //
    {
        rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        anim.SetBool("Jump", true);
        canMove = false;
        isJumping = true;
        onGround = false;       
    }

    IEnumerator Attack() // Ataque //
    {
        canMove = false;
        isAttacking = true;
        anim.SetTrigger("Attack");
        yield return new WaitForSeconds(0.5F);
        anim.SetBool("Run", false);
        isAttacking = false;
        canMove = true;
    }

    public void TookDamage(float damage) // Recebendo dano //
    {
        anim.SetTrigger("Hurt");
        if(facingRight)
        {
            rb.AddForce(new Vector2(-8, 8), ForceMode2D.Impulse);
        }
        else if(!facingRight)
        {
            rb.AddForce(new Vector2(8, 8), ForceMode2D.Impulse);
        }
        if(isJumping && facingRight)
        {
            rb.AddForce(new Vector2(-1, 0), ForceMode2D.Impulse);
        }
        else if(isJumping && !facingRight)
        {
            rb.AddForce(new Vector2(1, 0), ForceMode2D.Impulse);
        }
        StartCoroutine(Invincibility());
        hp -= damage;
    }

    public void DamageHealthBar(float damage) // Atualizando barra de vida ao receber dano //
    {
        lifeBarUI.fillAmount = (float)hp / 100;
    }
    
    public IEnumerator Invincibility() // Invencibilidade após dano //
    {
        invincibility = true;
        canMove = false;
        yield return new WaitForSeconds(0.75F);
        canMove = true;
        Physics2D.IgnoreLayerCollision(9, 10, true);
        yield return new WaitForSeconds(1.5F);
        Physics2D.IgnoreLayerCollision(9, 10, false);
        invincibility = false;
    }

    void OnTriggerEnter2D(Collider2D other) // Coleta de itens //
    {
        switch(other.tag)
            {
                case "Hearth":
                    hearths += 1;
                    hearthText.text = hearths.ToString();
                    Destroy(other.gameObject);
                break;
                case "BigHearth":
                    hearths += 5;
                    hearthText.text = hearths.ToString();
                    Destroy(other.gameObject);
                break;
                case "Food":
                    hp += 50;
                    if(hp >= maxHp)
                    {
                        hp = maxHp;
                    }
                    lifeBarUI.fillAmount = (float)hp / 100;
                    Destroy(other.gameObject);
                break;
                case "Knife":
                    equipped = 1;
                    Destroy(other.gameObject);
                break;
                case "Axe":
                    equipped = 2;
                    Destroy(other.gameObject);
                break;
                case "HolyWater":
                    equipped = 3;
                    Destroy(other.gameObject);
                break;
                case "Cross":
                    equipped = 4;
                    Destroy(other.gameObject);
                break;
            }
    }

    void ReloadScene() // Reiniciar cena //
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
