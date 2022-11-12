Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Collections
Imports System.IO
Imports System.IO.Packaging
Partial Public Class registroTeatro
    Inherits System.Web.UI.Page
    Dim cn As New SqlClient.SqlConnection(SqlConex)
    Dim aDeletedCUIL As New ArrayList
    Dim quien As usuario
    Shared ds As dsIntegrantes
    Shared localidad As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim sAccion As String
        Dim nCodigo As Integer
        SetearVariablesSession()
        If Not ds Is Nothing Then
            GridView1.DataSource = ds.Integrantes
            GridView1.DataBind()
            GridView1.Visible = True
        End If
        'Inicializar()
        If Not Page.IsPostBack Then
            'La primera vez
            If User.Identity.IsAuthenticated Then
                sAccion = Request.QueryString("accion")
                nCodigo = Request.QueryString("codigo")
                Session("CodRegistro") = nCodigo
                Session("sAccion") = sAccion
                If sAccion = "M" Then
                    HyperLinkBack.Attributes.Add("href", "RegistroLista.aspx")
                End If
                If sAccion.ToUpper = "A" Then
                    Inicializar()
                    BorrarTemporal()
                    BtnGuardar.Text = "Confirmar Registro"
                ElseIf sAccion.ToUpper = "M" Then
                    'Inicializar()
                    CargarDatos(nCodigo)
                    BtnGuardar.Text = "Confirmar Actualización de Registro"
                    Session.Add("CODIGO", nCodigo)
                    'OcultarAdjuntos()
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
            MaintainScrollPositionOnPostBack = True
            'If BtnGuardar.Text = "Confirmar Registro" Then
            GuardarAdjuntos()
            GuardarEspacio1()
            GuardarEspacio2()
            GuardarEspacio3()
            GuardarEspacio4()
            'End If
        End If

    End Sub

    Private Sub OcultarAdjuntos()
        DivTablaPeritaje.Visible = False
        DivTablaEquipamiento.Visible = False
        PlanoEscena1.Visible = False
        PlanoEscena2.Visible = False
        PlanoEscena3.Visible = False
        PlanoEscena4.Visible = False
        PlantaLuz1.Visible = False
        PlantaLuz2.Visible = False
        PlantaLuz3.Visible = False
        PlantaLuz4.Visible = False
        FotoPlanoEscena1.Visible = False
        FotoPlanoEscena2.Visible = False
        FotoPlanoEscena3.Visible = False
        FotoPlanoEscena4.Visible = False
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
    End Sub

    Private Sub GuardarEspacio1()
        Dim wfecha As Date = DateTime.Now.ToString
        Dim wdia As Integer = wfecha.Day
        Dim wmes As Integer = wfecha.Month
        Dim wano As Integer = wfecha.Year
        Dim whora As Integer = wfecha.Hour
        Dim wminu As Integer = wfecha.Minute
        Dim woperador As String = Session("CUIL")
        If Session("FileUploadPlanoEscena11") Is Nothing Or FileUploadPlanoEscena1.HasFile Then
            If FileUploadPlanoEscena1.HasFile Then
                Dim FileName As String = Path.GetFileName(FileUploadPlanoEscena1.PostedFile.FileName)
                Dim Extension As String = Path.GetExtension(FileUploadPlanoEscena1.PostedFile.FileName)
                Dim wsegu As Integer = wfecha.Second
                Dim wdir As String = wano.ToString + wmes.ToString + wdia.ToString + whora.ToString + wminu.ToString + wsegu.ToString
                If UCase(Extension) = ".PDF" Or UCase(Extension) = ".JPG" Or UCase(Extension) = ".JPEG" Then
                    Dim randomName As String = RTrim(woperador) + wdir
                    If Not Directory.Exists(Server.MapPath("~/Temp/" & randomName & "/")) Then
                        Directory.CreateDirectory(Server.MapPath("~/Temp/" & randomName & "/"))
                    End If
                    Dim FilePath As String = (Server.MapPath("~/Temp/" & randomName & "/")) + FileName
                    Try
                        FileUploadPlanoEscena1.SaveAs(FilePath)
                    Catch ex As Exception
                    End Try
                    Session("FileUploadPlanoEscena11") = FileUploadPlanoEscena1
                    Session("FileUploadPlanoEscena1FileName") = FilePath
                    LabelPlanoEscena1.Text = FileUploadPlanoEscena1.FileName
                End If
            End If
        Else
            If Session("FileUploadPlanoEscena11") IsNot Nothing Then
                Try
                    Dim FileUploadPlanoEscena11 As FileUpload = CType(Session("FileUploadPlanoEscena11"), FileUpload)
                    LabelPlanoEscena1.Text = FileUploadPlanoEscena11.FileName
                Catch ex As Exception
                End Try
            End If
        End If

        If Session("FileUploadPlantaLuz11") Is Nothing Or FileUploadPlantaLuz1.HasFile Then
            If FileUploadPlantaLuz1.HasFile Then
                Dim FileName As String = Path.GetFileName(FileUploadPlantaLuz1.PostedFile.FileName)
                Dim Extension As String = Path.GetExtension(FileUploadPlantaLuz1.PostedFile.FileName)
                Dim wsegu As Integer = wfecha.Second
                Dim wdir As String = wano.ToString + wmes.ToString + wdia.ToString + whora.ToString + wminu.ToString + wsegu.ToString
                If UCase(Extension) = ".PDF" Or UCase(Extension) = ".JPG" Or UCase(Extension) = ".JPEG" Then
                    Dim randomName As String = RTrim(woperador) + wdir
                    If Not Directory.Exists(Server.MapPath("~/Temp/" & randomName & "/")) Then
                        Directory.CreateDirectory(Server.MapPath("~/Temp/" & randomName & "/"))
                    End If
                    Dim FilePath As String = (Server.MapPath("~/Temp/" & randomName & "/")) + FileName
                    Try
                        FileUploadPlantaLuz1.SaveAs(FilePath)
                    Catch ex As Exception
                    End Try
                    Session("FileUploadPlantaLuz11") = FileUploadPlantaLuz1
                    Session("FileUploadPlantaLuz1FileName") = FilePath
                    LabelPlantaLuz1.Text = FileUploadPlantaLuz1.FileName
                End If
            End If
        Else
            If Session("FileUploadPlantaLuz11") IsNot Nothing Then
                Try
                    Dim FileUploadPlantaLuz11 As FileUpload = CType(Session("FileUploadPlantaLuz11"), FileUpload)
                    LabelPlantaLuz1.Text = FileUploadPlantaLuz11.FileName
                Catch ex As Exception
                End Try
            End If
        End If

        If Session("FileUploadFotoEscena11") Is Nothing Or FileUploadFotoEscena1.HasFile Then
            If FileUploadFotoEscena1.HasFile Then
                Dim FileName As String = Path.GetFileName(FileUploadFotoEscena1.PostedFile.FileName)
                Dim Extension As String = Path.GetExtension(FileUploadFotoEscena1.PostedFile.FileName)
                Dim wsegu As Integer = wfecha.Second
                Dim wdir As String = wano.ToString + wmes.ToString + wdia.ToString + whora.ToString + wminu.ToString + wsegu.ToString
                If UCase(Extension) = ".JPG" Or UCase(Extension) = ".JPEG" Then
                    Dim randomName As String = RTrim(woperador) + wdir
                    If Not Directory.Exists(Server.MapPath("~/Temp/" & randomName & "/")) Then
                        Directory.CreateDirectory(Server.MapPath("~/Temp/" & randomName & "/"))
                    End If
                    Dim FilePath As String = (Server.MapPath("~/Temp/" & randomName & "/")) + FileName
                    Try
                        FileUploadFotoEscena1.SaveAs(FilePath)
                    Catch ex As Exception
                    End Try
                    Session("FileUploadFotoEscena11") = FileUploadFotoEscena1
                    Session("FileUploadFotoEscena1FileName") = FilePath
                    LabelFotoEscena1.Text = FileUploadFotoEscena1.FileName
                End If
            End If
        Else
            If Session("FileUploadFotoEscena11") IsNot Nothing Then
                Try
                    Dim FileUploadFotoEscena11 As FileUpload = CType(Session("FileUploadFotoEscena11"), FileUpload)
                    LabelFotoEscena1.Text = FileUploadFotoEscena11.FileName
                Catch ex As Exception
                End Try
            End If
        End If
    End Sub

    Private Sub GuardarEspacio2()
        Dim wfecha As Date = DateTime.Now.ToString
        Dim wdia As Integer = wfecha.Day
        Dim wmes As Integer = wfecha.Month
        Dim wano As Integer = wfecha.Year
        Dim whora As Integer = wfecha.Hour
        Dim wminu As Integer = wfecha.Minute
        Dim woperador As String = Session("CUIL")
        If Session("FileUploadPlanoEscena21") Is Nothing Or FileUploadPlanoEscena2.HasFile Then
            If FileUploadPlanoEscena2.HasFile Then
                Dim FileName As String = Path.GetFileName(FileUploadPlanoEscena2.PostedFile.FileName)
                Dim Extension As String = Path.GetExtension(FileUploadPlanoEscena2.PostedFile.FileName)
                Dim wsegu As Integer = wfecha.Second
                Dim wdir As String = wano.ToString + wmes.ToString + wdia.ToString + whora.ToString + wminu.ToString + wsegu.ToString
                If UCase(Extension) = ".PDF" Or UCase(Extension) = ".JPG" Or UCase(Extension) = ".JPEG" Then
                    Dim randomName As String = RTrim(woperador) + wdir
                    If Not Directory.Exists(Server.MapPath("~/Temp/" & randomName & "/")) Then
                        Directory.CreateDirectory(Server.MapPath("~/Temp/" & randomName & "/"))
                    End If
                    Dim FilePath As String = (Server.MapPath("~/Temp/" & randomName & "/")) + FileName
                    Try
                        FileUploadPlanoEscena2.SaveAs(FilePath)
                    Catch ex As Exception
                    End Try
                    Session("FileUploadPlanoEscena21") = FileUploadPlanoEscena2
                    Session("FileUploadPlanoEscena2FileName") = FilePath
                    LabelPlanoEscena2.Text = FileUploadPlanoEscena2.FileName
                End If
            End If
        Else
            If Session("FileUploadPlanoEscena21") IsNot Nothing Then
                Try
                    Dim FileUploadPlanoEscena21 As FileUpload = CType(Session("FileUploadPlanoEscena21"), FileUpload)
                    LabelPlanoEscena2.Text = FileUploadPlanoEscena21.FileName
                Catch ex As Exception
                End Try
            End If
        End If

        If Session("FileUploadPlantaLuz21") Is Nothing Or FileUploadPlantaLuz2.HasFile Then
            If FileUploadPlantaLuz2.HasFile Then
                Dim FileName As String = Path.GetFileName(FileUploadPlantaLuz2.PostedFile.FileName)
                Dim Extension As String = Path.GetExtension(FileUploadPlantaLuz2.PostedFile.FileName)
                Dim wsegu As Integer = wfecha.Second
                Dim wdir As String = wano.ToString + wmes.ToString + wdia.ToString + whora.ToString + wminu.ToString + wsegu.ToString
                If UCase(Extension) = ".PDF" Or UCase(Extension) = ".JPG" Or UCase(Extension) = ".JPEG" Then
                    Dim randomName As String = RTrim(woperador) + wdir
                    If Not Directory.Exists(Server.MapPath("~/Temp/" & randomName & "/")) Then
                        Directory.CreateDirectory(Server.MapPath("~/Temp/" & randomName & "/"))
                    End If
                    Dim FilePath As String = (Server.MapPath("~/Temp/" & randomName & "/")) + FileName
                    Try
                        FileUploadPlantaLuz2.SaveAs(FilePath)
                    Catch ex As Exception
                    End Try
                    Session("FileUploadPlantaLuz21") = FileUploadPlantaLuz2
                    Session("FileUploadPlantaLuz2FileName") = FilePath
                    LabelPlantaLuz2.Text = FileUploadPlantaLuz2.FileName
                End If
            End If
        Else
            If Session("FileUploadPlantaLuz21") IsNot Nothing Then
                Try
                    Dim FileUploadPlantaLuz21 As FileUpload = CType(Session("FileUploadPlantaLuz21"), FileUpload)
                    LabelPlantaLuz2.Text = FileUploadPlantaLuz21.FileName
                Catch ex As Exception
                End Try
            End If
        End If

        If Session("FileUploadFotoEscena21") Is Nothing Or FileUploadFotoEscena2.HasFile Then
            If FileUploadFotoEscena2.HasFile Then
                Dim FileName As String = Path.GetFileName(FileUploadFotoEscena2.PostedFile.FileName)
                Dim Extension As String = Path.GetExtension(FileUploadFotoEscena2.PostedFile.FileName)
                Dim wsegu As Integer = wfecha.Second
                Dim wdir As String = wano.ToString + wmes.ToString + wdia.ToString + whora.ToString + wminu.ToString + wsegu.ToString
                If UCase(Extension) = ".JPG" Or UCase(Extension) = ".JPEG" Then
                    Dim randomName As String = RTrim(woperador) + wdir
                    If Not Directory.Exists(Server.MapPath("~/Temp/" & randomName & "/")) Then
                        Directory.CreateDirectory(Server.MapPath("~/Temp/" & randomName & "/"))
                    End If
                    Dim FilePath As String = (Server.MapPath("~/Temp/" & randomName & "/")) + FileName
                    Try
                        FileUploadFotoEscena2.SaveAs(FilePath)
                    Catch ex As Exception
                    End Try
                    Session("FileUploadFotoEscena21") = FileUploadFotoEscena2
                    Session("FileUploadFotoEscena2FileName") = FilePath
                    LabelFotoEscena2.Text = FileUploadFotoEscena2.FileName
                End If
            End If
        Else
            If Session("FileUploadFotoEscena21") IsNot Nothing Then
                Try
                    Dim FileUploadFotoEscena21 As FileUpload = CType(Session("FileUploadFotoEscena21"), FileUpload)
                    LabelFotoEscena2.Text = FileUploadFotoEscena21.FileName
                Catch ex As Exception
                End Try
            End If
        End If
    End Sub

    Private Sub GuardarEspacio3()
        Dim wfecha As Date = DateTime.Now.ToString
        Dim wdia As Integer = wfecha.Day
        Dim wmes As Integer = wfecha.Month
        Dim wano As Integer = wfecha.Year
        Dim whora As Integer = wfecha.Hour
        Dim wminu As Integer = wfecha.Minute
        Dim woperador As String = Session("CUIL")
        If Session("FileUploadPlanoEscena31") Is Nothing Or FileUploadPlanoEscena3.HasFile Then
            If FileUploadPlanoEscena3.HasFile Then
                Dim FileName As String = Path.GetFileName(FileUploadPlanoEscena3.PostedFile.FileName)
                Dim Extension As String = Path.GetExtension(FileUploadPlanoEscena3.PostedFile.FileName)
                Dim wsegu As Integer = wfecha.Second
                Dim wdir As String = wano.ToString + wmes.ToString + wdia.ToString + whora.ToString + wminu.ToString + wsegu.ToString
                If UCase(Extension) = ".PDF" Or UCase(Extension) = ".JPG" Or UCase(Extension) = ".JPEG" Then
                    Dim randomName As String = RTrim(woperador) + wdir
                    If Not Directory.Exists(Server.MapPath("~/Temp/" & randomName & "/")) Then
                        Directory.CreateDirectory(Server.MapPath("~/Temp/" & randomName & "/"))
                    End If
                    Dim FilePath As String = (Server.MapPath("~/Temp/" & randomName & "/")) + FileName
                    Try
                        FileUploadPlanoEscena3.SaveAs(FilePath)
                    Catch ex As Exception
                    End Try
                    Session("FileUploadPlanoEscena31") = FileUploadPlanoEscena3
                    Session("FileUploadPlanoEscena3FileName") = FilePath
                    LabelPlanoEscena3.Text = FileUploadPlanoEscena3.FileName
                End If
            End If
        Else
            If Session("FileUploadPlanoEscena31") IsNot Nothing Then
                Try
                    Dim FileUploadPlanoEscena31 As FileUpload = CType(Session("FileUploadPlanoEscena31"), FileUpload)
                    LabelPlanoEscena3.Text = FileUploadPlanoEscena31.FileName
                Catch ex As Exception
                End Try
            End If
        End If

        If Session("FileUploadPlantaLuz31") Is Nothing Or FileUploadPlantaLuz3.HasFile Then
            If FileUploadPlantaLuz3.HasFile Then
                Dim FileName As String = Path.GetFileName(FileUploadPlantaLuz3.PostedFile.FileName)
                Dim Extension As String = Path.GetExtension(FileUploadPlantaLuz3.PostedFile.FileName)
                Dim wsegu As Integer = wfecha.Second
                Dim wdir As String = wano.ToString + wmes.ToString + wdia.ToString + whora.ToString + wminu.ToString + wsegu.ToString
                If UCase(Extension) = ".PDF" Or UCase(Extension) = ".JPG" Or UCase(Extension) = ".JPEG" Then
                    Dim randomName As String = RTrim(woperador) + wdir
                    If Not Directory.Exists(Server.MapPath("~/Temp/" & randomName & "/")) Then
                        Directory.CreateDirectory(Server.MapPath("~/Temp/" & randomName & "/"))
                    End If
                    Dim FilePath As String = (Server.MapPath("~/Temp/" & randomName & "/")) + FileName
                    Try
                        FileUploadPlantaLuz3.SaveAs(FilePath)
                    Catch ex As Exception
                    End Try
                    Session("FileUploadPlantaLuz31") = FileUploadPlantaLuz3
                    Session("FileUploadPlantaLuz3FileName") = FilePath
                    LabelPlantaLuz3.Text = FileUploadPlantaLuz3.FileName
                End If
            End If
        Else
            If Session("FileUploadPlantaLuz31") IsNot Nothing Then
                Try
                    Dim FileUploadPlantaLuz31 As FileUpload = CType(Session("FileUploadPlantaLuz31"), FileUpload)
                    LabelPlantaLuz3.Text = FileUploadPlantaLuz31.FileName
                Catch ex As Exception
                End Try
            End If
        End If

        If Session("FileUploadFotoEscena31") Is Nothing Or FileUploadFotoEscena3.HasFile Then
            If FileUploadFotoEscena3.HasFile Then
                Dim FileName As String = Path.GetFileName(FileUploadFotoEscena3.PostedFile.FileName)
                Dim Extension As String = Path.GetExtension(FileUploadFotoEscena3.PostedFile.FileName)
                Dim wsegu As Integer = wfecha.Second
                Dim wdir As String = wano.ToString + wmes.ToString + wdia.ToString + whora.ToString + wminu.ToString + wsegu.ToString
                If UCase(Extension) = ".JPG" Or UCase(Extension) = ".JPEG" Then
                    Dim randomName As String = RTrim(woperador) + wdir
                    If Not Directory.Exists(Server.MapPath("~/Temp/" & randomName & "/")) Then
                        Directory.CreateDirectory(Server.MapPath("~/Temp/" & randomName & "/"))
                    End If
                    Dim FilePath As String = (Server.MapPath("~/Temp/" & randomName & "/")) + FileName
                    Try
                        FileUploadFotoEscena3.SaveAs(FilePath)
                    Catch ex As Exception
                    End Try
                    Session("FileUploadFotoEscena31") = FileUploadFotoEscena3
                    Session("FileUploadFotoEscena3FileName") = FilePath
                    LabelFotoEscena3.Text = FileUploadFotoEscena3.FileName
                End If
            End If
        Else
            If Session("FileUploadFotoEscena31") IsNot Nothing Then
                Try
                    Dim FileUploadFotoEscena31 As FileUpload = CType(Session("FileUploadFotoEscena31"), FileUpload)
                    LabelFotoEscena3.Text = FileUploadFotoEscena31.FileName
                Catch ex As Exception
                End Try
            End If
        End If

    End Sub

    Private Sub GuardarEspacio4()
        Dim wfecha As Date = DateTime.Now.ToString
        Dim wdia As Integer = wfecha.Day
        Dim wmes As Integer = wfecha.Month
        Dim wano As Integer = wfecha.Year
        Dim whora As Integer = wfecha.Hour
        Dim wminu As Integer = wfecha.Minute
        Dim woperador As String = Session("CUIL")
        If Session("FileUploadPlanoEscena41") Is Nothing Or FileUploadPlanoEscena4.HasFile Then
            If FileUploadPlanoEscena4.HasFile Then
                Dim FileName As String = Path.GetFileName(FileUploadPlanoEscena4.PostedFile.FileName)
                Dim Extension As String = Path.GetExtension(FileUploadPlanoEscena4.PostedFile.FileName)
                Dim wsegu As Integer = wfecha.Second
                Dim wdir As String = wano.ToString + wmes.ToString + wdia.ToString + whora.ToString + wminu.ToString + wsegu.ToString
                If UCase(Extension) = ".PDF" Or UCase(Extension) = ".JPG" Or UCase(Extension) = ".JPEG" Then
                    Dim randomName As String = RTrim(woperador) + wdir
                    If Not Directory.Exists(Server.MapPath("~/Temp/" & randomName & "/")) Then
                        Directory.CreateDirectory(Server.MapPath("~/Temp/" & randomName & "/"))
                    End If
                    Dim FilePath As String = (Server.MapPath("~/Temp/" & randomName & "/")) + FileName
                    Try
                        FileUploadPlanoEscena4.SaveAs(FilePath)
                    Catch ex As Exception
                    End Try
                    Session("FileUploadPlanoEscena41") = FileUploadPlanoEscena4
                    Session("FileUploadPlanoEscena4FileName") = FilePath
                    LabelPlanoEscena4.Text = FileUploadPlanoEscena4.FileName
                End If
            End If
        Else
            If Session("FileUploadPlanoEscena41") IsNot Nothing Then
                Try
                    Dim FileUploadPlanoEscena41 As FileUpload = CType(Session("FileUploadPlanoEscena41"), FileUpload)
                    LabelPlanoEscena4.Text = FileUploadPlanoEscena41.FileName
                Catch ex As Exception
                End Try
            End If
        End If

        If Session("FileUploadPlantaLuz41") Is Nothing Or FileUploadPlantaLuz4.HasFile Then
            If FileUploadPlantaLuz4.HasFile Then
                Dim FileName As String = Path.GetFileName(FileUploadPlantaLuz4.PostedFile.FileName)
                Dim Extension As String = Path.GetExtension(FileUploadPlantaLuz4.PostedFile.FileName)
                Dim wsegu As Integer = wfecha.Second
                Dim wdir As String = wano.ToString + wmes.ToString + wdia.ToString + whora.ToString + wminu.ToString + wsegu.ToString
                If UCase(Extension) = ".PDF" Or UCase(Extension) = ".JPG" Or UCase(Extension) = ".JPEG" Then
                    Dim randomName As String = RTrim(woperador) + wdir
                    If Not Directory.Exists(Server.MapPath("~/Temp/" & randomName & "/")) Then
                        Directory.CreateDirectory(Server.MapPath("~/Temp/" & randomName & "/"))
                    End If
                    Dim FilePath As String = (Server.MapPath("~/Temp/" & randomName & "/")) + FileName
                    Try
                        FileUploadPlantaLuz4.SaveAs(FilePath)
                    Catch ex As Exception
                    End Try
                    Session("FileUploadPlantaLuz41") = FileUploadPlantaLuz4
                    Session("FileUploadPlantaLuz4FileName") = FilePath
                    LabelPlantaLuz4.Text = FileUploadPlantaLuz4.FileName
                End If
            End If
        Else
            If Session("FileUploadPlantaLuz41") IsNot Nothing Then
                Try
                    Dim FileUploadPlantaLuz41 As FileUpload = CType(Session("FileUploadPlantaLuz41"), FileUpload)
                    LabelPlantaLuz4.Text = FileUploadPlantaLuz41.FileName
                Catch ex As Exception
                End Try
            End If
        End If

        If Session("FileUploadFotoEscena41") Is Nothing Or FileUploadFotoEscena4.HasFile Then
            If FileUploadFotoEscena4.HasFile Then
                Dim FileName As String = Path.GetFileName(FileUploadFotoEscena4.PostedFile.FileName)
                Dim Extension As String = Path.GetExtension(FileUploadFotoEscena4.PostedFile.FileName)
                Dim wsegu As Integer = wfecha.Second
                Dim wdir As String = wano.ToString + wmes.ToString + wdia.ToString + whora.ToString + wminu.ToString + wsegu.ToString
                If UCase(Extension) = ".JPG" Or UCase(Extension) = ".JPEG" Then
                    Dim randomName As String = RTrim(woperador) + wdir
                    If Not Directory.Exists(Server.MapPath("~/Temp/" & randomName & "/")) Then
                        Directory.CreateDirectory(Server.MapPath("~/Temp/" & randomName & "/"))
                    End If
                    Dim FilePath As String = (Server.MapPath("~/Temp/" & randomName & "/")) + FileName
                    Try
                        FileUploadFotoEscena4.SaveAs(FilePath)
                    Catch ex As Exception
                    End Try
                    Session("FileUploadFotoEscena41") = FileUploadFotoEscena4
                    Session("FileUploadFotoEscena4FileName") = FilePath
                    LabelFotoEscena4.Text = FileUploadFotoEscena4.FileName
                End If
            End If
        Else
            If Session("FileUploadFotoEscena41") IsNot Nothing Then
                Try
                    Dim FileUploadFotoEscena41 As FileUpload = CType(Session("FileUploadFotoEscena41"), FileUpload)
                    LabelFotoEscena4.Text = FileUploadFotoEscena41.FileName
                Catch ex As Exception
                End Try
            End If
        End If

    End Sub

    Private Sub SetearVariablesSession()
        quien = CType(Session("usuario"), usuario)
        Session("USER_ID") = quien.Codigo
        Session("PROVINCIA") = quien.codprovin
        Session("CUIT") = quien.Usuario
        Session("SECTOR") = 1
        'Session("CODIGO") = quien.Codigo
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

        'Siuacion de uso
        cn.Open()
        Dim sql4 As String = "select 0 as codigo,' Seleccione Situación de Uso' as descrip union select codigo,descrip from Espacio order by codigo"
        Dim Psql4 As New SqlClient.SqlCommand(sql4, cn)
        Dim dr4 As SqlClient.SqlDataReader = Psql4.ExecuteReader
        ddlEspacios.DataSource = dr4
        ddlEspacios.DataTextField = "descrip"
        ddlEspacios.DataValueField = "codigo"
        ddlEspacios.DataBind()
        cn.Close()
        dr4.Close()

        'Documentacion
        cn.Open()
        Dim sql5 As String = "select 0 as codigo,' Seleccione Documentación' as descrip union select codigo,descrip from TipoDocum order by descrip"
        Dim Psql5 As New SqlClient.SqlCommand(sql5, cn)
        Dim dr5 As SqlClient.SqlDataReader = Psql5.ExecuteReader
        DdlDocumentacion.DataSource = dr5
        DdlDocumentacion.DataTextField = "descrip"
        DdlDocumentacion.DataValueField = "codigo"
        DdlDocumentacion.DataBind()
        cn.Close()
        dr5.Close()

        'Tipo de Sala
        cn.Open()
        Dim sql6 As String = "select 0 as codigo,' Seleccione tipo de Sala' as descrip union select codigo,descrip from TipoSala order by descrip"
        Dim Psql6 As New SqlClient.SqlCommand(sql6, cn)
        Dim dr6 As SqlClient.SqlDataReader = Psql6.ExecuteReader
        DdlTipoSala1.DataSource = dr6
        DdlTipoSala1.DataTextField = "descrip"
        DdlTipoSala1.DataValueField = "codigo"
        DdlTipoSala1.DataBind()
        cn.Close()
        dr6.Close()

        cn.Open()
        Dim sql61 As String = "select 0 as codigo,' Seleccione tipo de Sala' as descrip union select codigo,descrip from TipoSala order by descrip"
        Dim Psql61 As New SqlClient.SqlCommand(sql61, cn)
        Dim dr61 As SqlClient.SqlDataReader = Psql61.ExecuteReader
        DdlTipoSala2.DataSource = dr61
        DdlTipoSala2.DataTextField = "descrip"
        DdlTipoSala2.DataValueField = "codigo"
        DdlTipoSala2.DataBind()
        cn.Close()
        dr61.Close()

        cn.Open()
        Dim sql62 As String = "select 0 as codigo,' Seleccione tipo de Sala' as descrip union select codigo,descrip from TipoSala order by descrip"
        Dim Psql62 As New SqlClient.SqlCommand(sql62, cn)
        Dim dr62 As SqlClient.SqlDataReader = Psql62.ExecuteReader
        DdlTipoSala3.DataSource = dr62
        DdlTipoSala3.DataTextField = "descrip"
        DdlTipoSala3.DataValueField = "codigo"
        DdlTipoSala3.DataBind()
        cn.Close()
        dr62.Close()

        cn.Open()
        Dim sql63 As String = "select 0 as codigo,' Seleccione tipo de Sala' as descrip union select codigo,descrip from TipoSala order by descrip"
        Dim Psql63 As New SqlClient.SqlCommand(sql63, cn)
        Dim dr63 As SqlClient.SqlDataReader = Psql63.ExecuteReader
        DdlTipoSala4.DataSource = dr63
        DdlTipoSala4.DataTextField = "descrip"
        DdlTipoSala4.DataValueField = "codigo"
        DdlTipoSala4.DataBind()
        cn.Close()
        dr63.Close()

        'Cantidad de Espacios
        cn.Open()
        Dim sql7 As String = "select 0 as codigo,'Seleccione Cantidad' as descrip union select 1 as codigo,'1 Espacio' as descrip " &
             " union select 2 as codigo,'2 Espacios' as descrip union select 3 as codigo,'3 Espacios' as descrip union select 4 as codigo,'4 Espacios' as descrip"
        Dim Psql7 As New SqlClient.SqlCommand(sql7, cn)
        Dim dr7 As SqlClient.SqlDataReader = Psql7.ExecuteReader
        DdlCantidadEspacios.DataSource = dr7
        DdlCantidadEspacios.DataTextField = "descrip"
        DdlCantidadEspacios.DataValueField = "codigo"
        DdlCantidadEspacios.DataBind()
        cn.Close()
        dr7.Close()

        'Tipo de Asientos
        cn.Open()
        Dim sql8 As String = "select 0 as codigo,' Seleccione Tipo de Asiento' as descrip union select codigo,descrip from TipoAsien order by codigo"
        Dim Psql8 As New SqlClient.SqlCommand(sql8, cn)
        Dim dr8 As SqlClient.SqlDataReader = Psql8.ExecuteReader
        ddlTipoasien1.DataSource = dr8
        ddlTipoasien1.DataTextField = "descrip"
        ddlTipoasien1.DataValueField = "codigo"
        ddlTipoasien1.DataBind()
        cn.Close()
        dr8.Close()

        cn.Open()
        Dim sql81 As String = "select 0 as codigo,' Seleccione Tipo de Asiento' as descrip union select codigo,descrip from TipoAsien order by codigo"
        Dim Psql81 As New SqlClient.SqlCommand(sql81, cn)
        Dim dr81 As SqlClient.SqlDataReader = Psql81.ExecuteReader
        ddlTipoasien2.DataSource = dr81
        ddlTipoasien2.DataTextField = "descrip"
        ddlTipoasien2.DataValueField = "codigo"
        ddlTipoasien2.DataBind()
        cn.Close()
        dr81.Close()

        cn.Open()
        Dim sql82 As String = "select 0 as codigo,' Seleccione Tipo de Asiento' as descrip union select codigo,descrip from TipoAsien order by codigo"
        Dim Psql82 As New SqlClient.SqlCommand(sql82, cn)
        Dim dr82 As SqlClient.SqlDataReader = Psql82.ExecuteReader
        ddlTipoasien3.DataSource = dr82
        ddlTipoasien3.DataTextField = "descrip"
        ddlTipoasien3.DataValueField = "codigo"
        ddlTipoasien3.DataBind()
        cn.Close()
        dr82.Close()

        cn.Open()
        Dim sql83 As String = "select 0 as codigo,' Seleccione Tipo de Asiento' as descrip union select codigo,descrip from TipoAsien order by codigo"
        Dim Psql83 As New SqlClient.SqlCommand(sql83, cn)
        Dim dr83 As SqlClient.SqlDataReader = Psql83.ExecuteReader
        ddlTipoasien4.DataSource = dr83
        ddlTipoasien4.DataTextField = "descrip"
        ddlTipoasien4.DataValueField = "codigo"
        ddlTipoasien4.DataBind()
        cn.Close()
        dr83.Close()

        CreateDatatables()
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
                        "WHERE codigoRegistro = " & Session("USER_ID") & " and fechaBaja is null"
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
                sSubject = "INTeatroDigital - Solicitud de Registro de Sala o Espacio teatral"
                sBody = "Estimada/o usuaria/o de INTeatroDigital:" & "<br />" & "<br />"
                sBody += "Se ha recepcionado su gestión de: REGISTRO DE SALA O ESPACIO TEATRAL" & "<br />"
            Else
                nTipoMail = MAIL_MODIF_REGISTRO
                sSubject = "INTeatroDigital - Actualización de Registro de Sala o Espacio teatral"
                sBody = "Estimada/o usuaria/o de INTeatroDigital:" & "<br />" & "<br />"
                sBody += "Se ha recepcionado su gestión de: ACTUALIZACION DE REGISTRO DE SALA O ESPACIO TEATRAL " & "<br />"
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
            sBody += "Localidad: " & ddlLocalidades.SelectedItem.Text & "<br />"
            sBody += "Código postal: " & txtCP.Text & "<br />"
            sBody += "Domicilio: " & txtDomicilio.Text & "<br />"
            sBody += "Fecha de Inauguración de la Sala: " & TextDesde.Value & "<br />"
            'sBody += "Cuenta con la documentación pertinente vigente para funcionar como sala o espacio teatral: " & DdlDocumentacion.SelectedItem.Text & "<br />"
            sBody += "<br />"

            sBody += "Lista de Integrantes" & "<br />"
            sBody += RegistroModulo.GetIntegrantes(sIdRegistro, True)
            sBody += "<br />"

            If BtnGuardar.Text = "Confirmar Registro" Then
                sBody += "Usted ha realizado el trámite de Registro de SALA en INTeatroDigital. Estamos " & "<br />"
            Else
                sBody += "Usted ha realizado el trámite de Actualización de Registro de SALA en INTeatroDigital. Estamos " & "<br />"
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
                sSubject = "INTeatroDigital - Vinculación a Registro de Sala o Espacio Teatral"
                sBody = "Estimado usuario de INTeatroDigital: " & "<br />"
                sBody += "Usted ha sido incorporado como integrante de " & RTrim(txtDenominacion.Text) & " en el Registro Nacional del Teatro Independiente. " & "<br />"
                sBody += "A partir de este momento, para poder 'validar' su vinculación a dicha Sala o Espacio Teatral, deberá ingresar a la plataforma de " & "<br />"
                sBody += "INTeatroDigital, y en la sección 'Mis Vinculaciones' clickear en 'Confirmar Vinculación'. " & "<br />"
                sBody += "<br />"
            Else
                sSubject = "INTeatroDigital - Actualización de Registro Vinculado"
                sBody = "Estimado usuario de INTeatroDigital: " & "<br />"
                sBody += "Se ha procesado satisfactoriamente la solicitud de Actualización del Registro de Registro de Sala o Espacio Teatral " & RTrim(txtDenominacion.Text) & "<br />"
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
                sSubject = "INTeatroDigital - Solicitud de Registro de Sala o Espacio Teatral"
                sBody = "REGISTRO de SALA: " & txtDenominacion.Text.Trim & "<br />"
            Else
                sSubject = "INTeatroDigital - Actualización de Registro de Sala o Espacio Teatral"
                sBody = "ACTUALIZACION DE REGISTRO de SALA: " & txtDenominacion.Text.Trim & "<br />"
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
            sBody += "Localidad: " & ddlLocalidades.SelectedItem.Text & "<br />"
            sBody += "Código postal: " & txtCP.Text & "<br />"
            sBody += "Domicilio: " & txtDomicilio.Text & "<br />"
            sBody += "Fecha de Inicio de actividades: " & TextDesde.Value & "<br />"
            sBody += "Cuenta con la documentación pertinente vigente para funcionar como sala o espacio teatral: " & DdlDocumentacion.SelectedItem.Text & "<br />"
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

        Dim nLocalidades As Integer
        Dim dFecha As Date

        'Limpio los errores
        txtErrorLocalidades.Text = ""
        txtErrorCantidadLocalidades.Text = ""
        txtErrorComentariosEspacio.Text = ""
        txtErrorEspacioEscenico.Text = ""
        txtErrorEquipamiento.Text = ""
        txtErrorAcepto.Text = ""
        txtErrorFechaInicio.Text = ""
        txtErrorFechaGestion.Text = ""
        txtErrorAnterior.Text = ""
        TxtErrorEspacios.Text = ""
        txtErrorDocumentacion.Text = ""
        txtErrorEquipamiento.Text = ""
        LblErrorEspacio1.Text = ""
        LblErrorEspacio2.Text = ""
        LblErrorEspacio3.Text = ""
        LblErrorEspacio4.Text = ""

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
        Try
            dFecha = CDate(TextGestion.Value)
        Catch ex As Exception
            txtErrorFechaGestion.Text = "Fecha inválida"
            TextGestion.Focus()
            Return False
        End Try
        Try
            If Not ValidarFecha(TextGestion.Value.Trim) Then
                txtErrorFechaGestion.Text = "Fecha inválida"
                TextGestion.Focus()
                Return False
            End If
        Catch ex As Exception
            txtErrorFechaGestion.Text = "Fecha inválida"
            TextGestion.Focus()
            Return False
        End Try
        If ddlLocalidades.SelectedValue = 0 Then
            txtErrorLocalidades.Text = "Debe seleccionar una localidad"
            ddlLocalidades.Focus()
            Return False
        End If
        If RadioButtonEdificio1.Checked = False And RadioButtonEdificio2.Checked = False Then
            txtErrorAnterior.Text = "Debe informar Funcionamiento Anterior"
            RadioButtonEdificio1.Focus()
            Return False
        End If
        Dim waniomudanza As Integer = 0
        Try
            waniomudanza = CInt(TextBoxAnioMudanza.Text)
        Catch ex As Exception
            waniomudanza = 0
        End Try
        If RadioButtonEdificio1.Checked = True And (waniomudanza = 0 Or Len(TextBoxDomiAnterior.Text) = 0) Then
            txtErrorAnterior.Text = "Debe cargar datos del Funcionamiento Anterior"
            RadioButtonEdificio1.Focus()
            Return False
        End If
        If ddlEspacios.SelectedValue = 0 Then
            TxtErrorEspacios.Text = "Debe selecionar Situación de uso"
            ddlEspacios.Focus()
            Return False
        End If
        If DdlDocumentacion.SelectedValue = 0 Then
            txtErrorDocumentacion.Text = "seleccione tipo de Documentación"
            DdlDocumentacion.Focus()
            Return False
        End If
        If RadioButtonEquipa1.Checked = False And RadioButtonEquipa2.Checked = False Then
            txtErrorEquipamiento.Text = "Debe informar sobre equipamiento"
            RadioButtonEquipa1.Focus()
            Return False
        End If
        If Funciones.CaracteresEspecialesnumeros(txtDenominacion.Text.Trim) Then
            txtErrorAcepto.Text = "La denominación contiene caracteres especiales"
            txtDenominacion.Focus()
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
                        txtErrorEquipamiento.Text = "El Listado de Equipamiento tiene un tamaño superior a 10 MB"
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
                            txtErrorEquipamiento.Text = "El Listado de Equipamiento tiene un tamaño superior a 10 MB"
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
                    txtErrorEquipamiento.Text = "El Equipamiento adquirido no es un documento Adobe .PDF o Word .DOC .DOCX"
                    txtErrorEquipamiento.Focus()
                    Return False
                Else
                    fcv = 1
                End If
                Dim sizeInBytes As Long = FileUploadEquipaSub.PostedFile.ContentLength
                If sizeInBytes / 1000000 > 10 Then
                    txtErrorEquipamiento.Text = "El Listado de Equipamiento Adquirido tiene un tamaño superior a 10 MB"
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
                            txtErrorEquipamiento.Text = "El Listado de Equipamiento Adquirido tiene un tamaño superior a 10 MB"
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
        If Not ValidarEspacios() Then
            Return False
        End If
        'End If
        Dim wespacios As Integer = ddlEspacios.SelectedValue
        If wespacios = 0 Then
            TxtErrorEspacios.Text = "Debe informar el Espacio Escénico"
            ddlEspacios.Focus()
            Return False
        End If
        Dim wcant1 As Integer = 0
        Try
            wcant1 = CInt(TextBoxCant1.Text)
        Catch ex As Exception
            wcant1 = 0
        End Try
        Dim wcant2 As Integer = 0
        Try
            wcant2 = CInt(TextBoxCant2.Text)
        Catch ex As Exception
            wcant2 = 0
        End Try
        Dim wcant3 As Integer = 0
        Try
            wcant3 = CInt(TextBoxCant3.Text)
        Catch ex As Exception
            wcant3 = 0
        End Try
        Dim wcant4 As Integer = 0
        Try
            wcant4 = CInt(TextBoxCant4.Text)
        Catch ex As Exception
            wcant4 = 0
        End Try
        If wespacios >= 1 And (DdlTipoSala1.SelectedValue = 0 Or wcant1 = 0 Or ddlTipoasien1.SelectedValue = 0) Then
            LblErrorEspacio1.Text = "Complete los datos del Espacio escénico 1"
            ddlTipoasien1.Focus()
            Return False
        End If
        If wespacios >= 2 And (DdlTipoSala2.SelectedValue = 0 Or wcant2 = 0 Or ddlTipoasien2.SelectedValue = 0) Then
            LblErrorEspacio2.Text = "Complete los datos del Espacio escénico 2"
            ddlTipoasien2.Focus()
            Return False
        End If
        If wespacios >= 3 And (DdlTipoSala3.SelectedValue = 0 Or wcant3 = 0 Or ddlTipoasien3.SelectedValue = 0) Then
            LblErrorEspacio3.Text = "Complete los datos del Espacio escénico 3"
            ddlTipoasien3.Focus()
            Return False
        End If
        If wespacios = 4 And (DdlTipoSala4.SelectedValue = 0 Or wcant4 = 0 Or ddlTipoasien4.SelectedValue = 0) Then
            LblErrorEspacio4.Text = "Complete los datos del Espacio escénico 4"
            ddlTipoasien4.Focus()
            Return False
        End If
        If wcant1 + wcant2 + wcant3 + wcant4 > 300 Then
            txtErrorCantidadLocalidades.Text = "El número de localidades no debe superar 300"
            txtLocalidades.Focus()
            Return False
        End If
        Session("localidades") = wcant1 + wcant2 + wcant3 + wcant4
        'If quien.Persona = "FISICA" And ds.Integrantes.Count < 1 Then
        '    txtErrorIntegrante.Text = "Debe ingresar al menos 1 integrante"
        '    Return False
        'ElseIf quien.Persona = "JURIDICA" And ds.Integrantes.Count < 2 Then
        '    txtErrorIntegrante.Text = "Debe ingresar al menos 2 integrantes"
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
        Dim sHabilitacion As String
        Dim sFechaInicio As String
        Dim sfechaGestion As String
        Dim sPrefijo As Object
        Dim sTelefono As Object
        Dim nEquipPropio As Integer = 0
        If RadioButtonEquipa1.Checked = True Then
            nEquipPropio = 1
        End If
        Dim wfecha As Date = DateAndTime.Now
        Dim nCantespacios As Integer = DdlCantidadEspacios.SelectedValue
        Try
            sHabilitacion = DdlDocumentacion.SelectedValue.ToString
            wfecha = CDate(TextDesde.Value)
            sFechaInicio = Year(wfecha) * 10000 + Month(wfecha) * 100 + Day(wfecha)
            wfecha = CDate(TextGestion.Value)
            sfechaGestion = Year(wfecha) * 10000 + Month(wfecha) * 100 + Day(wfecha)
            sPrefijo = IIf(txtPrefijo.Text.Trim <> "", txtPrefijo.Text.Trim, "NULL")
            sTelefono = IIf(txtNumero.Text.Trim <> "", txtNumero.Text.Trim, "NULL")
            Dim sLocalidades As String = Session("localidades").ToString

            'INSERT Registro
            sSQLCmd = "INSERT INTO Registro " &
                            "(responsable, sector, provincia, " &
                            "denominacion, localidad, copost, " &
                            "domicilio, prefijo, telefono, email, " &
                            "pagina, localidades, inicio, " &
                            "espacio, comespacio, habilitacion, " &
                            "espaescen, equipamiento, fechAlta, FECHAGEST," &
                            "EQUIPPROPIO, CANTESPACIOS ) " &
                        "VALUES " &
                            "(" & Session("USER_ID") & ", " & Session("SECTOR") & ", " & ddlProvincias.SelectedValue & ", " &
                            "'" & txtDenominacion.Text.Trim.ToUpper & "', " & ddlLocalidades.Text & "," & txtCP.Text & " ," &
                            "'" & txtDomicilio.Text.Trim.ToUpper & "'," & sPrefijo & "," & sTelefono & " , '" & txtMail.Text.Trim & "', " &
                            "'" & txtWeb.Text.Trim & "', " & sLocalidades & " ,Convert(datetime,'" & sFechaInicio & "'), " &
                            " " & ddlEspacios.SelectedValue & ", '" & txtComentariosEspacio.Text.Trim.ToUpper & "'," & sHabilitacion & "," &
                            "'" & txtEspacioEscenico.Text.Trim.ToUpper & "', '" & txtEquipamiento.Text.Trim.ToUpper & "', getdate()," &
                            " Convert(datetime,'" & sfechaGestion & "')," & nEquipPropio & "," & nCantespacios & " ) " &
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
            Try
                MyCommand.ExecuteNonQuery()
            Catch ex As Exception
                txtErrorAcepto.Text = "Error al ingresar datos. Intente más tarde"
                Return False
            End Try
            nIdRegistro = mIdRegistro.Value
            MyCommand.Dispose()
            MyConnection.Dispose()

            'Funcionamiento Anterior
            Dim nFUNCIONANT As Integer = 0
            If RadioButtonEdificio1.Checked = True Then
                nFUNCIONANT = 1
            End If
            Dim nANIOMUDANZA As Integer = 0
            Try
                nANIOMUDANZA = CInt(TextBoxAnioMudanza.Text)
            Catch ex As Exception
                nANIOMUDANZA = 0
            End Try
            Dim sDOMIANT As String = TextBoxDomiAnterior.Text.Trim
            Dim sqlm As String = "update REGISTRO set FUNCIONANT=" & nFUNCIONANT & ",ANIOMUDANZA=" & nANIOMUDANZA & ",DOMIANT='" & sDOMIANT & "'  where CODIGO=" & nIdRegistro
            Dim Psqlm As New SqlClient.SqlCommand(sqlm, cn)
            cn.Open()
            Try
                Psqlm.ExecuteNonQuery()
            Catch ex As Exception
                txtErrorAcepto.Text = "Error al ingresar datos del funcionamiento anterior"
                Return False
            End Try
            cn.Close()

            dsInte.AceptaCambios(ds, nIdRegistro)

            ' Registro de Espacios
            If nCantespacios >= 1 Then
                Dim wespacio As Integer = 1
                Dim wtipo As Integer = DdlTipoSala1.SelectedValue
                Dim wcantidad As Integer = 0
                Try
                    wcantidad = CInt(TextBoxCant1.Text)
                Catch ex As Exception
                    wcantidad = 0
                End Try
                Dim wasientos As Integer = ddlTipoasien1.SelectedValue
                Dim sql As String = "insert into REGISTROESPACIOS (CODIGOREGISTRO,ESPACIO,TIPO,CANTIDAD,ASIENTOS,FECHALTA) " &
                       " values (" & nIdRegistro & "," & wespacio & "," & wtipo & "," & wcantidad & "," & wasientos & ",getdate()) "
                Dim Psql As New SqlClient.SqlCommand(sql, cn)
                cn.Open()
                Try
                    Psql.ExecuteNonQuery()
                Catch ex As Exception
                    txtErrorAcepto.Text = "Error al actualizar espacio 1"
                    Return False
                End Try
                cn.Close()
            End If
            If nCantespacios >= 2 Then
                Dim wespacio As Integer = 2
                Dim wtipo As Integer = DdlTipoSala2.SelectedValue
                Dim wcantidad As Integer = 0
                Try
                    wcantidad = CInt(TextBoxCant2.Text)
                Catch ex As Exception
                    wcantidad = 0
                End Try
                Dim wasientos As Integer = ddlTipoasien2.SelectedValue
                Dim sql As String = "insert into REGISTROESPACIOS (CODIGOREGISTRO,ESPACIO,TIPO,CANTIDAD,ASIENTOS,FECHALTA) " &
                       " values (" & nIdRegistro & "," & wespacio & "," & wtipo & "," & wcantidad & "," & wasientos & ",getdate()) "
                Dim Psql As New SqlClient.SqlCommand(sql, cn)
                cn.Open()
                Try
                    Psql.ExecuteNonQuery()
                Catch ex As Exception
                    txtErrorAcepto.Text = "Error al actualizar espacio 2"
                    Return False
                End Try
                cn.Close()
            End If
            If nCantespacios >= 3 Then
                Dim wespacio As Integer = 3
                Dim wtipo As Integer = DdlTipoSala3.SelectedValue
                Dim wcantidad As Integer = 0
                Try
                    wcantidad = CInt(TextBoxCant3.Text)
                Catch ex As Exception
                    wcantidad = 0
                End Try
                Dim wasientos As Integer = ddlTipoasien3.SelectedValue
                Dim sql As String = "insert into REGISTROESPACIOS (CODIGOREGISTRO,ESPACIO,TIPO,CANTIDAD,ASIENTOS,FECHALTA) " &
                       " values (" & nIdRegistro & "," & wespacio & "," & wtipo & "," & wcantidad & "," & wasientos & ",getdate()) "
                Dim Psql As New SqlClient.SqlCommand(sql, cn)
                cn.Open()
                Try
                    Psql.ExecuteNonQuery()
                Catch ex As Exception
                    txtErrorAcepto.Text = "Error al actualizar espacio 3"
                    Return False
                End Try
                cn.Close()
            End If
            If nCantespacios = 4 Then
                Dim wespacio As Integer = 4
                Dim wtipo As Integer = DdlTipoSala4.SelectedValue
                Dim wcantidad As Integer = 0
                Try
                    wcantidad = CInt(TextBoxCant4.Text)
                Catch ex As Exception
                    wcantidad = 0
                End Try
                Dim wasientos As Integer = ddlTipoasien4.SelectedValue
                Dim sql As String = "insert into REGISTROESPACIOS (CODIGOREGISTRO,ESPACIO,TIPO,CANTIDAD,ASIENTOS,FECHALTA) " &
                       " values (" & nIdRegistro & "," & wespacio & "," & wtipo & "," & wcantidad & "," & wasientos & ",getdate()) "
                Dim Psql As New SqlClient.SqlCommand(sql, cn)
                cn.Open()
                Try
                    Psql.ExecuteNonQuery()
                Catch ex As Exception
                    txtErrorAcepto.Text = "Error al actualizar espacio 4"
                    Return False
                End Try
                cn.Close()
            End If
            'Fin de Registro de Espacios

            'Espacios Complementarios
            Dim nENSAYO As Integer = 0
            Dim nRESTAURANT As Integer = 0
            Dim nGALERIA As Integer = 0
            Dim nBIBLIO As Integer = 0
            Dim nOTROS As Integer = 0
            Dim sDESCOTROS As String = TextBoxObservaOtros.Text
            If chkSalaEnsayo.Checked = True Then
                nENSAYO = 1
            End If
            If chkBarRestaurant.Checked = True Then
                nRESTAURANT = 1
            End If
            If chkGaleriaArte.Checked = True Then
                nGALERIA = 1
            End If
            If chkBiblioteca.Checked = 1 Then
                nBIBLIO = 1
            End If
            If ChkOtros.Checked = True Then
                nOTROS = 1
            End If
            Dim sqlo As String = "update REGISTRO set ENSAYO=" & nENSAYO & ",RESTAURANT=" & nRESTAURANT & ",GALERIA=" & nGALERIA & ",BIBLIO=" & nBIBLIO & ",OTROS=" & nOTROS & ",DESCOTROS='" & sDESCOTROS & "' where CODIGO=" & nIdRegistro
            Dim Psqlo As New SqlClient.SqlCommand(sqlo, cn)
            cn.Open()
            Try
                Psqlo.ExecuteNonQuery()
            Catch ex As Exception
                txtErrorAcepto.Text = "Error al actualizar espacios complementarios"
            End Try
            cn.Close()
            'End espacios Complementarios

            'Personas Necesarias
            Dim nPERSONAS As Integer = 0
            Dim nPRODUCCION As Integer = 0
            Dim nGESTION As Integer = 0
            Dim nPROGRAMA As Integer = 0
            Dim nBOLETERIA As Integer = 0
            Dim nASISTENCIA As Integer = 0
            Dim nLIMPIEZA As Integer = 0
            Dim nMANTEN As Integer = 0
            Dim nTECNICA As Integer = 0
            Dim nINGRESO As Integer = 0
            Dim nOTROSPER As Integer = 0
            Dim sDESCOTPER As String = TxtPersonasOtros.Text
            Try
                nPERSONAS = CInt(TextBoxPersonas.Text)
            Catch ex As Exception
                nPERSONAS = 0
            End Try
            If ChkProduccion.Checked = True Then
                nPRODUCCION = 1
            End If
            If ChkGestion.Checked = True Then
                nGESTION = 1
            End If
            If ChkPrograma.Checked = True Then
                nPROGRAMA = 1
            End If
            If ChkBoleteria.Checked = True Then
                nBOLETERIA = 1
            End If
            If ChkAsistencia.Checked = True Then
                nASISTENCIA = 1
            End If
            If ChkLimpieza.Checked = True Then
                nLIMPIEZA = 1
            End If
            If ChkMantenimiento.Checked = True Then
                nMANTEN = 1
            End If
            If ChkTecnica.Checked = True Then
                nTECNICA = 1
            End If
            If ChkControl.Checked = True Then
                nINGRESO = 1
            End If
            If ChkPersonasOtros.Checked = True Then
                nOTROSPER = 1
            End If
            Dim sqlp As String = "update registro set PERSONAS=" & nPERSONAS & ",PRODUCCION=" & nPRODUCCION & ",GESTION=" & nGESTION & ",PROGRAMA=" & nPROGRAMA & ",BOLETERIA=" & nBOLETERIA & ",ASISTENCIA=" & nASISTENCIA & "," &
                  "LIMPIEZA=" & nLIMPIEZA & ",MANTEN=" & nMANTEN & ",TECNICA=" & nTECNICA & ",INGRESO=" & nINGRESO & ",OTROSPER=" & nOTROSPER & ",DESCOTPER='" & sDESCOTPER & "' where CODIGO=" & nIdRegistro
            Dim Psqlp As New SqlClient.SqlCommand(sqlp, cn)
            cn.Open()
            Try
                Psqlp.ExecuteNonQuery()
            Catch ex As Exception
                txtErrorAcepto.Text = "Error al actualizar personas necesarias"
            End Try
            cn.Close()
            'End Personas

            Session.Add("CODIGO_REGISTRO", nIdRegistro)

            Session("CodRegistro") = nIdRegistro
            Dim bGrabar As Boolean = False
            bGrabar = GrabarAdjuntos()
            If bGrabar = False Then
                GuardarDatos = False
                Return False
            End If

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
        Dim sFechaInicio As String
        Dim sHabilitacion As String
        Dim sPrefijo As String
        Dim sTelefono As String
        Dim wfecha As Date = DateAndTime.Now
        Dim nIdregistro As Integer = Session("CODIGO")
        Try
            sHabilitacion = DdlDocumentacion.SelectedValue.ToString
            wfecha = CDate(TextDesde.Value)
            sFechaInicio = Year(wfecha) * 10000 + Month(wfecha) * 100 + Day(wfecha)
            sPrefijo = IIf(txtPrefijo.Text.Trim <> "", txtPrefijo.Text.Trim, "NULL")
            sTelefono = IIf(txtNumero.Text.Trim <> "", txtNumero.Text.Trim, "NULL")
            Dim sLocalidades As String = Session("localidades").ToString
            'UPDATE Registro
            sSQLCmd = "UPDATE Registro " &
                           "SET RESPONSABLE = " & Session("USER_ID") & ",  " &
                              "SECTOR = " & Session("SECTOR") & ",  " &
                              "Provincia = " & ddlProvincias.SelectedValue & ", " &
                              "DENOMINACION = '" & txtDenominacion.Text.Trim.ToUpper & "', " &
                              "LOCALIDAD = " & ddlLocalidades.Text & ", " &
                              "copost = " & txtCP.Text & ", " &
                              "domicilio = '" & txtDomicilio.Text.Trim.ToUpper & "', " &
                              "prefijo = " & sPrefijo & ", " &
                              "telefono = " & sTelefono & ", " &
                              "EMAIL = '" & txtMail.Text.Trim & "', " &
                              "PAGINA = '" & txtWeb.Text.Trim & "', " &
                              "localidades = '" & sLocalidades & "', " &
                              "INICIO = Convert(datetime,'" & sFechaInicio & "'), " &
                              "EQUIPAMIENTO = '" & txtEquipamiento.Text.Trim.ToUpper & "', " &
                              "ESPACIO = " & ddlEspacios.Text & ", " &
                              "COMESPACIO =  '" & txtComentariosEspacio.Text.Trim.ToUpper & "', " &
                              "habilitacion =  '" & sHabilitacion & "', " &
                              "espaescen =  '" & txtComentariosEspacio.Text.Trim.ToUpper & "' " &
                         "WHERE codigo = " & nIdregistro
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
            Dim nENSAYO As Integer = 0
            If chkSalaEnsayo.Checked = True Then
                nENSAYO = 1
            End If
            Dim nRESTAURANT As Integer = 0
            If chkBarRestaurant.Checked = True Then
                nRESTAURANT = 1
            End If
            Dim nGALERIA As Integer = 0
            If chkGaleriaArte.Checked = True Then
                nGALERIA = 1
            End If
            Dim nBIBLIO As Integer = 0
            If chkBiblioteca.Checked = True Then
                nBIBLIO = 1
            End If
            Dim nOTROS As Integer = 0
            If ChkOtros.Checked = True Then
                nOTROS = 1
            End If
            Dim sDESCOTROS As String = TextBoxObservaOtros.Text
            Dim nPERSONAS As Integer = 0
            Try
                nPERSONAS = CInt(TextBoxPersonas.Text)
            Catch ex As Exception
                nPERSONAS = 0
            End Try
            Dim nPRODUCCION As Integer = 0
            If ChkProduccion.Checked = True Then
                nPRODUCCION = 1
            End If
            Dim nGESTION As Integer = 0
            If ChkGestion.Checked = True Then
                nGESTION = 1
            End If
            Dim nPROGRAMA As Integer = 0
            If ChkPrograma.Checked = True Then
                nPROGRAMA = 1
            End If
            Dim nBOLETERIA As Integer = 0
            If ChkBoleteria.Checked = True Then
                nBOLETERIA = 1
            End If
            Dim nASISTENCIA As Integer = 0
            If ChkAsistencia.Checked = True Then
                nASISTENCIA = 1
            End If
            Dim nLIMPIEZA As Integer = 0
            If ChkLimpieza.Checked = True Then
                nLIMPIEZA = 1
            End If
            Dim nMANTEN As Integer = 0
            If ChkMantenimiento.Checked = True Then
                nMANTEN = 1
            End If
            Dim nTECNICA As Integer = 0
            If ChkTecnica.Checked = True Then
                nTECNICA = 1
            End If
            Dim nINGRESO As Integer = 0
            If ChkControl.Checked = True Then
                nINGRESO = 1
            End If
            Dim nOTROSPER As Integer = 0
            If ChkPersonasOtros.Checked = True Then
                nOTROS = 1
            End If
            Dim sDESCOTPER As String = TxtPersonasOtros.Text
            Dim nEQUIPPROPIO As String = 0
            If RadioButtonEquipa1.Checked = True Then
                nEQUIPPROPIO = 1
            End If
            Dim dFECHAGEST As Date = Nothing
            Dim sFECHAGEST As String = TextGestion.Value.Trim
            Try
                dFECHAGEST = CDate(sFECHAGEST)
                sFECHAGEST = (Year(dFECHAGEST) * 10000 + Month(dFECHAGEST) * 100 + Day(dFECHAGEST)).ToString
            Catch ex As Exception
                sFECHAGEST = ""
            End Try
            Dim nCANTESPACIOS As Integer = DdlCantidadEspacios.SelectedValue
            Dim nFUNCIONANT As Integer = 0
            If RadioButtonEdificio1.Checked = True Then
                nFUNCIONANT = 1
            End If
            Dim nANIOMUDANZA As Integer = 0
            Try
                nANIOMUDANZA = CInt(TextBoxAnioMudanza.Text)
            Catch ex As Exception
                nANIOMUDANZA = 0
            End Try
            Dim sDOMIANT As String = TextBoxDomiAnterior.Text.Trim

            Dim sqln As String = ""

            If Len(sFECHAGEST) > 0 Then
                sqln = "update registro set ENSAYO=" & nENSAYO & ",RESTAURANT=" & nRESTAURANT & ",GALERIA=" & nGALERIA & " ,BIBLIO=" & nBIBLIO & ",OTROS=" & nOTROS & ",DESCOTROS='" & sDESCOTROS & "'," &
                "PERSONAS=" & nPERSONAS & ",PRODUCCION=" & nPRODUCCION & ",GESTION=" & nGESTION & ",PROGRAMA=" & nPROGRAMA & ",BOLETERIA=" & nBOLETERIA & ",ASISTENCIA=" & nASISTENCIA & ",LIMPIEZA=" & nLIMPIEZA & "," &
                "MANTEN=" & nMANTEN & ",TECNICA=" & nTECNICA & ",INGRESO=" & nINGRESO & ",OTROSPER=" & nOTROSPER & ",DESCOTPER='" & sDESCOTPER & "',EQUIPPROPIO=" & nEQUIPPROPIO & ",FECHAGEST=convert(datetime,'" & dFECHAGEST & "')," &
                "CANTESPACIOS=" & nCANTESPACIOS & ",FUNCIONANT= " & nFUNCIONANT & ",ANIOMUDANZA=" & nANIOMUDANZA & ",DOMIANT='" & sDOMIANT & "' where CODIGO=" & nIdregistro
            Else
                sqln = "update registro set ENSAYO=" & nENSAYO & ",RESTAURANT=" & nRESTAURANT & ",GALERIA=" & nGALERIA & " ,BIBLIO=" & nBIBLIO & ",OTROS=" & nOTROS & ",DESCOTROS='" & sDESCOTROS & "'," &
                "PERSONAS=" & nPERSONAS & ",PRODUCCION=" & nPRODUCCION & ",GESTION=" & nGESTION & ",PROGRAMA=" & nPROGRAMA & ",BOLETERIA=" & nBOLETERIA & ",ASISTENCIA=" & nASISTENCIA & ",LIMPIEZA=" & nLIMPIEZA & "," &
                "MANTEN=" & nMANTEN & ",TECNICA=" & nTECNICA & ",INGRESO=" & nINGRESO & ",OTROSPER=" & nOTROSPER & ",DESCOTPER='" & sDESCOTPER & "',EQUIPPROPIO=" & nEQUIPPROPIO & ",FECHAGEST=NULL," &
                "CANTESPACIOS=" & nCANTESPACIOS & ",FUNCIONANT= " & nFUNCIONANT & ",ANIOMUDANZA=" & nANIOMUDANZA & ",DOMIANT='" & sDOMIANT & "' where CODIGO=" & nIdregistro
            End If

            Dim cmdn As New SqlClient.SqlCommand(sqln, cn)
            cn.Open()
            Try
                cmdn.ExecuteNonQuery()
            Catch ex As Exception
                txtErrorAcepto.Text = "Error al registrar datos"
                Return False
            End Try
            cn.Close()

            dsInte.AceptaCambios(ds, Session("CODIGO"))

            If nCANTESPACIOS > 0 Then
                Dim nCant As Integer = 0
                Dim sqle As String = "delete from REGISTROESPACIOS where CODIGOREGISTRO=" & nIdregistro & " and ESPACIO>" & nCANTESPACIOS
                Dim cmde As New SqlClient.SqlCommand(sqle, cn)
                cn.Open()
                Try
                    cmde.ExecuteNonQuery()
                Catch ex As Exception
                    txtErrorAcepto.Text = "Error al borrar espacios"
                    Return False
                End Try
                cn.Close()
                If nCANTESPACIOS >= 1 Then
                    sqle = "select count(*) from REGISTROESPACIOS where CODIGOREGISTRO=" & nIdregistro & " and ESPACIO=1"
                    cn.Open()
                    Dim Psqle As New SqlClient.SqlCommand(sqle, cn)
                    Dim dre As SqlClient.SqlDataReader = Psqle.ExecuteReader
                    While dre.Read()
                        nCant = dre.GetInt32(0)
                    End While
                    dre.Close()
                    cn.Close()
                    Dim wespacio As Integer = 1
                    Dim wtipo As Integer = DdlTipoSala1.SelectedValue
                    Dim wcantidad As Integer = 0
                    Try
                        wcantidad = CInt(TextBoxCant1.Text)
                    Catch ex As Exception
                        wcantidad = 0
                    End Try
                    Dim wasientos As Integer = ddlTipoasien1.SelectedValue
                    Dim sqlp As String = ""
                    If nCant = 0 Then
                        sqlp = "insert into REGISTROESPACIOS (CODIGOREGISTRO,ESPACIO,TIPO,CANTIDAD,ASIENTOS,FECHALTA) " &
                               " values (" & nIdregistro & "," & wespacio & "," & wtipo & "," & wcantidad & "," & wasientos & ",getdate()) "
                    Else
                        sqlp = "update REGISTROESPACIOS set ESPACIO=" & wespacio & ",TIPO=" & wtipo & ",CANTIDAD=" & wcantidad & ",ASIENTOS=" & wasientos & ",FECHALTA=getdate() " &
                               " where CODIGOREGISTRO=" & nIdregistro & " and ESPACIO=1"
                    End If
                    Dim Psql As New SqlClient.SqlCommand(sqlp, cn)
                    cn.Open()
                    Try
                        Psql.ExecuteNonQuery()
                    Catch ex As Exception
                        txtErrorAcepto.Text = "Error al actualizar espacio 1"
                        Return False
                    End Try
                    cn.Close()
                End If
                If nCANTESPACIOS >= 2 Then
                    sqle = "select count(*) from REGISTROESPACIOS where CODIGOREGISTRO=" & nIdregistro & " and ESPACIO=2"
                    cn.Open()
                    Dim Psqle As New SqlClient.SqlCommand(sqle, cn)
                    Dim dre As SqlClient.SqlDataReader = Psqle.ExecuteReader
                    While dre.Read()
                        nCant = dre.GetInt32(0)
                    End While
                    dre.Close()
                    cn.Close()
                    Dim wespacio As Integer = 2
                    Dim wtipo As Integer = DdlTipoSala2.SelectedValue
                    Dim wcantidad As Integer = 0
                    Try
                        wcantidad = CInt(TextBoxCant2.Text)
                    Catch ex As Exception
                        wcantidad = 0
                    End Try
                    Dim wasientos As Integer = ddlTipoasien2.SelectedValue
                    Dim sqlp As String = ""
                    If nCant = 0 Then
                        sqlp = "insert into REGISTROESPACIOS (CODIGOREGISTRO,ESPACIO,TIPO,CANTIDAD,ASIENTOS,FECHALTA) " &
                               " values (" & nIdregistro & "," & wespacio & "," & wtipo & "," & wcantidad & "," & wasientos & ",getdate()) "
                    Else
                        sqlp = "update REGISTROESPACIOS set ESPACIO=" & wespacio & ",TIPO=" & wtipo & ",CANTIDAD=" & wcantidad & ",ASIENTOS=" & wasientos & ",FECHALTA=getdate() " &
                               " where CODIGOREGISTRO=" & nIdregistro & " and ESPACIO=2"
                    End If
                    Dim Psql As New SqlClient.SqlCommand(sqlp, cn)
                    cn.Open()
                    Try
                        Psql.ExecuteNonQuery()
                    Catch ex As Exception
                        txtErrorAcepto.Text = "Error al actualizar espacio 2"
                        Return False
                    End Try
                    cn.Close()
                End If
                If nCANTESPACIOS >= 3 Then
                    sqle = "select count(*) from REGISTROESPACIOS where CODIGOREGISTRO=" & nIdregistro & " and ESPACIO=3"
                    cn.Open()
                    Dim Psqle As New SqlClient.SqlCommand(sqle, cn)
                    Dim dre As SqlClient.SqlDataReader = Psqle.ExecuteReader
                    While dre.Read()
                        nCant = dre.GetInt32(0)
                    End While
                    dre.Close()
                    cn.Close()
                    Dim wespacio As Integer = 3
                    Dim wtipo As Integer = DdlTipoSala3.SelectedValue
                    Dim wcantidad As Integer = 0
                    Try
                        wcantidad = CInt(TextBoxCant3.Text)
                    Catch ex As Exception
                        wcantidad = 0
                    End Try
                    Dim wasientos As Integer = ddlTipoasien3.SelectedValue
                    Dim sqlp As String = ""
                    If nCant = 0 Then
                        sqlp = "insert into REGISTROESPACIOS (CODIGOREGISTRO,ESPACIO,TIPO,CANTIDAD,ASIENTOS,FECHALTA) " &
                               " values (" & nIdregistro & "," & wespacio & "," & wtipo & "," & wcantidad & "," & wasientos & ",getdate()) "
                    Else
                        sqlp = "update REGISTROESPACIOS set ESPACIO=" & wespacio & ",TIPO=" & wtipo & ",CANTIDAD=" & wcantidad & ",ASIENTOS=" & wasientos & ",FECHALTA=getdate() " &
                               " where CODIGOREGISTRO=" & nIdregistro & " and ESPACIO=3"
                    End If
                    Dim Psql As New SqlClient.SqlCommand(sqlp, cn)
                    cn.Open()
                    Try
                        Psql.ExecuteNonQuery()
                    Catch ex As Exception
                        txtErrorAcepto.Text = "Error al actualizar espacio 3"
                        Return False
                    End Try
                    cn.Close()
                End If
                If nCANTESPACIOS = 4 Then
                    sqle = "select count(*) from REGISTROESPACIOS where CODIGOREGISTRO=" & nIdregistro & " and ESPACIO=4"
                    cn.Open()
                    Dim Psqle As New SqlClient.SqlCommand(sqle, cn)
                    Dim dre As SqlClient.SqlDataReader = Psqle.ExecuteReader
                    While dre.Read()
                        nCant = dre.GetInt32(0)
                    End While
                    dre.Close()
                    cn.Close()
                    Dim wespacio As Integer = 4
                    Dim wtipo As Integer = DdlTipoSala4.SelectedValue
                    Dim wcantidad As Integer = 0
                    Try
                        wcantidad = CInt(TextBoxCant4.Text)
                    Catch ex As Exception
                        wcantidad = 0
                    End Try
                    Dim wasientos As Integer = ddlTipoasien4.SelectedValue
                    Dim sqlp As String = ""
                    If nCant = 0 Then
                        sqlp = "insert into REGISTROESPACIOS (CODIGOREGISTRO,ESPACIO,TIPO,CANTIDAD,ASIENTOS,FECHALTA) " &
                               " values (" & nIdregistro & "," & wespacio & "," & wtipo & "," & wcantidad & "," & wasientos & ",getdate()) "
                    Else
                        sqlp = "update REGISTROESPACIOS set ESPACIO=" & wespacio & ",TIPO=" & wtipo & ",CANTIDAD=" & wcantidad & ",ASIENTOS=" & wasientos & ",FECHALTA=getdate() " &
                               " where CODIGOREGISTRO=" & nIdregistro & " and ESPACIO=4"
                    End If
                    Dim Psql As New SqlClient.SqlCommand(sqlp, cn)
                    cn.Open()
                    Try
                        Psql.ExecuteNonQuery()
                    Catch ex As Exception
                        txtErrorAcepto.Text = "Error al actualizar espacio 4"
                        Return False
                    End Try
                    cn.Close()
                End If

            End If

            'Dim Sql As String = "update integrantes Set verificado = null where codigoregistro=" & Session("CODIGO")
            'Dim cmd As New SqlClient.SqlCommand(Sql, cn)
            'cn.Open()
            'cmd.ExecuteNonQuery()
            'cn.Close()

            Session("CodRegistro") = nIdregistro
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

        'Guardara Peritaje Fotos
        fileSavePath = Server.MapPath("~/Documentos/REGISTRO/" & nIdRegistro & "/FOTOSPERITAJE")
        Try
            MkDir(fileSavePath)
        Catch ex As Exception
        End Try
        Dim dt = CType(Session("mFotos"), DataTable)
        Dim i As Integer = 0
        Do While Not i = dt.Rows.Count
            Dim r As DataRow = dt.Rows(i)
            Dim identidad As String = r.Item("identidad").ToString
            Dim FilePath As String = r.Item("filepath").ToString
            FileName = r.Item("documento").ToString
            Dim FilepathDest As String = fileSavePath + "\" + FileName
            Try
                File.Copy(FilePath, FilepathDest)
            Catch ex As Exception
                txtErrorEquipamiento.Text = "No se pudieron guardar Fotos para Peritaje"
                Return False
            End Try
            i += 1
        Loop

        'Guardar Peritaje Planos
        fileSavePath = Server.MapPath("~/Documentos/REGISTRO/" & nIdRegistro & "/PLANOSPERITAJE")
        Try
            MkDir(fileSavePath)
        Catch ex As Exception
        End Try
        Dim dtp = CType(Session("mPlanos"), DataTable)
        i = 0
        Do While Not i = dt.Rows.Count
            Dim r As DataRow = dt.Rows(i)
            Dim identidad As String = r.Item("identidad").ToString
            Dim FilePath As String = r.Item("filepath").ToString
            FileName = r.Item("documento").ToString
            Dim FilepathDest As String = fileSavePath + "\" + FileName
            Try
                File.Copy(FilePath, FilepathDest)
            Catch ex As Exception
                txtErrorEquipamiento.Text = "No se pudieron guardar Planos para Peritaje"
                Return False
            End Try
            i += 1
        Loop

        'Guardar Espacio Escénico 1
        FileName = Path.GetFileName(FileUploadPlanoEscena1.PostedFile.FileName)
        Extension = Path.GetExtension(FileUploadPlanoEscena1.PostedFile.FileName)
        FolderPath = ConfigurationManager.AppSettings("FolderPath")
        fileSavePath = Server.MapPath("~/Documentos/REGISTRO/" & nIdRegistro & "/ESPACIO/1/PLANO")
        Try
            MkDir(fileSavePath)
        Catch ex As Exception
        End Try
        If FileUploadPlanoEscena1.HasFile Then
            Dim Filepath As String = fileSavePath + "\" + FileName
            Try
                FileUploadPlanoEscena1.SaveAs(Filepath)
            Catch ex As Exception
                LblErrorEspacio1.Text = "No se pudo guardar Plano Escena Espacio 1"
                Return False
            End Try
        Else
            If Session("FileUploadPlanoEscena1FileName") IsNot Nothing Then
                Dim UploadImporta1 As FileUpload = CType(Session("FileUploadPlanoEscena11"), FileUpload)
                FileName = UploadImporta1.FileName
                If Len(RTrim(FileName)) = 0 Then
                    Dim Archivo As String = Session("FileUploadPlanoEscena1FileName")
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
                Dim Filepath As String = Session("FileUploadPlanoEscena1FileName")
                Dim FilepathDest As String = fileSavePath + "\" + FileName
                Try
                    File.Copy(Filepath, FilepathDest)
                Catch ex As Exception
                    LblErrorEspacio1.Text = "No se pudo guardar Plano Escena Espacio 1"
                    Return False
                End Try
            End If
        End If

        FileName = Path.GetFileName(FileUploadPlantaLuz1.PostedFile.FileName)
        Extension = Path.GetExtension(FileUploadPlantaLuz1.PostedFile.FileName)
        fileSavePath = Server.MapPath("~/Documentos/REGISTRO/" & nIdRegistro & "/ESPACIO/1/LUZ")
        Try
            MkDir(fileSavePath)
        Catch ex As Exception
        End Try
        If FileUploadPlantaLuz1.HasFile Then
            Dim Filepath As String = fileSavePath + "\" + FileName
            Try
                FileUploadPlantaLuz1.SaveAs(Filepath)
            Catch ex As Exception
                LblErrorEspacio1.Text = "No se pudo guardar Planta de Luz Espacio 1"
                Return False
            End Try
        Else
            If Session("FileUploadPlantaLuz1FileName") IsNot Nothing Then
                Dim UploadImporta1 As FileUpload = CType(Session("FileUploadPlantaLuz11"), FileUpload)
                FileName = UploadImporta1.FileName
                If Len(RTrim(FileName)) = 0 Then
                    Dim Archivo As String = Session("FileUploadPlantaLuz1FileName")
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
                Dim Filepath As String = Session("FileUploadPlantaLuz1FileName")
                Dim FilepathDest As String = fileSavePath + "\" + FileName
                Try
                    File.Copy(Filepath, FilepathDest)
                Catch ex As Exception
                    LblErrorEspacio1.Text = "No se pudo guardar Planta de Luz Espacio 1"
                    Return False
                End Try
            End If
        End If

        FileName = Path.GetFileName(FileUploadFotoEscena1.PostedFile.FileName)
        Extension = Path.GetExtension(FileUploadFotoEscena1.PostedFile.FileName)
        fileSavePath = Server.MapPath("~/Documentos/REGISTRO/" & nIdRegistro & "/ESPACIO/1/ESCENA")
        Try
            MkDir(fileSavePath)
        Catch ex As Exception
        End Try
        If FileUploadFotoEscena1.HasFile Then
            Dim Filepath As String = fileSavePath + "\" + FileName
            Try
                FileUploadFotoEscena1.SaveAs(Filepath)
            Catch ex As Exception
                LblErrorEspacio1.Text = "No se pudo guardar Foto Escenario Espacio 1"
                Return False
            End Try
        Else
            If Session("FileUploadFotoEscena1FileName") IsNot Nothing Then
                Dim UploadImporta1 As FileUpload = CType(Session("FileUploadFotoEscena11"), FileUpload)
                FileName = UploadImporta1.FileName
                If Len(RTrim(FileName)) = 0 Then
                    Dim Archivo As String = Session("FileUploadFotoEscena1FileName")
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
                Dim Filepath As String = Session("FileUploadFotoEscena1FileName")
                Dim FilepathDest As String = fileSavePath + "\" + FileName
                Try
                    File.Copy(Filepath, FilepathDest)
                Catch ex As Exception
                    LblErrorEspacio1.Text = "No se pudo guardar Foto Escenario de Luz Espacio 1"
                    Return False
                End Try
            End If
        End If

        'Guardar Espacio Escénico 2
        FileName = Path.GetFileName(FileUploadPlanoEscena2.PostedFile.FileName)
        Extension = Path.GetExtension(FileUploadPlanoEscena2.PostedFile.FileName)
        FolderPath = ConfigurationManager.AppSettings("FolderPath")
        fileSavePath = Server.MapPath("~/Documentos/REGISTRO/" & nIdRegistro & "/ESPACIO/2/PLANO")
        Try
            MkDir(fileSavePath)
        Catch ex As Exception
        End Try
        If FileUploadPlanoEscena2.HasFile Then
            Dim Filepath As String = fileSavePath + "\" + FileName
            Try
                FileUploadPlanoEscena2.SaveAs(Filepath)
            Catch ex As Exception
                LblErrorEspacio2.Text = "No se pudo guardar Plano Escena Espacio 2"
                Return False
            End Try
        Else
            If Session("FileUploadPlanoEscena2FileName") IsNot Nothing Then
                Dim UploadImporta1 As FileUpload = CType(Session("FileUploadPlanoEscena21"), FileUpload)
                FileName = UploadImporta1.FileName
                If Len(RTrim(FileName)) = 0 Then
                    Dim Archivo As String = Session("FileUploadPlanoEscena2FileName")
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
                Dim Filepath As String = Session("FileUploadPlanoEscena2FileName")
                Dim FilepathDest As String = fileSavePath + "\" + FileName
                Try
                    File.Copy(Filepath, FilepathDest)
                Catch ex As Exception
                    LblErrorEspacio2.Text = "No se pudo guardar Plano Escena Espacio 2"
                    Return False
                End Try
            End If
        End If

        FileName = Path.GetFileName(FileUploadPlantaLuz2.PostedFile.FileName)
        Extension = Path.GetExtension(FileUploadPlantaLuz2.PostedFile.FileName)
        fileSavePath = Server.MapPath("~/Documentos/REGISTRO/" & nIdRegistro & "/ESPACIO/2/LUZ")
        Try
            MkDir(fileSavePath)
        Catch ex As Exception
        End Try
        If FileUploadPlantaLuz2.HasFile Then
            Dim Filepath As String = fileSavePath + "\" + FileName
            Try
                FileUploadPlantaLuz2.SaveAs(Filepath)
            Catch ex As Exception
                LblErrorEspacio2.Text = "No se pudo guardar Planta de Luz Espacio 2"
                Return False
            End Try
        Else
            If Session("FileUploadPlantaLuz2FileName") IsNot Nothing Then
                Dim UploadImporta1 As FileUpload = CType(Session("FileUploadPlantaLuz21"), FileUpload)
                FileName = UploadImporta1.FileName
                If Len(RTrim(FileName)) = 0 Then
                    Dim Archivo As String = Session("FileUploadPlantaLuz2FileName")
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
                Dim Filepath As String = Session("FileUploadPlantaLuz2FileName")
                Dim FilepathDest As String = fileSavePath + "\" + FileName
                Try
                    File.Copy(Filepath, FilepathDest)
                Catch ex As Exception
                    LblErrorEspacio2.Text = "No se pudo guardar Planta de Luz Espacio 2"
                    Return False
                End Try
            End If
        End If

        FileName = Path.GetFileName(FileUploadFotoEscena2.PostedFile.FileName)
        Extension = Path.GetExtension(FileUploadFotoEscena2.PostedFile.FileName)
        fileSavePath = Server.MapPath("~/Documentos/REGISTRO/" & nIdRegistro & "/ESPACIO/2/ESCENA")
        Try
            MkDir(fileSavePath)
        Catch ex As Exception
        End Try
        If FileUploadFotoEscena2.HasFile Then
            Dim Filepath As String = fileSavePath + "\" + FileName
            Try
                FileUploadFotoEscena2.SaveAs(Filepath)
            Catch ex As Exception
                LblErrorEspacio2.Text = "No se pudo guardar Foto Escenario Espacio 2"
                Return False
            End Try
        Else
            If Session("FileUploadFotoEscena2FileName") IsNot Nothing Then
                Dim UploadImporta1 As FileUpload = CType(Session("FileUploadFotoEscena21"), FileUpload)
                FileName = UploadImporta1.FileName
                If Len(RTrim(FileName)) = 0 Then
                    Dim Archivo As String = Session("FileUploadFotoEscena2FileName")
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
                Dim Filepath As String = Session("FileUploadFotoEscena2FileName")
                Dim FilepathDest As String = fileSavePath + "\" + FileName
                Try
                    File.Copy(Filepath, FilepathDest)
                Catch ex As Exception
                    LblErrorEspacio2.Text = "No se pudo guardar Foto Escenario de Luz Espacio 2"
                    Return False
                End Try
            End If
        End If

        'Guardar Espacio Escénico 3
        FileName = Path.GetFileName(FileUploadPlanoEscena3.PostedFile.FileName)
        Extension = Path.GetExtension(FileUploadPlanoEscena3.PostedFile.FileName)
        FolderPath = ConfigurationManager.AppSettings("FolderPath")
        fileSavePath = Server.MapPath("~/Documentos/REGISTRO/" & nIdRegistro & "/ESPACIO/3/PLANO")
        Try
            MkDir(fileSavePath)
        Catch ex As Exception
        End Try
        If FileUploadPlanoEscena3.HasFile Then
            Dim Filepath As String = fileSavePath + "\" + FileName
            Try
                FileUploadPlanoEscena3.SaveAs(Filepath)
            Catch ex As Exception
                LblErrorEspacio3.Text = "No se pudo guardar Plano Escena Espacio 3"
                Return False
            End Try
        Else
            If Session("FileUploadPlanoEscena3FileName") IsNot Nothing Then
                Dim UploadImporta1 As FileUpload = CType(Session("FileUploadPlanoEscena31"), FileUpload)
                FileName = UploadImporta1.FileName
                If Len(RTrim(FileName)) = 0 Then
                    Dim Archivo As String = Session("FileUploadPlanoEscena3FileName")
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
                Dim Filepath As String = Session("FileUploadPlanoEscena3FileName")
                Dim FilepathDest As String = fileSavePath + "\" + FileName
                Try
                    File.Copy(Filepath, FilepathDest)
                Catch ex As Exception
                    LblErrorEspacio3.Text = "No se pudo guardar Plano Escena Espacio 3"
                    Return False
                End Try
            End If
        End If

        FileName = Path.GetFileName(FileUploadPlantaLuz3.PostedFile.FileName)
        Extension = Path.GetExtension(FileUploadPlantaLuz3.PostedFile.FileName)
        fileSavePath = Server.MapPath("~/Documentos/REGISTRO/" & nIdRegistro & "/ESPACIO/3/LUZ")
        Try
            MkDir(fileSavePath)
        Catch ex As Exception
        End Try
        If FileUploadPlantaLuz3.HasFile Then
            Dim Filepath As String = fileSavePath + "\" + FileName
            Try
                FileUploadPlantaLuz3.SaveAs(Filepath)
            Catch ex As Exception
                LblErrorEspacio3.Text = "No se pudo guardar Planta de Luz Espacio 3"
                Return False
            End Try
        Else
            If Session("FileUploadPlantaLuz3FileName") IsNot Nothing Then
                Dim UploadImporta1 As FileUpload = CType(Session("FileUploadPlantaLuz31"), FileUpload)
                FileName = UploadImporta1.FileName
                If Len(RTrim(FileName)) = 0 Then
                    Dim Archivo As String = Session("FileUploadPlantaLuz3FileName")
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
                Dim Filepath As String = Session("FileUploadPlantaLuz3FileName")
                Dim FilepathDest As String = fileSavePath + "\" + FileName
                Try
                    File.Copy(Filepath, FilepathDest)
                Catch ex As Exception
                    LblErrorEspacio3.Text = "No se pudo guardar Planta de Luz Espacio 3"
                    Return False
                End Try
            End If
        End If

        FileName = Path.GetFileName(FileUploadFotoEscena3.PostedFile.FileName)
        Extension = Path.GetExtension(FileUploadFotoEscena3.PostedFile.FileName)
        fileSavePath = Server.MapPath("~/Documentos/REGISTRO/" & nIdRegistro & "/ESPACIO/3/ESCENA")
        Try
            MkDir(fileSavePath)
        Catch ex As Exception
        End Try
        If FileUploadFotoEscena3.HasFile Then
            Dim Filepath As String = fileSavePath + "\" + FileName
            Try
                FileUploadFotoEscena3.SaveAs(Filepath)
            Catch ex As Exception
                LblErrorEspacio3.Text = "No se pudo guardar Foto Escenario Espacio 3"
                Return False
            End Try
        Else
            If Session("FileUploadFotoEscena3FileName") IsNot Nothing Then
                Dim UploadImporta1 As FileUpload = CType(Session("FileUploadFotoEscena31"), FileUpload)
                FileName = UploadImporta1.FileName
                If Len(RTrim(FileName)) = 0 Then
                    Dim Archivo As String = Session("FileUploadFotoEscena3FileName")
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
                Dim Filepath As String = Session("FileUploadFotoEscena3FileName")
                Dim FilepathDest As String = fileSavePath + "\" + FileName
                Try
                    File.Copy(Filepath, FilepathDest)
                Catch ex As Exception
                    LblErrorEspacio3.Text = "No se pudo guardar Foto Escenario de Luz Espacio 3"
                    Return False
                End Try
            End If
        End If

        'Guardar Espacio Escénico 4
        FileName = Path.GetFileName(FileUploadPlanoEscena4.PostedFile.FileName)
        Extension = Path.GetExtension(FileUploadPlanoEscena4.PostedFile.FileName)
        FolderPath = ConfigurationManager.AppSettings("FolderPath")
        fileSavePath = Server.MapPath("~/Documentos/REGISTRO/" & nIdRegistro & "/ESPACIO/4/PLANO")
        Try
            MkDir(fileSavePath)
        Catch ex As Exception
        End Try
        If FileUploadPlanoEscena4.HasFile Then
            Dim Filepath As String = fileSavePath + "\" + FileName
            Try
                FileUploadPlanoEscena4.SaveAs(Filepath)
            Catch ex As Exception
                LblErrorEspacio4.Text = "No se pudo guardar Plano Escena Espacio 4"
                Return False
            End Try
        Else
            If Session("FileUploadPlanoEscena4FileName") IsNot Nothing Then
                Dim UploadImporta1 As FileUpload = CType(Session("FileUploadPlanoEscena41"), FileUpload)
                FileName = UploadImporta1.FileName
                If Len(RTrim(FileName)) = 0 Then
                    Dim Archivo As String = Session("FileUploadPlanoEscena4FileName")
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
                Dim Filepath As String = Session("FileUploadPlanoEscena4FileName")
                Dim FilepathDest As String = fileSavePath + "\" + FileName
                Try
                    File.Copy(Filepath, FilepathDest)
                Catch ex As Exception
                    LblErrorEspacio4.Text = "No se pudo guardar Plano Escena Espacio 4"
                    Return False
                End Try
            End If
        End If

        FileName = Path.GetFileName(FileUploadPlantaLuz4.PostedFile.FileName)
        Extension = Path.GetExtension(FileUploadPlantaLuz4.PostedFile.FileName)
        fileSavePath = Server.MapPath("~/Documentos/REGISTRO/" & nIdRegistro & "/ESPACIO/4/LUZ")
        Try
            MkDir(fileSavePath)
        Catch ex As Exception
        End Try
        If FileUploadPlantaLuz4.HasFile Then
            Dim Filepath As String = fileSavePath + "\" + FileName
            Try
                FileUploadPlantaLuz4.SaveAs(Filepath)
            Catch ex As Exception
                LblErrorEspacio4.Text = "No se pudo guardar Planta de Luz Espacio 4"
                Return False
            End Try
        Else
            If Session("FileUploadPlantaLuz4FileName") IsNot Nothing Then
                Dim UploadImporta1 As FileUpload = CType(Session("FileUploadPlantaLuz41"), FileUpload)
                FileName = UploadImporta1.FileName
                If Len(RTrim(FileName)) = 0 Then
                    Dim Archivo As String = Session("FileUploadPlantaLuz4FileName")
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
                Dim Filepath As String = Session("FileUploadPlantaLuz4FileName")
                Dim FilepathDest As String = fileSavePath + "\" + FileName
                Try
                    File.Copy(Filepath, FilepathDest)
                Catch ex As Exception
                    LblErrorEspacio4.Text = "No se pudo guardar Planta de Luz Espacio 4"
                    Return False
                End Try
            End If
        End If

        FileName = Path.GetFileName(FileUploadFotoEscena4.PostedFile.FileName)
        Extension = Path.GetExtension(FileUploadFotoEscena4.PostedFile.FileName)
        fileSavePath = Server.MapPath("~/Documentos/REGISTRO/" & nIdRegistro & "/ESPACIO/4/ESCENA")
        Try
            MkDir(fileSavePath)
        Catch ex As Exception
        End Try
        If FileUploadFotoEscena4.HasFile Then
            Dim Filepath As String = fileSavePath + "\" + FileName
            Try
                FileUploadFotoEscena4.SaveAs(Filepath)
            Catch ex As Exception
                LblErrorEspacio4.Text = "No se pudo guardar Foto Escenario Espacio 4"
                Return False
            End Try
        Else
            If Session("FileUploadFotoEscena4FileName") IsNot Nothing Then
                Dim UploadImporta1 As FileUpload = CType(Session("FileUploadFotoEscena41"), FileUpload)
                FileName = UploadImporta1.FileName
                If Len(RTrim(FileName)) = 0 Then
                    Dim Archivo As String = Session("FileUploadFotoEscena4FileName")
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
                Dim Filepath As String = Session("FileUploadFotoEscena4FileName")
                Dim FilepathDest As String = fileSavePath + "\" + FileName
                Try
                    File.Copy(Filepath, FilepathDest)
                Catch ex As Exception
                    LblErrorEspacio4.Text = "No se pudo guardar Foto Escenario de Luz Espacio 4"
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
        Const F_COPOST As Integer = 5
        Const F_DOMICILIO As Integer = 6
        Const F_PREFIJO As Integer = 7
        Const F_TELEFONO As Integer = 8
        Const F_EMAIL As Integer = 9
        Const F_PAGINA As Integer = 10
        Const F_LOCALIDADES As Integer = 11
        Const F_INICIO As Integer = 12
        Const F_ESPACIO As Integer = 13
        Const F_COMESPACIO As Integer = 14
        Const F_HABILITACION As Integer = 15
        Const F_ESPAESCEN As Integer = 16
        Const F_EQUIPAMIENTO As Integer = 17
        'Const F_FECHALTA As Integer = 18
        Dim nENSAYO As Integer = 0
        Dim nRESTAURANT As Integer = 0
        Dim nGALERIA As Integer = 0
        Dim nBIBLIO As Integer = 0
        Dim nOTROS As Integer = 0
        Dim sDESCOTROS As String = 0
        Dim nPERSONAS As Integer = 0
        Dim nPRODUCCION As Integer = 0
        Dim nGESTION As Integer = 0
        Dim nPROGRAMA As Integer = 0
        Dim nBOLETERIA As Integer = 0
        Dim nASISTENCIA As Integer = 0
        Dim nLIMPIEZA As Integer = 0
        Dim nMANTEN As Integer = 0
        Dim nTECNICA As Integer = 0
        Dim nINGRESO As Integer = 0
        Dim nOTROSPER As Integer = 0
        Dim sDESCOTPER As String = ""
        Dim nEQUIPPROPIO As String = 0
        Dim sFECHAGEST As String = ""
        Dim nCANTESPACIOS As Integer = 0
        Dim nFUNCIONANT As Integer = 0
        Dim nANIOMUDANZA As Integer = 0
        Dim sDOMIANT As String = ""
        Dim sSQLCmd As String
        Dim MyConnection As SqlConnection
        Dim MyCommand As SqlCommand
        Dim MyReader As SqlDataReader
        Try
            'Load Registro
            'Inicializar()

            sSQLCmd = "Select responsable, sector, provincia, denominacion, localidad, copost, domicilio, prefijo, telefono, email, pagina, localidades, convert(Char(10),inicio,103) inicio, espacio, comespacio, habilitacion, espaescen, equipamiento, fechAlta, " &
                      " isnull(ENSAYO,0), isnull(RESTAURANT,0), isnull(GALERIA,0), isnull(BIBLIO,0), isnull(OTROS,0), isnull(DESCOTROS,'')," &
                      " isnull(PERSONAS,0), isnull(PRODUCCION,0), isnull(GESTION,0), isnull(PROGRAMA,0), isnull(BOLETERIA,0), isnull(ASISTENCIA,0), isnull(LIMPIEZA,0), isnull(MANTEN,0), isnull(TECNICA,0), isnull(INGRESO,0), isnull(OTROSPER,0), isnull(DESCOTPER,'')," &
                      " isnull(EQUIPPROPIO,0), isnull(convert(char(10),FECHAGEST,103),'') , isnull(CANTESPACIOS,0), isnull(FUNCIONANT,0),isnull(ANIOMUDANZA,0),isnull(DOMIANT,'') " &
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
                Inicializar()
                ddlSectores.Text = MyReader.Item(F_SECTOR).ToString.Trim
                ddlProvincias.Text = MyReader.Item(F_PROVINCIA)
                txtDenominacion.Text = MyReader.Item(F_DENOMINACION).ToString.Trim
                ddlLocalidades.Text = MyReader.Item(F_LOCALIDAD)
                ddlLocalidades.SelectedItem.Value = MyReader.Item(F_LOCALIDAD)
                localidad = ddlLocalidades.SelectedItem.Text
                txtCP.Text = MyReader.Item(F_COPOST)
                txtDomicilio.Text = MyReader.Item(F_DOMICILIO)
                txtPrefijo.Text = IIf(MyReader.IsDBNull(F_PREFIJO), "", MyReader.Item(F_PREFIJO))
                txtNumero.Text = IIf(MyReader.IsDBNull(F_TELEFONO), "", MyReader.Item(F_TELEFONO))
                txtMail.Text = MyReader.Item(F_EMAIL).ToString.Trim
                txtWeb.Text = MyReader.Item(F_PAGINA).ToString.Trim
                txtLocalidades.Text = MyReader.Item(F_LOCALIDADES).ToString.Trim
                TextDesde.Value = MyReader.Item(F_INICIO)
                ddlEspacios.Text = MyReader.Item(F_ESPACIO).ToString.Trim
                txtComentariosEspacio.Text = MyReader.Item(F_COMESPACIO).ToString.Trim
                'CheckBoxHabilitacion.Checked = IIf(MyReader.Item(F_HABILITACION) = 2, True, False)
                DdlDocumentacion.SelectedValue = MyReader.Item(F_HABILITACION)
                txtEspacioEscenico.Text = MyReader.Item(F_ESPAESCEN).ToString.Trim
                txtEquipamiento.Text = MyReader.Item(F_EQUIPAMIENTO).ToString.Trim
                nENSAYO = MyReader.GetInt32(19)
                nRESTAURANT = MyReader.GetInt32(20)
                nGALERIA = MyReader.GetInt32(21)
                nBIBLIO = MyReader.GetInt32(22)
                nOTROS = MyReader.GetInt32(23)
                sDESCOTROS = MyReader.GetString(24)
                nPERSONAS = MyReader.GetInt32(25)
                nPRODUCCION = MyReader.GetInt32(26)
                nGESTION = MyReader.GetInt32(27)
                nPROGRAMA = MyReader.GetInt32(28)
                nBOLETERIA = MyReader.GetInt32(29)
                nASISTENCIA = MyReader.GetInt32(30)
                nLIMPIEZA = MyReader.GetInt32(31)
                nMANTEN = MyReader.GetInt32(32)
                nTECNICA = MyReader.GetInt32(33)
                nINGRESO = MyReader.GetInt32(34)
                nOTROSPER = MyReader.GetInt32(35)
                sDESCOTPER = MyReader.GetString(36)
                nEQUIPPROPIO = MyReader.GetInt32(37)
                sFECHAGEST = MyReader.GetString(38)
                nCANTESPACIOS = MyReader.GetInt32(39)
                nFUNCIONANT = MyReader.GetInt32(40)
                nANIOMUDANZA = MyReader.GetInt32(41)
                sDOMIANT = MyReader.GetString(42)
            End If
            MyCommand.Dispose()
            MyConnection.Dispose()
            TextGestion.Value = sFECHAGEST
            If nFUNCIONANT = 1 Then
                RadioButtonEdificio1.Checked = True
                RadioButtonEdificio2.Checked = False
                DivTablaMudanza.Style.Item("display") = "block"
            Else
                RadioButtonEdificio1.Checked = False
                RadioButtonEdificio2.Checked = True
                DivTablaMudanza.Style.Item("display") = "none"
            End If
            If nEQUIPPROPIO = 1 Then
                RadioButtonEquipa1.Checked = True
                RadioButtonEquipa2.Checked = False
                DivTablaEquipamiento.Style.Item("display") = "block"
            Else
                RadioButtonEquipa1.Checked = False
                RadioButtonEquipa2.Checked = True
                DivTablaEquipamiento.Style.Item("display") = "none"
            End If
            DdlCantidadEspacios.SelectedValue = nCANTESPACIOS
            TextBoxAnioMudanza.Text = nANIOMUDANZA
            TextBoxDomiAnterior.Text = sDOMIANT
            If nENSAYO = 1 Then
                chkSalaEnsayo.Checked = True
            End If
            If nRESTAURANT = 1 Then
                chkBarRestaurant.Checked = True
            End If
            If nGALERIA = 1 Then
                chkGaleriaArte.Checked = True
            End If
            If nBIBLIO = 1 Then
                chkBiblioteca.Checked = True
            End If
            If nOTROS = 1 Then
                ChkOtros.Checked = True
            End If
            TextBoxObservaOtros.Text = sDESCOTROS
            TextBoxPersonas.Text = nPERSONAS
            If nPRODUCCION = 1 Then
                ChkProduccion.Checked = True
            End If
            If nGESTION = 1 Then
                ChkGestion.Checked = True
            End If
            If nPROGRAMA = 1 Then
                ChkPrograma.Checked = True
            End If
            If nBOLETERIA = 1 Then
                ChkBoleteria.Checked = True
            End If
            If nASISTENCIA = 1 Then
                ChkAsistencia.Checked = True
            End If
            If nLIMPIEZA = 1 Then
                ChkLimpieza.Checked = True
            End If
            If nMANTEN = 1 Then
                ChkMantenimiento.Checked = True
            End If
            If nTECNICA = 1 Then
                ChkTecnica.Checked = True
            End If
            If nINGRESO = 1 Then
                ChkControl.Checked = True
            End If
            If nOTROSPER = 1 Then
                ChkPersonasOtros.Checked = True
            End If
            TxtPersonasOtros.Text = sDESCOTPER
            If nEQUIPPROPIO = 1 Then
                RadioButtonEquipa1.Checked = True
            End If

            'Cargo Integrantes
            BorrarTemporal()
            CargarTemporal(nCodigo)

            'Espacios Escenicos
            Dim nTipo As Integer = 0
            Dim ncantidad As Integer = 0
            Dim nAsientos As Integer = 0
            If nCANTESPACIOS >= 1 Then
                cn.Open()
                Dim Sqle1 As String = "select TIPO,CANTIDAD,ASIENTOS from REGISTROESPACIOS where CODIGOREGISTRO=" & nCodigo.ToString & " AND ESPACIO=1"
                Dim Psqle1 As New SqlClient.SqlCommand(Sqle1, cn)
                Dim dre1 As SqlClient.SqlDataReader = Psqle1.ExecuteReader
                While dre1.Read()
                    nTipo = dre1.GetInt32(0)
                    ncantidad = dre1.GetInt32(1)
                    nAsientos = dre1.GetInt32(2)
                End While
                dre1.Close()
                cn.Close()
                DdlTipoSala1.SelectedValue = nTipo
                TextBoxCant1.Text = ncantidad
                ddlTipoasien1.SelectedValue = nAsientos
            End If
            If nCANTESPACIOS >= 2 Then
                cn.Open()
                Dim Sqle2 As String = "select TIPO,CANTIDAD,ASIENTOS from REGISTROESPACIOS where CODIGOREGISTRO=" & nCodigo.ToString & " AND ESPACIO=2"
                Dim Psqle2 As New SqlClient.SqlCommand(Sqle2, cn)
                Dim dre2 As SqlClient.SqlDataReader = Psqle2.ExecuteReader
                While dre2.Read()
                    nTipo = dre2.GetInt32(0)
                    ncantidad = dre2.GetInt32(1)
                    nAsientos = dre2.GetInt32(2)
                End While
                dre2.Close()
                cn.Close()
                DdlTipoSala2.SelectedValue = nTipo
                TextBoxCant2.Text = ncantidad
                ddlTipoasien2.SelectedValue = nAsientos
            Else
                TablaEspacio2.Style.Item("display") = "none"
            End If
            If nCANTESPACIOS >= 3 Then
                cn.Open()
                Dim Sqle3 As String = "select TIPO,CANTIDAD,ASIENTOS from REGISTROESPACIOS where CODIGOREGISTRO=" & nCodigo.ToString & " AND ESPACIO=3"
                Dim Psqle3 As New SqlClient.SqlCommand(Sqle3, cn)
                Dim dre3 As SqlClient.SqlDataReader = Psqle3.ExecuteReader
                While dre3.Read()
                    nTipo = dre3.GetInt32(0)
                    ncantidad = dre3.GetInt32(1)
                    nAsientos = dre3.GetInt32(2)
                End While
                dre3.Close()
                cn.Close()
                DdlTipoSala3.SelectedValue = nTipo
                TextBoxCant3.Text = ncantidad
                ddlTipoasien3.SelectedValue = nAsientos
            Else
                TablaEspacio3.Style.Item("display") = "none"
            End If
            If nCANTESPACIOS = 4 Then
                cn.Open()
                Dim Sqle4 As String = "select TIPO,CANTIDAD,ASIENTOS from REGISTROESPACIOS where CODIGOREGISTRO=" & nCodigo.ToString & " AND ESPACIO=4"
                Dim Psqle4 As New SqlClient.SqlCommand(Sqle4, cn)
                Dim dre4 As SqlClient.SqlDataReader = Psqle4.ExecuteReader
                While dre4.Read()
                    nTipo = dre4.GetInt32(0)
                    ncantidad = dre4.GetInt32(1)
                    nAsientos = dre4.GetInt32(2)
                End While
                dre4.Close()
                cn.Close()
                DdlTipoSala4.SelectedValue = nTipo
                TextBoxCant4.Text = ncantidad
                ddlTipoasien4.SelectedValue = nAsientos
            Else
                TablaEspacio4.Style.Item("display") = "none"
            End If

        Catch ex As Exception
            txtErrorAcepto.Text = "Error al cargar Datos"
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
        sSQLCmd = "SELECT count(*) AS cantidad " &
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
        sSQLCmd = "SELECT count(*) AS cantidad " &
                        "FROM IntegrantesTemp t " &
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

    Private Sub GridView1_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles GridView1.RowDeleting
        Dim id As String
        id = GridView1.DataKeys(e.RowIndex).Value
        dsInte.DesvinculaIntegrante(ds, Convert.ToInt32(Session("codigo")), id)
        ds.AcceptChanges()
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
        CargAdjunto()
    End Sub

    Private Sub CreateDatatables()
        Dim mFotos As DataTable = New DataTable
        Dim column As DataColumn = New DataColumn
        Dim column1 As DataColumn = New DataColumn
        Dim column2 As DataColumn = New DataColumn
        column.DataType = System.Type.GetType("System.Int32")
        column.AllowDBNull = False
        column.Caption = "Identidad"
        column.ColumnName = "identidad"
        column.AutoIncrement = True
        column.Unique = True
        mFotos.Columns.Add(column)
        Dim keys(1) As DataColumn
        keys(0) = column
        mFotos.PrimaryKey = keys
        column1.DataType = System.Type.GetType("System.String")
        column1.AllowDBNull = True
        column1.Caption = "FilePath"
        column1.ColumnName = "FILEPATH"
        mFotos.Columns.Add(column1)
        column2.DataType = System.Type.GetType("System.String")
        column2.AllowDBNull = True
        column2.Caption = "Documento"
        column2.ColumnName = "DOCUMENTO"
        mFotos.Columns.Add(column2)
        Session("mFotos") = mFotos

        Dim mPlanos As DataTable = New DataTable
        Dim column0 As DataColumn = New DataColumn
        Dim column01 As DataColumn = New DataColumn
        Dim column02 As DataColumn = New DataColumn
        column0.DataType = System.Type.GetType("System.Int32")
        column0.AllowDBNull = False
        column0.Caption = "Identidad"
        column0.ColumnName = "identidad"
        column0.AutoIncrement = True
        column0.Unique = True
        mPlanos.Columns.Add(column0)
        Dim keys0(1) As DataColumn
        keys0(0) = column0
        mPlanos.PrimaryKey = keys0
        column01.DataType = System.Type.GetType("System.String")
        column01.AllowDBNull = True
        column01.Caption = "FilePath"
        column01.ColumnName = "FILEPATH"
        mPlanos.Columns.Add(column01)
        column02.DataType = System.Type.GetType("System.String")
        column02.AllowDBNull = True
        column02.Caption = "Documento"
        column02.ColumnName = "DOCUMENTO"
        mPlanos.Columns.Add(column02)
        Session("mPlanos") = mPlanos

    End Sub

    Protected Sub Borra_Foto_Click_Event(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim dt = CType(Session("mFotos"), DataTable)
        Dim Row As GridViewRow = CType(CType(sender, Control).Parent.Parent, GridViewRow)
        Dim index As Integer = Row.RowIndex
        Dim Identidad As String = RTrim(Row.Cells(0).Text)
        Dim i As Integer = 0
        Dim d As Integer = 0
        Do While Not i = dt.Rows.Count
            Dim r As DataRow = dt.Rows(i)
            Dim c As String = r.Item("identidad").ToString
            If c = Identidad Then
                d = i
            End If
            i += 1
        Loop
        dt.Rows.RemoveAt(d)
        dt.AcceptChanges()
        Session("mFotos") = dt
        GridView2.EditIndex = -1
        GridView2.DataSource = dt
        GridView2.DataBind()
    End Sub

    Protected Sub BtnAgregaFotos_Click(sender As Object, e As EventArgs) Handles BtnAgregaFotos.Click
        LblErrorFotos.Text = ""
        If UploadImportaFotos.HasFile Then
            Dim FileName As String = Path.GetFileName(UploadImportaFotos.PostedFile.FileName)
            Dim Extension As String = Path.GetExtension(UploadImportaFotos.PostedFile.FileName)
            Dim wfecha As Date = DateTime.Now.ToString
            Dim wdia As Integer = wfecha.Day
            Dim wmes As Integer = wfecha.Month
            Dim wano As Integer = wfecha.Year
            Dim whora As Integer = wfecha.Hour
            Dim wminu As Integer = wfecha.Minute
            Dim wsegu As Integer = wfecha.Second
            Dim woperador As String = Session("CUIT")
            Dim wdir As String = wano.ToString + wmes.ToString + wdia.ToString + whora.ToString + wminu.ToString + wsegu.ToString
            If UCase(Extension) = ".JPG" Or UCase(Extension) = ".JPEG" Then
                Dim sizeInBytes As Long = UploadImportaFotos.PostedFile.ContentLength
                If sizeInBytes / 1000000 > 10 Then
                    LblErrorFotos.Text = "La Foto tiene un tamaño mayor a 10 Mb"
                    Return
                End If
                Dim randomName As String = RTrim(woperador) + wdir
                If Not Directory.Exists(Server.MapPath("~/Temp/" & randomName & "/")) Then
                    Directory.CreateDirectory(Server.MapPath("~/Temp/" & randomName & "/"))
                End If
                Dim FilePath As String = (Server.MapPath("~/Temp/" & randomName & "/")) + FileName
                Try
                    UploadImportaFotos.SaveAs(FilePath)
                Catch ex As Exception
                End Try
                Dim dt = CType(Session("mFotos"), DataTable)
                Dim R As DataRow = dt.NewRow
                R("FILEPATH") = FilePath
                R("DOCUMENTO") = FileName
                dt.Rows.Add(R)
                GridView2.DataSource = dt
                GridView2.DataBind()
            Else
                LblErrorFotos.Text = "No es una imagen .JPG o .JPEG"
                Return
            End If
        End If
    End Sub

    Protected Sub BtnVisualizaFotos_Click(sender As Object, e As EventArgs) Handles BtnVisualizaFotos.Click
        LblErrorFotos.Text = ""
        If UploadImportaFotos.HasFile Then
            Dim woperador As String = Session("CUIT")
            Dim FileName As String = Path.GetFileName(UploadImportaFotos.PostedFile.FileName)
            Dim Extension As String = Path.GetExtension(UploadImportaFotos.PostedFile.FileName)
            Dim FolderPath As String = ConfigurationManager.AppSettings("FolderPath")
            Dim FilePath As String = ""
            Dim wfecha As Date = DateTime.Now.ToString
            Dim wdia As Integer = wfecha.Day
            Dim wmes As Integer = wfecha.Month
            Dim wano As Integer = wfecha.Year
            Dim whora As Integer = wfecha.Hour
            Dim wminu As Integer = wfecha.Minute
            Dim wsegu As Integer = wfecha.Second
            Dim wdir As String = wano.ToString + wmes.ToString + wdia.ToString + whora.ToString + wminu.ToString + wsegu.ToString
            If UCase(Extension) <> ".JPG" And UCase(Extension) <> ".JPEG" Then
                LblErrorFotos.Text = "No es una imagen .JPG o .JPEG"
                Return
            End If
            Dim randomName As String = RTrim(woperador) + wdir
            If Not Directory.Exists(Server.MapPath("~/Temp/" & randomName & "/")) Then
                Directory.CreateDirectory(Server.MapPath("~/Temp/" & randomName & "/"))
            End If
            FilePath = (Server.MapPath("~/Temp/" & randomName & "/")) + FileName
            Try
                UploadImportaFotos.SaveAs(FilePath)
            Catch ex As Exception
            End Try

            If UCase(Extension) = ".JPG" Then
                Response.ContentType = "image/jpg"
                Response.AppendHeader("Content-Disposition", "attachment;filename=" & FilePath)
                Response.TransmitFile(FilePath)
                Response.End()
            End If

            If UCase(Extension) = ".JPEG" Then
                Response.ContentType = "image/jpeg"
                Response.AppendHeader("Content-Disposition", "attachment;filename=" & FilePath)
                Response.TransmitFile(FilePath)
                Response.End()
            End If
        End If
    End Sub

    Protected Sub Borra_Plano_Click_Event(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim dt = CType(Session("mPlanos"), DataTable)
        Dim Row As GridViewRow = CType(CType(sender, Control).Parent.Parent, GridViewRow)
        Dim index As Integer = Row.RowIndex
        Dim Identidad As String = RTrim(Row.Cells(0).Text)
        Dim i As Integer = 0
        Dim d As Integer = 0
        Do While Not i = dt.Rows.Count
            Dim r As DataRow = dt.Rows(i)
            Dim c As String = r.Item("identidad").ToString
            If c = Identidad Then
                d = i
            End If
            i += 1
        Loop
        dt.Rows.RemoveAt(d)
        dt.AcceptChanges()
        Session("mPlanos") = dt
        GridView3.EditIndex = -1
        GridView3.DataSource = dt
        GridView3.DataBind()
    End Sub

    Protected Sub BtnAgregaPlanos_Click(sender As Object, e As EventArgs) Handles BtnAgregaPlanos.Click
        LblErrorPlanos.Text = ""
        If UploadImportaPlanos.HasFile Then
            Dim FileName As String = Path.GetFileName(UploadImportaPlanos.PostedFile.FileName)
            Dim Extension As String = Path.GetExtension(UploadImportaPlanos.PostedFile.FileName)
            Dim wfecha As Date = DateTime.Now.ToString
            Dim wdia As Integer = wfecha.Day
            Dim wmes As Integer = wfecha.Month
            Dim wano As Integer = wfecha.Year
            Dim whora As Integer = wfecha.Hour
            Dim wminu As Integer = wfecha.Minute
            Dim wsegu As Integer = wfecha.Second
            Dim woperador As String = Session("CUIT")
            Dim wdir As String = wano.ToString + wmes.ToString + wdia.ToString + whora.ToString + wminu.ToString + wsegu.ToString
            If UCase(Extension) = ".PDF" Or UCase(Extension) = ".JPG" Or UCase(Extension) = ".JPEG" Then
                Dim sizeInBytes As Long = UploadImportaPlanos.PostedFile.ContentLength
                If sizeInBytes / 1000000 > 10 Then
                    LblErrorPlanos.Text = "El plano tiene un tamaño mayor a 10 Mb"
                    Return
                End If
                Dim randomName As String = RTrim(woperador) + wdir
                If Not Directory.Exists(Server.MapPath("~/Temp/" & randomName & "/")) Then
                    Directory.CreateDirectory(Server.MapPath("~/Temp/" & randomName & "/"))
                End If
                Dim FilePath As String = (Server.MapPath("~/Temp/" & randomName & "/")) + FileName
                Try
                    UploadImportaPlanos.SaveAs(FilePath)
                Catch ex As Exception
                End Try
                Dim dt = CType(Session("mPlanos"), DataTable)
                Dim R As DataRow = dt.NewRow
                R("FILEPATH") = FilePath
                R("DOCUMENTO") = FileName
                dt.Rows.Add(R)
                GridView3.DataSource = dt
                GridView3.DataBind()
            Else
                LblErrorPlanos.Text = "No es un archivo Adobe .PDF o una imagen .JPG o .JPEG"
                Return
            End If
        End If

    End Sub

    Protected Sub BtnVisualizaPlanos_Click(sender As Object, e As EventArgs) Handles BtnVisualizaPlanos.Click
        LblErrorPlanos.Text = ""
        If UploadImportaPlanos.HasFile Then
            Dim woperador As String = Session("CUIT")
            Dim FileName As String = Path.GetFileName(UploadImportaPlanos.PostedFile.FileName)
            Dim Extension As String = Path.GetExtension(UploadImportaPlanos.PostedFile.FileName)
            Dim FolderPath As String = ConfigurationManager.AppSettings("FolderPath")
            Dim FilePath As String = ""
            Dim wfecha As Date = DateTime.Now.ToString
            Dim wdia As Integer = wfecha.Day
            Dim wmes As Integer = wfecha.Month
            Dim wano As Integer = wfecha.Year
            Dim whora As Integer = wfecha.Hour
            Dim wminu As Integer = wfecha.Minute
            Dim wsegu As Integer = wfecha.Second
            Dim wdir As String = wano.ToString + wmes.ToString + wdia.ToString + whora.ToString + wminu.ToString + wsegu.ToString

            If UCase(Extension) <> ".PDF" And UCase(Extension) <> ".JPG" And UCase(Extension) <> ".JPEG" Then
                LblErrorFotos.Text = "No es un archivo Adobe .PDF o una imagen .JPG o .JPEG"
                Return
            End If
            Dim randomName As String = RTrim(woperador) + wdir
            If Not Directory.Exists(Server.MapPath("~/Temp/" & randomName & "/")) Then
                Directory.CreateDirectory(Server.MapPath("~/Temp/" & randomName & "/"))
            End If
            FilePath = (Server.MapPath("~/Temp/" & randomName & "/")) + FileName
            Try
                UploadImportaPlanos.SaveAs(FilePath)
            Catch ex As Exception
            End Try

            If UCase(Extension) = ".PDF" Then
                Response.ContentType = "application/pdf"
                Response.AppendHeader("Content-Disposition", "attachment;filename=" & FilePath)
                Response.TransmitFile(FilePath)
                Response.End()
            End If

            If UCase(Extension) = ".JPG" Then
                Response.ContentType = "image/jpg"
                Response.AppendHeader("Content-Disposition", "attachment;filename=" & FilePath)
                Response.TransmitFile(FilePath)
                Response.End()
            End If

            If UCase(Extension) = ".JPEG" Then
                Response.ContentType = "image/jpeg"
                Response.AppendHeader("Content-Disposition", "attachment;filename=" & FilePath)
                Response.TransmitFile(FilePath)
                Response.End()
            End If
        End If

    End Sub

    Protected Sub BtnVisualizaEquipa_Click(sender As Object, e As EventArgs) Handles BtnVisualizaEquipa.Click
        txtErrorEquipamiento.Text = ""
        If FileUploadEquipa.HasFile Or Session("FileUploadEquipa1") IsNot Nothing Then
            Dim woperador As String = Session("CUIT")
            Dim FileName As String = Path.GetFileName(FileUploadEquipa.PostedFile.FileName)
            Dim Extension As String = Path.GetExtension(FileUploadEquipa.PostedFile.FileName)
            Dim FolderPath As String = ConfigurationManager.AppSettings("FolderPath")
            Dim FilePath As String = ""
            If FileUploadEquipa.HasFile Then
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
            If FileUploadEquipaSub.HasFile Then
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

    Protected Sub BtnPlanoEscena1_Click(sender As Object, e As EventArgs) Handles BtnPlanoEscena1.Click
        LblErrorEspacio1.Text = ""
        If FileUploadPlanoEscena1.HasFile Or Session("FileUploadPlanoEscena11") IsNot Nothing Then
            Dim woperador As String = Session("CUIT")
            Dim FileName As String = Path.GetFileName(FileUploadPlanoEscena1.PostedFile.FileName)
            Dim Extension As String = Path.GetExtension(FileUploadPlanoEscena1.PostedFile.FileName)
            Dim FolderPath As String = ConfigurationManager.AppSettings("FolderPath")
            Dim FilePath As String = ""
            If FileUploadPlanoEscena1.HasFile Then
                Dim wfecha As Date = DateTime.Now.ToString
                Dim wdia As Integer = wfecha.Day
                Dim wmes As Integer = wfecha.Month
                Dim wano As Integer = wfecha.Year
                Dim whora As Integer = wfecha.Hour
                Dim wminu As Integer = wfecha.Minute
                Dim wsegu As Integer = wfecha.Second
                Dim wdir As String = wano.ToString + wmes.ToString + wdia.ToString + whora.ToString + wminu.ToString + wsegu.ToString
                If UCase(Extension) <> ".PDF" And UCase(Extension) <> ".JPG" And UCase(Extension) <> ".JPEG" Then
                    LblErrorEspacio1.Text = "No es un documento Adobe .PDF o imagen .JPG .JPEG"
                    Return
                End If
                Dim sizeInBytes As Long = FileUploadPlanoEscena1.PostedFile.ContentLength
                If sizeInBytes / 1000000 > 10 Then
                    LblErrorEspacio1.Text = "El documento tiene un tamaño mayor a 10 Mb"
                    Return
                End If
                Dim randomName As String = RTrim(woperador) + wdir
                If Not Directory.Exists(Server.MapPath("~/Temp/" & randomName & "/")) Then
                    Directory.CreateDirectory(Server.MapPath("~/Temp/" & randomName & "/"))
                End If
                FilePath = (Server.MapPath("~/Temp/" & randomName & "/")) + FileName
                Try
                    FileUploadPlanoEscena1.SaveAs(FilePath)
                Catch ex As Exception
                End Try
                Session("FileUploadPlanoEscena11") = FileUploadPlanoEscena1
                Session("FileUploadPlanoEscena1FileName") = FilePath
            Else
                FilePath = Session("FileUploadPlanoEscena1FileName")
                Dim letra As String = Right(FilePath.Trim, 5)
                If Left(letra, 1) = "." Then
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

            If UCase(Extension) = ".JPG" Then
                Response.ContentType = "image/jpg"
                Response.AppendHeader("Content-Disposition", "attachment;filename=" & FilePath)
                Response.TransmitFile(FilePath)
                Response.End()
            End If

            If UCase(Extension) = ".JPEG" Then
                Response.ContentType = "image/jpeg"
                Response.AppendHeader("Content-Disposition", "attachment;filename=" & FilePath)
                Response.TransmitFile(FilePath)
                Response.End()
            End If

        End If
    End Sub

    Protected Sub BtnPlantaLuz1_Click(sender As Object, e As EventArgs) Handles BtnPlantaLuz1.Click
        LblErrorEspacio1.Text = ""
        If FileUploadPlantaLuz1.HasFile Or Session("FileUploadPlantaLuz11") IsNot Nothing Then
            Dim woperador As String = Session("CUIT")
            Dim FileName As String = Path.GetFileName(FileUploadPlantaLuz1.PostedFile.FileName)
            Dim Extension As String = Path.GetExtension(FileUploadPlantaLuz1.PostedFile.FileName)
            Dim FolderPath As String = ConfigurationManager.AppSettings("FolderPath")
            Dim FilePath As String = ""
            If FileUploadPlantaLuz1.HasFile Then
                Dim wfecha As Date = DateTime.Now.ToString
                Dim wdia As Integer = wfecha.Day
                Dim wmes As Integer = wfecha.Month
                Dim wano As Integer = wfecha.Year
                Dim whora As Integer = wfecha.Hour
                Dim wminu As Integer = wfecha.Minute
                Dim wsegu As Integer = wfecha.Second
                Dim wdir As String = wano.ToString + wmes.ToString + wdia.ToString + whora.ToString + wminu.ToString + wsegu.ToString

                If UCase(Extension) <> ".PDF" And UCase(Extension) <> ".JPG" And UCase(Extension) <> ".JPEG" Then
                    LblErrorEspacio1.Text = "No es un documento Adobe .PDF o imagen .JPG .JPEG"
                    Return
                End If
                Dim sizeInBytes As Long = FileUploadPlantaLuz1.PostedFile.ContentLength
                If sizeInBytes / 1000000 > 10 Then
                    LblErrorEspacio1.Text = "El documento tiene un tamaño mayor a 10 Mb"
                    Return
                End If

                Dim randomName As String = RTrim(woperador) + wdir
                If Not Directory.Exists(Server.MapPath("~/Temp/" & randomName & "/")) Then
                    Directory.CreateDirectory(Server.MapPath("~/Temp/" & randomName & "/"))
                End If
                FilePath = (Server.MapPath("~/Temp/" & randomName & "/")) + FileName
                Try
                    FileUploadPlantaLuz1.SaveAs(FilePath)
                Catch ex As Exception
                End Try
                Session("FileUploadPlantaLuz11") = FileUploadPlantaLuz1
                Session("FileUploadPlantaLuz1FileName") = FilePath
            Else
                FilePath = Session("FileUploadPlantaLuz1FileName")
                Dim letra As String = Right(FilePath.Trim, 5)
                If Left(letra, 1) = "." Then
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

            If UCase(Extension) = ".JPG" Then
                Response.ContentType = "image/jpg"
                Response.AppendHeader("Content-Disposition", "attachment;filename=" & FilePath)
                Response.TransmitFile(FilePath)
                Response.End()
            End If

            If UCase(Extension) = ".JPEG" Then
                Response.ContentType = "image/jpeg"
                Response.AppendHeader("Content-Disposition", "attachment;filename=" & FilePath)
                Response.TransmitFile(FilePath)
                Response.End()
            End If

        End If
    End Sub

    Protected Sub BtnFotoPlanoEscena1_Click(sender As Object, e As EventArgs) Handles BtnFotoPlanoEscena1.Click
        LblErrorEspacio1.Text = ""
        If FileUploadFotoEscena1.HasFile Or Session("FileUploadFotoEscena11") IsNot Nothing Then
            Dim woperador As String = Session("CUIT")
            Dim FileName As String = Path.GetFileName(FileUploadFotoEscena1.PostedFile.FileName)
            Dim Extension As String = Path.GetExtension(FileUploadFotoEscena1.PostedFile.FileName)
            Dim FolderPath As String = ConfigurationManager.AppSettings("FolderPath")
            Dim FilePath As String = ""
            If FileUploadFotoEscena1.HasFile Then
                Dim wfecha As Date = DateTime.Now.ToString
                Dim wdia As Integer = wfecha.Day
                Dim wmes As Integer = wfecha.Month
                Dim wano As Integer = wfecha.Year
                Dim whora As Integer = wfecha.Hour
                Dim wminu As Integer = wfecha.Minute
                Dim wsegu As Integer = wfecha.Second
                Dim wdir As String = wano.ToString + wmes.ToString + wdia.ToString + whora.ToString + wminu.ToString + wsegu.ToString

                If UCase(Extension) <> ".JPG" And UCase(Extension) <> ".JPEG" Then
                    LblErrorEspacio1.Text = "No es una imagen .JPG .JPEG"
                    Return
                End If
                Dim sizeInBytes As Long = FileUploadFotoEscena1.PostedFile.ContentLength
                If sizeInBytes / 1000000 > 10 Then
                    LblErrorEspacio1.Text = "El documento tiene un tamaño mayor a 10 Mb"
                    Return
                End If

                Dim randomName As String = RTrim(woperador) + wdir
                If Not Directory.Exists(Server.MapPath("~/Temp/" & randomName & "/")) Then
                    Directory.CreateDirectory(Server.MapPath("~/Temp/" & randomName & "/"))
                End If
                FilePath = (Server.MapPath("~/Temp/" & randomName & "/")) + FileName
                Try
                    FileUploadFotoEscena1.SaveAs(FilePath)
                Catch ex As Exception
                End Try
                Session("FileUploadFotoEscena11") = FileUploadFotoEscena1
                Session("FileUploadFotoEscena1FileName") = FilePath
            Else
                FilePath = Session("FileUploadFotoEscena1FileName")
                Dim letra As String = Right(FilePath.Trim, 5)
                If Left(letra, 1) = "." Then
                    Extension = Right(FilePath.Trim, 5)
                Else
                    Extension = Right(FilePath.Trim, 4)
                End If
            End If

            If UCase(Extension) = ".JPG" Then
                Response.ContentType = "image/jpg"
                Response.AppendHeader("Content-Disposition", "attachment;filename=" & FilePath)
                Response.TransmitFile(FilePath)
                Response.End()
            End If

            If UCase(Extension) = ".JPEG" Then
                Response.ContentType = "image/jpeg"
                Response.AppendHeader("Content-Disposition", "attachment;filename=" & FilePath)
                Response.TransmitFile(FilePath)
                Response.End()
            End If

        End If
    End Sub

    Protected Sub BtnPlanoEscena2_Click(sender As Object, e As EventArgs) Handles BtnPlanoEscena2.Click
        LblErrorEspacio2.Text = ""
        If FileUploadPlanoEscena2.HasFile Or Session("FileUploadPlanoEscena21") IsNot Nothing Then
            Dim woperador As String = Session("CUIT")
            Dim FileName As String = Path.GetFileName(FileUploadPlanoEscena2.PostedFile.FileName)
            Dim Extension As String = Path.GetExtension(FileUploadPlanoEscena2.PostedFile.FileName)
            Dim FolderPath As String = ConfigurationManager.AppSettings("FolderPath")
            Dim FilePath As String = ""
            If FileUploadPlanoEscena2.HasFile Then
                Dim wfecha As Date = DateTime.Now.ToString
                Dim wdia As Integer = wfecha.Day
                Dim wmes As Integer = wfecha.Month
                Dim wano As Integer = wfecha.Year
                Dim whora As Integer = wfecha.Hour
                Dim wminu As Integer = wfecha.Minute
                Dim wsegu As Integer = wfecha.Second
                Dim wdir As String = wano.ToString + wmes.ToString + wdia.ToString + whora.ToString + wminu.ToString + wsegu.ToString

                If UCase(Extension) <> ".PDF" And UCase(Extension) <> ".JPG" And UCase(Extension) <> ".JPEG" Then
                    LblErrorEspacio2.Text = "No es un documento Adobe .PDF o imagen .JPG .JPEG"
                    Return
                End If
                Dim sizeInBytes As Long = FileUploadFotoEscena2.PostedFile.ContentLength
                If sizeInBytes / 1000000 > 10 Then
                    LblErrorEspacio2.Text = "El documento tiene un tamaño mayor a 10 Mb"
                    Return
                End If

                Dim randomName As String = RTrim(woperador) + wdir
                If Not Directory.Exists(Server.MapPath("~/Temp/" & randomName & "/")) Then
                    Directory.CreateDirectory(Server.MapPath("~/Temp/" & randomName & "/"))
                End If
                FilePath = (Server.MapPath("~/Temp/" & randomName & "/")) + FileName
                Try
                    FileUploadPlanoEscena2.SaveAs(FilePath)
                Catch ex As Exception
                End Try
                Session("FileUploadPlanoEscena21") = FileUploadPlanoEscena2
                Session("FileUploadPlanoEscena2FileName") = FilePath
            Else
                FilePath = Session("FileUploadPlanoEscena2FileName")
                Dim letra As String = Right(FilePath.Trim, 5)
                If Left(letra, 1) = "." Then
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

            If UCase(Extension) = ".JPG" Then
                Response.ContentType = "image/jpg"
                Response.AppendHeader("Content-Disposition", "attachment;filename=" & FilePath)
                Response.TransmitFile(FilePath)
                Response.End()
            End If

            If UCase(Extension) = ".JPEG" Then
                Response.ContentType = "image/jpeg"
                Response.AppendHeader("Content-Disposition", "attachment;filename=" & FilePath)
                Response.TransmitFile(FilePath)
                Response.End()
            End If

        End If
    End Sub

    Protected Sub BtnPlantaLuz2_Click(sender As Object, e As EventArgs) Handles BtnPlantaLuz2.Click
        LblErrorEspacio2.Text = ""
        If FileUploadPlantaLuz2.HasFile Or Session("FileUploadPlantaLuz21") IsNot Nothing Then
            Dim woperador As String = Session("CUIT")
            Dim FileName As String = Path.GetFileName(FileUploadPlantaLuz2.PostedFile.FileName)
            Dim Extension As String = Path.GetExtension(FileUploadPlantaLuz2.PostedFile.FileName)
            Dim FolderPath As String = ConfigurationManager.AppSettings("FolderPath")
            Dim FilePath As String = ""
            If FileUploadPlantaLuz2.HasFile Then
                Dim wfecha As Date = DateTime.Now.ToString
                Dim wdia As Integer = wfecha.Day
                Dim wmes As Integer = wfecha.Month
                Dim wano As Integer = wfecha.Year
                Dim whora As Integer = wfecha.Hour
                Dim wminu As Integer = wfecha.Minute
                Dim wsegu As Integer = wfecha.Second
                Dim wdir As String = wano.ToString + wmes.ToString + wdia.ToString + whora.ToString + wminu.ToString + wsegu.ToString

                If UCase(Extension) <> ".PDF" And UCase(Extension) <> ".JPG" And UCase(Extension) <> ".JPEG" Then
                    LblErrorEspacio2.Text = "No es un documento Adobe .PDF o imagen .JPG .JPEG"
                    Return
                End If
                Dim sizeInBytes As Long = FileUploadPlantaLuz2.PostedFile.ContentLength
                If sizeInBytes / 1000000 > 10 Then
                    LblErrorEspacio2.Text = "El documento tiene un tamaño mayor a 10 Mb"
                    Return
                End If

                Dim randomName As String = RTrim(woperador) + wdir
                If Not Directory.Exists(Server.MapPath("~/Temp/" & randomName & "/")) Then
                    Directory.CreateDirectory(Server.MapPath("~/Temp/" & randomName & "/"))
                End If
                FilePath = (Server.MapPath("~/Temp/" & randomName & "/")) + FileName
                Try
                    FileUploadPlantaLuz2.SaveAs(FilePath)
                Catch ex As Exception
                End Try
                Session("FileUploadPlantaLuz21") = FileUploadPlantaLuz2
                Session("FileUploadPlantaLuz2FileName") = FilePath
            Else
                FilePath = Session("FileUploadPlantaLuz2FileName")
                Dim letra As String = Right(FilePath.Trim, 5)
                If Left(letra, 1) = "." Then
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

            If UCase(Extension) = ".JPG" Then
                Response.ContentType = "image/jpg"
                Response.AppendHeader("Content-Disposition", "attachment;filename=" & FilePath)
                Response.TransmitFile(FilePath)
                Response.End()
            End If

            If UCase(Extension) = ".JPEG" Then
                Response.ContentType = "image/jpeg"
                Response.AppendHeader("Content-Disposition", "attachment;filename=" & FilePath)
                Response.TransmitFile(FilePath)
                Response.End()
            End If

        End If
    End Sub

    Protected Sub BtnFotoPlanoEscena2_Click(sender As Object, e As EventArgs) Handles BtnFotoPlanoEscena2.Click
        LblErrorEspacio2.Text = ""
        If FileUploadFotoEscena2.HasFile Or Session("FileUploadFotoEscena21") IsNot Nothing Then
            Dim woperador As String = Session("CUIT")
            Dim FileName As String = Path.GetFileName(FileUploadFotoEscena2.PostedFile.FileName)
            Dim Extension As String = Path.GetExtension(FileUploadFotoEscena2.PostedFile.FileName)
            Dim FolderPath As String = ConfigurationManager.AppSettings("FolderPath")
            Dim FilePath As String = ""
            If FileUploadFotoEscena2.HasFile Then
                Dim wfecha As Date = DateTime.Now.ToString
                Dim wdia As Integer = wfecha.Day
                Dim wmes As Integer = wfecha.Month
                Dim wano As Integer = wfecha.Year
                Dim whora As Integer = wfecha.Hour
                Dim wminu As Integer = wfecha.Minute
                Dim wsegu As Integer = wfecha.Second
                Dim wdir As String = wano.ToString + wmes.ToString + wdia.ToString + whora.ToString + wminu.ToString + wsegu.ToString

                If UCase(Extension) <> ".JPG" And UCase(Extension) <> ".JPEG" Then
                    LblErrorEspacio2.Text = "No es una imagen .JPG .JPEG"
                    Return
                End If
                Dim sizeInBytes As Long = FileUploadFotoEscena2.PostedFile.ContentLength
                If sizeInBytes / 1000000 > 10 Then
                    LblErrorEspacio2.Text = "El documento tiene un tamaño mayor a 10 Mb"
                    Return
                End If
                Dim randomName As String = RTrim(woperador) + wdir
                If Not Directory.Exists(Server.MapPath("~/Temp/" & randomName & "/")) Then
                    Directory.CreateDirectory(Server.MapPath("~/Temp/" & randomName & "/"))
                End If
                FilePath = (Server.MapPath("~/Temp/" & randomName & "/")) + FileName
                Try
                    FileUploadFotoEscena2.SaveAs(FilePath)
                Catch ex As Exception
                End Try
                Session("FileUploadFotoEscena21") = FileUploadFotoEscena2
                Session("FileUploadFotoEscena2FileName") = FilePath
            Else
                FilePath = Session("FileUploadFotoEscena2FileName")
                Dim letra As String = Right(FilePath.Trim, 5)
                If Left(letra, 1) = "." Then
                    Extension = Right(FilePath.Trim, 5)
                Else
                    Extension = Right(FilePath.Trim, 4)
                End If
            End If

            If UCase(Extension) = ".JPG" Then
                Response.ContentType = "image/jpg"
                Response.AppendHeader("Content-Disposition", "attachment;filename=" & FilePath)
                Response.TransmitFile(FilePath)
                Response.End()
            End If

            If UCase(Extension) = ".JPEG" Then
                Response.ContentType = "image/jpeg"
                Response.AppendHeader("Content-Disposition", "attachment;filename=" & FilePath)
                Response.TransmitFile(FilePath)
                Response.End()
            End If

        End If
    End Sub

    Protected Sub BtnPlanoEscena3_Click(sender As Object, e As EventArgs) Handles BtnPlanoEscena3.Click
        LblErrorEspacio3.Text = ""
        If FileUploadPlanoEscena3.HasFile Or Session("FileUploadPlanoEscena31") IsNot Nothing Then
            Dim woperador As String = Session("CUIT")
            Dim FileName As String = Path.GetFileName(FileUploadPlanoEscena3.PostedFile.FileName)
            Dim Extension As String = Path.GetExtension(FileUploadPlanoEscena3.PostedFile.FileName)
            Dim FolderPath As String = ConfigurationManager.AppSettings("FolderPath")
            Dim FilePath As String = ""
            If FileUploadPlanoEscena3.HasFile Then
                Dim wfecha As Date = DateTime.Now.ToString
                Dim wdia As Integer = wfecha.Day
                Dim wmes As Integer = wfecha.Month
                Dim wano As Integer = wfecha.Year
                Dim whora As Integer = wfecha.Hour
                Dim wminu As Integer = wfecha.Minute
                Dim wsegu As Integer = wfecha.Second
                Dim wdir As String = wano.ToString + wmes.ToString + wdia.ToString + whora.ToString + wminu.ToString + wsegu.ToString

                If UCase(Extension) <> ".PDF" And UCase(Extension) <> ".JPG" And UCase(Extension) <> ".JPEG" Then
                    LblErrorEspacio3.Text = "No es un documento Adobe .PDF o imagen .JPG .JPEG"
                    Return
                End If
                Dim sizeInBytes As Long = FileUploadPlanoEscena3.PostedFile.ContentLength
                If sizeInBytes / 1000000 > 10 Then
                    LblErrorEspacio3.Text = "El documento tiene un tamaño mayor a 10 Mb"
                    Return
                End If
                Dim randomName As String = RTrim(woperador) + wdir
                If Not Directory.Exists(Server.MapPath("~/Temp/" & randomName & "/")) Then
                    Directory.CreateDirectory(Server.MapPath("~/Temp/" & randomName & "/"))
                End If
                FilePath = (Server.MapPath("~/Temp/" & randomName & "/")) + FileName
                Try
                    FileUploadPlanoEscena3.SaveAs(FilePath)
                Catch ex As Exception
                End Try
                Session("FileUploadPlanoEscena31") = FileUploadPlanoEscena3
                Session("FileUploadPlanoEscena3FileName") = FilePath
            Else
                FilePath = Session("FileUploadPlanoEscena3FileName")
                Dim letra As String = Right(FilePath.Trim, 5)
                If Left(letra, 1) = "." Then
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

            If UCase(Extension) = ".JPG" Then
                Response.ContentType = "image/jpg"
                Response.AppendHeader("Content-Disposition", "attachment;filename=" & FilePath)
                Response.TransmitFile(FilePath)
                Response.End()
            End If

            If UCase(Extension) = ".JPEG" Then
                Response.ContentType = "image/jpeg"
                Response.AppendHeader("Content-Disposition", "attachment;filename=" & FilePath)
                Response.TransmitFile(FilePath)
                Response.End()
            End If

        End If
    End Sub

    Protected Sub BtnPlantaLuz3_Click(sender As Object, e As EventArgs) Handles BtnPlantaLuz3.Click
        LblErrorEspacio3.Text = ""
        If FileUploadPlantaLuz3.HasFile Or Session("FileUploadPlantaLuz31") IsNot Nothing Then
            Dim woperador As String = Session("CUIT")
            Dim FileName As String = Path.GetFileName(FileUploadPlantaLuz3.PostedFile.FileName)
            Dim Extension As String = Path.GetExtension(FileUploadPlantaLuz3.PostedFile.FileName)
            Dim FolderPath As String = ConfigurationManager.AppSettings("FolderPath")
            Dim FilePath As String = ""
            If FileUploadPlantaLuz3.HasFile Then
                Dim wfecha As Date = DateTime.Now.ToString
                Dim wdia As Integer = wfecha.Day
                Dim wmes As Integer = wfecha.Month
                Dim wano As Integer = wfecha.Year
                Dim whora As Integer = wfecha.Hour
                Dim wminu As Integer = wfecha.Minute
                Dim wsegu As Integer = wfecha.Second
                Dim wdir As String = wano.ToString + wmes.ToString + wdia.ToString + whora.ToString + wminu.ToString + wsegu.ToString

                If UCase(Extension) <> ".PDF" And UCase(Extension) <> ".JPG" And UCase(Extension) <> ".JPEG" Then
                    LblErrorEspacio3.Text = "No es un documento Adobe .PDF o imagen .JPG .JPEG"
                    Return
                End If
                Dim sizeInBytes As Long = FileUploadPlantaLuz3.PostedFile.ContentLength
                If sizeInBytes / 1000000 > 10 Then
                    LblErrorEspacio3.Text = "El documento tiene un tamaño mayor a 10 Mb"
                    Return
                End If
                Dim randomName As String = RTrim(woperador) + wdir
                If Not Directory.Exists(Server.MapPath("~/Temp/" & randomName & "/")) Then
                    Directory.CreateDirectory(Server.MapPath("~/Temp/" & randomName & "/"))
                End If
                FilePath = (Server.MapPath("~/Temp/" & randomName & "/")) + FileName
                Try
                    FileUploadPlantaLuz1.SaveAs(FilePath)
                Catch ex As Exception
                End Try
                Session("FileUploadPlantaLuz31") = FileUploadPlantaLuz3
                Session("FileUploadPlantaLuz3FileName") = FilePath
            Else
                FilePath = Session("FileUploadPlantaLuz3FileName")
                Dim letra As String = Right(FilePath.Trim, 5)
                If Left(letra, 1) = "." Then
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

            If UCase(Extension) = ".JPG" Then
                Response.ContentType = "image/jpg"
                Response.AppendHeader("Content-Disposition", "attachment;filename=" & FilePath)
                Response.TransmitFile(FilePath)
                Response.End()
            End If

            If UCase(Extension) = ".JPEG" Then
                Response.ContentType = "image/jpeg"
                Response.AppendHeader("Content-Disposition", "attachment;filename=" & FilePath)
                Response.TransmitFile(FilePath)
                Response.End()
            End If

        End If
    End Sub

    Protected Sub BtnFotoPlanoEscena3_Click(sender As Object, e As EventArgs) Handles BtnFotoPlanoEscena3.Click
        LblErrorEspacio3.Text = ""
        If FileUploadFotoEscena3.HasFile Or Session("FileUploadFotoEscena31") IsNot Nothing Then
            Dim woperador As String = Session("CUIT")
            Dim FileName As String = Path.GetFileName(FileUploadFotoEscena3.PostedFile.FileName)
            Dim Extension As String = Path.GetExtension(FileUploadFotoEscena3.PostedFile.FileName)
            Dim FolderPath As String = ConfigurationManager.AppSettings("FolderPath")
            Dim FilePath As String = ""
            If FileUploadFotoEscena3.HasFile Then
                Dim wfecha As Date = DateTime.Now.ToString
                Dim wdia As Integer = wfecha.Day
                Dim wmes As Integer = wfecha.Month
                Dim wano As Integer = wfecha.Year
                Dim whora As Integer = wfecha.Hour
                Dim wminu As Integer = wfecha.Minute
                Dim wsegu As Integer = wfecha.Second
                Dim wdir As String = wano.ToString + wmes.ToString + wdia.ToString + whora.ToString + wminu.ToString + wsegu.ToString

                If UCase(Extension) <> ".JPG" And UCase(Extension) <> ".JPEG" Then
                    LblErrorEspacio3.Text = "No es una imagen .JPG .JPEG"
                    Return
                End If
                Dim sizeInBytes As Long = FileUploadFotoEscena3.PostedFile.ContentLength
                If sizeInBytes / 1000000 > 10 Then
                    LblErrorEspacio3.Text = "El documento tiene un tamaño mayor a 10 Mb"
                    Return
                End If
                Dim randomName As String = RTrim(woperador) + wdir
                If Not Directory.Exists(Server.MapPath("~/Temp/" & randomName & "/")) Then
                    Directory.CreateDirectory(Server.MapPath("~/Temp/" & randomName & "/"))
                End If
                FilePath = (Server.MapPath("~/Temp/" & randomName & "/")) + FileName
                Try
                    FileUploadFotoEscena3.SaveAs(FilePath)
                Catch ex As Exception
                End Try
                Session("FileUploadFotoEscena31") = FileUploadFotoEscena3
                Session("FileUploadFotoEscena3FileName") = FilePath
            Else
                FilePath = Session("FileUploadFotoEscena3FileName")
                Dim letra As String = Right(FilePath.Trim, 5)
                If Left(letra, 1) = "." Then
                    Extension = Right(FilePath.Trim, 5)
                Else
                    Extension = Right(FilePath.Trim, 4)
                End If
            End If

            If UCase(Extension) = ".JPG" Then
                Response.ContentType = "image/jpg"
                Response.AppendHeader("Content-Disposition", "attachment;filename=" & FilePath)
                Response.TransmitFile(FilePath)
                Response.End()
            End If

            If UCase(Extension) = ".JPEG" Then
                Response.ContentType = "image/jpeg"
                Response.AppendHeader("Content-Disposition", "attachment;filename=" & FilePath)
                Response.TransmitFile(FilePath)
                Response.End()
            End If

        End If
    End Sub

    Protected Sub BtnPlanoEscena4_Click(sender As Object, e As EventArgs) Handles BtnPlanoEscena4.Click
        LblErrorEspacio4.Text = ""
        If FileUploadPlanoEscena4.HasFile Or Session("FileUploadPlanoEscena41") IsNot Nothing Then
            Dim woperador As String = Session("CUIT")
            Dim FileName As String = Path.GetFileName(FileUploadPlanoEscena4.PostedFile.FileName)
            Dim Extension As String = Path.GetExtension(FileUploadPlanoEscena4.PostedFile.FileName)
            Dim FolderPath As String = ConfigurationManager.AppSettings("FolderPath")
            Dim FilePath As String = ""
            If FileUploadPlanoEscena4.HasFile Then
                Dim wfecha As Date = DateTime.Now.ToString
                Dim wdia As Integer = wfecha.Day
                Dim wmes As Integer = wfecha.Month
                Dim wano As Integer = wfecha.Year
                Dim whora As Integer = wfecha.Hour
                Dim wminu As Integer = wfecha.Minute
                Dim wsegu As Integer = wfecha.Second
                Dim wdir As String = wano.ToString + wmes.ToString + wdia.ToString + whora.ToString + wminu.ToString + wsegu.ToString

                If UCase(Extension) <> ".PDF" And UCase(Extension) <> ".JPG" And UCase(Extension) <> ".JPEG" Then
                    LblErrorEspacio4.Text = "No es un documento Adobe .PDF o imagen .JPG .JPEG"
                    Return
                End If
                Dim sizeInBytes As Long = FileUploadPlanoEscena4.PostedFile.ContentLength
                If sizeInBytes / 1000000 > 10 Then
                    LblErrorEspacio4.Text = "El documento tiene un tamaño mayor a 10 Mb"
                    Return
                End If
                Dim randomName As String = RTrim(woperador) + wdir
                If Not Directory.Exists(Server.MapPath("~/Temp/" & randomName & "/")) Then
                    Directory.CreateDirectory(Server.MapPath("~/Temp/" & randomName & "/"))
                End If
                FilePath = (Server.MapPath("~/Temp/" & randomName & "/")) + FileName
                Try
                    FileUploadPlanoEscena4.SaveAs(FilePath)
                Catch ex As Exception
                End Try
                Session("FileUploadPlanoEscena41") = FileUploadPlanoEscena4
                Session("FileUploadPlanoEscena4FileName") = FilePath
            Else
                FilePath = Session("FileUploadPlanoEscena4FileName")
                Dim letra As String = Right(FilePath.Trim, 5)
                If Left(letra, 1) = "." Then
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

            If UCase(Extension) = ".JPG" Then
                Response.ContentType = "image/jpg"
                Response.AppendHeader("Content-Disposition", "attachment;filename=" & FilePath)
                Response.TransmitFile(FilePath)
                Response.End()
            End If

            If UCase(Extension) = ".JPEG" Then
                Response.ContentType = "image/jpeg"
                Response.AppendHeader("Content-Disposition", "attachment;filename=" & FilePath)
                Response.TransmitFile(FilePath)
                Response.End()
            End If

        End If
    End Sub

    Protected Sub BtnPlantaLuz4_Click(sender As Object, e As EventArgs) Handles BtnPlantaLuz4.Click
        LblErrorEspacio4.Text = ""
        If FileUploadPlantaLuz4.HasFile Or Session("FileUploadPlantaLuz41") IsNot Nothing Then
            Dim woperador As String = Session("CUIT")
            Dim FileName As String = Path.GetFileName(FileUploadPlantaLuz4.PostedFile.FileName)
            Dim Extension As String = Path.GetExtension(FileUploadPlantaLuz4.PostedFile.FileName)
            Dim FolderPath As String = ConfigurationManager.AppSettings("FolderPath")
            Dim FilePath As String = ""
            If FileUploadPlantaLuz4.HasFile Then
                Dim wfecha As Date = DateTime.Now.ToString
                Dim wdia As Integer = wfecha.Day
                Dim wmes As Integer = wfecha.Month
                Dim wano As Integer = wfecha.Year
                Dim whora As Integer = wfecha.Hour
                Dim wminu As Integer = wfecha.Minute
                Dim wsegu As Integer = wfecha.Second
                Dim wdir As String = wano.ToString + wmes.ToString + wdia.ToString + whora.ToString + wminu.ToString + wsegu.ToString

                If UCase(Extension) <> ".PDF" And UCase(Extension) <> ".JPG" And UCase(Extension) <> ".JPEG" Then
                    LblErrorEspacio4.Text = "No es un documento Adobe .PDF o imagen .JPG .JPEG"
                    Return
                End If
                Dim sizeInBytes As Long = FileUploadPlantaLuz4.PostedFile.ContentLength
                If sizeInBytes / 1000000 > 10 Then
                    LblErrorEspacio4.Text = "El documento tiene un tamaño mayor a 10 Mb"
                    Return
                End If

                Dim randomName As String = RTrim(woperador) + wdir
                If Not Directory.Exists(Server.MapPath("~/Temp/" & randomName & "/")) Then
                    Directory.CreateDirectory(Server.MapPath("~/Temp/" & randomName & "/"))
                End If
                FilePath = (Server.MapPath("~/Temp/" & randomName & "/")) + FileName
                Try
                    FileUploadPlantaLuz4.SaveAs(FilePath)
                Catch ex As Exception
                End Try
                Session("FileUploadPlantaLuz14") = FileUploadPlantaLuz4
                Session("FileUploadPlantaLuz4FileName") = FilePath
            Else
                FilePath = Session("FileUploadPlantaLuz4FileName")
                Dim letra As String = Right(FilePath.Trim, 5)
                If Left(letra, 1) = "." Then
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

            If UCase(Extension) = ".JPG" Then
                Response.ContentType = "image/jpg"
                Response.AppendHeader("Content-Disposition", "attachment;filename=" & FilePath)
                Response.TransmitFile(FilePath)
                Response.End()
            End If

            If UCase(Extension) = ".JPEG" Then
                Response.ContentType = "image/jpeg"
                Response.AppendHeader("Content-Disposition", "attachment;filename=" & FilePath)
                Response.TransmitFile(FilePath)
                Response.End()
            End If

        End If
    End Sub

    Protected Sub BtnFotoPlanoEscena4_Click(sender As Object, e As EventArgs) Handles BtnFotoPlanoEscena4.Click
        LblErrorEspacio4.Text = ""
        If FileUploadFotoEscena4.HasFile Or Session("FileUploadFotoEscena41") IsNot Nothing Then
            Dim woperador As String = Session("CUIT")
            Dim FileName As String = Path.GetFileName(FileUploadFotoEscena4.PostedFile.FileName)
            Dim Extension As String = Path.GetExtension(FileUploadFotoEscena4.PostedFile.FileName)
            Dim FolderPath As String = ConfigurationManager.AppSettings("FolderPath")
            Dim FilePath As String = ""
            If FileUploadFotoEscena4.HasFile Then
                Dim wfecha As Date = DateTime.Now.ToString
                Dim wdia As Integer = wfecha.Day
                Dim wmes As Integer = wfecha.Month
                Dim wano As Integer = wfecha.Year
                Dim whora As Integer = wfecha.Hour
                Dim wminu As Integer = wfecha.Minute
                Dim wsegu As Integer = wfecha.Second
                Dim wdir As String = wano.ToString + wmes.ToString + wdia.ToString + whora.ToString + wminu.ToString + wsegu.ToString

                If UCase(Extension) <> ".JPG" And UCase(Extension) <> ".JPEG" Then
                    LblErrorEspacio4.Text = "No es una imagen .JPG .JPEG"
                    Return
                End If
                Dim sizeInBytes As Long = FileUploadFotoEscena4.PostedFile.ContentLength
                If sizeInBytes / 1000000 > 10 Then
                    LblErrorEspacio4.Text = "El documento tiene un tamaño mayor a 10 Mb"
                    Return
                End If
                Dim randomName As String = RTrim(woperador) + wdir
                If Not Directory.Exists(Server.MapPath("~/Temp/" & randomName & "/")) Then
                    Directory.CreateDirectory(Server.MapPath("~/Temp/" & randomName & "/"))
                End If
                FilePath = (Server.MapPath("~/Temp/" & randomName & "/")) + FileName
                Try
                    FileUploadFotoEscena4.SaveAs(FilePath)
                Catch ex As Exception
                End Try
                Session("FileUploadFotoEscena41") = FileUploadFotoEscena4
                Session("FileUploadFotoEscena4FileName") = FilePath
            Else
                FilePath = Session("FileUploadFotoEscena4FileName")
                Dim letra As String = Right(FilePath.Trim, 5)
                If Left(letra, 1) = "." Then
                    Extension = Right(FilePath.Trim, 5)
                Else
                    Extension = Right(FilePath.Trim, 4)
                End If
            End If

            If UCase(Extension) = ".JPG" Then
                Response.ContentType = "image/jpg"
                Response.AppendHeader("Content-Disposition", "attachment;filename=" & FilePath)
                Response.TransmitFile(FilePath)
                Response.End()
            End If

            If UCase(Extension) = ".JPEG" Then
                Response.ContentType = "image/jpeg"
                Response.AppendHeader("Content-Disposition", "attachment;filename=" & FilePath)
                Response.TransmitFile(FilePath)
                Response.End()
            End If

        End If
    End Sub

    Protected Function ValidarEspacios()
        'If BtnGuardar.Text = "Confirmar Registro" Then
        If DdlCantidadEspacios.SelectedValue >= 1 Then
            If FileUploadPlanoEscena1.HasFile Then
                Dim Extension As String = Path.GetExtension(FileUploadPlanoEscena1.PostedFile.FileName)
                If UCase(Extension) <> ".PDF" And UCase(Extension) <> ".JPG" And UCase(Extension) <> ".JPEG" Then
                    LblErrorEspacio1.Text = "El documento del Plano Escenico no es un documento Adobe .PDF o Imagen .JPG .JPEG"
                    LblErrorEspacio1.Focus()
                    Return False
                End If
                Dim sizeInBytes As Long = FileUploadPlanoEscena1.PostedFile.ContentLength
                If sizeInBytes / 1000000 > 10 Then
                    LblErrorEspacio1.Text = "El documento del Plano Escenico tiene un tamaño mayor a 10 Mb"
                    LblErrorEspacio1.Focus()
                    Return False
                End If
            Else
                If Session("FileUploadPlanoEscena11") IsNot Nothing Then
                    Try
                        Dim UploadImporta1 As FileUpload = CType(Session("FileUploadPlanoEscena11"), FileUpload)
                        Dim Extension As String = Path.GetExtension(UploadImporta1.PostedFile.FileName)
                        If UCase(Extension) <> ".PDF" And UCase(Extension) <> ".JPG" And UCase(Extension) <> ".JEPG" Then
                            LblErrorEspacio1.Text = "El documento del Plano Escenico 1 no es un documento Adobe PDF o Imagen JPG JPEG"
                            LblErrorEspacio1.Focus()
                            Return False
                        End If
                        Dim sizeInBytes As Long = UploadImporta1.PostedFile.ContentLength
                        If sizeInBytes / 1000000 > 10 Then
                            LblErrorEspacio1.Text = "El documento del Plano Escenico tiene un tamaño mayor a 10 Mb"
                            LblErrorEspacio1.Focus()
                            Return False
                        End If
                    Catch ex As Exception
                    End Try
                End If
            End If
            If FileUploadPlantaLuz1.HasFile Then
                Dim Extension As String = Path.GetExtension(FileUploadPlantaLuz1.PostedFile.FileName)
                If UCase(Extension) <> ".PDF" And UCase(Extension) <> ".JPG" And UCase(Extension) <> ".JPEG" Then
                    LblErrorEspacio1.Text = "El documento de Planta de Luz 1 no es un documento Adobe .PDF o Imagen .JPG .JPEG"
                    LblErrorEspacio1.Focus()
                    Return False
                End If
                Dim sizeInBytes As Long = FileUploadPlantaLuz1.PostedFile.ContentLength
                If sizeInBytes / 1000000 > 10 Then
                    LblErrorEspacio1.Text = "El documento de Planta de Luz 1 tiene un tamaño mayor a 10 Mb"
                    LblErrorEspacio1.Focus()
                    Return False
                End If
            Else
                If Session("FileUploadPlantaLuz11") IsNot Nothing Then
                    Try
                        Dim UploadImporta1 As FileUpload = CType(Session("FileUploadPlantaLuz11"), FileUpload)
                        Dim Extension As String = Path.GetExtension(UploadImporta1.PostedFile.FileName)
                        If UCase(Extension) <> ".PDF" And UCase(Extension) <> ".JPG" And UCase(Extension) <> ".JPEG" Then
                            LblErrorEspacio1.Text = "El documento de Planta de Luz no es un documento Adobe PDF o Imagen JPG JPEG"
                            LblErrorEspacio1.Focus()
                            Return False
                        End If
                        Dim sizeInBytes As Long = UploadImporta1.PostedFile.ContentLength
                        If sizeInBytes / 1000000 > 10 Then
                            LblErrorEspacio1.Text = "El documento de Planta de Luz 1 tiene un tamaño mayor a 10 Mb"
                            LblErrorEspacio1.Focus()
                            Return False
                        End If
                    Catch ex As Exception
                    End Try
                End If
            End If
            If FileUploadFotoEscena1.HasFile Then
                Dim Extension As String = Path.GetExtension(FileUploadFotoEscena1.PostedFile.FileName)
                If UCase(Extension) <> ".JPG" And UCase(Extension) <> ".JPEG" Then
                    LblErrorEspacio1.Text = "La foto del Espacio escenico 1 no es una Imagen .JPG o .JPEG"
                    LblErrorEspacio1.Focus()
                    Return False
                End If
                Dim sizeInBytes As Long = FileUploadFotoEscena1.PostedFile.ContentLength
                If sizeInBytes / 1000000 > 10 Then
                    LblErrorEspacio1.Text = "La foto del Espacio escenico 1 tiene un tamaño mayor a 10 Mb"
                    LblErrorEspacio1.Focus()
                    Return False
                End If
            Else
                If Session("FileUploadFotoEscena11") IsNot Nothing Then
                    Try
                        Dim UploadImporta1 As FileUpload = CType(Session("FileUploadFotoEscena11"), FileUpload)
                        Dim Extension As String = Path.GetExtension(UploadImporta1.PostedFile.FileName)
                        If UCase(Extension) <> ".PDF" And UCase(Extension) <> ".JPG" And UCase(Extension) <> ".JPEG" Then
                            LblErrorEspacio1.Text = "La foto del Espacio escenico no es una Imagen .JPG o .JPEG"
                            LblErrorEspacio1.Focus()
                            Return False
                        End If
                        Dim sizeInBytes As Long = UploadImporta1.PostedFile.ContentLength
                        If sizeInBytes / 1000000 > 10 Then
                            LblErrorEspacio1.Text = "La foto del Espacio escenico 1 tiene un tamaño mayor a 10 Mb"
                            LblErrorEspacio1.Focus()
                            Return False
                        End If
                    Catch ex As Exception
                    End Try
                End If
            End If
        End If

        If DdlCantidadEspacios.SelectedValue >= 2 Then
                If FileUploadPlanoEscena2.HasFile Then
                    Dim Extension As String = Path.GetExtension(FileUploadPlanoEscena2.PostedFile.FileName)
                    If UCase(Extension) <> ".PDF" And UCase(Extension) <> ".JPG" And UCase(Extension) <> ".JPEG" Then
                        LblErrorEspacio2.Text = "El documento del Plano Escenico 2 no es un documento Adobe .PDF o Imagen .JPG .JPEG"
                        LblErrorEspacio2.Focus()
                        Return False
                    End If
                    Dim sizeInBytes As Long = FileUploadPlanoEscena2.PostedFile.ContentLength
                    If sizeInBytes / 1000000 > 10 Then
                        LblErrorEspacio2.Text = "El documento del Plano Escenico 2 tiene un tamaño mayor a 10 Mb"
                        LblErrorEspacio2.Focus()
                        Return False
                    End If
                Else
                If Session("FileUploadPlanoEscena21") IsNot Nothing Then
                    Try
                        Dim UploadImporta1 As FileUpload = CType(Session("FileUploadPlanoEscena21"), FileUpload)
                        Dim Extension As String = Path.GetExtension(UploadImporta1.PostedFile.FileName)
                        If UCase(Extension) <> ".PDF" And UCase(Extension) <> ".JPG" And UCase(Extension) <> ".JEPG" Then
                            LblErrorEspacio2.Text = "El documento del Plano Escenico no es un documento Adobe PDF o Imagen JPG JPEG"
                            LblErrorEspacio2.Focus()
                            Return False
                        End If
                        Dim sizeInBytes As Long = UploadImporta1.PostedFile.ContentLength
                        If sizeInBytes / 1000000 > 10 Then
                            LblErrorEspacio2.Text = "El documento del Plano Escenico 2 tiene un tamaño mayor a 10 Mb"
                            LblErrorEspacio2.Focus()
                            Return False
                        End If
                    Catch ex As Exception
                    End Try
                End If
            End If
                If FileUploadPlantaLuz2.HasFile Then
                    Dim Extension As String = Path.GetExtension(FileUploadPlantaLuz2.PostedFile.FileName)
                    If UCase(Extension) <> ".PDF" And UCase(Extension) <> ".JPG" And UCase(Extension) <> ".JPEG" Then
                        LblErrorEspacio2.Text = "El documento de Planta de Luz 2 no es un documento Adobe .PDF o Imagen .JPG .JPEG"
                        LblErrorEspacio2.Focus()
                        Return False
                    End If
                    Dim sizeInBytes As Long = FileUploadPlantaLuz2.PostedFile.ContentLength
                    If sizeInBytes / 1000000 > 10 Then
                        LblErrorEspacio2.Text = "El documento de Planta de Luz 2 tiene un tamaño mayor a 10 Mb"
                        LblErrorEspacio2.Focus()
                        Return False
                    End If
                Else
                If Session("FileUploadPlantaLuz21") IsNot Nothing Then
                    Try
                        Dim UploadImporta1 As FileUpload = CType(Session("FileUploadPlantaLuz21"), FileUpload)
                        Dim Extension As String = Path.GetExtension(UploadImporta1.PostedFile.FileName)
                        If UCase(Extension) <> ".PDF" And UCase(Extension) <> ".JPG" And UCase(Extension) <> ".JPEG" Then
                            LblErrorEspacio2.Text = "El documento de Planta de Luz no es un documento Adobe PDF o Imagen JPG JPEG"
                            LblErrorEspacio2.Focus()
                            Return False
                        End If
                        Dim sizeInBytes As Long = UploadImporta1.PostedFile.ContentLength
                        If sizeInBytes / 1000000 > 10 Then
                            LblErrorEspacio2.Text = "El documento de Planta de Luz 2 tiene un tamaño mayor a 10 Mb"
                            LblErrorEspacio2.Focus()
                            Return False
                        End If
                    Catch ex As Exception
                    End Try
                End If
            End If
                If FileUploadFotoEscena2.HasFile Then
                    Dim Extension As String = Path.GetExtension(FileUploadFotoEscena2.PostedFile.FileName)
                    If UCase(Extension) <> ".JPG" And UCase(Extension) <> ".JPEG" Then
                        LblErrorEspacio2.Text = "La foto del Espacio escenico 2 no es una Imagen .JPG o .JPEG"
                        LblErrorEspacio2.Focus()
                        Return False
                    End If
                    Dim sizeInBytes As Long = FileUploadFotoEscena2.PostedFile.ContentLength
                    If sizeInBytes / 1000000 > 10 Then
                        LblErrorEspacio2.Text = "La foto del Espacio escenico 2 tiene un tamaño mayor a 10 Mb"
                        LblErrorEspacio2.Focus()
                        Return False
                    End If
                Else
                If Session("FileUploadFotoEscena21") IsNot Nothing Then
                    Try
                        Dim UploadImporta1 As FileUpload = CType(Session("FileUploadFotoEscena21"), FileUpload)
                        Dim Extension As String = Path.GetExtension(UploadImporta1.PostedFile.FileName)
                        If UCase(Extension) <> ".PDF" And UCase(Extension) <> ".JPG" And UCase(Extension) <> ".JPEG" Then
                            LblErrorEspacio2.Text = "La foto del Espacio escenico 2 no es una Imagen .JPG o .JPEG"
                            LblErrorEspacio2.Focus()
                            Return False
                        End If
                        Dim sizeInBytes As Long = FileUploadFotoEscena2.PostedFile.ContentLength
                        If sizeInBytes / 1000000 > 10 Then
                            LblErrorEspacio2.Text = "La foto del Espacio escenico 2 tiene un tamaño mayor a 10 Mb"
                            LblErrorEspacio2.Focus()
                            Return False
                        End If
                    Catch ex As Exception
                    End Try
                End If
            End If
            End If

        If DdlCantidadEspacios.SelectedValue >= 3 Then
            If FileUploadPlanoEscena3.HasFile Then
                Dim Extension As String = Path.GetExtension(FileUploadPlanoEscena3.PostedFile.FileName)
                If UCase(Extension) <> ".PDF" And UCase(Extension) <> ".JPG" And UCase(Extension) <> ".JPEG" Then
                    LblErrorEspacio3.Text = "El documento del Plano Escenico 3 no es un documento Adobe .PDF o Imagen .JPG .JPEG"
                    LblErrorEspacio3.Focus()
                    Return False
                End If
                Dim sizeInBytes As Long = FileUploadPlanoEscena3.PostedFile.ContentLength
                If sizeInBytes / 1000000 > 10 Then
                    LblErrorEspacio3.Text = "El documento del Plano Escenico 3 tiene un tamaño mayor a 10 Mb"
                    LblErrorEspacio3.Focus()
                    Return False
                End If
            Else
                If Session("FileUploadPlanoEscena31") IsNot Nothing Then
                    Try
                        Dim UploadImporta1 As FileUpload = CType(Session("FileUploadPlanoEscena31"), FileUpload)
                        Dim Extension As String = Path.GetExtension(UploadImporta1.PostedFile.FileName)
                        If UCase(Extension) <> ".PDF" And UCase(Extension) <> ".JPG" And UCase(Extension) <> ".JEPG" Then
                            LblErrorEspacio3.Text = "El documento del Plano Escenico 3 no es un documento Adobe PDF o Imagen JPG JPEG"
                            LblErrorEspacio3.Focus()
                            Return False
                        End If
                        Dim sizeInBytes As Long = UploadImporta1.PostedFile.ContentLength
                        If sizeInBytes / 1000000 > 10 Then
                            LblErrorEspacio3.Text = "El documento del Plano Escenico 3 tiene un tamaño mayor a 10 Mb"
                            LblErrorEspacio3.Focus()
                            Return False
                        End If
                    Catch ex As Exception
                    End Try
                End If
            End If
            If FileUploadPlantaLuz3.HasFile Then
                Dim Extension As String = Path.GetExtension(FileUploadPlantaLuz3.PostedFile.FileName)
                If UCase(Extension) <> ".PDF" And UCase(Extension) <> ".JPG" And UCase(Extension) <> ".JPEG" Then
                    LblErrorEspacio3.Text = "El documento de Planta de Luz 3 no es un documento Adobe .PDF o Imagen .JPG .JPEG"
                    LblErrorEspacio3.Focus()
                    Return False
                End If
                Dim sizeInBytes As Long = FileUploadPlanoEscena3.PostedFile.ContentLength
                If sizeInBytes / 1000000 > 10 Then
                    LblErrorEspacio3.Text = "El documento de Planta de Luz 3 tiene un tamaño mayor a 10 Mb"
                    LblErrorEspacio3.Focus()
                    Return False
                End If
            Else
                If Session("FileUploadPlantaLuz31") IsNot Nothing Then
                    Try
                        Dim UploadImporta1 As FileUpload = CType(Session("FileUploadPlantaLuz31"), FileUpload)
                        Dim Extension As String = Path.GetExtension(UploadImporta1.PostedFile.FileName)
                        If UCase(Extension) <> ".PDF" And UCase(Extension) <> ".JPG" And UCase(Extension) <> ".JPEG" Then
                            LblErrorEspacio3.Text = "El documento de Planta de Luz no es un documento Adobe PDF o Imagen JPG JPEG"
                            LblErrorEspacio3.Focus()
                            Return False
                        End If
                        Dim sizeInBytes As Long = UploadImporta1.PostedFile.ContentLength
                        If sizeInBytes / 1000000 > 10 Then
                            LblErrorEspacio3.Text = "El documento de Planta de Luz 3 tiene un tamaño mayor a 10 Mb"
                            LblErrorEspacio3.Focus()
                            Return False
                        End If
                    Catch ex As Exception
                    End Try
                End If
            End If
            If FileUploadFotoEscena3.HasFile Then
                Dim Extension As String = Path.GetExtension(FileUploadFotoEscena3.PostedFile.FileName)
                If UCase(Extension) <> ".JPG" And UCase(Extension) <> ".JPEG" Then
                    LblErrorEspacio3.Text = "La foto del Espacio escenico 3 no es una Imagen .JPG o .JPEG"
                    LblErrorEspacio3.Focus()
                    Return False
                End If
                Dim sizeInBytes As Long = FileUploadFotoEscena3.PostedFile.ContentLength
                If sizeInBytes / 1000000 > 10 Then
                    LblErrorEspacio3.Text = "La foto del Espacio escenico 3 tiene un tamaño mayor a 10 Mb"
                    LblErrorEspacio3.Focus()
                    Return False
                End If
            Else
                If Session("FileUploadFotoEscena31") IsNot Nothing Then
                    Try
                        Dim UploadImporta1 As FileUpload = CType(Session("FileUploadFotoEscena31"), FileUpload)
                        Dim Extension As String = Path.GetExtension(UploadImporta1.PostedFile.FileName)
                        If UCase(Extension) <> ".PDF" And UCase(Extension) <> ".JPG" And UCase(Extension) <> ".JPEG" Then
                            LblErrorEspacio3.Text = "La foto del Espacio escenico 3 no es una Imagen .JPG o .JPEG"
                            LblErrorEspacio3.Focus()
                            Return False
                        End If
                        Dim sizeInBytes As Long = UploadImporta1.PostedFile.ContentLength
                        If sizeInBytes / 1000000 > 10 Then
                            LblErrorEspacio3.Text = "La foto del Espacio escenico 3 tiene un tamaño mayor a 10 Mb"
                            LblErrorEspacio3.Focus()
                            Return False
                        End If
                    Catch ex As Exception
                    End Try
                End If
            End If
        End If

        If DdlCantidadEspacios.SelectedValue >= 4 Then
            If FileUploadPlanoEscena4.HasFile Then
                Dim Extension As String = Path.GetExtension(FileUploadPlanoEscena4.PostedFile.FileName)
                If UCase(Extension) <> ".PDF" And UCase(Extension) <> ".JPG" And UCase(Extension) <> ".JPEG" Then
                    LblErrorEspacio4.Text = "El documento del Plano Escenico 4 no es un documento Adobe .PDF o Imagen .JPG .JPEG"
                    LblErrorEspacio4.Focus()
                    Return False
                End If
                Dim sizeInBytes As Long = FileUploadPlanoEscena4.PostedFile.ContentLength
                If sizeInBytes / 1000000 > 10 Then
                    LblErrorEspacio4.Text = "El documento del Plano Escenico 4 tiene un tamaño mayor a 10 Mb"
                    LblErrorEspacio4.Focus()
                    Return False
                End If
            Else
                If Session("FileUploadPlanoEscena41") IsNot Nothing Then
                    Try
                        Dim UploadImporta1 As FileUpload = CType(Session("FileUploadPlanoEscena41"), FileUpload)
                        Dim Extension As String = Path.GetExtension(UploadImporta1.PostedFile.FileName)
                        If UCase(Extension) <> ".PDF" And UCase(Extension) <> ".JPG" And UCase(Extension) <> ".JEPG" Then
                            LblErrorEspacio4.Text = "El documento del Plano Escenico 4 no es un documento Adobe PDF o Imagen JPG JPEG"
                            LblErrorEspacio4.Focus()
                            Return False
                        End If
                        Dim sizeInBytes As Long = UploadImporta1.PostedFile.ContentLength
                        If sizeInBytes / 1000000 > 10 Then
                            LblErrorEspacio4.Text = "El documento del Plano Escenico 4 tiene un tamaño mayor a 10 Mb"
                            LblErrorEspacio4.Focus()
                            Return False
                        End If
                    Catch ex As Exception
                    End Try
                End If
            End If
            If FileUploadPlantaLuz4.HasFile Then
                Dim Extension As String = Path.GetExtension(FileUploadPlantaLuz4.PostedFile.FileName)
                If UCase(Extension) <> ".PDF" And UCase(Extension) <> ".JPG" And UCase(Extension) <> ".JPEG" Then
                    LblErrorEspacio4.Text = "El documento de Planta de Luz no 4 es un documento Adobe .PDF o Imagen .JPG .JPEG"
                    LblErrorEspacio4.Focus()
                    Return False
                End If
                Dim sizeInBytes As Long = FileUploadPlantaLuz4.PostedFile.ContentLength
                If sizeInBytes / 1000000 > 10 Then
                    LblErrorEspacio4.Text = "El documento de Planta de Luz no 4 tiene un tamaño mayor a 10 Mb"
                    LblErrorEspacio4.Focus()
                    Return False
                End If
            Else
                If Session("FileUploadPlantaLuz41") IsNot Nothing Then
                    Try
                        Dim UploadImporta1 As FileUpload = CType(Session("FileUploadPlantaLuz41"), FileUpload)
                        Dim Extension As String = Path.GetExtension(UploadImporta1.PostedFile.FileName)
                        If UCase(Extension) <> ".PDF" And UCase(Extension) <> ".JPG" And UCase(Extension) <> ".JPEG" Then
                            LblErrorEspacio4.Text = "El documento de Planta de Luz no es un documento Adobe PDF o Imagen JPG JPEG"
                            LblErrorEspacio4.Focus()
                            Return False
                        End If
                        Dim sizeInBytes As Long = UploadImporta1.PostedFile.ContentLength
                        If sizeInBytes / 1000000 > 10 Then
                            LblErrorEspacio4.Text = "El documento de Planta de Luz no 4 tiene un tamaño mayor a 10 Mb"
                            LblErrorEspacio4.Focus()
                            Return False
                        End If
                    Catch ex As Exception
                    End Try
                End If
            End If
            If FileUploadFotoEscena4.HasFile Then
                Dim Extension As String = Path.GetExtension(FileUploadFotoEscena4.PostedFile.FileName)
                If UCase(Extension) <> ".JPG" And UCase(Extension) <> ".JPEG" Then
                    LblErrorEspacio4.Text = "La foto del Espacio escenico 4 no es una Imagen .JPG o .JPEG"
                    LblErrorEspacio4.Focus()
                    Return False
                End If
                Dim sizeInBytes As Long = FileUploadFotoEscena4.PostedFile.ContentLength
                If sizeInBytes / 1000000 > 10 Then
                    LblErrorEspacio4.Text = "La foto del Espacio escenico 4 tiene un tamaño mayor a 10 Mb"
                    LblErrorEspacio4.Focus()
                    Return False
                End If
            Else
                If Session("FileUploadFotoEscena41") IsNot Nothing Then
                    Try
                        Dim UploadImporta1 As FileUpload = CType(Session("FileUploadFotoEscena41"), FileUpload)
                        Dim Extension As String = Path.GetExtension(UploadImporta1.PostedFile.FileName)
                        If UCase(Extension) <> ".PDF" And UCase(Extension) <> ".JPG" And UCase(Extension) <> ".JPEG" Then
                            LblErrorEspacio4.Text = "La foto del Espacio escenico 4 no es una Imagen .JPG o .JPEG"
                            LblErrorEspacio4.Focus()
                            Return False
                        End If
                        Dim sizeInBytes As Long = UploadImporta1.PostedFile.ContentLength
                        If sizeInBytes / 1000000 > 10 Then
                            LblErrorEspacio4.Text = "La foto del Espacio escenico 4 tiene un tamaño mayor a 10 Mb"
                            LblErrorEspacio4.Focus()
                            Return False
                        End If
                    Catch ex As Exception
                    End Try
                End If
            End If
        End If
        'End If

        Return True
    End Function

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
        fileSavePath = Server.MapPath("~/Documentos/REGISTRO/" & nCodigo & "/FOTOSPERITAJE/")
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
        fileSavePath = Server.MapPath("~/Documentos/REGISTRO/" & nCodigo & "/PLANOSPERITAJE/")
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
        fileSavePath = Server.MapPath("~/Documentos/REGISTRO/" & nCodigo & "/ESPACIO/1/PLANO/")
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
        fileSavePath = Server.MapPath("~/Documentos/REGISTRO/" & nCodigo & "/ESPACIO/1/LUZ/")
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
        fileSavePath = Server.MapPath("~/Documentos/REGISTRO/" & nCodigo & "/ESPACIO/1/ESCENA/")
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
        fileSavePath = Server.MapPath("~/Documentos/REGISTRO/" & nCodigo & "/ESPACIO/2/PLANO/")
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
        fileSavePath = Server.MapPath("~/Documentos/REGISTRO/" & nCodigo & "/ESPACIO/2/LUZ/")
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
        fileSavePath = Server.MapPath("~/Documentos/REGISTRO/" & nCodigo & "/ESPACIO/2/ESCENA/")
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
        fileSavePath = Server.MapPath("~/Documentos/REGISTRO/" & nCodigo & "/ESPACIO/3/PLANO/")
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
        fileSavePath = Server.MapPath("~/Documentos/REGISTRO/" & nCodigo & "/ESPACIO/3/LUZ/")
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
        fileSavePath = Server.MapPath("~/Documentos/REGISTRO/" & nCodigo & "/ESPACIO/3/ESCENA/")
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
        fileSavePath = Server.MapPath("~/Documentos/REGISTRO/" & nCodigo & "/ESPACIO/4/PLANO/")
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
        fileSavePath = Server.MapPath("~/Documentos/REGISTRO/" & nCodigo & "/ESPACIO/4/LUZ/")
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
        fileSavePath = Server.MapPath("~/Documentos/REGISTRO/" & nCodigo & "/ESPACIO/4/ESCENA/")
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

    Private Sub CargAdjunto()
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
        If wcantidad < 1 Then
            TablaEspacio1.Style.Item("display") = "none"
        End If
        If wcantidad < 2 Then
            TablaEspacio2.Style.Item("display") = "none"
        End If
        If wcantidad < 3 Then
            TablaEspacio3.Style.Item("display") = "none"
        End If
        If wcantidad < 4 Then
            TablaEspacio4.Style.Item("display") = "none"
        End If
        If wequipo = 0 Then
            DivTablaEquipamiento.Style.Item("display") = "none"
            RadioButtonEquipa1.Checked = False
            RadioButtonEquipa2.Checked = True
        Else
            RadioButtonEquipa1.Checked = True
            RadioButtonEquipa2.Checked = False
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

        wsegu = DateAndTime.Now.Second
        wdir = wano.ToString + wmes.ToString + wdia.ToString + whora.ToString + wminu.ToString + wsegu.ToString
        randomName = RTrim(woperador) + wdir
        sDocumento = ""
        sPath = ""
        fileSavePath = Server.MapPath("~/Documentos/REGISTRO/" & nCodigo & "/ESPACIO/1/ESCENA/")
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
            Session("FileUploadPlanoEscena11") = FileUploadPlanoEscena1
            Session("FileUploadPlanoEscena1FileName") = sPath
            Session("sDocumentoEscena1") = sDocumento
            LabelPlanoEscena1.Text = sDocumento
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
            Session("FileUploadPlanoEscena1FileName") = FilePathDest
        End If

        sDocumento = ""
        sPath = ""
        fileSavePath = Server.MapPath("~/Documentos/REGISTRO/" & nCodigo & "/ESPACIO/1/LUZ/")
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
            Session("FileUploadPlantaLuz11") = FileUploadPlantaLuz1
            Session("FileUploadPlantaLuz1FileName") = sPath
            Session("sDocumentoLuz1") = sDocumento
            LabelPlantaLuz1.Text = sDocumento
            randomName = randomName + "5"
            If Not Directory.Exists(Server.MapPath("~/Temp/" & randomName & "/")) Then
                Directory.CreateDirectory(Server.MapPath("~/Temp/" & randomName & "/"))
            End If
            Dim FilePathDest As String = (Server.MapPath("~/Temp/" & randomName & "/")) + sDocumento
            Try
                File.Copy(sPath, FilePathDest)
            Catch ex As Exception
                Return
            End Try
            Session("FileUploadPlantaLuz1FileName") = FilePathDest
        End If

        sDocumento = ""
        sPath = ""
        fileSavePath = Server.MapPath("~/Documentos/REGISTRO/" & nCodigo & "/ESPACIO/1/PLANO/")
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
            Session("FileUploadFotoEscena11") = FileUploadFotoEscena1
            Session("FileUploadFotoEscena1FileName") = sPath
            Session("sDocumentoFoto1") = sDocumento
            LabelFotoEscena1.Text = sDocumento
            randomName = randomName + "6"
            If Not Directory.Exists(Server.MapPath("~/Temp/" & randomName & "/")) Then
                Directory.CreateDirectory(Server.MapPath("~/Temp/" & randomName & "/"))
            End If
            Dim FilePathDest As String = (Server.MapPath("~/Temp/" & randomName & "/")) + sDocumento
            Try
                File.Copy(sPath, FilePathDest)
            Catch ex As Exception
                Return
            End Try
            Session("FileUploadFotoEscena1FileName") = FilePathDest
        End If

        wsegu = DateAndTime.Now.Second
        wdir = wano.ToString + wmes.ToString + wdia.ToString + whora.ToString + wminu.ToString + wsegu.ToString
        randomName = RTrim(woperador) + wdir
        sDocumento = ""
        sPath = ""
        fileSavePath = Server.MapPath("~/Documentos/REGISTRO/" & nCodigo & "/ESPACIO/2/ESCENA/")
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
            Session("FileUploadPlanoEscena21") = FileUploadPlanoEscena2
            Session("FileUploadPlanoEscena2FileName") = sPath
            Session("sDocumentoEscena2") = sDocumento
            LabelPlanoEscena2.Text = sDocumento
            randomName = randomName + "7"
            If Not Directory.Exists(Server.MapPath("~/Temp/" & randomName & "/")) Then
                Directory.CreateDirectory(Server.MapPath("~/Temp/" & randomName & "/"))
            End If
            Dim FilePathDest As String = (Server.MapPath("~/Temp/" & randomName & "/")) + sDocumento
            Try
                File.Copy(sPath, FilePathDest)
            Catch ex As Exception
                Return
            End Try
            Session("FileUploadPlanoEscena2FileName") = FilePathDest
        End If

        sDocumento = ""
        sPath = ""
        fileSavePath = Server.MapPath("~/Documentos/REGISTRO/" & nCodigo & "/ESPACIO/2/LUZ/")
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
            Session("FileUploadPlantaLuz21") = FileUploadPlantaLuz2
            Session("FileUploadPlantaLuz2FileName") = sPath
            Session("sDocumentoLuz2") = sDocumento
            LabelPlantaLuz2.Text = sDocumento
            randomName = randomName + "8"
            If Not Directory.Exists(Server.MapPath("~/Temp/" & randomName & "/")) Then
                Directory.CreateDirectory(Server.MapPath("~/Temp/" & randomName & "/"))
            End If
            Dim FilePathDest As String = (Server.MapPath("~/Temp/" & randomName & "/")) + sDocumento
            Try
                File.Copy(sPath, FilePathDest)
            Catch ex As Exception
                Return
            End Try
            Session("FileUploadPlantaLuz2FileName") = FilePathDest
        End If

        sDocumento = ""
        sPath = ""
        fileSavePath = Server.MapPath("~/Documentos/REGISTRO/" & nCodigo & "/ESPACIO/2/PLANO/")
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
            Session("FileUploadFotoEscena21") = FileUploadFotoEscena2
            Session("FileUploadFotoEscena2FileName") = sPath
            Session("sDocumentoFoto2") = sDocumento
            LabelFotoEscena2.Text = sDocumento
            randomName = randomName + "9"
            If Not Directory.Exists(Server.MapPath("~/Temp/" & randomName & "/")) Then
                Directory.CreateDirectory(Server.MapPath("~/Temp/" & randomName & "/"))
            End If
            Dim FilePathDest As String = (Server.MapPath("~/Temp/" & randomName & "/")) + sDocumento
            Try
                File.Copy(sPath, FilePathDest)
            Catch ex As Exception
                Return
            End Try
            Session("FileUploadFotoEscena2FileName") = FilePathDest
        End If

        wsegu = DateAndTime.Now.Second
        wdir = wano.ToString + wmes.ToString + wdia.ToString + whora.ToString + wminu.ToString + wsegu.ToString
        randomName = RTrim(woperador) + wdir
        sDocumento = ""
        sPath = ""
        fileSavePath = Server.MapPath("~/Documentos/REGISTRO/" & nCodigo & "/ESPACIO/3/ESCENA/")
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
            Session("FileUploadPlanoEscena31") = FileUploadPlanoEscena3
            Session("FileUploadPlanoEscena3FileName") = sPath
            Session("sDocumentoEscena3") = sDocumento
            LabelPlanoEscena3.Text = sDocumento
            randomName = randomName + "10"
            If Not Directory.Exists(Server.MapPath("~/Temp/" & randomName & "/")) Then
                Directory.CreateDirectory(Server.MapPath("~/Temp/" & randomName & "/"))
            End If
            Dim FilePathDest As String = (Server.MapPath("~/Temp/" & randomName & "/")) + sDocumento
            Try
                File.Copy(sPath, FilePathDest)
            Catch ex As Exception
                Return
            End Try
            Session("FileUploadPlanoEscena3FileName") = FilePathDest
        End If

        sDocumento = ""
        sPath = ""
        fileSavePath = Server.MapPath("~/Documentos/REGISTRO/" & nCodigo & "/ESPACIO/3/LUZ/")
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
            Session("FileUploadPlantaLuz31") = FileUploadPlantaLuz3
            Session("FileUploadPlantaLuz3FileName") = sPath
            Session("sDocumentoLuz3") = sDocumento
            LabelPlantaLuz3.Text = sDocumento
            randomName = randomName + "11"
            If Not Directory.Exists(Server.MapPath("~/Temp/" & randomName & "/")) Then
                Directory.CreateDirectory(Server.MapPath("~/Temp/" & randomName & "/"))
            End If
            Dim FilePathDest As String = (Server.MapPath("~/Temp/" & randomName & "/")) + sDocumento
            Try
                File.Copy(sPath, FilePathDest)
            Catch ex As Exception
                Return
            End Try
            Session("FileUploadPlantaLuz3FileName") = FilePathDest
        End If

        sDocumento = ""
        sPath = ""
        fileSavePath = Server.MapPath("~/Documentos/REGISTRO/" & nCodigo & "/ESPACIO/3/PLANO/")
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
            Session("FileUploadFotoEscena31") = FileUploadFotoEscena3
            Session("FileUploadFotoEscena3FileName") = sPath
            Session("sDocumentoFoto3") = sDocumento
            LabelFotoEscena3.Text = sDocumento
            randomName = randomName + "12"
            If Not Directory.Exists(Server.MapPath("~/Temp/" & randomName & "/")) Then
                Directory.CreateDirectory(Server.MapPath("~/Temp/" & randomName & "/"))
            End If
            Dim FilePathDest As String = (Server.MapPath("~/Temp/" & randomName & "/")) + sDocumento
            Try
                File.Copy(sPath, FilePathDest)
            Catch ex As Exception
                Return
            End Try
            Session("FileUploadFotoEscena3FileName") = FilePathDest
        End If

        wsegu = DateAndTime.Now.Second
        wdir = wano.ToString + wmes.ToString + wdia.ToString + whora.ToString + wminu.ToString + wsegu.ToString
        randomName = RTrim(woperador) + wdir
        sDocumento = ""
        sPath = ""
        fileSavePath = Server.MapPath("~/Documentos/REGISTRO/" & nCodigo & "/ESPACIO/4/ESCENA/")
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
            Session("FileUploadPlanoEscena41") = FileUploadPlanoEscena4
            Session("FileUploadPlanoEscena4FileName") = sPath
            Session("sDocumentoEscena4") = sDocumento
            LabelPlanoEscena4.Text = sDocumento
            randomName = randomName + "13"
            If Not Directory.Exists(Server.MapPath("~/Temp/" & randomName & "/")) Then
                Directory.CreateDirectory(Server.MapPath("~/Temp/" & randomName & "/"))
            End If
            Dim FilePathDest As String = (Server.MapPath("~/Temp/" & randomName & "/")) + sDocumento
            Try
                File.Copy(sPath, FilePathDest)
            Catch ex As Exception
                Return
            End Try
            Session("FileUploadPlanoEscena4FileName") = FilePathDest
        End If

        sDocumento = ""
        sPath = ""
        fileSavePath = Server.MapPath("~/Documentos/REGISTRO/" & nCodigo & "/ESPACIO/4/LUZ/")
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
            Session("FileUploadPlantaLuz41") = FileUploadPlantaLuz4
            Session("FileUploadPlantaLuz4FileName") = sPath
            Session("sDocumentoLuz4") = sDocumento
            LabelPlantaLuz4.Text = sDocumento
            randomName = randomName + "14"
            If Not Directory.Exists(Server.MapPath("~/Temp/" & randomName & "/")) Then
                Directory.CreateDirectory(Server.MapPath("~/Temp/" & randomName & "/"))
            End If
            Dim FilePathDest As String = (Server.MapPath("~/Temp/" & randomName & "/")) + sDocumento
            Try
                File.Copy(sPath, FilePathDest)
            Catch ex As Exception
                Return
            End Try
            Session("FileUploadPlantaLuz4FileName") = FilePathDest
        End If

        sDocumento = ""
        sPath = ""
        fileSavePath = Server.MapPath("~/Documentos/REGISTRO/" & nCodigo & "/ESPACIO/4/PLANO/")
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
            Session("FileUploadFotoEscena41") = FileUploadFotoEscena4
            Session("FileUploadFotoEscena4FileName") = sPath
            Session("sDocumentoFoto4") = sDocumento
            LabelFotoEscena4.Text = sDocumento
            randomName = randomName + "15"
            If Not Directory.Exists(Server.MapPath("~/Temp/" & randomName & "/")) Then
                Directory.CreateDirectory(Server.MapPath("~/Temp/" & randomName & "/"))
            End If
            Dim FilePathDest As String = (Server.MapPath("~/Temp/" & randomName & "/")) + sDocumento
            Try
                File.Copy(sPath, FilePathDest)
            Catch ex As Exception
                Return
            End Try
            Session("FileUploadFotoEscena4FileName") = FilePathDest
        End If

        CreateDatatables()
        sDocumento = ""
        sPath = ""
        fileSavePath = Server.MapPath("~/Documentos/REGISTRO/" & nCodigo & "/FOTOSPERITAJE/")
        wlong = Len(fileSavePath)
        Dim i As Integer = 20
        Try
            Dim dt = CType(Session("mFotos"), DataTable)
            Dim files() As String = IO.Directory.GetFiles(fileSavePath)

            For Each sfile As String In files
                sPath = sfile.ToString
                Dim wlong2 As Integer = Len(sPath)
                sDocumento = Mid(sPath, wlong + 1, wlong2 - wlong)
                randomName = randomName + i.ToString
                If Not Directory.Exists(Server.MapPath("~/Temp/" & randomName & "/")) Then
                    Directory.CreateDirectory(Server.MapPath("~/Temp/" & randomName & "/"))
                End If
                Dim FilePathDest As String = (Server.MapPath("~/Temp/" & randomName & "/")) + sDocumento
                Try
                    File.Copy(sPath, FilePathDest)
                Catch ex As Exception
                    Return
                End Try
                Dim R As DataRow = dt.NewRow
                R("FILEPATH") = FilePathDest
                R("DOCUMENTO") = sDocumento
                dt.Rows.Add(R)
                i = i + 1
            Next
            GridView2.DataSource = dt
            GridView2.DataBind()
        Catch ex As Exception
            sDocumento = ""
        End Try

        sDocumento = ""
        sPath = ""
        fileSavePath = Server.MapPath("~/Documentos/REGISTRO/" & nCodigo & "/PLANOSPERITAJE/")
        wlong = Len(fileSavePath)
        Try
            Dim dt = CType(Session("mPlanos"), DataTable)
            Dim files() As String = IO.Directory.GetFiles(fileSavePath)
            For Each sfile As String In files
                sPath = sfile.ToString
                Dim wlong2 As Integer = Len(sPath)
                sDocumento = Mid(sPath, wlong + 1, wlong2 - wlong)
                randomName = randomName + i.ToString
                If Not Directory.Exists(Server.MapPath("~/Temp/" & randomName & "/")) Then
                    Directory.CreateDirectory(Server.MapPath("~/Temp/" & randomName & "/"))
                End If
                Dim FilePathDest As String = (Server.MapPath("~/Temp/" & randomName & "/")) + sDocumento
                Try
                    File.Copy(sPath, FilePathDest)
                Catch ex As Exception
                    Return
                End Try
                Dim R As DataRow = dt.NewRow
                R("FILEPATH") = FilePathDest
                R("DOCUMENTO") = sDocumento
                dt.Rows.Add(R)
                i = i + 1
            Next
            GridView3.DataSource = dt
            GridView3.DataBind()
        Catch ex As Exception
            sDocumento = ""
        End Try

    End Sub


End Class