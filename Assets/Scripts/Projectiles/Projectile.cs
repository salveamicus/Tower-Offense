using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public interface Projectile
{
    void Shoot(MonoBehaviour owner, Vector2 direction);
}