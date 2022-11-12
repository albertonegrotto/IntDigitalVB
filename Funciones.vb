Public Module Funciones
    Dim cn As New SqlClient.SqlConnection(SqlConex)
    Public Function GetNombreSector(ByVal nSector As Integer) As String

        cn.Open()
        Dim wdescrip As String = ""
        Dim sql As String = "select descrip from SECTORES  where CODIGO=" & nSector
        Dim Psql As New SqlClient.SqlCommand(sql, cn)
        Dim dr As SqlClient.SqlDataReader = Psql.ExecuteReader
        While dr.Read()
            wdescrip = dr.GetString(0)
        End While
        dr.Close()
        cn.Close()
        Return wdescrip
    End Function

    Public Function CaracteresEspeciales(ByVal nPalabra As String) As Boolean
        Dim j As Integer = 1
        nPalabra = RTrim(nPalabra)
        Dim jm As Integer = Len(nPalabra)
        Dim fr As Integer = 0
        While j < jm And fr = 0
            Dim nLetra As String = Mid(nPalabra, j, 1)
            Dim keycode As Integer = Asc(nLetra)
            If (keycode > 64 And keycode < 91) Or (keycode > 96 And keycode < 123) Or keycode = 241 Or keycode = 209 Or keycode = 32 Or keycode = 39 Then
            Else
                fr = 1
            End If
            j = j + 1
        End While
        If fr = 1 Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Function CaracteresEspecialesn(ByVal nPalabra As String) As Boolean
        Dim j As Integer = 1
        nPalabra = RTrim(nPalabra)
        Dim jm As Integer = Len(nPalabra)
        Dim fr As Integer = 0
        While j < jm And fr = 0
            Dim nLetra As String = Mid(nPalabra, j, 1)
            Dim keycode As Integer = Asc(nLetra)
            If (keycode > 64 And keycode < 91) Or (keycode > 96 And keycode < 123) Or keycode = 241 Or keycode = 209 Or keycode = 32 Or keycode = 46 Then
            Else
                fr = 1
            End If
            j = j + 1
        End While
        If fr = 1 Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Function CaracteresEspecialesnumeros(ByVal nPalabra As String) As Boolean
        Dim j As Integer = 1
        nPalabra = RTrim(nPalabra)
        Dim jm As Integer = Len(nPalabra)
        Dim fr As Integer = 0
        While j < jm And fr = 0
            Dim nLetra As String = Mid(nPalabra, j, 1)
            Dim keycode As Integer = Asc(nLetra)
            If (keycode > 64 And keycode < 91) Or (keycode > 96 And keycode < 123) Or keycode = 241 Or keycode = 209 Or keycode = 32 Or (keycode >= 48 And keycode <= 57) Or keycode = 40 Or keycode = 41 Or keycode = 44 Or keycode = 33 Or keycode = 63 Or keycode = 46 Or keycode = 35 Or keycode = 176 Or keycode = 37 Then
            Else
                fr = 1
            End If
            j = j + 1
        End While
        If fr = 1 Then
            Return True
        Else
            Return False
        End If
    End Function


End Module