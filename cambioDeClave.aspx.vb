Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Partial Public Class cambioDeClave
    Inherits System.Web.UI.Page
    Dim cn As SqlClient.SqlConnection = New SqlClient.SqlConnection(SqlConex)
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            inicializa()
            txtClaveActual.Focus()
        End If
    End Sub

    Private Sub inicializa()
        Dim quien As usuario = CType(Session("usuario"), usuario)
        Dim Cuil As Decimal = quien.Usuario
        cn.Open()
        Dim sql As String = "select 0 as codigo,'Seleccione' as descrip union select * From RECUPCONTRA where codigo in (1,3,5)"
        Dim Psql As New SqlClient.SqlCommand(sql, cn)
        Dim dr As SqlClient.SqlDataReader = Psql.ExecuteReader
        DdlPregunta.DataSource = dr
        DdlPregunta.DataTextField = "descrip"
        DdlPregunta.DataValueField = "codigo"
        DdlPregunta.DataBind()
        dr.Close()
        cn.Close()
        cn.Open()
        Dim wpregunta As Integer = 0
        Dim wrespuesta As String = ""
        sql = "select pregunta,respuesta From REGISDIG where CUIL=" & Cuil
        Dim Psqlp As New SqlClient.SqlCommand(sql, cn)
        Dim drp As SqlClient.SqlDataReader = Psqlp.ExecuteReader
        While drp.Read()
            Try
                wpregunta = drp.GetInt32(0)
            Catch ex As Exception
                wpregunta = 0
            End Try
            Try
                wrespuesta = drp.GetString(1)
            Catch ex As Exception
                wrespuesta = ""
            End Try
        End While
        drp.Close()
        cn.Close()
        If wpregunta = 1 Or wpregunta = 3 Or wpregunta = 5 Then
            DdlPregunta.SelectedValue = wpregunta
            TextBoxRespuesta.Text = RTrim(wrespuesta)
        End If
    End Sub

    Private Sub BtnEnviar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnEnviar.Click
        Dim quien As usuario = CType(Session("usuario"), usuario)
        Dim wcuit As Decimal = quien.Usuario
        Dim wclaveactual As String = RTrim(txtClaveActual.Text)
        If Len(RTrim(wclaveactual)) = 0 Then
            FailureText.Text = "Ingrese Clave Actual"
            Return
        End If
        Dim wcontrasena As String = txtNuevaClave.Text.Trim
        Me.Validate()
        If Not Me.IsValid Then
            Dim ctrl As BaseValidator
            Dim errorCount As Integer
            For Each ctrl In Me.Validators
                If Not ctrl.IsValid Then errorCount += 1
            Next
            If errorCount > 0 Then
                txtClaveActual.Text = wclaveactual
                FailureText.Text = "Debe completar todos los datos"
                Return
            End If
        Else
            If Len(wcontrasena) <> 8 Then
                txtClaveActual.Text = wclaveactual
                FailureText.Text = "Debe ingresar 8 caracteres"
                Return
            End If
            Dim tj As String = ""
            Dim j As Integer = 0
            Dim f As Integer = 0
            While j <= 9 And f = 0
                tj = j.ToString
                f = InStr(1, wcontrasena, tj, CompareMethod.Text)
                j = j + 1
            End While
            If f = 0 Then
                txtClaveActual.Text = wclaveactual
                FailureText.Text = "La contraseña debe tener por lo menos 1 (un) número"
                Return
            End If
            Dim wpregunta As Integer = DdlPregunta.SelectedValue
            Dim wrespuesta As String = RTrim(TextBoxRespuesta.Text)
            If wpregunta = 0 Then
                txtClaveActual.Text = wclaveactual
                FailureText.Text = "Seleccione pregunta de seguridad"
                Return
            End If
            If Len(RTrim(wrespuesta)) = 0 Then
                txtClaveActual.Text = wclaveactual
                FailureText.Text = "Ingrese respuesta a pregunta de seguridad"
                Return
            End If
            If UsuarioValido(wcuit, txtClaveActual.Text.Trim()) Then
                If CambiarClave(wcuit, txtNuevaClave.Text.Trim()) Then
                    Dim werror As Integer = 0
                    cn.Open()
                    Dim sql As String = "update REGISDIG set pregunta=" & wpregunta & ",respuesta='" & wrespuesta & "' where cuil=" & wcuit
                    Dim Cmd As New SqlClient.SqlCommand(sql, cn)
                    Try
                        Cmd.ExecuteNonQuery()
                    Catch ex As Exception
                        werror = 1
                    End Try
                    cn.Close()
                    If werror = 0 Then
                        FailureText.ForeColor = Drawing.Color.Blue
                        FailureText.Font.Bold = True
                        FailureText.Font.Size = 12
                        FailureText.Text = "Clave modificada con éxito"
                    Else
                        FailureText.ForeColor = Drawing.Color.Blue
                        FailureText.Font.Bold = True
                        FailureText.Font.Size = 12
                        FailureText.Text = "Clave modificada con éxito no se pudo actualizar la pregunta de seguridad"
                    End If
                    txtClaveActual.Enabled = False
                    DdlPregunta.SelectedValue = 0
                    TextBoxRespuesta.Text = ""
                    DdlPregunta.Enabled = False
                    TextBoxRespuesta.Enabled = False
                    txtNuevaClave.Enabled = False
                    txtConfirmacionClave.Enabled = False
                    BtnEnviar.Visible = False
                Else
                    FailureText.Text = "No se pudo efectuar el cambio de clave"
                End If
            Else
                FailureText.Text = "Usuario o clave inválido"
            End If
        End If
    End Sub

    Private Function UsuarioValido(ByVal sCUIL As String, ByVal sPassword As String) As Boolean
        Dim sSQLCmd As String
        Dim nCantidad As Integer
        Dim MyConnection As SqlConnection
        Dim MyCommand As SqlCommand
        sSQLCmd = "SELECT count(*) AS cantidad " &
                        "FROM RegisDig " &
                        "WHERE cuil = '" & sCUIL & "' AND " &
                            "contrasena = '" & sPassword & "'"

        MyConnection = New SqlConnection()
        MyConnection.ConnectionString = ConfigurationManager.ConnectionStrings("INTeatroDig").ConnectionString

        MyCommand = New SqlCommand()
        MyCommand.CommandText = sSQLCmd
        MyCommand.CommandType = CommandType.Text
        MyCommand.Connection = MyConnection
        MyCommand.Connection.Open()
        nCantidad = Convert.ToInt32(MyCommand.ExecuteScalar())
        If nCantidad > 0 Then
            UsuarioValido = True
        Else
            UsuarioValido = False
        End If
        MyCommand.Dispose()
        MyConnection.Dispose()
    End Function

    Private Function CambiarClave(ByVal sCUIT As String, ByVal sNuevaPassword As String) As Boolean
        Dim MyConnection As SqlConnection
        Dim MyCommand As SqlCommand
        Dim sSQLCmd As String
        sSQLCmd = "UPDATE RegisDig " &
                    "SET contrasena = '" & sNuevaPassword & "', " &
                        "fechmodi = Getdate() , CAMBCLAVE = 0 " &
                    " WHERE cuil = " & sCUIT
        Try
            MyConnection = New SqlConnection()
            MyConnection.ConnectionString = ConfigurationManager.ConnectionStrings("INTeatroDig").ConnectionString
            MyConnection.Open()
            MyCommand = New SqlCommand()
            MyCommand.CommandText = sSQLCmd
            MyCommand.CommandType = CommandType.Text
            MyCommand.Connection = MyConnection
            MyCommand.ExecuteNonQuery()
        Catch ex As Exception
            Response.Redirect("Error.aspx?errNum=10001&errMsg=" & ex.Message)
        Finally
            MyCommand.Dispose()
            MyConnection.Dispose()
        End Try
        CambiarClave = True
    End Function

End Class