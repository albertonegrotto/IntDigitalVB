Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.IO
Imports System.IO.Packaging
Public Class ActuDocSala
    Inherits System.Web.UI.Page
    Dim cn As New SqlClient.SqlConnection(SqlConex)
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Dim quien As usuario = CType(Session("usuario"), usuario)
            Session("USER_ID") = quien.Codigo
            Session("CUIL") = quien.Usuario
            Dim nCodigo As Integer = Request.QueryString("codigo")
            Session.Add("CodRegistro", nCodigo)
            Inicializa()
        Else
            GuardarAdjunto()
            MaintainScrollPositionOnPostBack = True
        End If
    End Sub

    Private Sub Inicializa()
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
        LblDenominacion.InnerText = wdenominacion
        If wcantidad < 1 Then
            TablaEspacio1.Visible = False
        End If
        If wcantidad < 2 Then
            TablaEspacio2.Visible = False
        End If
        If wcantidad < 3 Then
            TablaEspacio3.Visible = False
        End If
        If wcantidad < 4 Then
            TablaEspacio4.Visible = False
        End If
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

        FileUploadEquipa.Visible = False
        BtnVisualizaEquipa.Visible = False
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
                FileUploadEquipa.Visible = True
                BtnVisualizaEquipa.Visible = True
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
                FailureText.Text = "No se pudo guardar documento de equipamiento"
                Return
            End Try
            Session("FileUploadEquipaFileName") = FilePathDest
        End If

        FileUploadEquipaSub.Visible = False
        BtnVisualizaEquipaSub.Visible = False
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
                FileUploadEquipaSub.Visible = True
                BtnVisualizaEquipaSub.Visible = True
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
                FailureText.Text = "No se pudo guardar documento de equipamiento adquirido"
                Return
            End Try
            Session("FileUploadEquipaSubFileName") = FilePathDest
        End If

        FileUploadPlanoEscena1.Visible = False
        BtnPlanoEscena1.Visible = False
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
                FileUploadPlanoEscena1.Visible = True
                BtnPlanoEscena1.Visible = True
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
                FailureText.Text = "No se pudo guardar documento de Escena 1"
                Return
            End Try
            Session("FileUploadPlanoEscena1FileName") = FilePathDest
        End If

        FileUploadPlantaLuz1.Visible = False
        BtnPlantaLuz1.Visible = False
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
                FileUploadPlantaLuz1.Visible = True
                BtnPlantaLuz1.Visible = True
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
                FailureText.Text = "No se pudo guardar documento de Luz 1"
                Return
            End Try
            Session("FileUploadPlantaLuz1FileName") = FilePathDest
        End If

        FileUploadFotoEscena1.Visible = False
        BtnFotoPlanoEscena1.Visible = False
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
                FileUploadFotoEscena1.Visible = True
                BtnFotoPlanoEscena1.Visible = True
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
                FailureText.Text = "No se pudo guardar documento de Foto 1"
                Return
            End Try
            Session("FileUploadFotoEscena1FileName") = FilePathDest
        End If

        FileUploadPlanoEscena2.Visible = False
        BtnPlanoEscena2.Visible = False
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
                FileUploadPlanoEscena2.Visible = True
                BtnPlanoEscena2.Visible = True
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
                FailureText.Text = "No se pudo guardar documento de Escena 2"
                Return
            End Try
            Session("FileUploadPlanoEscena2FileName") = FilePathDest
        End If

        FileUploadPlantaLuz2.Visible = False
        BtnPlantaLuz2.Visible = False
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
                FileUploadPlantaLuz2.Visible = True
                BtnPlantaLuz2.Visible = True
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
                FailureText.Text = "No se pudo guardar documento de Luz 2"
                Return
            End Try
            Session("FileUploadPlantaLuz2FileName") = FilePathDest
        End If

        FileUploadFotoEscena2.Visible = False
        BtnFotoPlanoEscena2.Visible = False
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
                FileUploadFotoEscena2.Visible = True
                BtnFotoPlanoEscena2.Visible = True
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
                FailureText.Text = "No se pudo guardar documento de Foto 2"
                Return
            End Try
            Session("FileUploadFotoEscena2FileName") = FilePathDest
        End If

        FileUploadPlanoEscena3.Visible = False
        BtnPlanoEscena3.Visible = False
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
                FileUploadPlanoEscena3.Visible = True
                BtnPlanoEscena3.Visible = True
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
                FailureText.Text = "No se pudo guardar documento de Escena 3"
                Return
            End Try
            Session("FileUploadPlanoEscena3FileName") = FilePathDest
        End If

        FileUploadPlantaLuz3.Visible = False
        BtnPlantaLuz3.Visible = False
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
                FileUploadPlantaLuz3.Visible = True
                BtnPlantaLuz3.Visible = True
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
                FailureText.Text = "No se pudo guardar documento de Luz 3"
                Return
            End Try
            Session("FileUploadPlantaLuz3FileName") = FilePathDest
        End If

        FileUploadFotoEscena3.Visible = False
        BtnFotoPlanoEscena3.Visible = False
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
                FileUploadFotoEscena3.Visible = True
                BtnFotoPlanoEscena3.Visible = True
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
                FailureText.Text = "No se pudo guardar documento de Foto 3"
                Return
            End Try
            Session("FileUploadFotoEscena3FileName") = FilePathDest
        End If

        FileUploadPlanoEscena4.Visible = False
        BtnPlanoEscena4.Visible = False
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
                FileUploadPlanoEscena4.Visible = True
                BtnPlanoEscena4.Visible = True
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
                FailureText.Text = "No se pudo guardar documento de Escena 4"
                Return
            End Try
            Session("FileUploadPlanoEscena4FileName") = FilePathDest
        End If

        FileUploadPlantaLuz4.Visible = False
        BtnPlantaLuz4.Visible = False
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
                FileUploadPlantaLuz4.Visible = True
                BtnPlantaLuz4.Visible = True
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
                FailureText.Text = "No se pudo guardar documento de Luz 4"
                Return
            End Try
            Session("FileUploadPlantaLuz4FileName") = FilePathDest
        End If

        FileUploadFotoEscena4.Visible = False
        BtnFotoPlanoEscena4.Visible = False
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
                FileUploadFotoEscena4.Visible = True
                BtnFotoPlanoEscena4.Visible = True
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
                FailureText.Text = "No se pudo guardar documento de Foto 4"
                Return
            End Try
            Session("FileUploadFotoEscena4FileName") = FilePathDest
        End If

        CreateDatatables()
        BtnAgregaFotos.Visible = False
        UploadImportaFotos.Visible = False
        BtnVisualizaFotos.Visible = False
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
                    FailureText.Text = "No se pudieron guardar Fotos Peritaje"
                    Return
                End Try
                Dim R As DataRow = dt.NewRow
                R("FILEPATH") = FilePathDest
                R("DOCUMENTO") = sDocumento
                dt.Rows.Add(R)
                i = i + 1
                BtnAgregaFotos.Visible = True
                UploadImportaFotos.Visible = True
                BtnVisualizaFotos.Visible = True
            Next
            GridView2.DataSource = dt
            GridView2.DataBind()
        Catch ex As Exception
            sDocumento = ""
        End Try

        BtnAgregaPlanos.Visible = False
        UploadImportaPlanos.Visible = False
        BtnVisualizaPlanos.Visible = False
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
                    FailureText.Text = "No se pudieron guardar Planos Peritaje"
                    Return
                End Try
                Dim R As DataRow = dt.NewRow
                R("FILEPATH") = FilePathDest
                R("DOCUMENTO") = sDocumento
                dt.Rows.Add(R)
                i = i + 1
                BtnAgregaPlanos.Visible = True
                UploadImportaPlanos.Visible = True
                BtnVisualizaPlanos.Visible = True
            Next
            GridView3.DataSource = dt
            GridView3.DataBind()
        Catch ex As Exception
            sDocumento = ""
        End Try

    End Sub

    Private Sub GuardarAdjunto()
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
                    Session("sDocumentoEquipa") = FileUploadEquipa.FileName
                End If
            End If
        Else
            If Session("FileUploadEquipa1") IsNot Nothing Then
                Dim FileUploadEquipa1 As FileUpload = CType(Session("FileUploadEquipa1"), FileUpload)
                If FileUploadEquipa1.FileName.ToString = "" Then
                    LabelUploadEquipa.Text = Session("sDocumentoEquipa")
                Else
                    LabelUploadEquipa.Text = FileUploadEquipa1.FileName
                End If
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
                    Session("sDocumentoEquipaSub") = FileUploadEquipaSub.FileName
                End If
            End If
        Else
            If Session("FileUploadEquipaSub1") IsNot Nothing Then
                Dim FileUploadEquipaSub1 As FileUpload = CType(Session("FileUploadEquipaSub1"), FileUpload)
                If FileUploadEquipaSub1.FileName.ToString = "" Then
                    LabelUploadEquipaSub.Text = Session("sDocumentoEquipaSub")
                Else
                    LabelUploadEquipaSub.Text = FileUploadEquipaSub1.FileName
                End If
            End If
        End If
        GuardarEspacio1()
        GuardarEspacio2()
        GuardarEspacio3()
        GuardarEspacio4()
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
                    Session("sDocumentoEscena1") = FileUploadPlanoEscena1.FileName
                End If
            End If
        Else
            If Session("FileUploadPlanoEscena11") IsNot Nothing Then
                Dim FileUploadPlanoEscena11 As FileUpload = CType(Session("FileUploadPlanoEscena11"), FileUpload)
                If FileUploadPlanoEscena11.FileName.ToString = "" Then
                    LabelPlanoEscena1.Text = Session("sDocumentoEscena1")
                Else
                    LabelPlanoEscena1.Text = FileUploadPlanoEscena11.FileName
                End If
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
                    Session("sDocumentoLuz1") = FileUploadPlantaLuz1.FileName
                End If
            End If
        Else
            If Session("FileUploadPlantaLuz11") IsNot Nothing Then
                Dim FileUploadPlantaLuz11 As FileUpload = CType(Session("FileUploadPlantaLuz11"), FileUpload)
                If FileUploadPlantaLuz11.FileName.ToString = "" Then
                    LabelPlantaLuz1.Text = Session("sDocumentoLuz1")
                Else
                    LabelPlantaLuz1.Text = FileUploadPlantaLuz11.FileName
                End If
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
                    Session("sDocumentoFoto1") = FileUploadFotoEscena1.FileName
                End If
            End If
        Else
            If Session("FileUploadFotoEscena11") IsNot Nothing Then
                Dim FileUploadFotoEscena11 As FileUpload = CType(Session("FileUploadFotoEscena11"), FileUpload)
                If FileUploadFotoEscena11.FileName.ToString = "" Then
                    LabelFotoEscena1.Text = Session("sDocumentoFoto1")
                Else
                    LabelFotoEscena1.Text = FileUploadFotoEscena11.FileName
                End If
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
                    Session("sDocumentoEscena2") = FileUploadPlanoEscena2.FileName
                End If
            End If
        Else
            If Session("FileUploadPlanoEscena21") IsNot Nothing Then
                Dim FileUploadPlanoEscena21 As FileUpload = CType(Session("FileUploadPlanoEscena21"), FileUpload)
                If FileUploadPlanoEscena21.FileName.ToString = "" Then
                    LabelPlanoEscena2.Text = Session("sDocumentoEscena2")
                Else
                    LabelPlanoEscena2.Text = FileUploadPlanoEscena21.FileName
                End If
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
                    Session("sDocumentoLuz2") = FileUploadPlantaLuz2.FileName
                End If
            End If
        Else
            If Session("FileUploadPlantaLuz21") IsNot Nothing Then
                Dim FileUploadPlantaLuz21 As FileUpload = CType(Session("FileUploadPlantaLuz21"), FileUpload)
                If FileUploadPlantaLuz21.FileName.ToString = "" Then
                    LabelPlantaLuz2.Text = Session("sDocumentoLuz2")
                Else
                    LabelPlantaLuz2.Text = FileUploadPlantaLuz21.FileName
                End If
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
                    Session("sDocumentoFoto2") = FileUploadFotoEscena2.FileName
                End If
            End If
        Else
            If Session("FileUploadFotoEscena21") IsNot Nothing Then
                Dim FileUploadFotoEscena21 As FileUpload = CType(Session("FileUploadFotoEscena21"), FileUpload)
                If FileUploadFotoEscena21.FileName.ToString = "" Then
                    LabelFotoEscena2.Text = Session("sDocumentoFoto2")
                Else
                    LabelFotoEscena2.Text = FileUploadFotoEscena21.FileName
                End If
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
                    Session("sDocumentoEscena3") = FileUploadPlanoEscena3.FileName
                End If
            End If
        Else
            If Session("FileUploadPlanoEscena31") IsNot Nothing Then
                Dim FileUploadPlanoEscena31 As FileUpload = CType(Session("FileUploadPlanoEscena31"), FileUpload)
                If FileUploadPlanoEscena31.FileName.ToString = "" Then
                    LabelPlanoEscena3.Text = Session("sDocumentoEscena3")
                Else
                    LabelPlanoEscena3.Text = FileUploadPlanoEscena31.FileName
                End If
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
                    Session("sDocumentoLuz3") = FileUploadPlantaLuz3.FileName
                End If
            End If
        Else
            If Session("FileUploadPlantaLuz31") IsNot Nothing Then
                Dim FileUploadPlantaLuz31 As FileUpload = CType(Session("FileUploadPlantaLuz31"), FileUpload)
                If FileUploadPlantaLuz31.FileName.ToString = "" Then
                    LabelPlantaLuz3.Text = Session("sDocumentoLuz3")
                Else
                    LabelPlantaLuz3.Text = FileUploadPlantaLuz31.FileName
                End If
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
                    Session("sDocumentoFoto3") = FileUploadFotoEscena3.FileName
                End If
            End If
        Else
            If Session("FileUploadFotoEscena31") IsNot Nothing Then
                Dim FileUploadFotoEscena31 As FileUpload = CType(Session("FileUploadFotoEscena31"), FileUpload)
                If FileUploadFotoEscena31.FileName.ToString = "" Then
                    LabelFotoEscena3.Text = Session("sDocumentoFoto3")
                Else
                    LabelFotoEscena3.Text = FileUploadFotoEscena31.FileName
                End If
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
                    Session("sDocumentoEscena4") = FileUploadPlanoEscena4.FileName
                End If
            End If
        Else
            If Session("FileUploadPlanoEscena41") IsNot Nothing Then
                Dim FileUploadPlanoEscena41 As FileUpload = CType(Session("FileUploadPlanoEscena41"), FileUpload)
                If FileUploadPlanoEscena41.FileName.ToString = "" Then
                    LabelPlanoEscena4.Text = Session("sDocumentoEscena4")
                Else
                    LabelPlanoEscena4.Text = FileUploadPlanoEscena41.FileName
                End If
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
                    Session("sDocumentoLuz4") = FileUploadPlantaLuz4.FileName
                End If
            End If
        Else
            If Session("FileUploadPlantaLuz41") IsNot Nothing Then
                Dim FileUploadPlantaLuz41 As FileUpload = CType(Session("FileUploadPlantaLuz41"), FileUpload)
                If FileUploadPlantaLuz41.FileName.ToString = "" Then
                    LabelPlantaLuz4.Text = Session("sDocumentoLuz4")
                Else
                    LabelPlantaLuz4.Text = FileUploadPlantaLuz41.FileName
                End If
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
                    Session("sDocumentoFoto4") = FileUploadFotoEscena4.FileName
                End If
            End If
        Else
            If Session("FileUploadFotoEscena41") IsNot Nothing Then
                Dim FileUploadFotoEscena41 As FileUpload = CType(Session("FileUploadFotoEscena41"), FileUpload)
                If FileUploadFotoEscena41.FileName.ToString = "" Then
                    LabelFotoEscena4.Text = Session("sDocumentoFoto4")
                Else
                    LabelFotoEscena4.Text = FileUploadFotoEscena41.FileName
                End If
            End If
        End If

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
                    FailureText.Text = "La Foto tiene un tamaño mayor a 10 Mb"
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
                    FailureText.Text = "El Plano tiene un tamaño mayor a 10 Mb"
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
        LblErrorEspacio1.Text = ""
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
                    LblErrorEspacio1.Text = "No es un documento Adobe .PDF o imagen .JPG .JPEG"
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
        LblErrorEspacio1.Text = ""
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
                    LblErrorEspacio1.Text = "No es un documento Adobe .PDF o imagen .JPG .JPEG"
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
        LblErrorEspacio1.Text = ""
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
                    LblErrorEspacio1.Text = "No es una imagen .JPG .JPEG"
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
        LblErrorEspacio1.Text = ""
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
                    LblErrorEspacio1.Text = "No es un documento Adobe .PDF o imagen .JPG .JPEG"
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
        LblErrorEspacio1.Text = ""
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
                    LblErrorEspacio1.Text = "No es un documento Adobe .PDF o imagen .JPG .JPEG"
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
        LblErrorEspacio1.Text = ""
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
                    LblErrorEspacio1.Text = "No es una imagen .JPG .JPEG"
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
        LblErrorEspacio1.Text = ""
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
                    LblErrorEspacio1.Text = "No es un documento Adobe .PDF o imagen .JPG .JPEG"
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
        LblErrorEspacio1.Text = ""
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
                    LblErrorEspacio1.Text = "No es un documento Adobe .PDF o imagen .JPG .JPEG"
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
        LblErrorEspacio1.Text = ""
        If FileUploadFotoEscena1.HasFile Or Session("FileUploadFotoEscena41") IsNot Nothing Then
            Dim woperador As String = Session("CUIT")
            Dim FileName As String = Path.GetFileName(FileUploadFotoEscena4.PostedFile.FileName)
            Dim Extension As String = Path.GetExtension(FileUploadFotoEscena4.PostedFile.FileName)
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

    Protected Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        Response.Redirect("AdjuntosLista.aspx")
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

    Protected Sub BtnEnviar_Click(sender As Object, e As EventArgs) Handles BtnEnviar.Click
        'Guardar Lista de Equipamiento
        Dim nIdRegistro As Integer = Session("CodRegistro")
        Dim woperador As String = Session("CUIT")
        Dim Extension As String = ""
        Dim letra As String = ""
        Dim DocuControl As String = Session("sDocumentoEquipa")

        Try
            letra = Right(DocuControl.Trim, 5)
            If Left(letra, 1) = "." Then
                Extension = Right(DocuControl.Trim, 5)
            Else
                Extension = Right(DocuControl.Trim, 4)
            End If
            If UCase(Extension) <> ".PDF" And UCase(Extension) <> ".DOC" And UCase(Extension) <> ".DOCX" Then
                txtErrorEquipamiento.Text = "Equipamiento No es un documento Adobe .PDF o Word .DOC .DOCX"
                Return
            End If
        Catch ex As Exception
        End Try
        If FileUploadEquipa.HasFile Then
            Dim sizeInBytes As Long = FileUploadEquipa.PostedFile.ContentLength
            If sizeInBytes / 1000000 > 10 Then
                FailureText.Text = "Equipamiento tiene un tamaño mayor a 10 Mb"
                Return
            End If
        Else
            If Session("FileUploadEquipaFileName") IsNot Nothing Then
                Try
                    Dim UploadImporta1 As FileUpload = CType(Session("FileUploadEquipa1"), FileUpload)
                    Dim sizeInBytes As Long = UploadImporta1.PostedFile.ContentLength
                    If sizeInBytes / 1000000 > 10 Then
                        FailureText.Text = "Equipamiento tiene un tamaño mayor a 10 Mb"
                        Return
                    End If
                Catch ex As Exception
                End Try
            End If
        End If

        DocuControl = Session("sDocumentoEquipaSub")
        Try
            letra = Right(DocuControl.Trim, 5)
            If Left(letra, 1) = "." Then
                Extension = Right(DocuControl.Trim, 5)
            Else
                Extension = Right(DocuControl.Trim, 4)
            End If
            If UCase(Extension) <> ".PDF" And UCase(Extension) <> ".DOC" And UCase(Extension) <> ".DOCX" Then
                txtErrorEquipamiento.Text = "Equipamiento Adquirido No es un documento Adobe .PDF o Word .DOC .DOCX"
                Return
            End If
        Catch ex As Exception
        End Try
        If FileUploadEquipaSub.HasFile Then
            Dim sizeInBytes As Long = FileUploadEquipaSub.PostedFile.ContentLength
            If sizeInBytes / 1000000 > 10 Then
                FailureText.Text = "Equipamiento Adquirido tiene un tamaño mayor a 10 Mb"
                Return
            End If
        Else
            If Session("FileUploadEquipaSubFileName") IsNot Nothing Then
                Try
                    Dim UploadImporta1 As FileUpload = CType(Session("FileUploadEquipaSub1"), FileUpload)
                    Dim sizeInBytes As Long = UploadImporta1.PostedFile.ContentLength
                    If sizeInBytes / 1000000 > 10 Then
                        FailureText.Text = "Equipamiento Adquirido tiene un tamaño mayor a 10 Mb"
                        Return
                    End If
                Catch ex As Exception
                End Try
            End If
        End If

        DocuControl = Session("sDocumentoEscena1")
        Try
            letra = Right(DocuControl.Trim, 5)
            If Left(letra, 1) = "." Then
                Extension = Right(DocuControl.Trim, 5)
            Else
                Extension = Right(DocuControl.Trim, 4)
            End If
            If UCase(Extension) <> ".PDF" And UCase(Extension) <> ".JPG" And UCase(Extension) <> ".JPEG" Then
                LblErrorEspacio1.Text = "Plano Escenico No es un documento Adobe .PDF o imagen .JPG .JPEG"
                Return
            End If
        Catch ex As Exception
        End Try
        If FileUploadPlanoEscena1.HasFile Then
            Dim sizeInBytes As Long = FileUploadPlanoEscena1.PostedFile.ContentLength
            If sizeInBytes / 1000000 > 10 Then
                FailureText.Text = "Plano de Escena 1 tiene un tamaño mayor a 10 Mb"
                Return
            End If
        Else
            If Session("FileUploadPlanoEscena1FileName") IsNot Nothing Then
                Try
                    Dim UploadImporta1 As FileUpload = CType(Session("FileUploadPlanoEscena11"), FileUpload)
                    Dim sizeInBytes As Long = UploadImporta1.PostedFile.ContentLength
                    If sizeInBytes / 1000000 > 10 Then
                        FailureText.Text = "Plano de Escena 1 tiene un tamaño mayor a 10 Mb"
                        Return
                    End If
                Catch ex As Exception
                End Try
            End If
        End If

        DocuControl = Session("sDocumentoLuz1")
        Try
            letra = Right(DocuControl.Trim, 5)
            If Left(letra, 1) = "." Then
                Extension = Right(DocuControl.Trim, 5)
            Else
                Extension = Right(DocuControl.Trim, 4)
            End If
            If UCase(Extension) <> ".PDF" And UCase(Extension) <> ".JPG" And UCase(Extension) <> ".JPEG" Then
                LblErrorEspacio1.Text = "Planta de Luz No es un documento Adobe .PDF o imagen .JPG .JPEG"
                Return
            End If
        Catch ex As Exception
        End Try
        If FileUploadPlantaLuz1.HasFile Then
            Dim sizeInBytes As Long = FileUploadPlantaLuz1.PostedFile.ContentLength
            If sizeInBytes / 1000000 > 10 Then
                FailureText.Text = "Planta de Luz 1 tiene un tamaño mayor a 10 Mb"
                Return
            End If
        Else
            If Session("FileUploadPlantaLuz1FileName") IsNot Nothing Then
                Try
                    Dim UploadImporta1 As FileUpload = CType(Session("FileUploadPlantaLuz11"), FileUpload)
                    Dim sizeInBytes As Long = UploadImporta1.PostedFile.ContentLength
                    If sizeInBytes / 1000000 > 10 Then
                        FailureText.Text = "Planta de Luz 1 tiene un tamaño mayor a 10 Mb"
                        Return
                    End If
                Catch ex As Exception
                End Try
            End If
        End If

        DocuControl = Session("sDocumentoFoto1")
        Try
            letra = Right(DocuControl.Trim, 5)
            If Left(letra, 1) = "." Then
                Extension = Right(DocuControl.Trim, 5)
            Else
                Extension = Right(DocuControl.Trim, 4)
            End If
            If UCase(Extension) <> ".JPG" And UCase(Extension) <> ".JPEG" Then
                LblErrorEspacio1.Text = "Foto del Plano Escenico 1 No es una imagen .JPG .JPEG"
                Return
            End If
        Catch ex As Exception
        End Try
        If FileUploadFotoEscena1.HasFile Then
            Dim sizeInBytes As Long = FileUploadFotoEscena1.PostedFile.ContentLength
            If sizeInBytes / 1000000 > 10 Then
                FailureText.Text = "Foto del Plano Escenico 1 tiene un tamaño mayor a 10 Mb"
                Return
            End If
        Else
            If Session("FileUploadFotoEscena1FileName") IsNot Nothing Then
                Try
                    Dim UploadImporta1 As FileUpload = CType(Session("FileUploadFotoEscena11"), FileUpload)
                    Dim sizeInBytes As Long = FileUploadFotoEscena1.PostedFile.ContentLength
                    If sizeInBytes / 1000000 > 10 Then
                        FailureText.Text = "Foto del Plano Escenico 1 tiene un tamaño mayor a 10 Mb"
                        Return
                    End If
                Catch ex As Exception
                End Try
            End If
        End If

        DocuControl = Session("sDocumentoEscena2")
        Try
            letra = Right(DocuControl.Trim, 5)
            If Left(letra, 1) = "." Then
                Extension = Right(DocuControl.Trim, 5)
            Else
                Extension = Right(DocuControl.Trim, 4)
            End If
            If UCase(Extension) <> ".PDF" And UCase(Extension) <> ".JPG" And UCase(Extension) <> ".JPEG" Then
                LblErrorEspacio2.Text = "Plano Escenico 2 No es un documento Adobe .PDF o imagen .JPG .JPEG"
                Return
            End If
        Catch ex As Exception
        End Try
        If FileUploadPlanoEscena2.HasFile Then
            Dim sizeInBytes As Long = FileUploadPlanoEscena2.PostedFile.ContentLength
            If sizeInBytes / 1000000 > 10 Then
                FailureText.Text = "Plano de Escena 2 tiene un tamaño mayor a 10 Mb"
                Return
            End If
        Else
            If Session("FileUploadPlanoEscena2FileName") IsNot Nothing Then
                Try
                    Dim UploadImporta1 As FileUpload = CType(Session("FileUploadPlanoEscena21"), FileUpload)
                    Dim sizeInBytes As Long = UploadImporta1.PostedFile.ContentLength
                    If sizeInBytes / 1000000 > 10 Then
                        FailureText.Text = "Plano de Escena 2 tiene un tamaño mayor a 10 Mb"
                        Return
                    End If
                Catch ex As Exception
                End Try
            End If
        End If

        DocuControl = Session("sDocumentoLuz2")
        Try
            letra = Right(DocuControl.Trim, 5)
            If Left(letra, 1) = "." Then
                Extension = Right(DocuControl.Trim, 5)
            Else
                Extension = Right(DocuControl.Trim, 4)
            End If
            If UCase(Extension) <> ".PDF" And UCase(Extension) <> ".JPG" And UCase(Extension) <> ".JPEG" Then
                LblErrorEspacio2.Text = "Planta de Luz No es un documento Adobe .PDF o imagen .JPG .JPEG"
                Return
            End If
        Catch ex As Exception
        End Try
        If FileUploadPlantaLuz2.HasFile Then
            Dim sizeInBytes As Long = FileUploadPlantaLuz2.PostedFile.ContentLength
            If sizeInBytes / 1000000 > 10 Then
                FailureText.Text = "Planta de Luz 2 tiene un tamaño mayor a 10 Mb"
                Return
            End If
        Else
            If Session("FileUploadPlantaLuz2FileName") IsNot Nothing Then
                Try
                    Dim UploadImporta1 As FileUpload = CType(Session("FileUploadPlantaLuz21"), FileUpload)
                    Dim sizeInBytes As Long = UploadImporta1.PostedFile.ContentLength
                    If sizeInBytes / 1000000 > 10 Then
                        FailureText.Text = "Planta de Luz 2 tiene un tamaño mayor a 10 Mb"
                        Return
                    End If
                Catch ex As Exception
                End Try
            End If
        End If

        DocuControl = Session("sDocumentoFoto2")
        Try
            letra = Right(DocuControl.Trim, 5)
            If Left(letra, 1) = "." Then
                Extension = Right(DocuControl.Trim, 5)
            Else
                Extension = Right(DocuControl.Trim, 4)
            End If
            If UCase(Extension) <> ".JPG" And UCase(Extension) <> ".JPEG" Then
                LblErrorEspacio2.Text = "Foto del Plano Escenico No es una imagen .JPG .JPEG"
                Return
            End If
        Catch ex As Exception
        End Try
        If FileUploadFotoEscena2.HasFile Then
            Dim sizeInBytes As Long = FileUploadFotoEscena2.PostedFile.ContentLength
            If sizeInBytes / 1000000 > 10 Then
                FailureText.Text = "Foto del Plano Escenico 2 tiene un tamaño mayor a 10 Mb"
                Return
            End If
        Else
            If Session("FileUploadFotoEscena2FileName") IsNot Nothing Then
                Try
                    Dim UploadImporta1 As FileUpload = CType(Session("FileUploadFotoEscena21"), FileUpload)
                    Dim sizeInBytes As Long = FileUploadFotoEscena1.PostedFile.ContentLength
                    If sizeInBytes / 1000000 > 10 Then
                        FailureText.Text = "Foto del Plano Escenico 2 tiene un tamaño mayor a 10 Mb"
                        Return
                    End If
                Catch ex As Exception
                End Try
            End If
        End If

        DocuControl = Session("sDocumentoEscena3")
        Try
            letra = Right(DocuControl.Trim, 5)
            If Left(letra, 1) = "." Then
                Extension = Right(DocuControl.Trim, 5)
            Else
                Extension = Right(DocuControl.Trim, 4)
            End If
            If UCase(Extension) <> ".PDF" And UCase(Extension) <> ".JPG" And UCase(Extension) <> ".JPEG" Then
                LblErrorEspacio3.Text = "Plano Escenico No es un documento Adobe .PDF o imagen .JPG .JPEG"
                Return
            End If
        Catch ex As Exception
        End Try
        If FileUploadPlanoEscena3.HasFile Then
            Dim sizeInBytes As Long = FileUploadPlanoEscena3.PostedFile.ContentLength
            If sizeInBytes / 1000000 > 10 Then
                FailureText.Text = "Plano de Escena 3 tiene un tamaño mayor a 10 Mb"
                Return
            End If
        Else
            If Session("FileUploadPlanoEscena3FileName") IsNot Nothing Then
                Try
                    Dim UploadImporta1 As FileUpload = CType(Session("FileUploadPlanoEscena31"), FileUpload)
                    Dim sizeInBytes As Long = UploadImporta1.PostedFile.ContentLength
                    If sizeInBytes / 1000000 > 10 Then
                        FailureText.Text = "Plano de Escena 1 tiene un tamaño mayor a 10 Mb"
                        Return
                    End If
                Catch ex As Exception
                End Try
            End If
        End If

        DocuControl = Session("sDocumentoLuz3")
        Try
            letra = Right(DocuControl.Trim, 5)
            If Left(letra, 1) = "." Then
                Extension = Right(DocuControl.Trim, 5)
            Else
                Extension = Right(DocuControl.Trim, 4)
            End If
            If UCase(Extension) <> ".PDF" And UCase(Extension) <> ".JPG" And UCase(Extension) <> ".JPEG" Then
                LblErrorEspacio3.Text = "Planta de Luz No es un documento Adobe .PDF o imagen .JPG .JPEG"
                Return
            End If
        Catch ex As Exception
        End Try
        If FileUploadPlantaLuz3.HasFile Then
            Dim sizeInBytes As Long = FileUploadPlantaLuz3.PostedFile.ContentLength
            If sizeInBytes / 1000000 > 10 Then
                FailureText.Text = "Planta de Luz 3 tiene un tamaño mayor a 10 Mb"
                Return
            End If
        Else
            If Session("FileUploadPlantaLuz3FileName") IsNot Nothing Then
                Try
                    Dim UploadImporta1 As FileUpload = CType(Session("FileUploadPlantaLuz31"), FileUpload)
                    Dim sizeInBytes As Long = UploadImporta1.PostedFile.ContentLength
                    If sizeInBytes / 1000000 > 10 Then
                        FailureText.Text = "Planta de Luz 3 tiene un tamaño mayor a 10 Mb"
                        Return
                    End If
                Catch ex As Exception
                End Try
            End If
        End If

        DocuControl = Session("sDocumentoFoto3")
        Try
            letra = Right(DocuControl.Trim, 5)
            If Left(letra, 1) = "." Then
                Extension = Right(DocuControl.Trim, 5)
            Else
                Extension = Right(DocuControl.Trim, 4)
            End If
            If UCase(Extension) <> ".JPG" And UCase(Extension) <> ".JPEG" Then
                LblErrorEspacio3.Text = "Foto del Plano Escenico 3 No es una imagen .JPG .JPEG"
                Return
            End If
        Catch ex As Exception
        End Try
        If FileUploadFotoEscena3.HasFile Then
            Dim sizeInBytes As Long = FileUploadFotoEscena3.PostedFile.ContentLength
            If sizeInBytes / 1000000 > 10 Then
                FailureText.Text = "Foto del Plano Escenico 3 tiene un tamaño mayor a 10 Mb"
                Return
            End If
        Else
            If Session("FileUploadFotoEscena3FileName") IsNot Nothing Then
                Try
                    Dim UploadImporta1 As FileUpload = CType(Session("FileUploadFotoEscena31"), FileUpload)
                    Dim sizeInBytes As Long = FileUploadFotoEscena1.PostedFile.ContentLength
                    If sizeInBytes / 1000000 > 10 Then
                        FailureText.Text = "Foto del Plano Escenico 3 tiene un tamaño mayor a 10 Mb"
                        Return
                    End If
                Catch ex As Exception
                End Try
            End If
        End If

        DocuControl = Session("sDocumentoEscena4")
        Try
            letra = Right(DocuControl.Trim, 5)
            If Left(letra, 1) = "." Then
                Extension = Right(DocuControl.Trim, 5)
            Else
                Extension = Right(DocuControl.Trim, 4)
            End If
            If UCase(Extension) <> ".PDF" And UCase(Extension) <> ".JPG" And UCase(Extension) <> ".JPEG" Then
                LblErrorEspacio4.Text = "Plano Escenico No es un documento Adobe .PDF o imagen .JPG .JPEG"
                Return
            End If
        Catch ex As Exception
        End Try
        If FileUploadPlanoEscena4.HasFile Then
            Dim sizeInBytes As Long = FileUploadPlanoEscena4.PostedFile.ContentLength
            If sizeInBytes / 1000000 > 10 Then
                FailureText.Text = "Plano de Escena 4 tiene un tamaño mayor a 10 Mb"
                Return
            End If
        Else
            If Session("FileUploadPlanoEscena4FileName") IsNot Nothing Then
                Try
                    Dim UploadImporta1 As FileUpload = CType(Session("FileUploadPlanoEscena41"), FileUpload)
                    Dim sizeInBytes As Long = UploadImporta1.PostedFile.ContentLength
                    If sizeInBytes / 1000000 > 10 Then
                        FailureText.Text = "Plano de Escena 4 tiene un tamaño mayor a 10 Mb"
                        Return
                    End If
                Catch ex As Exception
                End Try
            End If
        End If

        DocuControl = Session("sDocumentoLuz4")
        Try
            letra = Right(DocuControl.Trim, 5)
            If Left(letra, 1) = "." Then
                Extension = Right(DocuControl.Trim, 5)
            Else
                Extension = Right(DocuControl.Trim, 4)
            End If
            If UCase(Extension) <> ".PDF" And UCase(Extension) <> ".JPG" And UCase(Extension) <> ".JPEG" Then
                LblErrorEspacio4.Text = "Planta de Luz No es un documento Adobe .PDF o imagen .JPG .JPEG"
                Return
            End If
        Catch ex As Exception
        End Try
        If FileUploadPlantaLuz4.HasFile Then
            Dim sizeInBytes As Long = FileUploadPlantaLuz4.PostedFile.ContentLength
            If sizeInBytes / 1000000 > 10 Then
                FailureText.Text = "Planta de Luz 4 tiene un tamaño mayor a 10 Mb"
                Return
            End If
        Else
            If Session("FileUploadPlantaLuz4FileName") IsNot Nothing Then
                Try
                    Dim UploadImporta1 As FileUpload = CType(Session("FileUploadPlantaLuz41"), FileUpload)
                    Dim sizeInBytes As Long = UploadImporta1.PostedFile.ContentLength
                    If sizeInBytes / 1000000 > 10 Then
                        FailureText.Text = "Planta de Luz 4 tiene un tamaño mayor a 10 Mb"
                        Return
                    End If
                Catch ex As Exception
                End Try
            End If
        End If

        DocuControl = Session("sDocumentoFoto4")
        Try
            letra = Right(DocuControl.Trim, 5)
            If Left(letra, 1) = "." Then
                Extension = Right(DocuControl.Trim, 5)
            Else
                Extension = Right(DocuControl.Trim, 4)
            End If
            If UCase(Extension) <> ".JPG" And UCase(Extension) <> ".JPEG" Then
                LblErrorEspacio4.Text = "Foto del Plano Escenico No es una imagen .JPG .JPEG"
                Return
            End If
        Catch ex As Exception
        End Try
        If FileUploadFotoEscena4.HasFile Then
            Dim sizeInBytes As Long = FileUploadFotoEscena4.PostedFile.ContentLength
            If sizeInBytes / 1000000 > 10 Then
                FailureText.Text = "Foto del Plano Escenico 4 tiene un tamaño mayor a 10 Mb"
                Return
            End If
        Else
            If Session("FileUploadFotoEscena4FileName") IsNot Nothing Then
                Try
                    Dim UploadImporta1 As FileUpload = CType(Session("FileUploadFotoEscena41"), FileUpload)
                    Dim sizeInBytes As Long = FileUploadFotoEscena1.PostedFile.ContentLength
                    If sizeInBytes / 1000000 > 10 Then
                        FailureText.Text = "Foto del Plano Escenico 4 tiene un tamaño mayor a 10 Mb"
                        Return
                    End If
                Catch ex As Exception
                End Try
            End If
        End If

        BorraAdjuntos()

        'Guardar Lista de Equipamiento
        Dim FileName As String = Path.GetFileName(FileUploadEquipa.PostedFile.FileName)
        Extension = Path.GetExtension(FileUploadEquipa.PostedFile.FileName)
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
                Return
            End Try
        Else
            If Session("FileUploadEquipaFileName") IsNot Nothing Then
                Dim UploadImporta1 As FileUpload = CType(Session("FileUploadEquipa1"), FileUpload)
                FileName = Session("sDocumentoEquipa")
                Dim Filepath As String = Session("FileUploadEquipaFileName")
                Dim FilepathDest As String = fileSavePath + "\" + FileName
                Try
                    File.Copy(Filepath, FilepathDest)
                Catch ex As Exception
                    txtErrorEquipamiento.Text = "No se pudo guardar Lista de equipamiento"
                    Return
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
                Return
            End Try
        Else
            If Session("FileUploadEquipaSubFileName") IsNot Nothing Then
                Dim UploadImporta1 As FileUpload = CType(Session("FileUploadEquipaSub1"), FileUpload)
                FileName = Session("sDocumentoEquipaSub")
                Dim Filepath As String = Session("FileUploadEquipaSubFileName")
                Dim FilepathDest As String = fileSavePath + "\" + FileName
                Try
                    File.Copy(Filepath, FilepathDest)
                Catch ex As Exception
                    txtErrorEquipamiento.Text = "No se pudo guardar Lista de equipamiento Adquirido"
                    Return
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
                LblErrorFotos.Text = "No se pudieron guardar Fotos para Peritaje"
                Return
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
                LblErrorPlanos.Text = "No se pudieron guardar Planos para Peritaje"
                Return
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
                Return
            End Try
        Else
            If Session("FileUploadPlanoEscena1FileName") IsNot Nothing Then
                Dim UploadImporta1 As FileUpload = CType(Session("FileUploadPlanoEscena11"), FileUpload)
                FileName = Session("sDocumentoEscena1")
                Dim Filepath As String = Session("FileUploadPlanoEscena1FileName")
                Dim FilepathDest As String = fileSavePath + "\" + FileName
                Try
                    File.Copy(Filepath, FilepathDest)
                Catch ex As Exception
                    LblErrorEspacio1.Text = "No se pudo guardar Plano Escena Espacio 1"
                    Return
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
                Return
            End Try
        Else
            If Session("FileUploadPlantaLuz1FileName") IsNot Nothing Then
                Dim UploadImporta1 As FileUpload = CType(Session("FileUploadPlantaLuz11"), FileUpload)
                FileName = Session("sDocumentoLuz1")
                Dim Filepath As String = Session("FileUploadPlantaLuz1FileName")
                Dim FilepathDest As String = fileSavePath + "\" + FileName
                Try
                    File.Copy(Filepath, FilepathDest)
                Catch ex As Exception
                    LblErrorEspacio1.Text = "No se pudo guardar Planta de Luz Espacio 1"
                    Return
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
                Return
            End Try
        Else
            If Session("FileUploadFotoEscena1FileName") IsNot Nothing Then
                Dim UploadImporta1 As FileUpload = CType(Session("FileUploadFotoEscena11"), FileUpload)
                FileName = Session("sDocumentoFoto1")
                Dim Filepath As String = Session("FileUploadFotoEscena1FileName")
                Dim FilepathDest As String = fileSavePath + "\" + FileName
                Try
                    File.Copy(Filepath, FilepathDest)
                Catch ex As Exception
                    LblErrorEspacio1.Text = "No se pudo guardar Foto Escenario de Luz Espacio 1"
                    Return
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
                Return
            End Try
        Else
            If Session("FileUploadPlanoEscena2FileName") IsNot Nothing Then
                Dim UploadImporta1 As FileUpload = CType(Session("FileUploadPlanoEscena21"), FileUpload)
                FileName = Session("sDocumentoEscena2")
                Dim Filepath As String = Session("FileUploadPlanoEscena2FileName")
                Dim FilepathDest As String = fileSavePath + "\" + FileName
                Try
                    File.Copy(Filepath, FilepathDest)
                Catch ex As Exception
                    LblErrorEspacio2.Text = "No se pudo guardar Plano Escena Espacio 2"
                    Return
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
                Return
            End Try
        Else
            If Session("FileUploadPlantaLuz2FileName") IsNot Nothing Then
                Dim UploadImporta1 As FileUpload = CType(Session("FileUploadPlantaLuz21"), FileUpload)
                FileName = Session("sDocumentoLuz2")
                Dim Filepath As String = Session("FileUploadPlantaLuz2FileName")
                Dim FilepathDest As String = fileSavePath + "\" + FileName
                Try
                    File.Copy(Filepath, FilepathDest)
                Catch ex As Exception
                    LblErrorEspacio2.Text = "No se pudo guardar Planta de Luz Espacio 2"
                    Return
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
                Return
            End Try
        Else
            If Session("FileUploadFotoEscena2FileName") IsNot Nothing Then
                Dim UploadImporta1 As FileUpload = CType(Session("FileUploadFotoEscena21"), FileUpload)
                FileName = Session("sDocumentoFoto2")
                Dim Filepath As String = Session("FileUploadFotoEscena2FileName")
                Dim FilepathDest As String = fileSavePath + "\" + FileName
                Try
                    File.Copy(Filepath, FilepathDest)
                Catch ex As Exception
                    LblErrorEspacio2.Text = "No se pudo guardar Foto Escenario de Luz Espacio 2"
                    Return
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
                Return
            End Try
        Else
            If Session("FileUploadPlanoEscena3FileName") IsNot Nothing Then
                Dim UploadImporta1 As FileUpload = CType(Session("FileUploadPlanoEscena31"), FileUpload)
                FileName = Session("sDocumentoEscena3")
                Dim Filepath As String = Session("FileUploadPlanoEscena3FileName")
                Dim FilepathDest As String = fileSavePath + "\" + FileName
                Try
                    File.Copy(Filepath, FilepathDest)
                Catch ex As Exception
                    LblErrorEspacio3.Text = "No se pudo guardar Plano Escena Espacio 3"
                    Return
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
                Return
            End Try
        Else
            If Session("FileUploadPlantaLuz3FileName") IsNot Nothing Then
                Dim UploadImporta1 As FileUpload = CType(Session("FileUploadPlantaLuz31"), FileUpload)
                FileName = Session("sDocumentoLuz3")
                Dim Filepath As String = Session("FileUploadPlantaLuz3FileName")
                Dim FilepathDest As String = fileSavePath + "\" + FileName
                Try
                    File.Copy(Filepath, FilepathDest)
                Catch ex As Exception
                    LblErrorEspacio3.Text = "No se pudo guardar Planta de Luz Espacio 3"
                    Return
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
                Return
            End Try
        Else
            If Session("FileUploadFotoEscena3FileName") IsNot Nothing Then
                Dim UploadImporta1 As FileUpload = CType(Session("FileUploadFotoEscena31"), FileUpload)
                FileName = Session("sDocumentoFoto3")
                Dim Filepath As String = Session("FileUploadFotoEscena3FileName")
                Dim FilepathDest As String = fileSavePath + "\" + FileName
                Try
                    File.Copy(Filepath, FilepathDest)
                Catch ex As Exception
                    LblErrorEspacio3.Text = "No se pudo guardar Foto Escenario de Luz Espacio 3"
                    Return
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
                Return
            End Try
        Else
            If Session("FileUploadPlanoEscena4FileName") IsNot Nothing Then
                Dim UploadImporta1 As FileUpload = CType(Session("FileUploadPlanoEscena41"), FileUpload)
                FileName = Session("sDocumentoEscena4")
                Dim Filepath As String = Session("FileUploadPlanoEscena4FileName")
                Dim FilepathDest As String = fileSavePath + "\" + FileName
                Try
                    File.Copy(Filepath, FilepathDest)
                Catch ex As Exception
                    LblErrorEspacio4.Text = "No se pudo guardar Plano Escena Espacio 4"
                    Return
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
                Return
            End Try
        Else
            If Session("FileUploadPlantaLuz4FileName") IsNot Nothing Then
                Dim UploadImporta1 As FileUpload = CType(Session("FileUploadPlantaLuz41"), FileUpload)
                FileName = Session("sDocumentoLuz4")
                Dim Filepath As String = Session("FileUploadPlantaLuz4FileName")
                Dim FilepathDest As String = fileSavePath + "\" + FileName
                Try
                    File.Copy(Filepath, FilepathDest)
                Catch ex As Exception
                    LblErrorEspacio4.Text = "No se pudo guardar Planta de Luz Espacio 4"
                    Return
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
                Return
            End Try
        Else
            If Session("FileUploadFotoEscena4FileName") IsNot Nothing Then
                Dim UploadImporta1 As FileUpload = CType(Session("FileUploadFotoEscena41"), FileUpload)
                FileName = Session("sDocumentoFoto4")
                Dim Filepath As String = Session("FileUploadFotoEscena4FileName")
                Dim FilepathDest As String = fileSavePath + "\" + FileName
                Try
                    File.Copy(Filepath, FilepathDest)
                Catch ex As Exception
                    LblErrorEspacio4.Text = "No se pudo guardar Foto Escenario de Luz Espacio 4"
                    Return
                End Try
            End If
        End If

        Response.Clear()
        Response.Redirect("ConfirmaDocumento.aspx", False)

    End Sub
End Class