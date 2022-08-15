using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toadder : MonoBehaviour
{
    private SpriteRenderer SpriteRenderer;
    public Sprite Idle;
    public Sprite LeapSprite;
    public Sprite Dead;

    private void Awake()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            Move(Vector3.up);
        }

       else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            transform.rotation = Quaternion.Euler(0, 0, 180);
            Move(Vector3.down);
        }

        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.rotation = Quaternion.Euler(0, 0, 90);
            Move(Vector3.left);
        }

       else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.rotation = Quaternion.Euler(0, 0, 270);
            Move(Vector3.right);
        }



       
    }
    void Move(Vector3 direction)
    {
<<<<<<< Updated upstream
            Vector3 destination = transform.position + direction;
            StartCoroutine((Leap(destination)));
=======
        Vector3 destination = transform.position + direction;

        Collider2D barrier = Physics2D.OverlapBox(destination, Vector2.zero, 0f, LayerMask.GetMask("Barrier"));
        Collider2D platform = Physics2D.OverlapBox(destination, Vector2.zero, 0f, LayerMask.GetMask("Platform"));
        Collider2D obstacle = Physics2D.OverlapBox(destination, Vector2.zero, 0f, LayerMask.GetMask("Obstacle"));

        if (barrier != null)
        {
            return;
        }

        if (platform != null)
        {
            transform.SetParent(platform.transform);
        }
        else
        {
            transform.SetParent(null);
        }

        if(obstacle != null && platform == null)
        {
            transform.position = destination;
            Death();
        }
        else
        {
            StartCoroutine((Leap(destination)));
        }

>>>>>>> Stashed changes
    }
    private IEnumerator Leap(Vector3 destination)
    {
        Vector3 StartPosition = transform.position;
        float elapsed = 0f;
        float duration = 0.125f;

        SpriteRenderer.sprite = LeapSprite;

        while(elapsed < duration)
        {
            float t = elapsed / duration;
            transform.position = Vector3.Lerp(StartPosition, destination, t);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.position = destination;
        SpriteRenderer.sprite = Idle;
    }

    private void Death()
    {
        transform.rotation = Quaternion.identity;
        SpriteRenderer.sprite = Dead;
        enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (enabled && other.gameObject.layer == LayerMask.NameToLayer("Obstacle") && transform.parent == null)
        {
            Death();
        }
    }
}
