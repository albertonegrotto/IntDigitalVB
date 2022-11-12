Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Partial Public Class Regist_Asistente
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If User.Identity.IsAuthenticated Then
            Dim wregistro As Integer = Session("CODIGO")
            Dim ReportName As String = "Registro_Asistente.rpt"
            Dim rptDocument As New Registro_Asistente
            rptDocument.Load(ReportName)
            rptDocument.SetParameterValue(0, wregistro)
            rptDocument.SetDatabaseLogon(DBUsername, DBPassword)
            'rptDocument.SetDatabaseLogon(DBUsername, DBPassword, "MZNOTEBOOK", "IntTeatroDig")
            Response.ClearContent()
            Response.ClearHeaders()
            Response.ContentType = "application/pdf"
            Try
                rptDocument.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, False, "Registro_Asistente")
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

    'Sub BindReport()
    '    Dim myConnection As New SqlClient.SqlConnection()
    '    myConnection.ConnectionString = "server= (local)\NetSDK;database=pubs;Trusted_Connection=yes"
    '    Dim MyCommand As New SqlClient.SqlCommand()
    '    MyCommand.Connection = myConnection
    '    MyCommand.CommandText = "Select * from Stores"
    '    MyCommand.CommandType = CommandType.Text
    '    Dim MyDA As New SqlClient.SqlDataAdapter()
    '    MyDA.SelectCommand = MyCommand
    '    Dim myDS As New Dataset1()
    '    'This is our DataSet created at Design Time      
    '    MyDA.Fill(myDS, "Stores")
    '    'You have to use the same name as that of your Dataset that you created during design time
    '    Dim oRpt As New CrystalReport1()
    '    ' This is the Crystal Report file created at Design Time
    '    oRpt.SetDataSource(myDS)
    '    ' Set the SetDataSource property of the Report to the Dataset
    '    CrystalReportViewer1.ReportSource = oRpt
    '    ' Set the Crystal Report Viewer's property to the oRpt Report object that we created
    'End Sub

End Class