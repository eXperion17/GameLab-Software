using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    
    private Rigidbody2D rigidBody;
    private float speed;
    [SerializeField]
    private float freezeMultiplier;
    private bool isSlowed = false;
    private List<TowerBase> towers;
    private TowerBase closestTower;
    private Vector3 currentPosition;
    private float offset;

    [SerializeField]
    float freezeTime;
    // Use this for initialization
    void Start () {
        //rigidBody = this.GetComponent<Rigidbody2D>();
        speed = 0.5f;
        offset = 0.1f;
	    GetTowers();

        //Debug.Log(towers.Count);
        transform.position = new Vector3(transform.position.x, transform.position.y + offset, transform.position.z);
    }

    void GetTowers() {
        GameObject[] towersTEMP = GameObject.FindGameObjectsWithTag("Tower");
        towers = new List<TowerBase>();
        for (int i = 0; i < towersTEMP.Length; i++) {
            towers.Add(towersTEMP[i].GetComponent<TowerBase>());
        }
    }

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Explosion")) {
            closestTower.Shoot(this.gameObject);
            GameObject player = GameObject.Find("MainTower");
            Combo comboScript = player.GetComponent<Combo>();
            comboScript.EnemyDied();
            Explode();
        }
        else if (other.CompareTag("Freeze"))
        {
            Freeze();
        }

    }
    void Freeze()
    {
        
        if (isSlowed == false)
        {
            speed *= freezeMultiplier;
            isSlowed = true;
            
        }
    }

    // Update is called once per frame
    void Update () {
        currentPosition = this.transform.position;
        if (closestTower == null || !closestTower.Active())
        {
            closestTower = GetClosestTower(currentPosition);
        }

	    if (closestTower != null)
        {
            transform.position = Vector3.MoveTowards(currentPosition, closestTower.transform.position, Time.deltaTime * speed);
            transform.LookAt(closestTower.transform);
       
        if (isSlowed && freezeTime > 0)
        {
            freezeTime -= Time.deltaTime;
            
        }
        else if (freezeTime <= 0)
        {
            freezeTime = 2;
            isSlowed = false;
            speed = 0.5f;
            
        }
        }
    }

    private TowerBase GetClosestTower(Vector3 currentPos) {
        float minDist = Mathf.Infinity;
        TowerBase tMin = null;

        foreach (TowerBase tower in towers) {
            if (!tower.Active()) continue;

            float dist = Vector3.Distance(currentPos, tower.transform.position);
            if(dist < minDist)
            {
                tMin = tower;
                minDist = dist;
            }
        }
        return tMin;
    }
    public void Explode()
    {
        SoundManager.Instance.PlayBombExplode();
        ParticleSystem exp = GetComponent<ParticleSystem>();
        exp.Play();
        transform.FindChild("Bomb_Roll").gameObject.SetActive(false);
        speed = 0;
        Destroy(this.gameObject, exp.main.duration);
    }

}
