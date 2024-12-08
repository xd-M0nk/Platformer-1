using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    public float speed = 1;
    public TextMeshProUGUI textBox;
    public float lifeTime = 1;


    void Start()
    {
        Destroy(gameObject, lifeTime);
        textBox = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        transform.localPosition += Vector3.up * speed * Time.deltaTime;
    }

    public void SetText(int damage)
    {
        textBox.text = damage.ToString();
    }
}
