Option Strict Off
Option Explicit On
Public Class Section_prod
	'%-------------------------------------------------------%'
	'% $Workfile:: Section_prod.cls                        $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 12:35p                               $%'
	'% $Revision:: 22                                       $%'
	'%-------------------------------------------------------%'
	
	'- Definición de la tabla Section_prod tomada el 07/05/2002 15:31
	'- Column_Name                                   Type      Length  Prec  Scale Nullable
	'------------------------------ --------------- - -------- ------- ----- ------ --------
	Public nBranch As Integer ' NUMBER        22     5      0 No
	Public nProduct As Integer ' NUMBER        22     5      0 No
	Public sCodispl_orig As String ' NUMBER        22     5      0 No
	Public sCodispl As String ' DATE           7              No
	Public dEffecfate As Integer ' NUMBER        22     5      0 Yes
	Public Nsequence As Integer
	Public sDescript As String
	
	Public bError As Boolean
	'- Propiedades auxiliares
	Public nExist As Integer
	Public lintExist As Integer
	
	'    Private Const cintActionAdd = 1
	'    Private Const cintActionUpdate = 2
	'    Private Const cintActionDel = 3
	
	'% Find: Busca la información de un determinado Ramo/Producto/
	'Public Function Find(ByVal nBranch As Long, _
	''                     ByVal nProduct As Long, _
	''                     Optional ByVal bFind As Boolean) As Boolean
	''---------------------------------------------------------------------------------------------------
	'    Dim lrecReaSection_prod As eRemoteDB.Execute
	'    Find = True
	'    On Error GoTo Find_Err
	'
	'    If nBranch <> Me.nBranch Or _
	''       nProduct <> Me.nProduct Or _
	''       nWay_pay <> Me.nWay_pay Or _
	''       nPayFreq <> Me.nPayFreq Or _
	''       dEffecdate <> Me.dEffecdate Or _
	''       bFind Then
	'
	'        Set lrecReaSection_prod = New eRemoteDB.Execute
	'
	''+ Definición de parámetros para stored procedure 'insudb.reaSection_prod'
	''+ Información leída el 07/05/2002 15:39:55
	'
	'        With lrecReaSection_prod
	'            .StoredProcedure = "reaSection_prod"
	'            .Parameters.Add "nBranch", nBranch, rdbParamInput, rdbInteger, 22, 0, 10, rdbParamNullable
	'            .Parameters.Add "nProduct", nProduct, rdbParamInput, rdbInteger, 22, 0, 10, rdbParamNullable
	'
	'            .Parameters.Add "dEffecdate", dEffecdate, rdbParamInput, rdbDBTimeStamp, 0, 0, 0, rdbParamNullable
	'            If .Run Then
	'                Me.nBranch = .FieldToClass("nBranch")
	'                Me.nProduct = .FieldToClass("nProduct")
	'                Me.dEffecdate = .FieldToClass("dEffecdate")
	'                Me.dNulldate = .FieldToClass("dNulldate")
	'                .RCloseRec
	'            Else
	'                Find = False
	'            End If
	'        End With
	'    End If
	'
	'Find_Err:
	'    If Err Then
	'       Find = False
	'    End If
	'
	'    On Error GoTo 0
	'    Set lrecReaSection_prod = Nothing
	'End Function
	
	'%Add: Crea un registro en la tabla
	'Public Function Add() As Boolean
	''--------------------------------------------------------------------------
	'    Add = InsUpdSection_prod(cintActionAdd)
	'End Function
	'
	''%Update: Actualiza los datos de la tabla
	''--------------------------------------------------------------------------
	'Public Function Update() As Boolean
	''--------------------------------------------------------------------------
	'    Update = InsUpdSection_prod(cintActionUpdate)
	'End Function
	'
	''%Delete: Borra los datos de la tabla
	''--------------------------------------------------------------------------
	'Public Function Delete() As Boolean
	''--------------------------------------------------------------------------
	'    Delete = InsUpdSection_prod(cintActionDel)
	'End Function
	
	'%InsValSection_prod: Lee los datos de la tabla, valida la existencia de una fila
	'Public Function InsValSection_prod(ByVal nBranch As Long, _
	''                                    ByVal nProduct As Long, _
	''                                    ByVal dEffecdate As Date, _
	''                                    Optional ByVal nExist As Long) As Boolean
	''-------------------------------------------------------------------------------------------------
	'    Dim lrecreaSection_prod_v As eRemoteDB.Execute
	'
	'    On Error GoTo reaSection_prod_v_Err
	'
	'    Set lrecreaSection_prod_v = New eRemoteDB.Execute
	'
	''+ Definición de store procedure reaSection_prod 06-05-2002 19:42:00
	'    With lrecreaSection_prod_v
	'        .StoredProcedure = "reaSection_prod_v"
	'            .Parameters.Add "nBranch", nBranch, rdbParamInput, rdbInteger, 22, 0, 10, rdbParamNullable
	'            .Parameters.Add "nProduct", nProduct, rdbParamInput, rdbInteger, 22, 0, 10, rdbParamNullable
	'            .Parameters.Add "nWay_pay", nWay_pay, rdbParamInput, rdbInteger, 22, 0, 10, rdbParamNullable
	'            .Parameters.Add "nPayFreq", nPayFreq, rdbParamInput, rdbInteger, 22, 0, 10, rdbParamNullable
	'            .Parameters.Add "dEffecdate", dEffecdate, rdbParamInput, rdbDBTimeStamp, 0, 0, 0, rdbParamNullable
	'            .Parameters.Add "nExist", nExist, rdbParamInputOutput, rdbNumeric, 22, 0, 10, rdbParamNullable
	'        If .Run(False) Then
	'            InsValSection_prod = .Parameters("nExist").Value = 1
	'        Else
	'            InsValSection_prod = False
	'        End If
	'    End With
	'
	'reaSection_prod_v_Err:
	'    If Err Then
	'        InsValSection_prod = False
	'    End If
	'
	'    Set lrecreaSection_prod_v = Nothing
	'    On Error GoTo 0
	'End Function
	'%insValDP578: Esta función se encarga de validar los datos del Form
	'%Vias de pago por producto
	'Public Function insValDP578(ByVal sCodispl As String, _
	''                            ByVal sAction As String, _
	''                            ByVal nBranch As Long, _
	''                            ByVal nProduct As Long, _
	''                            ByVal nWay_pay As Long, _
	''                            ByVal nPayFreq As Long, _
	''                            ByVal dEffecdate As Date, _
	''                            ByVal nCurrency As Long, _
	''                            ByVal nPre_issue As Double, _
	''                            ByVal nPre_amend As Double) As String
	''---------------------------------------------------------------------------------------------
	'
	''- Se define el objeto para el manejo de las clases
	'    Dim lobjErrors As eFunctions.Errors
	'    Dim lobjValues As eFunctions.Values
	'    Dim lblnError  As Boolean
	'
	'    Dim lintBranch As Long
	'    Dim lintProduct As Long
	'
	'    Set lobjErrors = New eFunctions.Errors
	'    Set lobjValues = New eFunctions.Values
	'
	'    On Error GoTo insValDP578_Err
	'    lblnError = False
	'
	''+ Validación de Moneda
	'    With lobjErrors
	'        If (nCurrency = NumNull Or nCurrency = 0) And _
	''           ((nPre_issue <> NumNull And nPre_issue <> 0) Or _
	''           (nPre_amend <> NumNull And nPre_amend <> 0)) Then
	'            Call .ErrorMessage(sCodispl, 1351)
	'        End If
	'    End With
	'
	''+ Valida la exitencia previa del registro Ramo/Producto/Via de pago/Frecuencia de Pago/Fecha efecto
	''+ al agregar una fila
	'    With lobjErrors
	'        If sAction = "Add" Then
	'            If Not lblnError Then
	'                lintExist = 0
	'                Call InsValSection_prod(nBranch, _
	''                                         nProduct, _
	''                                         nWay_pay, _
	''                                         nPayFreq, _
	''                                         dEffecdate, _
	''                                         lintExist)
	'               If lintExist = 1 Then
	'                  Call .ErrorMessage(sCodispl, 10284)
	'               End If
	'            End If
	'        End If
	'    End With
	'
	'
	'
	'insValDP578 = lobjErrors.Confirm
	'
	'    Set lobjErrors = Nothing
	'    Set lobjValues = Nothing
	'
	'insValDP578_Err:
	'    If Err Then
	'        insValDP578 = "insValDP578: " & Err.Description
	'    End If
	'
	'    On Error GoTo 0
	'End Function
	'%insPostDP809: Esta función realiza los cambios de BD según especificaciones funcionales
	'%                 de la transacción (DP809), a la tabla section_prod
	Public Function insPostDP809(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal sCodispl_orig As String, ByVal sCodispl As String, ByVal sSel As Integer, ByVal nUsercode As Integer, ByVal dEffecdate As Date) As Boolean
		Dim lrecinsPostDP809 As eRemoteDB.Execute
		
		On Error GoTo insPostDP809_Err
		
		lrecinsPostDP809 = New eRemoteDB.Execute
		
		With lrecinsPostDP809
			.StoredProcedure = "Insupdsection_prod"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCodispl_orig", sCodispl_orig, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCodispl", sCodispl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sSel", sSel, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				insPostDP809 = True
			Else
				insPostDP809 = False
			End If
		End With
		
insPostDP809_Err: 
		If Err.Number Then
			insPostDP809 = False
		End If
		'UPGRADE_NOTE: Object lrecinsPostDP809 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsPostDP809 = Nothing
	End Function
	
	'%InsUpdSection_prod: Realiza la actualización de la tabla
	'Private Function InsUpdSection_prod(ByVal nAction As Long) As Boolean
	''--------------------------------------------------------------------------
	'    Dim lrecInsUpdSection_prod As eRemoteDB.Execute
	'
	'    On Error GoTo InsUpdSection_prod_Err
	'
	'    Set lrecInsUpdSection_prod = New eRemoteDB.Execute
	'
	''+ Definición de parámetros para stored procedure 'InsUpdSection_prod'
	''+ Información leída el 07/05/2002
	'    With lrecInsUpdSection_prod
	'        .StoredProcedure = "InsUpdSection_prod"
	'        .Parameters.Add "nAction", nAction, rdbParamInput, rdbNumeric, 22, 0, 38, rdbParamNullable
	'        .Parameters.Add "nBranch", nBranch, rdbParamInput, rdbInteger, 22, 0, 10, rdbParamNullable
	'        .Parameters.Add "nProduct", nProduct, rdbParamInput, rdbInteger, 22, 0, 10, rdbParamNullable
	'        .Parameters.Add "nWay_pay", nWay_pay, rdbParamInput, rdbInteger, 22, 0, 10, rdbParamNullable
	'        .Parameters.Add "nPayFreq", nPayFreq, rdbParamInput, rdbInteger, 22, 0, 10, rdbParamNullable
	'        .Parameters.Add "dEffecdate", dEffecdate, rdbParamInput, rdbDBTimeStamp, 0, 0, 0, rdbParamNullable
	'        .Parameters.Add "nCurrency", nCurrency, rdbParamInput, rdbInteger, 22, 0, 10, rdbParamNullable
	'        .Parameters.Add "nPre_issue", nPre_issue, rdbParamInput, rdbNumeric, 22, 6, 18, rdbParamNullable
	'        .Parameters.Add "nPre_amend", nPre_amend, rdbParamInput, rdbNumeric, 22, 6, 18, rdbParamNullable
	'        .Parameters.Add "nUsercode", nUsercode, rdbParamInput, rdbInteger, 22, 0, 10, rdbParamNullable
	'        InsUpdSection_prod = .Run(False)
	'    End With
	'
	'InsUpdSection_prod_Err:
	'    If Err Then
	'        InsUpdSection_prod = False
	'    End If
	'
	'    Set lrecInsUpdSection_prod = Nothing
	'    On Error GoTo 0
	'End Function
	
	'* Class_Initialize: se controla la apertura de la clase
	'Private Sub Class_Initialize()
	'    nUsercode = NumNull
	'    nBranch = NumNull
	'    nProduct = NumNull
	'    dNulldate = dtmNull
	'    sCodispl_orig = NumNull
	'    sCodispl = NumNull
	'    dEffecfate = NumNull
	'    Nsequence = NumNull
	'    sDescript = strNull
	'
	'End Sub
	
	'% Find_O: Busca si existen registros en la tabla frecuencias permitidas por vías
	'%       de pago y producto
	'Public Function Find_O(ByVal nBranch As Long, _
	''                       ByVal nProduct As Long, _
	''                       ByVal nWay_pay As Long, _
	''                       ByVal dEffecdate As Date) As Boolean
	''---------------------------------------------------------------------------------------------------
	'    Dim lrecreaSection_prod_o As eRemoteDB.Execute
	'
	'    On Error GoTo reaSection_prod_o_Err
	'
	'    Set lrecreaSection_prod_o = New eRemoteDB.Execute
	'
	''+
	''+ Definición de store procedure reaSection_prod_o al 07-29-2002 10:48:06
	''+
	'    With lrecreaSection_prod_o
	'        .StoredProcedure = "reaSection_prod_o"
	'        .Parameters.Add "nBranch", nBranch, rdbParamInput, rdbInteger, 22, 0, 10, rdbParamNullable
	'        .Parameters.Add "nProduct", nProduct, rdbParamInput, rdbInteger, 22, 0, 10, rdbParamNullable
	'        .Parameters.Add "nWay_pay", nWay_pay, rdbParamInput, rdbInteger, 22, 0, 10, rdbParamNullable
	'        .Parameters.Add "dEffecdate", dEffecdate, rdbParamInput, rdbDBTimeStamp, 0, 0, 0, rdbParamNullable
	'
	'        If .Run(True) Then
	'            Find_O = True
	'        Else
	'            Find_O = False
	'        End If
	'    End With
	'
	'reaSection_prod_o_Err:
	'    If Err Then
	'        Find_O = False
	'    End If
	'
	'    Set lrecreaSection_prod_o = Nothing
	'    On Error GoTo 0
	'
	'End Function
	
	Public Sub inspreDP809(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date)
        Dim lclsSequen_pol As Sequen_pol

        lclsSequen_pol = New Sequen_pol
		
		If Not lclsSequen_pol.valSequenByProduct(nBranch, nProduct, dEffecdate) Then
			bError = True
		End If
	End Sub
End Class






