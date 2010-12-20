﻿using System;
using System.Runtime.InteropServices;

namespace Box2CS
{
#if !NEW_JOINTS
	[StructLayout(LayoutKind.Sequential)]
	public class DistanceJointDef : JointDef, IFixedSize
	{
		Vec2 _localAnchorA;
		Vec2 _localAnchorB;
		float _length;
		float _frequencyHz;
		float _dampingRatio;

		int IFixedSize.FixedSize()
		{
			return Marshal.SizeOf(typeof(DistanceJointDef));
		}

		void IFixedSize.Lock()
		{
		}

		void IFixedSize.Unlock()
		{
		}

		public DistanceJointDef()
		{
			JointType = EJointType.e_distanceJoint;
			_localAnchorA = Vec2.Empty;
			_localAnchorB = Vec2.Empty;
			_length = 1.0f;
			_frequencyHz = 0.0f;
			_dampingRatio = 0.0f;
		}

		/// Initialize the bodies, anchors, and length using the world
		/// anchors.
		public void Initialize(Body bodyA, Body bodyB,
						Vec2 anchorA, Vec2 anchorB)
		{
			BodyA = bodyA;
			BodyB = bodyB;
			_localAnchorA = bodyA.GetLocalPoint(anchorA);
			_localAnchorB = bodyB.GetLocalPoint(anchorB);
			Vec2 d = anchorB - anchorA;
			_length = d.Length();
		}

		public Vec2 LocalAnchorA
		{
			get { return _localAnchorA; }
			set { _localAnchorA = value; }
		}

		public Vec2 LocalAnchorB
		{
			get { return _localAnchorB; }
			set { _localAnchorB = value; }
		}

		public float Length
		{
			get { return _length; }
			set { _length = value; }
		}

		public float FrequencyHz
		{
			get { return _frequencyHz; }
			set { _frequencyHz = value; }
		}

		public float DampingRatio
		{
			get { return _dampingRatio; }
			set { _dampingRatio = value; }
		}
	}
#else
	public class DistanceJointDef : JointDef, IDisposable
	{
		static class NativeMethods
		{
			[DllImport(Box2DSettings.Box2CDLLName)]
			public static extern IntPtr b2distancejointdef_constructor();

			[DllImport(Box2DSettings.Box2CDLLName)]
			public static extern void b2distancejointdef_destroy(IntPtr ptr);

			[DllImport(Box2DSettings.Box2CDLLName)]
			public static extern void b2distancejointdef_getlocalanchora(IntPtr jointDef, out Vec2 outPtr);

			[DllImport(Box2DSettings.Box2CDLLName)]
			public static extern void b2distancejointdef_setlocalanchora(IntPtr jointDef, Vec2 vec);

			[DllImport(Box2DSettings.Box2CDLLName)]
			public static extern void b2distancejointdef_getlocalanchorb(IntPtr jointDef, out Vec2 outPtr);

			[DllImport(Box2DSettings.Box2CDLLName)]
			public static extern void b2distancejointdef_setlocalanchorb(IntPtr jointDef, Vec2 vec);

			[DllImport(Box2DSettings.Box2CDLLName)]
			public static extern float b2distancejointdef_getlength(IntPtr jointDef);

			[DllImport(Box2DSettings.Box2CDLLName)]
			public static extern void b2distancejointdef_setlength(IntPtr jointDef, float vec);

			[DllImport(Box2DSettings.Box2CDLLName)]
			public static extern float b2distancejointdef_getfrequency(IntPtr jointDef);

			[DllImport(Box2DSettings.Box2CDLLName)]
			public static extern void b2distancejointdef_setfrequency(IntPtr jointDef, float vec);

			[DllImport(Box2DSettings.Box2CDLLName)]
			public static extern float b2distancejointdef_getdampingratio(IntPtr jointDef);

			[DllImport(Box2DSettings.Box2CDLLName)]
			public static extern void b2distancejointdef_setdampingratio(IntPtr jointDef, float vec);
		}

		public DistanceJointDef()
		{
			JointDefPtr = NativeMethods.b2distancejointdef_constructor();
			shouldDispose = true;
		}

		public void Initialize(Body b1, Body b2,
									Vec2 anchor1, Vec2 anchor2)
		{
			BodyA = b1;
			BodyB = b2;
			LocalAnchorA = BodyA.GetLocalPoint(anchor1);
			LocalAnchorB = BodyB.GetLocalPoint(anchor2);
			Vec2 d = anchor2 - anchor1;
			Length = d.Length();
		}

		public Vec2 LocalAnchorA
		{
			get { Vec2 temp; NativeMethods.b2distancejointdef_getlocalanchora(JointDefPtr, out temp); return temp; }
			set { NativeMethods.b2distancejointdef_setlocalanchora(JointDefPtr, value); }
		}

		public Vec2 LocalAnchorB
		{
			get { Vec2 temp; NativeMethods.b2distancejointdef_getlocalanchorb(JointDefPtr, out temp); return temp; }
			set { NativeMethods.b2distancejointdef_setlocalanchorb(JointDefPtr, value); }
		}

		public float Length
		{
			get { return NativeMethods.b2distancejointdef_getlength(JointDefPtr); }
			set { NativeMethods.b2distancejointdef_setlength(JointDefPtr, value); }
		}

		public float Frequency
		{
			get { return NativeMethods.b2distancejointdef_getfrequency(JointDefPtr); }
			set { NativeMethods.b2distancejointdef_setfrequency(JointDefPtr, value); }
		}

		public float DampingRatio
		{
			get { return NativeMethods.b2distancejointdef_getdampingratio(JointDefPtr); }
			set { NativeMethods.b2distancejointdef_setdampingratio(JointDefPtr, value); }
		}

		#region IDisposable
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		bool disposed;
		bool shouldDispose;

		private void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				if (disposing)
				{
				}

				if (shouldDispose)
				{
					NativeMethods.b2distancejointdef_destroy(JointDefPtr);
					//System.Console.WriteLine("b2distancejointdef_destroy");
				}

				disposed = true;
			}
		}

		~DistanceJointDef()
		{
			Dispose(false);
		}
		#endregion
	}
#endif
	public class DistanceJoint : Joint
	{
		static class NativeMethods
		{
			[DllImport(Box2DSettings.Box2CDLLName)]
			public static extern void b2distancejoint_setratio(IntPtr joint, float data);

			[DllImport(Box2DSettings.Box2CDLLName)]
			public static extern float b2distancejoint_getratio(IntPtr joint);

			[DllImport(Box2DSettings.Box2CDLLName)]
			public static extern void b2distancejoint_setfrequency(IntPtr joint, float data);

			[DllImport(Box2DSettings.Box2CDLLName)]
			public static extern float b2distancejoint_getfrequency(IntPtr joint);

			[DllImport(Box2DSettings.Box2CDLLName)]
			public static extern void b2distancejoint_setdampingratio(IntPtr joint, float data);

			[DllImport(Box2DSettings.Box2CDLLName)]
			public static extern float b2distancejoint_getdampingratio(IntPtr joint);
		}

		public DistanceJoint(IntPtr ptr) :
			base(ptr)
		{
		}

		public float Ratio
		{
			get { return NativeMethods.b2distancejoint_getratio(JointPtr); }
			set { NativeMethods.b2distancejoint_setratio(JointPtr, value); }
		}

		public float Frequency
		{
			get { return NativeMethods.b2distancejoint_getfrequency(JointPtr); }
			set { NativeMethods.b2distancejoint_setfrequency(JointPtr, value); }
		}

		public float DampingRatio
		{
			get { return NativeMethods.b2distancejoint_getdampingratio(JointPtr); }
			set { NativeMethods.b2distancejoint_setdampingratio(JointPtr, value); }
		}
	}
}
