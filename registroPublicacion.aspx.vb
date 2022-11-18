Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Collections

Partial Public Class registroPublicacion
    Inherits System.Web.UI.Page
    Dim cn As New SqlClient.SqlConnection(SqlConex)
    Dim aDeletedCUIL As New ArrayList
    Dim quien As usuario
    Shared ds As dsIntegrantes

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim sAccion As String
        Dim nCodigo As Integer
        SetearVariablesSession()
        If Not ds Is Nothing Then
            GridView1.DataSource = ds.Integrantes
            GridView1.DataBind()
            GridView1.Visible = True
        End If
        If Not Page.IsPostBack Then
            'La primera vez
            If User.Identity.IsAuthenticated Then
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
        Session("SECTOR") = 7
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
        dr.Close()

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
        dr2.Close()

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

            Dim nTipoMail As Integer
            Dim sSubject As String
            Dim sBody As String

            If BtnGuardar.Text = "Confirmar Registro" Then
                nTipoMail = MAIL_ALTA_REGISTRO
                sSubject = "INTeatroDigital - Solicitud de Registro de Publicación"
                sBody = "Estimada/o usuaria/o de INTeatroDigital:" & "<br />" & "<br />"
                sBody += "Se ha recepcionado su gestión de: REGISTRO DE PUBLICACIÓN" & "<br />"
            Else
                nTipoMail = MAIL_MODIF_REGISTRO
                sSubject = "INTeatroDigital - Actualización de Registro de Publicación"
                sBody = "Estimada/o usuaria/o de INTeatroDigital:" & "<br />" & "<br />"
                sBody += "Se ha recepcionado su gestión de: ACTUALIZACION DE REGISTRO DE PUBLICACIÓN) " & "<br />"
                sBody += "REGISTRO INT N°: " & CodigoRegistro & "<br />"
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
            sBody += "Denominación: " & txtDenominacion.Text & "<br />"
            sBody += "Tipo de Publicación: " & IIf(radioPeriodica.Checked, "Periódica", "Eventual") & "<br />"
            If TextInicio.Value.Trim <> "" Then
                sBody += "Fecha desde: " & TextInicio.Value.Trim & "<br />"
            End If
            sBody += "<br />"

            sBody += "Lista de Integrantes" & "<br />"
            sBody += RegistroModulo.GetIntegrantes(sIdRegistro, True)
            sBody += "<br />"

            If BtnGuardar.Text = "Confirmar Registro" Then
                sBody += "Usted ha realizado el trámite de Registro de PUBLICACIÓN en INTeatroDigital. Estamos " & "<br />"
            Else
                sBody += "Usted ha realizado el trámite de Actualización de Registro de PUBLICACIÓN en INTeatroDigital. Estamos " & "<br />"
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
                sSubject = "INTeatroDigital - Vinculación a Publicación"
                sBody = "Estimado usuario de INTeatroDigital: " & "<br />"
                sBody += "Usted ha sido incorporado como integrante de " & RTrim(txtDenominacion.Text) & " en el Registro Nacional del Teatro Independiente. " & "<br />"
                sBody += "A partir de este momento, para poder 'validar' su vinculación a dicha Publicación, deberá ingresar a la plataforma de " & "<br />"
                sBody += "INTeatroDigital, y en la sección 'Mis Vinculaciones' clickear en 'Confirmar Vinculación'. " & "<br />"
                sBody += "<br />"
            Else
                sSubject = "INTeatroDigital - Actualización de Registro Vinculado"
                sBody = "Estimado usuario de INTeatroDigital: " & "<br />"
                sBody += "Se ha procesado satisfactoriamente la solicitud de Actualización del Registro de Publicación " & RTrim(txtDenominacion.Text) & "<br />"
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
                sSubject = "INTeatroDigital - Solicitud de Registro de Publicación"
                sBody = "REGISTRO de PUBLICACIÓN: " & txtDenominacion.Text.Trim & "<br />"
            Else
                sSubject = "INTeatroDigital - Actualización de Registro de Publicación"
                sBody = "ACTUALIZACION DE REGISTRO de PUBLICACIÓN: " & txtDenominacion.Text.Trim & "<br />"
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
            sBody += "Tipo de Publicación: " & IIf(radioPeriodica.Checked, "Periódica", "Eventual") & "<br />"
            If TextInicio.Value.Trim <> "" Then
                sBody += "Fecha desde: " & TextInicio.Value.Trim & "<br />"
            End If
            sBody += "<br />"
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
        txtErrorAcepto.Text = ""
        txtErrorTipoPublicacion.Text = ""
        txtErrorFechaInicio.Text = ""

        If Funciones.CaracteresEspecialesnumeros(txtDenominacion.Text.Trim) Then
            txtErrorAcepto.Text = "La denominación contiene caracteres especiales"
            txtDenominacion.Focus()
            Return False
        End If

        If ddlLocalidades.SelectedValue = 0 Then
            txtErrorLocalidades.Text = "Debe seleccionar una localidad"
            ddlLocalidades.Focus()
            Return False
        End If

        If radioPeriodica.Checked = True And TextInicio.Value = "" Then
            txtErrorTipoPublicacion.Text = "Debe ingresar la fecha de inicio de actividades"
            TextInicio.Focus()
            Return False

            Try
                dFecha = CDate(TextInicio.Value)
            Catch ex As Exception
                txtErrorFechaInicio.Text = "Fecha inválida"
                Return False
            End Try

            Try
                If Not ValidarFecha(TextInicio.Value.Trim) Then
                    txtErrorFechaInicio.Text = "Fecha inválida"
                    Return False
                End If
            Catch ex As Exception
                txtErrorFechaInicio.Text = "Fecha inválida"
                Return False
            End Try

        End If

        'If quien.Persona = "HUMANA" And ds.Integrantes.Count < 1 Then
        '    txtIntegrante.Text = "Debe ingresar al menos 1 integrante"
        '    Return False
        'ElseIf quien.Persona = "JURIDICA" And ds.Integrantes.Count < 2 Then
        '    txtIntegrante.Text = "Debe ingresar al menos 2 integrantes"
        '    Return False
        'End If

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
        Dim sFechaInicio As String
        Dim sTipoPublicacion As String

        Try

            If radioPeriodica.Checked Then
                sTipoPublicacion = "1"
                Dim wfecha As Date = CDate(TextInicio.Value)
                sFechaInicio = Year(wfecha) * 10000 + Month(wfecha) * 100 + Day(wfecha)
            Else
                sTipoPublicacion = "2"
                sFechaInicio = "NULL"
            End If


            'INSERT Registro
            sSQLCmd = "INSERT INTO Registro " & _
                            "(responsable, sector, provincia, " & _
                            "denominacion, localidad, copost, " & _
                            "domicilio, prefijo, telefono, email, " & _
                            "pagina, tipopubli, inicioactiv, " & _
                            "fechAlta) " & _
                        "VALUES " & _
                            "(" & Session("USER_ID") & ", " & Session("SECTOR") & ", " & ddlProvincias.SelectedValue & ", " & _
                            "'" & txtDenominacion.Text.Trim.ToUpper & "', " & ddlLocalidades.Text & ",'" & txtCP.Text & "' ," & _
                            "'" & txtDomicilio.Text.Trim.ToUpper & "','" & txtPrefijo.Text.Trim & "','" & txtNumero.Text.Trim & "' , '" & txtMail.Text.Trim & "', "

            If radioPeriodica.Checked Then
                sSQLCmd += "'" & txtWeb.Text.Trim & "', " & sTipoPublicacion & ", Convert(datetime,'" & sFechaInicio & "'), "
            Else
                sSQLCmd += "'" & txtWeb.Text.Trim & "', " & sTipoPublicacion & ", " & sFechaInicio & " , "
            End If

            sSQLCmd += "getdate()) " & _
                            "SET @nIdRegistro = SCOPE_IDENTITY()"

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

            dsInte.AceptaCambios(ds, nIdRegistro)

            'INSERT Palabras
            'sSQLCmd = "INSERT INTO RegistroPalabras " & _
            '                "(codigoRegistro, " & _
            '                "APELLIDO_Y_NOMBRE_DEL_RESPONSABLE, " & _
            '                "DIRECCION_DE_CORREO_ELECTRONICO_DE_LA_PUBLICACION, " & _
            '                "DIRECCION_DE_CORREO_ELECTRONICO_DEL_RESPONSABLE, " & _
            '                "PAGINA_WEB_DE_LA_PUBLICACION, " & _
            '                "TELEFONO_DE_LA_REDACCION, " & _
            '                "TELEFONOS_DEL_RESPONSABLE) " & _
            '            "VALUES " & _
            '                "(" & nIdRegistro & ", " & _
            '                " " & IIf(chkPalabra1.Checked, 1, 0) & ", " & _
            '                " " & IIf(chkPalabra10.Checked, 1, 0) & ", " & _
            '                " " & IIf(chkPalabra15.Checked, 1, 0) & ", " & _
            '                " " & IIf(chkPalabra18.Checked, 1, 0) & ", " & _
            '                " " & IIf(chkPalabra24.Checked, 1, 0) & ", " & _
            '                " " & IIf(chkPalabra27.Checked, 1, 0) & _
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
        Dim sFechaInicio As String
        Dim sTipoPublicacion As String
        Dim wfecha As Date = DateAndTime.Now
        Try

            If radioPeriodica.Checked Then
                sTipoPublicacion = "1"
                wfecha = CDate(TextInicio.Value)
                sFechaInicio = Year(wfecha) * 10000 + Month(wfecha) * 100 + Day(wfecha)
            Else
                sTipoPublicacion = "2"
                sFechaInicio = "NULL"
            End If
            Try
                wfecha = CDate(TextInicio.Value)
                sFechaInicio = Year(wfecha) * 10000 + Month(wfecha) * 100 + Day(wfecha)
            Catch ex As Exception
                If sTipoPublicacion = "1" Then
                    txtErrorAcepto.Text = "Ingrese fecha de inicio"
                    Return False
                Else
                    sFechaInicio = ""
                End If
            End Try

            'UPDATE Registro
            sSQLCmd = "UPDATE Registro " & _
                           "SET RESPONSABLE = " & Session("USER_ID") & ",  " & _
                              "SECTOR = " & Session("SECTOR") & ",  " & _
                              "DENOMINACION = '" & txtDenominacion.Text.Trim.ToUpper & "', " & _
                              "LOCALIDAD = " & ddlLocalidades.Text & ", " & _
                              "copost = '" & txtCP.Text & "', " & _
                              "domicilio = '" & txtDomicilio.Text.Trim.ToUpper & "', " & _
                              "prefijo = '" & txtPrefijo.Text.Trim & "', " & _
                              "telefono = '" & txtNumero.Text.Trim & "', " & _
                              "Provincia = " & ddlProvincias.SelectedValue & ", " & _
                              "tipopubli = " & sTipoPublicacion & ", "
            If radioPeriodica.Checked Then
                sSQLCmd += "inicioactiv = Convert(datetime,'" & sFechaInicio & "'), "
            Else
                If sFechaInicio.Length > 0 Then
                    sSQLCmd += "inicioactiv = Convert(datetime,'" & sFechaInicio & "'), "
                End If
                'sSQLCmd += "inicioactiv = " & sFechaInicio & ", "
            End If

            sSQLCmd += "EMAIL = '" & txtMail.Text.Trim & "', " & _
                            "PAGINA = '" & txtWeb.Text.Trim & "' " & _
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

            dsInte.AceptaCambios(ds, Session("CODIGO"))

            Dim Sql As String = "update integrantes set verificado = null where codigoregistro=" & Session("CODIGO")
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
        Const F_COPOST As Integer = 5
        Const F_DOMICILIO As Integer = 6
        Const F_PREFIJO As Integer = 7
        Const F_TELEFONO As Integer = 8
        Const F_EMAIL As Integer = 9
        Const F_PAGINA As Integer = 10
        Const F_TIPO_PUBLICACION As Integer = 11
        Const F_INICIO As Integer = 12
        'Const F_FECHALTA As Integer = 13

        If Len(RTrim(TxtPrefijo.Text)) + Len(RTrim(TxtNumero.Text)) > 10 Then
            lblErrorTelefono.Text = "Número de TE Incorrecto"
            TxtPrefijo.Focus()
            Return
        End If

        Dim sSQLCmd As String
        Dim MyConnection As SqlConnection
        Dim MyCommand As SqlCommand
        Dim MyReader As SqlDataReader

        Try
            'Load Registro
            sSQLCmd = "SELECT responsable, sector, provincia, " & _
                                "denominacion, localidad, copost, " & _
                                "domicilio, prefijo, telefono, " & _
                                "email, pagina, " & _
                                "tipopubli, inicioactiv, fechAlta " & _
                            "FROM Registro " & _
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
                ddlLocalidades.Text = MyReader.Item(F_LOCALIDAD)
                txtCP.Text = IIf(MyReader.Item(F_COPOST) <> 0, MyReader.Item(F_COPOST), "")
                txtDomicilio.Text = MyReader.Item(F_DOMICILIO)
                txtPrefijo.Text = IIf(MyReader.Item(F_PREFIJO) <> 0, MyReader.Item(F_PREFIJO), "")
                txtNumero.Text = IIf(MyReader.Item(F_TELEFONO) <> 0, MyReader.Item(F_TELEFONO), "")
                txtMail.Text = MyReader.Item(F_EMAIL).ToString.Trim
                txtWeb.Text = MyReader.Item(F_PAGINA).ToString.Trim
                radioPeriodica.Checked = IIf(MyReader.Item(F_TIPO_PUBLICACION) = 1, True, False)
                RadioEventual.Checked = IIf(MyReader.Item(F_TIPO_PUBLICACION) = 2, True, False)
                If MyReader.Item(F_INICIO).Equals(DBNull.Value) Then
                    TextInicio.Value = ""
                Else
                    TextInicio.Value = MyReader.Item(F_INICIO)
                End If
            End If

            MyReader.Dispose()
            MyCommand.Dispose()
            MyConnection.Dispose()

            'Cargo Integrantes
            BorrarTemporal()
            CargarTemporal(nCodigo)

            'Carga las combos
            Inicializar()

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
        sSQLCmd = "SELECT count(*) AS cantidad " & _
                        "FROM Regisdig " & _
                        "WHERE CUIL = " & sCUIT & " AND fechBaja IS NULL"

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
        sSQLCmd = "SELECT count(*) AS cantidad " & _
                        "FROM IntegrantesTemp " & _
                        "WHERE cuit = " & Session("CUIT") & " AND CUIL = " & sCUIT

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
            sSQLCmd = "INSERT INTO IntegrantesTemp " & _
                            "(cuit, codigoRegistro, cuil) " & _
                            "VALUES " & _
                            "(" & Session("CUIT") & ", NULL, '" & txtIntegrante.Text.Trim.ToUpper & "') " & _
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
        tablaDatos.visible = AceptoDJ.Checked
    End Sub
End Class