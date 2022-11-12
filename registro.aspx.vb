Public Partial Class registro
    Inherits System.Web.UI.Page

    Dim cn As New SqlClient.SqlConnection(SqlConex)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If User.Identity.IsAuthenticated Then
            Else
                Response.Clear()
                Response.Redirect("http://www.inteatro.gob.ar", False)
            End If
        End If

        If Request.QueryString.Count > 0 Then
            lblTitulo.Text = "Acceso a Menú de Impresión"
            divBannerTop.Visible = False
            'divBannerMiddle.Visible = False
            'divBannerBottom.Visible = False
        End If

        'MZ: Sacar el usuario y password de. .aspx
        'txtCUIT.Text = "20124169195"
        'txtPassword.Text = "juanpi89"
        'CheckBoxAcepto.Checked = True

    End Sub

    Private Sub BtnEnviar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnEnviar.Click

        Me.Validate()

        If Not Me.IsValid Then
            Dim ctrl As BaseValidator
            Dim errorCount As Integer
            For Each ctrl In Me.Validators
                If Not ctrl.IsValid Then errorCount += 1
            Next
            FailureText.Text = "Faltan Datos Obligatorios."
        Else

            'Valido el checkBox primero que todo
            If CheckBoxAcepto.Checked = False Then
                FailureText.Text = "Debe aceptar los términos"
                CheckBoxAcepto.Focus()
                Return
            End If

            'Valido CUIT
            If Not Validaciones.ValidarCUIT(txtCUIT.Text.Trim()) Then
                FailureText.Text = "CUIT/CUIL erróneo"
                txtCUIT.Focus()
                Return
            End If

            'Valido Password
            If UsuarioValido() Then

                'Valido inhibición
                If Validaciones.EstaInhibido(txtCUIT.Text.Trim()) Then
                    FailureText.Text = "El titular del CUIL/CUIT que intenta ingresar se encuentra actualmente ""inhabilitado"" por este I.N.T."
                    Return
                End If

                FormsAuthentication.RedirectFromLoginPage(txtCUIT.Text, CheckBoxAcepto.Checked)
                Response.Clear()

                Dim sRedirect As String
                sRedirect = Request.QueryString("redirect")
                If sRedirect = "" Then
                    Response.Redirect("registroLista.aspx", False)
                Else
                    Response.Redirect(sRedirect, False)
                End If
            Else
                FailureText.Text = "CUIT/CUIL o clave erróneos"
                Return
            End If
        End If

    End Sub

    Protected Sub BtnRecupera_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnRecupera.Click
        Dim wusuario As Decimal = 0
        Try
            wusuario = CDec(txtCUIT.Text.Trim())
        Catch ex As Exception
            FailureText.Text = "CUIL / CUIT Incorrecto."
        End Try
        Dim wuser As String = wusuario.ToString
        Dim wclave As String = UCase(RTrim(txtPassword.Text.Trim()))
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
            FormsAuthentication.RedirectFromLoginPage(txtCUIT.Text.Trim(), False)
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
            txtCUIT.Attributes.Add("onkeydown", "return false;")
            txtPassword.Attributes.Add("onkeydown", "return false;")
            BtnEnviar.Enabled = False
        End If
    End Sub

    Private Function UsuarioValido() As Boolean
        Const F_USERID As Integer = 0
        Const F_CLAVE As Integer = 16

        Dim cn As New SqlClient.SqlConnection(SqlConex)
        Dim sql As String
        Dim Clave As String
        Dim nUserId As Integer
        Dim wUser As String
        Dim wClave As String

        wUser = txtCUIT.Text.Trim
        wClave = txtPassword.Text.Trim

        cn.Open()

        sql = "SELECT * FROM REGISDIG WHERE cuil = " & wUser & " AND fechbaja IS NULL"
        Dim Psql As New SqlClient.SqlCommand(sql, cn)
        Dim dr As SqlClient.SqlDataReader = Psql.ExecuteReader
        Clave = ""

        If dr.HasRows = True Then
            While (dr.Read())
                Clave = RTrim(UCase(dr.GetString(F_CLAVE)))
                nUserId = dr.GetInt32(F_USERID)
            End While
            If Clave.ToUpper = wClave.ToUpper Then
                Session.Add("USER_ID", nUserId)
                Session.Add("id_user", wUser)
                Session.Add("Cuit", wUser)
                Return True
            Else
                FailureText.Text = "Se ingresó un usuario o clave incorrecta"
                intentos = intentos + 1
                Return False
            End If
        Else
            FailureText.Text = "CUIL / CUIT Incorrecto"
            intentos = intentos + 1
            Return False
        End If

        cn.Close()
        dr.Close()

        If intentos = 3 Then
            UserMsgBox(Me, "Tres Intentos Fallidos. Inicio Deshabilitado")
            txtCUIT.Attributes.Add("onkeydown", "return false;")
            txtPassword.Attributes.Add("onkeydown", "return false;")
            CheckBoxAcepto.Enabled = False
            BtnEnviar.Enabled = False
        End If

    End Function


End Class