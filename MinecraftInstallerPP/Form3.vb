Public Class Form3

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim registrolink As String = "http://bit.do/registrominecraft"
        Process.Start(registrolink)
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim instalarlink As String = "http://bit.do/instalarminecraft"
        Process.Start(instalarlink)
    End Sub
End Class