Partial Public Class Activa
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim controla As String
        controla = Request.Params("__EVENTTARGET")
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
                        If Validaciones.EstaInhibido(user.Usuario) Then
                            Response.Redirect("inhibido.aspx")
                        Else
                            FormsAuthentication.RedirectFromLoginPage(user.Usuario, True, "/")
                        End If
                    End If
            End Select
        Else
            Dim returnurl As String = Request.QueryString("ReturnURL")
            If returnurl.Length > 0 Then
                Dim codigo As Integer = getCodigo(returnurl)
                ActivaCuenta(codigo)
            End If
        End If
    End Sub
    Private Function getCodigo(ByVal ret As String) As Integer
        Dim lugar As Integer = ret.LastIndexOf("=")
        Dim strcod As String = ret.Substring(lugar + 1, ret.Length() - (lugar + 1))
        Return Convert.ToInt32(strcod)
    End Function
    Private Sub ActivaCuenta(ByVal codigo As Integer)
        Dim cn As SqlClient.SqlConnection
        Dim cmd As SqlClient.SqlCommand
        cn = New SqlClient.SqlConnection(SqlConex)
        cmd = New SqlClient.SqlCommand("ActivaCuenta", cn)
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@codigo", SqlDbType.Int)
        cmd.Parameters("@codigo").Value = codigo

        Try
            cn.Open()
            cmd.ExecuteNonQuery()
            cn.Close()

        Catch ex As Exception
            If cn.State = ConnectionState.Open Then
                cn.Close()
            End If
            'hdError.Value = "El CUIT/CUIL o la clave ingresados son incorrectos"
        End Try
    End Sub
    ''Protected Sub Login_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Login.Click
    'Dim user As usuario = DBAuthenticate(txtUsuario.Text, txtPwd.Text)
    'If Not user Is Nothing Then
    '    Session("usuario") = user
    '    FormsAuthentication.RedirectFromLoginPage(user.Usuario, True, "/")

    'End If

    'End Sub
    <System.Web.Services.WebMethod()> _
 <System.Web.Script.Services.ScriptMethod()> _
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
End Class