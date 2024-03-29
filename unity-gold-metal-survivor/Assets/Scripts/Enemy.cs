using System.Collections;
using UnityEngine;


public class Enemey : MonoBehaviour
{
    public float speed;
    public float health;
    public float maxHealth;
    
    public RuntimeAnimatorController[] animCon;
    public Rigidbody2D target;

    bool isLive = true;

    Rigidbody2D _rigidbody;
    private Collider2D _collider;
    Animator _anim;
    SpriteRenderer _sprite;
    WaitForFixedUpdate _wait;
    private static readonly int Dead1 = Animator.StringToHash("Dead");

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _sprite = GetComponent<SpriteRenderer>();
        _anim = GetComponent<Animator>();
        _wait = new WaitForFixedUpdate();
        _collider = GetComponent<Collider2D>();
    }

    // When Enemy Enabled and Active
    private void OnEnable()
    {
        target = GameManager.Instance.player.GetComponent<Rigidbody2D>();
        isLive = true;
        health = maxHealth;
        
        //초기화 과정
        isLive = true;
        _collider.enabled = true;
        _rigidbody.simulated = true;
        _sprite.sortingOrder = 2; // 이걸 왜 1 로 내리는가?
        _anim.SetBool("Dead", false);
    }

    public void Init(SpawnData spawnData)
    {
        _anim.runtimeAnimatorController = animCon[spawnData.spriteType];
        speed = spawnData.speed;
        maxHealth = spawnData.maxHealth;
        health = spawnData.maxHealth;
    }

    private void FixedUpdate()
    {
        if(!isLive) // 0 은 base를 의미함 Hit 상태에서는 물리법칙을 잠시 멈춘다는 
            return;
        if (_anim.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
            return;
        Vector2 dirVec = target.position - _rigidbody.position;
        Vector2 nextVec = dirVec.normalized * (speed * Time.fixedDeltaTime);
        _rigidbody.MovePosition(_rigidbody.position + nextVec);
        _rigidbody.velocity = Vector2.zero;
    }

    private void LateUpdate()
    {
        if(!isLive)
            return;
        _sprite.flipX = target.position.x < _rigidbody.position.x;
    }
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Bullet") || !isLive) //충돌이 연달아 일어났을 때를 방지하기 위함
            return;
        health -= collision.GetComponent<Bullet>().damage;
        StartCoroutine(KnockBack()); // 비동기 실행 함수

        if (health > 0)
        {
            _anim.SetTrigger("Hit");
        }
        else //으앙 쥬금
        {
            // Debug.Log("Triggered?");
            isLive = false;
            _collider.enabled = false;
            _rigidbody.simulated = false;
            _sprite.sortingOrder = 1; // 이걸 왜 1 로 내리는가?
            _anim.SetBool("Dead", true);
            GameManager.Instance.kill++;
            GameManager.Instance.GetExp();
        }
    }

    IEnumerator KnockBack()
    {
        // yield return null; // 1프레임을 쉬기 (오로지 비동기 용)
        // yield return new WaitForSeconds(2f); // 2초 쉬기 (오로지 비동기 용)
        yield return _wait; // 다음 하나의 물리 프레임까지 대기
        Vector3 playerPos = GameManager.Instance.player.transform.position;
        Vector3 dirVec = transform.position - playerPos;
        _rigidbody.AddForce(dirVec.normalized * 3, ForceMode2D.Impulse);
    }

    private void Dead()
    {
        gameObject.SetActive(false);
        // var degree = Random.Range(0.00f, 6.28f);
        // var posX = Mathf.Cos(degree);
        // var posY = Mathf.Sin(degree);
        //
        // transform.position = GameManager.Instance.player.transform.position 
        //                      + new Vector3(10f*posX, 10f*posY, 0);
    }
}
