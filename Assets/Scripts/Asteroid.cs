﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    private readonly float _rotateSpeed = 3.0f;
    [SerializeField] private GameObject _explosion = null;
    private SpawnManager _spawnManager = null;

    private void Start()
    {
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
    }

    void Update()
    {
        transform.Rotate(new Vector3(0, 0, _rotateSpeed * Time.deltaTime));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Laser"))
        {
            Destroy(collision.gameObject);
            _ = Instantiate(_explosion, transform.position, Quaternion.identity);
            gameObject.GetComponent<Renderer>().enabled = false;
            Destroy(gameObject, 3.0f);
            _ = StartCoroutine(StartSpawning());
        }
    }

    private IEnumerator StartSpawning()
    {
        yield return new WaitForSeconds(2.5f);
        if (null != _spawnManager)
        {
            _spawnManager.StartSpawning();
        }
    }
}