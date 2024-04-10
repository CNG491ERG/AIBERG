using System;
using System.Collections;
using AIBERG.Core;
using NUnit.Framework;
using AIBERG.Utilities;
using UnityEngine;
using UnityEngine.TestTools;
public class BossTesting
{
    [UnityTest]
    public IEnumerator TakeDamage_ShootProjectileTowardsBoss_DamageTaken()
    {
        var environment = GameObject.Instantiate(Resources.Load("Environment_Test")) as GameEnvironment;
        environment.Awake();
        environment.Start();
        while(environment.Boss.Health == environment.Boss.MaxHealth){
            environment.Player.basicAbility.UseAbility(true);
            yield return null;
        } 
        Assert.Less(environment.Boss.Health, environment.Boss.MaxHealth);
    }
}
