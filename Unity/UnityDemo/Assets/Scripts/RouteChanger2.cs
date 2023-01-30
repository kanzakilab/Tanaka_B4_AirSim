using System.Collections;
using System.Collections.Generic;
using AirSimUnity.DroneStructs;
using UnityEngine;
namespace AirSimUnity
{
    [RequireComponent(typeof(Drone))]
    public class RouteChanger2 : MonoBehaviour
    {
        private Drone drone;
        [SerializeField] private GameObject OtherDrone;
        private float inclination1;
        private float inclination2;
        private Vector3 pos1;
        private Vector3 pos2;
        private float dis;
        [SerializeField] private int direction;
        [SerializeField] private float power;
        RouteChanger2 otherscript;
        private int recieved = 0;

        // Start is called before the first frame update
        void Start()
        {
            drone = this.gameObject.GetComponent<Drone>();
            otherscript = OtherDrone.GetComponent<RouteChanger2>();
            inclination1 = 0.6f * direction;
            inclination2 = 0.0f;
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            pos1 = this.gameObject.transform.position;
            pos2 = OtherDrone.transform.position;
            dis = Vector3.Distance(pos1, pos2);
            if (dis <= 50.0f)
            {
                otherscript.connect();
            }
            if (recieved >= 240)
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
            recieved++;
            otherscript.ack();
        }

        void ack()
        {
            recieved++;
        }
    }
}