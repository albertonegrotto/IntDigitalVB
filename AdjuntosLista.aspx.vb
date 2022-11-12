Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Public Class AdjuntosLista
    Inherits System.Web.UI.Page
    Dim quien As usuario
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If User.Identity.IsAuthenticated Then
                SetearVariablesSession()
                Inicializar()
                SqlDataSource1.SelectParameters("codigo").DefaultValue = Session("USER_ID")

                GridView1.DataBind()
                GridView1.Visible = True
            Else
                Response.Clear()
                Response.Redirect("http://www.inteatro.gob.ar", False)
            End If
        End If
    End Sub

    Private Sub SetearVariablesSession()
        quien = CType(Session("usuario"), usuario)
        Session("USER_ID") = quien.Codigo
        Session("ID_USER") = quien.Usuario
    End Sub

    Private Sub Inicializar()
        Dim cn As New SqlClient.SqlConnection(SqlConex)
        Dim sql1 As String = ""
    End Sub

    Private Function GetURL(ByVal nSector As Integer) As String
        Select Case nSector
            Case 0
                Return "ActualizaCVPersona.aspx"
            Case 1
                Return "ActuDocSala.aspx"
            Case 2, 3, 4
                Return "ActuDocGrupo.aspx"
            Case 5
                Return "ActuDocAsistente.aspx"
            Case Else
                Return ""
        End Select
    End Function

    Private Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound
        Dim nSector As Integer
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim hl As HyperLink = TryCast(e.Row.FindControl("HyperLinkModificar"), HyperLink)
            nSector = DataBinder.Eval(e.Row.DataItem, "sector")
            If e.Row.RowType = DataControlRowType.DataRow Then
                e.Row.Cells(4).Text = Funciones.GetNombreSector(nSector)
                'e.Row.Font.Bold = True
            End If
            hl.Text = "Ir"
            hl.NavigateUrl = GetURL(nSector) & "?"
            Select Case nSector
                Case Is = 0
                    'hl.Text = "Datos Personales"
                Case Is = 1
                    'hl.Text = "Sala de Teatro Independiente"
                Case Is = 2
                    'hl.Text = "Grupo de Teatro Independiente"
                Case Is = 3
                    'hl.Text = "Grupo Comunitario"
                Case Is = 4
                    'hl.Text = "Grupo Vocacional"
                Case Is = 5
                    'hl.Text = "Asistente Técnico"
                Case Else
                    hl.NavigateUrl = "*"
                    hl.Text = ""
            End Select
            If hl.Text <> "" Then
                hl.NavigateUrl += "accion=M&"
                hl.NavigateUrl += "codigo=" & DataBinder.Eval(e.Row.DataItem, "codigo")
            End If
        End If
    End Sub

End Class