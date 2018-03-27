using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FutureRobot.FuroWare.Common.Utils
{
    /// <summary>
    /// 공용 설정
    /// 2013.11.13_shkwak 
    /// </summary>
    public class JoystickSetting
    {
        public JoyFuncSetting JoyFunc { set; get; }
        public JoyButtonSetting JoyButton { set; get; }
        public JoySingleButtonSetting JoySingleButton { set; get; }
        public JoyControlMappingValue JoyControlValue { set; get; }

        public JoystickSetting()
        {
            JoyFunc         = new JoyFuncSetting();
            JoyButton       = new JoyButtonSetting();
            JoySingleButton = new JoySingleButtonSetting();
            JoyControlValue = new JoyControlMappingValue();
        }

        public void Save(string path)
        {
            XmlSerializer xs = new XmlSerializer(typeof(JoystickSetting));
            StreamWriter sw = new StreamWriter(path);
            xs.Serialize(sw, this);
            sw.Close();
        }

        public static JoystickSetting Load(string path)
        {
            XmlSerializer xs = new XmlSerializer(typeof(JoystickSetting));
            StreamReader sr = new StreamReader(path);

            JoystickSetting result = xs.Deserialize(sr) as JoystickSetting;
            sr.Close();
            return result;
        }
    }

    public class JoyFuncSetting
    {
        public string Func1;
        public string Func2;
        public string Func3;
        public string Func4;
        public string Func5;
        public string Func6;
        public string Func7;
        public string Func8;
        public string Func9;
        public string Func10;

        public JoyFuncSetting()
        {

        }
    }

    public class JoyButtonSetting
    {
        //public List<string> Mix = new List<string>();
        public string Mix1;
        public string Mix2;
        public string Mix3;
        public string Mix4;
        public string Mix5;
        public string Mix6;
        public string Mix7;
        public string Mix8;
        public string Mix9;
        public string Mix10;

        public JoyButtonSetting()
        {
            //Mix.Add
        }
    }

    public class JoySingleButtonSetting
    {
        public string SingleBtn0;
        public string SingleBtn1;
        public string SingleBtn2;
        public string SingleBtn3;
        public string SingleBtn4;
        public string SingleBtn5;
        public string SingleBtn6;
        public string SingleBtn7;
        public string SingleBtn8;
        public string SingleBtn9;        

        public JoySingleButtonSetting()
        {

        }
    }

    public class JoyControlMappingValue
    {
        public string Value1;
        public string Value2;
        public string Value3;
        public string Value4;
        public string Value5;
        public string Value6;
        public string Value7;
        public string Value8;
        public string Value9;
        public string Value10;

        public JoyControlMappingValue()
        {

        }
    }

    //////////////////////////////////////////////////

    /// <summary>
    /// 공용 설정
    /// 2013.10.31 by Mini
    /// </summary>
    public class RobotSetting
    {
        public MechanicalSetting Mechanic { set; get; }
        public DrivingSetting Driving { set; get; }
        public WallFollowingSetting WallFollowing { set; get; }
        public SensorSetting Sensor { set; get; }
        public StarList Stars { set; get; }

        public RobotSetting()
        {
            Mechanic = new MechanicalSetting();
            Driving = new DrivingSetting();
            WallFollowing = new WallFollowingSetting();
            Sensor = new SensorSetting();
            Stars = new StarList();
        }

        public void Save(string path)
        {
            XmlSerializer xs = new XmlSerializer(typeof(RobotSetting));
            StreamWriter sw = new StreamWriter(path);
            xs.Serialize(sw, this);
            sw.Close();
        }

        public static RobotSetting Load(string path)
        {
            XmlSerializer xs = new XmlSerializer(typeof(RobotSetting));
            StreamReader sr = new StreamReader(path);

            RobotSetting result = xs.Deserialize(sr) as RobotSetting;
            sr.Close();
            return result;
        }
    }

    /// <summary>
    /// 기구 설정
    /// 2013.10.31 by Mini
    /// </summary>
    public class MechanicalSetting
    {
        public double WheelBase { set; get; }
        public double WheelDiameter { set; get; }
        public int LeftReverse { set; get; }
        public int RightReverse { set; get; }
        public int MotorPulse { set; get; }
        public int MotorMultiplier { set; get; }
        public int GearRatio { set; get; }

        public RangeSensorList RangeSensors { set; get; }

        public MechanicalSetting()
        {
        }
    }

    /// <summary>
    /// Wall Following 설정
    /// 2013.10.31 by Mini
    /// </summary>
    public class WallFollowingSetting
    {
        public double TurnningDistance { set; get; }        // 벽 근접에 따른 감속 시작 거리
        public double TurnningStopDistance { set; get; }    // 벽 근접에 따른 접근 제한 거리

        public double WallDistance { set; get; }            // 로봇과 벽간 거리 설정
        public double DrivingVelocity { set; get; }         // 주행 속도 설정

        public int sensorNo_Rear = 0;          // 주행방향 센서 번호
        public int sensorNo_RearSide = 0;      // 주행방향 측면 센서 번호
        public int sensorNo_Side = 0;               // 측면 센서 번호
        public int sensorNo_FrontSide = 0;    // 주행반대방향 측면 센서 번호

        public bool RotateInverse { set; get; }     // 좌/우수에 따른 회전 방향 
        public bool DriveFoword { set; get; }    // 전진 기준/후진기준

        public IIRSetting PathPlanGain { set; get; }        // 경로 계획 gain
        public IIRSetting RouteObeyingGain { set; get; }    // 경로 추종 gain


        public WallFollowingSetting()
        {
            TurnningDistance = 1;
            TurnningStopDistance = 0.5;
            WallDistance = 0.6;
            DrivingVelocity = -0.1;

            PathPlanGain = new IIRSetting();
            PathPlanGain.PGain = 50;
            PathPlanGain.OutputAdjustment = -1;

            RouteObeyingGain = new IIRSetting();
            RouteObeyingGain.PGain = 10;
            RouteObeyingGain.OutputAdjustment = -1;

            RotateInverse = false;
            DriveFoword = false;

            // 후진 우수법 설정
            //sensorNo_Direction = 5;
            //sensorNo_DirectionSide = 6;
            //sensorNo_Side = 8;
            //sensorNo_IndirectionSide = 9;

            // 후진 좌수법 설정
            sensorNo_Rear = 5;
            sensorNo_RearSide = 4;
            sensorNo_Side = 2;
            sensorNo_FrontSide = 1;
        }
    }

    /// <summary>
    /// 주행 설정
    /// 2013.10.31 by Mini
    /// </summary>
    public class DrivingSetting
    {
        public double ProximityLimitRange { set; get; } // 근접 거리 제한

        public double MaxLinearVelocity { set; get; }   // 최대 선속도
        public double MinLinearVelocity { set; get; }   // 최대 선속도
        public double LinearAcceleration { set; get; }  // 선가속도

        public double MaxAngularVelocity { set; get; }  // 최대 각속도
        public double MinAngularVelocity { set; get; }  // 최대 각속도
        public double AngularAcceleration { set; get; } // 각가속도

        public DrivingSetting()
        {
            ProximityLimitRange = 0.3;

            MaxLinearVelocity = 0.2;
            MinLinearVelocity = -0.2;
            LinearAcceleration = 0.02;

            MaxAngularVelocity = 10;
            MinLinearVelocity = -10;
            AngularAcceleration = 1;
        }
    }

    /// <summary>
    /// IIR Filter 설정
    /// 2013.10.31 by Mini
    /// </summary>
    public class IIRSetting
    {
        public double PGain { set; get; }
        public double IGain { set; get; }
        public double DGain { set; get; }
        public double OutputAdjustment { set; get; }

        public bool NoINT { set; get; }
        public bool NoDER { set; get; }

        public IIRSetting()
        {
            NoINT = true;
            NoDER = true;

            PGain = 200;
        }
    }

    public class SensorSetting
    {
        public double UltrasonicThreshhold { set; get; }

        public SensorSetting()
        {
            UltrasonicThreshhold = 0.1;
        }
    }

    
    public class StarInfo
    {
        public string name;
        public int starnumber;

        public string Name { get { return name; } }
        public int StarNumber { get { return starnumber; } }

        public StarInfo()
        {
            name = "";
            starnumber = 0;
        }

        public StarInfo(int num, int star)
            : this()
        {
            name = num.ToString();
            starnumber = star;
        }

        public StarInfo(string name, int star)
            : this()
        {
            this.name = name;
            this.starnumber = star;
        }
    }

    public class StarList : List<StarInfo>
    {
        public void Add(string name, int star)
        {
            this.Add(new StarInfo(name, star));
        }

        public string GetName(int id)
        {
            foreach (StarInfo si in this)
            {
                if (si.StarNumber == id)
                    return si.Name;
            }
            return "";
        }

        public bool IsExistID(int id)
        {
            foreach (StarInfo si in this)
                if (si.StarNumber == id) return true;
            return false;
        }

        public void Save(string path)
        {
            XmlSerializer xs = new XmlSerializer(typeof(StarList));
            StreamWriter sw = new StreamWriter(path);
            xs.Serialize(sw, this);
            sw.Close();
        }

        public static StarList Load(string path)
        {
            XmlSerializer xs = new XmlSerializer(typeof(StarList));
            StreamReader sr = new StreamReader(path);

            StarList result = xs.Deserialize(sr) as StarList;
            sr.Close();
            return result;
        }
    }
}
