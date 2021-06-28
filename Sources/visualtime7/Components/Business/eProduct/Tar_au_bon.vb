Option Strict Off
Option Explicit On
Public Class Tar_au_bon
	'%-------------------------------------------------------%'
	'% $Workfile:: Tar_au_bon.cls                           $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 12:36p                               $%'
	'% $Revision:: 11                                       $%'
	'%-------------------------------------------------------%'
	
	'+ Propiedades según la tabla en el sistema al 09/07/2002
	'+ Los campos llave de la tabla corresponden a nBranch , nProduct, nYear, nClaimrat, dEffecdate
	
	'+ Column_name        Type
	'-------------------- ---------------------
	Public nYear As Integer 'NUMBER(5)
	Public nBranch As Integer 'NUMBER(5)
	Public nClaimrat As Double 'NUMBER(6,2)
	Public nProduct As Integer 'NUMBER(5)
	Public dEffecdate As Date 'datetime
	Public nDiscount As Double 'NUMBER(5,2)
	Public nUsercode As Integer 'NUMBER(5)
	
	'% Delete: Elimina los datos de la tabla
	Public Function Delete() As Boolean
		Dim lrecinsdelTar_Au_bon As eRemoteDB.Execute
		
		On Error GoTo Delete_Err
		
		lrecinsdelTar_Au_bon = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure lrecdelSequen_pol
		'+ Información leída el 14/05/2001 11:39:59
		
		With lrecinsdelTar_Au_bon
			.StoredProcedure = "insdelTar_Au_bon"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nYear", nYear, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nClaimrat", nClaimrat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 6, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("datEfecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDiscount", nDiscount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			Delete = .Run(False)
		End With
		
Delete_Err: 
		If Err.Number Then
			Delete = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecinsdelTar_Au_bon may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsdelTar_Au_bon = Nothing
	End Function
	
	'% Update: Actualiza un registro de la tabla Tar_Au_bon
	Public Function Update() As Boolean
		Dim lrecinsTar_Au_bon As eRemoteDB.Execute
		
		On Error GoTo Update_Err
		
		lrecinsTar_Au_bon = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'insudb.insTar_Au_bon'
		'+ Información leída el 14/05/2001 13:50:56
		
		With lrecinsTar_Au_bon
			.StoredProcedure = "insTar_Au_bon"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nYear", nYear, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nClaimrat", nClaimrat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 6, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("datEfecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDiscount", nDiscount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			Update = .Run(False)
		End With
		
Update_Err: 
		If Err.Number Then
			Update = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecinsTar_Au_bon may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsTar_Au_bon = Nothing
	End Function
	
	'% InsValDP041: Se realizan las validaciones de la página
	Public Function insValDP041(ByVal sWindowType As String, ByVal sAction As String, ByVal sCodispl As String, ByVal nCount As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal nYear As Integer, ByVal nClaimrat As Double, ByVal nDiscount As Double) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lcolTar_au_bons As eProduct.Tar_au_bons
		Dim lclsTar_au_bons As eProduct.Tar_au_bon
		
		insValDP041 = CStr(True)
		
		On Error GoTo insValDP041_Err
		
		lclsErrors = New eFunctions.Errors
		
		With lclsErrors
			If sWindowType = "PopUp" Then
				lcolTar_au_bons = New eProduct.Tar_au_bons
				
				If nYear = eRemoteDB.Constants.intNull And nClaimrat = eRemoteDB.Constants.intNull Then
					Call .ErrorMessage(sCodispl, 11390)
				Else
					If nDiscount = eRemoteDB.Constants.intNull Then
						Call .ErrorMessage(sCodispl, 1020)
					End If
				End If
				
				If sAction = "Add" Then
					If lcolTar_au_bons.Find(nBranch, nProduct, dEffecdate) Then
						For	Each lclsTar_au_bons In lcolTar_au_bons
							nYear = IIf(nYear = eRemoteDB.Constants.intNull, 0, nYear)
							nClaimrat = IIf(nClaimrat = eRemoteDB.Constants.intNull, 0, nClaimrat)
							If lclsTar_au_bons.nYear = nYear And lclsTar_au_bons.nClaimrat = nClaimrat Then
								Call .ErrorMessage(sCodispl, 11176)
							End If
						Next lclsTar_au_bons
					End If
				End If
				
				If nClaimrat > 100 Then
					Call .ErrorMessage(sCodispl, 10057)
                End If

                If nYear <> eRemoteDB.Constants.intNull And nClaimrat <> eRemoteDB.Constants.intNull Then
                    Call .ErrorMessage(sCodispl, 90000040)
                End If

            Else
                If nCount = 0 Then
                    Call .ErrorMessage(sCodispl, 1928)
                End If
            End If

            insValDP041 = .Confirm
        End With
		
insValDP041_Err: 
		If Err.Number Then
			insValDP041 = "insValDP041: " & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		'UPGRADE_NOTE: Object lclsTar_au_bons may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsTar_au_bons = Nothing
		'UPGRADE_NOTE: Object lcolTar_au_bons may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lcolTar_au_bons = Nothing
	End Function
	
	'% Find: Devuelve la información de los Descuentos por Siniestralidad
	Public Function Find(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal nUsercode As Integer, Optional ByVal lblnFind As Boolean = False) As Boolean
		Dim lrecreaTar_au_bonDP041 As eRemoteDB.Execute
		Static lblnRead As Boolean
		
		On Error GoTo Find_Err
		
		lrecreaTar_au_bonDP041 = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'insudb.reaTar_au_bonDP041'
		'+ Información leída el 14/05/2001 14:08:12
		
		With lrecreaTar_au_bonDP041
			.StoredProcedure = "reaTar_au_bonDP041"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				Me.nYear = .FieldToClass("nYear")
				Me.nClaimrat = .FieldToClass("nClaimrat")
				Me.nDiscount = .FieldToClass("nDiscount")
				With Me
					.nBranch = nBranch
					.nProduct = nProduct
					.dEffecdate = dEffecdate
					.nUsercode = nUsercode
				End With
				.RCloseRec()
				lblnRead = True
			Else
				lblnRead = False
			End If
		End With
		
Find_Err: 
		If Err.Number Then
			lblnRead = False
		End If
		Find = lblnRead
		'UPGRADE_NOTE: Object lrecreaTar_au_bonDP041 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaTar_au_bonDP041 = Nothing
	End Function
	
	'% insUpdTar_au_bonDP041: Modifica la tabla tar_au_bon
	Private Function insUpdTar_au_bonDP041(ByVal sAction As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal nYear As Integer, ByVal nClaimrat As Double, ByVal nDiscount As Double, ByVal nUsercode As Integer) As Boolean
		Dim lclsTar_au_bon As eProduct.Tar_au_bon
		
		On Error GoTo insUpdTar_au_bonDP041_Err
		
		lclsTar_au_bon = New eProduct.Tar_au_bon
		
		With Me
			.nBranch = nBranch
			.nProduct = nProduct
			.dEffecdate = dEffecdate
			.nYear = IIf(nYear = eRemoteDB.Constants.intNull, 0, nYear)
			.nClaimrat = IIf(nClaimrat = eRemoteDB.Constants.intNull, 0, nClaimrat)
			.nDiscount = nDiscount
			.nUsercode = nUsercode
		End With
		
		Select Case sAction
			Case "Del"
				insUpdTar_au_bonDP041 = Delete
			Case "Update"
				If lclsTar_au_bon.Delete Then
					insUpdTar_au_bonDP041 = Update
				End If
			Case "Add"
				insUpdTar_au_bonDP041 = Update
		End Select
		
insUpdTar_au_bonDP041_Err: 
		If Err.Number Then
			insUpdTar_au_bonDP041 = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsTar_au_bon may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsTar_au_bon = Nothing
	End Function
	
	'% insPostDP041: se actualizan los campos en la tabla
	Public Function insPostDP041(ByVal sAction As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal nYear As Integer, ByVal nClaimrat As Double, ByVal nDiscount As Double, ByVal nUsercode As Integer) As Boolean
		Dim lclsProd_win As Prod_win
		Dim lclsTar_au_bons As eProduct.Tar_au_bons
		Dim lstrContent As String
		
		On Error GoTo insPostDP041Upd_Err
		
		lclsTar_au_bons = New eProduct.Tar_au_bons
		
		insPostDP041 = insUpdTar_au_bonDP041(sAction, nBranch, nProduct, dEffecdate, nYear, nClaimrat, nDiscount, nUsercode)
		
		lstrContent = "1"
		If insPostDP041 Then
			lclsProd_win = New Prod_win
			If GetCountTar_au_bon(nBranch, nProduct, dEffecdate) > 0 Then
				lstrContent = "2"
			End If
			insPostDP041 = lclsProd_win.Add_Prod_win(nBranch, nProduct, dEffecdate, "DP041", lstrContent, nUsercode)
		End If
		
insPostDP041Upd_Err: 
		If Err.Number Then
			insPostDP041 = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsTar_au_bons may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsTar_au_bons = Nothing
		'UPGRADE_NOTE: Object lclsProd_win may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsProd_win = Nothing
	End Function
	
	'%GetCountTar_au_bon
	Public ReadOnly Property GetCountTar_au_bon(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date) As Integer
		Get
			Dim lrecGetCountTar_au_bon As eRemoteDB.Execute
			
			On Error GoTo GetCountTar_au_bon_Err
			
			lrecGetCountTar_au_bon = New eRemoteDB.Execute
			
			'+ Definición de parámetros para stored procedure 'insudb.GetCountTar_au_bon'
			'+ Información leída el 19/07/2001 11:38:55 AM
			
			With lrecGetCountTar_au_bon
				.StoredProcedure = "GetCountTar_au_bon"
				.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					GetCountTar_au_bon = .FieldToClass("nCount")
					.RCloseRec()
				End If
			End With
			
GetCountTar_au_bon_Err: 
			If Err.Number Then
				GetCountTar_au_bon = 0
			End If
			'UPGRADE_NOTE: Object lrecGetCountTar_au_bon may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lrecGetCountTar_au_bon = Nothing
			On Error GoTo 0
		End Get
	End Property
End Class






