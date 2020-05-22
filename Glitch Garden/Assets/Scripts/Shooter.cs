using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private GameObject[] gun;
    [SerializeField] private bool isMultiLane = false;
    private AttackerSpawner myLaneSpawner;
    private AttackerSpawner myUpperLaneSpawner;
    private AttackerSpawner myLowerLaneSpawner;
    private Animator animator;
    private GameObject projectileParent;
    private const string PROJECTILE_PARENT_NAME = "Projectiles";

    private void Start()
    {
        SetLaneSpawner();
        animator = GetComponent<Animator>();
        CreateProjectileParent();
    }

    private void CreateProjectileParent()
    {
        projectileParent = GameObject.Find(PROJECTILE_PARENT_NAME);
        if (!projectileParent)
        {
            projectileParent = new GameObject(PROJECTILE_PARENT_NAME);
        }
    }

    private void Update()
    {
        if (IsAttackerInLane())
        {
            //    Change animation state to shooting
            animator.SetBool("isAttacking", true);
        }
        else
        {
            //    Change animation state to idle
            animator.SetBool("isAttacking", false);
        }
    }

    private void SetLaneSpawner()
    {
        AttackerSpawner[] spawners = FindObjectsOfType<AttackerSpawner>();
        
        foreach (AttackerSpawner spawner in spawners)
        {
            bool IsCloseEnough =
                (Mathf.Abs(spawner.transform.position.y - transform.position.y) <=
                 Mathf.Epsilon); //    find its lane's spawner
            bool IsUpperCloseEnough =
                (Mathf.Abs(spawner.transform.position.y - (transform.position.y + 1)) <=
                 Mathf.Epsilon); //    find its upper lane's spawner
            bool IsLowerCloseEnough =
                (Mathf.Abs(spawner.transform.position.y - (transform.position.y - 1)) <=
                 Mathf.Epsilon); //    find its lower lane's spawner

            if (IsCloseEnough)
            {
                myLaneSpawner = spawner;
            }

            else if (IsUpperCloseEnough)
            {
                myUpperLaneSpawner = spawner;
            }

            else if (IsLowerCloseEnough)
            {
                myLowerLaneSpawner = spawner;
            }
        }
    }

    private bool IsAttackerInLane()
    {
        if (isMultiLane)
        {
            if (myUpperLaneSpawner && myUpperLaneSpawner.transform.childCount > 0)
            {
                return true;
            }
        
            if (myLowerLaneSpawner && myLowerLaneSpawner.transform.childCount > 0)
            {
                return true;
            }
        }
        
        //    if my lane spawner child count less than or equal to 0
        //    return false
        if (myLaneSpawner.transform.childCount <= 0)
        {
            return false;
        }
        else
        {
            return true;
        }
        
    }

    public void Fire()
    {
        for (int i = 0; i < gun.Length; i++)
        {
            GameObject newProjectile =
                Instantiate(projectile, gun[i].transform.position, transform.rotation) as GameObject;
            newProjectile.transform.parent = projectileParent.transform;
        }

        // GameObject newProjectile = Instantiate(projectile, gun.transform.position, transform.rotation) as GameObject;
        // newProjectile.transform.parent = projectileParent.transform;
    }
}