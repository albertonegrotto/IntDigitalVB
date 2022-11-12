Public Partial Class registroImpresion
    Inherits System.Web.UI.Page
    Dim quien As usuario
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim sRedirect As String
        Dim sURL As String

        If Not Page.IsPostBack Then
            If User.Identity.IsAuthenticated Then
                SetearVariablesSession()
                If Not Session("USER_ID") Is Nothing Then
                    SqlDataSource1.SelectParameters("codigo").DefaultValue = Session("USER_ID")
                    GridView1.DataBind()
                    GridView1.Visible = True
                Else
                    sRedirect = "registroImpresion.aspx"
                    sURL = MAIL_WEB_SERVER & MAIL_LOGIN_PAGE & "?redirect="
                    sRedirect = System.Web.HttpUtility.UrlEncode(sRedirect)
                    sURL += sRedirect & "&from=r"
                    Response.Redirect(sURL)

                End If
            Else
                'Response.Clear()
                'Response.Redirect("http://www.inteatro.gob.ar", False)
            End If
        End If

    End Sub
    Private Sub SetearVariablesSession()
        quien = CType(Session("usuario"), usuario)
        Session("USER_ID") = quien.Codigo
    End Sub
    Private Function GetURL(ByVal nSector As Integer) As String
        Select Case nSector
            Case 1
                Return "registroTeatro.aspx"
            Case 2, 3, 4
                Return "registroGrupo.aspx"
            Case 5
                Return "registroAsistenteTecnico.aspx"
            Case 6
                Return "registroEspectaculo.aspx"
            Case 7
                Return "registroPublicacion.aspx"
            Case 8
                Return "registroONG.aspx"
            Case 9
                Return "registroEvento.aspx"
            Case Else
                Return ""
        End Select
    End Function

    Private Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound
        Dim nSector As Integer
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim hlValidacion As HyperLink = TryCast(e.Row.FindControl("lnkValidacion"), HyperLink)
            Dim hlConstancia As HyperLink = TryCast(e.Row.FindControl("lnkConstancia"), HyperLink)
            'nSector = DataBinder.Eval(e.Row.DataItem, "sector")
            'e.Row.Cells(1).Text = Funciones.GetNombreSector(nSector)
            'If e.Row.RowType = DataControlRowType.DataRow Then
            '    e.Row.Cells(0).Text = Funciones.GetNombreSector(nSector)
            'End If
            'Si el campo registro está en NULL
            'If DataBinder.Eval(e.Row.DataItem, "registro").Equals(DBNull.Value) Then
            '    hlConstancia.Visible = False
            'Else
            '    hlConstancia.Visible = True
            'End If
            hlValidacion.Text = "Ir"
            hlValidacion.NavigateUrl = "reportRegistro.aspx?accion=p&codigo=" & DataBinder.Eval(e.Row.DataItem, "codigo")
            hlConstancia.Text = "Ir"
            hlConstancia.NavigateUrl = "reportConstancia.aspx?accion=p&codigo=" & DataBinder.Eval(e.Row.DataItem, "codigo")
        End If
    End Sub

End Class