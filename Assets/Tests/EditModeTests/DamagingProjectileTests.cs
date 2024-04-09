using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using AIBERG.Core;

public class DamagingProjectileTests
{
    [Test]
    public void AddTagToDamage_AddNewTag_TagAddedSuccessfully()
    {
        var damagingProjectile = new GameObject().AddComponent<DamagingProjectile>();
        var damagingProjectileComponent = damagingProjectile.GetComponent<DamagingProjectile>();
        string tagToAdd = "Enemy";

        damagingProjectileComponent.AddTagToDamage(tagToAdd);

        Assert.Contains(tagToAdd, damagingProjectile.tagsToDamage);
    }

    [Test]
    public void AddTagToDamage_TryAddExistingTag_TagNotAdded(){
        var damagingProjectile = new GameObject().AddComponent<DamagingProjectile>();
        var damagingProjectileComponent = damagingProjectile.GetComponent<DamagingProjectile>();
        string tagToAdd = "Enemy";

        damagingProjectileComponent.AddTagToDamage(tagToAdd);

        damagingProjectileComponent.AddTagToDamage(tagToAdd);
        Assert.AreEqual(1, damagingProjectile.tagsToDamage.Count);
    }
    
}
