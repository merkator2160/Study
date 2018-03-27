using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FutureRobot.FuroWare.Common.Utils
{
    public class Pose
    {
        public double x, y, theta;

        public Pose()
        {
            x = 0; y = 0; theta = 0;
        }

        public Pose(double x, double y, double theta)
        {
            this.x = x; this.y = y; this.theta = theta;
        }
    }

    public class Position
    {
        public double x, y, z;

        public Position()
        {
            x = 0; y = 0; z = 0;
        }

        public Position(double x, double y, double z)
        {
            this.x = x; this.y = y; this.z = z;
        }

        public bool Compare(Position pos)
        {
            return this.x == pos.x && this.y == pos.y && this.z == pos.z;
        }
    }

    public class StarGazerPosition
    {
        public int id;
        public double angle, x, y, z;

        public StarGazerPosition()
        {
            id = 0; angle = 0; x = 0; y = 0; z = 0;
        }

        public StarGazerPosition(int id, double angle, double x, double y, double z)
        {
            this.id = id; this.angle = angle; this.x = x; this.y = y; this.z = z;
        }
    }

    public class BoardStatus
    {
        public int magneticLine { get; set; }
        public int batteryLevel { get; set; }
        public int chargingStatus { get; set; }
        public int bumperStatus { get; set; }
    }

    public class WheelEncoder
    {
        public int leftValue { get; set; }
        public int rightValue { get; set; }
        public int waistValue { get; set; }
    }
    /// <summary>
    /// Body Axis 1: head yaw, 2 : pitch 3: roll
    /// </summary>
    public class BodyPose
    {
        public UInt16 headYaw { get; set; }
        public UInt16 headPitch { get; set; }
        public UInt16 headRoll { get; set; }
        public UInt16 wristPitch { get; set; }
        public UInt16 waistPitch { get; set; }
    }
    public class BodyPoseLimit
    {
        public UInt16[] headYaw = new UInt16[3];
        public UInt16[] headPitch = new UInt16[3];
        public UInt16[] headRoll = new UInt16[3];
        public UInt16[] wristPitch = new UInt16[3];
        public UInt16[] waistPitch = new UInt16[3];
    }
    public class BodyPoseGain
    {
        public UInt32[] headYaw = new UInt32[3];
        public UInt32[] headPitch = new UInt32[3];
        public UInt32[] headRoll = new UInt32[3];
        public UInt32[] wristPitch = new UInt32[3];
        public UInt32[] waistPitch = new UInt32[3];
    }

    public class BoardInfo
    {
        public int[] version = new int[3];
        public int[] operatingState = new int[4];
        public int homeButton;
    }

    public class ProtocolDef
    {
        public class ID
        {
            // Axis ID
            public const byte HEAD_YAW = 0x27;
            public const byte HEAD_PITCH = 0x28;
            public const byte HEAD_ROLL = 0x29;
            public const byte WAIST_PITCH = 0x2B;
            public const byte WRIST_PITCH = 0x2D;
            public const byte MOBILE_LEFT = 0x2E;
            public const byte MOBILE_RIGHT = 0x2F;
            // Board ID
            public const byte MOBILEBOARD = 0x2E;
            public const byte BODYBOARD = 0x2F;
            public const byte BROADCAST = 0xFE;
        }

        public class LENGTH
        {
            public const byte MOBILE_ENCODER_MODE = 0x08;
            public const byte MOBILE_POSITION_MODE = 0x08;
            public const byte BODY_POSITION = 0x0F;
            public const byte BODY_GAIN = 0x14;
            public const byte BODY_LIMIT = 0x0E;
            public const byte BODY_GET_GAIN = 0x14;
            public const byte BODY_POSITION_MODE = 0x08;
            public const byte BODY_AXIS1 = 0x0D;
            public const byte BODY_AXIS2 = 0x12;
            public const byte BODY_AXIS3 = 0x17;
            public const byte BODY_AXIS5 = 0x21;
            public const byte BODY_AXIS_DATA = 0x04;
            public const byte MOBILE_PACKET = 0x12;
            public const byte BOARD_PACKET = 0x0B;
            public const byte ODOMETRY_PACKET = 0x13;
            public const byte MOBILE_ENCODER_PACKET = 0x0F;
            public const byte QUERY = 0x07;
            public const byte REQUEST_LIMIT = 0x08;
            public const byte VERSION = 0x0A;
            public const byte OPERATING_STATE = 0x0B;
            public const byte HOME_BUTTON = 0x08;
            public const byte ZOBE_BOARD_MODE = 0x08;
            public const byte ACCELATION_SET = 0x0F;
        }

        public class COMMAND
        {
            public const byte PING = 0x01;
            public const byte GET = 0x02;
            public const byte SET = 0x03;
            public const byte READY = 0x04;
            public const byte ACT = 0x05;
            public const byte SYNC = 0x06;
            public const byte EVENT = 0x07;
        }

        public class FUNCTION
        {
            public const byte DEV_INFO = 0x00;
            public const byte MOBILE_ODOMETRY = 0xA0;
            public const byte OPERATION_STATE = 0xA4;
            public const byte ZOBE_BOARD_MODE = 0xAA;
            public const byte BODY_POSITON = 0xB0;
            public const byte BODY_LIMIT = 0xB1;
            public const byte BODY_GAIN = 0xB2;
            public const byte BODY_AUTO_HOME = 0xB3;
            public const byte HOME_BUTTON = 0xA5;
            public const byte ACCELATION_SET = 0xAB;
            
        }

        public class DXL
        {
            public const byte PREAMBLE = 0xFF;
            public const byte DXC_SYNC_WRITE = 0x83;
            public const byte DXI_GOAL_POSITION_L = 0x1E;
            public const byte DXI_MOVING_SPEED_L = 0x20;
            public const byte DXI_MOVING_SPEED_ACCELERATION = 0x21;
        }
    }

    public class Language
    {
        public const string CA_ES = "ca-es";
        public const string DA_DK = "da-dk";
        public const string DE_DE = "de-de";
        public const string EN_AU = "en-au";
        public const string EN_CA = "en-ca";
        public const string EN_GB = "en-gb";
        public const string EN_IN = "en-in";
        public const string EN_US = "en-us";
        public const string ES_ES = "es-es";
        public const string ES_MX = "es-mx";
        public const string FI_FI = "fi-fi";
        public const string FR_CA = "fr-ca";
        public const string FR_FR = "fr-fr";
        public const string IT_IT = "it-it";
        public const string JA_JP = "ja-jp";
        public const string KO_KR = "ko-kr";
        public const string NB_NO = "nb-no";
        public const string NL_NL = "nl-nl";
        public const string PL_PL = "pl-pl";
        public const string PT_BR = "pt-br";
        public const string PT_PT = "pt-pt";
        public const string RU_RU = "ru-ru";
        public const string SV_SE = "sv-se";
        public const string ZH_CN = "zh-cn";
        public const string ZH_HK = "zh-hk";
        public const string ZH_TW = "zh-tw";
    }

    public class AlarmType
    {
        public int id;
        public bool enable;
        public string time24;
        public string repeat_day;
        public int repeat_count;
        public string text;
    }

    public class WheelProperties
    {
        public int leftNum = 0;
        public int leftReverse = 1;
        public int rightReverse = -1;
        public int odometryUse = 5;
        public double wheelDiameter = 0.15;
        public double axleDistance = 0.333;
        public double motorPulse = 512;
        public double motorMultiplier = 4;
        public double gearRatio = 64;
    }

    public struct RECT
    {
        public int left;
        public int top;
        public int right;
        public int bottom;
    }

    public enum DrivePriority
    {
        HIGHEST,
        HIGH,
        NORMAL,
        LOW,
        LOWEST,
        STOP
    }
}
