Public Partial Class ConfirmaIndiv
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If User.Identity.IsAuthenticated Then
                Dim wcodigo As Integer = Session("wsolicitud_")
                Label7.Text = wcodigo.ToString
                'Else
                '    Response.Clear()
                '    Response.Redirect("http://www.inteatro.gob.ar", False)
            End If
        End If
    End Sub

    Protected Sub BtnEnviar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnEnviar.Click
        Response.Clear()
        Response.Redirect("index.aspx", False)
    End Sub
End Class