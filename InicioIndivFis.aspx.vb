Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.IO
Imports System.IO.Packaging
Imports DocumentFormat.OpenXml.Packaging
Imports DocumentFormat.OpenXml.Wordprocessing
Partial Public Class InicioIndivFis
    Inherits System.Web.UI.Page
    Dim cn As New SqlClient.SqlConnection(SqlConex)
    Dim Provincia As Integer
    Dim Persona As Integer
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Provincia = Session("id_provincia")
        Persona = Session("id_persona")
        If Not Page.IsPostBack Then
            inicializa()
            TextBoxCUIT.Text = Session("CUIL")
            TextBoxCUIT.Enabled = False
            ddlDia.Focus()
        Else
            GuardarAdjunto()
            MaintainScrollPositionOnPostBack = True
        End If
    End Sub

    Private Sub GuardarAdjunto()
        If Session("UploadImporta1") Is Nothing Or UploadImporta.HasFile Then
            If UploadImporta.HasFile Then
                Dim FileName As String = Path.GetFileName(UploadImporta.PostedFile.FileName)
                Dim Extension As String = Path.GetExtension(UploadImporta.PostedFile.FileName)
                Dim wfecha As Date = DateTime.Now.ToString
                Dim wdia As Integer = wfecha.Day
                Dim wmes As Integer = wfecha.Month
                Dim wano As Integer = wfecha.Year
                Dim whora As Integer = wfecha.Hour
                Dim wminu As Integer = wfecha.Minute
                Dim wsegu As Integer = wfecha.Second
                Dim woperador As String = Session("CUIL")
                Dim wdir As String = wano.ToString + wmes.ToString + wdia.ToString + whora.ToString + wminu.ToString + wsegu.ToString
                If UCase(Extension) = ".PDF" Or UCase(Extension) = ".DOC" Or UCase(Extension) = ".DOCX" Then
                    Dim randomName As String = RTrim(woperador) + wdir
                    If Not Directory.Exists(Server.MapPath("~/Temp/" & randomName & "/")) Then
                        Directory.CreateDirectory(Server.MapPath("~/Temp/" & randomName & "/"))
                    End If
                    Dim FilePath As String = (Server.MapPath("~/Temp/" & randomName & "/")) + FileName
                    Try
                        UploadImporta.SaveAs(FilePath)
                    Catch ex As Exception
                    End Try
                    Session("UploadImporta1") = UploadImporta
                    Session("UploadFileName") = FilePath
                    LabelNombreUpload.Text = UploadImporta.FileName
                End If
            End If
        Else
            If Session("UploadImporta1") IsNot Nothing Then
                Dim UploadImporta1 As FileUpload = CType(Session("UploadImporta1"), FileUpload)
                LabelNombreUpload.Text = UploadImporta1.FileName
            End If
        End If
    End Sub

    Private Sub inicializa()
        'Dim Provincia As Integer = Session("id_provincia")
        'Dim Persona As Integer = Session("id_persona")
        cn.Open()
        Dim sql As String = "select 0 as codigo, 'Seleccione Provincia' as descrip union select codigo,descrip from provin where codigo>=2 and codigo<=94 order by codigo"
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
        ddlProvincia.Enabled = True
        ddlPersona.Enabled = False
        cn.Open()
        Dim sql2 As String = "select 0 as codigo,'Día' as descrip union select codigo,descrip from dias order by codigo"
        Dim Psql2 As New SqlClient.SqlCommand(sql2, cn)
        Dim dr2 As SqlClient.SqlDataReader = Psql2.ExecuteReader
        ddlDia.DataSource = dr2
        ddlDia.DataTextField = "descrip"
        ddlDia.DataValueField = "codigo"
        ddlDia.DataBind()
        cn.Close()
        dr2.Close()
        cn.Open()
        Dim sql3 As String = "select 0 as codigo,'Elija Mes' as descrip union select codigo,descrip from meses order by codigo"
        Dim Psql3 As New SqlClient.SqlCommand(sql3, cn)
        Dim dr3 As SqlClient.SqlDataReader = Psql3.ExecuteReader
        ddlMes.DataSource = dr3
        ddlMes.DataTextField = "descrip"
        ddlMes.DataValueField = "codigo"
        ddlMes.DataBind()
        cn.Close()
        dr3.Close()
        cn.Open()
        Dim sql4 As String = "select 0 as codigo,'Año' as descrip union select codigo,descrip from anios order by codigo"
        Dim Psql4 As New SqlClient.SqlCommand(sql4, cn)
        Dim dr4 As SqlClient.SqlDataReader = Psql4.ExecuteReader
        ddlAnio.DataSource = dr4
        ddlAnio.DataTextField = "descrip"
        ddlAnio.DataValueField = "codigo"
        ddlAnio.DataBind()
        cn.Close()
        dr4.Close()
        cn.Open()
        Dim sql5 As String = "select 0 as codigo,'Seleccione' as descrip union select codigo,descrip from sexo order by codigo"
        Dim Psql5 As New SqlClient.SqlCommand(sql5, cn)
        Dim dr5 As SqlClient.SqlDataReader = Psql5.ExecuteReader
        ddlSexo.DataSource = dr5
        ddlSexo.DataTextField = "descrip"
        ddlSexo.DataValueField = "codigo"
        ddlSexo.DataBind()
        cn.Close()
        dr5.Close()
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
        cn.Close()
        dr6.Close()
        cn.Open()
        Dim sql8 As String = "select 0 as codigo,' Seleccione Nacionalidad' as descrip union select codigo,descrip from nacional"
        Dim Psql8 As New SqlClient.SqlCommand(sql8, cn)
        Dim dr8 As SqlClient.SqlDataReader = Psql8.ExecuteReader
        DdlNacional.DataSource = dr8
        DdlNacional.DataTextField = "descrip"
        DdlNacional.DataValueField = "codigo"
        DdlNacional.DataBind()
        cn.Close()
        dr8.Close()
        cn.Open()
        Dim sql9 As String = "select 0 as codigo,'Seleccione' as descrip union select 1 as codigo,'SI' as descrip union select 2 as codigo,'NO' as descrip"
        Dim Psql9 As New SqlClient.SqlCommand(sql9, cn)
        Dim dr9 As SqlClient.SqlDataReader = Psql9.ExecuteReader
        DDlWhatsApp.DataSource = dr9
        DDlWhatsApp.DataTextField = "descrip"
        DDlWhatsApp.DataValueField = "codigo"
        DDlWhatsApp.DataBind()
        cn.Close()
        dr9.Close()
        cn.Open()
        Dim sql10 As String = "select 0 as codigo,' Seleccione Tipo de Formación' as descrip union select codigo,descrip from TIPOFORMACION"
        Dim Psql10 As New SqlClient.SqlCommand(sql10, cn)
        Dim dr10 As SqlClient.SqlDataReader = Psql10.ExecuteReader
        DdlFormacion.DataSource = dr10
        DdlFormacion.DataTextField = "descrip"
        DdlFormacion.DataValueField = "codigo"
        DdlFormacion.DataBind()
        cn.Close()
        dr10.Close()
        cn.Open()
        Dim sql11 As String = "select 0 as codigo,' Seleccione Título Alcanzado' as descrip union select codigo,descrip from TITULOALCANZADO"
        Dim Psql11 As New SqlClient.SqlCommand(sql11, cn)
        Dim dr11 As SqlClient.SqlDataReader = Psql11.ExecuteReader
        DdlTitulo.DataSource = dr11
        DdlTitulo.DataTextField = "descrip"
        DdlTitulo.DataValueField = "codigo"
        DdlTitulo.DataBind()
        cn.Close()
        dr11.Close()
        DdlTitulo.Enabled = False
    End Sub

    Protected Sub BtnEnviar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnEnviar.Click
        'Begin of Nuevo
        'Limpio los campos
        FailureText.Text = ""
        txtErrorActividad.Text = ""
        txtErrorDisciplina.Text = ""
        lblErrorCheckBoxEdad.Text = ""
        lblErrorCheckBox1.Text = ""
        lblErrorCheckBoxAcepto.Text = ""
        lblErrorcheckAutorizoPublicar.Text = ""
        lblErrorTextBoxCUIT.Text = ""
        lblErrorFechaNacimiento.Text = ""
        lblErrorddlSexo.Text = ""
        lblErrorDdlLocalidad.Text = ""
        lblErrorTelefono.Text = ""
        lblErrorCelular.Text = ""
        lblErrorTextBoxMail.Text = ""
        lblErrorTextBoxContra.Text = ""
        lblErrorDdlPregunta.Text = ""
        lblErrorTextBoxRespuesta.Text = ""
        lblErrorDdlNacional.Text = ""
        lblErrorTextBoxresid.Text = ""
        LabelErrorFormacion.Text = ""
        LabelErrorTitulo.Text = ""
        'Chequeo los checkbox
        If CheckBox1.Checked = False Then
            lblErrorCheckBox1.Text = " Debe aceptar los términos y Condiciones"
            CheckBox1.Focus()
            Return
        End If
        If CheckBoxAcepto.Checked = False Then
            lblErrorCheckBoxAcepto.Text = " Debe aceptar los términos y Condiciones"
            CheckBoxAcepto.Focus()
            Return
        End If
        If checkAutorizoPublicar.Checked = False Then
            lblErrorcheckAutorizoPublicar.Text = " Debe aceptar los términos y Condiciones"
            Return
        End If
        If Not chkActividad1.Checked And Not chkActividad2.Checked And Not chkActividad3.Checked And
            Not chkActividad4.Checked And Not chkActividad5.Checked And Not chkActividad6.Checked And
            Not chkActividad7.Checked And Not chkActividad8.Checked And Not chkActividad9.Checked And
            Not chkActividad10.Checked And Not chkActividad11.Checked And Not chkActividad12.Checked And
            Not chkActividad13.Checked And Not chkActividad14.Checked And Not chkActividad15.Checked And
            Not chkActividad16.Checked And Not chkActividad17.Checked And Not chkActividad18.Checked And
            Not chkActividad19.Checked And Not chkActividad20.Checked And Not chkActividad21.Checked And
            Not chkActividad22.Checked Then
            txtErrorActividad.Text = " Debe seleccionar al menos una profesión"
            chkActividad1.Focus()
            Return
        End If
        If Not ChkDiscipli1.Checked And Not ChkDiscipli2.Checked And Not ChkDiscipli3.Checked And
            Not ChkDiscipli4.Checked And Not ChkDiscipli5.Checked And Not ChkDiscipli6.Checked And
            Not ChkDiscipli7.Checked And Not ChkDiscipli8.Checked And Not ChkDiscipli9.Checked And
            Not ChkDiscipli10.Checked And Not ChkDiscipli11.Checked And Not ChkDiscipli12.Checked And
            Not ChkDiscipli13.Checked And Not ChkDiscipli14.Checked And Not ChkDiscipli15.Checked And
            Not ChkDiscipli16.Checked And Not ChkDiscipli17.Checked And Not ChkDiscipli18.Checked And
            Not ChkDiscipli19.Checked And Not ChkDiscipli20.Checked And Not ChkDiscipli21.Checked And
            Not ChkDiscipli20.Checked And Not ChkDiscipli21.Checked And Not ChkDiscipli22.Checked And
            Not ChkDiscipli23.Checked And Not ChkDiscipli24.Checked And Not ChkDiscipli25.Checked And
            Not ChkDiscipli26.Checked And Not ChkDiscipli27.Checked And Not ChkDiscipli28.Checked And
            Not ChkDiscipli29.Checked And Not ChkDiscipli30.Checked And Not ChkDiscipli31.Checked And
            Not ChkDiscipli32.Checked And Not ChkDiscipli33.Checked And Not ChkDiscipli34.Checked And
            Not ChkDiscipli35.Checked Then
            txtErrorDisciplina.Text = " Debe seleccionar al menos una disciplina"
            ChkDiscipli1.Focus()
            Return
        End If
        'End of chequeo los checkbox
        'Valido CUIL
        If Not Validaciones.ValidarCUIT(TextBoxCUIT.Text.Trim()) Then
            lblErrorTextBoxCUIT.Text = "CUIT/CUIL erróneo"
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
        'Valido fecha de nacimiento
        Dim wd As Integer
        Dim wm As Integer
        Dim wa As Integer
        Try
            wd = ddlDia.SelectedItem.Text
            wm = ddlMes.SelectedValue
            wa = ddlAnio.SelectedItem.Text
        Catch ex As Exception
            lblErrorFechaNacimiento.Text = " Fecha de Nacimiento Incorrecta"
            ddlDia.Focus()
            Return
        End Try
        If Not Validaciones.ValidarFecha(wd, wm, wa) Then
            lblErrorFechaNacimiento.Text = "Fecha de Nacimiento Incorrecta"
            ddlDia.Focus()
            Return
        End If
        Dim wfechnac As DateTime = DateTime.Now
        If Not GetFecha(wd.ToString + "/" + wm.ToString + "/" + wa.ToString, wfechnac) Then
            lblErrorFechaNacimiento.Text = " Fecha de Nacimiento Incorrecta"
            ddlDia.Focus()
            Return
        End If
        If Calcular_Edad(wd.ToString + "/" + wm.ToString + "/" + wa.ToString) < 18 Then
            If CheckBoxEdad.Checked = False Then
                lblErrorCheckBoxEdad.Text = " Debe aceptar Condición de edad"
                CheckBoxEdad.Focus()
                Return
            End If
        End If
        If ddlSexo.SelectedValue = 0 Then
            lblErrorddlSexo.Text = " Debe Ingresar el sexo"
            ddlSexo.Focus()
            Return
        Else
            Dim wsexo As Integer = ddlSexo.SelectedValue
        End If
        If DdlLocalidad.SelectedValue = 0 Then
            lblErrorDdlLocalidad.Text = " Debe seleccionar localidad"
            DdlLocalidad.Focus()
            Return
        Else
            Dim wlocalidad As Integer = DdlLocalidad.SelectedValue
        End If
        'Validación teléfonos
        'Dim bUnTelefono As Boolean
        'bUnTelefono = False
        'Valida teléfonos
        Dim wprefipart As Integer = 0
        Dim wtelepart As Integer = 0
        Dim wpreficelu As Integer = 0
        Dim wcelupart As Integer = 0
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
        If Len(RTrim(TextBoxPrefTelPart.Text)) + Len(RTrim(TextBoxTelPart.Text)) > 10 Then
            lblErrorTelefono.Text = " Teléfono particular Incorrecto"
            TextBoxPrefTelPart.Focus()
            Return
        End If
        If Len(RTrim(TextBoxPrefCelu.Text)) > 0 Then
            Try
                wpreficelu = CInt(TextBoxPrefCelu.Text)
            Catch ex As Exception
                lblErrorCelular.Text = " Prefijo TE Celular Incorrecto"
                TextBoxPrefCelu.Focus()
                Return
            End Try
            If wpreficelu < 11 And wpreficelu > 0 Then
                lblErrorCelular.Text = " Prefijo TE Celular Incorrecto"
                TextBoxPrefCelu.Focus()
                Return
            End If
        End If
        If Len(RTrim(TextBoxCelular.Text)) > 0 Then
            Try
                wcelupart = CInt(TextBoxCelular.Text)
            Catch ex As Exception
                lblErrorCelular.Text = " Teléfono Celular Incorrecto"
                TextBoxPrefCelu.Focus()
                Return
            End Try
            If wtelepart < 10000 And wtelepart > 0 Then
                lblErrorCelular.Text = " Teléfono Celular Incorrecto"
                TextBoxPrefCelu.Focus()
                Return
            End If
        End If
        If Len(RTrim(TextBoxPrefCelu.Text)) + Len(RTrim(TextBoxCelular.Text)) > 10 Then
            lblErrorTelefono.Text = " Teléfono particular Incorrecto"
            TextBoxPrefTelPart.Focus()
            Return
        End If
        If (wprefipart = 0 Or wtelepart = 0) And (wpreficelu = 0 Or wcelupart = 0) Then
            lblErrorTelefono.Text = " Debe ingresar por lo menos un Teléfono"
            TextBoxPrefCelu.Focus()
            Return
        End If
        'End of validación teléfonos
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
            TextBoxConfMail.Focus()
            Return
        End If
        Dim arr As Integer = TextBoxMail.Text.Trim.IndexOf("@")
        If arr <= 0 And Len(TextBoxMail.Text.Trim) > 0 Then
            lblErrorTextBoxMail.Text = " Cuenta de Correo Electrónica errónea"
            TextBoxConfMail.Focus()
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
            lblErrorTextBoxContra.Text = " No coincide la Contraseña"
            TextBoxContra.Focus()
            Return
        End If
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
        Dim wnacional As Integer = DdlNacional.SelectedValue
        If wnacional = 0 Then
            lblErrorDdlNacional.Text = " Debe elegir Nacionalidad"
            DdlNacional.Focus()
            Return
        End If

        Dim wresidencia As Integer = 0
        If wnacional = 2 Then
            Try
                wresidencia = CInt(TextBoxresid.Text)
            Catch ex As Exception
                lblErrorTextBoxresid.Text = " Residencia Incorrecta"
                TextBoxresid.Focus()
                Return
            End Try
        End If

        If wnacional = 2 And (wresidencia >= 0 And wresidencia <= 4) Then
            lblErrorTextBoxresid.Text = " Residencia Incorrecta, debe ser mayor a 4 años"
            TextBoxresid.Focus()
            Return
        End If
        'End of Nuevo
        If Len(Trim(TextBoxDompar.Text)) = 0 Then
            lblErrorTextBoxDompar.Text = "Debe completar su domicilio"
            TextBoxDompar.Focus()
            Return
        End If

        Dim wcopost As Int32
        If TextBoxCopost.Text.Length = 0 Then
            lblErrorCPOSTAL.Text = "Debe ingresar el Código Postal"
            TextBoxCopost.Focus()
            Return
        Else
            Try
                wcopost = CInt(TextBoxCopost.Text)
            Catch ex As Exception
                lblErrorCPOSTAL.Text = "Código Postal Incorrecto"
                TextBoxCopost.Focus()
                Return
            End Try
        End If
        Dim wformacion As Integer = DdlFormacion.SelectedValue
        If wformacion = 0 Then
            LabelErrorFormacion.Text = "Debe seleccionar Tipo de Formación "
            Return
        End If
        Dim wtitulo As Integer = DdlTitulo.SelectedValue
        If wformacion = 1 And wtitulo = 0 Then
            LabelErrorTitulo.Text = "Debe seleccionar Título alcanzado"
            Return
        End If
        Dim wapellido As String = RTrim(TextBoxApellido.Text).ToUpper
        Dim wnombre As String = RTrim(TextBoxNombre.Text).ToUpper
        Dim wdomipart As String = RTrim(TextBoxDompar.Text).ToUpper
        Dim wemail As String = RTrim(TextBoxMail.Text)
        Dim wobservaotros As String = ""

        If Funciones.CaracteresEspeciales(wnombre) Then
            FailureText.Text = "El nombre contiene caracteres especiales"
            TextBoxNombre.Focus()
            Return
        End If

        If Funciones.CaracteresEspecialesn(wapellido) Then
            FailureText.Text = "El apellido contiene caracteres especiales"
            TextBoxApellido.Focus()
            Return
        End If

        If UploadImporta.HasFile Then
            Dim Extension As String = Path.GetExtension(UploadImporta.PostedFile.FileName)
            If UCase(Extension) <> ".PDF" And UCase(Extension) <> ".DOC" And UCase(Extension) <> ".DOCX" Then
                FailureText.Text = "El CV no es un documento Adobe .PDF o Word .DOC .DOCX"
                Return
            End If
            Dim sizeInBytes As Long = UploadImporta.PostedFile.ContentLength
            If sizeInBytes / 1000000 > 10 Then
                FailureText.Text = "El CV tiene un tamaño mayor a 10 Mb"
                Return
            End If
        Else
            If Session("UploadFileName") IsNot Nothing Then
                Dim UploadImporta1 As FileUpload = CType(Session("UploadImporta1"), FileUpload)
                Dim Extension As String = Path.GetExtension(UploadImporta1.PostedFile.FileName)
                If UCase(Extension) <> ".PDF" And UCase(Extension) <> ".DOC" And UCase(Extension) <> ".DOCX" Then
                    FailureText.Text = "El CV no es un documento Adobe PDF o Word DOC DOCX"
                    Return
                End If
                Dim sizeInBytes As Long = UploadImporta1.PostedFile.ContentLength
                If sizeInBytes / 1000000 > 10 Then
                    FailureText.Text = "El CV tiene un tamaño mayor a 10 Mb"
                    Return
                End If
            End If
        End If

        Dim wcodigo As Integer = 0
        Dim qsl As String = "Execute alta_regisdig_fisica_activ @WPROVINCIA,@WPERSONA,@WCUIL,@WFECHNAC,@WAPELLIDO," &
         "@WNOMBRE,@WSEXO,@WLOCALIDAD,@WCOPOST,@WDOMIPART,@WPREFIPART,@WTELEPART,@WPREFICELU,@WCELUPART,@WEMAIL," &
         "@WCONTRASENA,@WPREGUNTA,@WRESPUESTA,@WDENOMINACION,@WPERSONERIA,@WDOMICILIO,@WNACIONAL,@WRESIDENCIA," &
          "@actividad1, @actividad2, @actividad3, " &
          "@actividad4, @actividad5, @actividad6, " &
          "@actividad7, @actividad8, @actividad9, " &
          "@actividad10, @actividad11, @actividad12, " &
          "@actividad13, @actividad14, @actividad15, " &
          "@actividad16, @actividad17, @actividad18,@wobservaotros," &
          "@wcodigo"

        Dim cmd As New SqlClient.SqlCommand(qsl, cn)
        cmd.Parameters.AddWithValue("@WPROVINCIA", ddlProvincia.SelectedValue)
        cmd.Parameters.AddWithValue("@WPERSONA", Session("id_persona"))
        cmd.Parameters.AddWithValue("@WCUIL", CDec(TextBoxCUIT.Text))
        cmd.Parameters.AddWithValue("@WFECHNAC", wfechnac)
        cmd.Parameters.AddWithValue("@WAPELLIDO", wapellido.Trim.ToUpper)
        cmd.Parameters.AddWithValue("@WNOMBRE", wnombre.Trim.ToUpper)
        cmd.Parameters.AddWithValue("@WSEXO", ddlSexo.SelectedValue)
        cmd.Parameters.AddWithValue("@WLOCALIDAD", DdlLocalidad.SelectedValue)
        cmd.Parameters.AddWithValue("@WCOPOST", wcopost)
        cmd.Parameters.AddWithValue("@WDOMIPART", wdomipart.Trim.ToUpper)
        cmd.Parameters.AddWithValue("@WPREFIPART", wprefipart)
        cmd.Parameters.AddWithValue("@WTELEPART", wtelepart)
        cmd.Parameters.AddWithValue("@WPREFICELU", wpreficelu)
        cmd.Parameters.AddWithValue("@WCELUPART", wcelupart)
        cmd.Parameters.AddWithValue("@WEMAIL", wemail)
        cmd.Parameters.AddWithValue("@WCONTRASENA", wcontrasena)
        cmd.Parameters.AddWithValue("@WPREGUNTA", DdlPregunta.SelectedValue)
        cmd.Parameters.AddWithValue("@WRESPUESTA", wrespuesta.Trim.ToUpper)
        cmd.Parameters.AddWithValue("@WDENOMINACION", "")
        cmd.Parameters.AddWithValue("@WPERSONERIA", 0)
        cmd.Parameters.AddWithValue("@WDOMICILIO", "")
        cmd.Parameters.AddWithValue("@WNACIONAL", wnacional)
        cmd.Parameters.AddWithValue("@WRESIDENCIA", wresidencia)

        cmd.Parameters.AddWithValue("@actividad1", 0)
        cmd.Parameters.AddWithValue("@actividad2", 0)
        cmd.Parameters.AddWithValue("@actividad3", 0)
        cmd.Parameters.AddWithValue("@actividad4", 0)
        cmd.Parameters.AddWithValue("@actividad5", 0)
        cmd.Parameters.AddWithValue("@actividad6", 0)
        cmd.Parameters.AddWithValue("@actividad7", 0)
        cmd.Parameters.AddWithValue("@actividad8", 0)
        cmd.Parameters.AddWithValue("@actividad9", 0)
        cmd.Parameters.AddWithValue("@actividad10", 0)
        cmd.Parameters.AddWithValue("@actividad11", 0)
        cmd.Parameters.AddWithValue("@actividad12", 0)
        cmd.Parameters.AddWithValue("@actividad13", 0)
        cmd.Parameters.AddWithValue("@actividad14", 0)
        cmd.Parameters.AddWithValue("@actividad15", 0)
        cmd.Parameters.AddWithValue("@actividad16", 0)
        cmd.Parameters.AddWithValue("@actividad17", 0)
        cmd.Parameters.AddWithValue("@actividad18", 0)
        cmd.Parameters.AddWithValue("@wobservaotros", wobservaotros)
        cmd.Parameters.AddWithValue("@wcodigo", wcodigo)
        If Not cn.State = ConnectionState.Open Then
            cn.Open()
        End If
        Dim wcod As Integer = 0
        Try
            wcod = CInt(cmd.ExecuteScalar())
            Session("wsolicitud_") = wcod
        Catch ex As Exception
            FailureText.Text = " Error al grabar Datos REGISDIG"
            Return
        End Try
        cn.Close()
        If wcod > 0 Then
            'WHATASPP y EDUCACION
            Dim Whatsapp As Integer = DDlWhatsApp.SelectedValue
            sql = "update REGISDIG set whatsapp=" & Whatsapp & ",formacion=" & wformacion & ",titulo=" & wtitulo & " where codigo=" & wcod
            Dim cmdw As New SqlClient.SqlCommand(sql, cn)
            cn.Open()
            Try
                cmdw.ExecuteNonQuery()
            Catch ex As Exception
                FailureText.Text = " Error al grabar Datos Regisdig"
                Return
            End Try
            cn.Close()
            ' REDES SOCIALES
            If facebook.Checked = True Then
                sql = "insert into REGISDIGREDES (IDREGISDIG,REDSOCIAL) values (" & wcod & "," & 1 & ")"
                Dim cmdr As New SqlClient.SqlCommand(sql, cn)
                cn.Open()
                Try
                    cmdr.ExecuteNonQuery()
                Catch ex As Exception
                    FailureText.Text = " Error al grabar Datos Redes"
                    Return
                End Try
                cn.Close()
            End If
            If instagram.Checked = True Then
                sql = "insert into REGISDIGREDES (IDREGISDIG,REDSOCIAL) values (" & wcod & "," & 2 & ")"
                Dim cmdr As New SqlClient.SqlCommand(sql, cn)
                cn.Open()
                Try
                    cmdr.ExecuteNonQuery()
                Catch ex As Exception
                    FailureText.Text = " Error al grabar Datos Redes"
                    Return
                End Try
                cn.Close()
            End If
            If twiter.Checked = True Then
                sql = "insert into REGISDIGREDES (IDREGISDIG,REDSOCIAL) values (" & wcod & "," & 3 & ")"
                Dim cmdr As New SqlClient.SqlCommand(sql, cn)
                cn.Open()
                Try
                    cmdr.ExecuteNonQuery()
                Catch ex As Exception
                    FailureText.Text = " Error al grabar Datos Redes"
                    Return
                End Try
                cn.Close()
            End If
            If youtube.Checked = True Then
                sql = "insert into REGISDIGREDES (IDREGISDIG,REDSOCIAL) values (" & wcod & "," & 4 & ")"
                Dim cmdr As New SqlClient.SqlCommand(sql, cn)
                cn.Open()
                Try
                    cmdr.ExecuteNonQuery()
                Catch ex As Exception
                    FailureText.Text = " Error al grabar Datos Redes"
                    Return
                End Try
                cn.Close()
            End If
            sql = "Execute alta_regisdigprofesion @WIDREGISDIG,@WPROFESION1,@WPROFESION2,@WPROFESION3,@WPROFESION4,@WPROFESION5,@WPROFESION6,@WPROFESION7,@WPROFESION8,@WPROFESION9,@WPROFESION10,@WPROFESION11," &
                  "@WPROFESION12,@WPROFESION13,@WPROFESION14,@WPROFESION15,@WPROFESION16,@WPROFESION17,@WPROFESION18,@WPROFESION19,@WPROFESION20,@WPROFESION21,@WPROFESION22"
            Dim cmdpf As New SqlClient.SqlCommand(sql, cn)
            cmdpf.Parameters.AddWithValue("@WIDREGISDIG", wcod)
            cmdpf.Parameters.AddWithValue("@WPROFESION1", IIf(chkActividad1.Checked = True, 1, 0))
            cmdpf.Parameters.AddWithValue("@WPROFESION2", IIf(chkActividad2.Checked = True, 1, 0))
            cmdpf.Parameters.AddWithValue("@WPROFESION3", IIf(chkActividad3.Checked = True, 1, 0))
            cmdpf.Parameters.AddWithValue("@WPROFESION4", IIf(chkActividad4.Checked = True, 1, 0))
            cmdpf.Parameters.AddWithValue("@WPROFESION5", IIf(chkActividad5.Checked = True, 1, 0))
            cmdpf.Parameters.AddWithValue("@WPROFESION6", IIf(chkActividad6.Checked = True, 1, 0))
            cmdpf.Parameters.AddWithValue("@WPROFESION7", IIf(chkActividad7.Checked = True, 1, 0))
            cmdpf.Parameters.AddWithValue("@WPROFESION8", IIf(chkActividad8.Checked = True, 1, 0))
            cmdpf.Parameters.AddWithValue("@WPROFESION9", IIf(chkActividad9.Checked = True, 1, 0))
            cmdpf.Parameters.AddWithValue("@WPROFESION10", IIf(chkActividad10.Checked = True, 1, 0))
            cmdpf.Parameters.AddWithValue("@WPROFESION11", IIf(chkActividad11.Checked = True, 1, 0))
            cmdpf.Parameters.AddWithValue("@WPROFESION12", IIf(chkActividad12.Checked = True, 1, 0))
            cmdpf.Parameters.AddWithValue("@WPROFESION13", IIf(chkActividad13.Checked = True, 1, 0))
            cmdpf.Parameters.AddWithValue("@WPROFESION14", IIf(chkActividad14.Checked = True, 1, 0))
            cmdpf.Parameters.AddWithValue("@WPROFESION15", IIf(chkActividad15.Checked = True, 1, 0))
            cmdpf.Parameters.AddWithValue("@WPROFESION16", IIf(chkActividad16.Checked = True, 1, 0))
            cmdpf.Parameters.AddWithValue("@WPROFESION17", IIf(chkActividad17.Checked = True, 1, 0))
            cmdpf.Parameters.AddWithValue("@WPROFESION18", IIf(chkActividad18.Checked = True, 1, 0))
            cmdpf.Parameters.AddWithValue("@WPROFESION19", IIf(chkActividad19.Checked = True, 1, 0))
            cmdpf.Parameters.AddWithValue("@WPROFESION20", IIf(chkActividad20.Checked = True, 1, 0))
            cmdpf.Parameters.AddWithValue("@WPROFESION21", IIf(chkActividad21.Checked = True, 1, 0))
            cmdpf.Parameters.AddWithValue("@WPROFESION22", IIf(chkActividad22.Checked = True, 1, 0))
            cn.Open()
            Try
                cmdpf.ExecuteNonQuery()
            Catch ex As Exception
                FailureText.Text = " Error al grabar Datos Profesion"
                Return
            End Try
            cn.Close()

            sql = "Execute alta_regisdigdiscipli @WIDREGISDIG,@WDISCIPLI1,@WDISCIPLI2,@WDISCIPLI3,@WDISCIPLI4,@WDISCIPLI5,@WDISCIPLI6,@WDISCIPLI7,@WDISCIPLI8,@WDISCIPLI9,@WDISCIPLI10,@WDISCIPLI11," &
                  "@WDISCIPLI12,@WDISCIPLI13,@WDISCIPLI14,@WDISCIPLI15,@WDISCIPLI16,@WDISCIPLI17,@WDISCIPLI18,@WDISCIPLI19,@WDISCIPLI20,@WDISCIPLI21,@WDISCIPLI22,@WDISCIPLI23,@WDISCIPLI24,@WDISCIPLI25," &
                  "@WDISCIPLI26,@WDISCIPLI27,@WDISCIPLI28,@WDISCIPLI29,@WDISCIPLI30,@WDISCIPLI31,@WDISCIPLI32,@WDISCIPLI33,@WDISCIPLI34,@WDISCIPLI35"
            Dim cmdpd As New SqlClient.SqlCommand(sql, cn)
            cmdpd.Parameters.AddWithValue("@WIDREGISDIG", wcod)
            cmdpd.Parameters.AddWithValue("@WDISCIPLI1", IIf(ChkDiscipli1.Checked = True, 1, 0))
            cmdpd.Parameters.AddWithValue("@WDISCIPLI2", IIf(ChkDiscipli2.Checked = True, 1, 0))
            cmdpd.Parameters.AddWithValue("@WDISCIPLI3", IIf(ChkDiscipli3.Checked = True, 1, 0))
            cmdpd.Parameters.AddWithValue("@WDISCIPLI4", IIf(ChkDiscipli4.Checked = True, 1, 0))
            cmdpd.Parameters.AddWithValue("@WDISCIPLI5", IIf(ChkDiscipli5.Checked = True, 1, 0))
            cmdpd.Parameters.AddWithValue("@WDISCIPLI6", IIf(ChkDiscipli6.Checked = True, 1, 0))
            cmdpd.Parameters.AddWithValue("@WDISCIPLI7", IIf(ChkDiscipli7.Checked = True, 1, 0))
            cmdpd.Parameters.AddWithValue("@WDISCIPLI8", IIf(ChkDiscipli8.Checked = True, 1, 0))
            cmdpd.Parameters.AddWithValue("@WDISCIPLI9", IIf(ChkDiscipli9.Checked = True, 1, 0))
            cmdpd.Parameters.AddWithValue("@WDISCIPLI10", IIf(ChkDiscipli10.Checked = True, 1, 0))
            cmdpd.Parameters.AddWithValue("@WDISCIPLI11", IIf(ChkDiscipli11.Checked = True, 1, 0))
            cmdpd.Parameters.AddWithValue("@WDISCIPLI12", IIf(ChkDiscipli12.Checked = True, 1, 0))
            cmdpd.Parameters.AddWithValue("@WDISCIPLI13", IIf(ChkDiscipli13.Checked = True, 1, 0))
            cmdpd.Parameters.AddWithValue("@WDISCIPLI14", IIf(ChkDiscipli14.Checked = True, 1, 0))
            cmdpd.Parameters.AddWithValue("@WDISCIPLI15", IIf(ChkDiscipli15.Checked = True, 1, 0))
            cmdpd.Parameters.AddWithValue("@WDISCIPLI16", IIf(ChkDiscipli16.Checked = True, 1, 0))
            cmdpd.Parameters.AddWithValue("@WDISCIPLI17", IIf(ChkDiscipli17.Checked = True, 1, 0))
            cmdpd.Parameters.AddWithValue("@WDISCIPLI18", IIf(ChkDiscipli18.Checked = True, 1, 0))
            cmdpd.Parameters.AddWithValue("@WDISCIPLI19", IIf(ChkDiscipli19.Checked = True, 1, 0))
            cmdpd.Parameters.AddWithValue("@WDISCIPLI20", IIf(ChkDiscipli20.Checked = True, 1, 0))
            cmdpd.Parameters.AddWithValue("@WDISCIPLI21", IIf(ChkDiscipli21.Checked = True, 1, 0))
            cmdpd.Parameters.AddWithValue("@WDISCIPLI22", IIf(ChkDiscipli22.Checked = True, 1, 0))
            cmdpd.Parameters.AddWithValue("@WDISCIPLI23", IIf(ChkDiscipli23.Checked = True, 1, 0))
            cmdpd.Parameters.AddWithValue("@WDISCIPLI24", IIf(ChkDiscipli24.Checked = True, 1, 0))
            cmdpd.Parameters.AddWithValue("@WDISCIPLI25", IIf(ChkDiscipli25.Checked = True, 1, 0))
            cmdpd.Parameters.AddWithValue("@WDISCIPLI26", IIf(ChkDiscipli26.Checked = True, 1, 0))
            cmdpd.Parameters.AddWithValue("@WDISCIPLI27", IIf(ChkDiscipli27.Checked = True, 1, 0))
            cmdpd.Parameters.AddWithValue("@WDISCIPLI28", IIf(ChkDiscipli28.Checked = True, 1, 0))
            cmdpd.Parameters.AddWithValue("@WDISCIPLI29", IIf(ChkDiscipli29.Checked = True, 1, 0))
            cmdpd.Parameters.AddWithValue("@WDISCIPLI30", IIf(ChkDiscipli30.Checked = True, 1, 0))
            cmdpd.Parameters.AddWithValue("@WDISCIPLI31", IIf(ChkDiscipli31.Checked = True, 1, 0))
            cmdpd.Parameters.AddWithValue("@WDISCIPLI32", IIf(ChkDiscipli32.Checked = True, 1, 0))
            cmdpd.Parameters.AddWithValue("@WDISCIPLI33", IIf(ChkDiscipli33.Checked = True, 1, 0))
            cmdpd.Parameters.AddWithValue("@WDISCIPLI34", IIf(ChkDiscipli34.Checked = True, 1, 0))
            cmdpd.Parameters.AddWithValue("@WDISCIPLI35", IIf(ChkDiscipli35.Checked = True, 1, 0))

            cn.Open()
            Try
                cmdpd.ExecuteNonQuery()
            Catch ex As Exception
                FailureText.Text = " Error al grabar Datos Disciplina"
                Return
            End Try
            cn.Close()

            'Guardar CV
            Dim woperador As String = TextBoxCUIT.Text
            Dim FileName As String = Path.GetFileName(UploadImporta.PostedFile.FileName)
            Dim Extension As String = Path.GetExtension(UploadImporta.PostedFile.FileName)
            Dim FolderPath As String = ConfigurationManager.AppSettings("FolderPath")
            Dim fileSavePath As Object = Server.MapPath("~/Documentos/REGISDIG/" & wcod & "/CV")
            Try
                MkDir(fileSavePath)
            Catch ex As Exception
            End Try
            If UploadImporta.HasFile Then
                Dim Filepath As String = fileSavePath + "\" + FileName
                Try
                    UploadImporta.SaveAs(Filepath)
                Catch ex As Exception
                    FailureText.Text = "No se pudo guardar documento de CV"
                    Return
                End Try
            Else
                If Session("UploadFileName") IsNot Nothing Then
                    Dim UploadImporta1 As FileUpload = CType(Session("UploadImporta1"), FileUpload)
                    FileName = UploadImporta1.FileName
                    Dim Filepath As String = Session("UploadFileName")
                    Dim FilepathDest As String = fileSavePath + "\" + FileName
                    Try
                        File.Copy(Filepath, FilepathDest)
                    Catch ex As Exception
                        FailureText.Text = "No se pudo guardar documento de CV"
                        Return
                    End Try
                End If
            End If
        End If

        If Len(RTrim(wemail)) > 0 Then
            Dim wcodi As String = wcod.ToString
            Dim wpers As String = RTrim(ddlPersona.SelectedItem.Text)
            Dim wprov As String = RTrim(ddlProvincia.SelectedItem.Text)
            Dim wsex As String = RTrim(ddlSexo.SelectedItem.Text)
            Dim wlocali As String = RTrim(DdlLocalidad.SelectedItem.Text)
            Dim wnacion As String = RTrim(DdlNacional.SelectedItem.Text)
            Dim wresid As String = RTrim(TextBoxresid.Text)

            'Enviar email
            Dim sResult As String
            Dim sSubject As String
            Dim sBody As String

            sSubject = "INTeatroDigital - Alta Individual de Persona Humana - CUIL " & TextBoxCUIT.Text
            sBody = "Estimada/o usuaria/o de INTeatroDigital: " & wnombre.Trim.ToUpper & " " & wapellido.Trim.ToUpper & " - " & TextBoxCUIT.Text & "<br />" & "<br />"
            sBody += "Se ha recepcionado su gestión de: ALTA INDIVIDUAL (PERSONA HUMANA)" & "<br />"
            sBody += "Quedando ingresados los siguientes datos:" & "<br />"
            sBody += "Persona: " & wpers & "<br />"
            sBody += "Provincia: " & wprov & "<br />"
            sBody += "CUIL / CUIT: " & TextBoxCUIT.Text & "<br />"
            sBody += "Fecha de Nacimiento: " & Left(wfechnac.ToString, 10) & "<br />"
            sBody += "Apellido y Nombre: " & RTrim(wapellido) & ", " & RTrim(wnombre) & "<br />"
            'sBody += "Sexo: " & wsex & "<br />"
            sBody += "Nacionalidad: " & wnacion & "<br />"
            'sBody += "Residencia: " & wresid & "<br />"
            sBody += "Localidad: " & wlocali & "<br />"
            sBody += "Domicilio: " & wdomipart & "<br />"
            sBody += "Teléfono Particular: " & wprefipart.ToString & " " & wtelepart.ToString & "<br />"
            sBody += "Teléfono Celular: " & wpreficelu.ToString & " 15" & wcelupart.ToString & "<br />"
            sBody += "<br />"
            'sBody += Mail.GetTextoAviso(MAIL_ALTA_INDIV_FIS) & "<br />"
            'sBody += "<br />"
            'sBody += "Click para confirmar<br />"
            sBody += Mail.GetLink(MAIL_ALTA_INDIV_FIS, wcod) & "<br />"
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
                Response.Redirect("confirmaRegistro.aspx?r=er&m=" & sResult & "&t=f")
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
            sSubject = "INTeatroDigital - Alta Individual de Persona Humana - CUIL " & TextBoxCUIT.Text
            sBody = "ALTA INDIVIDUAL del usuario de INTeatroDigital: " & wnombre.Trim.ToUpper & " " & wapellido.Trim.ToUpper & " - " & TextBoxCUIT.Text & "<br />"
            sBody += "Quedando ingresados los siguientes datos: " & "<br />"
            sBody += "Persona: " & wpers & "<br />"
            sBody += "Provincia: " & wprov & "<br />"
            sBody += "CUIL / CUIT: " & TextBoxCUIT.Text & "<br />"
            sBody += "Fecha de Nacimiento: " & Left(wfechnac.ToString, 10) & "<br />"
            sBody += "Apellido y Nombre: " & RTrim(wapellido) & ", " & RTrim(wnombre) & "<br />"
            'sBody += "Sexo: " & wsex & "<br />"
            sBody += "Nacionalidad: " & wnacion & "<br />"
            'sBody += "Residencia: " & wresid & "<br />"
            sBody += "Localidad: " & wlocali & "<br />"
            sBody += "Domicilio: " & wdomipart & "<br />"
            sBody += "Teléfono Particular: " & wprefipart.ToString & " " & wtelepart.ToString & "<br />"
            sBody += "Teléfono Celular: " & wpreficelu.ToString & " 15" & wcelupart.ToString & "<br />"
            sBody += "<br />"
            sResult2 = SendMail(wemailprov, sSubject, sBody)

            sMail = MAIL_CONTROL
            sResult2 = SendMail(sMail, sSubject, sBody)

            Response.Redirect("confirmaRegistro.aspx?r=ok&m=" & sResult & "&t=f")
        End If
        Response.Clear()
        Response.Redirect("ConfirmaIndiv.aspx", False)
    End Sub

    Private Sub btnCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        Response.Redirect("altaini.aspx")
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

    Protected Sub BtnVisualiza_Click(sender As Object, e As EventArgs) Handles BtnVisualiza.Click
        FailureText.Text = ""
        If UploadImporta.HasFile Or Session("UploadImporta1") IsNot Nothing Then
            Dim woperador As String = TextBoxCUIT.Text
            Dim FileName As String = Path.GetFileName(UploadImporta.PostedFile.FileName)
            Dim Extension As String = Path.GetExtension(UploadImporta.PostedFile.FileName)
            Dim FolderPath As String = ConfigurationManager.AppSettings("FolderPath")
            Dim FilePath As String = ""
            If UploadImporta.HasFile Then
                Dim wfecha As Date = DateTime.Now.ToString
                Dim wdia As Integer = wfecha.Day
                Dim wmes As Integer = wfecha.Month
                Dim wano As Integer = wfecha.Year
                Dim whora As Integer = wfecha.Hour
                Dim wminu As Integer = wfecha.Minute
                Dim wsegu As Integer = wfecha.Second
                Dim wdir As String = wano.ToString + wmes.ToString + wdia.ToString + whora.ToString + wminu.ToString + wsegu.ToString

                If UCase(Extension) <> ".PDF" And UCase(Extension) <> ".DOC" And UCase(Extension) <> ".DOCX" Then
                    FailureText.Text = "No es un documento Adobe .PDF o Word .DOC .DOCX"
                    Return
                End If
                Dim randomName As String = RTrim(woperador) + wdir
                If Not Directory.Exists(Server.MapPath("~/Temp/" & randomName & "/")) Then
                    Directory.CreateDirectory(Server.MapPath("~/Temp/" & randomName & "/"))
                End If
                FilePath = (Server.MapPath("~/Temp/" & randomName & "/")) + FileName
                Try
                    UploadImporta.SaveAs(FilePath)
                Catch ex As Exception
                End Try
                Session("UploadImporta1") = UploadImporta
                Session("UploadFileName") = FilePath
            Else
                FilePath = Session("UploadFileName")
                Dim letra As String = Right(FilePath.Trim, 1)
                If UCase(letra) = "X" Then
                    Extension = Right(FilePath.Trim, 5)
                Else
                    Extension = Right(FilePath.Trim, 4)
                End If
            End If

            If UCase(Extension) = ".PDF" Then
                Response.ContentType = "application/pdf"
                Response.AppendHeader("Content-Disposition", "attachment;filename=" & FilePath)
                Response.TransmitFile(FilePath)
                Response.End()
            End If

            If UCase(Extension) = ".DOC" Then
                Dim fileStream As New FileStream(FilePath, FileMode.Open)
                Dim bytBytes(fileStream.Length) As Byte
                fileStream.Read(bytBytes, 0, fileStream.Length)
                fileStream.Close()
                Response.Clear()
                Response.AddHeader("Content-Disposition", "attachment; filename=" & FilePath)
                Response.ContentType = "application/msword"
                Response.BinaryWrite(bytBytes)
                Response.Flush()
                Response.Close()
                Response.End()
            End If

            If UCase(Extension) = ".DOCX" Then
                Dim fileStream As New FileStream(FilePath, FileMode.Open)
                Dim memStream As MemoryStream = New MemoryStream()
                memStream.SetLength(fileStream.Length)
                fileStream.Read(memStream.GetBuffer(), 0, fileStream.Length)
                Response.Clear()
                Response.ContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document"
                Response.AddHeader("Content-Disposition", "attachment; filename=" & FilePath)
                Response.BinaryWrite(memStream.ToArray())
                Response.Flush()
                Response.Close()
                Response.End()
            End If

        End If
    End Sub
End Class