using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Unit
{
    public void Shoot(Vector3 direction);
    public void Damage(float amount);
}
