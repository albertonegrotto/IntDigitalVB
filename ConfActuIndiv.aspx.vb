Public Partial Class ConfActuIndiv
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then
            If User.Identity.IsAuthenticated Then
                Dim wcodigo As Integer = Session("wsolicitud_")
                Dim cod As String = Request.QueryString("u")
                LabelSoli.Text = cod
                ' LabelSoli.Text = wcodigo.ToString
            Else
                Response.Clear()
                Response.Redirect("index.aspx", False)
            End If
        End If
    End Sub

    Protected Sub BtnEnviar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnEnviar.Click
        Response.Clear()
        Response.Redirect("menuFinal.aspx", False)
    End Sub
End Class