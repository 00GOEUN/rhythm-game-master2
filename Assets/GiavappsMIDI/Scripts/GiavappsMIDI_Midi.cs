using System;
using System.Runtime.InteropServices;

namespace Giavapps
{
	namespace MIDI
	{
		public static class Midi
		{
			[DllImport(Plugin.Name)]
			private static extern void Midi_Deinitialize();

			public static void Deinitialize()
			{
			Midi_Deinitialize();
			}

			public static class Input
			{
				public static class Device
				{
				[DllImport(Plugin.Name)]
				private static extern uint Midi_InputDeviceCount();
				
				[DllImport(Plugin.Name)]
				private static extern IntPtr Midi_InputDeviceName(uint DeviceIndex);
				
				[DllImport(Plugin.Name)]
				private static extern bool Midi_InputDeviceOpen(uint DeviceIndex);

				[DllImport(Plugin.Name)]
				private static extern void Midi_InputDeviceOpenAll();

				[DllImport(Plugin.Name)]
				private static extern bool Midi_InputDeviceIsOpen(uint DeviceIndex);

				[DllImport(Plugin.Name)]
				private static extern ulong Midi_InputDeviceOpenCount();

				[DllImport(Plugin.Name)]
				private static extern bool Midi_InputDeviceClose(uint DeviceIndex);

				[DllImport(Plugin.Name)]
				private static extern void Midi_InputDeviceCloseAll();

				[DllImport(Plugin.Name)]
				private static extern bool Midi_InputDeviceConnect(uint InputDeviceIndex, uint OutputDeviceIndex);

				[DllImport(Plugin.Name)]
				private static extern bool Midi_InputDeviceDisconnect(uint InputDeviceIndex, uint OutputDeviceIndex);

				[DllImport(Plugin.Name)]
				private static extern ulong Midi_InputDeviceConnectionCount(uint DeviceIndex);

				[DllImport(Plugin.Name)]
				private static extern bool Midi_InputDeviceConnectionExists(uint InputDeviceIndex, uint OutputDeviceIndex);

					public static uint Count()
					{
					return Midi_InputDeviceCount();
					}

					public static string Name(uint DeviceIndex)
					{
					return Marshal.PtrToStringUni(Midi_InputDeviceName(DeviceIndex));
					}

					public static bool Open(uint DeviceIndex)
					{
					return Midi_InputDeviceOpen(DeviceIndex);
					}

					public static void OpenAll()
					{
					Midi_InputDeviceOpenAll();
					}

					public static bool IsOpen(uint DeviceIndex)
					{
					return Midi_InputDeviceIsOpen(DeviceIndex);
					}

					public static ulong OpenCount()
					{
					return Midi_InputDeviceOpenCount();
					}

					public static bool Close(uint DeviceIndex)
					{
					return Midi_InputDeviceClose(DeviceIndex);
					}

					public static void CloseAll()
					{
					Midi_InputDeviceCloseAll();
					}

					public static bool Connect(uint InputDeviceIndex, uint OutputDeviceIndex)
					{
					return Midi_InputDeviceConnect(InputDeviceIndex, OutputDeviceIndex);
					}

					public static bool Disconnect(uint InputDeviceIndex, uint OutputDeviceIndex)
					{
					return Midi_InputDeviceDisconnect(InputDeviceIndex, OutputDeviceIndex);
					}

					public static ulong ConnectionCount(uint DeviceIndex)
					{
					return Midi_InputDeviceConnectionCount(DeviceIndex);
					}

					public static bool ConnectionExists(uint InputDeviceIndex, uint OutputDeviceIndex)
					{
					return Midi_InputDeviceConnectionExists(InputDeviceIndex, OutputDeviceIndex);
					}
				}

				public static class Message
				{
					[DllImport(Plugin.Name)]
					private static extern void Midi_InputMessageManualChecking(bool ManualChecking);

					[DllImport(Plugin.Name)]
					private static extern ulong Midi_InputMessageCount(uint DeviceIndex);

					[DllImport(Plugin.Name)]
					private static extern ulong Midi_InputMessageSize(uint DeviceIndex, ulong MessageIndex);

					[DllImport(Plugin.Name)]
					private static extern byte Midi_InputMessageByte(uint DeviceIndex, ulong MessageIndex, ulong ByteIndex);

					[DllImport(Plugin.Name)]
					private static extern ulong Midi_InputMessageTime(uint DeviceIndex, ulong MessageIndex);

					public static void ManualChecking(bool ManualChecking)
					{
					Midi_InputMessageManualChecking(ManualChecking);
					}

					public static ulong Count(uint DeviceIndex)
					{
					return Midi_InputMessageCount(DeviceIndex);
					}

					public static ulong Size(uint DeviceIndex, ulong MessageIndex)
					{
					return Midi_InputMessageSize(DeviceIndex, MessageIndex);
					}

					public static byte Byte(uint DeviceIndex, ulong MessageIndex, ulong ByteIndex)
					{
					return Midi_InputMessageByte(DeviceIndex, MessageIndex, ByteIndex);
					}

					public static ulong Time(uint DeviceIndex, ulong MessageIndex)
					{
					return Midi_InputMessageTime(DeviceIndex, MessageIndex);
					}
				}
			}

			public static class Output
			{
				public static class Device
				{
					[DllImport(Plugin.Name)]
					private static extern uint Midi_OutputDeviceCount();
				
					[DllImport(Plugin.Name)]
					private static extern IntPtr Midi_OutputDeviceName(uint DeviceIndex);

					[DllImport(Plugin.Name)]
					private static extern bool Midi_OutputDeviceOpen(uint DeviceIndex);

					[DllImport(Plugin.Name)]
					private static extern void Midi_OutputDeviceOpenAll();

					[DllImport(Plugin.Name)]
					private static extern bool Midi_OutputDeviceIsOpen(uint DeviceIndex);

					[DllImport(Plugin.Name)]
					private static extern ulong Midi_OutputDeviceOpenCount();

					[DllImport(Plugin.Name)]
					private static extern bool Midi_OutputDeviceClose(uint DeviceIndex);

					[DllImport(Plugin.Name)]
					private static extern void Midi_OutputDeviceCloseAll();

					public static uint Count()
					{
					return Midi_OutputDeviceCount();
					}

					public static string Name(uint DeviceIndex)
					{
					return Marshal.PtrToStringUni(Midi_OutputDeviceName(DeviceIndex));
					}

					public static bool Open(uint DeviceIndex)
					{
					return Midi_OutputDeviceOpen(DeviceIndex);
					}

					public static void OpenAll()
					{
					Midi_OutputDeviceOpenAll();
					}

					public static bool IsOpen(uint DeviceIndex)
					{
					return Midi_OutputDeviceIsOpen(DeviceIndex);
					}

					public static ulong OpenCount()
					{
					return Midi_OutputDeviceOpenCount();
					}

					public static bool Close(uint DeviceIndex)
					{
					return Midi_OutputDeviceClose(DeviceIndex);
					}

					public static void CloseAll()
					{
					Midi_OutputDeviceCloseAll();
					}
				}

				public static class Message
				{
	
					[DllImport(Plugin.Name)]
					private static extern void Midi_OutputMessageClear();
			
					[DllImport(Plugin.Name)]
					private static extern ulong Midi_OutputMessageSize();
			
					[DllImport(Plugin.Name)]
					private static extern void Midi_OutputMessageByte(byte Byte);
			
					[DllImport(Plugin.Name)]
					private static extern bool Midi_OutputMessageSend(uint DeviceIndex);

					[DllImport(Plugin.Name)]
					private static extern bool Midi_OutputMessageSendShort(uint DeviceIndex, byte Byte1, byte Byte2, byte Byte3);
		
					public static void Clear()
					{
					Midi_OutputMessageClear();
					}
			
					public static ulong Size()
					{
					return Midi_OutputMessageSize();
					}
					
					public static void Byte(byte Byte)
					{
					Midi_OutputMessageByte(Byte);
					}
					
					public static bool Send(uint DeviceIndex)
					{
					return Midi_OutputMessageSend(DeviceIndex);
					}

					public static bool SendShort(uint DeviceIndex, byte Byte1, byte Byte2, byte Byte3)
					{
					return Midi_OutputMessageSendShort(DeviceIndex, Byte1, Byte2, Byte3);
					}
					
				}

			}

			public static class Error
			{
				[DllImport(Plugin.Name)]
				private static extern void Midi_ErrorManualChecking(bool ManualChecking);
		
				[DllImport(Plugin.Name)]
				private static extern ulong Midi_ErrorCount();
		
				[DllImport(Plugin.Name)]
				private static extern IntPtr Midi_ErrorString(ulong ErrorIndex);
		
				public static void ManualChecking(bool ManualChecking)
				{
				Midi_ErrorManualChecking(ManualChecking);
				}
		
				public static ulong Count()
				{
				return Midi_ErrorCount();
				}
		
				public static string String(ulong ErrorIndex)
				{
				return Marshal.PtrToStringUni(Midi_ErrorString(ErrorIndex));
				}
	
			}

		}
	}
}