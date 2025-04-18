using UnityEngine;

public class Rifle : Weapon
{
    public override void Shoot()
    {
        Instantiate(Bullet, ShootPoint.position, Quaternion.identity);
    }
}
