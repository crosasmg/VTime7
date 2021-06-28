Option Strict Off
Option Explicit On
Public Class Agencies
	
	'+
	'+ Estructura de tabla insudb.agencies al 11-06-2001 16:19:49
	'+     Property                Type         DBType   Size Scale  Prec  Null
	'+-------------------------------------------------------------------------
	Public nAgency As Integer ' NUMBER     22   0     5    N
	Public nOfficeagen As Integer ' NUMBER     22   0     5    N
	Public nBran_off As Integer ' NUMBER     22   0     5    N
	Public nUsercode As Integer ' NUMBER     22   0     5    N
	Public dCompdate As Date ' DATE       7    0     0    N
	
    Public Function Find(ByVal nAgency As Object) As Boolean
        Dim lrecreaAgencies As eRemoteDB.Execute
        On Error GoTo reaAgencies_Err
        lrecreaAgencies = New eRemoteDB.Execute

        '+
        '+ Definición de store procedure reaAgencies al 11-06-2001 16:20:30
        '+
        With lrecreaAgencies
            .StoredProcedure = "reaAgencies"
            .Parameters.Add("nAgency", nAgency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run(True) Then
                Me.nAgency = .FieldToClass("nAgency")
                Me.nOfficeagen = .FieldToClass("nOfficeagen")
                Me.nBran_off = .FieldToClass("nBran_off")
                Me.nUsercode = .FieldToClass("nUsercode")
                Me.dCompdate = .FieldToClass("dCompdate")
                .RCloseRec()
            End If
        End With
reaAgencies_Err:
        If Err.Number Then
            Find = False
        End If
        'UPGRADE_NOTE: Object lrecreaAgencies may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaAgencies = Nothing
        On Error GoTo 0

    End Function
End Class






