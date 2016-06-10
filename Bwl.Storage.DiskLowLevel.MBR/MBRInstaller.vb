Public Class MBRInstaller
    Dim drive = New LowlevelDrive
    Private Sub frmMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim loaders = IO.Directory.GetFiles(Application.StartupPath + "\..\loaders\")
        For Each file In loaders
            Dim info As New IO.FileInfo(file)
            cbLoaders.Items.Add(info.Name)
        Next
        cbLoaders.SelectedIndex = 0
    End Sub

    Private Sub bInstallMBR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bInstallMBR.Click
        Dim bytes = IO.File.ReadAllBytes(Application.StartupPath + "\..\loaders\" + cbLoaders.Text)
        Try
            drive.OpenDriveReadWrite(tDriveNumber.Value)
            drive.WriteBytes(0, bytes)
            lResult.Text = "Загрузчик успешно установлен (" + bytes.Length.ToString + " байт)"
        Catch ex As Exception
            lResult.Text = "Ошибка! " + ex.Message
        End Try
    End Sub

    Private Sub bSelectProgram_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bSelectProgram.Click
        If openFile.ShowDialog() = Windows.Forms.DialogResult.OK Then
            tbProgram.Text = openFile.FileName
        End If
    End Sub

    Private Sub bInstallProgram_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bInstallProgram.Click
        If tbProgram.Text > "" Then
            Dim bytes = IO.File.ReadAllBytes(tbProgram.Text)
            Dim drive = New LowlevelDrive
            Try
                drive.OpenDriveReadWrite(tDriveNumber.Value)
                drive.WriteBytes(512, bytes)
                lResult.Text = "Программа успешно установлена (" + bytes.Length.ToString + " байт)"
            Catch ex As Exception
                lResult.Text = "Ошибка! " + ex.Message
            End Try
        End If
    End Sub
End Class
