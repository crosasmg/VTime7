Option Strict Off
Option Explicit On
Public Class Section_pol
	'%-------------------------------------------------------%'
	'% $Workfile:: Section_pol.cls                        $%'
	'% $Author:: nmoreno                                   $%'
	'% $Date:: 16/07/07 17:20p                               $%'
	'% $Revision::                                        $%'
	'%-------------------------------------------------------%'
	
	'- Definición de la tabla Section_pol tomada el 16/03/2007 17:25
	'- Column_Name                                   Type      Length  Prec  Scale Nullable
	'------------------------------ --------------- - -------- ------- ----- ------ --------
	Public sCertype As String ' NUMBER        22     5      0 No
	Public nBranch As Integer ' NUMBER        22     5      0 No
	Public nProduct As Integer ' NUMBER        22     5      0 No
	Public nPolicy As Integer ' NUMBER        22     5      0 No
	Public nCertif As Integer ' NUMBER        22     5      0 No
	Public sCodispl_orig As String ' NUMBER        22     5      0 No
	Public sCodispl As String ' DATE           7              No
	Public dEffecfate As Date ' NUMBER        22     5      0 Yes
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
	'    Dim lrecReaSection_pol As eRemoteDB.Execute
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
	'        Set lrecReaSection_pol = New eRemoteDB.Execute
	'
	''+ Definición de parámetros para stored procedure 'insudb.reaSection_pol'
	''+ Información leída el 07/05/2002 15:39:55
	'
	'        With lrecReaSection_pol
	'            .StoredProcedure = "reaSection_pol"
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
	'    Set lrecReaSection_pol = Nothing
	'End Function
	
	'%Add: Crea un registro en la tabla
	'Public Function Add() As Boolean
	''--------------------------------------------------------------------------
	'    Add = InsUpdSection_pol(cintActionAdd)
	'End Function
	'
	''%Update: Actualiza los datos de la tabla
	''--------------------------------------------------------------------------
	'Public Function Update() As Boolean
	''--------------------------------------------------------------------------
	'    Update = InsUpdSection_pol(cintActionUpdate)
	'End Function
	'
	''%Delete: Borra los datos de la tabla
	''--------------------------------------------------------------------------
	'Public Function Delete() As Boolean
	''--------------------------------------------------------------------------
	'    Delete = InsUpdSection_pol(cintActionDel)
	'End Function
	
	'%InsValSection_pol: Lee los datos de la tabla, valida la existencia de una fila
	'Public Function InsValSection_pol(ByVal nBranch As Long, _
	''                                    ByVal nProduct As Long, _
	''                                    ByVal dEffecdate As Date, _
	''                                    Optional ByVal nExist As Long) As Boolean
	''-------------------------------------------------------------------------------------------------
	'    Dim lrecreaSection_pol_v As eRemoteDB.Execute
	'
	'    On Error GoTo reaSection_pol_v_Err
	'
	'    Set lrecreaSection_pol_v = New eRemoteDB.Execute
	'
	''+ Definición de store procedure reaSection_pol 06-05-2002 19:42:00
	'    With lrecreaSection_pol_v
	'        .StoredProcedure = "reaSection_pol_v"
	'            .Parameters.Add "nBranch", nBranch, rdbParamInput, rdbInteger, 22, 0, 10, rdbParamNullable
	'            .Parameters.Add "nProduct", nProduct, rdbParamInput, rdbInteger, 22, 0, 10, rdbParamNullable
	'            .Parameters.Add "nWay_pay", nWay_pay, rdbParamInput, rdbInteger, 22, 0, 10, rdbParamNullable
	'            .Parameters.Add "nPayFreq", nPayFreq, rdbParamInput, rdbInteger, 22, 0, 10, rdbParamNullable
	'            .Parameters.Add "dEffecdate", dEffecdate, rdbParamInput, rdbDBTimeStamp, 0, 0, 0, rdbParamNullable
	'            .Parameters.Add "nExist", nExist, rdbParamInputOutput, rdbNumeric, 22, 0, 10, rdbParamNullable
	'        If .Run(False) Then
	'            InsValSection_pol = .Parameters("nExist").Value = 1
	'        Else
	'            InsValSection_pol = False
	'        End If
	'    End With
	'
	'reaSection_pol_v_Err:
	'    If Err Then
	'        InsValSection_pol = False
	'    End If
	'
	'    Set lrecreaSection_pol_v = Nothing
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
	'                Call InsValSection_pol(nBranch, _
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
	'%insPostCA659: Esta función realiza los cambios de BD según especificaciones funcionales
	'%                 de la transacción (CA659), a la tabla Section_pol
	Public Function insPostCA659(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Integer, ByVal nCertif As Integer, ByVal sCodispl_orig As String, ByVal sCodispl As String, ByVal sSel As Integer, ByVal nUsercode As Integer, ByVal dEffecdate As Date) As Boolean
		Dim lrecinsPostCA659 As eRemoteDB.Execute
		
		On Error GoTo insPostCA659_Err
		
		lrecinsPostCA659 = New eRemoteDB.Execute
		
		With lrecinsPostCA659
			.StoredProcedure = "InsupdSection_pol"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 1, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCodispl_orig", sCodispl_orig, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCodispl", sCodispl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sSel", sSel, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				insPostCA659 = True
			Else
				insPostCA659 = False
			End If
		End With
		
insPostCA659_Err: 
		If Err.Number Then
			insPostCA659 = False
		End If
		'UPGRADE_NOTE: Object lrecinsPostCA659 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsPostCA659 = Nothing
	End Function
	
	Public Function Find_section_pol(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Integer, ByVal nCertif As Integer, ByVal nUsercode As Integer, ByVal dEffecdate As Date) As Object
		Dim lrecreainssection_pol As eRemoteDB.Execute
		
		On Error GoTo Find_section_pol_Err
		
		lrecreainssection_pol = New eRemoteDB.Execute
		
		With lrecreainssection_pol
			.StoredProcedure = "Reainssection_pol"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				Find_section_pol = True
			Else
				Find_section_pol = False
			End If
		End With
		
Find_section_pol_Err: 
		If Err.Number Then
			Find_section_pol = False
		End If
		
		'UPGRADE_NOTE: Object lrecreainssection_pol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreainssection_pol = Nothing
		On Error GoTo 0
		
	End Function
End Class






