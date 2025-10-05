using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class RobotAI : MonoBehaviour
{
    Animator anim;
    
    [SerializeField] private GameObject player;
    
    [SerializeField] private Bullet bullet;
    [SerializeField] private Transform bulletSpawner;
    [SerializeField] private int bulletForce = 1000;

    [SerializeField] private int maxHP = 100;
    [SerializeField] private TMP_Text hpTxt;
    private int hp;
    
    // private bool isHealing;
    // public bool IsHealing => isHealing;
    
    private NavMeshAgent _agent;
    private float _stoppingDistance;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();
        
        _stoppingDistance = _agent.stoppingDistance;
        
        hp = maxHP;
        hpTxt.text = $"Robot HP: {hp}";
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("distance", Vector3.Distance(transform.position, player.transform.position) + _stoppingDistance);
        Debug.Log($"Distance to player: {Vector3.Distance(transform.position, player.transform.position)}");
        anim.SetBool("isChasing", CanSeeTarget());
    }
    
    public GameObject GetPlayer()
    {
        return player;
    }
    
    bool CanSeeTarget()
    {
        RaycastHit raycastInfo;
        Vector3 rayToTarget = player.transform.position - this.transform.position;
        if (Physics.Raycast(this.transform.position, rayToTarget, out raycastInfo))
        {
            if (raycastInfo.transform.gameObject.CompareTag("Player") && Vector3.Distance(transform.position, raycastInfo.transform.position) < 40)
                return true;
        }
        
        return false;
    }
    
    public void StartFiring()
    {
        InvokeRepeating(nameof(Fire), 0.5f, 0.5f);
    }
    
    void Fire()
    {
        GameObject b = Instantiate(bullet.gameObject, bulletSpawner.position, Quaternion.identity);
        b.GetComponent<Rigidbody>().AddForce(bulletSpawner.right * bulletForce);
    }
    
    public void StopFiring()
    {
        CancelInvoke(nameof(Fire));
    }

    public void ReceiveDamage(int damage)
    {
        hp -= damage;
        hpTxt.text = $"Robot HP: {hp}";
    }

    public void StartHealing()
    {
        InvokeRepeating(nameof(Heal), 0.0f, 0.5f);
    }

    private void Heal()
    {
        hp += 5;
        hpTxt.text = $"Robot HP: {hp}";

        if (hp >= maxHP)
            StopHealing();
    }
    
    private void StopHealing()
    {
        anim.SetBool("isHealing", false);
        CancelInvoke(nameof(Heal));
    }

    public int GetHP()
    {
        return hp;
    }
}
