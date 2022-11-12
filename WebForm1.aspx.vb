Public Partial Class WebForm1
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub BtnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnGuardar.Click
        Dim i As Integer
        Dim dInicio As DateTime
        Dim dfin As DateTime
        Dim resultado As Integer





        While True
            dInicio = Convert.ToDateTime("1965/10/30")
            dfin = Convert.ToDateTime("1966/10/30")

            resultado = DateDiff("yyyy", dInicio, dfin)

            i = i
        End While

    End Sub


End Class