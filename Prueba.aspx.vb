Imports System.Data.SqlClient
Partial Public Class Prueba
    Inherits System.Web.UI.Page
    Shared ds As dsIntegrantes

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim cuit As String
        cuit = TextBox1.Text
        Session("codigo") = "73"
        Dim cod As String = "73"
        Dim cn As New SqlClient.SqlConnection(SqlConex)
        'Sectores / Grupos
        If Not IsPostBack Then
            ds = Nothing
            dsInte.CargaIntegrantes(ds, cod)

        End If
        GridView1.DataSource = ds.Integrantes
        GridView1.DataBind()
        cn.Close()


    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button1.Click
        'Dim dsi As New dsInte
        dsInte.AceptaCambios(ds, -1)
    End Sub

    Protected Sub GridView1_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles GridView1.RowDeleting
        Dim id As String

        'id = GridView1.Rows(e.RowIndex).Cells(1).Text
        id = GridView1.DataKeys(e.RowIndex).Value
        'Dim dsi As New dsInte
        dsInte.DesvinculaIntegrante(ds, Convert.ToInt32(Session("codigo")), id)
        GridView1.DataBind()

    
    End Sub

    Protected Sub GridView1_RowDeleted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeletedEventArgs) Handles GridView1.RowDeleted
    End Sub

    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button2.Click
        'Dim dsi As New dsInte
        dsInte.AgregaIntegrantes(ds, Convert.ToInt32(Session("codigo")), TextBox1.Text)
        GridView1.DataBind()
        TextBox1.Text = ""
    End Sub
End Class