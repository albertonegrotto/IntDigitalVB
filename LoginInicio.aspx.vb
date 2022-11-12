Public Partial Class LoginInicio
    Inherits System.Web.UI.Page
    Dim cn As New SqlClient.SqlConnection(SqlConex)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim sUserId As String
        Dim sRedirect As String
        Dim nPos As Integer

        If Not Page.IsPostBack Then
            intentos = 0
            Remember.Enabled = False
            Username.Focus()

            'Si viene de una llamada desde afuera por querystring
            If Request.QueryString.Count > 0 Then

                lblTitulo.Text = "Ingreso a INTeatro Digital"

                sRedirect = Request.QueryString("redirect")
                nPos = InStrRev(sRedirect, "=")
                sUserId = Mid(sRedirect, nPos + 1, sRedirect.Length - nPos)

                'CreateSession()

                Try
                    If Session("USER_ID") IsNot Nothing Then
                        If Session("USER_ID").ToString.ToUpper = sUserId Then
                            'Si el request es del mismo usuario logoneado, redirect a la consulta
                            If sRedirect.Length > 0 Then
                                'Lo redicciono a la página que vino por querystring
                                Response.Redirect(sRedirect)
                            End If
                        Else
                            'Si el request es de otro usuario, logout de la sesión actual
                            FormsAuthentication.SignOut()
                            Session.Abandon()
                            ViewState("REDIRECT") = sRedirect
                            ViewState("REDIRECT_USER") = sUserId
                        End If
                    Else
                        ViewState("REDIRECT") = sRedirect
                        ViewState("REDIRECT_USER") = sUserId
                    End If

                Catch ex As Exception
                End Try

            End If


        Else

        End If

    End Sub

    Protected Sub BtnEnviar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnEnviar.Click
        Dim nUserId As Integer

        'MZ: Sacar el usuario y password de. .aspx
        'Password.Text = "juanpi89"

        Me.Validate()

        If Not Me.IsValid Then
            Dim ctrl As BaseValidator
            Dim errorCount As Integer
            For Each ctrl In Me.Validators
                If Not ctrl.IsValid Then errorCount += 1
            Next
            FailureText.Text = "Falta Datos Obligatorios."

        Else

            Dim wusuario As Decimal = 0
            Try
                wusuario = CDec(Username.Text)
            Catch ex As Exception
                FailureText.Text = "CUIL / CUIT Incorrecto."
            End Try

            Dim wuser As String = wusuario.ToString
            Dim wclave As String = UCase(RTrim(Password.Text))

            cn.Open()
            Dim sql As String = "select * from REGISDIG where cuil=" & wuser & " and fechbaja is null"
            Dim Psql As New SqlClient.SqlCommand(sql, cn)
            Dim dr As SqlClient.SqlDataReader = Psql.ExecuteReader
            Dim Clave As String = ""
            Dim wProvincia As Integer = 0
            Dim wPersona As Integer = 0
            Dim wcambio As Integer = 0
            If dr.HasRows = True Then
                While (dr.Read())
                    nUserId = dr.GetInt32(0)    'codigo
                    wProvincia = dr.GetInt32(1)
                    wPersona = dr.GetInt32(2)
                    Clave = RTrim(UCase(dr.GetString(16)))
                    wcambio = dr.GetInt32(44)
                End While

                If Clave = wclave Then
                    Session.Add("id_user", wuser)
                    Session.Add("USER_ID", nUserId)
                    Session.Add("CUIT", wuser)
                    Session.Add("id_provincia", wProvincia)
                    Session.Add("id_persona", wPersona)
                    FormsAuthentication.RedirectFromLoginPage(Username.Text, Remember.Checked)
                    If wcambio = 1 Then
                        Response.Clear()
                        Response.Redirect("cambioDeClave.aspx", False)
                        Return
                    End If
                    If ViewState("REDIRECT") Is Nothing Then
                        Response.Clear()
                        If wPersona = 1 Then
                            Response.Redirect("ActualIndivFis.aspx", False)
                        Else
                            Response.Redirect("ActualIndivJur.aspx", False)
                        End If
                        Return
                        'Response.Redirect("~/main.aspx", False)
                    Else
                        If ViewState("REDIRECT_USER").ToString.ToUpper = Session("USER_ID").ToString.ToUpper Then
                            Response.Redirect(ViewState("REDIRECT"), False)
                            Return
                        Else
                            If wPersona = 1 Then
                                Response.Redirect("ActualIndivFis.aspx", False)
                            Else
                                Response.Redirect("ActualIndivJur.aspx", False)
                            End If
                        End If
                    End If

                    ''Viejo
                    'Response.Clear()
                    'If wPersona = 1 Then
                    '    Response.Redirect("ActualIndivFis.aspx", False)
                    'Else
                    '    Response.Redirect("ActualIndivJur.aspx", False)
                    'End If

                Else
                    FailureText.Text = "Se ingresó una Clave Incorrecta"
                    intentos = intentos + 1
                End If
            Else
                FailureText.Text = "CUIL / CUIT Incorrecto"
                intentos = intentos + 1
            End If
            cn.Close()
            dr.Close()
            If intentos = 3 Then
                UserMsgBox(Me, "Tres Intentos Fallidos. Inicio Deshabilitado")
                Username.Attributes.Add("onkeydown", "return false;")
                Password.Attributes.Add("onkeydown", "return false;")
                Remember.Enabled = False
                BtnEnviar.Enabled = False
            End If
        End If
    End Sub

    Protected Sub BtnRecupera_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnRecupera.Click
        Dim wusuario As Decimal = 0
        Try
            wusuario = CDec(Username.Text)
        Catch ex As Exception
            FailureText.Text = "CUIL / CUIT Incorrecto."
        End Try
        Dim wuser As String = wusuario.ToString
        Dim wclave As String = UCase(RTrim(Password.Text))
        cn.Open()
        Dim sql As String = "select * from REGISDIG where cuil=" & wuser & " and fechbaja is null"
        Dim Psql As New SqlClient.SqlCommand(sql, cn)
        Dim dr As SqlClient.SqlDataReader = Psql.ExecuteReader
        If dr.HasRows = True Then
            Dim wpregunta As Integer = 0
            While (dr.Read())
                wpregunta = dr.GetInt32(17)
            End While
            Session.Add("Cuit", wuser)
            Session.Add("Pregunta", wpregunta)
            FormsAuthentication.RedirectFromLoginPage(Username.Text, Remember.Checked)
            Response.Clear()
            Response.Redirect("RecupContra.aspx", False)
        Else
            FailureText.Text = "CUIL / CUIT Incorrecto"
            intentos = intentos + 1
        End If
        cn.Close()
        dr.Close()
        If intentos = 3 Then
            UserMsgBox(Me, "Tres Intentos Fallidos. Inicio Desabilitado")
            Username.Attributes.Add("onkeydown", "return false;")
            Password.Attributes.Add("onkeydown", "return false;")
            Remember.Enabled = False
            BtnEnviar.Enabled = False
        End If
    End Sub

    'Private Function CreateSession() As Boolean
    '    Dim SesionDT As String = ""

    '    SesionDT = DateTime.Now.ToString("G")
    '    'Username.Text = SesionDT
    '    Session.Add("id_user", SesionDT)
    '    Session.Add("wsolicitud_", 0)
    '    FormsAuthentication.RedirectFromLoginPage(Username.Text, Remember.Checked)
    '    'Response.Clear()
    '    'Response.Redirect("AltaIni.aspx", False)

    'End Function

End Class