Public Class Site2
    Inherits System.Web.UI.MasterPage
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Me.Page.User.Identity.IsAuthenticated Then
            CerrarSesion.Visible = True
        Else
            CerrarSesion.Visible = False
        End If

        Dim controla As String
        controla = Request.Params("__EVENTTARGET")
        If controla = "CierraSession" Then
            FormsAuthentication.SignOut()
            Session.Abandon()
            Response.Redirect("index.aspx")
        End If
    End Sub

End Class