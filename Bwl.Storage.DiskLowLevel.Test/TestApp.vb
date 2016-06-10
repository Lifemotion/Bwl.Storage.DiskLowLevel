Imports Bwl.Framework

Module TestApp
    Private _app As New AppBase
    Private _disk As New LowlevelDrive
    Private _ascii As System.Text.Encoding = System.Text.Encoding.ASCII
    Private WithEvents _testRead As New AutoButton(_app.AutoUI, "Test Read Volumes")
    Private WithEvents _testRead2 As New AutoButton(_app.AutoUI, "Test Read Disks")

    Private WithEvents _readIdentifier As New AutoButton(_app.AutoUI, "Read Identifier Volume")
    Private WithEvents _writeIdentifier As New AutoButton(_app.AutoUI, "Write Identifier Volume")

    Private WithEvents _readIdentifier2 As New AutoButton(_app.AutoUI, "Read Identifier Disk")
    Private WithEvents _writeIdentifier2 As New AutoButton(_app.AutoUI, "Write Identifier Disk")

    Public Sub Main()
        Application.EnableVisualStyles()
        Application.Run(AutoUIForm.Create(_app))
    End Sub

    Private Sub _testRead_Click(source As AutoButton) Handles _testRead.Click
        Dim drives = My.Computer.FileSystem.Drives
        For Each drive In drives
            Try
                _app.RootLogger.AddMessage(drive.Name)
                _disk.OpenVolumeReadWrite(drive.Name(0))
                Dim result = _disk.ReadBytes(0, 512)
                _app.RootLogger.AddMessage(_ascii.GetString(result))
            Catch ex As Exception
                _app.RootLogger.AddWarning(ex.Message)
            End Try
        Next
    End Sub

    Private Sub _testRead2_Click(source As AutoButton) Handles _testRead2.Click
        Try
            '   _app.RootLogger.AddMessage(drive.Name)
            _disk.OpenDriveReadWrite(0)
            Dim result = _disk.ReadBytes(0, 512)
            _app.RootLogger.AddMessage(_ascii.GetString(result))
        Catch ex As Exception
            _app.RootLogger.AddWarning(ex.Message)
        End Try
    End Sub

    Private Sub _readIdentifier_Click(source As AutoButton) Handles _readIdentifier.Click
        Try
            Dim letter = InputBox("Letter: ")
            _app.RootLogger.AddMessage("""" + LowLevelIdentifier.ReadIdentifierFromVolume(letter(0)) + """")
        Catch ex As Exception
            _app.RootLogger.AddWarning(ex.Message)
        End Try
    End Sub

    Private Sub _writeIdentifier_Click(source As AutoButton) Handles _writeIdentifier.Click
        Dim letter = InputBox("Letter: ")
        Dim ident = InputBox("Identifier: ")
        Try
            LowLevelIdentifier.WriteIdentifierToVolume(letter(0), ident)
        Catch ex As Exception
            _app.RootLogger.AddWarning(ex.Message)
        End Try
    End Sub

    Private Sub _readIdentifier2_Click(source As AutoButton) Handles _readIdentifier2.Click
        Try
            Dim number = CInt(Val(InputBox("DiskNumber: ",, "1")))
            _app.RootLogger.AddMessage("""" + LowLevelIdentifier.ReadIdentifierFromDisk(number) + """")
        Catch ex As Exception
            _app.RootLogger.AddWarning(ex.Message)
        End Try
    End Sub

    Private Sub _writeIdentifier2_Click(source As AutoButton) Handles _writeIdentifier2.Click
        Dim number = CInt(Val(InputBox("DiskNumber: ",, "1")))
        Dim ident = InputBox("Identifier: ")
        Try
            LowLevelIdentifier.WriteIdentifierToDrive(number, ident)
        Catch ex As Exception
            _app.RootLogger.AddWarning(ex.Message)
        End Try
    End Sub
End Module
