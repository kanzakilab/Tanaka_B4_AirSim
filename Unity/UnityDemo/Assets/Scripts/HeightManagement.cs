using System.Collections;
using System.Collections.Generic;
using AirSimUnity.DroneStructs;
using UnityEngine;

namespace AirSimUnity
{
    [RequireComponent(typeof(Drone))]
    public class HeightManagement : MonoBehaviour
    {
        private Drone drone;
        private float inclination;
        private float height;
        private float currentheight = 50.0f;
        private bool flag = true;
        private float target = 20.0f;
        [SerializeField] private float margin1 = 3.2f;

        // Start is called before the first frame update
        void Start()
        {
            drone = this.gameObject.GetComponent<Drone>();
            inclination = 0.65f;
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            height = this.gameObject.transform.position.y;
            if (flag)
            {
                if ((height - target) > margin1)
                {
                    inclination = 1.0f;
                }
                else if ((currentheight - target) > margin1)
                {
                    inclination = 0.0f;
                }
                if ((currentheight < target) && (height > target))
                {
                    flag = false;
                }
            }
            else
            {
                if (height < target)
                {
                    inclination = 0.5f;
                }
                else
                {
                    inclination = 0.7f;
                }
            }
            currentheight = height;
        }

        private void LateUpdate()
        {
            if (drone.ControllerAllowed())
            {
                AirSimRCData rcData = drone.GetRCData();

                rcData.is_valid = true;
                rcData.roll = inclination; //左右
                rcData.pitch = 0; //前後
                rcData.throttle = 1.0f; //上下
                rcData.yaw = 0; //回転
                rcData.left_z = 0;
                rcData.right_z = 0;

                drone.SetRCData(rcData);
            }
        }
    }
}
