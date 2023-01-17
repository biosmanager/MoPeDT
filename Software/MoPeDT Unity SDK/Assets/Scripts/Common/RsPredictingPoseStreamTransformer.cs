using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Intel.RealSense;
using UnityEngine;
using UnityEngine.Events;

namespace MoPeDT.Common
{
    public class RsPredictingPoseStreamTransformer : MonoBehaviour
    {
        [StructLayout(LayoutKind.Sequential)]
        public class RsPose
        {
            public Vector3 translation;
            public Vector3 velocity;
            public Vector3 acceleration;
            public Quaternion rotation;
            public Vector3 angular_velocity;
            public Vector3 angular_acceleration;
            public int tracker_confidence;
            public int mapper_confidence;
        }


        public RsFrameProvider Source;
        public UnityEvent onTransformUpdated;
        public float posePredictionSeconds = 0.0f;


        private RsPose pose = new RsPose();
        private FrameQueue q;

        private const float FLT_EPSILON = 1.192092896e-07F;


        void Start()
        {
            Source.OnStart += OnStartStreaming;
            Source.OnStop += OnStopStreaming;
        }

        private void OnStartStreaming(PipelineProfile profile)
        {
            q = new FrameQueue(1);
            Source.OnNewSample += OnNewSample;
        }


        private void OnStopStreaming()
        {
            Source.OnNewSample -= OnNewSample;

            if (q != null)
            {
                q.Dispose();
                q = null;
            }
        }


        private void OnNewSample(Frame f)
        {
            if (f.IsComposite)
            {
                using (var fs = f.As<FrameSet>())
                using (var poseFrame = fs.FirstOrDefault(Stream.Pose, Format.SixDOF))
                    if (poseFrame != null)
                        q.Enqueue(poseFrame);
            }
            else
            {
                using (var p = f.Profile)
                    if (p.Stream == Stream.Pose && p.Format == Format.SixDOF)
                        q.Enqueue(f);
            }
        }

        void Update()
        {
            if (q != null)
            {
                PoseFrame frame;
                if (q.PollForFrame(out frame))
                    using (frame)
                    {
                        frame.CopyTo(pose);

                        pose = PredictPose(pose, posePredictionSeconds);

                        // Convert T265 coordinate system to Unity's
                        // see https://realsense.intel.com/how-to-getting-imu-data-from-d435i-and-t265/

                        var t = pose.translation;
                        t.Set(t.x, t.y, -t.z);

                        var e = pose.rotation.eulerAngles;
                        var r = Quaternion.Euler(-e.x, -e.y, e.z);

                        transform.localRotation = r;
                        transform.localPosition = t;

                        onTransformUpdated.Invoke();
                    }

            }
        }

        private RsPose PredictPose(RsPose pose, float deltaSeconds)
        {
            var P = new RsPose();
            P.velocity = pose.velocity;
            P.acceleration = pose.acceleration;
            P.angular_velocity = pose.angular_velocity;
            P.angular_acceleration = pose.angular_velocity;
            P.tracker_confidence = pose.tracker_confidence;
            P.mapper_confidence = pose.mapper_confidence;

            P.translation = deltaSeconds * (deltaSeconds / 2 * pose.acceleration + pose.velocity) + pose.translation;
            var W = deltaSeconds * (deltaSeconds / 2 * pose.angular_acceleration + pose.angular_velocity);
            P.rotation = QuaternionExp(W) * pose.rotation;

            return P;
        }

        private Quaternion QuaternionExp(Vector3 v)
        {
            float x = v.x / 2, y = v.y / 2, z = v.z / 2, th2, th = Mathf.Sqrt(th2 = x * x + y * y + z * z);
            float c = Mathf.Cos(th), s = th2 < Mathf.Sqrt(120 * FLT_EPSILON) ? 1 - th2 / 6 : Mathf.Sin(th) / th;
            return new Quaternion(s * x, s * y, s * z, c);
        }
    }
}