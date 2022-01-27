using System;
using System.Runtime.InteropServices;

namespace Giavapps
{
	namespace MIDI
	{
		public static class Mci
		{
		public const string Alias = "midi";

		[DllImport(Plugin.Name)]
		private static extern bool Mci_Initialize(string Alias);

		[DllImport(Plugin.Name)]
		private static extern void Mci_Deinitialize();

		[DllImport(Plugin.Name)]
		private static extern IntPtr Mci_Command(string Command);

		public static bool Initialize()
		{
		return Mci_Initialize(Alias);
		}

		public static void Deinitialize()
		{
		Mci_Deinitialize();
		}

		public static string Command(string Command)
		{
		return Marshal.PtrToStringUni(Mci_Command(Command));
		}

			public static class Sequencer
			{
				[DllImport(Plugin.Name)]
				private static extern ulong Mci_SequencerOpen(string FileName);

				[DllImport(Plugin.Name)]
				private static extern bool Mci_SequencerIsOpen(ulong DeviceIndex);

				[DllImport(Plugin.Name)]
				private static extern ulong Mci_SequencerOpenCount();

				[DllImport(Plugin.Name)]
				private static extern bool Mci_SequencerClose(ulong DeviceIndex);

				[DllImport(Plugin.Name)]
				private static extern bool Mci_SequencerPlay(ulong DeviceIndex);

				[DllImport(Plugin.Name)]
				private static extern bool Mci_SequencerPlayFrom(ulong DeviceIndex, string FromPosition);

				[DllImport(Plugin.Name)]
				private static extern bool Mci_SequencerPlayFromTo(ulong DeviceIndex, string FromPosition, string ToPosition);

				[DllImport(Plugin.Name)]
				private static extern bool Mci_SequencerStop(ulong DeviceIndex);

				[DllImport(Plugin.Name)]
				private static extern bool Mci_SequencerPause(ulong DeviceIndex);

				[DllImport(Plugin.Name)]
				private static extern bool Mci_SequencerSeek(ulong DeviceIndex, string ToPosition);

				[DllImport(Plugin.Name)]
				private static extern bool Mci_SequencerSeekToStart(ulong DeviceIndex);

				[DllImport(Plugin.Name)]
				private static extern bool Mci_SequencerSeekToEnd(ulong DeviceIndex);

				public static ulong Open(string FileName)
				{
				return Mci_SequencerOpen(FileName);
				}

				public static bool IsOpen(ulong DeviceIndex)
				{
				return Mci_SequencerIsOpen(DeviceIndex);
				}

				public static ulong OpenCount()
				{
				return Mci_SequencerOpenCount();
				}

				public static bool Close(ulong DeviceIndex)
				{
				return Mci_SequencerClose(DeviceIndex);
				}

				public static bool Play(ulong DeviceIndex)
				{
				return Mci_SequencerPlay(DeviceIndex);
				}

				public static bool PlayFrom(ulong DeviceIndex, string FromPosition)
				{
				return Mci_SequencerPlayFrom(DeviceIndex, FromPosition);
				}

				public static bool PlayFromTo(ulong DeviceIndex, string FromPosition, string ToPosition)
				{
				return Mci_SequencerPlayFromTo(DeviceIndex, FromPosition, ToPosition);
				}

				public static bool Stop(ulong DeviceIndex)
				{
				return Mci_SequencerStop(DeviceIndex);
				}

				public static bool Pause(ulong DeviceIndex)
				{
				return Mci_SequencerPause(DeviceIndex);
				}
				
				public static bool Seek(ulong DeviceIndex, string ToPosition)
				{
				return Mci_SequencerSeek(DeviceIndex, ToPosition);
				}

				public static bool SeekToStart(ulong DeviceIndex)
				{
				return Mci_SequencerSeekToStart(DeviceIndex);
				}

				public static bool SeekToEnd(ulong DeviceIndex)
				{
				return Mci_SequencerSeekToEnd(DeviceIndex);
				}

				public static class Set
				{
					[DllImport(Plugin.Name)]
					private static extern bool Mci_SequencerSetTempo(ulong DeviceIndex, string Tempo);
					
					public static bool Tempo(ulong DeviceIndex, string Tempo)
					{
					return Mci_SequencerSetTempo(DeviceIndex, Tempo);
					}

					public static class Time
					{
						public static class Format
						{
							[DllImport(Plugin.Name)]
							private static extern bool Mci_SequencerSetTimeFormatMilliseconds(ulong DeviceIndex);

							[DllImport(Plugin.Name)]
							private static extern bool Mci_SequencerSetTimeFormatSMPTE(ulong DeviceIndex, string FPS);

							[DllImport(Plugin.Name)]
							private static extern bool Mci_SequencerSetTimeFormatSMPTE30Drop(ulong DeviceIndex);

							[DllImport(Plugin.Name)]
							private static extern bool Mci_SequencerSetTimeFormatSongPointer(ulong DeviceIndex);

							public static bool Milliseconds(ulong DeviceIndex)
							{
							return Mci_SequencerSetTimeFormatMilliseconds(DeviceIndex);
							}

							public static bool SMPTE(ulong DeviceIndex, string FPS)
							{
							return Mci_SequencerSetTimeFormatSMPTE(DeviceIndex, FPS);
							}

							public static bool SMPTE30Drop(ulong DeviceIndex)
							{
							return Mci_SequencerSetTimeFormatSMPTE30Drop(DeviceIndex);
							}

							public static bool SongPointer(ulong DeviceIndex)
							{
							return Mci_SequencerSetTimeFormatSongPointer(DeviceIndex);
							}
						}
					}
				}

				public static class Status
				{
					[DllImport(Plugin.Name)]
					private static extern IntPtr Mci_SequencerStatusCurrentTrack(ulong DeviceIndex);

					[DllImport(Plugin.Name)]
					private static extern IntPtr Mci_SequencerStatusDivisionType(ulong DeviceIndex);

					[DllImport(Plugin.Name)]
					private static extern IntPtr Mci_SequencerStatusLength(ulong DeviceIndex);

					[DllImport(Plugin.Name)]
					private static extern IntPtr Mci_SequencerStatusLengthTrack(ulong DeviceIndex, string TrackNumber);

					[DllImport(Plugin.Name)]
					private static extern IntPtr Mci_SequencerStatusMediaPresent(ulong DeviceIndex);

					[DllImport(Plugin.Name)]
					private static extern IntPtr Mci_SequencerStatusMode(ulong DeviceIndex);

					[DllImport(Plugin.Name)]
					private static extern IntPtr Mci_SequencerStatusNumberOfTracks(ulong DeviceIndex);

					[DllImport(Plugin.Name)]
					private static extern IntPtr Mci_SequencerStatusOffset(ulong DeviceIndex);

					[DllImport(Plugin.Name)]
					private static extern IntPtr Mci_SequencerStatusPort(ulong DeviceIndex);

					[DllImport(Plugin.Name)]
					private static extern IntPtr Mci_SequencerStatusPosition(ulong DeviceIndex);

					[DllImport(Plugin.Name)]
					private static extern IntPtr Mci_SequencerStatusPositionTrack(ulong DeviceIndex, string TrackNumber);

					[DllImport(Plugin.Name)]
					private static extern IntPtr Mci_SequencerStatusReady(ulong DeviceIndex);

					[DllImport(Plugin.Name)]
					private static extern IntPtr Mci_SequencerStatusSlave(ulong DeviceIndex);

					[DllImport(Plugin.Name)]
					private static extern IntPtr Mci_SequencerStatusStartPosition(ulong DeviceIndex);

					[DllImport(Plugin.Name)]
					private static extern IntPtr Mci_SequencerStatusTempo(ulong DeviceIndex);

					[DllImport(Plugin.Name)]
					private static extern IntPtr Mci_SequencerStatusTimeFormat(ulong DeviceIndex);

					[DllImport(Plugin.Name)]
					private static extern bool Mci_SequencerStatusIsPlaying(ulong DeviceIndex);

					[DllImport(Plugin.Name)]
					private static extern bool Mci_SequencerStatusIsStopped(ulong DeviceIndex);

					[DllImport(Plugin.Name)]
					private static extern bool Mci_SequencerStatusIsPaused(ulong DeviceIndex);

					public static string CurrentTrack(ulong DeviceIndex)
					{
					return Marshal.PtrToStringUni(Mci_SequencerStatusCurrentTrack(DeviceIndex));
					}

					public static string DivisionType(ulong DeviceIndex)
					{
					return Marshal.PtrToStringUni(Mci_SequencerStatusDivisionType(DeviceIndex));
					}

					public static string Length(ulong DeviceIndex)
					{
					return Marshal.PtrToStringUni(Mci_SequencerStatusLength(DeviceIndex));
					}

					public static string LengthTrack(ulong DeviceIndex, string TrackNumber)
					{
					return Marshal.PtrToStringUni(Mci_SequencerStatusLengthTrack(DeviceIndex, TrackNumber));
					}

					public static string MediaPresent(ulong DeviceIndex)
					{
					return Marshal.PtrToStringUni(Mci_SequencerStatusMediaPresent(DeviceIndex));
					}

					public static string Mode(ulong DeviceIndex)
					{
					return Marshal.PtrToStringUni(Mci_SequencerStatusMode(DeviceIndex));
					}

					public static string NumberOfTracks(ulong DeviceIndex)
					{
					return Marshal.PtrToStringUni(Mci_SequencerStatusNumberOfTracks(DeviceIndex));
					}

					public static string Offset(ulong DeviceIndex)
					{
					return Marshal.PtrToStringUni(Mci_SequencerStatusOffset(DeviceIndex));
					}

					public static string Port(ulong DeviceIndex)
					{
					return Marshal.PtrToStringUni(Mci_SequencerStatusPort(DeviceIndex));
					}

					public static string Position(ulong DeviceIndex)
					{
					return Marshal.PtrToStringUni(Mci_SequencerStatusPosition(DeviceIndex));
					}

					public static string PositionTrack(ulong DeviceIndex, string TrackNumber)
					{
					return Marshal.PtrToStringUni(Mci_SequencerStatusPositionTrack(DeviceIndex, TrackNumber));
					}

					public static string Ready(ulong DeviceIndex)
					{
					return Marshal.PtrToStringUni(Mci_SequencerStatusReady(DeviceIndex));
					}

					public static string Slave(ulong DeviceIndex)
					{
					return Marshal.PtrToStringUni(Mci_SequencerStatusSlave(DeviceIndex));
					}

					public static string StartPosition(ulong DeviceIndex)
					{
					return Marshal.PtrToStringUni(Mci_SequencerStatusStartPosition(DeviceIndex));
					}

					public static string Tempo(ulong DeviceIndex)
					{
					return Marshal.PtrToStringUni(Mci_SequencerStatusTempo(DeviceIndex));
					}

					public static string TimeFormat(ulong DeviceIndex)
					{
					return Marshal.PtrToStringUni(Mci_SequencerStatusTimeFormat(DeviceIndex));
					}

					public static bool IsPlaying(ulong DeviceIndex)
					{
					return Mci_SequencerStatusIsPlaying(DeviceIndex);
					}

					public static bool IsStopped(ulong DeviceIndex)
					{
					return Mci_SequencerStatusIsStopped(DeviceIndex);
					}

					public static bool IsPaused(ulong DeviceIndex)
					{
					return Mci_SequencerStatusIsPaused(DeviceIndex);
					}
				}
			}

			public static class Error
			{
				[DllImport(Plugin.Name)]
				private static extern void Mci_ErrorManualChecking(bool ManualChecking);
		
				[DllImport(Plugin.Name)]
				private static extern ulong Mci_ErrorCount();
		
				[DllImport(Plugin.Name)]
				private static extern IntPtr Mci_ErrorString(ulong ErrorIndex);
		
				public static void ManualChecking(bool ManualChecking)
				{
				Mci_ErrorManualChecking(ManualChecking);
				}
		
				public static ulong Count()
				{
				return Mci_ErrorCount();
				}
		
				public static string String(ulong ErrorIndex)
				{
				return Marshal.PtrToStringUni(Mci_ErrorString(ErrorIndex));
				}
	
			}
		}
	}
}