using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject fence_prefab;

    [SerializeField] private GameObject mud_prefab;

    [SerializeField] private GameObject sack_prefab;
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
        spawn_cooldown = 3.0f;
        switch (current_lvl)
        {
            case 1:
                loadLevel1();
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
            Instantiate(current_wave.GetObject(), new Vector3(current_wave.GetPosition(), 5, -3),
                Quaternion.Euler(0, 0, current_wave.GetRotation()));
            spawn_cooldown = current_wave.GetDelay();
        }
        spawn_cooldown -= Time.deltaTime;
    }

    void loadLevel1()
    {
        waves = new Queue<Wave>();
        waves.Enqueue(new Wave(3,0,fence_prefab,10));
    }
}
