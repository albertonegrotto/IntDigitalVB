Public Partial Class InicioIndivJur
    Inherits System.Web.UI.Page
    Dim cn As New SqlClient.SqlConnection(SqlConex)
    ' Dim quien As usuario
    Dim Provincia As Integer
    Dim Persona As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Provincia = Session("id_provincia")
        Persona = Session("id_persona")

        If Not Page.IsPostBack Then
            'SeteaVariablesSession()
            inicializa()
            MaintainScrollPositionOnPostBack = True
            TextBoxCUIT.Text = Session("CUIL")
            TextBoxCUIT.Focus()
        End If
    End Sub

    Private Sub inicializa()
        Dim EntidadSociedad As String = Session("entidad_sociedad")
        Dim categ As Integer = Session("categoria")
        cn.Open()
        Dim sql As String = "Select 0 as codigo, 'Seleccione Provincia' as descrip union select codigo,descrip from provin where codigo>=2 and codigo<=94 order by codigo"
        Dim Psql As New SqlClient.SqlCommand(sql, cn)
        Dim dr As SqlClient.SqlDataReader = Psql.ExecuteReader
        ddlProvincia.DataSource = dr
        ddlProvincia.DataTextField = "descrip"
        ddlProvincia.DataValueField = "codigo"
        ddlProvincia.DataBind()
        cn.Close()
        dr.Close()
        cn.Open()
        Dim sql1 As String = "select codigo,descrip from personas order by codigo"
        Dim Psql1 As New SqlClient.SqlCommand(sql1, cn)
        Dim dr1 As SqlClient.SqlDataReader = Psql1.ExecuteReader
        ddlPersona.DataSource = dr1
        ddlPersona.DataTextField = "descrip"
        ddlPersona.DataValueField = "codigo"
        ddlPersona.DataBind()
        cn.Close()
        dr1.Close()
        ddlProvincia.SelectedValue = Provincia
        ddlPersona.SelectedValue = Persona
        ' ddlProvincia.Enabled = False
        ddlPersona.Enabled = False
        cn.Open()
        Dim sql6 As String = "select 0 as codloc,' Seleccione Localidad' as nomloc union select codloc,nomloc from localidades where provincia= " & Provincia & "order by nomloc"
        Dim Psql6 As New SqlClient.SqlCommand(sql6, cn)
        Dim dr6 As SqlClient.SqlDataReader = Psql6.ExecuteReader
        DdlLocalidad.DataSource = dr6
        DdlLocalidad.DataTextField = "nomloc"
        DdlLocalidad.DataValueField = "codloc"
        DdlLocalidad.DataBind()
        cn.Close()
        dr6.Close()
        cn.Open()
        Dim sql7 As String = "select 0 as codigo,' Seleccione Pregunta' as descrip union select codigo,descrip from recupcontra where codigo in (1,3,5)"
        Dim Psql7 As New SqlClient.SqlCommand(sql7, cn)
        Dim dr7 As SqlClient.SqlDataReader = Psql7.ExecuteReader
        DdlPregunta.DataSource = dr7
        DdlPregunta.DataTextField = "descrip"
        DdlPregunta.DataValueField = "codigo"
        DdlPregunta.DataBind()
        dr7.Close()

        Dim sql8 As String = "select '' as codigo,' Seleccione' as descrip union select distinct tipo codigo, tipo descrip from EntidadSociedad"
        Dim Psql8 As New SqlClient.SqlCommand(sql8, cn)
        Dim dr8 As SqlClient.SqlDataReader = Psql8.ExecuteReader
        ddlEntSoc.DataSource = dr8
        ddlEntSoc.DataTextField = "descrip"
        ddlEntSoc.DataValueField = "codigo"
        ddlEntSoc.DataBind()
        dr8.Close()

        Dim sql9 As String = "select 0 as codigo,' Seleccione Categoria' as descrip union select codigo, descrip from Sectores where Entidad = 1 or Sociedad = 1"
        Dim Psql9 As New SqlClient.SqlCommand(sql9, cn)
        Dim dr9 As SqlClient.SqlDataReader = Psql9.ExecuteReader
        ddlCat.DataSource = dr9
        ddlCat.DataTextField = "descrip"
        ddlCat.DataValueField = "codigo"
        ddlCat.DataBind()
        cn.Close()
        dr9.Close()
        ddlEntSoc.SelectedValue = EntidadSociedad
        ddlCat.SelectedValue = categ

        ddlEntSoc.Enabled = False
        ddlCat.Enabled = False

    End Sub

    Protected Sub BtnEnviar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnEnviar.Click

        'Begin of Nuevo
        'Limpio los campos
        FailureText.Text = ""
        lblErrorCheckBoxAcepto.Text = ""
        lblErrorTextBoxCUIT.Text = ""
        lblErrorDdlLocalidad.Text = ""
        lblErrorTelefono.Text = ""
        lblErrorTextBoxMail.Text = ""
        lblErrorTextBoxContra.Text = ""
        lblErrorDdlPregunta.Text = ""
        lblErrorTextBoxRespuesta.Text = ""
        'End of limpio los campos

        'Chequeo los checkbox
        If CheckBoxAcepto.Checked = False Then
            lblErrorCheckBoxAcepto.Text = " Debe aceptar los términos y Condiciones"
            CheckBoxAcepto.Focus()
            Return
        End If
        'End of chequeo los checkbox

        'Valido CUIL
        If Not Validaciones.ValidarCUIT(TextBoxCUIT.Text.Trim()) Then
            lblErrorTextBoxCUIT.Text = "CUIT erróneo"
            TextBoxCUIT.Focus()
            Return
        End If
        If Convert.ToInt32(Left(TextBoxCUIT.Text.Trim(), 2)) < 30 Then
            lblErrorTextBoxCUIT.Text = "El CUIT no pertenece a una Persona Jurídica"
            TextBoxCUIT.Focus()
            Return
        End If
        Dim wcuit As Decimal
        wcuit = TextBoxCUIT.Text
        If Not cn.State = ConnectionState.Open Then
            cn.Open()
        End If
        Dim sql As String = "select * from regisdig where cuil=" & wcuit
        Dim Psql As New SqlClient.SqlCommand(sql, cn)
        Dim dr As SqlClient.SqlDataReader = Psql.ExecuteReader
        If dr.HasRows = True Then
            lblErrorTextBoxCUIT.Text = " CUIT Existente"
            TextBoxCUIT.Focus()
            Return
        End If
        cn.Close()
        dr.Close()
        Dim wprovincia As Integer = ddlProvincia.SelectedValue
        If DdlLocalidad.SelectedValue = 0 Then
            lblErrorDdlLocalidad.Text = " Debe seleccionar localidad"
            DdlLocalidad.Focus()
            Return
        Else
            Dim wlocalidad As Integer = DdlLocalidad.SelectedValue
        End If
        'Validación teléfono
        Dim wprefipart As Integer = 0
        Dim wtelepart As Integer = 0
        If Len(RTrim(TextBoxPrefTelPart.Text)) > 0 Then
            Try
                wprefipart = CInt(TextBoxPrefTelPart.Text)
            Catch ex As Exception
                lblErrorTelefono.Text = " Prefijo TE particular Incorrecto"
                TextBoxPrefTelPart.Focus()
                Return
            End Try
            If wprefipart < 11 And wprefipart > 0 Then
                lblErrorTelefono.Text = " Prefijo TE particular Incorrecto"
                TextBoxPrefTelPart.Focus()
                Return
            End If
        End If

        If Len(RTrim(TextBoxTelPart.Text)) > 0 Then
            Try
                wtelepart = CInt(TextBoxTelPart.Text)
            Catch ex As Exception
                lblErrorTelefono.Text = " Teléfono particular Incorrecto"
                TextBoxPrefTelPart.Focus()
                Return
            End Try
            If wtelepart < 10000 And wtelepart > 0 Then
                lblErrorTelefono.Text = " Teléfono particular Incorrecto"
                TextBoxPrefTelPart.Focus()
                Return
            End If
        End If

        'If (wprefipart = 0 Or wtelepart = 0) Then
        '    lblErrorTelefono.Text = " Debe ingresar el Teléfono"
        '    TextBoxPrefTelPart.Focus()
        '    Return
        'End If
        'End of validación teléfono

        Dim sMail As String
        Dim sMailConfirmacion As String

        sMail = TextBoxMail.Text.Trim
        sMailConfirmacion = TextBoxConfMail.Text.Trim
        If sMail <> sMailConfirmacion Then
            lblErrorTextBoxMail.Text = " No coincide el email y su confirmación"
            TextBoxConfMail.Focus()
            Return
        End If
        If YaExiste(sMail) Then
            lblErrorTextBoxMail.Text = " Esta cuenta de correo ya ha sido utilizada por otra persona"
            TextBoxMail.Focus()
            Return
        End If

        Dim wcontrasena As String = TextBoxContra.Text.Trim
        If Len(wcontrasena) <> 8 Then
            lblErrorTextBoxContra.Text = "Debe ingresar 8 caracteres"
            TextBoxContra.Focus()
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
            lblErrorTextBoxContra.Text = "La contraseña debe tener por lo menos 1 (un) número"
            Return
        End If
        Dim wconfirmcont As String = RTrim(TextBoxReContra.Text)
        If wcontrasena <> wconfirmcont Then
            lblErrorTextBoxContra.Text = "No coincide la Contraseña"
            TextBoxContra.Focus()
            Return
        End If

        Dim wpregunta As Integer = DdlPregunta.SelectedValue
        If wpregunta = 0 Then
            lblErrorDdlPregunta.Text = "Debe elegir pregunta de contraseña"
            DdlPregunta.Focus()
            Return
        End If
        Dim wrespuesta As String = RTrim(TextBoxRespuesta.Text).ToUpper
        If Len(wrespuesta) = 0 Then
            lblErrorTextBoxRespuesta.Text = "Debe ingresar respuesta de contraseña"
            TextBoxRespuesta.Focus()
            Return
        End If
        'End of Nuevo

        Dim wdenominacion As String = TextBoxDenomina.Text.Trim.ToUpper
        Dim wpersoneria As String = TextBoxPersoner.Text
        Dim wcopost = CInt(TextBoxCopost.Text)
        Dim wdomipart As String = RTrim(TextBoxDomicilio.Text).ToUpper
        Dim wemail As String = RTrim(TextBoxMail.Text)

        If Funciones.CaracteresEspecialesnumeros(wdenominacion) Then
            FailureText.Text = "La denominación contiene caracteres especiales"
            TextBoxDenomina.Focus()
            Return
        End If

        Dim wcodigo As Integer = 0
        Dim qsl As String = "Execute alta_regisdig @WPROVINCIA,@WPERSONA,@WCUIL,@WFECHNAC,@WAPELLIDO," & _
         "@WNOMBRE,@WSEXO,@WLOCALIDAD,@WCOPOST,@WDOMIPART,@WPREFIPART,@WTELEPART,@WPREFICELU,@WCELUPART,@WEMAIL," & _
         "@WCONTRASENA,@WPREGUNTA,@WRESPUESTA,@WDENOMINACION,@WPERSONERIA,@WDOMICILIO,@WNACIONAL,@WRESIDENCIA,@wcodigo, @IDEntidadSociedad"

        Dim cmd As New SqlClient.SqlCommand(qsl, cn)
        cmd.Parameters.AddWithValue("@WPROVINCIA", wprovincia)
        cmd.Parameters.AddWithValue("@WPERSONA", Session("id_persona"))
        cmd.Parameters.AddWithValue("@WCUIL", CDec(TextBoxCUIT.Text))
        cmd.Parameters.AddWithValue("@WFECHNAC", DateTime.Now)
        cmd.Parameters.AddWithValue("@WAPELLIDO", "")
        cmd.Parameters.AddWithValue("@WNOMBRE", "")
        cmd.Parameters.AddWithValue("@WSEXO", 1)
        cmd.Parameters.AddWithValue("@WLOCALIDAD", DdlLocalidad.SelectedValue)
        cmd.Parameters.AddWithValue("@WCOPOST", wcopost)
        cmd.Parameters.AddWithValue("@WDOMIPART", wdomipart.Trim.ToUpper)
        cmd.Parameters.AddWithValue("@WPREFIPART", wprefipart)
        cmd.Parameters.AddWithValue("@WTELEPART", wtelepart)
        cmd.Parameters.AddWithValue("@WPREFICELU", 0)
        cmd.Parameters.AddWithValue("@WCELUPART", 0)
        cmd.Parameters.AddWithValue("@WEMAIL", wemail)
        cmd.Parameters.AddWithValue("@WCONTRASENA", wcontrasena)
        cmd.Parameters.AddWithValue("@WPREGUNTA", DdlPregunta.SelectedValue)
        cmd.Parameters.AddWithValue("@WRESPUESTA", wrespuesta.Trim.ToUpper)
        cmd.Parameters.AddWithValue("@WDENOMINACION", RTrim(TextBoxDenomina.Text.Trim.ToUpper))
        cmd.Parameters.AddWithValue("@WPERSONERIA", wpersoneria)
        cmd.Parameters.AddWithValue("@WDOMICILIO", RTrim(TextBoxDomicilio.Text.Trim.ToUpper))
        cmd.Parameters.AddWithValue("@WNACIONAL", 1)
        cmd.Parameters.AddWithValue("@WRESIDENCIA", 0)
        cmd.Parameters.AddWithValue("@wcodigo", wcodigo)
        cmd.Parameters.AddWithValue("@IDEntidadSociedad", Convert.ToInt32(ddlCat.SelectedItem.Value))
        cn.Close()
        cn.Open()
        Dim wcod As Integer = 0
        Try
            wcod = CInt(cmd.ExecuteScalar())
            Session("wsolicitud_") = wcod
        Catch ex As Exception
            FailureText.Text = " Error al grabar Datos"
            Return
        End Try
        cn.Close()
        If Len(RTrim(wemail)) > 0 Then
            Dim wcodi As String = wcod.ToString
            Dim wpers As String = RTrim(ddlPersona.SelectedItem.Text)
            Dim wprov As String = RTrim(ddlProvincia.SelectedItem.Text)
            Dim wlocali As String = RTrim(DdlLocalidad.SelectedItem.Text)
            Dim wperso As String = wpersoneria.ToString

            'Enviar email
            Dim sResult As String
            Dim sSubject As String
            Dim sBody As String

            sSubject = "INTeatroDigital - Alta Individual de Persona Jurídica - CUIT " & TextBoxCUIT.Text
            sBody = "Se ha recepcionado su gestión de: ALTA INDIVIDUAL (PERSONA JURIDICA)" & "<br />" & "<br />"
            sBody += "Quedando ingresados los siguientes datos:" & "<br />"
            sBody += "Persona: " & wpers & "<br />"
            sBody += "Provincia: " & wprov & "<br />"
            sBody += "CUIL / CUIT: " & TextBoxCUIT.Text & "<br />"
            sBody += "Personería Nº: " & wperso & "<br />"
            sBody += "Denominación: " & RTrim(wdenominacion) & "<br />"
            sBody += "Localidad: " & wlocali & "<br />"
            sBody += "Domicilio: " & wdomipart & "<br />"
            'sBody += "Teléfono Particular: " & wprefipart.ToString & " " & wtelepart.ToString & "<br />"
            sBody += "<br />"
            sBody += Mail.GetTextoAviso(MAIL_ALTA_INDIV_JUR) & "<br />"
            sBody += "<br />"
            'sBody += "Click para confirmar<br />"
            sBody += Mail.GetLink(MAIL_ALTA_INDIV_JUR, wcod) & "<br />"
            sBody += "<br />"
            sBody += "Si este mensaje no lo visualiza en formato HTML, debe copiar el hipervínculo "
            sBody += "que ve mas arriba en su navegador de internet.<br />"
            sBody += "Lo invitamos a leer detenidamente la sección 'PREGUNTAS FRECUENTES' (ubicada en la barra superior de " & "<br />"
            sBody += "la plataforma de INTeatroDigital) con el objeto de familiarizarse con las particularidades del  " & "<br />"
            sBody += "Registro Nacional del Teatro Independiente.  " & "<br />"
            sBody += "<br />"
            sBody += "Por necesidad de asistencia técnica, consultas, reclamos o sugerencias sobre INTeatroDigital no dude  " & "<br />"
            sBody += "en ingresar a la sección 'FORMULARIO DE CONTACTO' (ubicada en la barra superior de la plataforma) y  " & "<br />"
            sBody += "llenar el formulario correspondiente.  " & "<br />"
            sBody += "<br />"
            sBody += "Gracias." & "<br />"
            sBody += "<br />"
            sBody += "INTeatroDigital" & "<br />"
            sResult = SendMail(Mail.GetMailTo(wcod, TIPO_PERSONA), sSubject, sBody)

            If sResult = "OK" Then
                Session.Add("CUIT_TEMP", TextBoxCUIT.Text)

                sMail = MAIL_CONTROL
                sResult = SendMail(sMail, sSubject, sBody)

            Else
                Response.Redirect("confirmaRegistro.aspx?r=er&m=" & sResult & "&t=j")
            End If
            'End of Enviar email

            'A la representación provincial:
            Dim sResult2 As String = ""
            Dim wemailprov As String = ""
            cn.Open()
            sql = "select mail from provinciasmail where idprovincia=" & ddlProvincia.SelectedValue
            Dim cdmm As New SqlClient.SqlCommand(sql, cn)
            Dim drp As SqlClient.SqlDataReader = cdmm.ExecuteReader
            While drp.Read
                wemailprov = drp.GetString(0)
            End While
            drp.Close()
            cn.Close()
            sSubject = "INTeatroDigital - Alta Individual de Persona Jurídica - CUIT " & TextBoxCUIT.Text
            sBody = "ALTA INDIVIDUAL del usuario de INTeatroDigital: " & RTrim(wdenominacion) & " - " & TextBoxCUIT.Text & "<br />"
            sBody += "Quedando ingresados los siguientes datos:" & "<br />"
            sBody += "Persona: " & wpers & "<br />"
            sBody += "Provincia: " & wprov & "<br />"
            sBody += "CUIL / CUIT: " & TextBoxCUIT.Text & "<br />"
            sBody += "Personería Nº: " & wperso & "<br />"
            sBody += "Denominación: " & RTrim(wdenominacion) & "<br />"
            sBody += "Localidad: " & wlocali & "<br />"
            sBody += "Domicilio: " & wdomipart & "<br />"
            'sBody += "Teléfono Particular: " & wprefipart.ToString & " " & wtelepart.ToString & "<br />"
            sResult2 = SendMail(wemailprov, sSubject, sBody)

            sMail = MAIL_CONTROL
            sResult2 = SendMail(sMail, sSubject, sBody)

            Response.Redirect("confirmaRegistro.aspx?r=ok&m=" & sResult & "&t=j")
        End If

        Response.Clear()
        Response.Redirect("ConfirmaIndiv.aspx", False)

    End Sub

    Private Sub btnCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        Response.Redirect("index.aspx")
    End Sub

    Protected Sub ddlProvincia_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlProvincia.SelectedIndexChanged
        If Not cn.State = ConnectionState.Open Then
            cn.Open()
        End If
        Dim sql6 As String = "select 0 as codloc,' Seleccione Localidad' as nomloc union select codloc,nomloc from localidades where provincia= " & ddlProvincia.SelectedItem.Value & "order by nomloc"
        Dim Psql6 As New SqlClient.SqlCommand(sql6, cn)
        Dim dr6 As SqlClient.SqlDataReader = Psql6.ExecuteReader
        DdlLocalidad.DataSource = dr6
        DdlLocalidad.DataTextField = "nomloc"
        DdlLocalidad.DataValueField = "codloc"
        DdlLocalidad.DataBind()
        cn.Close()
        dr6.Close()
    End Sub

    Protected Sub AceptoDJ_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles AceptoDJ.CheckedChanged
        tablaDatos.Visible = AceptoDJ.Checked

    End Sub

    Private Function YaExiste(ByVal mail As String) As Boolean
        If Not cn.State = ConnectionState.Open Then
            cn.Open()
        End If
        Dim CUIT As String = TextBoxCUIT.Text.Trim()
        Dim sql As String = String.Format("Select Count(*) from regisdig where email = '{0}' and CUIL<>" & CUIT, mail)
        Dim cmd As New SqlClient.SqlCommand(sql, cn)
        Dim veces As Integer = CType(cmd.ExecuteScalar, Integer)
        cn.Close()
        If veces > 0 Then
            Return True
        Else
            Return False
        End If
    End Function
End Class