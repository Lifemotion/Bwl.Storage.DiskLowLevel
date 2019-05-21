Imports System.Runtime.InteropServices
Imports Microsoft.Win32.SafeHandles

Public Class LowLevelDrive
#Region "Win32Imports"

    <DllImport("kernel32", SetLastError:=True)>
    Private Shared Function DeviceIoControl(
            ByVal hVol As SafeFileHandle,
            ByVal controlCode As UInteger,
            ByVal inBuffer As IntPtr,
            ByVal inBufferSize As Integer,
            ByRef outBuffer As DiskExtents,
            ByVal outBufferSize As Integer,
            ByRef bytesReturned As Integer,
            ByVal overlapped As IntPtr) As <MarshalAs(UnmanagedType.Bool)> Boolean
    End Function

    <DllImport("kernel32", SetLastError:=True)>
    Private Shared Function DeviceIoControl(
            ByVal hVol As SafeFileHandle,
            ByVal controlCode As UInteger,
            ByVal inBuffer As IntPtr,
            ByVal inBufferSize As Integer,
            ByVal outBuffer As IntPtr,
            ByVal outBufferSize As Integer,
            ByRef bytesReturned As Integer,
            ByVal overlapped As IntPtr) As <MarshalAs(UnmanagedType.Bool)> Boolean
    End Function

    <DllImport("kernel32", SetLastError:=True)>
    Private Shared Function SetFilePointer(
            ByVal hFile As SafeFileHandle,
            ByVal lDistanceToMove As Integer,
            ByRef lpDistanceToMoveHigh As Integer,
            ByVal dwMoveMethod As Integer) As UInteger
    End Function

    ' you must write the whole inBuffer.
    <DllImport("kernel32", SetLastError:=True, CharSet:=CharSet.Ansi)>
    Private Shared Function WriteFile(
          ByVal hVol As SafeFileHandle,
          <MarshalAs(UnmanagedType.LPArray, SizeParamIndex:=3)>
          ByVal inBuffer() As Byte,
          ByVal numberOfBytesToWrite As Integer,
          ByRef numberOfBytesWritten As Integer,
          ByVal overlapped As IntPtr) As <MarshalAs(UnmanagedType.Bool)> Boolean
    End Function
    <DllImport("kernel32", CharSet:=CharSet.Unicode, SetLastError:=True)>
    Private Shared Function CreateFile(
            ByVal fileName As String,
            ByVal desiredAccess As UInteger,
            ByVal shareMode As UInteger,
            ByVal securityAttributes As IntPtr,
            ByVal creationDisposition As UInteger,
            ByVal flagsAndAttributes As UInteger,
            ByVal hTemplateFile As IntPtr) As SafeFileHandle
    End Function
    Private Const GenericRead As UInteger = &H80000000UI
    Private Const GenericWrite As UInteger = &H40000000
    Private Const FileShareRead As UInteger = 1
    Private Const Filesharewrite As UInteger = 2
    Private Const OpenExisting As UInteger = 3
    Private Const IoctlVolumeGetVolumeDiskExtents As UInteger = &H560000
    Private Const IncorrectFunction As UInteger = 1
    Private Const ErrorInsufficientBuffer As UInteger = 122
    ' DISK_EXTENT in the msdn.
    <StructLayout(LayoutKind.Sequential)>
    Private Structure DiskExtent
        Public DiskNumber As Integer
        Public StartingOffset As Long
        Public ExtentLength As Long
    End Structure

    ' DISK_EXTENTS
    <StructLayout(LayoutKind.Sequential)>
    Private Structure DiskExtents
        Public numberOfExtents As Integer
        Public first As DiskExtent ' We can't marhsal an array if we don't know its size.
    End Structure
#End Region

    Private _driveHandle As SafeFileHandle
    Private _fileStream As IO.FileStream
    Private _sectorSize As Integer = 512

    Public Sub OpenDriveReadWrite(ByVal driveNumber As Integer)
        _driveHandle = CreateFile("\\.\PHYSICALDRIVE" + driveNumber.ToString, GenericRead Or GenericWrite,
                FileShareRead Or Filesharewrite, IntPtr.Zero, OpenExisting, 0, IntPtr.Zero)
        If Not _driveHandle.IsInvalid Then
            _fileStream = New IO.FileStream(_driveHandle, IO.FileAccess.ReadWrite, 1)
        Else
            _fileStream = Nothing
            Throw New Exception("Не могу открыть диск!")
        End If
    End Sub

    Public Sub OpenVolumeReadWrite(ByVal driveLetter As Char)
        _driveHandle = CreateFile("\\.\" + driveLetter + ":", GenericRead Or GenericWrite,
                FileShareRead Or Filesharewrite, IntPtr.Zero, OpenExisting, 0, IntPtr.Zero)
        If Not _driveHandle.IsInvalid Then
            _fileStream = New IO.FileStream(_driveHandle, IO.FileAccess.ReadWrite, 1)
        Else
            _fileStream = Nothing
            Throw New Exception("Не могу открыть диск!")
        End If
    End Sub

    Public Sub OpenDriveRead(ByVal driveNumber As Integer)
        _driveHandle = CreateFile("\\.\PHYSICALDRIVE" + driveNumber.ToString, GenericRead,
        FileShareRead Or Filesharewrite, IntPtr.Zero, OpenExisting, 0, IntPtr.Zero)
        If Not _driveHandle.IsInvalid Then
            _fileStream = New IO.FileStream(_driveHandle, IO.FileAccess.ReadWrite, 1)
        Else
            _fileStream = Nothing
            Throw New Exception("Не могу открыть диск!")
        End If
    End Sub

    Public Function ReadBytesSectored(ByVal position As Integer, ByVal count As Integer) As Byte()
        If _fileStream Is Nothing Then Throw New Exception("Диск не был открыт!")
        If position Mod _sectorSize > 0 Then Throw New Exception("Позиция не кратна сектору!")
        If count Mod _sectorSize > 0 Then Throw New Exception("Размер массива байт не кратен сектору!")
        Dim bytes(count - 1) As Byte
        Dim seek = _fileStream.Seek(position, IO.SeekOrigin.Begin)
        Dim readCount = _fileStream.Read(bytes, 0, count)
        ReDim Preserve bytes(readCount - 1)
        Return bytes
    End Function

    Public Function ReadBytes(ByVal position As Integer, ByVal count As Integer) As Byte()
        Dim posStart = position - (position Mod _sectorSize)
        Dim posEnd = position + count
        posEnd = posEnd + 512 - (posEnd Mod _sectorSize)
        Dim buffer = ReadBytesSectored(posStart, posEnd - posStart)
        Dim result(count - 1) As Byte
        Array.ConstrainedCopy(buffer, position - posStart, result, 0, count)
        Return result
    End Function

    Public Sub WriteBytesSectored(ByVal position As Integer, ByVal bytes() As Byte)
        If _fileStream Is Nothing Then Throw New Exception("Диск не был открыт!")
        If position Mod _sectorSize > 0 Then Throw New Exception("Позиция не кратна сектору!")
        If bytes.Length Mod _sectorSize > 0 Then Throw New Exception("Размер массива байт не кратен сектору!")
        Dim seek = _fileStream.Seek(position, IO.SeekOrigin.Begin)
        _fileStream.Write(bytes, 0, bytes.Length)
    End Sub

    Public Sub WriteBytes(ByVal position As Integer, ByVal bytes() As Byte)
        Dim posStart = position - (position Mod _sectorSize)
        Dim posEnd = position + bytes.Length
        posEnd = posEnd + 512 - (posEnd Mod _sectorSize)
        Dim buffer = ReadBytesSectored(posStart, posEnd - posStart)
        Array.ConstrainedCopy(bytes, 0, buffer, position - posStart, bytes.Length)
        WriteBytesSectored(posStart, buffer)
    End Sub
End Class
