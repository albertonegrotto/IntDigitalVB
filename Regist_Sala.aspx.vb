Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Partial Public Class Regist_Sala
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If User.Identity.IsAuthenticated Then
            Dim wregistro As Integer = Session("CODIGO")
            Dim ReportName As String = "Registro_Sala.rpt"
            Dim rptDocument As New Registro_Sala
            rptDocument.Load(ReportName)
            rptDocument.SetParameterValue(0, wregistro)
            rptDocument.SetDatabaseLogon(DBUsername, DBPassword)
            Response.ClearContent()
            Response.ClearHeaders()
            Response.ContentType = "application/pdf"
            Try
                rptDocument.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, False, "Registro_Sala")
            Catch ex As Exception
            Finally
                Response.Flush()
                Response.Close()
                rptDocument.Dispose()
            End Try
        Else
            Response.Clear()
            Response.Clear()
            Response.Redirect("http://www.inteatro.gob.ar", False)
        End If
    End Sub
End Class