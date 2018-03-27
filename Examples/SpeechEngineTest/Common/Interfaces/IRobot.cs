using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ServiceModel;

using FutureRobot.FuroWare.Common.Utils;

namespace FutureRobot.FuroWare.Common.Interfaces
{
    [ServiceContract]
    public interface IRobot
    {
        [OperationContract]
        void ChangeLanguage(string language);
        [OperationContract]
        void PlayDialog(string dialogID);
        [OperationContract]
        void PlaySpeech(string speechData);
        [OperationContract]
        void StopSpeech();
        [OperationContract]
        void PlayLipSync(string speechData);
        [OperationContract]
        void PlayVisemes(string visemeData);

        [OperationContract]
        void AdjustVolume(bool isVolumeUp);
        [OperationContract]
        void SetVolume(float fLevel);
        [OperationContract]
        void SetMute(bool isMute);
        [OperationContract]
        void Beep();
        [OperationContract]
        void PlaySound(string fileName);
        [OperationContract]
        void StopSound();

        [OperationContract]
        void SetEmotion(int expressionNum, float intensity);
        [OperationContract]
        void SetEmotion(string emotion);
        [OperationContract]
        void SetFace(string faceFile);
        [OperationContract]
        void SetHair(string hairFile);
        [OperationContract]
        void SetCharacterPose(int poseIndex, float intensity);
        [OperationContract]
        void ShowCharacter();
        [OperationContract]
        void HideCharacter();
        [OperationContract]
        void MoveCharacter(int x, int y, int width, int height);

        [OperationContract]
        void InitPose();
        [OperationContract]
        void SetHeadRoll(double roll, double velocity);
        [OperationContract]
        void SetHeadPitch(double pitch, double velocity);
        [OperationContract]
        void SetHeadYaw(double yaw, double velocity);
        [OperationContract]
        void SetWaistPitch(double pitch, double velocity);
        [OperationContract]
        void SetWristPitch(double pitch, double velocity);
        [OperationContract]
        void StartSync();
        [OperationContract]
        void EndSync();
        [OperationContract]
        void PlayBehavior(string behaviorName);
        [OperationContract]
        void StopBehavior();

        [OperationContract]
        void DriveWheel(double linearVelocity, double angularVelocity);
        [OperationContract]
        void DriveWheel(double linearVelocity, double angularVelocity, DrivePriority priority);
        [OperationContract]
        void DriveWheel(double linearVelocity, double angularVelocity, double keepSeconds);
        [OperationContract]
        void DriveWheel(double linearVelocity, double angularVelocity, double keepSeconds, DrivePriority priority);
        [OperationContract]
        void MoveWheel(double distance, double linearVelocity);
        [OperationContract]
        void RotateWheel(double angle, double angularVelocity);
        [OperationContract]
        void StopWheel();
        [OperationContract]
        bool IsWheelRunning();

        [OperationContract]
        void SetPosition(double x, double y, double theta);
        [OperationContract]
        Pose GetPosition();
        [OperationContract]
        void MovePosition(string naviX, string naviY, string naviTheta);
        [OperationContract]
        void MovePositionName(string targetPose);

        [OperationContract]
        void PrintReceipt(byte[] printData);

        [OperationContract]
        void StartFaceTracking(bool isEnable);
        [OperationContract]
        void StartHumanFollowing(bool isEnable);
        [OperationContract]
        void SetVoiceRecognition(bool isEnable);
        [OperationContract]
        void StartAutonomousDriving(bool isEnable);
        [OperationContract]
        void StartIdleMotion(bool isEnable);
        [OperationContract]
        void StartAutonomousDrivingByStargazer(bool isEnable);
        [OperationContract]
        void StartMonitorAdjusting(bool isEnable);
        [OperationContract]
        void StartPatternDriving(bool isEnable);

        [OperationContract]
        void PopupManager();

        [OperationContract]
        void SendRobotEvent(string str);
        [OperationContract]
        void InitNavigation(string fileName);
        [OperationContract]
        void GotoPosition(double x, double y, double theta);

        [OperationContract]
        List<double> GetAngleDistanceData();
        [OperationContract]
        List<double> GetObstacleDistanceData();
        [OperationContract]
        byte[] GetCaptureImageStream();
        [OperationContract]
        void ProcessCommands(string command);

        [OperationContract]
        void DriveSenarioControl(string command);
    }
}
