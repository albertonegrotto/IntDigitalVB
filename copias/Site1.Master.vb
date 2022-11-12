Partial Public Class Site1
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'Dim oContent As ContentPlaceHolder
        'Dim oHyperlink As HyperLink

        'oContent = Page.Master.FindControl("MainContentPlaceHolder")
        'oHyperlink = oContent.FindControl("HyperLinkBack")
        'If Not oHyperlink Is Nothing Then
        '    oHyperlink.NavigateUrl = Request.ServerVariables("HTTP_REFERER")
        'End If

        If Session.Item("id_user") Is Nothing Then
            Response.Clear()
            Response.Redirect("http://www.inteatro.gob.ar", False)
            lnkImpresion.Visible = False
            'lnkConstancias.Visible = False
        Else
            If Not Session("USER_ID") Is Nothing Then
                lnkImpresion.NavigateUrl = "registroImpresion.aspx?r=" & Session("USER_ID")
                lnkImpresion.Text = "Menú de Impresión"
                lnkImpresion.Visible = True

                lnkConstancias.NavigateUrl = "registroImpresion.aspx?r=" & Session("USER_ID")
                lnkConstancias.Text = "Menú de Impresión"
                lnkConstancias.Visible = True
            Else
                lnkImpresion.Visible = False
                'lnkConstancias.Visible = False
            End If
        End If

        If Not Page.IsPostBack Then
        End If

    End Sub

    'Protected Sub SetLogoutMenu()
    '    If Not (Session("CUIT") Is Nothing) Then
    '        'Con sesión
    '        lnkLogout.NavigateUrl = "logout.aspx"
    '        lnkLogout.Text = "Logout"
    '    End If
    'End Sub

    'Protected Sub BtnSalida_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnSalida.Click
    '    Response.Clear()
    '    Response.Redirect("http://www.inteatro.gob.ar", False)
    'End Sub

    'Protected Sub BtnCarga_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnCarga.Click
    '    Response.Clear()
    '    Response.Redirect("AltaIni.aspx", False)
    'End Sub

    'Protected Sub BtnConsul_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnConsul.Click
    '    Response.Clear()
    '    Response.Redirect("LoginInicio.aspx", False)
    'End Sub
End Class