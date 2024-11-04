using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Numerics;
using JetBrains.Annotations;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.PlayerLoop;

[TestFixture]
public class ProjectTests 
{
    
    //ENEMY TESTS
    [Test]
    public void RotateEnemyTest(){
        Enemy1 e1;
        GameObject g = new GameObject("Enemy1");
        e1 = g.AddComponent<Enemy1>();
        int everythingOK = 0;
        
        
        e1.RotateEnemy(0);
        float currRot = e1.GetCurrentRotation();
        UnityEngine.Vector2 movDir = e1.GetMoveDirection();

        if(currRot == 0 && movDir != new UnityEngine.Vector2(0,1)){
            everythingOK = 1;
        }
        if(currRot == 90 && movDir != new UnityEngine.Vector2(-1,0)){
            everythingOK = 2;
        }
        if(currRot == 180 && movDir != new UnityEngine.Vector2(0,-1)){
            everythingOK = 3;
        }
        if(currRot == 270 && movDir != new UnityEngine.Vector2(1,0)){
            everythingOK = 4;
        }

        Assert.That(everythingOK, Is.EqualTo(0));

    }

    [Test]
    public void ChangeOrientationTest(){
        Enemy2 e2;
        GameObject g = new GameObject("Enemy2");
        e2 = g.AddComponent<Enemy2>();

        int everythingOK = 0;
        int i = 0;

        float currRot = e2.GetCurrentRotation();
        e2.ChangeOrientation(i);
        if(currRot != e2.GetCurrentRotation()){
            everythingOK = 1;
        }
        e2.ChangeOrientation(i+1);
        if(currRot != e2.GetCurrentRotation() - 90f){
            everythingOK = 2;
        }

        Assert.That(everythingOK, Is.EqualTo(0));

    }
    //SPAWNER TESTS



    //SNAKE TESTS


}
