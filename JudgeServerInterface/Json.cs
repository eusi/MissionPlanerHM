﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Web.Script.Serialization;

namespace JudgeServerInterface
{
    public class JsonServerInfo
    {
        public String message;
        public String message_timestamp;
    }

    public class JsonServerMessage
    {
        public JsonServerInfo server_info;
        public string server_time;
    }

    public class JsonObstacle
    {
        public String latitude;
        public String longitude;
    }

    public class JsonStationaryObstacle : JsonObstacle
    {
        public String cylinder_radius;
        public String cylinder_height;
    }

    public class JsonMovingObstacle : JsonObstacle
    {
        public String altitude_msl;
        public String sphere_radius;
    }

    public class JsonObstaclesMessage
    {
        public List<JsonStationaryObstacle> stationary_obstacles;
        public List<JsonMovingObstacle> moving_obstacles;
    }

    public static class JsonDeserializer
    {
        private static T Deserialize<T>(String json) where T : new()
        {
            T obj = new T();

            JavaScriptSerializer jss = new JavaScriptSerializer();

            return jss.Deserialize<T>(json);
        }

        public static ServerInfo GetServerInfo(String json) {
            ServerInfo si = new ServerInfo();

            JsonServerMessage jsm = JsonDeserializer.Deserialize<JsonServerMessage>(json);

            si.ServerMessage = jsm.server_info.message;
            si.MessageTimeStamp = jsm.server_info.message_timestamp;
            si.ServerTime = jsm.server_time;

            return si;
        }

        public static Obstacles GetObstacles(String json) {
            Obstacles obs = new Obstacles();

            JsonObstaclesMessage jom = JsonDeserializer.Deserialize<JsonObstaclesMessage>(json);

            CultureInfo culture = new CultureInfo("en-US");

            foreach(JsonStationaryObstacle jso in jom.stationary_obstacles) {
                StationaryObstacle so = new StationaryObstacle();
                so.CylinderHeight = Convert.ToDouble(jso.cylinder_height, culture);
                so.CylinderRadius = Convert.ToDouble(jso.cylinder_radius, culture);
                so.Latitude = Convert.ToDouble(jso.latitude, culture);
                so.Longitude = Convert.ToDouble(jso.longitude, culture);
                obs.StationaryObstacles.Add(so);
            }

            foreach (JsonMovingObstacle jmo in jom.moving_obstacles) {
                MovingObstacle mo = new MovingObstacle();
                mo.Altitude = Convert.ToDouble(jmo.altitude_msl, culture);
                mo.Latitude = Convert.ToDouble(jmo.latitude, culture);
                mo.Longitude = Convert.ToDouble(jmo.longitude, culture);
                mo.SphereRadius = Convert.ToDouble(jmo.sphere_radius, culture);
            }

            return obs;
        }
    }
}
