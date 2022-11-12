Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration

Public Module Validaciones

    Public Function ValidarFecha(ByVal sFecha As String) As Boolean
        Dim nD As Integer
        Dim nM As Integer
        Dim nY As Integer
        Dim wfecha As Date = DateAndTime.Now
        Try
            wfecha = CDate(sFecha)
        Catch ex As Exception
            Return False
        End Try
        Try
            nD = Day(wfecha)
            nM = Month(wfecha)
            nY = Year(wfecha)
            If ValidarFecha(nD, nM, nY) Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try

    End Function

    Public Function ValidarFecha(ByVal nDD As Integer, ByVal nMM As Integer, ByVal nYYYY As Integer) As Boolean

        If nDD + nMM + nYYYY = 0 Then Return False

        Dim wdia As Integer = CInt(nDD)
        Dim wmes As Integer = CInt(nMM)
        Dim wano As Integer = CInt(nYYYY)

        If wano < 1900 Then
            Return False
        End If

        If wdia = 31 And (wmes = 2 Or wmes = 4 Or wmes = 6 Or wmes = 9 Or wmes = 11) Then
            Return False
        End If

        If nMM = 2 Then
            Dim wrema = wano Mod 4
            If wdia = 29 And wrema > 0 Then
                Return False
            End If
        End If

        Return True

    End Function

    Public Function GetFecha(ByVal sFecha As String, ByRef dFecha As DateTime) As Boolean

        dFecha = DateTime.Now
        Try
            dFecha = DateTime.Parse(sFecha)
            Return True
        Catch ex As Exception
            Return False
        End Try

    End Function

    Function Calcular_Edad(ByVal Fecha_Nacimiento As Object) As Integer
        Dim nEdad As Object
        Dim dFecha As DateTime = DateTime.Now
        nEdad = DateDiff("yyyy", Fecha_Nacimiento, Now)

        If dFecha < DateSerial(Year(dFecha), Month(Fecha_Nacimiento), Day(Fecha_Nacimiento)) Then
            nEdad = nEdad - 1
        End If

        Return CInt(nEdad)
    End Function


    Public Function EsMayorDeEdad(ByVal sFecha As String) As Boolean

        Dim wfechnac As DateTime = DateTime.Now

    End Function

    Public Function ValidarCUIT(ByVal sCUIT As String) As Boolean
        Dim lnSuma As Integer
        Dim wcontrol As Integer
        Dim wrem As Integer
        Dim wdigito As Integer
        Dim i As Integer
        Dim nTotal As Integer

        For i = 0 To Len(sCUIT) - 1
            nTotal += Mid(sCUIT, i + 1, 1)
        Next

        If nTotal = 0 Then
            Return False
        End If

        lnSuma = 0

        If Len(sCUIT) = 11 Then
            Dim ln1 As Integer = CInt(Mid(sCUIT, 10, 1)) * 2
            Dim ln2 As Integer = CInt(Mid(sCUIT, 9, 1)) * 3
            Dim ln3 As Integer = CInt(Mid(sCUIT, 8, 1)) * 4
            Dim ln4 As Integer = CInt(Mid(sCUIT, 7, 1)) * 5
            Dim ln5 As Integer = CInt(Mid(sCUIT, 6, 1)) * 6
            Dim ln6 As Integer = CInt(Mid(sCUIT, 5, 1)) * 7
            Dim ln7 As Integer = CInt(Mid(sCUIT, 4, 1)) * 2
            Dim ln8 As Integer = CInt(Mid(sCUIT, 3, 1)) * 3
            Dim ln9 As Integer = CInt(Mid(sCUIT, 2, 1)) * 4
            Dim ln10 As Integer = CInt(Mid(sCUIT, 1, 1)) * 5

            lnSuma = ln1 + ln2 + ln3 + ln4 + ln5 + ln6 + ln7 + ln8 + ln9 + ln10
            wrem = lnSuma Mod 11

            If wrem = 0 Then
                wdigito = 0
            Else
                wdigito = 11 - wrem
            End If

            wcontrol = CInt(Mid(sCUIT, 11, 1))
            If wcontrol = wdigito Then
                Return True
            Else
                Return False
            End If
        Else
            Return False
        End If

    End Function

    Public Function EstaInhibido(ByVal sCUIL As String) As Boolean

        Dim MyConnection As SqlConnection
        Dim MyCommand As SqlCommand
        Dim sSQLCmd As String
        Dim nCantidad As Integer
        Dim bExiste As Boolean
        Dim sDNI As String


        Try

            bExiste = False
            sDNI = Mid(sCUIL, 3, 8)

            MyConnection = New SqlConnection()
            MyConnection.ConnectionString = ConfigurationManager.ConnectionStrings("INTeatroDig").ConnectionString
            MyConnection.Open()

            'Chequeo que exista el CUIL
            sSQLCmd = "SELECT count(*) AS cantidad " & _
                                "FROM Inhibi " & _
                                "WHERE cuil = '" & sCUIL & "' AND enabled = 1"

            MyCommand = New SqlCommand()
            MyCommand.CommandText = sSQLCmd
            MyCommand.CommandType = CommandType.Text
            MyCommand.Connection = MyConnection
            nCantidad = Convert.ToInt32(MyCommand.ExecuteScalar())

            If nCantidad > 0 Then
                bExiste = True
            Else
                bExiste = False
            End If

            MyCommand.Dispose()

            If Not bExiste Then

                'Chequeo que exista el DNI
                sSQLCmd = "SELECT count(*) AS cantidad " & _
                                "FROM Inhibi " & _
                                "WHERE dni = '" & sDNI & "' AND enabled = 1"

                MyCommand = New SqlCommand()
                MyCommand.CommandText = sSQLCmd
                MyCommand.CommandType = CommandType.Text
                MyCommand.Connection = MyConnection
                nCantidad = Convert.ToInt32(MyCommand.ExecuteScalar())

                If nCantidad > 0 Then
                    bExiste = True
                Else
                    bExiste = False
                End If

                MyCommand.Dispose()
            End If

            MyConnection.Dispose()

            Return bExiste

        Catch ex As Exception
            Return bExiste
        End Try

    End Function

    Public Function LimpiarCaracteres(ByVal s As String) As String

        Try
            s = Replace(s, "á", "a")
            s = Replace(s, "é", "e")
            s = Replace(s, "í", "i")
            s = Replace(s, "ó", "o")
            s = Replace(s, "ú", "u")
            s = Replace(s, "Á", "A")
            s = Replace(s, "É", "E")
            s = Replace(s, "Í", "I")
            s = Replace(s, "Ó", "O")
            s = Replace(s, "Ú", "U")

            's = Replace(s, ".", "")
            's = Replace(s, "-", "")
            's = Replace(s, "_", "")

            s = Replace(s, "'", " ")
            s = Replace(s, """", " ")

            Return s

        Catch ex As Exception
            Return ""
        End Try

    End Function

    'Public Function ValidarCUIT(ByVal wcuil As String) As Boolean

    '    Dim cCuit As String
    '    Dim lnSuma As Integer = 0
    '    Dim wrem As Integer
    '    Dim wdigito As Integer
    '    Dim wcontrol As Integer

    '    cCuit = RTrim(LTrim(wcuil.ToString))
    '    cCuit = Left(cCuit, 2) + "-" + Mid(cCuit, 3, 8) + "-" + Right(cCuit, 1)

    '    If Left(cCuit, 2) = "00" Or Right(cCuit, 1) = "0" Then
    '        Return False
    '    End If

    '    lnSuma = 0
    '    If Len(cCuit) = 13 Then
    '        Dim ln1 As Integer = CInt(Mid(cCuit, 11, 1)) * 2
    '        Dim ln2 As Integer = CInt(Mid(cCuit, 10, 1)) * 3
    '        Dim ln3 As Integer = CInt(Mid(cCuit, 9, 1)) * 4
    '        Dim ln4 As Integer = CInt(Mid(cCuit, 8, 1)) * 5
    '        Dim ln5 As Integer = CInt(Mid(cCuit, 7, 1)) * 6
    '        Dim ln6 As Integer = CInt(Mid(cCuit, 6, 1)) * 7
    '        Dim ln7 As Integer = CInt(Mid(cCuit, 5, 1)) * 2
    '        Dim ln8 As Integer = CInt(Mid(cCuit, 4, 1)) * 3
    '        Dim ln9 As Integer = CInt(Mid(cCuit, 2, 1)) * 4
    '        Dim ln10 As Integer = CInt(Mid(cCuit, 1, 1)) * 5
    '        lnSuma = ln1 + ln2 + ln3 + ln4 + ln5 + ln6 + ln7 + ln8 + ln9 + ln10

    '        wrem = lnSuma Mod 11
    '        If wrem = 0 Then
    '            wdigito = 0
    '        Else
    '            wdigito = 11 - wrem
    '        End If
    '        wcontrol = CInt(Mid(cCuit, 13, 1))

    '        If wcontrol = wdigito Then
    '            Return True
    '        Else
    '            Return False
    '        End If
    '    Else
    '        Return False
    '    End If

    'End Function

End Module
