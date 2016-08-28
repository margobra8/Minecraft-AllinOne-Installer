Public Class Form1

    Dim Link As String = "https://s3.amazonaws.com/Minecraft.Download/versions/"
    Dim linkdescriptivo As String = "http://pastebin.com/raw/=YZsf2xik"
    Dim linkVersion As String = "http://pastebin.com/raw/BdewabG6"
    Dim linkDescarga As String = "http://pastebin.com/raw/xypUDNLC"

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim temporal As String = System.IO.Path.GetTempPath()
        Dim VersionActual As String = My.Application.Info.Version.ToString

        My.Computer.Network.DownloadFile((linkVersion), (temporal & "\version.txt"), "", "", False, 2000, True)
        My.Computer.Network.DownloadFile((linkDescarga), (temporal & "\link.txt"), "", "", False, 2000, True)
        My.Computer.Network.DownloadFile((linkdescriptivo), (temporal & "\textodescriptivo.txt"), "", "", False, 2000, True)

        Dim leerVersion As String
        leerVersion = My.Computer.FileSystem.ReadAllText(temporal & "\version.txt")

        Dim leerEnlace As String
        leerEnlace = My.Computer.FileSystem.ReadAllText(temporal & "\link.txt")

        Dim textodescriptivo As String
        textodescriptivo = My.Computer.FileSystem.ReadAllText(temporal & "\textodescriptivo.txt")

        Label5.Text = ("Versión: " & VersionActual)

        If (leerVersion > VersionActual) Then
            Dim alerta As String
            alerta = MsgBox("Hay una nueva versión disponible: v" & leerVersion & vbNewLine & vbNewLine & textodescriptivo & vbNewLine & vbNewLine & "¿Descargar ahora?", vbOKCancel, "Actualizar")

            If (alerta = vbOK) Then
                WebBrowser1.Navigate(leerEnlace)

            End If
        End If

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

        With FolderBrowserDialog1
            .RootFolder = Environment.SpecialFolder.Desktop

            .Description = "Selecciona el destino de la carpeta .minecraft"

            .ShowNewFolderButton = True

            If .ShowDialog = Windows.Forms.DialogResult.OK Then
                TextBox1.Text = .SelectedPath
            End If

        End With

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click

        CheckForIllegalCrossThreadCalls = False
        Dim version As String = ComboBox1.SelectedItem
        Dim ruta As String = TextBox1.Text

        If My.Computer.Network.IsAvailable = True Then
        Else
            MsgBox("¡Necesitas una conexión a Internet para continuar!", MsgBoxStyle.Critical, "Error")
            Return
        End If

        If ComboBox1.SelectedItem = Nothing Then
            MsgBox("¡Necesitas seleccionar una versión para continuar!", MsgBoxStyle.Critical, "Error")
            Return
        End If

        If TextBox1.Text = Nothing Then
            MsgBox("¡Necesitas seleccionar una ruta de destino para continuar!", MsgBoxStyle.Critical, "Error")
            Return
        End If

        BackgroundWorker1.RunWorkerAsync()

    End Sub

    Private Sub BackgroundWorker1_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork

        Dim version As String = ComboBox1.SelectedItem
        Dim ruta As String = TextBox1.Text

        My.Computer.Network.DownloadFile((Link & version & "/" & version & ".jar"), (ruta & "\.minecraft\versions\" & version & "\" & version & ".jar"), "", "", True, 2000, True)
        My.Computer.Network.DownloadFile((Link & version & "/" & version & ".json"), (ruta & "\.minecraft\versions\" & version & "\" & version & ".json"), "", "", True, 2000, True)

        Dim MaxRam As String = ("-Xmx6G")
        Dim MinRam As String = ("-Xmn128M")
        Dim javaD As String = ""
        Dim javaA As String = ""

        Dim file As String = (ruta & "\.minecraft\launcher_profiles.json")

        Dim i As Integer
        Dim aryText(21) As String
        aryText(0) = "{"
        aryText(1) = "  ""profiles"": {"
        aryText(2) = "    ""Minecraft " & version & " - Vanilla"": {"
        aryText(3) = "      ""name"": ""Minecraft " & version & " - Vanilla"","
        aryText(4) = "      ""lastVersionId"": """ & version & ""","
        aryText(5) = "      ""javaDir"": """ & javaD & ""","
        aryText(6) = "      ""javaArgs"": """ & MaxRam & " -XX:+UseConcMarkSweepGC -XX:+CMSIncrementalMode -XX:-UseAdaptiveSizePolicy " & javaA & " " & MinRam & ""","
        aryText(7) = "      ""resolution"": {"
        aryText(8) = "        ""width"": """","
        aryText(9) = "        ""height"": """""
        aryText(10) = "      }"
        aryText(11) = "    }"
        aryText(12) = "  },"
        aryText(13) = "  ""selectedProfile"": ""Minecraft " & version & " - Vanilla"","
        aryText(14) = "  ""clientToken"": ""1446a366-9bc2-494f-a999-c17091f980b5"","
        aryText(15) = "  ""authenticationDatabase"": {},"
        aryText(16) = "  ""selectedUser"": ""d5d2e4a55e7f4802b381a231d8aafd34"","
        aryText(17) = "  ""launcherVersion"": {"
        aryText(18) = "    ""name"": ""1.6.11"","
        aryText(19) = "    ""format"": 17"
        aryText(20) = "  }"
        aryText(21) = "}"

        Dim objWriter As New System.IO.StreamWriter(file)
        For i = 0 To 21
            objWriter.WriteLine(aryText(i))
        Next
        objWriter.Close()

        Dim instalar As String
        instalar = MsgBox("¡Descarga realizada correctamente!" & vbNewLine & vbNewLine & "Para instalar la versión de minecraft copia la carpeta "".minecraft"" que generaste y pégala en la siguiente carpeta, o si lo prefieres simplemente déjala guardada y no la copies." & vbNewLine & vbNewLine & "¡CUIDADO! Si tienes una carpeta "".minecraft"" previamente creada y pegas la nueva, la sobreescribirás.", vbOKOnly, "Completar Instalación")

        If (instalar = vbOK) Then
            Dim appdata As String
            appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)
            Process.Start("explorer.exe", appdata)
        End If


    End Sub

    Private Sub ToolStripButton2_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Form2.ShowDialog()
    End Sub

    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click

        Dim temporal As String = System.IO.Path.GetTempPath()
        Dim VersionActual As String = My.Application.Info.Version.ToString

        My.Computer.Network.DownloadFile((linkVersion), (temporal & "\version.txt"), "", "", False, 2000, True)
        My.Computer.Network.DownloadFile((linkDescarga), (temporal & "\link.txt"), "", "", False, 2000, True)
        My.Computer.Network.DownloadFile((linkdescriptivo), (temporal & "\textodescriptivo.txt"), "", "", False, 2000, True)

        Dim leerVersion As String
        leerVersion = My.Computer.FileSystem.ReadAllText(temporal & "\version.txt")

        Dim leerEnlace As String
        leerEnlace = My.Computer.FileSystem.ReadAllText(temporal & "\link.txt")

        Dim textodescriptivo As String
        textodescriptivo = My.Computer.FileSystem.ReadAllText(temporal & "\textodescriptivo.txt")

        If (leerVersion = VersionActual) Then
            MsgBox("La versión más reciente está instalada.", MsgBoxStyle.Information, "Actualizar")
            Return
        End If

        If (leerVersion > VersionActual) Then
            Dim alerta As String
            alerta = MsgBox("Hay una nueva versión disponible: v" & leerVersion & vbNewLine & vbNewLine & textodescriptivo & vbNewLine & vbNewLine & "¿Descargar ahora?", vbOKCancel, "Actualizar")

            If (alerta = vbOK) Then
                WebBrowser1.Navigate(leerEnlace)

            End If
        End If

    End Sub

    Private Sub Label5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub Label1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label1.Click

    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged

    End Sub

    Private Sub Label3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label3.Click

    End Sub

    Private Sub ToolStripButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton2.Click
        Form2.ShowDialog()
    End Sub

    Private Sub ToolStripButton3_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton3.Click
        Form3.ShowDialog()
    End Sub
End Class

