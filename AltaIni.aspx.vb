Public Partial Class AltaIni
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then
            If User.Identity.IsAuthenticated Then
                'Con sesión Iniciada
            Else
                Response.Clear()
                Response.Redirect("http://www.inteatro.gob.ar", False)
            End If
        End If
    End Sub

End Class