Option Strict Off
Option Explicit On
Public Class Bas_sumins
	'%-------------------------------------------------------%'
	'% $Workfile:: Bas_sumins.cls                           $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 12:35p                               $%'
	'% $Revision:: 11                                       $%'
	'%-------------------------------------------------------%'
	
	'- Estructura de tabla BAS_SUMINS al 05-30-2002 10:16:56
	'-       Property                Type         DBType   Size Scale  Prec  Null
	Public nBranch As Integer ' NUMBER     22   0     5    N
	Public nSumins_co As Integer ' NUMBER     22   0     5    N
	Public nProduct As Integer ' NUMBER     22   0     5    N
	Public dEffecdate As Date ' DATE       7    0     0    N
	Public sDescript As String ' CHAR       30   0     0    S
	Public sShort_des As String ' CHAR       12   0     0    S
	Public dNulldate As Date ' DATE       7    0     0    S
	Public nUsercode As Integer ' NUMBER     22   0     5    N
	
	'- Variables auxiliares
	
	Public nSumins_co_max As Integer
	Public dDate As Date
	Public nSumins_rat As Double
	
	'%Delete: Elimina un capital asegurado según código, de la tabla Bas_sumins
	Public Function Delete() As Boolean
		
		'-Se define la variable lrecdelBas_sumins
		
		Dim lrecdelBas_sumins As eRemoteDB.Execute
		
		On Error GoTo Delete_Err
		
		lrecdelBas_sumins = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'insudb.delBas_sumins'
		'+Información leída el 02/04/2001 02:57:50 p.m.
		
		With lrecdelBas_sumins
			.StoredProcedure = "delBas_sumins"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSumins_co", nSumins_co, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			Delete = .Run(False)
		End With
		'UPGRADE_NOTE: Object lrecdelBas_sumins may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecdelBas_sumins = Nothing
		
Delete_Err: 
		If Err.Number Then
			Delete = False
		End If
		
		On Error GoTo 0
		
	End Function
	
	'%FindMax: Esta rutina encuentra el valor máximo del código del capital básico
	Public Function FindMax(ByVal nBranch As Integer, ByVal nProduct As Integer) As Boolean
		
		'-Se define la variable lrecreaBas_suminsMax
		
		Dim lrecreaBas_suminsMax As eRemoteDB.Execute
		
		On Error GoTo FindMax_Err
		
		lrecreaBas_suminsMax = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'insudb.reaBas_suminsMax'
		'+Información leída el 02/04/2001 05:19:25 p.m.
		
		With lrecreaBas_suminsMax
			.StoredProcedure = "reaBas_suminsMax"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				If .FieldToClass("Maximum") = eRemoteDB.Constants.intNull Then
					nSumins_co_max = 1
				Else
					nSumins_co_max = .FieldToClass("Maximum")
				End If
				.RCloseRec()
				FindMax = True
			Else
				FindMax = False
			End If
		End With
		'UPGRADE_NOTE: Object lrecreaBas_suminsMax may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaBas_suminsMax = Nothing
		
FindMax_Err: 
		If Err.Number Then
			FindMax = False
		End If
		
		On Error GoTo 0
		
	End Function
	
	'%Update: Actualiza los capitales básicos asegurados en la tabla Bas_sumins
	Public Function Update() As Boolean
		
		'-Se define la variable lrecinsBas_sumins
		
		Dim lrecinsBas_sumins As eRemoteDB.Execute
		
		On Error GoTo Update_Err
		
		lrecinsBas_sumins = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'insudb.insBas_sumins'
		'+Información leída el 02/04/2001 11:56:01 a.m.
		
		With lrecinsBas_sumins
			.StoredProcedure = "insBas_sumins"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSumins_co", nSumins_co, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dDate", dDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDescript", sDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sShort_des", sShort_des, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			Update = .Run(False)
		End With
		'UPGRADE_NOTE: Object lrecinsBas_sumins may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsBas_sumins = Nothing
		
Update_Err: 
		If Err.Number Then
			Update = False
		End If
		
		On Error GoTo 0
		
	End Function
	
	'%insValDP062: Esta rutina realiza la validacion de los datos de capitales básicos.
    Public Function insValDP062(ByVal sCodispl As String, ByVal sDescript As String, ByVal sShort_des As String, ByVal nContent As Integer, ByVal sWindowType As String) As String

        Dim lclsErrors As eFunctions.Errors
        Dim lcolBas_sumins As Bas_suminses

        lclsErrors = New eFunctions.Errors
        lcolBas_sumins = New Bas_suminses

        If sWindowType = "PopUp" Then
            '+Descripción del capital
            If sDescript = String.Empty Then
                Call lclsErrors.ErrorMessage("DP062", 11351)
            End If

            '+Descripción corta del capital
            If sShort_des = String.Empty Then
                Call lclsErrors.ErrorMessage("DP062", 11352)
            End If
        Else
            '+Validar que exista informacion en el grid.
            If nContent = 0 Then
                Call lclsErrors.ErrorMessage("DP062", 1928)
            End If
        End If

        insValDP062 = lclsErrors.Confirm

        lclsErrors = Nothing
        lcolBas_sumins = Nothing

insValDP062_err:
        If Err.Number Then
            insValDP062 = insValDP062 & Err.Description
        End If

        On Error GoTo 0

    End Function
	
	'%insPostDP062: Este metodo se encarga de realizar el impacto en la base de datos (descrito en las
	'%especificaciones funcionales)de la ventana "DP062"
    Public Function insPostDP062(ByVal sAction As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal sDescript As String, ByVal sShort_des As String, ByVal nSumins As Integer, ByVal nUsercode As Integer) As Boolean

        Dim lclsBas_sumins As Bas_sumins
        Dim lcolBas_sumins As Bas_suminses
        Dim lclsProd_win As Prod_win
        Dim lrecBas_sumins As eRemoteDB.Execute

        lclsBas_sumins = New Bas_sumins
        lcolBas_sumins = New Bas_suminses
        lclsProd_win = New Prod_win
        lrecBas_sumins = New eRemoteDB.Execute

        If sAction = "Add" Or sAction = "Update" Then
            With lclsBas_sumins
                .nBranch = nBranch
                .nProduct = nProduct
                If nSumins = eRemoteDB.Constants.intNull Or nSumins = 0 Then
                    Call lclsBas_sumins.FindMax(nBranch, nProduct)
                    .nSumins_co = .nSumins_co_max + 1
                Else
                    .nSumins_co = nSumins
                End If
                .dDate = dEffecdate
                .sDescript = sDescript
                .sShort_des = sShort_des
                .nUsercode = nUsercode
                insPostDP062 = .Update
            End With
        Else
            With lclsBas_sumins
                .nBranch = nBranch
                .nProduct = nProduct
                .nSumins_co = nSumins
                .dDate = dEffecdate
                .nUsercode = nUsercode
                insPostDP062 = .Delete
            End With
        End If

        If lcolBas_sumins.Find(nBranch, nProduct, dEffecdate) Then
            Call lclsProd_win.Add_Prod_win(nBranch, nProduct, dEffecdate, "DP062", "2", nUsercode)
        Else
            Call lclsProd_win.Add_Prod_win(nBranch, nProduct, dEffecdate, "DP062", "1", nUsercode)
        End If

        'UPGRADE_NOTE: Object lclsBas_sumins may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsBas_sumins = Nothing
        'UPGRADE_NOTE: Object lcolBas_sumins may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lcolBas_sumins = Nothing
        'UPGRADE_NOTE: Object lclsProd_win may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsProd_win = Nothing
        'UPGRADE_NOTE: Object lrecBas_sumins may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecBas_sumins = Nothing

    End Function
	
	'%IniatializeValues: se inicializan los valores de las variables públicas de la clase
	Private Sub IniatializeValues()
		nBranch = eRemoteDB.Constants.intNull
		nSumins_co = eRemoteDB.Constants.intNull
		nProduct = eRemoteDB.Constants.intNull
		dEffecdate = eRemoteDB.Constants.dtmNull
		sDescript = String.Empty
		sShort_des = String.Empty
		nSumins_rat = eRemoteDB.Constants.intNull
		dNulldate = eRemoteDB.Constants.dtmNull
		
	End Sub
	
	'%Class_Initialize: se inicializan los valores de la clase
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		Call IniatializeValues()
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
End Class






