using GMap.NET;
using System;
using System.Collections.Generic;
using System.Text;

namespace Asterix_Decoder
{
    public partial class MapPoints
    {
        double Lat;
        double Lon;
        public double Time;
        string SIC;
        string SAC;
        string Target_ID;
        PointLatLng Point_map;
        string TrackNumber;
        public MapPoints(double Lat,double Lon, double Time,string SIC, string SAC, string Target_ID, PointLatLng point_map,string TrackNumber)
        {
            this.Lat = Lat;
            this.Lon = Lon;
            this.Time = Time;
            this.SIC = SIC;
            this.SAC = SAC;
            this.Target_ID = Target_ID;
            this.Point_map = point_map;
            this.TrackNumber = TrackNumber;

        }
        public void ADD_to_MapPoints_List(List<PointLatLng> List, PointLatLng Point)
        {
            List.Add(Point);
        }
        public void ADD_to_Points_List(List<MapPoints> List, MapPoints Point)
        {
            List.Add(Point);
        }
        public double get_time()
        {
            return this.Time;
        }
        public string get_TargetID()
        {
            return this.Target_ID;
        }
        public string get_SIC()
        {
            return this.SIC;
        }
        public string get_Track_Number()
        {
            return this.TrackNumber;
        }
        public PointLatLng get_point()
        {
            return this.Point_map;
        }
    }
}
