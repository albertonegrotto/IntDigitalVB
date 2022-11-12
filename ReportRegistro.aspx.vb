Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Partial Public Class ReportRegistro
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

                Dim sEspacios As String
                sEspacios = ""
                lblAclaracion1.Text = "<b>Paso 1:</b> Deberá clickear en el botón de <b>'Responsable'</b> y una vez generado el archivo .pdf, imprimir 2 (DOS) copias de esa planilla</br>"

                If RegistroModulo.GetIdSector(nCodigo) = "5" Or RegistroModulo.GetIdSector(nCodigo) = "8" Or
                    (Convert.ToInt32(RegistroModulo.GetIdSector(nCodigo)) > 12 And Convert.ToInt32(RegistroModulo.GetIdSector(nCodigo)) < 24) Then
                    'Es un Asistente Técnico o una ONG
                    lblAclaracion2.Text = ""
                    imgPointingHand1.Visible = True
                    imgPointingHand2.Visible = False
                    BtnRegistro.Visible = True
                    BtnIntegrantes.Visible = False
                Else
                    'No es un Asistente Técnico ni una ONG
                    lblAclaracion2.Text = "<b>Paso 2:</b> Luego deberá clickear en el botón de <b>'Integrantes'</b> y una vez generado el archivo .pdf, imprimir 2 (DOS) copias de esa planilla</br>"
                    imgPointingHand1.Visible = True
                    imgPointingHand2.Visible = True
                    BtnRegistro.Visible = True
                    BtnIntegrantes.Visible = True
                End If
            Else
                Response.Clear()
                Response.Redirect("index.aspx", False)
            End If
        End If
    End Sub

    Protected Sub BtnRegistro_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnRegistro.Click
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
            'SALAS'
            Dim ReportName As String = "Registro_Sala.rpt"
            Dim rptDocument As New Registro_Sala
            rptDocument.Load(ReportName)
            rptDocument.SetParameterValue(0, wregistro)
            rptDocument.SetDatabaseLogon(DBUsername, DBPassword)
            Response.ClearContent()
            Response.ClearHeaders()
            Response.ContentType = "application/pdf"
            Try
                rptDocument.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, True, "Registro_Sala")
            Catch ex As Exception
            Finally
                Response.Flush()
                Response.Close()
                rptDocument.Dispose()
            End Try
        End If
        If wsector = 2 Or wsector = 3 Or wsector = 4 Then
            'GRUPO 
            Dim ReportName As String = "Registro_Grupo.rpt"
            Dim rptDocument As New Registro_Grupo
            rptDocument.Load(ReportName)
            rptDocument.SetParameterValue(0, wregistro)
            rptDocument.SetDatabaseLogon(DBUsername, DBPassword)
            Response.ClearContent()
            Response.ClearHeaders()
            Response.ContentType = "application/pdf"
            Try
                rptDocument.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, True, "Registro_Grupo")
            Catch ex As Exception
            Finally
                Response.Flush()
                Response.Close()
                rptDocument.Dispose()
            End Try
        End If
        If wsector = 5 Then
            'ASISTENTE(TECNICO)
            Dim ReportName As String = "Registro_Asistente.rpt"
            Dim rptDocument As New Registro_Asistente
            rptDocument.Load(ReportName)
            rptDocument.SetParameterValue(0, wregistro)
            rptDocument.SetDatabaseLogon(DBUsername, DBPassword)
            Response.ClearContent()
            Response.ClearHeaders()
            Response.ContentType = "application/pdf"
            Try
                rptDocument.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, True, "Registro_Asistente")
            Catch ex As Exception
            Finally
                Response.Flush()
                Response.Close()
                rptDocument.Dispose()
            End Try
        End If
        If wsector = 6 Then
            'ESPECTACULO(CONCERTADO)
            Dim ReportName As String = "Registro_Espec_Con.rpt"
            Dim rptDocument As New Registro_Espec_Con
            rptDocument.Load(ReportName)
            rptDocument.SetParameterValue(0, wregistro)
            rptDocument.SetDatabaseLogon(DBUsername, DBPassword)
            Response.ClearContent()
            Response.ClearHeaders()
            Response.ContentType = "application/pdf"
            Try
                rptDocument.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, True, "Registro_Espec_Con")
            Catch ex As Exception
            Finally
                Response.Flush()
                Response.Close()
                rptDocument.Dispose()
            End Try
        End If
        If wsector = 7 Then
            'PUBLICACIÓN
            Dim ReportName As String = "Registro_Publicacion.rpt"
            Dim rptDocument As New Registro_Publicacion
            rptDocument.Load(ReportName)
            rptDocument.SetParameterValue(0, wregistro)
            rptDocument.SetDatabaseLogon(DBUsername, DBPassword)
            Response.ClearContent()
            Response.ClearHeaders()
            Response.ContentType = "application/pdf"
            Try
                rptDocument.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, True, "Registro_Publicacion")
            Catch ex As Exception
            Finally
                Response.Flush()
                Response.Close()
                rptDocument.Dispose()
            End Try
        End If
        If wsector = 8 Then
            'O.N.G.
            Session("SECTOR") = wsector.ToString
            Dim ReportName As String = "Registro_ong.rpt"
            Dim rptDocument As New Registro_ong
            rptDocument.Load(ReportName)
            rptDocument.SetParameterValue(0, wregistro)
            rptDocument.SetDatabaseLogon(DBUsername, DBPassword)
            Response.ClearContent()
            Response.ClearHeaders()
            Response.ContentType = "application/pdf"
            rptDocument.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, True, "Registro_ong")
            Response.Flush()
            Response.Close()
        End If
        If wsector = 9 Then
            'EVENTO
            Dim ReportName As String = "Registro_Evento.rpt"
            Dim rptDocument As New Registro_Evento
            rptDocument.Load(ReportName)
            rptDocument.SetParameterValue(0, wregistro)
            rptDocument.SetDatabaseLogon(DBUsername, DBPassword)
            Response.ClearContent()
            Response.ClearHeaders()
            Response.ContentType = "application/pdf"
            Try
                rptDocument.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, True, "Registro_Evento")
            Catch ex As Exception
            Finally
                Response.Flush()
                Response.Close()
                rptDocument.Dispose()
            End Try
        End If
        If wsector > 9 Then
            'O.N.G.
            Session("SECTOR") = wsector.ToString
            Dim ReportName As String = "Registro_ong.rpt"
            Dim rptDocument As New Registro_ong
            rptDocument.Load(ReportName)
            rptDocument.SetParameterValue(0, wregistro)
            rptDocument.SetDatabaseLogon(DBUsername, DBPassword)
            Response.ClearContent()
            Response.ClearHeaders()
            Response.ContentType = "application/pdf"
            rptDocument.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, True, "Registro_ong")
            Response.Flush()
            Response.Close()
        End If

    End Sub

    Protected Sub ButIntegrantes_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnIntegrantes.Click
        Dim wregistro As Integer = Session("CODIGO")
        Dim ReportName As String = "Integran.rpt"
        Dim rptDocument As New Integran
        rptDocument.Load(ReportName)
        rptDocument.SetParameterValue(0, wregistro)
        rptDocument.SetDatabaseLogon(DBUsername, DBPassword)
        Response.ClearContent()
        Response.ClearHeaders()
        Response.ContentType = "application/pdf"
        Try
            rptDocument.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, True, "Integran")
        Catch ex As Exception
        Finally
            Response.Flush()
            Response.Close()
            rptDocument.Dispose()
        End Try
    End Sub


    Protected Sub BtonVolver_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtonVolver.Click
        Response.Clear()
        Response.Redirect("menuFinal.aspx")
        Response.End()
    End Sub
End Class