using UnityEngine;

public class MonsterShooting : MonoBehaviour
{
    public GameObject bulletPrefab; // 발사할 탄환 프리팹
    public Transform firePoint; // 탄환 발사 위치
    public float fireRate = 1f; // 발사 간격
    public float bulletSpeed = 5f; // 탄환 속도

    private float nextFireTime;
    private Transform player; // 플레이어 위치

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // 플레이어 찾기
    }

    void Update()
    {
        if (Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + 1f / fireRate;
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();

        // 플레이어를 향해 탄환 발사
        Vector3 direction = (player.position - firePoint.position).normalized;
        rb.linearVelocity = direction * bulletSpeed;

        // 탄환이 화면 밖으로 나가면 삭제
        Destroy(bullet, 3f);
    }
}