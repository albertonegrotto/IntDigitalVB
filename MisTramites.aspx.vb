Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration

Public Class MisTramites
    Inherits System.Web.UI.Page
    Dim quien As usuario
    Dim cn As New SqlClient.SqlConnection(SqlConex)
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If User.Identity.IsAuthenticated Then
                quien = CType(Session("usuario"), usuario)
                Dim codigo As Integer = quien.Codigo
                Dim wcuil As Decimal = 0
                cn.Open()
                Dim sql As String = "select cuil from REGISDIG where codigo=" & codigo
                Dim Psql As New SqlClient.SqlCommand(sql, cn)
                Dim dr As SqlClient.SqlDataReader = Psql.ExecuteReader
                While dr.Read()
                    wcuil = dr.GetDecimal(0)
                End While
                cn.Close()
                dr.Close()

                SqlDataSource1.SelectParameters("cuil").DefaultValue = wcuil
                GridView1.DataBind()
            Else
                Response.Clear()
                Response.Redirect("http://www.inteatro.gob.ar", False)
            End If
        End If

    End Sub

End Class