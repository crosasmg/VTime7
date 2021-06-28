Option Strict Off
Option Explicit On
Public Class TableTarifSeq
	'%LoadTabs: Arma la secuencia para las Ordenes de servicio segun el ramo
	Public Function LoadTabsTableTarif(ByVal nAction As Integer, ByVal sUserSchema As String, ByVal nTableTarif As Integer, ByVal dEffecdate As Date) As Object
		Const SEQUEN As String = "DP8001  DP8002  "
		Dim lrecWindows As Object
		Dim lclsSequence As Object
		
		Dim lobjTables As Object
		Dim lintCountWindows As Integer
        Dim lstrCodisp As String = ""
        Dim lstrCodispl As String
        Dim lstrShort_desc As String = ""
        Dim lblnContent As Boolean
		Dim lblnContentDP8001 As Boolean
		Dim lblnRequired As Boolean
		Dim lstrHTMLCode As String
		Dim lstrWindows As String
		Dim mintPageImage As Short
		
		On Error GoTo LoadTabsTableTarif_Err
		
		lrecWindows = eRemoteDB.NetHelper.CreateClassInstance("eRemoteDB.Query")
		lclsSequence = eRemoteDB.NetHelper.CreateClassInstance("eFunctions.Sequence")
		
		lstrHTMLCode = String.Empty
		
		lstrHTMLCode = lclsSequence.makeTable
		lintCountWindows = 1
		lstrWindows = SEQUEN
		lstrCodispl = Mid(lstrWindows, lintCountWindows, 8)
		
		Do While Trim(lstrCodispl) <> String.Empty
			lblnContent = False
			lblnRequired = False
			lstrCodispl = Trim(lstrCodispl)
			If lstrCodispl = "DP8001" Then
				lblnRequired = True
			End If
			
			'+ Se asignan los valores a las variables de descripción
			If lrecWindows.OpenQuery("Windows", "sCodisp, sShort_des", "sCodispl='" & lstrCodispl & "'") Then
				lstrCodisp = lrecWindows.FieldToClass("sCodisp")
				lstrShort_desc = lrecWindows.FieldToClass("sShort_des")
				lrecWindows.CloseQuery()
			End If

			'+ Se verifica contenido de las ventanas
			Select Case lstrCodispl

				'+ DP8001: Columnas de una tabla lógica de tarifa
				Case "DP8001"
					lobjTables = eRemoteDB.NetHelper.CreateClassInstance("eTarif.Tarif_tab_cols")
					lblnContent = lobjTables.Find(nTableTarif)
					lblnContentDP8001 = lblnContent
					'UPGRADE_NOTE: Object lobjTables may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lobjTables = Nothing
					
					'+ DP8002: Valores de una tabla lógica de tarifa
				Case "DP8002"
					lobjTables = eRemoteDB.NetHelper.CreateClassInstance("eTarif.Tarif_val_cols")
					lblnContent = lobjTables.Find_Value(nTableTarif)
					'UPGRADE_NOTE: Object lobjTables may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lobjTables = Nothing
					
			End Select
			
			If Not lblnContent Then
				If lblnRequired Then
					mintPageImage = 2 'eRequired
				Else
					mintPageImage = 0 'eEmpty
				End If
			Else
				mintPageImage = 1 'eOK
			End If
			
			If (lstrCodispl = "DP8002" And lblnContentDP8001) Or lstrCodispl <> "DP8002" Then
				
				lstrHTMLCode = lstrHTMLCode & lclsSequence.makeRow(lstrCodisp, lstrCodispl, nAction, lstrShort_desc, mintPageImage)
			End If
			'+ Se mueve al siguiente registro encontrado
			lintCountWindows = lintCountWindows + 8
			lstrCodispl = Mid(lstrWindows, lintCountWindows, 8)
		Loop 
		
		lstrHTMLCode = lstrHTMLCode & lclsSequence.closeTable()
		
		LoadTabsTableTarif = lstrHTMLCode
		
LoadTabsTableTarif_Err: 
		If Err.Number Then
			LoadTabsTableTarif = "LoadTabsTableTarif: " & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecWindows may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecWindows = Nothing
		'UPGRADE_NOTE: Object lclsSequence may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsSequence = Nothing
		'UPGRADE_NOTE: Object lobjTables may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjTables = Nothing
	End Function
    '% insValDP8000_k: Realiza la validación de los campos de la ventana DP8000_k

	Public Function insValDP8000_k(ByVal sCodispl As String, ByVal dEffecdate As Date, ByVal nTableTarif As Integer) As String
		Dim lobjErrors As Object
		Dim lclsTarif_tab_col As Object
		
		lobjErrors = eRemoteDB.NetHelper.CreateClassInstance("eFunctions.Errors")
		lclsTarif_tab_col = eRemoteDB.NetHelper.CreateClassInstance("eTarif.Tarif_tab_col")
		
		On Error GoTo insValDP8000_k_Err
		
		'+Validación del código de la tabla lógica
		If nTableTarif = eRemoteDB.Constants.intNull Then
			Call lobjErrors.ErrorMessage(sCodispl, 55537,  , 2, "Código de la tabla lógica ")
		Else
			'+Validación de la Fecha de ejecución
			If dEffecdate = eRemoteDB.Constants.dtmNull Then
				If Not lclsTarif_tab_col.insExistsTarifValue(nTableTarif) Then
					Call lobjErrors.ErrorMessage(sCodispl, 55537,  , 2, "Fecha ")
				End If
			End If
		End If

		insValDP8000_k = lobjErrors.Confirm
		
insValDP8000_k_Err: 
		If Err.Number Then
			insValDP8000_k = insValDP8000_k & Err.Description
		End If
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
		On Error GoTo 0
	End Function
	
	'%GetMaxDeffecdate: devuelve la mayor fecha de efecto de los registros de una tabla logica de tarifas
	Public Function GetMaxDeffecdate(ByVal nId_table As Integer) As Date
        Dim dEffecdate As Object = New Object
        Dim lrecGetMaxDeffecdate As eRemoteDB.Execute
		On Error GoTo GetMaxDeffecdate_Err
		
		lrecGetMaxDeffecdate = New eRemoteDB.Execute
		
		With lrecGetMaxDeffecdate
			.StoredProcedure = "InsDP8002pkg.GETMAXDEFFECDATE"
			.Parameters.Add("nId_table", nId_table, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(False) Then
				GetMaxDeffecdate = .Parameters("dEffecdate").Value
			Else
				GetMaxDeffecdate = eRemoteDB.Constants.dtmNull
			End If
		End With
		
GetMaxDeffecdate_Err: 
		If Err.Number Then
			GetMaxDeffecdate = eRemoteDB.Constants.dtmNull
		End If
		'UPGRADE_NOTE: Object lrecGetMaxDeffecdate may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecGetMaxDeffecdate = Nothing
		On Error GoTo 0
	End Function
End Class






