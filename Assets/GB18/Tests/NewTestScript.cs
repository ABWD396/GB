using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class NewTestScript
{
    private GameObject knight;
    private GameObject slime;


    [SetUp]
    public void Setup()
    {
        SceneManager.LoadScene("TestScene");

        knight = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/GB18/Prefabs/Player/Knight.prefab");
        slime = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/GB18/Prefabs/Creatures/Slime.prefab");
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator TestDies()
    {
        var slimeSpawned = Object.Instantiate(slime, new Vector3(0f, 0.16f, 0), new Quaternion());
        var knightDeath = Object.Instantiate(knight, new Vector3(0, 1, 0), new Quaternion());

        Assert.IsTrue(knightDeath.GetComponent<Health>().CheckIsAlive());

        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return new WaitForSeconds(4);

        UnityEngine.Assertions.Assert.IsNull(knightDeath);


        Assert.IsTrue(slimeSpawned.GetComponent<Health>().CheckIsAlive());


        var knightShoot = Object.Instantiate(knight, new Vector3(-0.32f, 0.17f, 0), new Quaternion());

        yield return new WaitForSeconds(1);
        knightShoot.GetComponent<Shooter>().Shoot(1);

        yield return new WaitForSeconds(2);

        UnityEngine.Assertions.Assert.IsNull(slimeSpawned);

    }
}
