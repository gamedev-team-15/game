using System.Collections;
using System.Collections.Generic;
using ModVal;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class ModValTest
{
    [Test]
    public void ModValFloatTest()
    {
        ModifiableValueFloat v = new(100);
        ValueModifier<float> mc100 = new(100);
        ValueModifier<float> mp100 = new(100, ModifierType.Percentage);
        ValueModifier<float> mpN50 = new(-50, ModifierType.Percentage);
        ValueModifier<float> mbp50 = new(50, ModifierType.BasePercentage);
        
        // Case 1
        Assert.AreEqual(100, v.Value);
        v.AddModifier(mc100);
        Assert.AreEqual(200, v.Value);
        v.AddModifier(mp100);
        Assert.AreEqual(400, v.Value);
        v.AddModifier(mpN50);
        Assert.AreEqual(200, v.Value);
        v.ClearModifiers();
        Assert.AreEqual(v.BaseValue, v.Value);
        Assert.AreEqual(100, v.Value);
        
        // Case 2
        v.AddModifier(mc100);
        v.AddModifier(mc100);
        Assert.True(v.HasModifier(mc100));
        v.RemoveModifier(mc100);
        Assert.True(v.HasModifier(mc100));
        Assert.AreEqual(200, v.Value);
        v.RemoveModifier(mc100);
        Assert.False(v.HasModifier(mc100));
        Assert.AreEqual(100, v.Value);
        v.ClearModifiers();
        
        // Case 3
        v.AddModifier(mc100);
        v.AddModifier(mbp50);
        v.AddModifier(mp100);
        v.AddModifier(mc100);
        Assert.AreEqual(600, v.Value);
        v.RemoveModifier(mp100);
        Assert.AreEqual(350, v.Value);
        v.ClearModifiers();
    }
}
