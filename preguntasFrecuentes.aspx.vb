Public Partial Class preguntasFrecuentes
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("usuario") Is Nothing Then           
            Dim MisDatos As HtmlAnchor = Nothing
            MisDatos = CType(Master.FindControl("lkMisDatos"), HtmlAnchor)
            MisDatos.Visible = False
        End If
    End Sub

    Private Sub btnVolver_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnVolver.Click
        If Not Session("usuario") Is Nothing Then
            Response.Redirect("menuFinal.aspx")
        Else
            Response.Redirect("index.aspx")
        End If
    End Sub
End Class