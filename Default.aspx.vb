Partial Public Class _Default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim SesionDT As String = ""
        Dim sRedirect As String
        Dim sFrom As String
        Dim sURL As String
        Dim sope As Integer
        Dim sreg As Integer

        SesionDT = DateTime.Now.ToString("G")
        Username.Text = SesionDT
        Session.Add("id_user", SesionDT)
        Session.Add("wsolicitud_", 0)
        FormsAuthentication.RedirectFromLoginPage(Username.Text, Remember.Checked)

        If Request.QueryString.Count = 0 Then
            Response.Clear()
            Response.Redirect("index.aspx", False)

        Else
            sRedirect = Request.QueryString("ReturnURL")
            sFrom = Request.QueryString("from")
            sope = sRedirect.IndexOf("o=m")
            sreg = sRedirect.IndexOf("t=r")

            If sFrom = "" Then
                If sope > 0 Then
                    sURL = "Index.aspx?ReturnURL=" & sRedirect
                Else
                    If sreg > 0 Then
                        sRedirect = System.Web.HttpUtility.UrlEncode(sRedirect)
                        sURL = "index.aspx?ReturnURL=" & sRedirect
                    Else
                        sRedirect = System.Web.HttpUtility.UrlEncode(sRedirect)
                        sURL = "Activa.aspx?ReturnURL=" & sRedirect

                    End If

                End If

            ElseIf sFrom = "r" Then
                sRedirect = System.Web.HttpUtility.UrlEncode(sRedirect)
                sURL = "registro.aspx?ReturnURL=" & sRedirect

            End If

            Response.Redirect(sURL)
        End If

    End Sub

    Protected Sub Username_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles Username.TextChanged

    End Sub

    Protected Sub Remember_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles Remember.CheckedChanged

    End Sub
End Class