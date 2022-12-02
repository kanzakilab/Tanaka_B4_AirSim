using System;
using System.Collections.Generic;
using AirSimUnity.DroneStructs;
using UnityEngine;

namespace AirSimUnity
{
    [RequireComponent(typeof(Drone))]
    public class MoveRightHorizontaly : MonoBehaviour
    {
        private Drone drone;
        private float height;
        private float inclination;
        private float target = 20.0f;

        // Start is called before the first frame update
        void Start()
        {
            drone = this.gameObject.GetComponent<Drone>();
            inclination = 0.7f;
        }

        void FixedUpdate()
        {
            height = this.gameObject.transform.position.y;
            if((height - target) < -5.0f)
            {
                inclination -= 0.02f;
            }else if((height - target) > 1.0f) { 
                inclination += 0.02f;
            }
            if (inclination < 0.0f)
            {
                inclination = 0.0f;
            }else if (inclination > 1.0f) {
                inclination = 1.0f;
            }
            if (Math.Abs(target - height) < 1.5f)
            {
                Debug.Log(inclination);
            }
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

