using System;
using System.Collections.Generic;
using AirSimUnity.DroneStructs;
using UnityEngine;

namespace AirSimUnity
{
    [RequireComponent(typeof(Drone))]
    public class RouteChanger : MonoBehaviour
    {
        private Drone drone;
        [SerializeField] private GameObject OtherDrone;
        private Vector3 pos1;
        private Vector3 pos2;
        private float dis;
        private bool flag = false;
        private int count = 0;
        private float inclination;
        RouteChanger script;

        private void Start()
        {
            drone = this.gameObject.GetComponent<Drone>();
            script = OtherDrone.GetComponent<RouteChanger>();
            inclination = 0.66f;
        }
        // Update is called once per frame
        void Update()
        {
            pos1 = this.gameObject.transform.position;
            pos2 = OtherDrone.transform.position;
            dis = Vector3.Distance(pos1, pos2);
            if (dis < 35.0f)
            {
                script.connection();
                inclination = 0.0f;
                if (count > 120)
                {
                    script.recieved();
                }
            }
            if (flag)
            {
                inclination = -0.66f;
            }
        }

        private void connection()
        {
            count++;
        }

        private void recieved()
        {
            flag = true;
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
//傾きの符号
//交互に送信するよう変更
