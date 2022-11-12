Public Partial Class confirmaMail
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Dim wcontrol As String = Request.QueryString.Get("m")
            If wcontrol IsNot Nothing Then
                lblResultado.Text = wcontrol
            End If
            If Not Session("usuario") Is Nothing Then
                HyperLinkBack.NavigateUrl = "MenuFinal.aspx"
            Else
                HyperLinkBack.NavigateUrl = "Index.aspx"
            End If
        End If
    End Sub

    Private Sub BtnEnviar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnEnviar.Click
        If Not Session("usuario") Is Nothing Then
            Response.Clear()
            Response.Redirect("MenuFinal.aspx")
        Else
            Response.Clear()
            Response.Redirect("Index.aspx")
        End If
    End Sub

End Class