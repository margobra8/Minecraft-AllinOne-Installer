Public Class Form2

    Private Sub Form2_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim VersionActual As String = My.Application.Info.Version.ToString
        Label6.Text = ("versión: " & VersionActual)
    End Sub
End Class