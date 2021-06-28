Option Strict Off
Option Explicit On
Public Class report_prod
	'%-------------------------------------------------------%'
	'% $Workfile:: report_prod.cls                          $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 12:36p                               $%'
	'% $Revision:: 22                                       $%'
	'%-------------------------------------------------------%'
	
	'+ Column_name                 Type           Computed   Length      Prec  Scale Nullable    TrimTrailingBlanks   FixedLenNullInSource    Collation
	'+ -------------------         -------------- ---------- ----------- ----- ----- ----------- -------------------- ----------------------- -----------
	Public nBranch As Integer 'smallint      no         2           5     0     no          (n/a)                (n/a)                   NULL
	Public nProduct As Integer 'smallint      no         2           5     0     no          (n/a)                (n/a)                   NULL
	Public dEffecdate As Date 'datetime      no         8                       no          (n/a)                (n/a)                   NULL
	Public dNulldate As Date 'datetime      no         8                       yes         (n/a)                (n/a)                   NULL
	Public nUsercode As Integer 'smallint      no         2           5     0     yes         (n/a)                (n/a)                   NULL
	Public sCodispl As String 'char          no         8                       no          no                   (n/a)                   NULL
	Public sCodCodispl As String 'char          no         8                       no          no                   (n/a)                   NULL
    Public nType_Report As Integer 'smallint      no         2           5     0     no          (n/a)                (n/a)                   NULL
    Public nRepType As Long     'number        no         5           5     0     yes          (n/a)               (n/a)                   NULL
    Public nTratypep As Long     'number        no         5           5     0     yes          (n/a)               (n/a)                   NULL
    Public sReport As String   'number        no         5           5     0     yes          (n/a)               (n/a)                   NULL

	
	'- Se definen las propiedades utilizadas en la ventana
	'- DP809 - Criterios técnicos - Selección de riesgo.
	
	Public sDesCurrency As String
	Public sDesCrite As String
    Public sDescript As String
    Public sDesRepType As String
    Public sDesTratypep As String
	
	'- Se define las constantes que contienen los máximos y minimos valores para las
	'- edades y capitales.
	
	Const MaxE As Integer = 130
	Const MinE As Integer = 0
	Const MaxCap As Double = 99999999#
	Const MinCap As Double = 1
	
	'%Add: Permite registrar la información de los criterios de selección de riesgos.
	Public Function Add() As Boolean
		Dim lrecCrereport_prod As eRemoteDB.Execute
		On Error GoTo Add_err
		lrecCrereport_prod = New eRemoteDB.Execute
		'+ Definición de parámetros para stored procedure 'insudb.crereport_prod'
		With lrecCrereport_prod
			.StoredProcedure = "crereport_prod"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCodCodispl", sCodCodispl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nReptype", nRepType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTratypep", nTratypep, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sReport", sReport, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 50, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Add = .Run(False)
		End With
Add_err: 
		If Err.Number Then
			Add = False
		End If
        lrecCrereport_prod = Nothing
		On Error GoTo 0
	End Function
	
	'%Update: Permite actualizar la información de los criterios de selección de riesgos.
	Public Function Update() As Boolean
		Dim lrecUpdreport_prod As eRemoteDB.Execute
		On Error GoTo Update_Err
		lrecUpdreport_prod = New eRemoteDB.Execute
		'+ Definición de parámetros para stored procedure 'insudb.insMortalityCre'
		With lrecUpdreport_prod
			.StoredProcedure = "updreport_prod"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCodCodispl", sCodCodispl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nReptype", nRepType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTratypep", nTratypep, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sReport", sReport, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 50, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Update = .Run(False)
		End With
Update_Err: 
		If Err.Number Then
			Update = False
		End If
        lrecUpdreport_prod = Nothing
		On Error GoTo 0
	End Function
	
	'%Delete: Permite borrar la información de criterios de selección de riesgos.
	Public Function Delete() As Boolean
		Dim lrecDereport_prod As eRemoteDB.Execute
		On Error GoTo Delete_Err
		lrecDereport_prod = New eRemoteDB.Execute
		'+ Definición de parámetros para stored procedure 'insudb.delMortality'
		With lrecDereport_prod
			.StoredProcedure = "delreport_prod"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCodCodispl", sCodCodispl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Delete = .Run(False)
		End With
Delete_Err: 
		If Err.Number Then
			Delete = False
		End If
        lrecDereport_prod = Nothing
		On Error GoTo 0
	End Function
	
	'% insValDP809: Realiza la validación de los campos puntuales de la página DP809 - Criterios técnicos - Selección de riesgo.
    Public Function insValDP809(ByVal sAction As String, ByVal nBranch As Long, _
                             ByVal nProduct As Long, ByVal dEffecdate As Date, _
                             ByVal sCodispl As String, ByVal sCodCodispl As String, _
                             ByVal nUsercode As Long, Optional ByVal nRepType As Long = 0, _
                             Optional ByVal nTratypep As Long = 0, Optional ByVal sReport As String = "") As String

        Dim lobjErrors As eFunctions.Errors
        Dim lrecinsValDP809 As eRemoteDB.Execute
        Dim sArrayerrors As String = String.Empty
        Dim nAction As Integer

        On Error GoTo insValDP809_Err

        Select Case sAction
            '+ Si la opción seleccionada es Registrar.
            Case "Add"
                nAction = 1
                '+ Si la opción seleccionada es Modificar.
            Case "Update"
                nAction = 2
            Case "Delete"
                nAction = 3
        End Select
        '+
        '+ Definición de store procedure insValDP809 al 06-30-2003 11:58:39
        '+
        lrecinsValDP809 = New eRemoteDB.Execute
        With lrecinsValDP809
            .StoredProcedure = "insValDP809"

            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sCodCodispl", sCodCodispl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nReptype", nRepType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTratypep", nTratypep, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sReport", sReport, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 50, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sArrayerrors", " ", eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(False) Then
                sArrayerrors = .Parameters("sArrayerrors").Value
            End If

        End With

        If sArrayerrors.Length > 0 Then
            lobjErrors = New eFunctions.Errors
            lobjErrors.ErrorMessage("DP809", , , , , , sArrayerrors)
            insValDP809 = lobjErrors.Confirm
            lobjErrors = Nothing
        End If

insValDP809_Err:
        If Err.Number Then
            insValDP809 = "insValDP809: " & Err.Description
        End If
        lobjErrors = Nothing
        lrecinsValDP809 = Nothing
    End Function
	'% insPostDP809: Esta función se encarga de almacenar los datos en las tablas, en este caso report_prod
	'% ventana DP809 - Criterios técnicos - Selección de riesgo.
    Public Function insPostDP809(ByVal sAction As String, ByVal nBranch As Long, ByVal nProduct As Long, ByVal dEffecdate As Date, _
                                 ByVal sCodispl As String, ByVal sCodCodispl As String, ByVal nUsercode As Long, _
                                 Optional ByVal nRepType As Long = 0, Optional ByVal nTratypep As Long = 0, Optional ByVal sReport As String = "") As Boolean
        Dim lclsreport_prods As eProduct.report_prods
        Dim lclsProd_win As Prod_win
        On Error GoTo insPostDP809_Err
        lclsreport_prods = New report_prods
        lclsProd_win = New eProduct.Prod_win

        insPostDP809 = True
        With Me
            .nBranch = nBranch
            .nProduct = nProduct
            .dEffecdate = dEffecdate
            .sCodispl = sCodispl
            .sCodCodispl = sCodCodispl
            .nUsercode = nUsercode
            .nRepType = nRepType
            .nTratypep = nTratypep
            .sReport = sReport
        End With
        Select Case sAction
            '+ Si la opción seleccionada es Registrar.
            Case "Add"
                insPostDP809 = Add()
                '+ Si la opción seleccionada es Modificar.
            Case "Update"
                insPostDP809 = Update()
            Case "Delete"
                insPostDP809 = Delete()
        End Select

        If lclsreport_prods.FindReport_prod(nBranch, nProduct, dEffecdate) Then
            Call lclsProd_win.Add_Prod_win(nBranch, nProduct, dEffecdate, "DP809", "2", nUsercode)
        Else
            Call lclsProd_win.Add_Prod_win(nBranch, nProduct, dEffecdate, "DP809", "1", nUsercode)
        End If

insPostDP809_Err:
        If Err.Number Then
            insPostDP809 = False
        End If
        On Error GoTo 0

        lclsreport_prods = Nothing
        lclsProd_win = Nothing

    End Function
	
	'% Find: Busca el codigo logico del cuador de polizas asociado al producto
	Public Function Find_V(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, Optional ByVal lblnFind As Boolean = True) As Boolean
		Dim lrecReaReport_prod As eRemoteDB.Execute
		
		lrecReaReport_prod = New eRemoteDB.Execute
		
		Find_V = True
		
		'+ Definición de parámetros para stored procedure 'insudb.reaConmutativ'.
		
		With lrecReaReport_prod
			.StoredProcedure = "REAREPORT_PROD_V"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				sCodispl = .FieldToClass("sCodispl")
			Else
				Find_V = False
			End If
		End With
        lrecReaReport_prod = Nothing
    End Function

    Public Function Find(ByVal nBranch As Long, _
                         ByVal nProduct As Long, _
                         ByVal dEffecdate As Date, _
                         ByVal sCodispl As String, _
                         ByVal nTratypep As Long)
        '-------------------------------------------------------------------------------------
        Dim lrecFind As eRemoteDB.Execute

        On Error GoTo Find_Err

        lrecFind = New eRemoteDB.Execute

        '+
        '+ Definición de store procedure reaNull_condi_o al 06-20-2002 10:09:58
        '+
        With lrecFind
            .StoredProcedure = "REAREPORT_PROD_COD"
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sCodispl", sCodispl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTratypep", nTratypep, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(True) Then
                Find = True
                Me.nBranch = .FieldToClass("nBranch")
                Me.nProduct = .FieldToClass("nProduct")
                Me.dEffecdate = .FieldToClass("dEffecdate")
                Me.nType_Report = .FieldToClass("nType_Report")
                Me.nRepType = .FieldToClass("nRepType")
                Me.nTratypep = .FieldToClass("nTratypep")
                Me.sReport = .FieldToClass("sReport")
                Me.sDesRepType = .FieldToClass("sDesRepType")
                Me.sDesTratypep = .FieldToClass("sDesTratypep")
            Else
                Find = False
            End If
        End With

Find_Err:
        If Err.Number Then
            Find = False
        End If
        lrecFind = Nothing
        On Error GoTo 0

    End Function


End Class