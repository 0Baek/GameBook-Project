using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreMove : MonoBehaviour
{
    public float MonsterSpeed = 0;
    public Vector2 StartPosition;
    // Start is called before the first frame update
    private void OnEnable()
    {
        transform.position = StartPosition;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.left * Time.deltaTime * MonsterSpeed);
        if (transform.position.x < -6)
        {
            gameObject.SetActive(false);
        }
    }
}
