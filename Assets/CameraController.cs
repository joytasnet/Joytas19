using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject prefab;

    void Update()
    {
        if(Input.GetMouseButtonDown(0)){
            StartCoroutine(Explosion());
        }
    }
    IEnumerator Explosion(){
        yield return null;
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray,out hit,100f)){
            if(hit.collider.CompareTag("Cube")){
                Destroy(hit.collider.gameObject);
                GameObject explosion = Instantiate(
                    prefab,
                    hit.point,
                    Quaternion.identity
                );
                Collider[] colls = Physics.OverlapSphere(hit.point,10f);
                foreach(Collider coll in colls){
                    Rigidbody rb = coll.GetComponent<Rigidbody>();
                    if(rb != null){
                        rb.AddExplosionForce(100f,hit.point,10f,10f);

                    }
                }
                Destroy(explosion,5f);
            }

        }
    }
}
