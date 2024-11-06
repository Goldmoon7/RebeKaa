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
using UnityEngine.TestTools.Utils;
using UnityEngine.UIElements;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

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

    [Test]

    public void SnakeRotateTest(){
        Snake s1;
        GameObject s = new GameObject("Kaa");
        s1 = s.AddComponent<Snake>();
        UnityEngine.Vector3 v = s1.transform.position;
        int everythingOK = 0;
        
        s1.Rotate(s,v);
        int currRotHor = s1.getCurrentHorizDir();
        int currRotVer = s1.getCurrentVertDir();
        UnityEngine.Quaternion dir = s1.transform.rotation;
        Vector3 locScale = s1.transform.localScale;
        
        if(currRotHor != 0 && (dir != Quaternion.Euler(new Vector3(0,0,90)) || locScale != new Vector3(1, -currRotHor, 1))){
            everythingOK = 1;
        }
        else if(currRotHor == 0 && (dir != Quaternion.Euler(new Vector3(0,0,180)) || locScale != new Vector3(1, -currRotVer, 1))){
            everythingOK = 1;
        }

        Assert.That(everythingOK, Is.EqualTo(0));
    }

    [Test]

    public void SnakeMakeBiggerTest(){
        Snake s1;
        GameObject s = new GameObject("Kaa");
        s1 = s.AddComponent<Snake>();
        s1.instanciar();
        List<GameObject> body = s1.getBody();
        s1.instanciarCola();
        
        
        int everythingOK = 0;

        s1.makeBigger();

        if(body.Count != 3){
            everythingOK = 1;
        }
        
        Assert.That(everythingOK, Is.EqualTo(0));
    }



}
