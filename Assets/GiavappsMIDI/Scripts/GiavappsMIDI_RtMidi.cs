using System;
using System.Runtime.InteropServices;

namespace Giavapps
{
	namespace MIDI
	{
		public static class RtMidi
		{
		[DllImport(Plugin.Name)]
		private static extern bool RtMidi_Initialize();
	
		[DllImport(Plugin.Name)]
		private static extern void RtMidi_Deinitialize();
	
		[DllImport(Plugin.Name)]
		private static extern IntPtr RtMidi_Version();

		public static bool Initialize()
		{
		return RtMidi_Initialize();
		}

		public static void Deinitialize()
		{
		RtMidi_Deinitialize();
		}
		
		public static string Version = Marshal.PtrToStringUni(RtMidi_Version());
	
			public static class Input
			{
				public static class Device
				{
					[DllImport(Plugin.Name)]
					private static extern int RtMidi_InputDeviceCount();
					
					[DllImport(Plugin.Name)]
					private static extern IntPtr RtMidi_InputDeviceName(uint DeviceIndex);
		
					[DllImport(Plugin.Name)]
					private static extern void RtMidi_InputDeviceAutoMessage(bool AutoMessage);
					
					[DllImport(Plugin.Name)]
					private static extern bool RtMidi_InputDeviceOpen(uint DeviceIndex);
				
					[DllImport(Plugin.Name)]
					private static extern int RtMidi_InputDeviceIsOpen();
				
					[DllImport(Plugin.Name)]
					private static extern bool RtMidi_InputDeviceClose();
		
					public static int Count()
					{
					return RtMidi_InputDeviceCount();
					}
			
					public static string Name(uint DeviceIndex)
					{
					return Marshal.PtrToStringUni(RtMidi_InputDeviceName(DeviceIndex));
					}
					
					public static void AutoMessage(bool AutoMessage)
					{
					RtMidi_InputDeviceAutoMessage(AutoMessage);
					}
			
					public static bool Open(uint DeviceIndex)
					{
					return RtMidi_InputDeviceOpen(DeviceIndex);
					}
			
					public static int IsOpen()
					{
					return RtMidi_InputDeviceIsOpen();
					}
			
					public static bool Close()
					{
					return RtMidi_InputDeviceClose();
					}
				}
		
				public static class Message
				{
					[DllImport(Plugin.Name)]
					private static extern void RtMidi_InputMessageManualChecking(bool ManualChecking);
				
					[DllImport(Plugin.Name)]
					private static extern void RtMidi_InputMessageType(bool Sysex, bool Time, bool Sense);
				
					[DllImport(Plugin.Name)]
					private static extern ulong RtMidi_InputMessageCount();
				
					[DllImport(Plugin.Name)]
					private static extern ulong RtMidi_InputMessageSize(ulong MessageIndex);
				
					[DllImport(Plugin.Name)]
					private static extern byte RtMidi_InputMessageByte(ulong MessageIndex, ulong ByteIndex);
				
					[DllImport(Plugin.Name)]
					private static extern double RtMidi_InputMessageTime(ulong MessageIndex);
			
					public static void ManualChecking(bool ManualChecking)
					{
					RtMidi_InputMessageManualChecking(ManualChecking);
					}
			
					public static void Type(bool Sysex, bool Time, bool Sense)
					{
					RtMidi_InputMessageType(Sysex, Time, Sense);
					}
			
					public static ulong Count()
					{
					return RtMidi_InputMessageCount();
					}
			
					public static ulong Size(ulong MessageIndex)
					{
					return RtMidi_InputMessageSize(MessageIndex);
					}
			
					public static byte Byte(ulong MessageIndex, ulong ByteIndex)
					{
					return RtMidi_InputMessageByte(MessageIndex, ByteIndex);
					}
			
					public static double Time(ulong MessageIndex)
					{
					return RtMidi_InputMessageTime(MessageIndex);
					}
				}
			}
		
			public static class Output
			{
				public static class Device
				{
					[DllImport(Plugin.Name)]
					private static extern int RtMidi_OutputDeviceCount();
			
					[DllImport(Plugin.Name)]
					private static extern IntPtr RtMidi_OutputDeviceName(uint DeviceIndex);
			
					[DllImport(Plugin.Name)]
					private static extern bool RtMidi_OutputDeviceOpen(uint DeviceIndex);
			
					[DllImport(Plugin.Name)]
					private static extern int RtMidi_OutputDeviceIsOpen();
			
					[DllImport(Plugin.Name)]
					private static extern bool RtMidi_OutputDeviceClose();
			
					public static int Count()
					{
					return RtMidi_OutputDeviceCount();
					}
			
					public static string Name(uint DeviceIndex)
					{
					return Marshal.PtrToStringUni(RtMidi_OutputDeviceName(DeviceIndex));
					}
			
					public static bool Open(uint DeviceIndex)
					{
					return RtMidi_OutputDeviceOpen(DeviceIndex);
					}
			
					public static int IsOpen()
					{
					return RtMidi_OutputDeviceIsOpen();
					}
			
					public static bool Close()
					{
					return RtMidi_OutputDeviceClose();
					}
	
				}
		
				public static class Message
				{
	
					[DllImport(Plugin.Name)]
					private static extern void RtMidi_OutputMessageClear();
			
					[DllImport(Plugin.Name)]
					private static extern ulong RtMidi_OutputMessageSize();
			
					[DllImport(Plugin.Name)]
					private static extern void RtMidi_OutputMessageByte(byte Byte);
			
					[DllImport(Plugin.Name)]
					private static extern bool RtMidi_OutputMessageSend();
		
					public static void Clear()
					{
					RtMidi_OutputMessageClear();
					}
			
					public static ulong Size()
					{
					return RtMidi_OutputMessageSize();
					}
			
					public static void Byte(byte Byte)
					{
					RtMidi_OutputMessageByte(Byte);
					}
			
					public static bool Send()
					{
					return RtMidi_OutputMessageSend();
					}
	
				}
	
			}
		
			public static class Error
			{
				[DllImport(Plugin.Name)]
				private static extern void RtMidi_ErrorManualChecking(bool ManualChecking);
		
				[DllImport(Plugin.Name)]
				private static extern ulong RtMidi_ErrorCount();
		
				[DllImport(Plugin.Name)]
				private static extern IntPtr RtMidi_ErrorString(ulong ErrorIndex);
		
				public static void ManualChecking(bool ManualChecking)
				{
				RtMidi_ErrorManualChecking(ManualChecking);
				}
		
				public static ulong Count()
				{
				return RtMidi_ErrorCount();
				}
		
				public static string String(ulong ErrorIndex)
				{
				return Marshal.PtrToStringUni(RtMidi_ErrorString(ErrorIndex));
				}
	
			}

		}

	}

}