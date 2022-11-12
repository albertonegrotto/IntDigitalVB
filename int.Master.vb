Imports System.Web
Partial Public Class int
    Inherits System.Web.UI.MasterPage
    Public usuario As New usuario
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        
        If (Request.IsAuthenticated = False) Then
            Response.Redirect("index.aspx")
        Else
            usuario = CType(Session("usuario"), usuario)
        End If

    End Sub

End Class