using System;
using System.Collections.Generic;
using AirSimUnity.DroneStructs;
using UnityEngine;

namespace AirSimUnity
{
    [RequireComponent(typeof(Drone))]
    public class DroneControllerInput : MonoBehaviour
    {
        private Drone drone;
        [SerializeField] private string number;

        private void Start()
        {
            drone = this.gameObject.GetComponent<Drone>();
        }
        private void LateUpdate()
        {
            if (drone.ControllerAllowed())
            {
                AirSimRCData rcData = drone.GetRCData();

                rcData.is_valid = true;
                rcData.roll = Input.GetAxis("Horizontal" + number); //左右
                rcData.pitch = Input.GetAxis("Vertical" + number); //前後
                rcData.throttle = Input.GetAxis("Depth" + number); //上下
                rcData.yaw = Input.GetAxis("Yaw" + number); //回転
                rcData.left_z = 0;
                rcData.right_z = 0;

                drone.SetRCData(rcData);
            }
        }
    }
}
