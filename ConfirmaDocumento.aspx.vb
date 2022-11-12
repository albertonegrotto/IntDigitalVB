Public Class ConfirmaDocumento
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            'If User.Identity.IsAuthenticated Then
            '    'BtnEnviar.Enabled = False
            'Else
            '    Response.Clear()
            '    Response.Redirect("http://www.inteatro.gob.ar", False)
            'End If
        End If
    End Sub
    Protected Sub BtnEnviar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnEnviar.Click
        Response.Clear()
        Response.Redirect("AdjuntosLista.aspx", False)
    End Sub

End Class