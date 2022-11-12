Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Collections

Partial Public Class registroEvento
    Inherits System.Web.UI.Page
    Dim cn As New SqlClient.SqlConnection(SqlConex)
    Dim aDeletedCUIL As New ArrayList
    Dim quien As usuario
    Shared ds As dsIntegrantes

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim sAccion As String
        Dim nCodigo As Integer
        quien = CType(Session("usuario"), usuario)
        If Not ds Is Nothing Then
            GridView1.DataSource = ds.Integrantes
            GridView1.DataBind()
            GridView1.Visible = True
        End If
        If Not Page.IsPostBack Then
            'La primera vez
            If User.Identity.IsAuthenticated Then
                SetearVariablesSession()
                sAccion = Request.QueryString("accion")
                nCodigo = Request.QueryString("codigo")
                Session("sAccion") = sAccion
                If sAccion = "M" Then
                    HyperLinkBack.Attributes.Add("href", "RegistroLista.aspx")
                End If
                If sAccion.ToUpper = "A" Then
                    Inicializar()
                    BorrarTemporal()
                    BtnGuardar.Text = "Confirmar Registro"
                ElseIf sAccion.ToUpper = "M" Then
                    CargarDatos(nCodigo)
                    BtnGuardar.Text = "Confirmar Actualización de Registro"
                    Session.Add("CODIGO", nCodigo)
                End If
                'La primera vez lo agrego al ViewState
                ViewState.Add("DELETED_CUIL", aDeletedCUIL)

                'Cargo la gridview
                dsInte.CargaIntegrantes(ds, nCodigo)
                GridView1.DataSource = ds.Integrantes
                GridView1.DataBind()
                GridView1.Visible = True

            Else
                Response.Clear()
                Response.Redirect("http://www.inteatro.gob.ar", False)
            End If

        Else
            'La respuesta
            MaintainScrollPositionOnPostBack = True
        End If

    End Sub

    Private Sub SetearVariablesSession()
        quien = CType(Session("usuario"), usuario)
        Session("USER_ID") = quien.Codigo
        Session("PROVINCIA") = quien.codprovin
        Session("CUIT") = quien.Usuario
        'Session("CODIGO") = quien.Codigo
        Session("SECTOR") = 9
    End Sub

    Private Sub Inicializar()
        Dim cn As New SqlClient.SqlConnection(SqlConex)

        'Sectores / Grupos
        cn.Open()
        Dim sql As String = "SELECT codigo, descrip FROM Sectores WHERE codigo = " & Session("SECTOR")
        Dim Psql As New SqlClient.SqlCommand(sql, cn)
        Dim dr As SqlClient.SqlDataReader = Psql.ExecuteReader
        ddlSectores.DataSource = dr
        ddlSectores.DataTextField = "descrip"
        ddlSectores.DataValueField = "codigo"
        ddlSectores.DataBind()
        cn.Close()
        dr.Close()

        'Provincias
        cn.Open()
        Dim sql2 As String = "Select 0 codigo, 'Seleccione Provincia' descrip union SELECT codigo,descrip FROM Provin WHERE region is not null" '& Session("PROVINCIA")
        Dim Psql2 As New SqlClient.SqlCommand(sql2, cn)
        Dim dr2 As SqlClient.SqlDataReader = Psql2.ExecuteReader
        ddlProvincias.DataSource = dr2
        ddlProvincias.DataTextField = "descrip"
        ddlProvincias.DataValueField = "codigo"
        ddlProvincias.DataBind()
        cn.Close()
        dr2.Close()

        'Localidades
        cn.Open()
        Dim sql3 As String = "select 0 as codloc,' Seleccione Localidad' as nomloc union select codloc,nomloc from localidades where provincia = " & Session("PROVINCIA") & " order by nomloc"
        Dim Psql3 As New SqlClient.SqlCommand(sql3, cn)
        Dim dr3 As SqlClient.SqlDataReader = Psql3.ExecuteReader
        ddlLocalidades.DataSource = dr3
        ddlLocalidades.DataTextField = "nomloc"
        ddlLocalidades.DataValueField = "codloc"
        ddlLocalidades.DataBind()
        cn.Close()
        dr3.Close()

        'Público
        cn.Open()
        Dim sql4 As String = "select 0 as codigo,' Seleccione Público' as descrip union select codigo,descrip from publico order by codigo"
        Dim Psql4 As New SqlClient.SqlCommand(sql4, cn)
        Dim dr4 As SqlClient.SqlDataReader = Psql4.ExecuteReader
        DdlPublico.DataSource = dr4
        DdlPublico.DataTextField = "descrip"
        DdlPublico.DataValueField = "codigo"
        DdlPublico.DataBind()
        cn.Close()
        dr4.Close()

        'Actividades
        cn.Open()
        Dim sql5 As String = "select 0 as codigo,' Seleccione Actividad' as descrip union select codigo,descrip from complementarias order by codigo"
        Dim Psql5 As New SqlClient.SqlCommand(sql5, cn)
        Dim dr5 As SqlClient.SqlDataReader = Psql5.ExecuteReader
        ddlActividad.DataSource = dr5
        ddlActividad.DataTextField = "descrip"
        ddlActividad.DataValueField = "codigo"
        ddlActividad.DataBind()
        cn.Close()
        dr5.Close()

        'Tipo de Gestión
        cn.Open()
        Dim sql6 As String = "select 0 as codigo,' Seleccione Gestión' as descrip union select codigo,descrip from tipogestion order by codigo"
        Dim Psql6 As New SqlClient.SqlCommand(sql6, cn)
        Dim dr6 As SqlClient.SqlDataReader = Psql6.ExecuteReader
        Ddlgestion.DataSource = dr6
        Ddlgestion.DataTextField = "descrip"
        Ddlgestion.DataValueField = "codigo"
        Ddlgestion.DataBind()
        cn.Close()
        dr6.Close()

    End Sub
    Private Sub GetLocal(ByVal pv As Integer)
        Dim cn As New SqlClient.SqlConnection(SqlConex)
        cn.Open()
        Dim sql3 As String = "select 0 as codloc,' Seleccione Localidad' as nomloc union select codloc,nomloc from localidades where provincia = " & pv & " order by nomloc"
        Dim Psql3 As New SqlClient.SqlCommand(sql3, cn)
        Dim dr3 As SqlClient.SqlDataReader = Psql3.ExecuteReader
        ddlLocalidades.DataSource = dr3
        ddlLocalidades.DataTextField = "nomloc"
        ddlLocalidades.DataValueField = "codloc"
        ddlLocalidades.DataBind()
        cn.Close()
        dr3.Close()

    End Sub
    Private Sub BorrarTemporal()
        Dim cn As New SqlClient.SqlConnection(SqlConex)
        cn.Open()
        Dim sql5 As String
        sql5 = "DELETE FROM IntegrantesTemp WHERE cuit = " & Session("CUIT")
        Dim Psql5 As New SqlClient.SqlCommand(sql5, cn)
        Psql5.ExecuteNonQuery()
        cn.Close()
    End Sub

    Private Sub CargarTemporal(ByVal nCodigo As Integer)
        Dim cn As New SqlClient.SqlConnection(SqlConex)
        'Traspaso Integrantes a IntegrantesTemp para trabajar temporalmente
        'Integrantes
        cn.Open()
        Dim sql4 As String
        sql4 = "INSERT INTO IntegrantesTemp (cuit, codigoRegistro, cuil) " &
                    "SELECT " & Session("CUIT") & ", NULL, cuil " &
                        "FROM Integrantes " &
                        "WHERE codigoRegistro = " & nCodigo & " and fechaBaja is null"
        Dim Psql4 As New SqlClient.SqlCommand(sql4, cn)
        Dim dr4 As SqlClient.SqlDataReader = Psql4.ExecuteReader
        cn.Close()
        dr4.Close()
    End Sub

    Private Sub CargarTemporalModificacion()
        Dim cn As New SqlClient.SqlConnection(SqlConex)
        'Traspaso Integrantes a IntegrantesTemp para trabajar temporalmente
        'Integrantes
        cn.Open()
        Dim sql5 As String
        sql5 = "DELETE FROM IntegrantesTemp WHERE cuit = " & Session("CUIT")
        Dim Psql5 As New SqlClient.SqlCommand(sql5, cn)
        Psql5.ExecuteNonQuery()
        cn.Close()

        cn.Open()
        Dim sql4 As String
        sql4 = "INSERT INTO IntegrantesTemp (cuit, codigoRegistro, cuil) " &
                    "SELECT " & Session("CUIT") & ", NULL, cuil " &
                        "FROM Integrantes " &
                        "WHERE codigoRegistro = " & Session("USER_ID")
        Dim Psql4 As New SqlClient.SqlCommand(sql4, cn)
        Dim dr4 As SqlClient.SqlDataReader = Psql4.ExecuteReader
        cn.Close()
        dr4.Close()
    End Sub

    Private Sub BtnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnGuardar.Click
        Dim sResult As String
        Dim bSaved As Boolean

        Try
            If Not ValidarDatos() Then
                Return
            End If

            If BtnGuardar.Text = "Confirmar Registro" Then
                bSaved = GuardarDatos()
            Else
                bSaved = ActualizarDatos()
            End If

            If Not bSaved Then
                txtErrorAcepto.Text = "Se produjo un error al guardar los datos, por favor intente mas tarde"
                Return
            End If

            'Enviar email
            Dim sIdRegistro As String = IIf(Session("CODIGO_REGISTRO") Is Nothing, Session("CODIGO"), Session("CODIGO_REGISTRO"))
            Dim sSector As String = RTrim(ddlSectores.SelectedItem.Text)
            Dim sProvincia As String = RTrim(ddlProvincias.SelectedItem.Text)
            Dim nTipoMail As Integer
            Dim sSubject As String
            Dim sBody As String

            cn.Close()
            Dim Apellido As String = ""
            Dim Nombre As String = ""
            Dim Denominacion As String = ""
            Dim Persona As Integer = 0
            Dim CUIT As Decimal = Session("CUIT")
            Dim sql As String = "select apellido,nombre,denominacion,persona from REGISDIG where CUIL=" & CUIT
            cn.Open()
            Dim Psql As New SqlClient.SqlCommand(sql, cn)
            Dim dr As SqlClient.SqlDataReader = Psql.ExecuteReader
            While dr.Read()
                Apellido = dr.GetString(0)
                Nombre = dr.GetString(1)
                Denominacion = dr.GetString(2)
                Persona = dr.GetInt32(3)
            End While
            dr.Close()
            cn.Close()

            Dim CodigoRegistro As Decimal = 0
            sql = "select REGISTRO from REGISTRO where CODIGO=" & sIdRegistro
            cn.Open()
            Dim Psqlr As New SqlClient.SqlCommand(sql, cn)
            Dim drr As SqlClient.SqlDataReader = Psqlr.ExecuteReader
            While drr.Read()
                Try
                    CodigoRegistro = drr.GetDecimal(0)
                Catch ex As Exception
                    CodigoRegistro = 0
                End Try
            End While
            drr.Close()
            cn.Close()

            If BtnGuardar.Text = "Confirmar Registro" Then
                nTipoMail = MAIL_ALTA_REGISTRO
                sSubject = "INTeatroDigital - Solicitud de Registro de Evento"
                sBody = "Estimada/o usuaria/o de INTeatroDigital:" & "<br />" & "<br />"
                sBody += "Se ha recepcionado su gestión de: REGISTRO DE EVENTO" & "<br />"
            Else
                nTipoMail = MAIL_MODIF_REGISTRO
                sSubject = "INTeatroDigital - Actualización de Registro de Evento"
                sBody = "Estimada/o usuaria/o de INTeatroDigital:" & "<br />" & "<br />"
                sBody += "Se ha recepcionado su gestión de: ACTUALIZACION DE REGISTRO DE EVENTO) " & "<br />"
                sBody += "REGISTRO INT N°: " & CodigoRegistro & "<br />"
            End If

            sBody += "Quedando ingresados los siguientes datos del Registro:" & "<br />"

            sBody += "Código de Ingreso (sólo para uso del INT): " & sIdRegistro & "<br />"
            sBody += "Tipo: " & sSector & "<br />"
            sBody += "Provincia: " & sProvincia & "<br />"
            If Persona = 1 Then
                sBody += "Responsable del Registro: " & Nombre & " " & Apellido & " - " & CUIT & "<br />"
            Else
                sBody += "Responsable del Registro: " & Denominacion & " - " & CUIT & "<br />"
            End If
            sBody += "Denominación: " & txtDenominacion.Text & "<br />"
            sBody += "Tipo de Evento: " & IIf(radioPeriodico.Checked, "Periódico", "Ocasional") & "<br />"
            If TextEdicion1.Value.Trim <> "" Then
                sBody += "Fecha desde: " & TextEdicion1.Value.Trim & "<br />"
            End If
            If TextHasta.Value.Trim <> "" Then
                sBody += "Fecha hasta: " & TextHasta.Value.Trim & "<br />"
            End If
            sBody += "<br />"

            sBody += "Lista de Integrantes" & "<br />"
            sBody += RegistroModulo.GetIntegrantes(sIdRegistro, True)
            sBody += "<br />"
            If BtnGuardar.Text = "Confirmar Registro" Then
                sBody += "Usted ha realizado el trámite de Registro de EVENTO en INTeatroDigital. Estamos " & "<br />"
            Else
                sBody += "Usted ha realizado el trámite de Actualización de Registro de EVENTO en INTeatroDigital. Estamos " & "<br />"
            End If
            sBody += "trabajando en el procesamiento de sus datos. Debe clickear en el link que figura al final de este  " & "<br />"
            sBody += "mensaje, con el fin de validar su identidad como usuario. Al hacerlo, se le abrirá en el navegador " & "<br />"
            sBody += "de internet, la plataforma de INTeatroDigital directamente en la sección 'Imprimir Constancias', " & "<br />"
            sBody += "desde la cual deberá emitir y descargar la constancia de registro y enviarla por correo electrónico " & "<br />"
            sBody += "a la Representación del INT correspondiente a su Provincia." & "<br />"
            If BtnGuardar.Text = "Confirmar Registro" Then
                sBody += "En ese mismo mail deberá adjuntar:" & "<br />"
                sBody += "- Si el responsable es una PERSONA HUMANA: copia de frente y dorso del DNI" & "<br />"
                sBody += "- Si el responsable es una ENTIDAD o una SOCIEDAD: copia de frente y dorso del DNI de una de las " & "<br />"
                sBody += "autoridades registradas, preferentemente la firmante." & "<br />"
            End If
            sBody += "<br />"
            sBody += "***Recuerde que para poder emitir y descargar su constancia, TODOS los integrantes de su proyecto " & "<br />"
            sBody += "tendrán que haber 'validado' previamente su vinculación al mismo***" & "<br />"
            sBody += "<br />"
            sBody += "Una vez recibida la documentación en la Sede Central del INT y procesado los datos, usted recibirá " & "<br />"
            sBody += "en esta dirección de correo electrónico la confirmación definitiva del trámite." & "<br />"
            sBody += "<br />"

            'sBody += Mail.GetTextoAviso(nTipoMail, sSector, sIdRegistro) & "<br />"
            'sBody += "<br />"

            sBody += Mail.GetLink(nTipoMail, sIdRegistro, Session("USER_ID")) & "<br />"
            sBody += "<br />"

            sBody += "Si este mensaje no lo visualiza en formato HTML, debe copiar el hipervínculo "
            sBody += "que ve mas arriba en su navegador de internet.<br />"
            sBody += "<br />"
            sBody += "Gracias.<br />"
            sBody += "<br />"
            sBody += "INTeatroDigital.<br />"

            sResult = SendMail(Mail.GetMailTo(Session("USER_ID"), TIPO_PERSONA), sSubject, sBody)

            'Integrantes
            Dim sResult2 As String = ""
            If BtnGuardar.Text = "Confirmar Registro" Then
                sSubject = "INTeatroDigital - Vinculación a Registro de Evento"
                sBody = "Estimado usuario de INTeatroDigital: " & "<br />"
                sBody += "Usted ha sido incorporado como integrante de " & RTrim(txtDenominacion.Text) & " en el Registro Nacional del Teatro Independiente. " & "<br />"
                sBody += "A partir de este momento, para poder 'validar' su vinculación a dicho Evento, deberá ingresar a la plataforma de " & "<br />"
                sBody += "INTeatroDigital, y en la sección 'Mis Vinculaciones' clickear en 'Confirmar Vinculación'. " & "<br />"
                sBody += "<br />"
            Else
                sSubject = "INTeatroDigital - Actualización de Registro Vinculado"
                sBody = "Estimado usuario de INTeatroDigital: " & "<br />"
                sBody += "Se ha procesado satisfactoriamente la solicitud de Actualización del Registro de Evento " & RTrim(txtDenominacion.Text) & "<br />"
                sBody += " Registro Nº " & CodigoRegistro & " al cual usted está vinculado. " & "<br />"
                sBody += "Con el objeto de 'validar' dicha actualización, deberá ingresar a la plataforma de " & "<br />"
                sBody += "INTeatroDigital, y en la sección 'Mis Vinculaciones' clickear en 'Confirmar Vinculación'. " & "<br />"
                sBody += "<br />"
            End If
            sBody += "***Tenga en cuenta que hasta que usted y todos los demás integrantes del proyecto no 'Validen' su " & "<br />"
            sBody += "vinculación al mismo, el responsable del Registro no podrá finalizar su tramitación de registro del " & "<br />"
            sBody += "espectáculo***." & "<br />"
            sBody += "<br />"
            sBody += "Gracias.<br />"
            sBody += "<br />"
            sBody += "INTeatroDigital.<br />"

            Dim sMail As String = ""
            sql = "select g.EMAIL from integrantes i, REGISDIG g where i.codigoRegistro=" & sIdRegistro & " and i.CUIL=g.CUIL and i.verificado is null "
            cn.Open()
            Dim Psqli As New SqlClient.SqlCommand(sql, cn)
            Dim dri As SqlClient.SqlDataReader = Psqli.ExecuteReader
            While dri.Read()
                sMail = dri.GetString(0)
                sResult2 = SendMail(sMail, sSubject, sBody)
            End While
            dri.Close()
            cn.Close()

            'A la representación provincial:
            Dim sResult3 As String = ""
            Dim wemailprov As String = ""
            cn.Open()
            sql = "select mail from provinciasmail where idprovincia=" & ddlProvincias.SelectedValue
            Dim cdmm As New SqlClient.SqlCommand(sql, cn)
            Dim drp As SqlClient.SqlDataReader = cdmm.ExecuteReader
            While drp.Read
                wemailprov = drp.GetString(0)
            End While
            drp.Close()
            cn.Close()

            If BtnGuardar.Text = "Confirmar Registro" Then
                sSubject = "INTeatroDigital - Solicitud de Registro de Evento"
                sBody = "REGISTRO de EVENTO: " & txtDenominacion.Text.Trim & "<br />"
            Else
                sSubject = "INTeatroDigital - Actualización de Registro de Evento"
                sBody = "ACTUALIZACION DE REGISTRO de EVENTO: " & txtDenominacion.Text.Trim & "<br />"
                sBody += "REGISTRO INT N°:  " & CodigoRegistro & "<br />"
            End If

            sBody += "Quedando ingresados los siguientes datos del Registro: " & "<br />"
            sBody += "Código de Ingreso (sólo para uso del INT): " & sIdRegistro & "<br />"
            sBody += "Tipo: " & sSector & "<br />"
            sBody += "Provincia: " & sProvincia & "<br />"
            If Persona = 1 Then
                sBody += "Responsable del Registro: " & Nombre & " " & Apellido & " - " & CUIT & "<br />"
            Else
                sBody += "Responsable del Registro: " & Denominacion & " - " & CUIT & "<br />"
            End If
            sBody += "Tipo de Evento: " & IIf(radioPeriodico.Checked, "Periódico", "Ocasional") & "<br />"
            If TextEdicion1.Value.Trim <> "" Then
                sBody += "Fecha desde: " & TextEdicion1.Value.Trim & "<br />"
            End If
            If TextHasta.Value.Trim <> "" Then
                sBody += "Fecha hasta: " & TextHasta.Value.Trim & "<br />"
            End If
            sResult3 = SendMail(wemailprov, sSubject, sBody)

            If BtnGuardar.Text = "Modificar" Then
                'Mail a los Integrantes borrados
                Dim sResult4 As String = ""
                Dim i As Integer
                Dim sIntegranteCUIT As String
                Dim sNombreRegistro As String
                Dim sNombreIntegrante As String
                If ds.Borrados.Count > 0 Then
                    For Each b As dsIntegrantes.BorradosRow In ds.Borrados
                        Try
                            'Mail al Integrante
                            sIntegranteCUIT = b.cuil    ' ViewState("DELETED_CUIL")(i).ToString
                            sNombreRegistro = GetNombreRegistro(sIdRegistro)
                            sNombreIntegrante = GetNombreRegisDig(sIntegranteCUIT)
                            sBody = "Estimado usuario de INTeatroDigital:" & "<br />"
                            sBody += "Usted ha sido desvinculado como integrante de " & sNombreRegistro & " en el Registro Nacional del Teatro Independiente. " & "<br />"
                            sBody += "A partir de este momento deja de tener vinculación con el Registro N° " & CodigoRegistro & "." & "<br />"
                            sBody += "<br />"
                            sBody += "Gracias.<br />"
                            sBody += "<br />"
                            sBody += "INTeatroDigital.<br />"

                            sResult4 = SendMail(GetMail(sIntegranteCUIT), "INTeatroDigital - Desvinculación como Integrante", sBody)

                            ''Mail al Responsable
                            'sBody = "Estimado usuario de INTeatroDigital:" & "<br />"
                            'sBody += "Se ha procesado satisfactoriamente la desvinculación de  " & sNombreIntegrante & " - " & sIntegranteCUIT & " de su Registro Nº " & CodigoRegistro & " - " & sNombreRegistro & "<br />"
                            'sBody += "Debe clickear en el link que figura al final de este mensaje; al hacerlo, se le abrirá en el navegador de internet, la plataforma de INTeatroDigital, " & "<br />"
                            'sBody += "en la cual deberá iniciar sesión y posteriormente clickear en la sección 'Imprimir Constancias', desde la cual deberá emitir y  " & "<br />"
                            'sBody += "descargar la constancia de registro y enviarla por correo electrónico a la Representación del INT correspondiente a su Provincia." & "<br />"
                            'sBody += "<br />"
                            'sBody += "Una vez recibida la documentación en la Sede Central del INT y procesado los datos, usted recibirá " & "<br />"
                            'sBody += "en esta dirección de correo electrónico la confirmación definitiva del trámite de desvinculación de integrante." & "<br />"
                            'sBody += " < br /> "
                            'sResult = SendMail(CUIT, "INTeatroDigital - Desvinculación de un Integrante", sBody)
                            'sBody += "<br />"
                            'sBody += Mail.GetLink(nTipoMail, sIdRegistro, Session("USER_ID")) & "<br />"
                            'sBody += "<br />"

                            'sBody += "Si este mensaje no lo visualiza en formato HTML, debe copiar el hipervínculo "
                            'sBody += "que ve mas arriba en su navegador de internet.<br />"
                            'sBody += "<br />"
                            'sBody += "Gracias.<br />"
                            'sBody += "<br />"
                            'sBody += "INTeatroDigital.<br />"

                            'sResult4 = SendMail(GetMail(CUIT), "INTeatroDigital - Desvinculación de un Integrante", sBody)

                            'Mail a la provincia
                            sBody = "REGISTRO de:" & ddlSectores.SelectedItem.Text & " - " & RTrim(sNombreRegistro) & "<br />"
                            sBody += "N° DE REGISTRO:" & CodigoRegistro & "<br />"
                            sBody += "Se ha confirmado la desvinculación al mencionado registro de " & sNombreIntegrante & " - " & sIntegranteCUIT

                            sResult4 = SendMail(wemailprov, "INTeatroDigital - Desvinculación de un Integrante", sBody)

                        Catch ex As Exception
                            Throw ex
                        End Try
                    Next
                End If
            End If

            If sResult = "OK" Then
                Response.Redirect("confirmaRegistro.aspx?r=ok&m=" & sResult)
            Else
                Response.Redirect("confirmaRegistro.aspx?r=er&m=" & sResult)
            End If
            'End of Enviar email

        Catch ex As Exception

        End Try
    End Sub

    Protected Function ValidarDatos()

        Dim dFecha As Date

        'Limpio los errores
        txtErrorLocalidades.Text = ""
        txtErrorTipoEvento.Text = ""
        txtErrorAcepto.Text = ""
        txtErrorFechaDesde.Text = ""
        txtErrorFechaHasta.Text = ""

        If Funciones.CaracteresEspecialesnumeros(txtDenominacion.Text.Trim) Then
            txtErrorAcepto.Text = "La denominación contiene caracteres especiales"
            txtDenominacion.Focus()
            Return False
        End If

        Try
            dFecha = CDate(TextEdicion1.Value)
        Catch ex As Exception
            txtErrorFechaDesde.Text = "Fecha inválida"
            TextEdicion1.Focus()
            Return False
        End Try

        Try
            If Not ValidarFecha(TextEdicion1.Value.Trim) Then
                txtErrorFechaDesde.Text = "Fecha inválida"
                TextEdicion1.Focus()
                Return False
            End If
        Catch ex As Exception
            txtErrorFechaDesde.Text = "Fecha inválida"
            TextEdicion1.Focus()
            Return False
        End Try

        Try
            dFecha = CDate(TextHasta.Value)
        Catch ex As Exception
            txtErrorFechaHasta.Text = "Fecha inválida"
            TextHasta.Focus()
            Return False
        End Try

        Try
            If Not ValidarFecha(TextHasta.Value.Trim) Then
                txtErrorFechaHasta.Text = "Fecha inválida"
                TextHasta.Focus()
                Return False
            End If
        Catch ex As Exception
            txtErrorFechaHasta.Text = "Fecha inválida"
            TextHasta.Focus()
            Return False
        End Try

        Try
            If Not ValidarFecha(TextDesde.Value.Trim) Then
                txtErrorFechaInicio.Text = "Fecha inválida"
                TextDesde.Focus()
                Return False
            End If
        Catch ex As Exception
            txtErrorFechaInicio.Text = "Fecha inválida"
            TextDesde.Focus()
            Return False
        End Try

        Try
            dFecha = CDate(TextDesde.Value)
        Catch ex As Exception
            txtErrorFechaInicio.Text = "Fecha inválida"
            TextDesde.Focus()
            Return False
        End Try

        If ddlLocalidades.SelectedValue = 0 Then
            txtErrorLocalidades.Text = "Debe seleccionar una localidad"
            ddlLocalidades.Focus()
            Return False
        End If

        If radioPeriodico.Checked = True And
            (TextEdicion1.Value = "" Or TextHasta.Value = "") Then
            txtErrorTipoEvento.Text = "Debe ingresar las fechas desde / hasta"
            TextEdicion1.Focus()
            Return False
        End If

        If DdlPublico.SelectedValue = 0 Then
            LblErrorDdlPublico.Text = "Seleccione Tipo de Público"
            DdlPublico.Focus()
            Return False
        End If

        If Ddlgestion.SelectedValue = 0 Then
            LblErrorDdlGestion.Text = "Seleccione tipo de Gestión"
            Ddlgestion.Focus()
            Return False
        End If

        If chkAcepto.Checked = False Then
            txtErrorAcepto.Text = "Debe aceptar los términos para continuar"
            chkAcepto.Focus()
            Return False
        End If

        If checkAutorizoPublicar.Checked = False Then
            lblErrorcheckAutorizoPublicar.Text = "Debe aceptar los términos para continuar"
            checkAutorizoPublicar.Focus()
            Return False
        End If

        txtDenominacion.Text = LimpiarCaracteres(txtDenominacion.Text.Trim)

        Return True

    End Function

    Protected Function GuardarDatos() As Integer
        Dim MyConnection As SqlConnection
        Dim MyCommand As SqlCommand
        Dim sSQLCmd As String
        Dim mIdRegistro As SqlParameter
        Dim nIdRegistro As Integer
        Dim sTipoEvento As String
        Dim sFechaDesde As String
        Dim sFechaHasta As String
        Dim sFechainicio As String
        Try
            If radioPeriodico.Checked Then
                sTipoEvento = "1"
            Else
                sTipoEvento = "2"
            End If
            Dim wfecha As Date = CDate(TextEdicion1.Value)
            sFechaDesde = Year(wfecha) * 10000 + Month(wfecha) * 100 + Day(wfecha)
            wfecha = CDate(TextHasta.Value)
            sFechaHasta = Year(wfecha) * 10000 + Month(wfecha) * 100 + Day(wfecha)
            wfecha = CDate(TextDesde.Value)
            sFechainicio = Year(wfecha) * 10000 + Month(wfecha) * 100 + Day(wfecha)

            'INSERT Registro
            sSQLCmd = "INSERT INTO Registro " &
                            "(responsable, sector, provincia, " &
                            "denominacion, localidad, email, " &
                            "pagina, tipoeven, " &
                            "desde, hasta, fechAlta, inicio) " &
                        "VALUES " &
                            "(" & Session("USER_ID") & ", " & Session("SECTOR") & ", " & ddlProvincias.Text & ", " &
                            "'" & txtDenominacion.Text.Trim.ToUpper & "', " & ddlLocalidades.Text & ", '" & txtMail.Text.Trim & "', " &
                            "'" & txtWeb.Text.Trim & "', " & sTipoEvento & ", "

            If radioPeriodico.Checked Then
                sSQLCmd += "Convert(datetime,'" & sFechaDesde & "'), Convert(datetime,'" & sFechaHasta & "'), getdate(), Convert(datetime,'" & sFechainicio & "')) "
            Else
                sSQLCmd += "NULL, NULL, getdate(), Convert(datetime,'" & sFechainicio & "')) "
            End If
            sSQLCmd += "SET @nIdRegistro = SCOPE_IDENTITY()"
            mIdRegistro = New SqlParameter
            mIdRegistro.ParameterName = "@nIdRegistro"
            mIdRegistro.SqlDbType = SqlDbType.Int
            mIdRegistro.Direction = ParameterDirection.Output
            mIdRegistro.Value = -1
            MyConnection = New SqlConnection()
            MyConnection.ConnectionString = ConfigurationManager.ConnectionStrings("INTeatroDig").ConnectionString
            MyConnection.Open()
            MyCommand = New SqlCommand()
            MyCommand.CommandText = sSQLCmd
            MyCommand.CommandType = CommandType.Text
            MyCommand.Connection = MyConnection
            MyCommand.Parameters.Add(mIdRegistro)
            MyCommand.ExecuteNonQuery()
            nIdRegistro = mIdRegistro.Value
            MyCommand.Dispose()
            MyConnection.Dispose()

            'REGISTROEVENTOS
            Dim nPublico As Integer = DdlPublico.SelectedValue
            Dim nActividad As Integer = ddlActividad.SelectedValue
            Dim sDesactiv As String = TextBoxDActividad.Text
            Dim nGestion As Integer = Ddlgestion.SelectedValue
            Dim nM1 As Integer = 0
            Dim nM2 As Integer = 0
            Dim nM3 As Integer = 0
            Dim nM4 As Integer = 0
            Dim nM5 As Integer = 0
            Dim nM6 As Integer = 0
            Dim nM7 As Integer = 0
            Dim nM8 As Integer = 0
            Dim nM9 As Integer = 0
            Dim nM10 As Integer = 0
            Dim nM11 As Integer = 0
            Dim nM12 As Integer = 0
            If ChkEnero.Checked = True Then
                nM1 = 1
            End If
            If ChkFebrero.Checked = True Then
                nM2 = 1
            End If
            If ChkMarzo.Checked = True Then
                nM3 = 1
            End If
            If ChkAbril.Checked = True Then
                nM4 = 1
            End If
            If ChkMayo.Checked = True Then
                nM5 = 1
            End If
            If ChkJunio.Checked = True Then
                nM6 = 1
            End If
            If ChkJulio.Checked = True Then
                nM7 = 1
            End If
            If ChkAgosto.Checked = True Then
                nM8 = 1
            End If
            If ChkSetiembre.Checked = True Then
                nM9 = 1
            End If
            If ChkOctubre.Checked = True Then
                nM10 = 1
            End If
            If ChkNoviembre.Checked = True Then
                nM11 = 1
            End If
            If ChkDiciembre.Checked = True Then
                nM12 = 1
            End If
            Dim sqle As String = "insert into REGISTROEVENTOS (CODIGOREGISTRO,PUBLICO,ACTIVIDAD,DESCACTIV,GESTION,M1,M2,M3,M4,M5,M6,M7,M8,M9,M10,M11,M12,FECHALTA)" &
                                " values (" & nIdRegistro & "," & nPublico & "," & nActividad & ",'" & sDesactiv & "'," & nGestion & "," & nM1 & "," &
                                 nM2 & "," & nM3 & "," & nM4 & "," & nM5 & "," & nM6 & "," & nM7 & "," & nM8 & "," &
                                 nM9 & "," & nM10 & "," & nM11 & "," & nM12 & ",getdate())"
            Dim Cmde As New SqlClient.SqlCommand(sqle, cn)
            cn.Open()
            Try
                Cmde.ExecuteNonQuery()
            Catch ex As Exception
                txtErrorAcepto.Text = "Error al ingresar Datos"
                Return False
            End Try
            cn.Close()

            dsInte.AceptaCambios(ds, nIdRegistro)

            'INSERT Palabras
            'sSQLCmd = "INSERT INTO RegistroPalabras " & _
            '                "(codigoRegistro, " & _
            '                "APELLIDO_Y_NOMBRE_DEL_RESPONSABLE, " & _
            '                "DIRECCION_DE_CORREO_ELECTRONICO_DEL_EVENTO, " & _
            '                "DIRECCION_DE_CORREO_ELECTRONICO_DEL_RESPONSABLE, " & _
            '                "PAGINA_WEB_DEL_EVENTO, " & _
            '                "TELEFONOS_DEL_RESPONSABLE, " & _
            '                "TIPO_DE_EVENTO) " & _
            '            "VALUES " & _
            '                "(" & nIdRegistro & ", " & _
            '                " " & IIf(chkPalabra1.Checked, 1, 0) & ", " & _
            '                " " & IIf(chkPalabra13.Checked, 1, 0) & ", " & _
            '                " " & IIf(chkPalabra15.Checked, 1, 0) & ", " & _
            '                " " & IIf(chkPalabra21.Checked, 1, 0) & ", " & _
            '                " " & IIf(chkPalabra27.Checked, 1, 0) & ", " & _
            '                " " & IIf(chkPalabra28.Checked, 1, 0) & _
            '                ")"
            'MyConnection = New SqlConnection()
            'MyConnection.ConnectionString = ConfigurationManager.ConnectionStrings("INTeatroDig").ConnectionString
            'MyConnection.Open()
            'MyCommand = New SqlCommand()
            'MyCommand.CommandText = sSQLCmd
            'MyCommand.CommandType = CommandType.Text
            'MyCommand.Connection = MyConnection
            'MyCommand.ExecuteNonQuery()
            'MyCommand.Dispose()
            'MyConnection.Dispose()
            'End of INSERT Palabras

            Session.Add("CODIGO_REGISTRO", nIdRegistro)

        Catch ex As Exception
            GuardarDatos = False
            'Response.Redirect("ErrorPage.aspx?errMessage=" & ex.Message & "&errSource=" & ex.Source)
        Finally
            MyCommand.Dispose()
            MyConnection.Dispose()
        End Try

        GuardarDatos = True
    End Function

    Protected Function ActualizarDatos() As Integer
        Dim MyConnection As SqlConnection
        Dim MyCommand As SqlCommand
        Dim sSQLCmd As String
        Dim sTipoEvento As String
        Dim sFechaDesde As String
        Dim sFechaHasta As String
        Dim sFechainicio As String
        Try
            If radioPeriodico.Checked Then
                sTipoEvento = "1"
            Else
                sTipoEvento = "2"
            End If
            Dim wfecha As Date = CDate(TextEdicion1.Value)
            sFechaDesde = Year(wfecha) * 10000 + Month(wfecha) * 100 + Day(wfecha)
            wfecha = CDate(TextHasta.Value)
            sFechaHasta = Year(wfecha) * 10000 + Month(wfecha) * 100 + Day(wfecha)
            wfecha = CDate(TextDesde.Value)
            sFechainicio = Year(wfecha) * 10000 + Month(wfecha) * 100 + Day(wfecha)
            'UPDATE Registro
            sSQLCmd = "UPDATE Registro " &
                           "SET RESPONSABLE = " & Session("USER_ID") & ",  " &
                              "SECTOR = " & Session("SECTOR") & ",  " &
                              "DENOMINACION = '" & txtDenominacion.Text.Trim.ToUpper & "', " &
                              "LOCALIDAD = " & ddlLocalidades.Text & ", " &
                              "Provincia = " & ddlProvincias.SelectedValue & ", " &
                              "EMAIL = '" & txtMail.Text.Trim & "', " &
                              "PAGINA = '" & txtWeb.Text.Trim & "', "

            If radioPeriodico.Checked Then
                sSQLCmd += "DESDE = Convert(datetime,'" & sFechaDesde & "'), " &
                            "HASTA = Convert(datetime,'" & sFechaHasta & "'), "
            Else
                sSQLCmd += "DESDE = NULL, " &
                            "HASTA = NULL, "
            End If
            sSQLCmd += "TIPOEVEN = '" & sTipoEvento & "', " &
                       "INICIO = Convert(datetime,'" & sFechainicio & "') " &
                         "WHERE codigo = " & Session("CODIGO")
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

            Dim nIdRegistro As Integer = Session("CODIGO")

            'REGISTROEVENTOS
            Dim nPublico As Integer = DdlPublico.SelectedValue
            Dim nActividad As Integer = ddlActividad.SelectedValue
            Dim sDesactiv As String = TextBoxDActividad.Text
            Dim nGestion As Integer = Ddlgestion.SelectedValue
            Dim nM1 As Integer = 0
            Dim nM2 As Integer = 0
            Dim nM3 As Integer = 0
            Dim nM4 As Integer = 0
            Dim nM5 As Integer = 0
            Dim nM6 As Integer = 0
            Dim nM7 As Integer = 0
            Dim nM8 As Integer = 0
            Dim nM9 As Integer = 0
            Dim nM10 As Integer = 0
            Dim nM11 As Integer = 0
            Dim nM12 As Integer = 0
            If ChkEnero.Checked = True Then
                nM1 = 1
            End If
            If ChkFebrero.Checked = True Then
                nM2 = 1
            End If
            If ChkMarzo.Checked = True Then
                nM3 = 1
            End If
            If ChkAbril.Checked = True Then
                nM4 = 1
            End If
            If ChkMayo.Checked = True Then
                nM5 = 1
            End If
            If ChkJunio.Checked = True Then
                nM6 = 1
            End If
            If ChkJulio.Checked = True Then
                nM7 = 1
            End If
            If ChkAgosto.Checked = True Then
                nM8 = 1
            End If
            If ChkSetiembre.Checked = True Then
                nM9 = 1
            End If
            If ChkOctubre.Checked = True Then
                nM10 = 1
            End If
            If ChkNoviembre.Checked = True Then
                nM11 = 1
            End If
            If ChkDiciembre.Checked = True Then
                nM12 = 1
            End If
            Dim sqle As String = "update REGISTROEVENTOS set PUBLICO=" & nPublico & ",ACTIVIDAD=" & nActividad & ",DESCACTIV='" & sDesactiv & "',GESTION=" & nGestion &
                                 ",M1=" & nM1 & ",M2=" & nM2 & ",M3=" & nM3 & ",M4=" & nM4 & ",M5=" & nM5 & ",M6=" & nM6 & ",M7=" & nM7 & ",M8=" & nM8 & ",M9=" & nM9 &
                                 ",M10=" & nM10 & ",M11=" & nM11 & ",M12=" & nM12 & ",FECHALTA=getdate() where CODIGOREGISTRO=" & nIdRegistro
            Dim Cmde As New SqlClient.SqlCommand(sqle, cn)
            cn.Open()
            Try
                Cmde.ExecuteNonQuery()
            Catch ex As Exception
                txtErrorAcepto.Text = "Error al ingresar Datos"
                Return False
            End Try
            cn.Close()


            'DELETE Integrantes
            dsInte.AceptaCambios(ds, Session("CODIGO"))

            ''UPDATE Palabras
            'sSQLCmd = "UPDATE RegistroPalabras " & _
            '               "Set APELLIDO_Y_NOMBRE_DEL_RESPONSABLE = " & IIf(chkPalabra1.Checked, 1, 0) & ",  " & _
            '                  "DIRECCION_DE_CORREO_ELECTRONICO_DEL_EVENTO = " & IIf(chkPalabra13.Checked, 1, 0) & ",  " & _
            '                  "DIRECCION_DE_CORREO_ELECTRONICO_DEL_RESPONSABLE = " & IIf(chkPalabra15.Checked, 1, 0) & ",  " & _
            '                  "PAGINA_WEB_DEL_EVENTO = " & IIf(chkPalabra21.Checked, 1, 0) & ",  " & _
            '                  "TELEFONOS_DEL_RESPONSABLE = " & IIf(chkPalabra27.Checked, 1, 0) & ",  " & _
            '                  "TIPO_DE_EVENTO = " & IIf(chkPalabra28.Checked, 1, 0) & " " & _
            '             "WHERE codigoRegistro = " & Session("CODIGO")
            'MyConnection = New SqlConnection()
            'MyConnection.ConnectionString = ConfigurationManager.ConnectionStrings("INTeatroDig").ConnectionString
            'MyConnection.Open()
            'MyCommand = New SqlCommand()
            'MyCommand.CommandText = sSQLCmd
            'MyCommand.CommandType = CommandType.Text
            'MyCommand.Connection = MyConnection
            'MyCommand.ExecuteNonQuery()
            'MyCommand.Dispose()
            'MyConnection.Dispose()
            'End of UPDATE Palabras

            Dim Sql As String = "update integrantes Set verificado = null where codigoregistro=" & Session("CODIGO")
            Dim cmd As New SqlClient.SqlCommand(Sql, cn)
            cn.Open()
            cmd.ExecuteNonQuery()
            cn.Close()

        Catch ex As Exception
            ActualizarDatos = False
            'Response.Redirect("ErrorPage.aspx?errMessage=" & ex.Message & "&errSource=" & ex.Source)
        Finally
            MyCommand.Dispose()
            MyConnection.Dispose()
        End Try

        ActualizarDatos = True
    End Function

    Private Sub CargarDatos(ByVal nCodigo As Integer)
        'Const F_RESPONSABLE As Integer = 0
        Const F_SECTOR As Integer = 1
        Const F_PROVINCIA As Integer = 2
        Const F_DENOMINACION As Integer = 3
        Const F_LOCALIDAD As Integer = 4
        Const F_EMAIL As Integer = 5
        Const F_PAGINA As Integer = 6
        Const F_TIPO_EVENTO As Integer = 7
        Const F_FECHA_DESDE As Integer = 8
        Const F_FECHA_HASTA As Integer = 9
        'Const F_FECHALTA As Integer = 10
        Dim sSQLCmd As String
        Dim MyConnection As SqlConnection
        Dim MyCommand As SqlCommand
        Dim MyReader As SqlDataReader

        Try
            'Load Registro
            sSQLCmd = "Select responsable, sector, provincia, denominacion, " &
                                "localidad, email, pagina, tipoeven, desde, hasta, fechAlta " &
                            "FROM Registro " &
                            "WHERE codigo = " & nCodigo.ToString

            MyConnection = New SqlConnection()
            MyConnection.ConnectionString = ConfigurationManager.ConnectionStrings("INTeatroDig").ConnectionString
            MyConnection.Open()
            MyCommand = New SqlCommand(sSQLCmd, MyConnection)
            MyReader = MyCommand.ExecuteReader()
            If MyReader.Read() Then
                Session.Add("SECTOR", MyReader.Item(F_SECTOR))
                Session.Add("PROVINCIA", MyReader.Item(F_PROVINCIA))
                ddlSectores.Text = MyReader.Item(F_SECTOR).ToString.Trim
                ddlProvincias.Text = MyReader.Item(F_PROVINCIA)
                txtDenominacion.Text = MyReader.Item(F_DENOMINACION).ToString.Trim
                GetLocal(MyReader.Item(F_PROVINCIA))
                ddlLocalidades.Text = MyReader.Item(F_LOCALIDAD)
                txtMail.Text = MyReader.Item(F_EMAIL).ToString.Trim
                txtWeb.Text = MyReader.Item(F_PAGINA).ToString.Trim
                If MyReader.Item(F_TIPO_EVENTO) = 1 Then
                    radioPeriodico.Checked = True
                    radioOcasional.Checked = False
                Else
                    radioPeriodico.Checked = False
                    radioOcasional.Checked = True
                End If
                If MyReader.Item(F_FECHA_DESDE).Equals(DBNull.Value) Then
                    TextEdicion1.Value = ""
                Else
                    TextEdicion1.Value = MyReader.Item(F_FECHA_DESDE)
                End If
                If MyReader.Item(F_FECHA_HASTA).Equals(DBNull.Value) Then
                    TextHasta.Value = ""
                Else
                    TextHasta.Value = MyReader.Item(F_FECHA_HASTA)
                End If
            End If
            MyCommand.Dispose()
            MyConnection.Dispose()

            'REGISTROEVENTOS
            Dim nPublico As Integer = 0
            Dim nActividad As Integer = 0
            Dim sDesactiv As String = ""
            Dim nGestion As Integer = 0
            Dim nM1 As Integer = 0
            Dim nM2 As Integer = 0
            Dim nM3 As Integer = 0
            Dim nM4 As Integer = 0
            Dim nM5 As Integer = 0
            Dim nM6 As Integer = 0
            Dim nM7 As Integer = 0
            Dim nM8 As Integer = 0
            Dim nM9 As Integer = 0
            Dim nM10 As Integer = 0
            Dim nM11 As Integer = 0
            Dim nM12 As Integer = 0
            Dim sqle As String = "select PUBLICO,ACTIVIDAD,DESCACTIV,GESTION,M1,M2,M3,M4,M5,M6,M7,M8,M9,M10,M11,M12 from REGISTROEVENTOS where CODIGOREGISTRO=" & nCodigo.ToString
            cn.Open()
            Dim Psqle As New SqlClient.SqlCommand(sqle, cn)
            Dim dre As SqlClient.SqlDataReader = Psqle.ExecuteReader
            While dre.Read()
                nPublico = dre.GetInt32(0)
                nActividad = dre.GetInt32(1)
                sDesactiv = dre.GetString(2)
                nGestion = dre.GetInt32(3)
                nM1 = dre.GetInt32(4)
                nM2 = dre.GetInt32(5)
                nM3 = dre.GetInt32(6)
                nM4 = dre.GetInt32(7)
                nM5 = dre.GetInt32(8)
                nM6 = dre.GetInt32(9)
                nM7 = dre.GetInt32(10)
                nM8 = dre.GetInt32(11)
                nM9 = dre.GetInt32(12)
                nM10 = dre.GetInt32(13)
                nM11 = dre.GetInt32(14)
                nM12 = dre.GetInt32(15)
            End While
            dre.Close()
            cn.Close()
            DdlPublico.SelectedValue = nPublico
            ddlActividad.SelectedValue = nActividad
            TextBoxDActividad.Text = sDesactiv
            Ddlgestion.SelectedValue = nGestion
            If nM1 = 1 Then
                ChkEnero.Checked = True
            End If
            If nM2 = 1 Then
                ChkFebrero.Checked = True
            End If
            If nM3 = 1 Then
                ChkMarzo.Checked = True
            End If
            If nM4 = 1 Then
                ChkAbril.Checked = True
            End If
            If nM5 = 1 Then
                ChkMayo.Checked = True
            End If
            If nM6 = 1 Then
                ChkJunio.Checked = True
            End If
            If nM7 = 1 Then
                ChkJulio.Checked = True
            End If
            If nM8 = 1 Then
                ChkAgosto.Checked = True
            End If
            If nM9 = 1 Then
                ChkSetiembre.Checked = True
            End If
            If nM10 = 1 Then
                ChkOctubre.Checked = True
            End If
            If nM11 = 1 Then
                ChkNoviembre.Checked = True
            End If
            If nM12 = 1 Then
                ChkDiciembre.Checked = True
            End If

            'Cargo Integrantes
            BorrarTemporal()
            CargarTemporal(nCodigo)


            'Load Palabras
            'sSQLCmd = "Select * " & _
            '                "FROM RegistroPalabras " & _
            '                "WHERE codigoRegistro = " & nCodigo.ToString
            'MyConnection = New SqlConnection()
            'MyConnection.ConnectionString = ConfigurationManager.ConnectionStrings("INTeatroDig").ConnectionString
            'MyConnection.Open()
            'MyCommand = New SqlCommand(sSQLCmd, MyConnection)
            'MyReader = MyCommand.ExecuteReader()
            'If MyReader.Read() Then
            '    If MyReader.Item(1) = 1 Then
            '        chkPalabra1.Checked = True
            '    Else
            '        chkPalabra1.Checked = False
            '    End If
            '    If MyReader.Item(13) = 1 Then
            '        chkPalabra13.Checked = True
            '    Else
            '        chkPalabra13.Checked = False
            '    End If
            '    If MyReader.Item(15) = 1 Then
            '        chkPalabra15.Checked = True
            '    Else
            '        chkPalabra15.Checked = False
            '    End If
            '    If MyReader.Item(21) = 1 Then
            '        chkPalabra21.Checked = True
            '    Else
            '        chkPalabra21.Checked = False
            '    End If
            '    If MyReader.Item(27) = 1 Then
            '        chkPalabra27.Checked = True
            '    Else
            '        chkPalabra27.Checked = False
            '    End If
            '    If MyReader.Item(28) = 1 Then
            '        chkPalabra28.Checked = True
            '    Else
            '        chkPalabra28.Checked = False
            '    End If
            'End If
            'MyReader.Dispose()
            'MyCommand.Dispose()
            'End of Load Palabras

            'Carga las combos
            Inicializar()


            MyConnection.Dispose()

        Catch ex As Exception

        End Try

    End Sub

    Private Sub btnIntegrante_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnIntegrante.Click
        Dim sResultado As String

        Try
            If txtIntegrante.Text.Trim <> "" Then
                sResultado = ValidarIntegrante(txtIntegrante.Text.Trim)
                If sResultado = "" Then
                    dsInte.AgregaIntegrantes(ds, Convert.ToInt32(Session("codigo")), txtIntegrante.Text)
                    txtIntegrante.Text = ""
                    GridView1.DataSource = ds.Integrantes
                    GridView1.DataBind()
                    GridView1.Visible = True
                    GridView1.Focus()
                Else
                    txtErrorIntegrante.Text = sResultado
                End If
            Else
                txtErrorIntegrante.Text = "Debe ingresar un CUIL"
            End If
        Catch ex As Exception

        End Try

    End Sub

    Protected Function ValidarIntegrante(ByVal sCUIT As String) As String
        Dim MyConnection As SqlConnection
        Dim MyCommand As SqlCommand
        Dim sSQLCmd As String
        Dim nCantidad As Integer
        Dim sResultado As String

        sResultado = ""
        txtErrorIntegrante.Text = ""

        If sCUIT = Session("CUIT") Then
            Return "Por ser el Responsable ya es un Integrante"
            Return False
        End If

        'Chequeo que exista en RegisDig
        sSQLCmd = "Select count(*) As cantidad " &
                        "FROM Regisdig " &
                        "WHERE CUIL = " & sCUIT & " And fechBaja Is NULL"

        MyConnection = New SqlConnection()
        MyConnection.ConnectionString = ConfigurationManager.ConnectionStrings("INTeatroDig").ConnectionString

        MyCommand = New SqlCommand()
        MyCommand.CommandText = sSQLCmd
        MyCommand.CommandType = CommandType.Text
        MyCommand.Connection = MyConnection
        MyCommand.Connection.Open()
        nCantidad = Convert.ToInt32(MyCommand.ExecuteScalar())

        If nCantidad = 0 Then
            Return "El integrante no existe"

        End If

        MyCommand.Dispose()
        MyConnection.Dispose()

        'Chequeo que no esté dado de alta
        sSQLCmd = "Select count(*) As cantidad " &
                        "FROM IntegrantesTemp " &
                        "WHERE cuit = " & Session("CUIT") & " And CUIL = " & sCUIT

        MyConnection = New SqlConnection()
        MyConnection.ConnectionString = ConfigurationManager.ConnectionStrings("INTeatroDig").ConnectionString

        MyCommand = New SqlCommand()
        MyCommand.CommandText = sSQLCmd
        MyCommand.CommandType = CommandType.Text
        MyCommand.Connection = MyConnection
        MyCommand.Connection.Open()
        nCantidad = Convert.ToInt32(MyCommand.ExecuteScalar())

        If nCantidad <> 0 Then
            Return "El integrante ya fue agregado"
        End If

        'Valido inhibición
        If Validaciones.EstaInhibido(sCUIT) Then
            Return "El titular del CUIL/CUIT que intenta ingresar se encuentra actualmente ""inhabilitado"" por este I.N.T."
        End If

        MyCommand.Dispose()
        MyConnection.Dispose()

        Return ""

    End Function

    Protected Function InsertIntegranteTemp() As Boolean
        Dim MyConnection As SqlConnection
        Dim MyCommand As SqlCommand
        Dim sSQLCmd As String
        Dim mIdIntegrante As SqlParameter
        Dim nIdIntegrante As Integer

        Try
            'INSERT Integrante
            sSQLCmd = "INSERT INTO IntegrantesTemp " &
                            "(cuit, codigoRegistro, cuil) " &
                            "VALUES " &
                            "(" & Session("CUIT") & ", NULL, '" & txtIntegrante.Text.Trim.ToUpper & "') " &
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
            InsertIntegranteTemp = False
            'Response.Redirect("ErrorPage.aspx?errMessage=" & ex.Message & "&errSource=" & ex.Source)
        Finally
            MyCommand.Dispose()
            MyConnection.Dispose()
        End Try

        InsertIntegranteTemp = True
    End Function

    Private Sub btnCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        Dim sAccion As String = Session("sAccion")
        If sAccion <> "M" Then
            Response.Redirect("menuFinal.aspx")
        Else
            Response.Redirect("RegistroLista.aspx")
        End If
    End Sub

    Private Sub GridView1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.Load

    End Sub

    Private Sub GridView1_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles GridView1.RowDeleting
        Dim id As String

        id = GridView1.DataKeys(e.RowIndex).Value
        dsInte.DesvinculaIntegrante(ds, Convert.ToInt32(Session("codigo")), id)
        GridView1.DataBind()
    End Sub

    Protected Sub ddlProvincias_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlProvincias.SelectedIndexChanged
        Dim cn As New SqlClient.SqlConnection(SqlConex)
        Dim prov As String = ddlProvincias.SelectedValue.ToString
        cn.Open()
        Dim sql6 As String = "select 0 as codloc,' Seleccione Localidad' as nomloc union select codloc,nomloc from localidades where provincia= " & prov & "order by nomloc"
        Dim Psql6 As New SqlClient.SqlCommand(sql6, cn)
        Dim dr6 As SqlClient.SqlDataReader = Psql6.ExecuteReader
        ddlLocalidades.DataSource = dr6
        ddlLocalidades.DataTextField = "nomloc"
        ddlLocalidades.DataValueField = "codloc"
        ddlLocalidades.DataBind()
        cn.Close()
    End Sub

    Protected Sub AceptoDJ_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles AceptoDJ.CheckedChanged
        tablaDatos.Visible = AceptoDJ.Checked

    End Sub
End Class
