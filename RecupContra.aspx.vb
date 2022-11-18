Public Partial Class RecupContra
    Inherits System.Web.UI.Page
    Dim cn As New SqlClient.SqlConnection(SqlConex)
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Dim sCUIT As String = Session("CUIL")
            Username.Text = sCUIT
            intentos = 0
            inicializa()
        End If
    End Sub
    Private Sub inicializa()
        cn.Open()
        Dim sql2 As String = "select 0 as codigo,'Seleccione' as descrip union select codigo,descrip from RECUPCONTRA order by codigo"
        Dim Psql2 As New SqlClient.SqlCommand(sql2, cn)
        Dim dr2 As SqlClient.SqlDataReader = Psql2.ExecuteReader
        DdlPregunta.DataSource = dr2
        DdlPregunta.DataTextField = "descrip"
        DdlPregunta.DataValueField = "codigo"
        DdlPregunta.DataBind()
        dr2.Close()
        cn.Close()
        BtnEnviar.Enabled = False
        Verificar()
        TextBoxRespuesta.Focus()
    End Sub

    Protected Sub BtnEnviar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnEnviar.Click
        Me.Validate()

        If Not Me.IsValid Then
            Dim ctrl As BaseValidator
            Dim errorCount As Integer
            For Each ctrl In Me.Validators
                If Not ctrl.IsValid Then errorCount += 1
            Next
            FailureText.ForeColor = Drawing.Color.Red
            FailureText.Text = " Debe completar la Respuesta"
        Else
            Dim wrecupero As String = RTrim(TextBoxRespuesta.Text)
            If Len(wrecupero) = 0 Then
                FailureText.ForeColor = Drawing.Color.Red
                FailureText.Text = " Debe completar la Respuesta"
                Return
            End If

            Dim wcuit As String = RTrim(Username.Text)
            If Len(wcuit) = 0 Then
                FailureText.ForeColor = Drawing.Color.Red
                FailureText.Text = "Ingrese CUIT / CUIL"
                Return
            End If

            cn.Open()
            Dim sql As String = "select * from REGISDIG where cuil=" & wcuit & " and fechbaja is null"
            Dim Psql As New SqlClient.SqlCommand(sql, cn)
            Dim dr As SqlClient.SqlDataReader = Psql.ExecuteReader
            Dim wProvincia As Integer = 0
            Dim wPersona As Integer = 0
            Dim wapellido As String = ""
            Dim wnombre As String = ""
            Dim wemail As String = ""
            Dim wdenominacion As String = ""
            Dim wcontrasena As String = ""
            Dim wrespuesta As String = ""
            If dr.HasRows = True Then
                While (dr.Read())
                    wapellido = RTrim(dr.GetString(5))
                    wnombre = RTrim(dr.GetString(6))
                    wProvincia = dr.GetInt32(1)
                    wPersona = dr.GetInt32(2)
                    wemail = RTrim(dr.GetString(15))
                    wcontrasena = RTrim(UCase(dr.GetString(16)))
                    wrespuesta = RTrim(dr.GetString(18))
                    wdenominacion = RTrim(dr.GetString(19))
                End While
            End If
            If UCase(wrespuesta) <> UCase(RTrim(TextBoxRespuesta.Text)) Then
                FailureText.ForeColor = Drawing.Color.Red
                FailureText.Text = " Respuesta Incorrecta"
                intentos = intentos + 1
                If intentos = 3 Then
                    'FailureText.ForeColor = Drawing.Color.Red
                    FailureText.Text = " 3 (tres) Intentos Fallidos."
                    BtnEnviar.Enabled = False
                    TextBoxRespuesta.Attributes.Add("onkeydown", "return false;")
                End If
                Return
            End If
            Dim wnom As String = ""
            If wPersona = 2 Then
                wnom = wdenominacion
            Else
                wnom = wnombre & " " & wapellido
            End If

            If Len(RTrim(wemail)) > 0 Then
                Dim sSubject As String
                Dim sBody As String
                sSubject = "INTeatroDigital - Recupero de Contraseña"
                sBody = "Estimado usuario de INTeatroDigital: " & wnom & " ,su contraseña es : " & wcontrasena
                sBody += "<br />"
                sBody += "Lo invitamos a leer detenidamente la sección 'PREGUNTAS FRECUENTES' (ubicada en la barra superior de " & "<br />"
                sBody += "la plataforma de INTeatroDigital) con el objeto de familiarizarse con las particularidades del " & "<br />"
                sBody += "Registro Nacional del Teatro Independiente." & "<br />"
                sBody += "<br />"
                sBody += "Por necesidad de asistencia técnica, consultas, reclamos o sugerencias sobre INTeatroDigital no dude " & "<br />"
                sBody += "en ingresar a la sección 'FORMULARIO DE CONTACTO' (ubicada en la barra superior de la plataforma) y " & "<br />"
                sBody += "llenar el formulario correspondiente." & "<br />"
                sBody += "<br />"
                sBody += "Gracias.<br />"
                sBody += "<br />"
                sBody += "INTeatroDigital.<br />"
                Try
                    Mail.SendMail(wemail, sSubject, sBody)
                Catch ex As Exception
                    UserMsgBox(Me, "Error al enviar correo")
                End Try
            End If
            Response.Clear()
            Response.Redirect("ConfirmRecup.aspx", False)
        End If
    End Sub

    Private Sub Verificar()
        cn.Open()
        Dim sql As String = "select codigo,descrip from recupcontra where codigo = (Select Pregunta from Regisdig where cuil = " & Username.Text & ")"
        'Dim sql As String = "select codigo,descrip from recupcontra where codigo in (1,3,5)"
        Dim Psql As New SqlClient.SqlCommand(sql, cn)
        Dim dr As SqlClient.SqlDataReader = Psql.ExecuteReader
        DdlPregunta.DataSource = dr
        DdlPregunta.DataTextField = "descrip"
        DdlPregunta.DataValueField = "codigo"
        DdlPregunta.DataBind()
        cn.Close()
        dr.Close()
        cn.Open()
        sql = "select email from REGISDIG g where g.CUIL=" & Username.Text
        Dim Psqlg As New SqlClient.SqlCommand(sql, cn)
        Dim drg As SqlClient.SqlDataReader = Psqlg.ExecuteReader
        Dim wemail As String = ""
        While drg.Read()
            wemail = drg.GetString(0)
        End While
        drg.Close()
        cn.Close()
        If Len(RTrim(wemail)) > 0 Then
            BtnEnviar.Enabled = True
            Dim arr As Integer = wemail.IndexOf("@")
            If arr > 0 Then
                Dim mail As String = Left(wemail, arr)
                Dim nombre As String = ""
                Dim m As Integer = 1
                Dim s As Integer = 0
                While m <= arr
                    If m <= 3 Then
                        nombre = nombre + Mid(mail, m, 1)
                    Else
                        If s < 4 Then
                            nombre = nombre + "X"
                            s = s + 1
                        Else
                            nombre = nombre + Mid(mail, m, 1)
                        End If
                    End If
                    m = m + 1
                End While
                nombre = nombre + Mid(wemail, arr + 1, Len(wemail) - arr)
                LabelEmail.Text = nombre
            Else
                LabelEmail.Text = wemail
            End If
        Else
            FailureText.Text = "CUIT / CUIL no registrado"
            BtnEnviar.Enabled = False
        End If
    End Sub

    Protected Sub BtnNorecuerdo_Click(sender As Object, e As EventArgs) Handles BtnNorecuerdo.Click
        tablaDatos.Visible = True
        TablaDatos2.Visible = False
    End Sub

    Protected Sub BtnMailActual_Click(sender As Object, e As EventArgs) Handles BtnMailActual.Click
        TablaDatos2.Visible = True
        tablaDatos.Visible = False
    End Sub

End Class