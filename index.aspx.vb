Partial Public Class index
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If User.Identity.IsAuthenticated Then
            CerrarSesion.Visible = True
        Else
            CerrarSesion.Visible = False
        End If
        Dim controla As String
        Dim sRedirectURL As String = ""
        controla = Request.Params("__EVENTTARGET")
        sRedirectURL = Request.QueryString("ReturnURL")
        If Session("RedirectURL") Is Nothing Then
            Session("RedirectURL") = sRedirectURL
        End If
        If Not controla Is Nothing Then
            Dim pars As String()
            Dim ch As Char() = New Char() {","c}
            pars = Request.Params.Get("__EVENTARGUMENT").Split(ch(0))
            Dim paruser As String = pars(0)
            Dim parpwd As String = pars(1)
            Select Case controla
                Case "IniciaSession"
                    Dim user As usuario = DBAuthenticate(paruser, parpwd)
                    If Not user Is Nothing Then
                        Session("usuario") = user
                        'If Validaciones.EstaInhibido(user.Usuario) Then
                        '    Response.Redirect("inhibido.aspx")
                        'End If
                        If Session("RedirectURL") Is Nothing Then
                            Session("RedirectURL") = ""
                        Else
                            sRedirectURL = Session("RedirectURL")
                        End If
                        If Left(sRedirectURL, 21) = "confirmarRegistracion" Then
                            Dim params As String
                            'FormsAuthentication.RedirectFromLoginPage(user.Usuario, True, "/")
                            params = "&o=" & Request.QueryString("o") & "&u=" & Request.QueryString("u") & "&r=" & Request.QueryString("r")
                            FormsAuthentication.RedirectFromLoginPage(user.Usuario, False)
                            If sRedirectURL.Length > 0 Then
                                Response.Redirect(sRedirectURL & params)
                            End If
                        Else
                            FormsAuthentication.RedirectFromLoginPage(user.Usuario, False)
                        End If
                    End If
                Case "ValidaCUIT"
                    Dim wcuit As Decimal = 0
                    Try
                        wcuit = CDec(paruser)
                    Catch ex As Exception
                        wcuit = 0
                    End Try
                    If wcuit <> 0 Then
                        Dim valida As Boolean = ValidaCUIT(paruser)
                        If valida Then
                            Session("CUIL") = paruser
                            inusuario.Value = paruser
                            Dim wnombre As String = GetNombre(paruser)
                            Bienvenida.InnerText = wnombre
                            ingresa.Visible = False
                            divPwd.Visible = True
                            DivContra.Visible = True
                            pwd.Focus()
                        Else
                            Session("CUIL") = paruser
                            ingresa.Visible = False
                            inusuario.Value = paruser
                            DivAlta.Visible = True
                        End If
                        inusuario.Disabled = True
                    End If
            End Select
        Else
            divPwd.Visible = False
            DivContra.Visible = False
            DivAlta.Visible = False
            Session("Redirect") = ""
        End If
    End Sub

    <System.Web.Services.WebMethod()>
    <System.Web.Script.Services.ScriptMethod()>
    Public Shared Function ValidaUsuario(ByVal usuario As String, ByVal pwd As String, ByVal conf As Boolean) As Boolean
        Dim cn As SqlClient.SqlConnection
        Dim cmd As SqlClient.SqlCommand
        Dim cod As Integer

        'cn = New SqlClient.SqlConnection(ConfigurationManager.AppSettings("INTeatroDig"))
        cn = New SqlClient.SqlConnection(SqlConex)
        cmd = New SqlClient.SqlCommand("DBAuthenticate", cn)
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@usuario", SqlDbType.VarChar, 50)
        cmd.Parameters("@usuario").Direction = ParameterDirection.InputOutput
        cmd.Parameters("@usuario").Value = usuario

        cmd.Parameters.Add("@pwd", SqlDbType.VarChar, 50)
        cmd.Parameters("@pwd").Direction = ParameterDirection.InputOutput
        cmd.Parameters("@pwd").Value = pwd

        cmd.Parameters.Add("@confirma", SqlDbType.Int)
        cmd.Parameters("@confirma").Direction = ParameterDirection.Input
        If conf Then
            cmd.Parameters("@confirma").Value = 1
        Else
            cmd.Parameters("@confirma").Value = 0
        End If

        cmd.Parameters.Add("@codigo", SqlDbType.Int)
        cmd.Parameters("@codigo").Direction = ParameterDirection.InputOutput

        cmd.Parameters.Add("@nombre", SqlDbType.VarChar, 50)
        cmd.Parameters("@nombre").Direction = ParameterDirection.InputOutput

        cmd.Parameters.Add("@sexo", SqlDbType.VarChar, 50)
        cmd.Parameters("@sexo").Direction = ParameterDirection.InputOutput

        cmd.Parameters.Add("@persona", SqlDbType.VarChar, 50)
        cmd.Parameters("@persona").Direction = ParameterDirection.InputOutput

        cmd.Parameters.Add("@dir", SqlDbType.VarChar, 100)
        cmd.Parameters("@dir").Direction = ParameterDirection.InputOutput

        cmd.Parameters.Add("@cpostal", SqlDbType.VarChar, 50)
        cmd.Parameters("@cpostal").Direction = ParameterDirection.InputOutput

        cmd.Parameters.Add("@email", SqlDbType.VarChar, 50)
        cmd.Parameters("@email").Direction = ParameterDirection.InputOutput

        cmd.Parameters.Add("@provincia", SqlDbType.VarChar, 50)
        cmd.Parameters("@provincia").Direction = ParameterDirection.InputOutput

        cmd.Parameters.Add("@denominacion", SqlDbType.VarChar, 100)
        cmd.Parameters("@denominacion").Direction = ParameterDirection.InputOutput

        cmd.Parameters.Add("@domicilio", SqlDbType.VarChar, 100)
        cmd.Parameters("@domicilio").Direction = ParameterDirection.InputOutput

        cmd.Parameters.Add("@teparticular", SqlDbType.VarChar, 100)
        cmd.Parameters("@teparticular").Direction = ParameterDirection.InputOutput

        cmd.Parameters.Add("@tecelular", SqlDbType.VarChar, 100)
        cmd.Parameters("@tecelular").Direction = ParameterDirection.InputOutput

        cmd.Parameters.Add("@codprovin", SqlDbType.Int)
        cmd.Parameters("@codprovin").Direction = ParameterDirection.InputOutput

        cmd.Parameters.Add("@personeria", SqlDbType.VarChar, 50)
        cmd.Parameters("@personeria").Direction = ParameterDirection.InputOutput

        cmd.Parameters.Add("@localidad", SqlDbType.VarChar, 50)
        cmd.Parameters("@localidad").Direction = ParameterDirection.InputOutput

        cmd.Parameters.Add("@CAMBCLAVE", SqlDbType.Int)
        cmd.Parameters("@CAMBCLAVE").Direction = ParameterDirection.InputOutput

        Try
            cn.Open()
            cmd.ExecuteNonQuery()
            cod = cmd.Parameters("@codigo").Value
            cn.Close()
            If cod > 0 Then
                Dim Sql As String = "Execute insert_LogIngresos @usuario"
                Dim cmdg As New SqlClient.SqlCommand(Sql, cn)
                cmdg.Parameters.AddWithValue("@usuario", SqlDbType.Decimal)
                cmdg.Parameters("@usuario").Value = CDec(usuario)
                Try
                    cn.Open()
                    cmdg.ExecuteNonQuery()
                    cn.Close()
                Catch ex As Exception
                    Dim lgerror As String = ex.Message
                End Try
                Return True
            Else

            End If
        Catch ex As Exception
            If cn.State = ConnectionState.Open Then
                cn.Close()
            End If
            Return False
            'hdError.Value = "El CUIT/CUIL o la clave ingresados son incorrectos"
        End Try

    End Function

    Public Function DBAuthenticate(ByVal user As String, ByVal pwd As String) As usuario
        Dim cn As SqlClient.SqlConnection
        Dim cmd As SqlClient.SqlCommand
        Dim cod As Integer
        Dim cuenta As usuario

        'cn = New SqlClient.SqlConnection(ConfigurationManager.AppSettings("INTeatroDig"))
        cn = New SqlClient.SqlConnection(SqlConex)
        cmd = New SqlClient.SqlCommand("DBAuthenticate", cn)
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@usuario", SqlDbType.VarChar, 50)
        cmd.Parameters("@usuario").Direction = ParameterDirection.InputOutput
        cmd.Parameters("@usuario").Value = user

        cmd.Parameters.Add("@pwd", SqlDbType.VarChar, 50)
        cmd.Parameters("@pwd").Direction = ParameterDirection.InputOutput
        cmd.Parameters("@pwd").Value = pwd

        cmd.Parameters.Add("@confirma", SqlDbType.Int)
        cmd.Parameters("@confirma").Direction = ParameterDirection.Input
        cmd.Parameters("@confirma").Value = 1

        cmd.Parameters.Add("@codigo", SqlDbType.Int)
        cmd.Parameters("@codigo").Direction = ParameterDirection.InputOutput

        cmd.Parameters.Add("@nombre", SqlDbType.VarChar, 50)
        cmd.Parameters("@nombre").Direction = ParameterDirection.InputOutput

        cmd.Parameters.Add("@sexo", SqlDbType.VarChar, 50)
        cmd.Parameters("@sexo").Direction = ParameterDirection.InputOutput

        cmd.Parameters.Add("@persona", SqlDbType.VarChar, 50)
        cmd.Parameters("@persona").Direction = ParameterDirection.InputOutput

        cmd.Parameters.Add("@dir", SqlDbType.VarChar, 100)
        cmd.Parameters("@dir").Direction = ParameterDirection.InputOutput

        cmd.Parameters.Add("@cpostal", SqlDbType.VarChar, 50)
        cmd.Parameters("@cpostal").Direction = ParameterDirection.InputOutput

        cmd.Parameters.Add("@email", SqlDbType.VarChar, 50)
        cmd.Parameters("@email").Direction = ParameterDirection.InputOutput

        cmd.Parameters.Add("@provincia", SqlDbType.VarChar, 50)
        cmd.Parameters("@provincia").Direction = ParameterDirection.InputOutput

        cmd.Parameters.Add("@denominacion", SqlDbType.VarChar, 100)
        cmd.Parameters("@denominacion").Direction = ParameterDirection.InputOutput

        cmd.Parameters.Add("@domicilio", SqlDbType.VarChar, 100)
        cmd.Parameters("@domicilio").Direction = ParameterDirection.InputOutput

        cmd.Parameters.Add("@teparticular", SqlDbType.VarChar, 100)
        cmd.Parameters("@teparticular").Direction = ParameterDirection.InputOutput

        cmd.Parameters.Add("@tecelular", SqlDbType.VarChar, 100)
        cmd.Parameters("@tecelular").Direction = ParameterDirection.InputOutput

        cmd.Parameters.Add("@codprovin", SqlDbType.Int, 32)
        cmd.Parameters("@codprovin").Direction = ParameterDirection.InputOutput

        cmd.Parameters.Add("@personeria", SqlDbType.VarChar, 50)
        cmd.Parameters("@personeria").Direction = ParameterDirection.InputOutput

        cmd.Parameters.Add("@localidad", SqlDbType.VarChar, 50)
        cmd.Parameters("@localidad").Direction = ParameterDirection.InputOutput

        cmd.Parameters.Add("@CAMBCLAVE", SqlDbType.Int)
        cmd.Parameters("@CAMBCLAVE").Direction = ParameterDirection.InputOutput

        Try
            cn.Open()
            cmd.ExecuteNonQuery()
            cod = cmd.Parameters("@codigo").Value
            If cod > 0 Then
                cuenta = New usuario
                cuenta.Usuario = user
                cuenta.Persona = cmd.Parameters("@persona").Value
                cuenta.Sexo = cmd.Parameters("@sexo").Value
                cuenta.Email = cmd.Parameters("@email").Value
                cuenta.Codigo = cod
                cuenta.CPostal = cmd.Parameters("@cpostal").Value
                cuenta.Direccion = cmd.Parameters("@dir").Value
                cuenta.Nombre = cmd.Parameters("@nombre").Value
                cuenta.Provincia = cmd.Parameters("@provincia").Value
                cuenta.Denominacion = cmd.Parameters("@denominacion").Value
                cuenta.Domicilio = cmd.Parameters("@domicilio").Value
                cuenta.TelefonoParticular = cmd.Parameters("@teparticular").Value
                cuenta.TelefonoCelular = cmd.Parameters("@tecelular").Value
                cuenta.codprovin = cmd.Parameters("@codprovin").Value
                cuenta.personeria = cmd.Parameters("@personeria").Value
                cuenta.localidad = cmd.Parameters("@localidad").Value
                cuenta.cambclave = cmd.Parameters("@CAMBCLAVE").Value
            Else

            End If
        Catch ex As Exception
            'hdError.Value = "El CUIT/CUIL o la clave ingresados son incorrectos"
        End Try
        cn.Close()

        If cod > 0 Then
            Return cuenta
        Else
            Return Nothing
        End If
    End Function

    '<System.Web.Services.WebMethod()>
    '<System.Web.Script.Services.ScriptMethod()>
    Public Shared Function ValidaCUIT(ByVal usuario As String) As Boolean
        Dim cn As SqlClient.SqlConnection = New SqlClient.SqlConnection(SqlConex)
        Dim existe As Integer = 0
        Dim sql As String = "select count(*) from REGISDIG where CUIL=" & usuario
        cn.Open()
        Dim Psql As New SqlClient.SqlCommand(sql, cn)
        Dim dr As SqlClient.SqlDataReader = Psql.ExecuteReader
        While dr.Read()
            existe = dr.GetInt32(0)
        End While
        dr.Close()
        cn.Close()
        If existe = 1 Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Shared Function GetNombre(ByVal usuario As String) As String
        Dim cn As SqlClient.SqlConnection = New SqlClient.SqlConnection(SqlConex)
        Dim wnombre As String = ""
        Dim sql As String = "select rtrim(nombre)+' '+rtrim(apellido)+rtrim(denominacion) from REGISDIG where CUIL=" & usuario
        cn.Open()
        Dim Psql As New SqlClient.SqlCommand(sql, cn)
        Dim dr As SqlClient.SqlDataReader = Psql.ExecuteReader
        While dr.Read()
            wnombre = dr.GetString(0)
        End While
        dr.Close()
        cn.Close()
        Return wnombre
    End Function


End Class