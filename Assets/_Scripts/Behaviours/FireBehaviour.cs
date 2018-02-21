using System.Collections.Generic;
using UnityEngine;

public class FireBehaviour : MonoBehaviour {
    
    #region public variables
    public BulletBehaviour originalBullet; //Original bulletPrefab;
    public Transform muzzle; //Transform child of the player. Where the bullet begins;
    public float initialShootForce = 200;
    #endregion

    #region private variables
    [SerializeField]
    private int bulletMaxQuantity = 10; //Quantity of bullets tht will be stored at the begining
    private List<BulletBehaviour> bulletPool = new List<BulletBehaviour>(); //Bullets instantiated at the beginning

    private float cooldown = 0.3f; // Time between each shoot
    private float counter; //The cooldown counter

    private Camera cam;
    #endregion

    void Start ()
    {

        cam = GetComponentInChildren<Camera>();

        //Able the player tho shoot at the gamestart
        counter = cooldown;

        //Creates the initial amount of bullets that will be reused in game
        for (int i = 0; i < bulletMaxQuantity; i++)
        {
            bulletPool.Add(Instantiate(originalBullet, muzzle.position, muzzle.rotation));
            bulletPool[i].gameObject.SetActive(false);
        }
	}
	
    void Update()
    {
        //Gets the mouse input
        if (Input.GetAxis("Fire1")>0)
        {
            GetBullet(); //Function that returns an inactive bullet and set it active
        }

        if (Input.GetAxis("Fire2") > 0)
        {
            cam.fieldOfView = Mathf.Lerp(60, 40, Input.GetAxis("Fire2"));
        }
        else
        {
            cam.fieldOfView = 60;
        }

        //Increases the time each frame
        counter += Time.deltaTime;
    }

    void GetBullet()
    {
        if (counter > cooldown)
        {
            for (int i = 0; i < bulletPool.Count; i++)
            {
                if (!bulletPool[i].gameObject.activeSelf)
                {
                    ShootBullet(bulletPool[i]);
                    counter = 0;
                    break;
                }
            }
        }
    }

    void ShootBullet(BulletBehaviour bullet)
    {
        bullet.transform.position = muzzle.position;
        bullet.transform.rotation = muzzle.rotation;
        bullet.gameObject.SetActive(true);
        bullet.ApplyForce(bullet.transform.forward*initialShootForce);
    }

}
