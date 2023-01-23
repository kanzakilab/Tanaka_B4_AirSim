using System.Collections;
using System.Collections.Generic;
using AirSimUnity.DroneStructs;
using UnityEngine;
namespace AirSimUnity
{
    [RequireComponent(typeof(Drone))]
    public class RouteChanger1 : MonoBehaviour
    {
        private Drone drone;
        [SerializeField] private GameObject OtherDrone;
        private float inclination1;
        private float inclination2;
        private Vector3 pos1;
        private Vector3 pos2;
        private float dis;
        private int framecount = 0;
        private bool mode_flag = true;
        [SerializeField] private int ap_time;
        [SerializeField] private int sta_time;
        [SerializeField] private int direction;
        [SerializeField] private float power;
        RouteChanger1 otherscript;
        private int recieved = 0;

        // Start is called before the first frame update
        void Start()
        {
            drone = this.gameObject.GetComponent<Drone>();
            otherscript = OtherDrone.GetComponent<RouteChanger1>();
            inclination1 = 0.5f * direction;
            inclination2 = 0.0f;
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            framecount++;
            if (mode_flag)
            {
                if (framecount == ap_time * 60)
                {
                    mode_flag = false;
                    framecount = 0;
                }
            }
            else
            {
                if(framecount == sta_time * 60)
                {
                    mode_flag = true;
                    framecount = 0;
                }
            }
            //if(recieved >= 60)
            //{
            //    mode_flag = true;
            //}else if(ack_count >= 60)
            //{
            //    mode_flag = false;
            //}
            pos1 = this.gameObject.transform.position;
            pos2 = OtherDrone.transform.position;
            dis = Vector3.Distance(pos1, pos2);
            if (dis <= 100.0f)
            {
                if (mode_flag)
                {
                    otherscript.connect();
                }
            }
            //if ((recieved > 0) || (ack_count > 0))
            //{
            //    inclination = -0.3f * direction;
            //}
            if (recieved >= 180)
            {
                inclination1 = -0.5f * direction;
                inclination2 = -0.5f * direction;
                power = 1.0f;
            }
            Debug.Log(direction * recieved);
        }

        private void LateUpdate()
        {
            if (drone.ControllerAllowed())
            {
                AirSimRCData rcData = drone.GetRCData();

                rcData.is_valid = true;
                rcData.roll = inclination1; //左右
                rcData.pitch = inclination2; //前後
                rcData.throttle = power; //上下
                rcData.yaw = 0; //回転
                rcData.left_z = 0;
                rcData.right_z = 0;

                drone.SetRCData(rcData);
            }
        }

        void connect()
        {
            if (!mode_flag)
            {
                recieved++;
                otherscript.ack();
            }
        }

        void ack()
        {
            recieved++;
        }
    }
}