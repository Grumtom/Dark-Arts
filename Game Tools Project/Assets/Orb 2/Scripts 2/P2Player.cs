using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.ProBuilder.MeshOperations;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Orb_2.Scripts_2
{
    public class P2Player : MonoBehaviour
    {
        private String[] combos = {"wa" };

        [Header("--Dont Change--")]
        public NavMeshAgent agent;
        public GameObject lookDest;
        public GameObject moveDest;
        public Image healthbar;
        public float health = 100f;
        
        public GameObject manaSpinner;
        public GameObject[] manaPos;
        public GameObject[] mana;

        [Header("--Spell Bits--")]
        public string spellStack;
        public int stackSize;
        public int stackMax;
        public bool isFiring;
        public float firetimer;
        public float reload = 0;

        // w = earth, a = air, s = fire, d = lazer
        [Header("sprays")]
        [Header("--Gen-Attacks--")]
        public GameObject sprayW;
        public GameObject sprayA;
        public GameObject sprayS;
        public GameObject sprayD;

        [Header("lazers")]
        public GameObject lazerW;
        public GameObject lazerA;
        public GameObject lazerS;
        public GameObject lazerD;
    
        [Header("projectiles")]
        public GameObject projectileW;
        public GameObject projectileA;
        public GameObject projectileS;
        public GameObject projectileD;
    
        [Header("blasts")]
        public GameObject blastW;
        public GameObject blastA;
        public GameObject blastS;
        public GameObject blastD;
    
        // w = earth, a = air, s = fire, d = lazer
    

        // Update is called once per frame
        void Update()
        {
            Move();
            Inputs();
            SpellChoose();
            SpellCast();
            if(Input.GetKeyDown("e")){damage(10);} // this bitch is temporary
        }

        void Move()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // raycasts to get the mouse pos in game
            RaycastHit hitpoint;

            if (Physics.Raycast(ray, out hitpoint))
            {
                lookDest.transform.position = hitpoint.point; // sets the look point at the raycast mouse position
            }
            
            
            if (Input.GetMouseButtonDown(0))
            {
                moveDest.transform.position = lookDest.transform.position; // sets the navmesh destination to the look point
                agent.SetDestination(hitpoint.point);
            }

            gameObject.transform.LookAt(lookDest.transform.position);
            gameObject.transform.eulerAngles = new Vector3(0,transform.eulerAngles.y, 0); // looks at the mouse and locks the x and z rotations so it doesnt look up or down, or roll sideways
        }

        void Inputs()
        {
            if(stackSize < stackMax){
                if (Input.GetKeyDown("w")) { spellStack += "w"; } // assigns elements to the stack if there is space
                if (Input.GetKeyDown("a")) { spellStack += "a"; }
                if (Input.GetKeyDown("s")) { spellStack += "s"; }
                if (Input.GetKeyDown("d")) { spellStack += "d"; }
             //   if (Input.GetKeyDown("f")) { spellStack += "f"; }
             //   if (Input.GetKeyDown("g")) { spellStack += "g"; }
            }
            if (Input.GetKeyDown("r")) { spellStack = ""; } //resets the stack if needed
        }

        void SpellChoose()
        {
            stackSize = spellStack.Length; // keeps track of how many elements are on the stack
            
            manaSpinner.transform.rotation = Quaternion.Euler(0,0,0); // holds the mana slot spinner in place
            for (int I = 0; I < 4; I++)
            {
                manaPos[I].transform.Rotate(0,I,0);
            } // spin mana go brr

            // spawns the mana orbs
            if(Input.GetKeyDown("w") || Input.GetKeyDown("a") || Input.GetKeyDown("s") || Input.GetKeyDown("d") || Input.GetKeyDown("r")){
                
                for (int I = 0; I < stackSize; I++)
                {
                    if (spellStack[I] == 'w') // spawns a mana orb and assigns it to a spinner position
                    {
                        GameObject manae = Instantiate(mana[0], transform.position + new Vector3(I, 4, 0), transform.rotation);
                        manae.transform.SetParent(manaPos[I].transform);
                        if(I>0){manae.gameObject.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);}
                    }

                    if (spellStack[I] == 'a')
                    {
                        GameObject manae = Instantiate(mana[1], transform.position + new Vector3(I, 4, 0),
                            transform.rotation);
                        manae.transform.SetParent(manaPos[I].transform);
                        if(I>0){manae.gameObject.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);}
                    }

                    if (spellStack[I] == 's')
                    {
                        GameObject manae = Instantiate(mana[2], transform.position + new Vector3(I, 4, 0),
                            transform.rotation);
                        manae.transform.SetParent(manaPos[I].transform);
                        if(I>0){manae.gameObject.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);}
                    }

                    if (spellStack[I] == 'd')
                    {
                        GameObject manae = Instantiate(mana[3], transform.position + new Vector3(I, 4, 0),
                            transform.rotation);
                        manae.transform.SetParent(manaPos[I].transform);
                        if(I>0){manae.gameObject.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);}
                    }
                }
            }
        }

        void SpellCast()
        {
            reload--; // ticks down the reload and fire timers
            firetimer--;
        
            if (firetimer < 1)isFiring = false; // if any active fire timer goes below zero then firing is set to false}
        
            if (Input.GetMouseButtonDown(1) && reload < 1 && spellStack.Length > 0)
            {
                reload = 300; // timer until spell can be cast again
                
                for (int I = 0; I <= combos.Length; I++)
                {
                 if(spellStack == combos[I]){doFunkySpell();}
                }
                { generateSpell(); }
            }
        }

        public void damage(float damage)
        {
            health -= damage; // takes damage
            if(health<=0){ SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);} // restarts if health < 0
            health = Mathf.Clamp(health, 0, 100f); // clamps health
            healthbar.fillAmount = health/100; // changes healthbar size
        }

        void doFunkySpell()
        {
            spellStack = ""; // resets stack on casting
        }
        void generateSpell()
        {
            // w = earth, a = air, s = fire, d = lazer
            switch (spellStack[0])
                {
                    case 'w':
                        for (int I = 0; I < stackSize; I++)
                        {
                            if (spellStack[I] == 'w')
                            {
                                GameObject projectile = Instantiate(projectileW,
                                    transform.position + new Vector3(0, 1, 0),
                                    transform.rotation); // spawns the rock projectiles
                                projectile.GetComponent<Rigidbody>().AddRelativeForce(new Vector3
                                    (0, 0, 500f));
                            }

                            if (spellStack[I] == 'a')
                            {
                                GameObject projectile = Instantiate(projectileA,
                                    transform.position + new Vector3(0, 1, 0), transform.rotation);
                                projectile.GetComponent<Rigidbody>().AddRelativeForce(new Vector3
                                    (0, 0, 500f));
                            }

                            if (spellStack[I] == 's')
                            {
                                GameObject projectile = Instantiate(projectileS,
                                    transform.position + new Vector3(0, 1, 0), transform.rotation);
                                projectile.GetComponent<Rigidbody>().AddRelativeForce(new Vector3
                                    (0, 0, 500f));
                            }

                            if (spellStack[I] == 'd')
                            {
                                GameObject projectile = Instantiate(projectileD,
                                    transform.position + new Vector3(0, 1, 0), transform.rotation);
                                projectile.GetComponent<Rigidbody>().AddRelativeForce(new Vector3
                                    (0, 0, 500f));
                            }
                        }

                        break;
                    case 'a':
                        for (int I = 0; I < stackSize; I++)
                        {
                            if (spellStack[I] == 'w')
                            {
                                GameObject projectile = Instantiate(projectileW,
                                    transform.position + new Vector3(0, 1, 0),
                                    transform.rotation); // spawns the rock projectiles
                                projectile.GetComponent<Rigidbody>().AddRelativeForce(new Vector3
                                    (0, 0, 5000f));
                            }

                            if (spellStack[I] == 'a')
                            {
                                GameObject projectile = Instantiate(projectileA,
                                    transform.position + new Vector3(0, 1, 0), transform.rotation);
                                projectile.GetComponent<Rigidbody>().AddRelativeForce(new Vector3
                                    (0, 0, 5000f));
                            }

                            if (spellStack[I] == 's')
                            {
                                GameObject projectile = Instantiate(projectileS,
                                    transform.position + new Vector3(0, 1, 0), transform.rotation);
                                projectile.GetComponent<Rigidbody>().AddRelativeForce(new Vector3
                                    (0, 0, 5000f));
                            }

                            if (spellStack[I] == 'd')
                            {
                                GameObject projectile = Instantiate(projectileD,
                                    transform.position + new Vector3(0, 1, 0), transform.rotation);
                                projectile.GetComponent<Rigidbody>().AddRelativeForce(new Vector3
                                    (0, 0, 5000f));
                            }
                        }

                        break;
                    case 's':
                        for (int I = 0; I < stackSize; I++)
                        {
                            if (spellStack[I] == 'w')
                            {
                                GameObject spray = Instantiate(sprayW, transform.position,
                                    transform.rotation); // spawns the spray attacks
                                spray.transform.SetParent(gameObject.transform);
                            }

                            if (spellStack[I] == 'a')
                            {
                                GameObject spray = Instantiate(sprayA, transform.position, transform.rotation);
                                spray.transform.SetParent(gameObject.transform);
                            }

                            if (spellStack[I] == 's')
                            {
                                GameObject spray = Instantiate(sprayS, transform.position, transform.rotation);
                                spray.transform.SetParent(gameObject.transform);
                            }

                            if (spellStack[I] == 'd')
                            {
                                GameObject spray = Instantiate(sprayD, transform.position, transform.rotation);
                                spray.transform.SetParent(gameObject.transform);
                            }
                        }

                        break;
                    case 'd':

                        for (int I = 0; I < stackSize; I++)
                        {
                            if (spellStack[I] == 'w')
                            {
                                GameObject lazer = Instantiate(lazerW, transform.position + new Vector3(0, 1, 0),
                                    transform.rotation);
                                lazer.transform.SetParent(gameObject.transform);
                            }

                            if (spellStack[I] == 'a')
                            {
                                GameObject lazer = Instantiate(lazerA, transform.position + new Vector3(0, 1, 0),
                                    transform.rotation);
                                lazer.transform.SetParent(gameObject.transform);
                            }

                            if (spellStack[I] == 's')
                            {
                                GameObject lazer = Instantiate(lazerS, transform.position + new Vector3(0, 1, 0),
                                    transform.rotation);
                                lazer.transform.SetParent(gameObject.transform);
                            }

                            if (spellStack[I] == 'd')
                            {
                                GameObject lazer = Instantiate(lazerD, transform.position + new Vector3(0, 1, 0),
                                    transform.rotation);
                                lazer.transform.SetParent(gameObject.transform);
                            }
                        }

                break;
                }
            spellStack = ""; // resets stack on casting
        }
    }
}
