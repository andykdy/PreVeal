using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject fence_prefab;

    [SerializeField] private GameObject mud_prefab;

    [SerializeField] private GameObject sack_prefab;

    [SerializeField] private GameObject cow_prefab;
    public int current_lvl; // TODO: Really weird??
    private float spawn_cooldown;
    private Queue<Wave> waves;

    struct Wave
    {
        private float xpos;
        private float rot;
        private GameObject pf;
        private float dly;

        public Wave(float xposition, float rotation, GameObject prefab, float delay)
        {
            xpos = xposition;
            rot = rotation;
            pf = prefab;
            dly = delay;
        }

        public float GetPosition()
        {
            return xpos;
        }

        public float GetRotation()
        {
            return rot;
        }

        public GameObject GetObject()
        {
            return pf;
        }

        public float GetDelay()
        {
            return dly;
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        spawn_cooldown = 0.0f;
        switch (current_lvl)
        {
            case 1:
                loadLevel1();
                break;
            case 2:
                loadLevel2();
                break;
            default:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (waves.Count > 0 && spawn_cooldown < 0.0f)
        {
            Wave current_wave = waves.Dequeue();
            Instantiate(current_wave.GetObject(), new Vector3(current_wave.GetPosition(), 6, -10),
                Quaternion.Euler(0, 0, current_wave.GetRotation()));
            spawn_cooldown = current_wave.GetDelay();
        }
        spawn_cooldown -= Time.deltaTime;
    }

    void loadLevel1()
    {
        waves = new Queue<Wave>();
        waves.Enqueue(new Wave(2.5f,0,mud_prefab,2));
        waves.Enqueue(new Wave(2.5f,0,fence_prefab,2));
        
        waves.Enqueue(new Wave(-2.5f,0,fence_prefab,0));
        waves.Enqueue(new Wave(-1.35f,0,fence_prefab,1));
        
        waves.Enqueue(new Wave(-1.5f,0,fence_prefab,0.5f));
        
        waves.Enqueue(new Wave(-0.75f,90,fence_prefab,0));
        waves.Enqueue(new Wave(-2.25f,90,fence_prefab,0));
        waves.Enqueue(new Wave(-1.5f, 0, cow_prefab, 0));
        waves.Enqueue(new Wave(1.5f, 180, cow_prefab, 0));
        waves.Enqueue(new Wave(2.5f,0,sack_prefab,1));
        
        waves.Enqueue(new Wave(-1.5f, 90, cow_prefab, 2));
        
        waves.Enqueue(new Wave(-2.5f,0,fence_prefab,0));
        waves.Enqueue(new Wave(-1.35f,0,fence_prefab,1.5f));
        
        waves.Enqueue(new Wave(2.5f,0,fence_prefab,0));
        waves.Enqueue(new Wave(1.35f, 0, fence_prefab, 0));
        waves.Enqueue(new Wave(-1.1f,0,mud_prefab,2));
        
        waves.Enqueue(new Wave(2.5f,0,cow_prefab,0.5f));
        waves.Enqueue(new Wave(1.35f,0,sack_prefab,1));
        
        waves.Enqueue(new Wave(-2.5f,0,fence_prefab,0));
        waves.Enqueue(new Wave(-1.35f,0,fence_prefab,2));
        
        waves.Enqueue(new Wave(1.5f,0,fence_prefab,0.4f));
        
        waves.Enqueue(new Wave(0.75f,90,fence_prefab,0));
        waves.Enqueue(new Wave(2.25f,90,fence_prefab,0));
        waves.Enqueue(new Wave(1.5f, 0, cow_prefab, 0));
        waves.Enqueue(new Wave(-1.5f, 0,mud_prefab,0));
        waves.Enqueue(new Wave(-1.5f,0,sack_prefab,3));
        
        waves.Enqueue(new Wave(2.75f,0,fence_prefab,0));
        waves.Enqueue(new Wave(1.4f,0,fence_prefab,0));
        waves.Enqueue(new Wave(0.0f, 0, fence_prefab, 0));
        waves.Enqueue(new Wave(-2.75f,0,fence_prefab,1));
        
        waves.Enqueue(new Wave(2.0f, 180, cow_prefab,0));
        waves.Enqueue(new Wave(-0.6f, 90, fence_prefab, 0));
        waves.Enqueue(new Wave(-2.25f,90,fence_prefab,3));
        
        waves.Enqueue(new Wave(2.75f,0,fence_prefab,0));
        waves.Enqueue(new Wave(-2.75f,0,fence_prefab,2));
        waves.Enqueue(new Wave(0.0f,0,fence_prefab,2));
        waves.Enqueue(new Wave(2.75f,0,fence_prefab,0));
        waves.Enqueue(new Wave(-2.75f,0,fence_prefab,2));
        
        waves.Enqueue(new Wave(0.0f, 0, mud_prefab, 2));
        waves.Enqueue(new Wave(2.5f,0,mud_prefab,2));
        waves.Enqueue(new Wave(-1.4f,0,mud_prefab,2));
        waves.Enqueue(new Wave(0.0f, 0, mud_prefab, 0));
        waves.Enqueue(new Wave(-1.8f, 0, cow_prefab, 3));
        waves.Enqueue(new Wave(-1.75f,0,mud_prefab,0));
        waves.Enqueue(new Wave(1.75f,0,mud_prefab,0));
        
        waves.Enqueue(new Wave(1.75f, 0, sack_prefab, 2));
        waves.Enqueue(new Wave(0.5f, 0, mud_prefab, 2));
        waves.Enqueue(new Wave(-2.25f,0,mud_prefab,2));
        
        waves.Enqueue(new Wave(0.5f, 0, fence_prefab, 2));
        waves.Enqueue(new Wave(-2.25f,0,fence_prefab,2));
        
        
        
        
        waves.Enqueue(new Wave(-2.75f,0,fence_prefab,0));
        waves.Enqueue(new Wave(-1.4f,0,fence_prefab,0));
        waves.Enqueue(new Wave(-0.0f, 0, fence_prefab, 0));
        waves.Enqueue(new Wave(2.75f,0,fence_prefab,1));
        
        waves.Enqueue(new Wave(-2.5f, 180, cow_prefab,0));
        waves.Enqueue(new Wave(0.6f, 90, fence_prefab, 0));
        waves.Enqueue(new Wave(2.25f,90,fence_prefab,1.5f));
        waves.Enqueue(new Wave(0.6f, 90, fence_prefab, 0));
        waves.Enqueue(new Wave(-1.8f, 90, fence_prefab, 0));
        waves.Enqueue(new Wave(-1.0f, 90, cow_prefab, 0));
        waves.Enqueue(new Wave(2.25f,90,fence_prefab,1));
        
        waves.Enqueue(new Wave(-1.4f,0,fence_prefab,0));
        waves.Enqueue(new Wave(2.75f,0,fence_prefab,1));
        
        
    }
    
    void loadLevel2()
    {
        waves = new Queue<Wave>();
    }
}
