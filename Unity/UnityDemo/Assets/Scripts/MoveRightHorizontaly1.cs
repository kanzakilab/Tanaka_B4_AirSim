using System;
using System.Collections.Generic;
using AirSimUnity.DroneStructs;
using UnityEngine;

namespace AirSimUnity
{
    [RequireComponent(typeof(Drone))]
    public class MoveRightHorizontaly1 : MonoBehaviour
    {
        private Drone drone;
        private float height;
        private float currentheight;
        private float inclination;
        private float target = 20.0f;
        private float margin1 = 2.0f; // 1. まずはここから出ないように
        private float di = 0.001f;

        // Start is called before the first frame update
        void Start()
        {
            drone = this.gameObject.GetComponent<Drone>();
            inclination = 0.65f;
        }

        void FixedUpdate()
        {
            height = this.gameObject.transform.position.y;
            if((height - target) > margin1)
            {
                inclination = 1.0f;
            }else if((target - height) > margin1)
            {
                inclination = 0.0f;
            }
            else
            {
                if ((currentheight - target) > margin1)
                {
                    inclination = 0.0f;
                }
                else if ((target - currentheight) > margin1)
                {
                    inclination = 1.0f;
                }
                else
                {
                    if((target < height)&&(currentheight < height))
                    {
                        inclination -= di;
                    }
                    if((height < target)&&(height < currentheight))
                    {
                        inclination += di;
                    }
                }
            }
            if (inclination < 0.0f)
            {
                inclination = 0.0f;
            }
            else if (inclination > 1.0f)
            {
                inclination = 1.0f;
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

