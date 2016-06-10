Public Class LowLevelIdentifier
    Public Const VolumeIdentifierOffset As Integer = 480
    Public Const DiskIdentifierOffset As Integer = 400

    Public Const IdentifierMaxLength As Integer = 16

    Public Shared Function ReadIdentifierFromVolume(volumeLetter As Char) As String
        Dim lld = New LowLevelDrive
        lld.OpenVolumeReadWrite(volumeLetter)
        Return ReadIdentifier(lld, VolumeIdentifierOffset)
    End Function

    Public Shared Function ReadIdentifierFromDisk(driveIndex As Integer) As String
        Dim lld = New LowLevelDrive
        lld.OpenDriveRead(driveIndex)
        Return ReadIdentifier(lld, DiskIdentifierOffset)
    End Function

    Public Shared Sub WriteIdentifierToVolume(volumeLetter As Char, identifier As String)
        Dim lld = New LowLevelDrive
        lld.OpenVolumeReadWrite(volumeLetter)
        WriteIdentifier(lld, VolumeIdentifierOffset, identifier)
    End Sub

    Public Shared Sub WriteIdentifierToDrive(driveIndex As Integer, identifier As String)
        Dim lld = New LowLevelDrive
        lld.OpenDriveReadWrite(driveIndex)
        WriteIdentifier(lld, DiskIdentifierOffset, identifier)
    End Sub

    Private Shared Function ReadIdentifier(disk As LowLevelDrive, offset As Integer) As String
        Dim bytes = disk.ReadBytes(0, 512)

        Dim length = 0
        Do While length < IdentifierMaxLength And bytes(offset + length) > 31 And bytes(offset + length) < 127
            length += 1
        Loop
        If length = 0 Then
            Return ""
        Else
            Dim str = Text.Encoding.ASCII.GetString(bytes, offset, length)
            Return str
        End If
    End Function

    Private Shared Sub WriteIdentifier(disk As LowLevelDrive, offset As Integer, identifier As String)
        If identifier Is Nothing Then Throw New ArgumentNullException(identifier)
        If identifier.Length > IdentifierMaxLength Then Throw New ArgumentOutOfRangeException("identifier must be shorter " + IdentifierMaxLength.ToString + " chars")

        Dim strBytes = Text.Encoding.ASCII.GetBytes(identifier)
        Dim bytes = disk.ReadBytes(0, 512)
        Dim length = 0
        Do While length < strBytes.Length
            bytes(offset + length) = strBytes(length)
            length += 1
        Loop
        bytes(offset + length) = 0
        disk.WriteBytes(0, bytes)
    End Sub

End Class
