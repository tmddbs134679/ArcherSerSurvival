using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollAndLoop : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float mapWidth = 8f;
    private float positionY;

    private void Start()
    {
        positionY = transform.position.y;
    }

    void Update()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);

        if (transform.position.x < -mapWidth * 2)
        {
            Reposition();
        }
    }

    private void Reposition()
    {
        transform.position = new Vector2(mapWidth * 2, positionY);
    }
}
