using System;
using System.Collections;
using System.Collections.Generic;
using Car;
using UnityEngine;

public class SlowDownObstacle : MonoBehaviour
{
   private void OnCollisionEnter(Collision collision)
   {
      if (collision.gameObject.CompareTag("Player"))
      {
         collision.gameObject.GetComponent<CarPlayerScript>().LoseSpeed(5f);
         Destroy(this.gameObject);
      }
   }
}
