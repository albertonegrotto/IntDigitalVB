Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Partial Public Class Integra
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If User.Identity.IsAuthenticated Then
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
                rptDocument.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, False, "Integran")
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