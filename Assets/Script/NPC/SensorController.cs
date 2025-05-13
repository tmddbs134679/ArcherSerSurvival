using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.Mathematics;
using UnityEngine;
public class SensorController : MonoBehaviour
{
    [SerializeField]
    List<GameObject> gameObjects = new List<GameObject>();
    [SerializeField]
    GameObject target;
    [SerializeField]
    GameObject player;
    float short_distance;
    void Start()
    {
    }

    void Update()
    {
        Target_select();
        if (gameObjects.Count == 0)
        {
            target = null;
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (target != null)
            {
                if (player.GetComponent<PlayerController>().isDialog == false)
                {
                    player.GetComponent<PlayerController>().isDialog = true;
                    target.gameObject.GetComponent<NPCController>().Show_dialog();
                }
                else
                {
                    target.gameObject.GetComponent<NPCController>().Next_dialog();
                    if (target.gameObject.GetComponent<NPCController>().dialog_index == 0)
                    {
                        player.GetComponent<PlayerController>().isDialog = false;
                    }
                }
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (collision.CompareTag("NPC"))
            {
                gameObjects.Add(collision.gameObject);
                collision.GetComponent<NPCController>()?.Auto_chat();
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (collision.CompareTag("NPC"))
            {
                collision.gameObject.GetComponent<NPCController>().Outline_off();
                collision.gameObject.GetComponent<NPCController>().Close_dialog();
                gameObjects.Remove(collision.gameObject);
            }
        }
    }
    void Target_select()
    {
        if (gameObjects.Count != 0)
        {
            foreach (GameObject obj in gameObjects)
            {
                short_distance = 20;
                if (short_distance > Vector3.Distance(obj.transform.position, transform.position))
                {
                    if (target != null)
                    {
                        target.GetComponent<NPCController>().Outline_off();
                    }
                    short_distance = Vector3.Distance(obj.transform.position, transform.position);
                    target = obj;
                    target.GetComponent<NPCController>().Outline_on();
                }
            }
        }
    }
}