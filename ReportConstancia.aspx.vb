Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Partial Public Class ReportConstancia
    Inherits System.Web.UI.Page
    Dim cn As New SqlClient.SqlConnection(SqlConex)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Dim sAccion As String
            Dim nCodigo As Integer
            If User.Identity.IsAuthenticated Then
                sAccion = Request.QueryString("accion")
                nCodigo = Request.QueryString("codigo")
                Session.Add("CODIGO", nCodigo)
                LabelIntegrantes.Visible = False
                Dim sinverif As Integer = 0
                Dim cn As SqlClient.SqlConnection = New SqlClient.SqlConnection(SqlConex)
                cn.Open()
                Dim sql As String = "select i.cuil,g.APELLIDO,g.NOMBRE from Integrantes i, REGISDIG g where i.codigoRegistro=" & nCodigo & " and i.cuil=g.CUIL and i.verificado is null"
                Dim Psql As New SqlClient.SqlCommand(sql, cn)
                Dim dr As SqlClient.SqlDataReader = Psql.ExecuteReader
                While dr.Read()
                    sinverif = 1
                End While
                dr.Close()
                cn.Close()
                If sinverif = 1 Then
                    LabelIntegrantes.Visible = True
                    cn.Open()
                    Dim Psqli As New SqlClient.SqlCommand(sql, cn)
                    Dim dri As SqlClient.SqlDataReader = Psqli.ExecuteReader
                    Dim dt As DataTable = New DataTable
                    dt.Load(dri)
                    dri.Close()
                    cn.Close()
                    GridView1.DataSource = dt
                    GridView1.DataBind()
                    BtnConstancia.Enabled = False
                End If
            Else
                Response.Clear()
                Response.Redirect("index.aspx", False)
            End If
        End If
    End Sub

    Protected Sub BtnConstancia_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnConstancia.Click
        Dim wregistro As Integer = Session("CODIGO")
        Dim wreg As String = wregistro.ToString
        Dim wsector As Integer = 0
        Dim sql As String = "select sector from registro where codigo =" & wreg
        cn.Open()
        Dim Psql As New SqlClient.SqlCommand(sql, cn)
        Dim dr As SqlClient.SqlDataReader = Psql.ExecuteReader
        While (dr.Read())
            wsector = dr.GetInt32(0)
        End While
        If wsector = 1 Then
            'SALA DE TEATRO INDEPENDIENTE  
            Dim ReportName As String = "Constancia_Sala.rpt"
            Dim rptDocument As New Constancia_Sala
            rptDocument.Load(ReportName)
            rptDocument.SetParameterValue(0, wregistro)
            rptDocument.SetDatabaseLogon(DBUsername, DBPassword)
            Response.ClearContent()
            Response.ClearHeaders()
            Response.ContentType = "application/pdf"
            Try
                rptDocument.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, True, "Constancia_Sala")
            Catch ex As Exception
            Finally
                Response.Flush()
                Response.Close()
                rptDocument.Dispose()
            End Try
        End If
        If wsector = 2 Or wsector = 3 Or wsector = 4 Then
            'GRUPO 
            Dim ReportName As String = "Constancia_Grupo.rpt"
            Dim rptDocument As New Constancia_Grupo
            rptDocument.Load(ReportName)
            rptDocument.SetParameterValue(0, wregistro)
            rptDocument.SetDatabaseLogon(DBUsername, DBPassword)
            Response.ClearContent()
            Response.ClearHeaders()
            Response.ContentType = "application/pdf"
            Try
                rptDocument.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, True, "Constancia_Grupo")
            Catch ex As Exception
            Finally
                Response.Flush()
                Response.Close()
                rptDocument.Dispose()
            End Try
        End If
        If wsector = 5 Then
            'ASISTENTE(TECNICO)
            Dim ReportName As String = "Constancia_Asistente.rpt"
            Dim rptDocument As New Constancia_Asistente
            rptDocument.Load(ReportName)
            rptDocument.SetParameterValue(0, wregistro)
            rptDocument.SetDatabaseLogon(DBUsername, DBPassword)
            Response.ClearContent()
            Response.ClearHeaders()
            Response.ContentType = "application/pdf"
            Try
                rptDocument.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, True, "Constancia_Asistente")
            Catch ex As Exception
            Finally
                Response.Flush()
                Response.Close()
                rptDocument.Dispose()
            End Try
        End If
        If wsector = 6 Then
            'ESPECTACULO(CONCERTADO)
            Dim ReportName As String = "Constancia_Esp_Con.rpt"
            Dim rptDocument As New Constancia_Esp_Conc
            rptDocument.Load(ReportName)
            rptDocument.SetParameterValue(0, wregistro)
            rptDocument.SetDatabaseLogon(DBUsername, DBPassword)
            Response.ClearContent()
            Response.ClearHeaders()
            Response.ContentType = "application/pdf"
            Try
                rptDocument.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, True, "Constancia_Esp_Conc")
            Catch ex As Exception
            Finally
                Response.Flush()
                Response.Close()
                rptDocument.Dispose()
            End Try
        End If
        If wsector = 7 Then
            'PUBLICACIÓN
            Dim ReportName As String = "Constancia_Publicacion"
            Dim rptDocument As New Constancia_Publicacion
            rptDocument.Load(ReportName)
            rptDocument.SetParameterValue(0, wregistro)
            rptDocument.SetDatabaseLogon(DBUsername, DBPassword)
            Response.ClearContent()
            Response.ClearHeaders()
            Response.ContentType = "application/pdf"
            Try
                rptDocument.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, True, "Constancia_Publicacion")
            Catch ex As Exception
            Finally
                Response.Flush()
                Response.Close()
                rptDocument.Dispose()
            End Try
        End If
        If wsector > 9 Then
            'O.N.G.
            Dim ReportName As String = "Constancia_ong.rpt"
            Dim rptDocument As New Constancia_ong
            rptDocument.Load(ReportName)
            rptDocument.SetParameterValue(0, wregistro)
            rptDocument.SetDatabaseLogon(DBUsername, DBPassword)
            Response.ClearContent()
            Response.ClearHeaders()
            Response.ContentType = "application/pdf"
            Try
                rptDocument.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, True, "Constancia_ong")
            Catch ex As Exception
            Finally
                Response.Flush()
                Response.Close()
                rptDocument.Dispose()
            End Try
        End If
        If wsector = 9 Then
            'EVENTO
            Dim ReportName As String = "Constancia_Evento.rpt"
            Dim rptDocument As New Constancia_Evento
            rptDocument.Load(ReportName)
            rptDocument.SetParameterValue(0, wregistro)
            rptDocument.SetDatabaseLogon(DBUsername, DBPassword)
            Response.ClearContent()
            Response.ClearHeaders()
            Response.ContentType = "application/pdf"
            Try
                rptDocument.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, False, "Constancia_Evento")
            Catch ex As Exception
            Finally
                Response.Flush()
                Response.Close()
                rptDocument.Dispose()
            End Try
        End If
    End Sub
    Protected Sub BtonVolver_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtonVolver.Click
        Response.Clear()
        Response.Redirect("RegistroImpresion.aspx")
        Response.End()
    End Sub
End Class