using UnityEngine;

public class Shotgun : Weapon
{
    private const float SpreadAngle = 12f;
    public override void Shoot()
    {
        Instantiate(Bullet, ShootPoint.position, Quaternion.identity);
        
        Quaternion leftRotation = ShootPoint.rotation * Quaternion.Euler(0, 0, -SpreadAngle);
        Instantiate(Bullet, ShootPoint.position, leftRotation);
        
        Quaternion rightRotation = ShootPoint.rotation * Quaternion.Euler(0, 0, SpreadAngle);
        Instantiate(Bullet, ShootPoint.position, rightRotation);

    }
}
