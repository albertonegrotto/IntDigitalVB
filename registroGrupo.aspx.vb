Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Collections
Imports System.IO
Imports System.IO.Packaging
Partial Public Class registroGrupo
    Inherits System.Web.UI.Page
    Dim cn As New SqlClient.SqlConnection(SqlConex)
    Dim aDeletedCUIL As New ArrayList
    Dim quien As usuario
    Dim nSector As Integer
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
                sAccion = Request.QueryString("accion")
                nCodigo = Request.QueryString("codigo")
                nSector = Request.QueryString("S")
                Session("sAccion") = sAccion
                If sAccion = "M" Then
                    HyperLinkBack.Attributes.Add("href", "RegistroLista.aspx")
                End If
                SetearVariablesSession()
                If sAccion.ToUpper = "A" Then
                    Inicializar()
                    BorrarTemporal()
                    BtnGuardar.Text = "Confirmar Registro"
                    Session.Add("CodRegistro", nCodigo)
                ElseIf sAccion.ToUpper = "M" Then
                    CargarDatos(nCodigo)
                    BtnGuardar.Text = "Confirmar Actualización de Registro"
                    Session.Add("CODIGO", nCodigo)
                    Session.Add("CodRegistro", nCodigo)
                    'OcultarAdjuntos()
                    'CargAdjuntos()
                End If

                'La primera vez lo agrego al ViewState
                ViewState.Add("DELETED_CUIL", aDeletedCUIL)

                'Cargo la gridview
                'SqlDataSource1.SelectParameters("cuit").DefaultValue = Session("CUIT")
                nCodigo = Session("CodRegistro")
                dsInte.CargaIntegrantes(ds, nCodigo)
                GridView1.DataSource = ds.Integrantes
                GridView1.DataBind()
                GridView1.Visible = True

            Else
                Response.Clear()
                Response.Redirect("index.aspx", False)
            End If

        Else
            'La respuesta
            MaintainScrollPositionOnPostBack = True
            'If BtnGuardar.Text = "Confirmar Registro" Then
            GuardarAdjuntos()
            'End If
        End If

    End Sub
    Private Sub SetearVariablesSession()
        quien = CType(Session("usuario"), usuario)
        Session("PROVINCIA") = quien.codprovin
        Session("CUIT") = quien.Usuario
        Session("USER_ID") = quien.Codigo
        Session("SECTOR") = nSector
    End Sub

    Private Sub OcultarAdjuntos()
        DivTablaEquipamiento.Visible = False
        DivTablaTrayectoria.Visible = False
    End Sub

    Private Sub GuardarAdjuntos()
        Dim wfecha As Date = DateTime.Now.ToString
        Dim wdia As Integer = wfecha.Day
        Dim wmes As Integer = wfecha.Month
        Dim wano As Integer = wfecha.Year
        Dim whora As Integer = wfecha.Hour
        Dim wminu As Integer = wfecha.Minute
        Dim woperador As String = Session("CUIL")
        If Session("FileUploadEquipa1") Is Nothing Or FileUploadEquipa.HasFile Then
            If FileUploadEquipa.HasFile Then
                Dim FileName As String = Path.GetFileName(FileUploadEquipa.PostedFile.FileName)
                Dim Extension As String = Path.GetExtension(FileUploadEquipa.PostedFile.FileName)
                Dim wsegu As Integer = wfecha.Second
                Dim wdir As String = wano.ToString + wmes.ToString + wdia.ToString + whora.ToString + wminu.ToString + wsegu.ToString
                If UCase(Extension) = ".PDF" Or UCase(Extension) = ".DOC" Or UCase(Extension) = ".DOCX" Then
                    Dim randomName As String = RTrim(woperador) + wdir
                    If Not Directory.Exists(Server.MapPath("~/Temp/" & randomName & "/")) Then
                        Directory.CreateDirectory(Server.MapPath("~/Temp/" & randomName & "/"))
                    End If
                    Dim FilePath As String = (Server.MapPath("~/Temp/" & randomName & "/")) + FileName
                    Try
                        FileUploadEquipa.SaveAs(FilePath)
                    Catch ex As Exception
                    End Try
                    Session("FileUploadEquipa1") = FileUploadEquipa
                    Session("FileUploadEquipaFileName") = FilePath
                    LabelUploadEquipa.Text = FileUploadEquipa.FileName
                End If
            End If
        Else
            If Session("FileUploadEquipa1") IsNot Nothing Then
                Try
                    Dim FileUploadEquipa1 As FileUpload = CType(Session("FileUploadEquipa1"), FileUpload)
                    LabelUploadEquipa.Text = FileUploadEquipa1.FileName
                Catch ex As Exception
                End Try
            End If
        End If

        If Session("FileUploadEquipaSub1") Is Nothing Or FileUploadEquipaSub.HasFile Then
            If FileUploadEquipaSub.HasFile Then
                Dim FileName As String = Path.GetFileName(FileUploadEquipaSub.PostedFile.FileName)
                Dim Extension As String = Path.GetExtension(FileUploadEquipaSub.PostedFile.FileName)
                Dim wsegu As Integer = wfecha.Second
                Dim wdir As String = wano.ToString + wmes.ToString + wdia.ToString + whora.ToString + wminu.ToString + wsegu.ToString
                If UCase(Extension) = ".PDF" Or UCase(Extension) = ".DOC" Or UCase(Extension) = ".DOCX" Then
                    Dim randomName As String = RTrim(woperador) + wdir
                    If Not Directory.Exists(Server.MapPath("~/Temp/" & randomName & "/")) Then
                        Directory.CreateDirectory(Server.MapPath("~/Temp/" & randomName & "/"))
                    End If
                    Dim FilePath As String = (Server.MapPath("~/Temp/" & randomName & "/")) + FileName
                    Try
                        FileUploadEquipaSub.SaveAs(FilePath)
                    Catch ex As Exception
                    End Try
                    Session("FileUploadEquipaSub1") = FileUploadEquipaSub
                    Session("FileUploadEquipaSubFileName") = FilePath
                    LabelUploadEquipaSub.Text = FileUploadEquipaSub.FileName
                End If
            End If
        Else
            If Session("FileUploadEquipaSub1") IsNot Nothing Then
                Try
                    Dim FileUploadEquipaSub1 As FileUpload = CType(Session("FileUploadEquipaSub1"), FileUpload)
                    LabelUploadEquipaSub.Text = FileUploadEquipaSub1.FileName
                Catch ex As Exception
                End Try
            End If
        End If

        If Session("FileUploadTrayectoria1") Is Nothing Or FileUploadTrayectoria.HasFile Then
            If FileUploadTrayectoria.HasFile Then
                Dim FileName As String = Path.GetFileName(FileUploadTrayectoria.PostedFile.FileName)
                Dim Extension As String = Path.GetExtension(FileUploadTrayectoria.PostedFile.FileName)
                Dim wsegu As Integer = wfecha.Second
                Dim wdir As String = wano.ToString + wmes.ToString + wdia.ToString + whora.ToString + wminu.ToString + wsegu.ToString
                If UCase(Extension) = ".PDF" Or UCase(Extension) = ".DOC" Or UCase(Extension) = ".DOCX" Then
                    Dim randomName As String = RTrim(woperador) + wdir
                    If Not Directory.Exists(Server.MapPath("~/Temp/" & randomName & "/")) Then
                        Directory.CreateDirectory(Server.MapPath("~/Temp/" & randomName & "/"))
                    End If
                    Dim FilePath As String = (Server.MapPath("~/Temp/" & randomName & "/")) + FileName
                    Try
                        FileUploadTrayectoria.SaveAs(FilePath)
                    Catch ex As Exception
                    End Try
                    Session("FileUploadTrayectoria1") = FileUploadTrayectoria
                    Session("FileUploadTrayectoriaFileName") = FilePath
                    LabelUploadTrayectoria.Text = FileUploadTrayectoria.FileName
                End If
            End If
        Else
            If Session("FileUploadTrayectoria1") IsNot Nothing Then
                Try
                    Dim FileUploadTrayectoria1 As FileUpload = CType(Session("FileUploadTrayectoria1"), FileUpload)
                    LabelUploadTrayectoria.Text = FileUploadTrayectoria1.FileName
                Catch ex As Exception
                End Try
            End If
        End If
    End Sub

    Private Sub Inicializar()
        Dim cn As New SqlClient.SqlConnection(SqlConex)

        'Sectores / Grupos
        cn.Open()
        Dim sql As String = "SELECT codigo, descrip FROM Sectores WHERE codigo = " & nSector  'Session("SECTOR")
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
        Dim sql2 As String = "SELECT 0 codigo, 'Seleccione Provincia' descrip union SELECT codigo,descrip FROM Provin WHERE region is not null" '& Session("PROVINCIA")
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
        bSaved = False
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
                sSubject = "INTeatroDigital - Solicitud de Registro de Grupo"
                sBody = "Estimada/o usuaria/o de INTeatroDigital:" & "<br />" & "<br />"
                sBody += "Se ha recepcionado su gestión de: REGISTRO DE GRUPO" & "<br />"
            Else
                nTipoMail = MAIL_MODIF_REGISTRO
                sSubject = "INTeatroDigital - Actualización de Registro de Grupo"
                sBody = "Estimada/o usuaria/o de INTeatroDigital:" & "<br />" & "<br />"
                sBody += "Se ha recepcionado su gestión de: ACTUALIZACION DE REGISTRO DE GRUPO) " & "<br />"
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
            sBody += "Fecha de Inicio de actividades: " & TextDesde.Value & "<br />"
            'sBody += "Descripción del equipamiento técnico: " & txtEquipamiento.Text & "<br />"
            sBody += "Cuenta con un espacio teatral: " & IIf(radioSi.Checked, "Si", "No") & "<br />"
            sBody += "Denominación del espacio: " & txtEspacio.Text & "<br />"
            sBody += "<br />"

            sBody += "Lista de Integrantes" & "<br />"
            sBody += RegistroModulo.GetIntegrantes(sIdRegistro, True)
            sBody += "<br />"

            If BtnGuardar.Text = "Confirmar Registro" Then
                sBody += "Usted ha realizado el trámite de Registro de GRUPO en INTeatroDigital. Estamos " & "<br />"
            Else
                sBody += "Usted ha realizado el trámite de Actualización de Registro de GRUPO en INTeatroDigital. Estamos " & "<br />"
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
                sSubject = "INTeatroDigital - Vinculación a Registro de Grupo"
                sBody = "Estimado usuario de INTeatroDigital: " & "<br />"
                sBody += "Usted ha sido incorporado como integrante de " & RTrim(txtDenominacion.Text) & " en el Registro Nacional del Teatro Independiente. " & "<br />"
                sBody += "A partir de este momento, para poder 'validar' su vinculación a dicho Grupo, deberá ingresar a la plataforma de " & "<br />"
                sBody += "INTeatroDigital, y en la sección 'Mis Vinculaciones' clickear en 'Confirmar Vinculación'. " & "<br />"
                sBody += "<br />"
            Else
                sSubject = "INTeatroDigital - Actualización de Registro Vinculado"
                sBody = "Estimado usuario de INTeatroDigital: " & "<br />"
                sBody += "Se ha procesado satisfactoriamente la solicitud de Actualización del Registro de Grupo " & RTrim(txtDenominacion.Text) & "<br />"
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
                sSubject = "INTeatroDigital - Solicitud de Registro de Grupo"
                sBody = "REGISTRO de GRUPO: " & txtDenominacion.Text.Trim & "<br />"
            Else
                sSubject = "INTeatroDigital - Actualización de Registro de Grupo"
                sBody = "ACTUALIZACION DE REGISTRO de GRUPO: " & txtDenominacion.Text.Trim & "<br />"
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
            sBody += "Fecha de Inicio de actividades: " & TextDesde.Value & "<br />"
            sBody += "Descripción del equipamiento técnico: " & txtEquipamiento.Text & "<br />"
            sBody += "Cuenta con un espacio teatral: " & IIf(radioSi.Checked, "Si", "No") & "<br />"
            sBody += "Denominación del espacio: " & txtEspacio.Text & "<br />"
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
        txtErrorEspacio.Text = ""
        txtErrorAcepto.Text = ""
        txtErrorFechaInicio.Text = ""
        txtErrorEquipamiento.Text = ""

        If Funciones.CaracteresEspecialesnumeros(txtDenominacion.Text.Trim) Then
            txtErrorAcepto.Text = "La denominación contiene caracteres especiales"
            txtDenominacion.Focus()
            Return False
        End If

        Try
            dFecha = CDate(TextDesde.Value)
        Catch ex As Exception
            txtErrorFechaInicio.Text = "Fecha inválida"
            TextDesde.Focus()
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

        If ddlLocalidades.SelectedValue = 0 Then
            txtErrorLocalidades.Text = "Debe seleccionar una localidad"
            ddlLocalidades.Focus()
            Return False
        End If

        If radioSi.Checked Then
            If txtEspacio.Text.Trim = "" Then
                txtErrorEspacio.Text = "Debe ingresar los datos de Denominación del Espacio"
                txtEspacio.Focus()
                Return False
            End If
        End If

        'If txtEquipamiento.Text.Length > 200 Then
        '    txtErrorEquipamiento.Text = "Máximo 200 caracteres"
        '    txtEquipamiento.Focus()
        '    Return False
        'End If

        If RadioButtonEquipa1.Checked = False And RadioButtonEquipa2.Checked = False Then
            txtErrorEquipamiento.Text = "Debe informar sobre equipamiento"
            RadioButtonEquipa1.Focus()
            Return False
        End If

        If txtEspacio.Text.Length > 200 Then
            txtErrorEspacio.Text = "Máximo 200 caracteres"
            txtEspacio.Focus()
            Return False
        End If

        txtDenominacion.Text = LimpiarCaracteres(txtDenominacion.Text.Trim)

        If quien.Persona = "FISICA" Then
            If ds.Integrantes.Count < 1 Then
                txtErrorIntegrante.Text = "Debe ingresar al menos 1 integrante"
                txtErrorIntegrante.Focus()
                Return False
            End If

        ElseIf quien.Persona = "JURIDICA" Then
            If ds.Integrantes.Count < 2 Then
                txtErrorIntegrante.Text = "Debe ingresar al menos 2 integrantes"
                txtErrorIntegrante.Focus()
                Return False
            End If
        End If
        'End of Al menos un integrante cargado

        If ChkAdultos.Checked = False And ChkInfantiles.Checked = False And ChkTodoPublico.Checked = False And ChkTalleres.Checked = False And ChkFormacion.Checked = False And ChkSala.Checked = False Then
            txtErrorActividades.Text = "Debe seleccionar por lo menos una Actividad"
            ChkAdultos.Focus()
            Return False
        End If

        'If BtnGuardar.Text = "Confirmar Registro" Then
        If RadioButtonEquipa1.Checked = True Then
                Dim fcv As Integer = 0
                If FileUploadEquipa.HasFile Then
                    Dim Extension As String = Path.GetExtension(FileUploadEquipa.PostedFile.FileName)
                    If UCase(Extension) <> ".PDF" And UCase(Extension) <> ".DOC" And UCase(Extension) <> ".DOCX" Then
                        txtErrorEquipamiento.Text = "El Listado de Equipamiento no es un documento Adobe .PDF o Word .DOC .DOCX"
                        txtErrorEquipamiento.Focus()
                        Return False
                    Else
                        fcv = 1
                    End If
                    Dim sizeInBytes As Long = FileUploadEquipa.PostedFile.ContentLength
                    If sizeInBytes / 1000000 > 10 Then
                        txtErrorEquipamiento.Text = "Equipamiento tiene un tamaño mayor a 10 Mb"
                        txtErrorEquipamiento.Focus()
                        Return False
                    Else
                        fcv = 1
                    End If
                Else
                If Session("FileUploadEquipa1") IsNot Nothing Then
                    Try
                        Dim UploadImporta1 As FileUpload = CType(Session("FileUploadEquipa1"), FileUpload)
                        Dim Extension As String = Path.GetExtension(UploadImporta1.PostedFile.FileName)
                        If UCase(Extension) <> ".PDF" And UCase(Extension) <> ".DOC" And UCase(Extension) <> ".DOCX" Then
                            txtErrorEquipamiento.Text = "El Listado de Equipamiento no es un documento Adobe PDF o Word DOC DOCX"
                            txtErrorEquipamiento.Focus()
                            Return False
                        Else
                            fcv = 1
                        End If
                        Dim sizeInBytes As Long = UploadImporta1.PostedFile.ContentLength
                        If sizeInBytes / 1000000 > 10 Then
                            txtErrorEquipamiento.Text = "Equipamiento tiene un tamaño mayor a 10 Mb"
                            txtErrorEquipamiento.Focus()
                            Return False
                        Else
                            fcv = 1
                        End If
                    Catch ex As Exception
                    End Try
                End If
            End If
            'If fcv = 0 Then
            '    txtErrorEquipamiento.Text = "Debe completar información del equipamiento"
            '    txtErrorEquipamiento.Focus()
            '    Return False
            'End If
            fcv = 0
            If FileUploadEquipaSub.HasFile Then
                Dim Extension As String = Path.GetExtension(FileUploadEquipaSub.PostedFile.FileName)
                If UCase(Extension) <> ".PDF" And UCase(Extension) <> ".DOC" And UCase(Extension) <> ".DOCX" Then
                    txtErrorEquipamiento.Text = "Equipamiento adquirido no es un documento Adobe .PDF o Word .DOC .DOCX"
                    txtErrorEquipamiento.Focus()
                    Return False
                Else
                    fcv = 1
                End If
                Dim sizeInBytes As Long = FileUploadEquipaSub.PostedFile.ContentLength
                If sizeInBytes / 1000000 > 10 Then
                    txtErrorEquipamiento.Text = "Equipamiento Adquirido tiene un tamaño mayor a 10 Mb"
                    txtErrorEquipamiento.Focus()
                    Return False
                Else
                    fcv = 1
                End If
            Else
                If Session("FileUploadEquipaSub1") IsNot Nothing Then
                    Try
                        Dim UploadImporta1 As FileUpload = CType(Session("FileUploadEquipaSub1"), FileUpload)
                        Dim Extension As String = Path.GetExtension(UploadImporta1.PostedFile.FileName)
                        If UCase(Extension) <> ".PDF" And UCase(Extension) <> ".DOC" And UCase(Extension) <> ".DOCX" Then
                            txtErrorEquipamiento.Text = "Equipamiento adquirido no es un documento Adobe PDF o Word DOC DOCX"
                            txtErrorEquipamiento.Focus()
                            Return False
                        Else
                            fcv = 1
                        End If
                        Dim sizeInBytes As Long = UploadImporta1.PostedFile.ContentLength
                        If sizeInBytes / 1000000 > 10 Then
                            txtErrorEquipamiento.Text = "Equipamiento adquirido tiene un tamaño mayor a 10 Mb"
                            txtErrorEquipamiento.Focus()
                            Return False
                        Else
                            fcv = 1
                        End If
                    Catch ex As Exception
                    End Try
                End If
            End If
            'If fcv = 0 Then
            '    txtErrorEquipamiento.Text = "Debe completar información del equipamiento adquirido"
            '    txtErrorEquipamiento.Focus()
            '    Return False
            'End If
        End If
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

        Return True

    End Function

    Protected Function GuardarDatos() As Integer
        Dim MyConnection As SqlConnection
        Dim MyCommand As SqlCommand
        Dim sSQLCmd As String
        Dim mIdRegistro As SqlParameter
        Dim nIdRegistro As Integer
        Dim sEspacio As String
        Dim sFechaInicio As String

        Try
            'If CheckBoxEspacioTeatral.Checked Then
            '    sEspacio = "1"
            'Else
            '    sEspacio = "0"
            'End If
            If radioSi.Checked Then
                sEspacio = "2"
            Else
                sEspacio = "1"
            End If

            Dim wfecha As Date = CDate(TextDesde.Value)
            sFechaInicio = Year(wfecha) * 10000 + Month(wfecha) * 100 + Day(wfecha)

            'INSERT Registro
            sSQLCmd = "INSERT INTO Registro " &
                            "(responsable, sector, provincia, " &
                            "denominacion, localidad, email, " &
                            "pagina, inicio, equipamiento, " &
                            "espaprop, comespacio, fechAlta) " &
                        "VALUES " &
                            "(" & Session("USER_ID") & ", " & Session("SECTOR") & ", " & ddlProvincias.SelectedValue & ", " &
                            "'" & txtDenominacion.Text.Trim.ToUpper & "', " & ddlLocalidades.Text & ", '" & txtMail.Text.Trim & "', " &
                            "'" & txtWeb.Text.Trim & "', Convert(datetime,'" & sFechaInicio & "'), '" & txtEquipamiento.Text.Trim.ToUpper & "', " &
                            "'" & sEspacio & "', '" & txtEspacio.Text.Trim.ToUpper & "', getdate())  " &
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

            Dim nADULTOS As Integer = 0
            Dim nINFANTIL As Integer = 0
            Dim nTODOPUB As Integer = 0
            Dim nTALLERES As Integer = 0
            Dim nESCUELA As Integer = 0
            Dim nSALA As Integer = 0
            If ChkAdultos.Checked = True Then
                nADULTOS = 1
            End If
            If ChkInfantiles.Checked = True Then
                nINFANTIL = 1
            End If
            If ChkTodoPublico.Checked = True Then
                nTODOPUB = 1
            End If
            If ChkTalleres.Checked = True Then
                nTALLERES = 1
            End If
            If ChkFormacion.Checked = True Then
                nESCUELA = 1
            End If
            If ChkSala.Checked = True Then
                nSALA = 1
            End If
            Dim nEquipPropio As Integer = 0
            If RadioButtonEquipa1.Checked = True Then
                nEquipPropio = 1
            End If
            Dim sql As String = "update REGISTRO set ADULTOS=" & nADULTOS & ",INFANTIL=" & nINFANTIL & ",TODOPUB = " & nTODOPUB & ",TALLERES=" & nTALLERES & ",ESCUELA=" & nESCUELA & ",SALA=" & nSALA &
                                 ",EQUIPPROPIO= " & nEquipPropio & " where CODIGO=" & nIdRegistro
            Dim Cmd As New SqlClient.SqlCommand(sql, cn)
            cn.Open()
            Try
                Cmd.ExecuteNonQuery()
            Catch ex As Exception
                txtErrorAcepto.Text = "Error al ingresar datos"
                Return False
            End Try
            cn.Close()

            dsInte.AceptaCambios(ds, nIdRegistro)

            Session("CodRegistro") = nIdRegistro

            Dim bGrabar As Boolean = False
            bGrabar = GrabarAdjuntos()
            If bGrabar = False Then
                GuardarDatos = False
                Return False
            End If

            Session.Add("CODIGO_REGISTRO", nIdRegistro)

        Catch ex As Exception
            GuardarDatos = False
            Return False
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
        'Dim mIdRegistro As SqlParameter
        'Dim nIdRegistro As Integer
        Dim sFechaInicio As String
        Dim sEspacio As String

        Try
            'If CheckBoxEspacioTeatral.Checked Then
            '    sEspacio = "1"
            'Else
            '    sEspacio = "0"
            'End If
            If radioSi.Checked Then
                sEspacio = "2"
            Else
                sEspacio = "1"
            End If

            Dim wfecha As Date = CDate(TextDesde.Value)
            sFechaInicio = Year(wfecha) * 10000 + Month(wfecha) * 100 + Day(wfecha)

            'UPDATE Registro
            sSQLCmd = "UPDATE Registro " &
                           "SET RESPONSABLE = " & Session("USER_ID") & ",  " &
                           "SECTOR = " & Session("SECTOR") & ",  " &
                           "DENOMINACION = '" & txtDenominacion.Text.Trim.ToUpper & "', " &
                           "LOCALIDAD = " & ddlLocalidades.Text & ", " &
                           "Provincia = " & ddlProvincias.SelectedValue & ", " &
                           "EMAIL = '" & txtMail.Text.Trim & "', " &
                           "PAGINA = '" & txtWeb.Text.Trim & "', " &
                           "INICIO = Convert(datetime,'" & sFechaInicio & "'), " &
                           "EQUIPAMIENTO = '" & txtEquipamiento.Text.Trim.ToUpper & "', " &
                           "espaprop = " & sEspacio & ", " &
                           "COMESPACIO =  '" & txtEspacio.Text.Trim.ToUpper & "', " &
                           "FECHMODI = getdate() " &
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
            'Session("CodRegistro") = Session("USER_ID")
            Dim ncodigo As Integer = Session("CodRegistro")

            Dim nADULTOS As Integer = 0
            Dim nINFANTIL As Integer = 0
            Dim nTODOPUB As Integer = 0
            Dim nTALLERES As Integer = 0
            Dim nESCUELA As Integer = 0
            Dim nSALA As Integer = 0
            If ChkAdultos.Checked = True Then
                nADULTOS = 1
            End If
            If ChkInfantiles.Checked = True Then
                nINFANTIL = 1
            End If
            If ChkTodoPublico.Checked = True Then
                nTODOPUB = 1
            End If
            If ChkTalleres.Checked = True Then
                nTALLERES = 1
            End If
            If ChkFormacion.Checked = True Then
                nESCUELA = 1
            End If
            If ChkSala.Checked = True Then
                nSALA = 1
            End If
            Dim nEquipPropio As Integer = 0
            If RadioButtonEquipa1.Checked = True Then
                nEquipPropio = 1
            End If
            Dim sql As String = "update REGISTRO set ADULTOS=" & nADULTOS & ",INFANTIL=" & nINFANTIL & ",TODOPUB = " & nTODOPUB & ",TALLERES=" & nTALLERES & ",ESCUELA=" & nESCUELA & ",SALA=" & nSALA &
                                 ",EQUIPPROPIO= " & nEquipPropio & " where CODIGO=" & ncodigo
            Dim Cmd As New SqlClient.SqlCommand(sql, cn)
            cn.Open()
            Try
                Cmd.ExecuteNonQuery()
            Catch ex As Exception
                txtErrorAcepto.Text = "Error al ingresar datos"
                Return False
            End Try
            cn.Close()

            dsInte.AceptaCambios(ds, ncodigo)

            'Dim Sqli As String = "update integrantes set verificado = null where codigoregistro=" & Session("CODIGO")
            'Dim cmdi As New SqlClient.SqlCommand(Sqli, cn)
            'cn.Open()
            'cmdi.ExecuteNonQuery()
            'cn.Close()

            Dim bGrabar As Boolean = False
            bGrabar = GrabarAdjuntos()
            If bGrabar = False Then
                ActualizarDatos = False
                Return False
            End If

        Catch ex As Exception
            ActualizarDatos = False
            Return False
            'Response.Redirect("ErrorPage.aspx?errMessage=" & ex.Message & "&errSource=" & ex.Source)
        Finally
            MyCommand.Dispose()
            MyConnection.Dispose()
        End Try

        ActualizarDatos = True
    End Function

    Protected Function GrabarAdjuntos() As Integer

        Dim nIdRegistro As Integer = Session("CodRegistro")

        BorraAdjuntos()

        'Guardar Lista de Equipamiento
        Dim woperador As String = Session("CUIT")
        Dim FileName As String = Path.GetFileName(FileUploadEquipa.PostedFile.FileName)
        Dim Extension As String = Path.GetExtension(FileUploadEquipa.PostedFile.FileName)
        Dim FolderPath As String = ConfigurationManager.AppSettings("FolderPath")
        Dim fileSavePath As Object = Server.MapPath("~/Documentos/REGISTRO/" & nIdRegistro & "/EQUIPAMIENTO")
        Try
            MkDir(fileSavePath)
        Catch ex As Exception
        End Try
        If FileUploadEquipa.HasFile Then
            Dim Filepath As String = fileSavePath + "\" + FileName
            Try
                FileUploadEquipa.SaveAs(Filepath)
            Catch ex As Exception
                txtErrorEquipamiento.Text = "No se pudo guardar Lista de equipamiento"
                Return False
            End Try
        Else
            If Session("FileUploadEquipaFileName") IsNot Nothing Then
                Dim UploadImporta1 As FileUpload = CType(Session("FileUploadEquipa1"), FileUpload)
                FileName = UploadImporta1.FileName
                If Len(RTrim(FileName)) = 0 Then
                    Dim Archivo As String = Session("FileUploadEquipaFileName")
                    Dim jm As Integer = Len(Archivo)
                    FileName = ""
                    While jm > 0
                        Dim letra As String = Mid(Archivo, jm, 1)
                        If letra <> "\" Then
                            FileName = letra + FileName
                        Else
                            jm = 0
                        End If
                        jm = jm - 1
                    End While
                End If
                Dim Filepath As String = Session("FileUploadEquipaFileName")
                Dim FilepathDest As String = fileSavePath + "\" + FileName
                Try
                    File.Copy(Filepath, FilepathDest)
                Catch ex As Exception
                    txtErrorEquipamiento.Text = "No se pudo guardar Lista de equipamiento"
                    Return False
                End Try
            End If
        End If

        'Guardar Lista de Equipamiento Adquirido
        FileName = Path.GetFileName(FileUploadEquipaSub.PostedFile.FileName)
        Extension = Path.GetExtension(FileUploadEquipaSub.PostedFile.FileName)
        FolderPath = ConfigurationManager.AppSettings("FolderPath")
        fileSavePath = Server.MapPath("~/Documentos/REGISTRO/" & nIdRegistro & "/EQUIPADQUIRIDO")
        Try
            MkDir(fileSavePath)
        Catch ex As Exception
        End Try
        If FileUploadEquipaSub.HasFile Then
            Dim Filepath As String = fileSavePath + "\" + FileName
            Try
                FileUploadEquipaSub.SaveAs(Filepath)
            Catch ex As Exception
                txtErrorEquipamiento.Text = "No se pudo guardar Lista de equipamiento Adquirido"
                Return False
            End Try
        Else
            If Session("FileUploadEquipaSubFileName") IsNot Nothing Then
                Dim UploadImporta1 As FileUpload = CType(Session("FileUploadEquipaSub1"), FileUpload)
                FileName = UploadImporta1.FileName
                If Len(RTrim(FileName)) = 0 Then
                    Dim Archivo As String = Session("FileUploadEquipaSubFileName")
                    Dim jm As Integer = Len(Archivo)
                    FileName = ""
                    While jm > 0
                        Dim letra As String = Mid(Archivo, jm, 1)
                        If letra <> "\" Then
                            FileName = letra + FileName
                        Else
                            jm = 0
                        End If
                        jm = jm - 1
                    End While
                End If
                Dim Filepath As String = Session("FileUploadEquipaSubFileName")
                Dim FilepathDest As String = fileSavePath + "\" + FileName
                Try
                    File.Copy(Filepath, FilepathDest)
                Catch ex As Exception
                    txtErrorEquipamiento.Text = "No se pudo guardar Lista de equipamiento Adquirido"
                    Return False
                End Try
            End If
        End If

        'Guardar Trayectoria
        FileName = Path.GetFileName(FileUploadTrayectoria.PostedFile.FileName)
        Extension = Path.GetExtension(FileUploadTrayectoria.PostedFile.FileName)
        FolderPath = ConfigurationManager.AppSettings("FolderPath")
        fileSavePath = Server.MapPath("~/Documentos/REGISTRO/" & nIdRegistro & "/TRAYECTORIA")
        Try
            MkDir(fileSavePath)
        Catch ex As Exception
        End Try
        If FileUploadTrayectoria.HasFile Then
            Dim Filepath As String = fileSavePath + "\" + FileName
            Try
                FileUploadTrayectoria.SaveAs(Filepath)
            Catch ex As Exception
                txtErrorTrayectoria.Text = "No se pudo guardar Trayectoria"
                Return False
            End Try
        Else
            If Session("FileUploadTrayectoriaFileName") IsNot Nothing Then
                Dim UploadImporta1 As FileUpload = CType(Session("FileUploadTrayectoria1"), FileUpload)
                FileName = UploadImporta1.FileName
                If Len(RTrim(FileName)) = 0 Then
                    Dim Archivo As String = Session("FileUploadTrayectoriaFileName")
                    Dim jm As Integer = Len(Archivo)
                    FileName = ""
                    While jm > 0
                        Dim letra As String = Mid(Archivo, jm, 1)
                        If letra <> "\" Then
                            FileName = letra + FileName
                        Else
                            jm = 0
                        End If
                        jm = jm - 1
                    End While
                End If
                Dim Filepath As String = Session("FileUploadTrayectoriaFileName")
                Dim FilepathDest As String = fileSavePath + "\" + FileName
                Try
                    File.Copy(Filepath, FilepathDest)
                Catch ex As Exception
                    txtErrorTrayectoria.Text = "No se pudo guardar Trayectoria"
                    Return False
                End Try
            End If
        End If
        Return True

    End Function

    Private Sub CargarDatos(ByVal nCodigo As Integer)
        'Const F_RESPONSABLE As Integer = 0
        Const F_SECTOR As Integer = 1
        Const F_PROVINCIA As Integer = 2
        Const F_DENOMINACION As Integer = 3
        Const F_LOCALIDAD As Integer = 4
        Const F_EMAIL As Integer = 5
        Const F_PAGINA As Integer = 6
        Const F_INICIO As Integer = 7
        Const F_EQUIPAMIENTO As Integer = 8
        Const F_ESPAPROP As Integer = 9
        Const F_COMESPACIO As Integer = 10
        'Const F_FECHALTA As Integer = 11
        Dim nADULTOS As Integer = 0
        Dim nINFANTIL As Integer = 0
        Dim nTODOPUB As Integer = 0
        Dim nTALLERES As Integer = 0
        Dim nESCUELA As Integer = 0
        Dim nSALA As Integer = 0
        Dim nEQUIPPROPIO As Integer = 0

        Dim sSQLCmd As String
        Dim MyConnection As SqlConnection
        Dim MyCommand As SqlCommand
        Dim MyReader As SqlDataReader

        Try
            'Load Registro
            sSQLCmd = "SELECT responsable, sector, provincia, denominacion, localidad, email, pagina, inicio, equipamiento, espaprop, comespacio, fechAlta, " &
                            "ADULTOS,INFANTIL,TODOPUB,TALLERES,ESCUELA,SALA,EQUIPPROPIO " &
                            " FROM Registro " &
                            " WHERE codigo = " & nCodigo.ToString

            MyConnection = New SqlConnection()
            MyConnection.ConnectionString = ConfigurationManager.ConnectionStrings("INTeatroDig").ConnectionString
            MyConnection.Open()
            MyCommand = New SqlCommand(sSQLCmd, MyConnection)
            MyReader = MyCommand.ExecuteReader()
            If MyReader.Read() Then
                Session.Add("SECTOR", MyReader.Item(F_SECTOR))
                Session.Add("PROVINCIA", MyReader.Item(F_PROVINCIA))
                nSector = Session("SECTOR")
                'Cargo Integrantes
                BorrarTemporal()
                CargarTemporal(nCodigo)
                Inicializar()
                ddlSectores.Text = MyReader.Item(F_SECTOR).ToString.Trim
                ddlProvincias.Text = MyReader.Item(F_PROVINCIA)
                txtDenominacion.Text = MyReader.Item(F_DENOMINACION).ToString.Trim
                ddlLocalidades.Text = MyReader.Item(F_LOCALIDAD)
                txtMail.Text = MyReader.Item(F_EMAIL).ToString.Trim
                txtWeb.Text = MyReader.Item(F_PAGINA).ToString.Trim
                TextDesde.Value = MyReader.Item(F_INICIO)
                txtEquipamiento.Text = MyReader.Item(F_EQUIPAMIENTO).ToString.Trim
                'CheckBoxEspacioTeatral.Checked = IIf(MyReader.Item(F_ESPACIO) = 1, True, False)
                If MyReader.Item(F_ESPAPROP) = 2 Then
                    radioSi.Checked = True
                Else
                    radioNo.Checked = True
                End If
                txtEspacio.Text = MyReader.Item(F_COMESPACIO)
                Try
                    nADULTOS = MyReader.GetInt32(12)
                Catch ex As Exception
                    nADULTOS = 0
                End Try
                Try
                    nINFANTIL = MyReader.GetInt32(13)
                Catch ex As Exception
                    nINFANTIL = 0
                End Try
                Try
                    nTODOPUB = MyReader.GetInt32(14)
                Catch ex As Exception
                    nTODOPUB = 0
                End Try
                Try
                    nTALLERES = MyReader.GetInt32(15)
                Catch ex As Exception
                    nTALLERES = 0
                End Try
                Try
                    nESCUELA = MyReader.GetInt32(16)
                Catch ex As Exception
                    nESCUELA = 0
                End Try
                Try
                    nSALA = MyReader.GetInt32(17)
                Catch ex As Exception
                    nSALA = 0
                End Try
                Try
                    nEQUIPPROPIO = MyReader.GetInt32(18)
                Catch ex As Exception
                    nEQUIPPROPIO = 0
                End Try
            End If
            MyCommand.Dispose()
            MyConnection.Dispose()

            If nADULTOS = 1 Then
                ChkAdultos.Checked = True
            End If
            If nINFANTIL = 1 Then
                ChkInfantiles.Checked = True
            End If
            If nTODOPUB = 1 Then
                ChkTodoPublico.Checked = True
            End If
            If nTALLERES = 1 Then
                ChkTalleres.Checked = True
            End If
            If nESCUELA = 1 Then
                ChkFormacion.Checked = True
            End If
            If nSALA = 1 Then
                ChkSala.Checked = True
            End If
            If nEQUIPPROPIO = 1 Then
                RadioButtonEquipa1.Checked = True
            End If

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
        sSQLCmd = "SELECT count(*) AS cantidad " &
                        "FROM IntegrantesTemp " &
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

    Private Sub GridView1_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles GridView1.RowDeleting
        Dim id As String = ""
        Try
            id = GridView1.DataKeys(e.RowIndex).Value
        Catch ex As Exception
            Return
        End Try
        Dim nCodigo As Integer = Session("CodRegistro")
        dsInte.DesvinculaIntegrante(ds, nCodigo, id)
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
        'If BtnGuardar.Text = "Confirmar Registro" Then
        UserMsgBox(Me, "Todos los campos en los cuales debe adjuntar documentación, son de llenado OPTATIVO. En otro momento, cuando haya reunido dicha documentación, podrá realizar la actualización en su opción de Datos Personales.")
        'End If
        CargAdjuntos()
    End Sub

    Protected Sub BtnVisualizaEquipa_Click(sender As Object, e As EventArgs) Handles BtnVisualizaEquipa.Click
        txtErrorEquipamiento.Text = ""
        If FileUploadEquipa.HasFile Or Session("FileUploadEquipa1") IsNot Nothing Then
            Dim woperador As String = Session("CUIT")
            Dim FileName As String = Path.GetFileName(FileUploadEquipa.PostedFile.FileName)
            Dim Extension As String = Path.GetExtension(FileUploadEquipa.PostedFile.FileName)
            Dim FolderPath As String = ConfigurationManager.AppSettings("FolderPath")
            Dim FilePath As String = ""
            If Session("FileUploadEquipa1") Is Nothing Then
                Dim wfecha As Date = DateTime.Now.ToString
                Dim wdia As Integer = wfecha.Day
                Dim wmes As Integer = wfecha.Month
                Dim wano As Integer = wfecha.Year
                Dim whora As Integer = wfecha.Hour
                Dim wminu As Integer = wfecha.Minute
                Dim wsegu As Integer = wfecha.Second
                Dim wdir As String = wano.ToString + wmes.ToString + wdia.ToString + whora.ToString + wminu.ToString + wsegu.ToString
                If UCase(Extension) <> ".PDF" And UCase(Extension) <> ".DOC" And UCase(Extension) <> ".DOCX" Then
                    txtErrorEquipamiento.Text = "No es un documento Adobe .PDF o Word .DOC .DOCX"
                    Return
                End If
                Dim randomName As String = RTrim(woperador) + wdir
                If Not Directory.Exists(Server.MapPath("~/Temp/" & randomName & "/")) Then
                    Directory.CreateDirectory(Server.MapPath("~/Temp/" & randomName & "/"))
                End If
                FilePath = (Server.MapPath("~/Temp/" & randomName & "/")) + FileName
                Try
                    FileUploadEquipa.SaveAs(FilePath)
                Catch ex As Exception
                End Try
                Session("FileUploadEquipa1") = FileUploadEquipa
                Session("FileUploadEquipaFileName") = FilePath
            Else
                FilePath = Session("FileUploadEquipaFileName")
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

    Protected Sub BtnVisualizaEquipaSub_Click(sender As Object, e As EventArgs) Handles BtnVisualizaEquipaSub.Click
        txtErrorEquipamiento.Text = ""
        If FileUploadEquipaSub.HasFile Or Session("FileUploadEquipaSub1") IsNot Nothing Then
            Dim woperador As String = Session("CUIT")
            Dim FileName As String = Path.GetFileName(FileUploadEquipaSub.PostedFile.FileName)
            Dim Extension As String = Path.GetExtension(FileUploadEquipaSub.PostedFile.FileName)
            Dim FolderPath As String = ConfigurationManager.AppSettings("FolderPath")
            Dim FilePath As String = ""
            If Session("FileUploadEquipaSub1") Is Nothing Then
                Dim wfecha As Date = DateTime.Now.ToString
                Dim wdia As Integer = wfecha.Day
                Dim wmes As Integer = wfecha.Month
                Dim wano As Integer = wfecha.Year
                Dim whora As Integer = wfecha.Hour
                Dim wminu As Integer = wfecha.Minute
                Dim wsegu As Integer = wfecha.Second
                Dim wdir As String = wano.ToString + wmes.ToString + wdia.ToString + whora.ToString + wminu.ToString + wsegu.ToString
                If UCase(Extension) <> ".PDF" And UCase(Extension) <> ".DOC" And UCase(Extension) <> ".DOCX" Then
                    txtErrorEquipamiento.Text = "No es un documento Adobe .PDF o Word .DOC .DOCX"
                    Return
                End If
                Dim randomName As String = RTrim(woperador) + wdir
                If Not Directory.Exists(Server.MapPath("~/Temp/" & randomName & "/")) Then
                    Directory.CreateDirectory(Server.MapPath("~/Temp/" & randomName & "/"))
                End If
                FilePath = (Server.MapPath("~/Temp/" & randomName & "/")) + FileName
                Try
                    FileUploadEquipaSub.SaveAs(FilePath)
                Catch ex As Exception
                End Try
                Session("FileUploadEquipaSub1") = FileUploadEquipaSub
                Session("FileUploadEquipaSubFileName") = FilePath
            Else
                FilePath = Session("FileUploadEquipaSubFileName")
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

    Protected Sub BtnVisualizaTrayectoria_Click(sender As Object, e As EventArgs) Handles BtnVisualizaTrayectoria.Click

        txtErrorEquipamiento.Text = ""
        If FileUploadTrayectoria.HasFile Or Session("FileUploadTrayectoria1") IsNot Nothing Then
            Dim woperador As String = Session("CUIT")
            Dim FileName As String = Path.GetFileName(FileUploadTrayectoria.PostedFile.FileName)
            Dim Extension As String = Path.GetExtension(FileUploadTrayectoria.PostedFile.FileName)
            Dim FolderPath As String = ConfigurationManager.AppSettings("FolderPath")
            Dim FilePath As String = ""
            If Session("FileUploadTrayectoria1") Is Nothing Then
                Dim wfecha As Date = DateTime.Now.ToString
                Dim wdia As Integer = wfecha.Day
                Dim wmes As Integer = wfecha.Month
                Dim wano As Integer = wfecha.Year
                Dim whora As Integer = wfecha.Hour
                Dim wminu As Integer = wfecha.Minute
                Dim wsegu As Integer = wfecha.Second
                Dim wdir As String = wano.ToString + wmes.ToString + wdia.ToString + whora.ToString + wminu.ToString + wsegu.ToString
                If UCase(Extension) <> ".PDF" And UCase(Extension) <> ".DOC" And UCase(Extension) <> ".DOCX" Then
                    txtErrorEquipamiento.Text = "No es un documento Adobe .PDF o Word .DOC .DOCX"
                    Return
                End If
                Dim randomName As String = RTrim(woperador) + wdir
                If Not Directory.Exists(Server.MapPath("~/Temp/" & randomName & "/")) Then
                    Directory.CreateDirectory(Server.MapPath("~/Temp/" & randomName & "/"))
                End If
                FilePath = (Server.MapPath("~/Temp/" & randomName & "/")) + FileName
                Try
                    FileUploadTrayectoria.SaveAs(FilePath)
                Catch ex As Exception
                End Try
                Session("FileUploadTrayectoria1") = FileUploadTrayectoria
                Session("FileUploadTrayectoriaFileName") = FilePath
            Else
                FilePath = Session("FileUploadTrayectoriaFileName")
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

    Private Sub BorraAdjuntos()
        Dim nCodigo As Integer = Session("CodRegistro")
        Dim sPath As String = ""
        Dim fileSavePath As String = Server.MapPath("~/Documentos/REGISTRO/" & nCodigo & "/EQUIPAMIENTO/")
        Dim wlong As Integer = Len(fileSavePath)
        Try
            Dim files() As String = IO.Directory.GetFiles(fileSavePath)
            For Each file As String In files
                sPath = file.ToString
                My.Computer.FileSystem.DeleteFile(sPath)
            Next
        Catch ex As Exception
        End Try

        sPath = ""
        fileSavePath = Server.MapPath("~/Documentos/REGISTRO/" & nCodigo & "/EQUIPADQUIRIDO/")
        wlong = Len(fileSavePath)
        Try
            Dim files() As String = IO.Directory.GetFiles(fileSavePath)
            For Each file As String In files
                sPath = file.ToString
                My.Computer.FileSystem.DeleteFile(sPath)
            Next
        Catch ex As Exception
        End Try

        sPath = ""
        fileSavePath = Server.MapPath("~/Documentos/REGISTRO/" & nCodigo & "/TRAYECTORIA/")
        wlong = Len(fileSavePath)
        Try
            Dim files() As String = IO.Directory.GetFiles(fileSavePath)
            For Each file As String In files
                sPath = file.ToString
                My.Computer.FileSystem.DeleteFile(sPath)
            Next
        Catch ex As Exception
        End Try

    End Sub

    Private Sub CargAdjuntos()
        Dim nCodigo As Integer = Session("CodRegistro")
        Dim wdenominacion As String = ""
        Dim wcantidad As Integer = 0
        Dim wequipo As Integer = 0
        Dim sql As String = "select denominacion,cantespacios,EQUIPPROPIO from REGISTRO where CODIGO=" & nCodigo
        cn.Open()
        Dim Psql As New SqlClient.SqlCommand(sql, cn)
        Dim dr As SqlClient.SqlDataReader = Psql.ExecuteReader
        While dr.Read
            wdenominacion = dr.GetString(0)
            Try
                wcantidad = dr.GetInt32(1)
            Catch ex As Exception
                wcantidad = 0
            End Try
            Try
                wequipo = dr.GetInt32(2)
            Catch ex As Exception
                wequipo = 0
            End Try
        End While
        dr.Close()
        cn.Close()
        If wequipo = 0 Then
            DivTablaEquipamiento.Visible = False
        End If
        Dim wfecha As Date = DateTime.Now.ToString
        Dim wdia As Integer = wfecha.Day
        Dim wmes As Integer = wfecha.Month
        Dim wano As Integer = wfecha.Year
        Dim whora As Integer = wfecha.Hour
        Dim wminu As Integer = wfecha.Minute
        Dim wsegu As Integer = wfecha.Second
        Dim woperador As String = Session("CUIL")
        Dim wdir As String = wano.ToString + wmes.ToString + wdia.ToString + whora.ToString + wminu.ToString + wsegu.ToString
        Dim randomName As String = RTrim(woperador) + wdir
        If Not Directory.Exists(Server.MapPath("~/Temp/" & randomName & "/")) Then
            Directory.CreateDirectory(Server.MapPath("~/Temp/" & randomName & "/"))
        End If

        Dim sDocumento As String = ""
        Dim sPath As String = ""
        Dim fileSavePath As String = Server.MapPath("~/Documentos/REGISTRO/" & nCodigo & "/EQUIPAMIENTO/")
        Dim wlong As Integer = Len(fileSavePath)
        Try
            Dim files() As String = IO.Directory.GetFiles(fileSavePath)
            For Each file As String In files
                sPath = file.ToString
                Dim wlong2 As Integer = Len(sPath)
                sDocumento = Mid(sPath, wlong + 1, wlong2 - wlong)
            Next
        Catch ex As Exception
            sDocumento = ""
        End Try
        If Len(RTrim(sDocumento)) > 0 Then
            Session("FileUploadEquipa1") = FileUploadEquipa
            Session("FileUploadEquipaFileName") = sPath
            Session("sDocumentoEquipa") = sDocumento
            LabelUploadEquipa.Text = sDocumento
            If Not Directory.Exists(Server.MapPath("~/Temp/" & randomName & "/")) Then
                Directory.CreateDirectory(Server.MapPath("~/Temp/" & randomName & "/"))
            End If
            Dim FilePathDest As String = (Server.MapPath("~/Temp/" & randomName & "/")) + sDocumento
            Try
                File.Copy(sPath, FilePathDest)
            Catch ex As Exception
                Return
            End Try
            Session("FileUploadEquipaFileName") = FilePathDest
        End If

        sDocumento = ""
        sPath = ""
        fileSavePath = Server.MapPath("~/Documentos/REGISTRO/" & nCodigo & "/EQUIPADQUIRIDO/")
        wlong = Len(fileSavePath)
        Try
            Dim files() As String = IO.Directory.GetFiles(fileSavePath)
            For Each file As String In files
                sPath = file.ToString
                Dim wlong2 As Integer = Len(sPath)
                sDocumento = Mid(sPath, wlong + 1, wlong2 - wlong)
            Next
        Catch ex As Exception
            sDocumento = ""
        End Try
        If Len(RTrim(sDocumento)) > 0 Then
            Session("FileUploadEquipaSub1") = FileUploadEquipaSub
            Session("FileUploadEquipaSubFileName") = sPath
            Session("sDocumentoEquipaSub") = sDocumento
            LabelUploadEquipaSub.Text = sDocumento
            randomName = randomName + "3"
            If Not Directory.Exists(Server.MapPath("~/Temp/" & randomName & "/")) Then
                Directory.CreateDirectory(Server.MapPath("~/Temp/" & randomName & "/"))
            End If
            Dim FilePathDest As String = (Server.MapPath("~/Temp/" & randomName & "/")) + sDocumento
            Try
                File.Copy(sPath, FilePathDest)
            Catch ex As Exception
                Return
            End Try
            Session("FileUploadEquipaSubFileName") = FilePathDest
        End If

        sDocumento = ""
        sPath = ""
        fileSavePath = Server.MapPath("~/Documentos/REGISTRO/" & nCodigo & "/TRAYECTORIA/")
        wlong = Len(fileSavePath)
        Try
            Dim files() As String = IO.Directory.GetFiles(fileSavePath)
            For Each file As String In files
                sPath = file.ToString
                Dim wlong2 As Integer = Len(sPath)
                sDocumento = Mid(sPath, wlong + 1, wlong2 - wlong)
            Next
        Catch ex As Exception
            sDocumento = ""
        End Try
        If Len(RTrim(sDocumento)) > 0 Then
            Session("FileUploadTrayectoria1") = FileUploadTrayectoria
            Session("FileUploadTrayectoriaFileName") = sPath
            Session("sDocumentoTrayectoria") = sDocumento
            LabelUploadTrayectoria.Text = sDocumento
            randomName = randomName + "4"
            If Not Directory.Exists(Server.MapPath("~/Temp/" & randomName & "/")) Then
                Directory.CreateDirectory(Server.MapPath("~/Temp/" & randomName & "/"))
            End If
            Dim FilePathDest As String = (Server.MapPath("~/Temp/" & randomName & "/")) + sDocumento
            Try
                File.Copy(sPath, FilePathDest)
            Catch ex As Exception
                Return
            End Try
            Session("FileUploadTrayectoriaFileName") = FilePathDest
        End If

    End Sub

End Class