Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration

Partial Public Class integrantes
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then
            If User.Identity.IsAuthenticated Then
                Session.Add("CODIGO_REGISTRO", Request.QueryString("codigo"))
                SqlDataSource1.SelectParameters("codigo").DefaultValue = Session("CODIGO_REGISTRO")
                GridView1.DataBind()
                GridView1.Visible = True
            Else
                Response.Clear()
                Response.Redirect("http://www.inteatro.gob.ar", False)
            End If
        End If

    End Sub

    Private Sub BtnAgregar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnAgregar.Click
        Try
            If txtCUIT.Text <> "" Then
                If InsertIntegrante() Then
                    txtCUIT.Text = ""
                    GridView1.DataBind()
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Function InsertIntegrante() As Boolean
        Dim MyConnection As SqlConnection
        Dim MyCommand As SqlCommand
        Dim sSQLCmd As String
        Dim mIdIntegrante As SqlParameter
        Dim nIdIntegrante As Integer
        Dim nCantidad As Integer

        Try

            txtErrorIntegrantes.Text = ""

            'Chequeo que exista en Regisdig
            sSQLCmd = "SELECT count(*) AS cantidad " & _
                            "FROM Regisdig " & _
                            "WHERE CUIL = " & txtCUIT.Text & " AND fechBaja IS NULL"

            MyConnection = New SqlConnection()
            MyConnection.ConnectionString = ConfigurationManager.ConnectionStrings("INTeatroDig").ConnectionString

            MyCommand = New SqlCommand()
            MyCommand.CommandText = sSQLCmd
            MyCommand.CommandType = CommandType.Text
            MyCommand.Connection = MyConnection
            MyCommand.Connection.Open()
            nCantidad = Convert.ToInt32(MyCommand.ExecuteScalar())

            If nCantidad = 0 Then
                txtErrorIntegrantes.Text = "El integrante no existe"
                Return False
            End If

            MyCommand.Dispose()
            MyConnection.Dispose()

            'Chequeo que no esté dado de alta
            sSQLCmd = "SELECT count(*) AS cantidad " &
                            "FROM Integrantes " &
                            "WHERE codigoRegistro = " & Session("CODIGO_REGISTRO") & " AND CUIL = " & txtCUIT.Text & " AND fechaBaja IS NULL"

            MyConnection = New SqlConnection()
            MyConnection.ConnectionString = ConfigurationManager.ConnectionStrings("INTeatroDig").ConnectionString

            MyCommand = New SqlCommand()
            MyCommand.CommandText = sSQLCmd
            MyCommand.CommandType = CommandType.Text
            MyCommand.Connection = MyConnection
            MyCommand.Connection.Open()
            nCantidad = Convert.ToInt32(MyCommand.ExecuteScalar())

            If nCantidad <> 0 Then
                txtErrorIntegrantes.Text = "El integrante ya fue agregado"
                Return False
            End If

            MyCommand.Dispose()
            MyConnection.Dispose()


            'INSERT Integrante
            sSQLCmd = "INSERT INTO Integrantes (codigoRegistro, CUIL, fechaAlta) " &
                            "VALUES (" & Session("CODIGO_REGISTRO") & ",'" & txtCUIT.Text & "', getdate()) " &
                            "SET @nIdIntegrante = SCOPE_IDENTITY()"

            mIdIntegrante = New SqlParameter
            mIdIntegrante.ParameterName = "@nIdIntegrante"
            mIdIntegrante.SqlDbType = SqlDbType.Int
            mIdIntegrante.Direction = ParameterDirection.Output
            mIdIntegrante.Value = -1

            MyConnection = New SqlConnection()
            MyConnection.ConnectionString = ConfigurationManager.ConnectionStrings("INTeatroDig").ConnectionString
            MyConnection.Open()

            MyCommand = New SqlCommand()
            MyCommand.CommandText = sSQLCmd
            MyCommand.CommandType = CommandType.Text
            MyCommand.Connection = MyConnection

            MyCommand.Parameters.Add(mIdIntegrante)

            MyCommand.ExecuteNonQuery()

            nIdIntegrante = mIdIntegrante.Value

        Catch ex As Exception
            Return False
            'Response.Redirect("ErrorPage.aspx?errMessage=" & ex.Message & "&errSource=" & ex.Source)
        Finally
            MyCommand.Dispose()
            MyConnection.Dispose()
        End Try

        InsertIntegrante = True

    End Function

    Private Sub GridView1_RowDeleted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeletedEventArgs) Handles GridView1.RowDeleted
        txtErrorIntegrantes.Text = ""
    End Sub
End Class