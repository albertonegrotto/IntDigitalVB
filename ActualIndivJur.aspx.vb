Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration

Partial Public Class ActualIndivJur
    Inherits System.Web.UI.Page

    Private Const F_CUIL As Integer = 0
    Private Const F_DENOMINACION As Integer = 1
    Private Const F_PROVINCIA As Integer = 2
    Private Const F_LOCALIDAD As Integer = 3
    Private Const F_DOMICILIO As Integer = 4
    Dim cn As New SqlClient.SqlConnection(SqlConex)
    Dim aKeyFields(5) As String
    Dim ValorCategoria As Integer
    Dim quien As usuario
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If User.Identity.IsAuthenticated Then
                SeteaVariablesSession()
                TextBoxCUIT.Text = Session("Cuit")
                TextBoxCUIT.Enabled = False
                inicializa()
                'BtnEnviar.Enabled = False
                TextBoxDenomina.Focus()
            Else
                Response.Clear()
                Response.Redirect("index.aspx", False)
            End If
        Else
            MaintainScrollPositionOnPostBack = True
        End If
    End Sub
    Private Sub SeteaVariablesSession()
        quien = CType(Session("usuario"), usuario)
        Session("Cuit") = quien.Usuario
        Session("id_provincia") = quien.codprovin
        If quien.Persona = "Fisica" Then
            Session("id_persona") = 1
        Else
            Session("id_persona") = 2
        End If
    End Sub
    Public Sub inicializa()
        Dim Provincia As Integer = Session("id_provincia")
        Dim Persona As Integer = Session("id_persona")
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
        'ddlProvincia.Enabled = False
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
        Dim sql7 As String = "select 0 as codigo,' Seleccione Pregunta' as descrip union select codigo,descrip from recupcontra"
        Dim Psql7 As New SqlClient.SqlCommand(sql7, cn)
        Dim dr7 As SqlClient.SqlDataReader = Psql7.ExecuteReader
        DdlPregunta.DataSource = dr7
        DdlPregunta.DataTextField = "descrip"
        DdlPregunta.DataValueField = "codigo"
        DdlPregunta.DataBind()
        dr7.Close()
        Dim sqlq As String = "select '' as codigo,'Seleccione ' as descrip union select distinct Tipo codigo, Tipo descrip from EntidadSociedad order by codigo"
        Dim Psqlq As New SqlClient.SqlCommand(sqlq, cn)
        Dim drq As SqlClient.SqlDataReader = Psqlq.ExecuteReader
        ddlEntidadSociedad.DataSource = drq
        ddlEntidadSociedad.DataTextField = "descrip"
        ddlEntidadSociedad.DataValueField = "codigo"
        ddlEntidadSociedad.DataBind()
        drq.Close()
        cn.Close()

        Dim wCODIGO As Integer = 0
        Dim wPROVINCIA As Integer = 0
        Dim wPERSONA As Integer = 0
        Dim wDENOMINACION As String = ""
        Dim wLOCALIDAD As Integer = 0
        Dim wCOPOST As Integer = 0
        Dim wDOMICILIO As String = ""
        Dim wPREFIPART As Integer = 0
        Dim wTELEPART As Integer = 0
        Dim wPERSONERIA As String = ""
        Dim wEMAIL As String = ""
        'Dim wCONTRASENA As String = ""
        Dim wPREGUNTA As Integer = 0
        Dim wRESPUESTA As String = ""
        Dim wEntidadSociedad As Integer = 0
        cn.Open()
        Dim wuser As String = Session("Cuit").ToString
        Dim sql9 As String = "select * from REGISDIG where cuil=" & wuser & " and fechbaja is null"
        Dim Psql9 As New SqlClient.SqlCommand(sql9, cn)
        Dim dr9 As SqlClient.SqlDataReader = Psql9.ExecuteReader
        If dr9.HasRows = True Then
            While (dr9.Read())
                wCODIGO = dr9.GetInt32(0)
                wPROVINCIA = dr9.GetInt32(1)
                wPERSONA = dr9.GetInt32(2)
                wDENOMINACION = RTrim(UCase(dr9.GetString(19)))
                wLOCALIDAD = dr9.GetInt32(8)
                wCOPOST = dr9.GetInt32(9)
                wPERSONERIA = dr9.GetString(20)
                wDOMICILIO = RTrim(UCase(dr9.GetString(21)))
                wPREFIPART = dr9.GetInt32(11)
                wTELEPART = dr9.GetInt32(12)
                wEMAIL = RTrim(dr9.GetString(15))
                'wCONTRASENA = RTrim(UCase(dr9.GetString(16)))
                wPREGUNTA = dr9.GetInt32(17)
                wRESPUESTA = RTrim(UCase(dr9.GetString(18)))
                wEntidadSociedad = dr9.GetInt32(45)
            End While
        End If
        dr9.Close()
        LabelSoli.Text = wCODIGO.ToString
        ddlProvincia.SelectedValue = wPROVINCIA
        TextBoxDenomina.Text = wDENOMINACION
        TextBoxPersoner.Text = wPERSONERIA.ToString
        DdlLocalidad.SelectedValue = wLOCALIDAD
        TextBoxPersoner.Text = wPERSONERIA.ToString
        TextBoxDomicilio.Text = wDOMICILIO
        TextBoxCopost.Text = wCOPOST.ToString
        TextBoxPrefTelPart.Text = wPREFIPART.ToString
        TextBoxTelPart.Text = wTELEPART.ToString
        TextBoxMail.Text = wEMAIL
        TextBoxConfMail.Text = wEMAIL
        'TextBoxContra.Text = wCONTRASENA
        DdlPregunta.SelectedValue = wPREGUNTA
        TextBoxRespuesta.Text = wRESPUESTA
        'TextBoxReContra.Text = wCONTRASENA
        CheckBoxAcepto.Checked = True
        ddlEntidadSociedad.SelectedValue = GetValorEntidadSociedad(wEntidadSociedad)
        ddlCategoria.SelectedValue = wEntidadSociedad

        aKeyFields(F_CUIL) = wuser
        aKeyFields(F_DENOMINACION) = wDENOMINACION
        aKeyFields(F_PROVINCIA) = wPROVINCIA
        aKeyFields(F_LOCALIDAD) = wLOCALIDAD
        aKeyFields(F_DOMICILIO) = wDOMICILIO
        ViewState("KEY_FIELDS") = aKeyFields

    End Sub
    Private Function GetValorEntidadSociedad(ByVal entsoc As Integer) As String
        Dim sqlq As String = "select isnull(Entidad, 0) from Sectores where codigo = " & entsoc.ToString
        Dim Psqlq As New SqlClient.SqlCommand(sqlq, cn)
        Dim resultado As String
        Dim escalar As Integer
        'cn.Open()
        If cn.State <> ConnectionState.Open Then
            cn.Open()
        End If
        escalar = Psqlq.ExecuteScalar
        If escalar = 1 Then
            resultado = "Entidad"
        Else
            resultado = "Sociedad"
        End If
        cn.Close()
        SetDdlCategoria(resultado)
        Return resultado
    End Function

    Private Sub SetDdlCategoria(ByVal entidadsociedad As String)
        Dim cn As New SqlClient.SqlConnection(SqlConex)
        cn.Open()
        Dim sql As String = "select '' as codigo,'Seleccione Categoría ' as descrip union select codigo, descrip from Sectores where " & entidadsociedad & " = 1"
        Dim Psql As New SqlClient.SqlCommand(sql, cn)
        Dim dr As SqlClient.SqlDataReader = Psql.ExecuteReader
        ddlCategoria.DataSource = dr
        ddlCategoria.DataTextField = "descrip"
        ddlCategoria.DataValueField = "codigo"
        ddlCategoria.DataBind()
        cn.Close()
        dr.Close()
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
        'lblErrorTextBoxContra.Text = ""
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
        Dim wpregunta As Integer = DdlPregunta.SelectedValue
        If wpregunta = 0 Then
            lblErrorDdlPregunta.Text = " Debe elegir pregunta de contraseña"
            DdlPregunta.Focus()
            Return
        End If
        Dim wrespuesta As String = RTrim(TextBoxRespuesta.Text).ToUpper
        If Len(wrespuesta) = 0 Then
            lblErrorTextBoxRespuesta.Text = " Debe ingresar respuesta de contraseña"
            TextBoxRespuesta.Focus()
            Return
        End If
        Dim wcodigo As Integer = 0
        Try
            wcodigo = CInt(LabelSoli.Text)
        Catch ex As Exception
            FailureText.Text = " Solicitud Incorrecta"
            Return
        End Try
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

        Dim qsl As String = "Execute actu_regisdig @WPROVINCIA,@WPERSONA,@WCODIGO,@WFECHNAC,@WAPELLIDO," & _
         "@WNOMBRE,@WSEXO,@WLOCALIDAD,@WCOPOST,@WDOMIPART,@WPREFIPART,@WTELEPART,@WPREFICELU,@WCELUPART,@WEMAIL," & _
         "@WPREGUNTA,@WRESPUESTA,@WDENOMINACION,@WPERSONERIA,@WDOMICILIO,@WNACIONAL,@WRESIDENCIA, @IDENTIDADSOCIEDAD"
        Dim cmd As New SqlClient.SqlCommand(qsl, cn)

        If Session("id_provincia") <> ddlProvincia.SelectedItem.Value Then
            cmd.Parameters.AddWithValue("@WPROVINCIA", ddlProvincia.SelectedItem.Value)
        Else
            cmd.Parameters.AddWithValue("@WPROVINCIA", Session("id_provincia"))
        End If
        cmd.Parameters.AddWithValue("@WPERSONA", Session("id_persona"))
        cmd.Parameters.AddWithValue("@WCODIGO", wcodigo)
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
        'cmd.Parameters.AddWithValue("@WCONTRASENA", wcontrasena)
        cmd.Parameters.AddWithValue("@WPREGUNTA", DdlPregunta.SelectedValue)
        cmd.Parameters.AddWithValue("@WRESPUESTA", wrespuesta.Trim.ToUpper)
        cmd.Parameters.AddWithValue("@WDENOMINACION", wdenominacion.Trim.ToUpper)
        cmd.Parameters.AddWithValue("@WPERSONERIA", wpersoneria)
        cmd.Parameters.AddWithValue("@WDOMICILIO", wdomipart.Trim.ToUpper)
        cmd.Parameters.AddWithValue("@WNACIONAL", 1)
        cmd.Parameters.AddWithValue("@WRESIDENCIA", 0)
        cmd.Parameters.AddWithValue("@IDENTIDADSOCIEDAD", Convert.ToInt32(ddlCategoria.SelectedValue))
        cn.Close()
        cn.Open()
        Try
            Dim wcod As Integer = CInt(cmd.ExecuteScalar())
            Session("wsolicitud_") = wcodigo
            Session("id_provincia") = ddlProvincia.SelectedItem.Value
        Catch ex As Exception
            FailureText.Text = " Error al grabar Datos"
            Return
        End Try
        'Actualizo los integrantes si se modificó el CUIL
        wcuit = TextBoxCUIT.Text.Trim
        If ViewState("KEY_FIELDS")(F_CUIL) <> wcuit Then
            ActualizarCUILIntegrantes(ViewState("KEY_FIELDS")(F_CUIL), wcuit)
        End If
        If Len(RTrim(wemail)) > 0 Then
            Dim wcodi As String = LabelSoli.Text.Trim.ToString
            Dim wpers As String = RTrim(ddlPersona.SelectedItem.Text)
            Dim wprov As String = RTrim(ddlProvincia.SelectedItem.Text)
            Dim wlocali As String = RTrim(DdlLocalidad.SelectedItem.Text)
            Dim wperso As String = wpersoneria.ToString

            'Modificación de Persona Jurídica
            'Enviar email
            Dim sResult As String
            Dim sSubject As String
            Dim sBody As String

            sSubject = "INTeatroDigital - Actualización de Datos de Persona Jurídica - CUIT  " & TextBoxCUIT.Text
            sBody = "Se ha recepcionado su gestión de: ACTUALIZACION DE DATOS (PERSONA JURIDICA)" & "<br />" & "<br />"
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

            sBody += Mail.GetTextoAviso(MAIL_MODIF_INDIV_JUR) & "<br />"
            sBody += "<br />"

            'sBody += "Click para confirmar<br />"
            sBody += Mail.GetLink(MAIL_MODIF_INDIV_JUR, LabelSoli.Text.Trim) & "<br />"
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

            sResult = SendMail(Mail.GetMailTo(LabelSoli.Text.Trim, TIPO_PERSONA), sSubject, sBody)
            'End of Modificación de Persona Jurídica

            'Campos claves modificados
            If CamposModificados() Then
                Dim MyConnection As SqlConnection
                Dim MyCommand As SqlCommand
                Dim sSQLCmd As String

                'Envío un mail a la provincia
                Dim MyReader As SqlDataReader
                Dim sCodigo As String = ""
                Dim sNombreResponsable As String = ""
                Dim sMail As String
                Dim sResult2 As String = ""
                Try
                    Dim sProv As Integer = ddlProvincia.SelectedItem.Value
                    'Obtengo el mail de la provinci
                    sSQLCmd = "select mail from provinciasmail where idprovincia=" & sProv
                    MyConnection = New SqlConnection()
                    MyConnection.ConnectionString = ConfigurationManager.ConnectionStrings("INTeatroDig").ConnectionString
                    MyConnection.Open()
                    MyCommand = New SqlCommand(sSQLCmd, MyConnection)
                    MyReader = MyCommand.ExecuteReader()
                    Do While MyReader.Read()
                        sMail = MyReader.Item(0).ToString.Trim
                        sSubject = "INTeatroDigital - Actualización de Datos de Persona Jurídica - CUIT  " & TextBoxCUIT.Text
                        sBody = "ACTUALIZACION DE DATOS del usuario de INTeatroDigital: " & RTrim(wdenominacion) & " - " & TextBoxCUIT.Text & "<br />"
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

                        sResult2 = SendMail(sMail, sSubject, sBody)

                        sMail = MAIL_CONTROL
                        sResult2 = SendMail(sMail, sSubject, sBody)
                    Loop
                Catch ex As Exception

                End Try
                MyReader.Dispose()
                MyCommand.Dispose()
                MyConnection.Dispose()
            End If
            'End of Campos claves modificados

            If sResult = "OK" Then
                Session.Add("CUIT_TEMP", TextBoxCUIT.Text)
                Response.Redirect("confirmaRegistro.aspx?r=ok&m=" & sResult & "&t=j")
            Else
                Response.Redirect("confirmaRegistro.aspx?r=er&m=" & sResult & "&t=j")
            End If
            'End of Enviar email

        End If

        Response.Clear()
        Response.Redirect("ConfActuIndiv.aspx", False)

    End Sub

    Protected Function ActualizarCUILIntegrantes(ByVal sCUILViejo As String, ByVal sCUILNuevo As String) As Boolean
        Dim MyConnection As SqlConnection
        Dim MyCommand As SqlCommand
        Dim sSQLCmd As String

        Try
            'UPDATE Integrantes
            sSQLCmd = "UPDATE Integrantes " &
                           "SET CUIL = " & sCUILNuevo & "  " &
                         "WHERE CUIL = " & sCUILViejo

            MyConnection = New SqlConnection()
            MyConnection.ConnectionString = ConfigurationManager.ConnectionStrings("INTeatroDig").ConnectionString
            MyConnection.Open()

            MyCommand = New SqlCommand()
            MyCommand.CommandText = sSQLCmd
            MyCommand.CommandType = CommandType.Text
            MyCommand.Connection = MyConnection

            MyCommand.ExecuteNonQuery()

            MyCommand.Dispose()
            MyConnection.Dispose()

            Return True

        Catch ex As Exception
            Return False
        End Try

    End Function

    Protected Function CamposModificados() As Boolean

        If ViewState("KEY_FIELDS")(F_CUIL) <> TextBoxCUIT.Text.Trim Or _
            ViewState("KEY_FIELDS")(F_DENOMINACION) <> TextBoxDenomina.Text.Trim Or _
            ViewState("KEY_FIELDS")(F_PROVINCIA) <> ddlProvincia.SelectedValue Or _
            ViewState("KEY_FIELDS")(F_LOCALIDAD) <> DdlLocalidad.SelectedValue Or _
            ViewState("KEY_FIELDS")(F_DOMICILIO) <> TextBoxDomicilio.Text.Trim Then
            Return True
        Else
            Return False
        End If

    End Function

    Protected Sub ddlProvincia_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlProvincia.SelectedIndexChanged
        Dim prov As String = ddlProvincia.SelectedValue.ToString
        cn.Open()
        Dim sql6 As String = "select 0 as codloc,' Seleccione Localidad' as nomloc union select codloc,nomloc from localidades where provincia= " & prov & "order by nomloc"
        Dim Psql6 As New SqlClient.SqlCommand(sql6, cn)
        Dim dr6 As SqlClient.SqlDataReader = Psql6.ExecuteReader
        DdlLocalidad.DataSource = dr6
        DdlLocalidad.DataTextField = "nomloc"
        DdlLocalidad.DataValueField = "codloc"
        DdlLocalidad.DataBind()
        cn.Close()
    End Sub

    Private Sub btnCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        Response.Redirect("menuFinal.aspx")
    End Sub

    Protected Sub ddlEntidadSociedad_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlEntidadSociedad.SelectedIndexChanged
        SetDdlCategoria(ddlEntidadSociedad.SelectedItem.Value)
    End Sub

    Protected Sub AceptoDJ_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles AceptoDJ.CheckedChanged
        tablaDatos.Visible = AceptoDJ.Checked
    End Sub

    Protected Sub ddlCategoria_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlCategoria.SelectedIndexChanged

    End Sub
End Class