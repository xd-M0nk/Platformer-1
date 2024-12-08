using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageIndicator : MonoBehaviour
{
    public static DamageIndicator instance;

    public GameObject damageTextPrefab;

    private void Awake() 
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public void ShowDamage(int damage, Vector3 position)
    {
        var screenPosition = Camera.main.WorldToScreenPoint(position);

        //print(position + " " + screenPosition);

        var damageText = Instantiate(damageTextPrefab, screenPosition, Quaternion.identity, transform).GetComponent<DamageText>();

        damageText.SetText(damage);
    }
}
