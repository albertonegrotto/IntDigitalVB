Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration

Partial Public Class registroEspectaculo
    Inherits System.Web.UI.Page
    Dim cn As New SqlClient.SqlConnection(SqlConex)
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
                Session.Add("CodRegistro", nCodigo)
                Session("sAccion") = sAccion
                If sAccion = "M" Then
                    HyperLinkBack.Attributes.Add("href", "RegistroLista.aspx")
                End If
                ds = New dsIntegrantes()
                If sAccion.ToUpper = "A" Then
                    Inicializar()
                    BorrarTemporal()
                    BtnGuardar.Text = "Confirmar Registro"
                ElseIf sAccion.ToUpper = "M" Then
                    CargarDatos(nCodigo)
                    BtnGuardar.Text = "Confirmar Actualización de Registro"
                    Session.Add("CODIGO", nCodigo)
                End If

                'Cargo la gridview
                'SqlDataSource1.SelectParameters("cuit").DefaultValue = Session("CUIT")
                If sAccion.ToUpper = "M" Then
                    dsInte.CargaIntegrantes(ds, nCodigo)
                    GridView1.DataSource = ds.Integrantes
                    GridView1.DataBind()
                End If
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
        ' Session("CODIGO") = quien.Codigo
        Session("SECTOR") = 6
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
        Dim sql2 As String = "Select 0 codigo, 'Seleccione Provincia' descrip union SELECT codigo,descrip FROM Provin WHERE region is not null"
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
                        "WHERE codigoRegistro = " & Session("USER_ID") & " AND fechaBaja is null"
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

            Dim sIdRegistro As String = IIf(Session("CODIGO_REGISTRO") Is Nothing, Session("CODIGO"), Session("CODIGO_REGISTRO"))
            Dim sSector As String = RTrim(ddlSectores.SelectedItem.Text)
            Dim sProvincia As String = RTrim(ddlProvincias.SelectedItem.Text)
            'Enviar email
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
            Dim sBody As String = ""

            If BtnGuardar.Text = "Confirmar Registro" Then
                nTipoMail = MAIL_ALTA_REGISTRO
                sSubject = "INTeatroDigital - Solicitud de Registro de Espectáculo Concertado"
                sBody = "Estimada/o usuaria/o de INTeatroDigital:" & "<br />" & "<br />"
                sBody += "Se ha recepcionado su gestión de: REGISTRO DE ESPECTACULO CONCERTADO" & "<br />"
            Else
                nTipoMail = MAIL_MODIF_REGISTRO
                sSubject = "INTeatroDigital - Actualización de Registro de Espectáculo Concertado"
                sBody = "Estimada/o usuaria/o de INTeatroDigital:" & "<br />" & "<br />"
                sBody += "Se ha recepcionado su gestión de: ACTUALIZACION DE REGISTRO DE ESPECTACULO CONCERTADO) " & "<br />"
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
            sBody += "Autor: " & txtAutor.Text & "<br />"
            sBody += "<br />"

            sBody += "Lista de Integrantes" & "<br />"
            sBody += RegistroModulo.GetIntegrantes(sIdRegistro, True)
            sBody += "<br />"

            If BtnGuardar.Text = "Confirmar Registro" Then
                sBody += "Usted ha realizado el trámite de Registro de ESPECTACULO CONCERTADO en INTeatroDigital. Estamos " & "<br />"
            Else
                sBody += "Usted ha realizado el trámite de Actualización de Registro de ESPECTACULO CONCERTADO en INTeatroDigital. Estamos " & "<br />"
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
            'End of Enviar email

            'Integrantes
            Dim sResult2 As String = ""
            If BtnGuardar.Text = "Confirmar Registro" Then
                sSubject = "INTeatroDigital - Vinculación a Registro de Espectáculo Concertado"
                sBody = "Estimado usuario de INTeatroDigital: " & "<br />"
                sBody += "Usted ha sido incorporado como integrante de " & RTrim(txtDenominacion.Text) & " en el Registro Nacional del Teatro Independiente. " & "<br />"
                sBody += "A partir de este momento, para poder 'validar' su vinculación a dicho Espectáculo Conertado, deberá ingresar a la plataforma de " & "<br />"
                sBody += "INTeatroDigital, y en la sección 'Mis Vinculaciones' clickear en 'Confirmar Vinculación'. " & "<br />"
                sBody += "<br />"
            Else
                sSubject = "INTeatroDigital - Actualización de Registro Vinculado"
                sBody = "Estimado usuario de INTeatroDigital: " & "<br />"
                sBody += "Se ha procesado satisfactoriamente la solicitud de Actualización del Registro de Espectáculo Concertado " & RTrim(txtDenominacion.Text) & "<br />"
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
            sql = "select g.EMAIL from integrantes i, REGISDIG g where i.codigoRegistro=" & sIdRegistro & " and i.CUIL=g.CUIL and i.verificado is null and fechaBaja is null"
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
                sSubject = "INTeatroDigital - Solicitud de Registro de Espectáculo Concertado"
                sBody = "REGISTRO de ESPECTACULO CONCERTADO: " & txtDenominacion.Text.Trim & "<br />"
            Else
                sSubject = "INTeatroDigital - Actualización de Registro de Espectáculo Concertado"
                sBody = "ACTUALIZACION DE REGISTRO de ESPECTACULO CONCERTADO: " & txtDenominacion.Text.Trim & "<br />"
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
            sBody += "Autor: " & txtAutor.Text & "<br />"
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

        Catch ex As Exception

        End Try
    End Sub

    Protected Function ValidarDatos()

        'Limpio los errores
        txtErrorLocalidades.Text = ""
        txtErrorAcepto.Text = ""
        If ddlLocalidades.SelectedValue = 0 Then
            ddlLocalidades.Focus()
            txtErrorLocalidades.Text = "Debe seleccionar una localidad"
            Return False
        End If
        If chkAcepto.Checked = False Then
            chkAcepto.Focus()
            txtErrorAcepto.Text = "Debe aceptar los términos para continuar"
            Return False
        End If
        txtDenominacion.Text = LimpiarCaracteres(txtDenominacion.Text.Trim)
        Dim nCantidad As Integer
        nCantidad = GridView1.Rows.Count()
        If quien.Persona = "FISICA" And nCantidad <1 Then
            txtErrorIntegrante.Text="Debe ingresar al menos 1 integrante"
            txtErrorIntegrante.Focus()
            Return False
        End If
        'End of Al menos un integrante cargado
        If quien.Persona = "JURIDICA" And nCantidad < 2 Then
            txtErrorIntegrante.Text = "Debe ingresar al menos 2 integrante"
            txtErrorIntegrante.Focus()
            Return False
        End If
        If Funciones.CaracteresEspecialesnumeros(txtDenominacion.Text.Trim) Then
            txtErrorAcepto.Text = "La denominación contiene caracteres especiales"
            txtDenominacion.Focus()
            Return False
        End If
        If checkAutorizoPublicar.Checked = False Then
            lblErrorcheckAutorizoPublicar.Text = "Debe aceptar los términos para continuar"
            checkAutorizoPublicar.Focus()
            Return False
        End If
        Return True

    End Function

    Protected Function GuardarDatos() As Integer
        Dim MyConnection As SqlConnection
        Dim MyCommand As SqlCommand
        Dim sSQLCmd As String
        Dim mIdRegistro As SqlParameter
        Dim nIdRegistro As Integer
        Try
            'INSERT Registro
            sSQLCmd = "INSERT INTO Registro " &
                            "(responsable, sector, provincia, " &
                            "denominacion, autor, localidad, email, " &
                            "pagina, fechAlta) " &
                        "VALUES " &
                            "(" & Session("USER_ID") & ", " & Session("SECTOR") & ", " & ddlProvincias.SelectedValue & ", " &
                            "'" & txtDenominacion.Text.Trim.ToUpper & "', '" & txtAutor.Text.Trim.ToUpper & "', " & ddlLocalidades.Text & ", '" & txtMail.Text.Trim & "', " &
                            "'" & txtWeb.Text.Trim & "', getdate())  " &
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
            Session("CodRegistro") = nIdRegistro

            'INSERT Palabras
            'sSQLCmd = "INSERT INTO RegistroPalabras " &
            '                "(codigoRegistro, " &
            '                "APELLIDO_Y_NOMBRE_DEL_RESPONSABLE, " &
            '                "CANTIDAD_DE_INTEGRANTES, " &
            '                "DIRECCION_DE_CORREO_ELECTRONICO_DEL_ESPECTACULO, " &
            '                "DIRECCION_DE_CORREO_ELECTRONICO_DEL_RESPONSABLE, " &
            '                "PAGINA_WEB_DEL_ESPECTACULO, " &
            '                "TELEFONOS_DEL_RESPONSABLE) " &
            '            "VALUES " &
            '                "(" & nIdRegistro & ", " &
            '                " " & IIf(chkPalabra1.Checked, 1, 0) & ", " &
            '                " " & IIf(chkPalabra2.Checked, 1, 0) & ", " &
            '                " " & IIf(chkPalabra12.Checked, 1, 0) & ", " &
            '                " " & IIf(chkPalabra15.Checked, 1, 0) & ", " &
            '                " " & IIf(chkPalabra20.Checked, 1, 0) & ", " &
            '                " " & IIf(chkPalabra27.Checked, 1, 0) &
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
        Dim cn As New SqlClient.SqlConnection(SqlConex)
        Dim MyConnection As SqlConnection
        Dim MyCommand As SqlCommand
        Dim sSQLCmd As String
        Dim nCodigo As Integer = Session("CodRegistro")
        Dim nResponsable As Integer = Session("USER_ID")
        cn.Close()
        cn.Open()
        Dim sql As String = "SELECT RESPONSABLE FROM REGISTRO WHERE codigo = " & nCodigo
        Dim Psql As New SqlClient.SqlCommand(sql, cn)
        Dim dr As SqlClient.SqlDataReader = Psql.ExecuteReader
        While dr.Read()
            nResponsable = dr.GetInt32(0)
        End While
        dr.Close()
        cn.Close()

        Try
            'UPDATE Registro
            sSQLCmd = "UPDATE Registro " &
                           "SET RESPONSABLE = " & Session("USER_ID") & ",  " &
                           "SECTOR = " & Session("SECTOR") & ",  " &
                           "DENOMINACION = '" & txtDenominacion.Text.Trim.ToUpper & "', " &
                           "AUTOR = '" & txtAutor.Text.Trim.ToUpper & "', " &
                           "LOCALIDAD = " & ddlLocalidades.Text & ", " &
                           "Provincia = " & ddlProvincias.SelectedValue & ", " &
                           "EMAIL = '" & txtMail.Text.Trim & "', " &
                           "PAGINA = '" & txtWeb.Text.Trim & "' " &
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
            dsInte.AceptaCambios(ds, nCodigo)

            'UPDATE Palabras
            'sSQLCmd = "UPDATE RegistroPalabras " &
            '               "SET APELLIDO_Y_NOMBRE_DEL_RESPONSABLE = " & IIf(chkPalabra1.Checked, 1, 0) & ",  " &
            '                  "CANTIDAD_DE_INTEGRANTES = " & IIf(chkPalabra2.Checked, 1, 0) & ",  " &
            '                  "DIRECCION_DE_CORREO_ELECTRONICO_DEL_ESPECTACULO = " & IIf(chkPalabra12.Checked, 1, 0) & ",  " &
            '                  "DIRECCION_DE_CORREO_ELECTRONICO_DEL_RESPONSABLE = " & IIf(chkPalabra15.Checked, 1, 0) & ",  " &
            '                  "PAGINA_WEB_DEL_ESPECTACULO = " & IIf(chkPalabra20.Checked, 1, 0) & ",  " &
            '                  "TELEFONOS_DEL_RESPONSABLE = " & IIf(chkPalabra27.Checked, 1, 0) & " " &
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

            sql = "update integrantes set verificado = null where codigoregistro=" & Session("CODIGO")
            Dim cmd As New SqlClient.SqlCommand(sql, cn)
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
        Const F_AUTOR As Integer = 4
        Const F_LOCALIDAD As Integer = 5
        Const F_EMAIL As Integer = 6
        Const F_PAGINA As Integer = 7
        'Const F_FECHALTA As Integer = 8
        Dim sSQLCmd As String
        Dim MyConnection As SqlConnection
        Dim MyCommand As SqlCommand
        Dim MyReader As SqlDataReader

        Try
            'Load Registro
            sSQLCmd = "SELECT responsable, sector, provincia, " &
                                "denominacion, autor, localidad, email, " &
                                "pagina, fechAlta " &
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
                txtAutor.Text = MyReader.Item(F_AUTOR).ToString.Trim
                ddlLocalidades.Text = MyReader.Item(F_LOCALIDAD)
                txtMail.Text = MyReader.Item(F_EMAIL).ToString.Trim
                txtWeb.Text = MyReader.Item(F_PAGINA).ToString.Trim
            End If
            MyReader.Dispose()
            MyCommand.Dispose()

            nCodigo = Session("CodRegistro")
            dsInte.CargaIntegrantes(ds, nCodigo)
            GridView1.DataSource = ds.Integrantes
            GridView1.DataBind()
            GridView1.Visible = True

            'Load Palabras
            'sSQLCmd = "SELECT * " & _
            '                "FROM RegistroPalabras " & _
            '                "WHERE codigoRegistro = " & nCodigo.ToString

            'MyCommand = New SqlCommand(sSQLCmd, MyConnection)
            'MyReader = MyCommand.ExecuteReader()
            'If MyReader.Read() Then
            '    If MyReader.Item(1) = 1 Then
            '        chkPalabra1.Checked = True
            '    Else
            '        chkPalabra1.Checked = False
            '    End If
            '    If MyReader.Item(2) = 1 Then
            '        chkPalabra2.Checked = True
            '    Else
            '        chkPalabra2.Checked = False
            '    End If
            '    If MyReader.Item(12) = 1 Then
            '        chkPalabra12.Checked = True
            '    Else
            '        chkPalabra12.Checked = False
            '    End If
            '    If MyReader.Item(15) = 1 Then
            '        chkPalabra15.Checked = True
            '    Else
            '        chkPalabra15.Checked = False
            '    End If
            '    If MyReader.Item(20) = 1 Then
            '        chkPalabra20.Checked = True
            '    Else
            '        chkPalabra20.Checked = False
            '    End If
            '    If MyReader.Item(27) = 1 Then
            '        chkPalabra27.Checked = True
            '    Else
            '        chkPalabra27.Checked = False
            '    End If
            'End If
            'MyReader.Dispose()
            'MyCommand.Dispose()
            'End of Load Palabras

            MyConnection.Dispose()
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
                    Dim nCodigo As Integer = Session("CodRegistro")
                    dsInte.AgregaIntegrantes(ds, nCodigo, txtIntegrante.Text)
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
        sSQLCmd = "SELECT count(*) AS cantidad " &
                        "FROM Regisdig " &
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
        If dsInte.YaExiste(ds, sCUIT) Then
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
            'InsertIntegranteTemp = False
            'Response.Redirect("ErrorPage.aspx?errMessage=" & ex.Message & "&errSource=" & ex.Source)
        Finally
            MyCommand.Dispose()
            MyConnection.Dispose()
        End Try

        'InsertIntegranteTemp = True
    End Function

    Private Sub btnCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        Dim sAccion As String = Session("sAccion")
        If sAccion <> "M" Then
            Response.Redirect("menuFinal.aspx", False)
        Else
            Response.Redirect("RegistroLista.aspx", False)
        End If
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

    Protected Sub GridView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles GridView1.SelectedIndexChanged

    End Sub

    Protected Sub GridView1_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles GridView1.RowDeleting
        Dim id As String
        id = GridView1.DataKeys(e.RowIndex).Value
        Dim nCodigo As Integer = Session("CodRegistro")
        dsInte.DesvinculaIntegrante(ds, nCodigo, id)
        GridView1.DataBind()
    End Sub
End Class
