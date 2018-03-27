using FutureRobot.FuroWare.Common.Interfaces;
using FutureRobot.FuroWare.Common.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace FutureRobot.FuroWare.Common.Utils
{
    /// <summary>
    /// 외부 입력
    /// 2013.10.31 by Mini
    /// </summary>
    public class ExternalInputInfo
    {
        public RangeSensorList UltrasonicList { set; get; }
        public List<double> DepthList { set; get; }
        public StarGazerPosition StargazerPosition { set; get; }
        public WheelEncoder Encoder { set; get; }
        public BodyPose BodyPose { set; get; }
        public Position NearestUserTorso { set; get; }

        public ExternalInputInfo()
        {
            UltrasonicList = null;
            DepthList = new List<double>();
            StargazerPosition = null;
            Encoder = new WheelEncoder();
            BodyPose = new BodyPose();
            NearestUserTorso = null;
        }
    }

    /// <summary>
    /// 외부 출력
    /// 2013.10.31 by Mini
    /// </summary>
    public class ExternalOutputInfo
    {
        // 감정

        // 발화

        // 자세

        // 주행
        public DriveInfo driveInfo;
        public DriveInfo DriveInfo { get { return driveInfo; } }

        // 컨텐트 이벤트
        private List<RobotContentEventArgs> contentEventList;
        public List<RobotContentEventArgs> ContentEventList { get { return contentEventList; } }

        public ExternalOutputInfo()
        {
            //driveInfo = null;
            driveInfo = new DriveInfo();
            contentEventList = null;
        }

        public void SetDriveInfo(double linearVelocity, double angularVelocity)
        {
            driveInfo = new DriveInfo(linearVelocity, angularVelocity);
        }

        public void SetDriveInfo(DriveInfo drive)
        {
            driveInfo = drive;
        }

        public void StopDrive(bool stop)
        {
            driveInfo = new DriveInfo(stop);
            SetAngularVelocity(0);
            SetLinearVelocity(0);
        }

        public void SetAngularVelocity(double angularVelocity)
        {
            if (driveInfo == null)
                driveInfo = new DriveInfo();
            if (Math.Abs(angularVelocity) > 0.0 && Math.Abs(angularVelocity) < 2.0)
                angularVelocity = 2.0 * Math.Sign(angularVelocity);
            driveInfo.AngularVelocity = angularVelocity;
        }

        public void SetLinearVelocity(double linearVelocity)
        {
            if (driveInfo == null)
                driveInfo = new DriveInfo();
            driveInfo.LinearVelocity = linearVelocity;
        }

        public void AddContentEvent(RobotContentEventArgs contentEvent)
        {
            if (contentEventList == null) contentEventList = new List<RobotContentEventArgs>();
            contentEventList.Add(contentEvent);
        }
    }

    /// <summary>
    /// 거리 센서 정보
    /// 2013.10.31 by MIni
    /// </summary>
    public class RangeSensor
    {
        #region Fields
        public double xo;
        public double yo;
        public double direction;
        public double distance;
        public double sensingAngle;

        public PointF installPosition;

        public double sensorValue = 0.0;
        public double filteredValue = 0.0;
        public double cutOffValue = 0.1;
        public double undetectedSet = 2.0;
        #endregion

        #region Properties
        public double SensorX { get { return installPosition.X; } }
        public double SensorY { get { return installPosition.Y; } }

        public double Value
        {
            set
            {
                double val = value;
                //if (val < cutOffValue)
                //{
                //    val = undetectedSet;
                //}
                sensorValue = val;
                filteredValue = MathUtil.LowPassFilter(filteredValue, sensorValue);
            }
            get { return sensorValue; }
        }


        public double FilteredValue { get { return filteredValue; } }

        #endregion

        public RangeSensor()
        {
            xo = 0;
            yo = 0;
            direction = 0;
            distance = 0;
            Value = 0;
            sensingAngle = MathUtil.DEG2RAD(40);
        }

        public RangeSensor(double xorigin, double yorigin, double degreeDirection, double distance, double degreeSensingAngle, double cutoff = 0.1)
            : this()
        {
            this.xo = xorigin;
            this.yo = yorigin;
            this.direction = degreeDirection;
            this.distance = distance;
            this.sensingAngle = degreeSensingAngle;
            this.installPosition = GetPosition();
            this.cutOffValue = 0.1;
        }

        private PointF GetPosition()
        {
            PointF point = new PointF();
            point.X = (float)(xo + distance * Math.Cos(MathUtil.DEG2RAD(direction)));
            point.Y = (float)(yo + distance * Math.Sin(MathUtil.DEG2RAD(direction)));
            return point;
        }

        /// <summary>
        /// 센서 정보를 이미지로 표시하기 위한 함수
        /// 2013.10.31 by Mini
        /// </summary>
        /// <param name="bitmap"></param>
        /// <param name="meterPerPixel"> Pixel Per Meter </param>
        /// <param name="robotX"> 로봇 X 좌표 </param>
        /// <param name="robotY"> 로봇 Y 좌표</param>
        /// <param name="sensedRange"> 센싱된 거리 </param>
        public void MappingToBitmap(ref Bitmap bitmap, Brush brush, double meterPerPixel, double robotX, double robotY, double robotDirection, double sensedRange, int drawX, int drawY, string text = "")
        {
            if (sensedRange == 0) return;

            Graphics g = Graphics.FromImage(bitmap);

            // 회전 변환
            double rotatedX = 0.0;
            double rotatedY = 0.0;
            MathUtil.Rotation(installPosition.X, installPosition.Y, robotDirection, out rotatedX, out rotatedY);

            Point scaledPosition = new Point();
            scaledPosition.X = (int)((rotatedX + robotX) / meterPerPixel);
            scaledPosition.Y = (int)((rotatedY + robotY) / meterPerPixel);

            float x, y, width, height, startAngle, sweepAngle;
            x = (float)(scaledPosition.X - sensedRange / meterPerPixel / 2) + drawX;
            y = (float)(scaledPosition.Y - sensedRange / meterPerPixel / 2) + drawY;

            width = (float)(sensedRange / meterPerPixel);
            height = (float)(sensedRange / meterPerPixel);
            startAngle = (float)(direction + robotDirection - sensingAngle / 2);
            sweepAngle = (float)sensingAngle;

            //Trace.WriteLine(string.Format("{0},{1},{2},{3},{4},{5}",x, y, width, height, startAngle, sweepAngle));

            g.FillPie(brush, x, y, width, height, startAngle, sweepAngle);

            g.DrawString(text, new Font("Arial", 10), new SolidBrush(Color.Black), x + width / 2, y + height / 2);
        }

        public void MappingToBitmap(ref Bitmap bitmap, Brush brush, double meterPerPixel, double robotX, double robotY, double robotDirection, int drawX, int drawY, string text = "")
        {
            MappingToBitmap(ref bitmap, brush, meterPerPixel, robotX, robotY, robotDirection, Value, drawX, drawY, text);
        }
    }

    /// <summary>
    /// 거리 센서 정보 목록
    /// 2013.10.31 by Mini
    /// </summary>
    public class RangeSensorList : List<RangeSensor>
    {
        public RangeSensorList()
        {

        }

        public bool SetValues(List<double> values)
        {
            if (values.Count != this.Count) return false;

            for (int i = 0; i < this.Count; i++)
            {
                this[i].Value = values[i];
            }
            return true;
        }
    }

    /// <summary>
    /// 주행 명령 정보
    /// 2013.10.31 by Mini
    /// </summary>
    public class DriveInfo
    {
        public double LinearVelocity { set; get; }
        public double AngularVelocity { set; get; }

        private bool stop = false;
        public bool Stop
        {
            set
            {
                stop = value;
                if (stop == true)
                {
                    LinearVelocity = 0;
                    AngularVelocity = 0;
                }
            }
            get
            {
                return stop;
            }
        }

        public DriveInfo()
        {
            LinearVelocity = 0.0;
            AngularVelocity = 0.0;
            stop = false;
        }

        public DriveInfo(double linearVelocity, double angularVelocity, bool stop = false)
            : this()
        {
            LinearVelocity = linearVelocity;
            AngularVelocity = angularVelocity;
            Stop = stop;
        }

        public DriveInfo(bool stop)
            : this()
        {
            Stop = stop;
        }
    }

    public class WallFollowingData
    {
        public int ControlCount;

        public RangeSensorList RangeSensorList;
        public WallFollowingSetting WallFollowingSetting;

        public double AngleFromWallDirectionSide;
        public double AngleFromWallIndirectionSide;
        public double AngleFromWall;

        public double DistanceFromWall;
    };

    /// <summary>
    /// 로봇 상태 정보
    /// 2013.11.19 by Mini
    /// </summary>
    public class RobotStatus
    {
        private double direction;

        // 위치 관련 상태
        /// <summary>
        /// meter
        /// </summary>
        public double XPosition { set; get; }   // X좌표

        /// <summary>
        /// meter
        /// </summary>
        public double YPosition { set; get; }   // Y좌표

        /// <summary>
        /// rad
        /// </summary>
        public double Direction    // 방향
        {
            set
            {
                direction = value;
                direction = direction % (Math.PI * 2);
            }
            get { return direction; }
        }

        public int AreaID { set; get; } // 현재 영역의 스타게이저 id. 

        public int DetectedStargazerID { set; get; } // 스타게이저에서 들어온 모든 id 

        // 주행 관련 상태
        public double LinearVelocity { set; get; }  // m/sec
        public double AngularVelocity { set; get; } // rad/sec

        private int leftEncoderCount;
        public int LeftEncoderCount 
        {
            set
            {
                OldLeftEncoderCount = leftEncoderCount;
                leftEncoderCount = value;
            }
            get { return leftEncoderCount; }
        }     // 엔코더 기준값

        private int rightEncoderCount;
        public int RightEncoderCount 
        {
            set
            {
                OldRightEncoderCount = rightEncoderCount;
                rightEncoderCount = value;
            }
            get { return rightEncoderCount; }
        }    // 엔코더 기준값

        public int OldLeftEncoderCount { set; get; }     // 이전 엔코더 기준값
        public int OldRightEncoderCount { set; get; }    // 이전 엔코더 기준값

        public DateTime OdometryTime { set; get; }      // 측정 시각

        public PathNodeInfo DatumPoint { set; get; }   // 기준점
        public PathNodeInfo TargetPoint { set; get; }   // 구간 목표점
        public PathNodeInfo Destination { set; get; }   // 네비게이션 목표점

        // 외부 입력
        public ExternalInputInfo Inputs { set; get; }

        // 초기화 여부
        public bool IsInitialized { set; get; }

        public RobotStatus()
        {
            IsInitialized = false;
            XPosition = 0;
            YPosition = 0;
            Direction = 0;
            LeftEncoderCount = 0;
            RightEncoderCount = 0;
            DatumPoint = new PathNodeInfo(0, 0, 0, "Start Point");
            AreaID = 0;
            TargetPoint = null;
            Destination = null;
        }
    }


    /// <summary>
    /// 노드 유형
    /// Absolute : 절대 위치. 원점, 스타게이저 지점
    /// Relative : 상대 위치. 데드레코닝 연산 지점
    /// 2013.11.19 by Mini
    /// </summary>
    public enum PathNodeType { Absolute, Relative }

    /// <summary>
    /// 노드 정보.
    /// 2013.11.19 by Mini
    /// </summary>
    public class PathNodeInfo
    {
        private string name;
        private PathNodeType type;
        private PathNodeInfo basePlace;
        private int landmarkID;
        private double position_x;
        private double position_y;
        private double direction;
        private double distance;
        private Dictionary<PathNodeInfo, ChannelInfo> channels;


        public string Name { get { return name; } }
        public PathNodeType Type { get { return type; } }         // 지점 유형
        public PathNodeInfo BasePlace
        {
            set { basePlace = value; }
            get { return type == PathNodeType.Absolute ? this : basePlace; } // 기준 지점. Type 이 Absolute 일 경우 self reference
        }
        public double XPosition { get { return position_x; } }
        public double YPosition { get { return position_y; } }
        public double Direction { get { return direction; } }
        public double Distance { get { return distance; } }
        public int LandMarkID { get { return landmarkID; } }

        public Dictionary<PathNodeInfo, ChannelInfo> Channels { get { return channels; } }

        public PathNodeInfo()
        {
            this.name = "";
            this.type = PathNodeType.Absolute;
            this.basePlace = this;
            this.position_x = 0;
            this.position_y = 0;
            this.direction = 0;
            this.distance = 0;
            this.landmarkID = 0;
            this.channels = new Dictionary<PathNodeInfo, ChannelInfo>();
        }

        public PathNodeInfo(string name, int id)
            : this()
        {
            this.type = PathNodeType.Absolute;
            this.position_x = 0;
            this.position_y = 0;
            this.distance = 0;
            this.direction = 0;
            this.name = name;
            this.landmarkID = id;
        }

        public PathNodeInfo(PathNodeType type, double x, double y, double direction, double distance, int id, string name)
            : this()
        {
            this.type = type;
            this.position_x = x;
            this.position_y = y;
            this.direction = direction;
            this.distance = distance;
            this.landmarkID = id;
            this.name = name;
        }

        public PathNodeInfo(double x, double y, int id, string name)
            : this()
        {
            this.type = PathNodeType.Absolute;
            this.position_x = x;
            this.position_y = y;
            this.distance = Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2));
            this.direction = Math.Atan2(y, x);
            this.landmarkID = id;
            this.name = name;
        }

        public PathNodeInfo(PathNodeInfo basePlace, double direction, double distance, string name)
            : this()
        {
            this.type = PathNodeType.Relative;
            this.basePlace = basePlace;
            this.direction = direction;
            this.distance = distance;
            this.position_x = distance * Math.Cos(direction);
            this.position_y = distance * Math.Sin(direction);
            this.name = name;
        }

        public void SetPosition(double x, double y)
        {
            this.position_x = x;
            this.position_y = y;
        }
    }

    /// <summary>
    /// 채널 정보
    /// 2013.11.20 by Mini
    /// </summary>
    public class ChannelInfo
    {
        private PathNodeInfo placeStart;
        private PathNodeInfo placeDestination;
        private double direction;
        private double distance;

        public PathNodeInfo StartingPoint { get { return placeStart; } }
        public PathNodeInfo Destination { get { return placeDestination; } }
        public double Direction { get { return direction; } }
        public double Distance { get { return distance; } }

        public ChannelInfo(PathNodeInfo start, PathNodeInfo dest, double direction, double distance)
        {
            this.placeStart = start;
            this.placeDestination = dest;
            this.direction = direction;
            this.distance = distance;
            this.placeStart.Channels.Add(dest, this);
        }
    }

    /// <summary>
    /// 경로지도
    /// 2013.11.20 by Mini
    /// </summary>
    public class PathNodeMap
    {
        private List<PathNodeInfo> listNode;
        private List<ChannelInfo> listChannel;

        public List<PathNodeInfo> Nodes { get { return listNode; } }
        public List<ChannelInfo> Channels { get { return listChannel; } }

        public PathNodeMap()
        {
            this.listNode = new List<PathNodeInfo>();
            this.listChannel = new List<ChannelInfo>();
        }

        public PathNodeMap(string path)
            : this()
        {
            LoadMap(path);
        }

        /// <summary>
        /// Dijkstra Algorithm 을 이용한 최단거리 탐색
        /// </summary>
        /// <param name="start"></param>
        /// <param name="dest"></param>
        /// <returns></returns>
        public List<ChannelInfo> GetPath(PathNodeInfo start, PathNodeInfo dest)
        {
            DijkstraNode seedNode = new DijkstraNode(new ChannelInfo(new PathNodeInfo(), start, 0, 0), null);
            DijkstraNode shortestPath = SearchPath(seedNode, dest);

            if (shortestPath == null) return null;

            return shortestPath.ChannelList;
        }

        public List<ChannelInfo> GetPath(string start, string dest)
        {
            Dictionary<string, PathNodeInfo> dicNodes = new Dictionary<string, PathNodeInfo>();
            foreach (PathNodeInfo node in listNode)
                dicNodes.Add(node.Name, node);

            PathNodeInfo startnode = GetNode(start);
            PathNodeInfo destnode = GetNode(dest);

            if (startnode == null || destnode == null) return null;
            return GetPath(startnode, destnode);
        }

        public PathNodeInfo GetNode(string name)
        {
            Dictionary<string, PathNodeInfo> dicNodes = new Dictionary<string, PathNodeInfo>();
            foreach (PathNodeInfo node in listNode)
                dicNodes.Add(node.Name, node);

            PathNodeInfo pni = null;
            if (!dicNodes.TryGetValue(name, out pni)) return null;
            return pni;
        }

        public StarList GetLandmarks()
        {
            StarList landmarks = new StarList();

            foreach (PathNodeInfo pni in listNode)
            {
                if (pni.Type == PathNodeType.Absolute)
                {
                    if (pni.LandMarkID > 0)
                        landmarks.Add(new StarInfo(pni.Name, pni.LandMarkID));
                }
            }
            return landmarks;
        }

        private DijkstraNode SearchPath(DijkstraNode node, PathNodeInfo dest)
        {
            DijkstraNode resultNode = null;
            if (node.Node == dest)
            {
                // node가 목표 노드일 경우 현 노드의 탐색 종료
                return node;
            }

            foreach (ChannelInfo channel in node.Node.Channels.Values)
            {
                DijkstraNode childnode = new DijkstraNode(channel, node);
                if (!childnode.CycleCheck())
                {
                    // 싸이클링 되지 않는 경우에만 노드 검색을 계속 한다.
                    DijkstraNode searchedNode = SearchPath(childnode, dest);
                    if (searchedNode != null)
                    {
                        if (resultNode == null)
                        {
                            // 기 검색된 노드가 없을 경우 새노드를 결과 노드로...
                            resultNode = searchedNode;
                        }
                        else
                        {
                            // 이전 결과 노드와 새 노드의 거리 비교
                            if (resultNode.PathLength > searchedNode.PathLength)
                            {
                                resultNode = searchedNode;
                            }
                            else if (resultNode.PathLength == searchedNode.PathLength)
                            {
                                // 이전 결과노드와 새노드 거리가 같을 경우는 경로 수 비교
                                if (resultNode.PathCount > searchedNode.PathCount)
                                    resultNode = searchedNode;
                            }
                        }

                    }
                }
            }
            return resultNode;
        }

        public void SaveMap(string path)
        {
            PathNodeMapData mapdata = new PathNodeMapData();
            foreach (PathNodeInfo node in listNode)
            {
                mapdata.nodes.Add(new PathNodeInfoData(node));
            }

            XmlSerializer xs = new XmlSerializer(typeof(PathNodeMapData));
            StreamWriter sw = new StreamWriter(path);
            xs.Serialize(sw, mapdata);
            sw.Close();
        }

        public bool LoadMap(string path)
        {
            // XML Serialize
            PathNodeMapData mapdata;
            XmlSerializer xs = new XmlSerializer(typeof(PathNodeMapData));
            StreamReader sr = new StreamReader(path);
            mapdata = xs.Deserialize(sr) as PathNodeMapData;
            sr.Close();

            if (mapdata == null)
                return false;

            // 정보 초기화
            List<PathNodeInfo> listNode = new List<PathNodeInfo>();
            List<ChannelInfo> listChannel = new List<ChannelInfo>();

            // basePlace 의 cycling 이 발생할 수도 있기 때문에 Node 의 생성과 BasePlace 연동을 구분하여 수행한다.

            Dictionary<string, PathNodeInfo> dicNodes = new Dictionary<string, PathNodeInfo>();
            // Node 정보 적용
            foreach (PathNodeInfoData nodedata in mapdata.nodes)
            {
                PathNodeInfo node = new PathNodeInfo(nodedata.type, nodedata.position_x, nodedata.position_y, nodedata.direction, nodedata.distance, nodedata.landmarkID, nodedata.name);
                dicNodes.Add(node.Name, node);
                listNode.Add(node);
            }

            // BasePlace 연동
            foreach (PathNodeInfoData nodedata in mapdata.nodes)
            {
                //NodeInfo node = new NodeInfo(nodedata.type, nodedata.position_x, nodedata.position_y, nodedata.direction, nodedata.distance, nodedata.name);
                PathNodeInfo node;
                if (!dicNodes.TryGetValue(nodedata.name, out node)) return false;

                PathNodeInfo baseNode;
                if (!dicNodes.TryGetValue(nodedata.basePlace, out baseNode)) return false;
                node.BasePlace = baseNode;
            }

            // Channel 설정
            foreach (PathNodeInfoData nodedata in mapdata.nodes)
            {
                PathNodeInfo start;
                if (!dicNodes.TryGetValue(nodedata.name, out start)) return false;

                foreach (ChannelInfoData channeldata in nodedata.channels)
                {
                    PathNodeInfo dest;
                    if (!dicNodes.TryGetValue(channeldata.PlaceDestination, out dest)) return false;

                    listChannel.Add(new ChannelInfo(start, dest, channeldata.Direction, channeldata.Distance));
                }
            }

            // 맵 정보 교체
            this.listNode = listNode;
            this.listChannel = listChannel;

            return true;
        }
    }

    /// <summary>
    /// Dijkstra Algorithm 을 위한 노드
    /// 2013.11.20 by Mini
    /// </summary>
    public class DijkstraNode
    {
        public ChannelInfo Channel { set; get; }
        public DijkstraNode Parent { set; get; }
        public double ChannelLength { get { return Channel.Distance; } }
        public PathNodeInfo Node { get { return Channel.Destination; } }

        public double PathLength { get { return Parent == null ? ChannelLength : ChannelLength + Parent.PathLength; } }
        public int PathCount { get { return Parent == null ? 1 : Parent.PathCount + 1; } }

        public List<ChannelInfo> ChannelList
        {
            get
            {
                List<ChannelInfo> result;
                if (Parent == null)
                {
                    result = new List<ChannelInfo>();
                }
                else
                {
                    result = Parent.ChannelList;
                    result.Add(this.Channel);
                }
                return result;
            }
        }

        public DijkstraNode(ChannelInfo channel, DijkstraNode parent)
        {
            Channel = channel;
            Parent = parent;
        }

        /// <summary>
        /// return 값이 true 일 경우 사이클링 발생상태.
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public bool CycleCheck(PathNodeInfo node)
        {
            if (Parent != null)
            {
                return Channel.StartingPoint == node || Parent.CycleCheck(node);
            }

            return Channel.StartingPoint == node;
        }

        public bool CycleCheck()
        {
            if (Parent != null)
                return Parent.CycleCheck(Node);
            return false;
        }
    }

    /// <summary>
    /// NodeInfo 를 파일 입출력 하기 위한 클래스.
    /// NodeInfo / ChannelInfo 는 상호참조를 하는 벡터형 클래스인 관계로 XmlSerialize 를 할 수 없다.
    /// 그래서 데이터 클래스를 거쳐서 Serialize 한다.
    /// 2013.11.20 by Mini
    /// </summary>
    public class PathNodeInfoData
    {
        public string name;
        public PathNodeType type;
        public string basePlace;
        public double position_x;
        public double position_y;
        public double direction;
        public double distance;
        public int landmarkID;

        public List<ChannelInfoData> channels;

        public string Name
        {
            set { this.name = value; }
            get { return this.name; }
        }

        public PathNodeType Type
        {
            set { this.type = value; }
            get { return this.type; }
        }

        public double X
        {
            set { this.position_x = value; }
            get { return this.position_x; }
        }

        public double Y
        {
            set { this.position_y = value; }
            get { return this.position_y; }
        }

        public double Direction
        {
            set { this.direction = value; }
            get { return this.direction; }
        }

        public double Distance
        {
            set { this.distance = value; }
            get { return this.distance; }
        }

        public PathNodeInfoData()
        {
            channels = new List<ChannelInfoData>();
        }

        public PathNodeInfoData(string name, PathNodeType type, double x, double y)
            : this()
        {
            this.name = name;
            this.type = type;
            this.position_x = x;
            this.position_y = y;
        }

        public PathNodeInfoData(PathNodeInfo node)
            : this()
        {
            this.name = node.Name;
            this.type = node.Type;
            this.basePlace = node.BasePlace != null ? node.BasePlace.Name : "";
            this.position_x = node.XPosition;
            this.position_y = node.YPosition;
            this.direction = node.Direction;
            this.distance = node.Distance;

            foreach (ChannelInfo channel in node.Channels.Values)
            {
                this.channels.Add(new ChannelInfoData(channel));
            }
        }
    }

    /// <summary>
    /// ChannelInfo 를 파일 입출력 하기 위한 클래스.
    /// NodeInfo / ChannelInfo 는 상호참조를 하는 벡터형 클래스인 관계로 XmlSerialize 를 할 수 없다.
    /// 그래서 데이터 클래스를 거쳐서 Serialize 한다.
    /// 2013.11.20 by Mini
    /// </summary>
    public class ChannelInfoData
    {
        #region Fields
        private string placeDestination;
        private double direction;
        private double distance;
        #endregion

        #region Properties
        public string PlaceDestination
        {
            set { this.placeDestination = value; }
            get { return this.placeDestination; }
        }

        public double Direction
        {
            set { this.direction = value; }
            get { return this.direction; }
        }

        public double Distance
        {
            set { this.distance = value; }
            get { return this.distance; }
        }
        #endregion

        public ChannelInfoData()
        {

        }

        public ChannelInfoData(ChannelInfo channel)
            : this()
        {
            this.placeDestination = channel.Destination.Name;
            this.direction = channel.Direction;
            this.distance = channel.Distance;
        }

        public ChannelInfoData(string dest, double dir, double dist)
            : this()
        {
            this.placeDestination = dest;
            this.direction = dir;
            this.distance = dist;
        }

    }

    /// <summary>
    /// RouteMap 을 저장하기 위한 클래스
    /// 2013.11.20 by Mini
    /// </summary>
    public class PathNodeMapData
    {
        public List<PathNodeInfoData> nodes;

        public PathNodeMapData()
        {
            nodes = new List<PathNodeInfoData>();
        }
    }

}
