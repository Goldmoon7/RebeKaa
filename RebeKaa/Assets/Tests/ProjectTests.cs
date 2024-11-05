using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Numerics;
using JetBrains.Annotations;
using NUnit.Framework;
using UnityEditor;
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
    
    [Test]
    public void SpawnRandomEnemyTest(){
        EnemySpawner e;
        GameObject g = new GameObject("EnemySpawner");
        e = g.AddComponent<EnemySpawner>();

        GameObject g1;
        string[] search = AssetDatabase.FindAssets("Lagarto");
        string prefabPath = AssetDatabase.GUIDToAssetPath(search[0]);
        g1 = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);
        

        int everythingOK = 0;

        e.SpawnRandomEnemy(g1);

        GameObject e1 = GameObject.FindWithTag("Enemy");

        UnityEngine.Vector3 v = e1.transform.position;
        if(v != new UnityEngine.Vector3(0,14,0) && v != new UnityEngine.Vector3(0,-14,0)
            && v != new UnityEngine.Vector3(33,0,0) && v != new UnityEngine.Vector3(-33,0,0)){
                everythingOK = 1;
        }

        Assert.That(everythingOK, Is.EqualTo(0));

    }

    [Test]
    public void SpawnFruitTest(){
        FruitSpawner e;
        GameObject g = new GameObject("FruitSpawner");
        e = g.AddComponent<FruitSpawner>();
        int everythingOK = 0;

        e.SpawnFruit();

        GameObject e1 = GameObject.FindWithTag("Fruit");

        if(e1 == null){
            everythingOK = 1;
        }

        Assert.That(everythingOK, Is.EqualTo(0));

    }

    //SNAKE TESTS




}
