Option Strict Off
Option Explicit On
Public Class Claus_co_gp
	'%-------------------------------------------------------%'
	'% $Workfile:: Claus_co_gp.cls                          $%'
	'% $Author:: Nvaplat41                                  $%'
	'% $Date:: 9/10/03 19.01                                $%'
	'% $Revision:: 20                                       $%'
	'%-------------------------------------------------------%'
	'- Propiedades según la tabla en el sistema al 07/12/2000.
	'- Los campos llave de la tabla corresponden a: sCertype, nBranch, nProduct, nPolicy, nGroup, nClause y dEffecdate.
	'   Column_name                   Type       Computed  Length  Prec  Scale Nullable    TrimTrailingBlanks     FixedLenNullInSource
	Public sCertype As String 'char          no        1                  no               no                       no
	Public nBranch As Integer 'smallint      no        2      5     0     no              (n/a)                    (n/a)
	Public nProduct As Integer 'smallint      no        2      5     0     no              (n/a)                    (n/a)
	Public nPolicy As Double 'int           no        4     10     0     no              (n/a)                    (n/a)
	Public nGroup As Integer 'smallint      no        2      5     0     no              (n/a)                    (n/a)
	Public nClause As Integer 'smallint      no        2      5     0     no              (n/a)                    (n/a)
	Public dEffecdate As Date 'datetime      no        8                  no              (n/a)                    (n/a)
	Public nNotenum As Integer 'int           no        4     10     0     yes             (n/a)                    (n/a)
	Public dNulldate As Date 'datetime      no        8                  yes             (n/a)                    (n/a)
	Public nUsercode As Integer 'smallint      no        2      5     0     yes             (n/a)                    (n/a)
	Public sType_clause As String
	Public sDoc_attach As String
	
	'-Variables auxiliares
	'-Indicador de clausula seleccionada por omision
	Public nSel As Integer
	Public sDescriptD As String
	Public sDefaultiD As String
	
	'- Variable de existencia de relación
	Public nLink As Integer
	
	'-Nota de producto y de grupo
	Public nNoteNumP As Integer
	Public nNotenumS As Integer
	Public nAction As Integer
	Public sModified As String
	Public sTyp_Clause As String
	Public nCountGroup As Integer
	Public bFindGroup As Boolean
	
	'-Tipo de datos con información de Tab_Clause y Claus_co_g
	Private Structure udtClaus_co_gp
		Dim nClauseP As Integer
		Dim nClauseS As Integer
		Dim nNoteNumP As Integer
		Dim nNotenumS As Integer
		Dim sDescriptD As String
		Dim sDefaultiD As String
		Dim sModified As String
		Dim sType_clause As String
		Dim sDoc_attach As String
	End Structure
	
	'- Arreglo para la carga de cláusulas por grupo
	Private marrClaus_co_gp() As udtClaus_co_gp
	
	'- Indica si el arreglo de cláusulas se cargo o no
	Private mblnCharge As Boolean
	
	'-Código de error puntual encontrado en carga inicial de forma
	Private mlngErrorClause As Integer
	
	'-Indicador que se encontró información en Claus_co_g
	Private mblnDataFound As Boolean
	
	'% UpdateG: Actualiza una cláusula en la tabla Claus_co_g
	'-------------------------------------------------------
	Public Function UpdateG() As Boolean
		'-------------------------------------------------------
		'- Se define la variable lrecinsClaus_co_g
		Dim lrecinsClaus_co_g As eRemoteDB.Execute
		
		On Error GoTo UpdateG_Err
		
		lrecinsClaus_co_g = New eRemoteDB.Execute
		
		With lrecinsClaus_co_g
			.StoredProcedure = "insClaus_co_g"
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nGroup", nGroup, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nClause", nClause, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNotenum", nNotenum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sType_clause", sType_clause, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDoc_attach", sDoc_attach, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 45, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			UpdateG = .Run(False)
		End With
UpdateG_Err: 
		If Err.Number Then
			UpdateG = False
		End If
		'UPGRADE_NOTE: Object lrecinsClaus_co_g may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsClaus_co_g = Nothing
	End Function
	
	'% LoadClauseByProductG: Devuelve las cláusulas definidas por grupo
	'%  a partir del diseñador.
	Public Function LoadClauseByProductG(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nGroup As Integer, ByVal dEffecdate As Date, ByVal bQuery As Boolean) As Boolean
		'- Se define la variable lrecreaTab_clause
		Dim lrecreaTab_clause As eRemoteDB.Execute
		Dim lintIndex As Integer
		Dim llngTop As Integer
		
		On Error GoTo LoadClauseByProductG_Err
		
		lrecreaTab_clause = New eRemoteDB.Execute
		
		With lrecreaTab_clause
			.StoredProcedure = "reaTab_clause_Claus_co_g_a"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nGroup", nGroup, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nQuery", IIf(bQuery, 1, 2), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				LoadClauseByProductG = Not .EOF
				mblnCharge = True
				lintIndex = -1
				Do While Not .EOF
					lintIndex = lintIndex + 1
					If lintIndex >= llngTop Then
						llngTop = llngTop + 50
						ReDim Preserve marrClaus_co_gp(llngTop)
					End If
					marrClaus_co_gp(lintIndex).nClauseP = .FieldToClass("nClauseP")
					marrClaus_co_gp(lintIndex).nClauseS = .FieldToClass("nClauseS")
					marrClaus_co_gp(lintIndex).nNoteNumP = .FieldToClass("nNoteNumP")
					marrClaus_co_gp(lintIndex).nNotenumS = .FieldToClass("nNoteNumS")
					marrClaus_co_gp(lintIndex).sDescriptD = .FieldToClass("sDescriptD")
					marrClaus_co_gp(lintIndex).sDefaultiD = .FieldToClass("sDefaultiD")
					marrClaus_co_gp(lintIndex).sType_clause = .FieldToClass("sType_clause")
					marrClaus_co_gp(lintIndex).sDoc_attach = .FieldToClass("sDoc_attach")
					marrClaus_co_gp(lintIndex).sModified = "2"
					
					If marrClaus_co_gp(lintIndex).nClauseS <> eRemoteDB.Constants.intNull Then
						mblnDataFound = True
					End If
					.RNext()
				Loop 
				
				.RCloseRec()
				ReDim Preserve marrClaus_co_gp(lintIndex)
			Else
				LoadClauseByProductG = False
				mblnCharge = False
			End If
		End With
		
LoadClauseByProductG_Err: 
		If Err.Number Then
			LoadClauseByProductG = False
			mblnCharge = False
		End If
		'UPGRADE_NOTE: Object lrecreaTab_clause may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaTab_clause = Nothing
		On Error GoTo 0
	End Function
	
	'%insPostCA022A: Se realiza la actualización de los datos en la ventana CA022A
	Public Function insPostCA022A(ByVal sCodispl As String, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal dEffecdate As Date, ByVal nUsercode As Integer, ByVal sCodClause As String, ByVal sSelClause As String, ByVal sNotenum As String, ByVal sNotenum_Prod As String, Optional ByVal nGroup As Integer = 0, Optional ByVal sType_clause As String = "", Optional ByVal sDoc_attach As String = "") As Boolean
		'-Objetos asociados a tablas a actualizar
		Dim lclsPolicy As ePolicy.Policy
		Dim lclsPolicyWin As ePolicy.Policy_Win
		Dim lclsClaus_co_gp As ePolicy.Claus_co_gp
		Dim lclsNotes As eGeneralForm.Notes
		
		'-Ubicación del matriz de datos
		Dim lintPos As Integer
		'-Variable que indica si se marca con contenido o no
		Dim blnConten As Boolean
		
		'-Códigos de clausula seleccionada, nota y clausula
		Dim lstrClause As Object
		Dim nClause As Integer
		Dim nNotenum As Integer
		Dim nNotenum_Prod As Integer
		Dim nSelClause As Integer
		
		Dim larrSel() As String
		Dim larrClause() As String
		Dim larrNote() As String
		Dim larrNote_Prod() As String
		
		On Error GoTo insPostCA022_Err
		
		lclsPolicy = New ePolicy.Policy
		
		blnConten = True
		'+ Se obtienen los datos de la póliza.
		If lclsPolicy.Find(sCertype, nBranch, nProduct, nPolicy) Then
			If (lclsPolicy.sTyp_Clause = "3" And nGroup = eRemoteDB.Constants.intNull) Or (lclsPolicy.sTyp_Clause <> "2" And lclsPolicy.sTyp_Clause <> "3") Then
				
				insPostCA022A = True
				blnConten = False
			Else
				'+ Se separan listas por comas
				'+ Con esto tenemos tres matrices del mismo largo
				larrSel = Microsoft.VisualBasic.Split(sSelClause, ",")
				larrClause = Microsoft.VisualBasic.Split(sCodClause, ",")
				larrNote = Microsoft.VisualBasic.Split(sNotenum, ",")
				larrNote_Prod = Microsoft.VisualBasic.Split(sNotenum_Prod, ",")
				
				lclsClaus_co_gp = New ePolicy.Claus_co_gp
				
				lintPos = 0
				For	Each lstrClause In larrClause
					nClause = Val(lstrClause)
					nNotenum = Val(larrNote(lintPos))
					nNotenum_Prod = Val(larrNote_Prod(lintPos))
					nSelClause = Val(larrSel(lintPos))
					
					If nNotenum = nNotenum_Prod And nSelClause = 1 Then
						lclsNotes = New eGeneralForm.Notes
						nNotenum = lclsNotes.CopyNotes(nNotenum_Prod, 6, nUsercode)
					End If
					
					With lclsClaus_co_gp
						'+Si esta seleccionado la accion es agregar; sino es eliminar
						.nAction = nSelClause
						.sCertype = sCertype
						.nBranch = nBranch
						.nProduct = nProduct
						.nPolicy = nPolicy
						'On Error Resume Next
						'+ Se controla error porque si es por póliza no existe campo por grupo
						.nGroup = nGroup
						'On Error GoTo insPostCA022_Err
						.nClause = nClause
						.dEffecdate = dEffecdate
						.nNotenum = nNotenum
						.nUsercode = nUsercode
						.sType_clause = sType_clause
						.sDoc_attach = sDoc_attach
						
						'+ Se trata según el caso: 2)Por póliza y 3)Por grupo.
						Select Case lclsPolicy.sTyp_Clause
							Case "2"
								insPostCA022A = .UpdateP
							Case "3"
								insPostCA022A = .UpdateG
						End Select
					End With
					lintPos = lintPos + 1
				Next lstrClause
			End If
			
			'+ Si la actualización es efectiva
			'+ se coloca el frame de la ventana con contenido.
			If insPostCA022A And blnConten Then
				lclsPolicyWin = New ePolicy.Policy_Win
				Call lclsPolicyWin.Add_PolicyWin(sCertype, nBranch, nProduct, nPolicy, 0, dEffecdate, nUsercode, sCodispl, "2")
				'UPGRADE_NOTE: Object lclsPolicyWin may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
				lclsPolicyWin = Nothing
			End If
		End If
		
insPostCA022_Err: 
		If Err.Number Then
			insPostCA022A = False
		End If
		'UPGRADE_NOTE: Object lclsClaus_co_gp may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsClaus_co_gp = Nothing
		'UPGRADE_NOTE: Object lclsPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsPolicy = Nothing
		'UPGRADE_NOTE: Object lclsPolicyWin may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsPolicyWin = Nothing
		'UPGRADE_NOTE: Object lclsNotes may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsNotes = Nothing
		On Error GoTo 0
	End Function
	
	'% insValCA022A: Realiza la validación de los campos a actualizar en la ventana CA022A
	Public Function insValCA022A(ByVal sCodispl As String, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal dEffecdate As Date, ByVal sCodClause As String, ByVal sSelClause As String, Optional ByVal nGroup As Integer = 0) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lclsPolicy As ePolicy.Policy
		Dim lclsClaus_co_gp As ePolicy.Claus_co_gp
		Dim lclsReqExc As ePolicy.Tab_reqexc
		
		On Error GoTo insValCA022A_Err
		
		lclsErrors = New eFunctions.Errors
		lclsPolicy = New ePolicy.Policy
		lclsReqExc = New ePolicy.Tab_reqexc
		
		insValCA022A = ""
		
		'+ Debe existir por lo menos una cláusula seleccionada cuando hay datos
		If InStr(sSelClause, "1") = 0 And Len(sCodClause) > 0 Then
			Call lclsErrors.ErrorMessage(sCodispl, 3877)
		End If
		
		'+ Se debe llamar a la rutina que valida los requisitos y
		'+ las exclusiones particulares de las cláusulas.
		Call lclsReqExc.InsValTab_Reqexc(sCodispl, sCertype, nBranch, nProduct, nPolicy, 0, dEffecdate, eProduct.Tab_reqexc.ReqexclType.cstrClause, sCodClause, sSelClause, lclsErrors)
		
		insValCA022A = lclsErrors.Confirm
		
insValCA022A_Err: 
		If Err.Number Then
			insValCA022A = "insValCA022A:" & Err.Description
		End If
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		'UPGRADE_NOTE: Object lclsPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsPolicy = Nothing
		'UPGRADE_NOTE: Object lclsClaus_co_gp may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsClaus_co_gp = Nothing
		'UPGRADE_NOTE: Object lclsReqExc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsReqExc = Nothing
		On Error GoTo 0
	End Function
	
	'% Count: Devuelve el número de cláusulas que se encuentran en el arreglo
	Public ReadOnly Property Count() As Integer
		Get
			If mblnCharge Then
				Count = UBound(marrClaus_co_gp)
			Else
				Count = -1
			End If
		End Get
	End Property
	
	'% Property Get sClausePreError : Muestra según sea el tipo de cláusulas (CA022A) los mensajes de
	'%                   advertencia correspondientes.
	
	'% Property Let sClausePreError : Se establece el número del error a mostrar
	Public Property sClausePreError() As String
		Get
			Dim lclsErrors As eFunctions.Errors
			lclsErrors = New eFunctions.Errors
			
			If mlngErrorClause <= 0 Then
				sClausePreError = ""
			Else
				sClausePreError = lclsErrors.ErrorMessage("CA022A", mlngErrorClause,  ,  ,  , True)
			End If
			
			'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lclsErrors = Nothing
		End Get
		Set(ByVal Value As String)
			mlngErrorClause = CInt(Value)
		End Set
	End Property
	
	'% Item: Carga en las variables de la clase la información de una cláusula
	Public Function Item(ByVal lintIndex As Integer) As Boolean
		
		If mblnCharge Then
			If lintIndex <= UBound(marrClaus_co_gp) Then
				With marrClaus_co_gp(lintIndex)
					sDefaultiD = .sDefaultiD
					nClause = .nClauseP
					'+ Si la clausula fue seleccionada previamente (existe en Claus_co_g) o
					'+ tiene marcada seleccion por defecto y no hay datos, se debe mostrar marcada
					If .nClauseS <> eRemoteDB.Constants.intNull Or (sDefaultiD = "1" And Not mblnDataFound) Then
						nSel = 1
					Else
						nSel = 2
					End If
					sDescriptD = .sDescriptD
					nNotenum = .nNoteNumP
					nNoteNumP = .nNoteNumP
					nNotenumS = .nNotenumS
					sModified = .sModified
					sType_clause = .sType_clause
					sDoc_attach = .sDoc_attach
				End With
				
				Item = True
			Else
				Item = False
			End If
		End If
	End Function
	
	'% DeleteG: Elimina los registros correspondientes a la tabla Claus_co_g
	Public Function DeleteG(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal dEffecdate As Date, ByVal nUsercode As Integer) As Boolean
		'-Variable de acceso a base de datos
		Dim lrecdelClaus_co_g As eRemoteDB.Execute
		
		On Error GoTo DeleteG_err
		
		lrecdelClaus_co_g = New eRemoteDB.Execute
		
		With lrecdelClaus_co_g
			.StoredProcedure = "delClaus_co_g"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			DeleteG = .Run(False)
		End With
		'UPGRADE_NOTE: Object lrecdelClaus_co_g may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecdelClaus_co_g = Nothing
		
DeleteG_err: 
		If Err.Number Then
			DeleteG = False
		End If
		On Error GoTo 0
	End Function
	
	'% Valida que registro en la tabla groups no tenga relaciones con otras tablas
	'% antes de realizar el borrado
	Public Function FindGroupLinks(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nPolicy As Double, ByVal nProduct As Integer, ByVal nGroup As Integer) As Boolean
		Dim lrecinsvalLinksGroups As eRemoteDB.Execute
		
		lrecinsvalLinksGroups = New eRemoteDB.Execute
		
		On Error GoTo FindGroupsLinks_Err
		
		With lrecinsvalLinksGroups
			.StoredProcedure = "insvalLinksGroups"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nGroup", nGroup, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(True) Then
				FindGroupLinks = True
				Me.nLink = .FieldToClass("nLink")
			Else
				FindGroupLinks = False
			End If
		End With
		'UPGRADE_NOTE: Object lrecinsvalLinksGroups may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsvalLinksGroups = Nothing
		
FindGroupsLinks_Err: 
		If Err.Number Then
			FindGroupLinks = False
		End If
		On Error GoTo 0
	End Function
	
	'% insPreCA022A : Carga los datos iniciales de la transacción
	Public Function insPreCA022A(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nGroup As Integer, ByVal bQuery As Boolean) As Object
		'- Objeto para buscar datos de poliza
		Dim lclsPolicy As ePolicy.Policy
		Dim lclsGroups As ePolicy.Groups
		'- Objeto a crear con datos de clausulas. Puede asignarse a Claus_co_g, Claus_co_p o Tab_Clause
		Dim lclsClass As Object
		
		On Error GoTo insPreCA022A_Err
		
		'UPGRADE_NOTE: Object lclsClass may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsClass = Nothing
		
		'+ Se obtienen los datos de la póliza
		lclsPolicy = New ePolicy.Policy
		If lclsPolicy.Find(sCertype, nBranch, nProduct, nPolicy) Then
			
			Me.sTyp_Clause = lclsPolicy.sTyp_Clause
			
			'+Es poliza matriz de colectivo/multilocal
			If lclsPolicy.sPolitype <> "1" And lclsPolicy.nCertif = 0 Then
				
				Select Case lclsPolicy.sTyp_Clause
					'+ Cuando la póliza no trabaja con cláusulas.
					Case "1"
						sClausePreError = CStr(3884)
						
						'+ Póliza posee cláusulas definidas por póliza.
					Case "2"
						lclsClass = New ePolicy.Claus_co_gp
						Call lclsClass.LoadClauseByProductP(lclsPolicy.sCertype, lclsPolicy.nBranch, lclsPolicy.nProduct, lclsPolicy.nPolicy, dEffecdate, bQuery)
						'+ Póliza posee cláusulas definidas por grupo.
					Case "3"
						lclsGroups = New ePolicy.Groups
						
						Me.nCountGroup = lclsGroups.getCountGroups(sCertype, nBranch, nProduct, nPolicy)
						'+ Si existen grupos asociados
						If Me.nCountGroup > 0 Then
							'+ Si no se indicó un grupo asegurado.
							If nGroup <= 0 Then
								'+ Se obtiene el primero que consiga (información por omisión)
								If lclsGroups.valGroupExist(sCertype, nBranch, nProduct, nPolicy, dEffecdate) Then
									Me.nGroup = lclsGroups.nGroup
									nGroup = lclsGroups.nGroup
								End If
								lclsClass = New ePolicy.Claus_co_gp
							Else
								Me.nGroup = nGroup
							End If
							Me.bFindGroup = True
							'+ Se cargan los valores al grid.
							lclsClass = New ePolicy.Claus_co_gp
							Call lclsClass.LoadClauseByProductG(sCertype, nBranch, nProduct, nPolicy, nGroup, dEffecdate, bQuery)
						Else
							'+ 3887: No existen Grupos asociados a la póliza
							sClausePreError = CStr(3887)
						End If
						
						'+ Póliza posee cláusulas definidas por certificado.
					Case "4"
						sClausePreError = CStr(3886)
						
					Case Else
						'+No se ha ingresado información en transaccion de información general colectivo
						sClausePreError = CStr(3885)
				End Select
				
			Else
				'+ Si no es matriz de colectivo/mutilocalidad la ventana no pertenece
				'+ a esta secuencia.
				sClausePreError = CStr(1402)
			End If
		End If
		
		insPreCA022A = lclsClass
		
insPreCA022A_Err: 
		If Err.Number Then
			'UPGRADE_NOTE: Object insPreCA022A may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			insPreCA022A = Nothing
		End If
		'UPGRADE_NOTE: Object lclsPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsPolicy = Nothing
		'UPGRADE_NOTE: Object lclsGroups may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsGroups = Nothing
		'UPGRADE_NOTE: Object lclsClass may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsClass = Nothing
		On Error GoTo 0
	End Function
	
	'% Class_Initialize: Asigna valores iniciales a varibales de modulo
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		mlngErrorClause = eRemoteDB.Constants.intNull
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'% UpdateP: Actualiza una cláusula en la tabla Claus_co_p
	'-------------------------------------------------------
	Public Function UpdateP() As Boolean
		'-------------------------------------------------------
		'- Se define la variable lrecinsClaus_co_p
		Dim lrecinsClaus_co_p As eRemoteDB.Execute
		
		On Error GoTo UpdateP_Err
		
		lrecinsClaus_co_p = New eRemoteDB.Execute
		
		With lrecinsClaus_co_p
			.StoredProcedure = "insClaus_co_p"
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nClause", nClause, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNotenum", nNotenum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sType_clause", sType_clause, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDoc_attach", sDoc_attach, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 45, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			UpdateP = .Run(False)
		End With
		
UpdateP_Err: 
		If Err.Number Then
			UpdateP = False
		End If
		'UPGRADE_NOTE: Object lrecinsClaus_co_p may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsClaus_co_p = Nothing
		On Error GoTo 0
	End Function
	
	'% LoadClauseByProductP: Devuelve las cláusulas definidas por certificado
	'% a partir del diseñador.
	Public Function LoadClauseByProductP(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal dEffecdate As Date, ByVal bQuery As Boolean) As Boolean
		'- Se define la variable lrecreaTab_clause
		Dim lrecreaTab_clause As eRemoteDB.Execute
		Dim lintIndex As Integer
		Dim llngTop As Integer
		
		On Error GoTo LoadClauseByProductP_Err
		
		lrecreaTab_clause = New eRemoteDB.Execute
		
		With lrecreaTab_clause
			.StoredProcedure = "reaTab_clause_Claus_co_p_a"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nQuery", IIf(bQuery, 1, 2), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				LoadClauseByProductP = Not .EOF
				mblnCharge = True
				lintIndex = -1
				Do While Not .EOF
					lintIndex = lintIndex + 1
					If lintIndex >= llngTop Then
						llngTop = llngTop + 50
						ReDim Preserve marrClaus_co_gp(llngTop)
					End If
					marrClaus_co_gp(lintIndex).nClauseP = .FieldToClass("nClauseP")
					marrClaus_co_gp(lintIndex).nClauseS = .FieldToClass("nClauseS")
					marrClaus_co_gp(lintIndex).nNoteNumP = .FieldToClass("nNoteNumP")
					marrClaus_co_gp(lintIndex).nNotenumS = .FieldToClass("nNoteNumS")
					marrClaus_co_gp(lintIndex).sDescriptD = .FieldToClass("sDescriptD")
					marrClaus_co_gp(lintIndex).sDefaultiD = .FieldToClass("sDefaultiD")
					marrClaus_co_gp(lintIndex).sType_clause = .FieldToClass("sType_clause")
					marrClaus_co_gp(lintIndex).sDoc_attach = .FieldToClass("sDoc_attach")
					marrClaus_co_gp(lintIndex).sModified = "2"
					
					If marrClaus_co_gp(lintIndex).nClauseS <> eRemoteDB.Constants.intNull Then
						mblnDataFound = True
					End If
					.RNext()
				Loop 
				.RCloseRec()
				ReDim Preserve marrClaus_co_gp(lintIndex)
			Else
				LoadClauseByProductP = False
				mblnCharge = False
			End If
		End With
		
LoadClauseByProductP_Err: 
		If Err.Number Then
			LoadClauseByProductP = False
			mblnCharge = False
		End If
		'UPGRADE_NOTE: Object lrecreaTab_clause may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaTab_clause = Nothing
		On Error GoTo 0
	End Function
	
	'% DeleteP: Elimina los registros correspondientes a la tabla Claus_co_p
	Public Function DeleteP(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal dEffecdate As Date, ByVal nUsercode As Integer) As Boolean
		'-Variable de accesso a base de datos
		Dim lrecdelClaus_co_p As eRemoteDB.Execute
		
		On Error GoTo DeleteP_err
		
		lrecdelClaus_co_p = New eRemoteDB.Execute
		
		With lrecdelClaus_co_p
			.StoredProcedure = "delClaus_co_p"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			DeleteP = .Run(False)
		End With
		
DeleteP_err: 
		If Err.Number Then
			DeleteP = False
		End If
		'UPGRADE_NOTE: Object lrecdelClaus_co_p may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecdelClaus_co_p = Nothing
		On Error GoTo 0
	End Function
	
	'% ValExistClaus_co_gp: Valida que existan clausulas para la poliza
	Public Function ValExistClaus_co_gp(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal dEffecdate As Date, ByVal sTyp_Discxp As String) As Boolean
		Dim lrecClaus_co_gp As eRemoteDB.Execute
		
		On Error GoTo ValExistClaus_co_gp_Err
		
		lrecClaus_co_gp = New eRemoteDB.Execute
		
		ValExistClaus_co_gp = False
		
		With lrecClaus_co_gp
			.StoredProcedure = "reaExistClaus_co_gp"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sTyp_Discxp", sTyp_Discxp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExist", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				ValExistClaus_co_gp = .Parameters("nExist").Value > 0
			End If
		End With
		
ValExistClaus_co_gp_Err: 
		If Err.Number Then
			ValExistClaus_co_gp = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecClaus_co_gp may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecClaus_co_gp = Nothing
	End Function
End Class






