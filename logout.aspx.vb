Public Partial Class logout
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            FormsAuthentication.SignOut()
            Session.Abandon()

            Response.Redirect("index.aspx", False)
        End If
    End Sub

End Class