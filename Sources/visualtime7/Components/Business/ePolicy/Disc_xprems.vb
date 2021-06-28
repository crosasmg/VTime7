Option Strict Off
Option Explicit On
Public Class Disc_xprems
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Disc_xprems.cls                          $%'
	'% $Author:: Nvaplat15                                  $%'
	'% $Date:: 8/10/04 13.09                                $%'
	'% $Revision:: 44                                       $%'
	'%-------------------------------------------------------%'
	
	'-Variables que guarda las monedas asociadas a la póliza
	Public sCurren_pol As String
	Public bGroups As Boolean
	Public nGroup As Integer
	Public nCountGroup As Integer
	Public nError As Integer
	Public sTyp_Discxp As String
	'- Variable utilizada en ca016
	Public nMasive As Short
	
	'-Variable local que guarda la collection
	Private mCol As Collection
	
	'- Variables que guardan la llave de busqueda
	Private mstrCertype As String
	Private mlngBranch As Integer
	Private mlngProduct As Integer
	Private mlngPolicy As Double
	Private mlngCertif As Double
	Private mdtmEffecdate As Date
	
	
	'%Add: Agrega un nuevo registro a la colección
	Public Function Add(ByRef objClass As Disc_xprem) As Disc_xprem
		If objClass Is Nothing Then
			objClass = New Disc_xprem
		End If

        With objClass
            mCol.Add(objClass, .sCertype & .nBranch & .nProduct & .nPolicy & .nCertif & .nDisc_code & .dEffecdate.ToString("yyyyMMdd"))
        End With
        Return objClass
    End Function
	
	'%InsPreCA016: Ejecuta los procesos para obtener la información de la CA016, según funcional
	Public Function insPreCA016(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal deffecdate As Date, ByVal nGroup As Integer, ByVal nTransaction As Short) As Boolean
		Dim lclsPolicy As ePolicy.Policy
		Dim lclsGroups As ePolicy.Groups
		Dim lclsCertificat As ePolicy.Certificat
		Dim lclsPolicy_Win As ePolicy.Policy_Win
		
		On Error GoTo InsPreCA016_Err
		
		lclsPolicy = New ePolicy.Policy
		lclsPolicy_Win = New ePolicy.Policy_Win
		
		insPreCA016 = True
		
		With lclsPolicy
			If .Find(sCertype, nBranch, nProduct, nPolicy) Then
				Me.sTyp_Discxp = .sTyp_Discxp
				Call lclsPolicy_Win.Find_Codispl(sCertype, nBranch, nProduct, nPolicy, nCertif, deffecdate, "CA006")
				If .sColinvot = String.Empty Or lclsPolicy_Win.sContent = "1" Then
					If .sPolitype <> "1" Then
						'+ Debe indicarse el tratamiento de los recargos/descuentos para la póliza
						Me.nError = 3885
						insPreCA016 = False
					End If
				Else
					If .sPolitype <> "1" And nCertif = 0 Then
						'+ Si las especificaciones de los recargos/descuentos/impuestos están por certificado
						If .sTyp_Discxp = "4" Or .sTyp_Discxp = "1" Then
							If .sTyp_Discxp = "4" Then
								Me.nError = 3896
							End If
							insPreCA016 = False
						Else
							'+ Si las especificaciones de los recargos/descuentos/impuestos están por grupo
							If .sTyp_Discxp = "3" Then
								lclsGroups = New ePolicy.Groups
								Me.nCountGroup = lclsGroups.getCountGroups(sCertype, nBranch, nProduct, nPolicy)
								'+ Si existen grupos asociados
								If Me.nCountGroup > 0 Then
									'+ Si no se indicó un grupo colectivo.
									If nGroup <= 0 Then
										'+ Se obtiene el primero que consiga (información por omisión)
										If lclsGroups.valGroupExist(sCertype, nBranch, nProduct, nPolicy, deffecdate) Then
											Me.nGroup = lclsGroups.nGroup
											nGroup = lclsGroups.nGroup
										End If
									Else
										Me.nGroup = nGroup
									End If
									Me.bGroups = True
								Else
									'+ Si no existen
									insPreCA016 = False
									'+ 3887: No existen grupos asociados a la póliza
									Me.nError = 3887
								End If
								'UPGRADE_NOTE: Object lclsGroups may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
								lclsGroups = Nothing
							End If
						End If
					Else
						'+ Si las especificaciones de los recargos/descuentos/impuestos están por grupo
						If .sTyp_Discxp = "3" Then
							'+ Si se trata de un certificado se busca el asociado a datos particulares
							If nGroup <= 0 Then
								'+ Se obtiene el grupo de datos particulares
								lclsCertificat = New ePolicy.Certificat
								nGroup = lclsCertificat.getParticularDataGroup(sCertype, nBranch, nProduct, nPolicy, nCertif, deffecdate)
								'+ Si no existe grupo asociado se devuelve falso
								If nGroup < 0 Then
									insPreCA016 = False
									'+ 3889: Debe ingresar información en la ventana de datos particulares.
									Me.nError = 3889
								End If
								'UPGRADE_NOTE: Object lclsCertificat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
								lclsCertificat = Nothing
							End If
							Me.nGroup = nGroup
						End If
					End If
				End If
			End If
		End With
		
		'+ Se obtiene la información asociada al diseñador o la previamente registrada.
		If insPreCA016 Then
			insPreCA016 = FindCA016(sCertype, nBranch, nProduct, nPolicy, nCertif, nGroup, deffecdate, lclsPolicy.sPolitype, lclsPolicy.sTyp_Discxp,  , nTransaction)
		End If
		
InsPreCA016_Err: 
		If Err.Number Then
			insPreCA016 = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsPolicy = Nothing
		'UPGRADE_NOTE: Object lclsGroups may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsGroups = Nothing
		'UPGRADE_NOTE: Object lclsCertificat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsCertificat = Nothing
		'UPGRADE_NOTE: Object lclsPolicy_Win may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsPolicy_Win = Nothing
	End Function
	
	'%FindCA016: Realiza la busqueda de los recargos/descuentos/impuestos de una póliza/certif.
	Public Function FindCA016(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nGroup As Integer, ByVal deffecdate As Date, ByVal sPolitype As String, ByVal sTyp_Discxp As String, Optional ByVal bFind As Boolean = False, Optional ByVal nTransaction As Short = 0) As Boolean
		Dim lrecInsReaCA016 As eRemoteDB.Execute
		Dim lclsDisc_xprem As Disc_xprem
		Dim lstrRoutine As String
		
		On Error GoTo FindCA016_Err
		If mstrCertype <> sCertype Or mlngBranch <> nBranch Or mlngProduct <> nProduct Or mlngPolicy <> nPolicy Or mlngCertif <> nCertif Or mdtmEffecdate <> deffecdate Or bFind Then
			
			'+ Definición de Stored Procedure InsReaCA016 al 09-07-2002
			lrecInsReaCA016 = New eRemoteDB.Execute
			With lrecInsReaCA016
				.StoredProcedure = "InsReaCA016"
				.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nGroup", IIf(nGroup = eRemoteDB.Constants.intNull, 0, nGroup), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("dEffecdate", deffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("sPoliType", sPolitype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("sTyp_Discxp", sTyp_Discxp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nTransaction", nTransaction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nMasive", IIf(nMasive = 3, nMasive, 2), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nCommit", 1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					Do While Not .EOF
						lclsDisc_xprem = New Disc_xprem
						lclsDisc_xprem.sCertype = sCertype
						lclsDisc_xprem.nBranch = nBranch
						lclsDisc_xprem.nProduct = nProduct
						lclsDisc_xprem.nPolicy = nPolicy
						lclsDisc_xprem.nCertif = nCertif
						lclsDisc_xprem.deffecdate = deffecdate
						lclsDisc_xprem.nDisc_code = .FieldToClass("nDisexprc")
						lclsDisc_xprem.sChanallo = .FieldToClass("sChanallo")
						lclsDisc_xprem.sDefaulti = .FieldToClass("sDefaulti")
						lclsDisc_xprem.sDescript = .FieldToClass("sDescript")
						lclsDisc_xprem.sRequire = .FieldToClass("sRequire")
						lclsDisc_xprem.nDisexaddper = .FieldToClass("nDisexaddper")
						lclsDisc_xprem.nDisexsubper = .FieldToClass("nDisexsubper")
						lclsDisc_xprem.sEdperapl = .FieldToClass("sEdperapl")
						lclsDisc_xprem.nAmount = .FieldToClass("nAmount")
						lclsDisc_xprem.nCurrency = .FieldToClass("nCurrency")
						lclsDisc_xprem.nPercent = .FieldToClass("nPercent")
						lclsDisc_xprem.nExist = .FieldToClass("nExist")
						lclsDisc_xprem.sDisexpri = .FieldToClass("sDisexpri")
						lclsDisc_xprem.sDisexpriDesc = .FieldToClass("sDisexpriDesc")
						lclsDisc_xprem.nOriPercent = .FieldToClass("nOriPercent")
						lclsDisc_xprem.nCurr_dexpr = .FieldToClass("nCurr_dexpr")
						lclsDisc_xprem.nDisexpra = .FieldToClass("nDisexpra")
						lclsDisc_xprem.nOriAmount = .FieldToClass("nOriAmount")
						lclsDisc_xprem.nCause = .FieldToClass("nCause")
						lclsDisc_xprem.sCauseDesc = .FieldToClass("sCauseDesc")
						lclsDisc_xprem.sAgree = .FieldToClass("sAgree")
						
						If lclsDisc_xprem.nExist = 2 Then
							lstrRoutine = .FieldToClass("sRoutine")
							If lstrRoutine <> String.Empty Then
								If .FieldToClass("sDefpol") = "1" Then
									lclsDisc_xprem.nPercent = eRemoteDB.Constants.intNull
									lclsDisc_xprem.nAmount = eRemoteDB.Constants.intNull
									lclsDisc_xprem.nOriAmount = lclsDisc_xprem.nAmount
									lclsDisc_xprem.nOriPercent = lclsDisc_xprem.nPercent
								End If
							Else
								If lclsDisc_xprem.nOriAmount <> eRemoteDB.Constants.intNull Then
									lclsDisc_xprem.nAmount = lclsDisc_xprem.nOriAmount
								End If
								If lclsDisc_xprem.nOriPercent <> eRemoteDB.Constants.intNull And lclsDisc_xprem.nPercent = eRemoteDB.Constants.intNull Then
									lclsDisc_xprem.nPercent = lclsDisc_xprem.nOriPercent
								End If
							End If
						End If
						
						Call Add(lclsDisc_xprem)
						'UPGRADE_NOTE: Object lclsDisc_xprem may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
						lclsDisc_xprem = Nothing
						.RNext()
					Loop 
					FindCA016 = True
					mstrCertype = sCertype
					mlngBranch = nBranch
					mlngProduct = nProduct
					mlngPolicy = nPolicy
					mlngCertif = nCertif
					mdtmEffecdate = deffecdate
					.RCloseRec()
				End If
			End With
		Else
			FindCA016 = True
		End If
		
FindCA016_Err: 
		If Err.Number Then
			FindCA016 = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecInsReaCA016 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecInsReaCA016 = Nothing
	End Function
	
	'%Item: Obtiene un elemento de la colección
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Disc_xprem
		Get
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	'%Count: Obtiene la cantidad de elementos de la colección
	Public ReadOnly Property Count() As Integer
		Get
			Count = mCol.Count()
		End Get
	End Property
	
	'%NewEnum: Permite hacer el recorrido de la colección a través del For each
	'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
	'Public ReadOnly Property NewEnum() As stdole.IUnknown
		'Get
			'NewEnum = mCol._NewEnum
		'End Get
	'End Property
	
	Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
		'UPGRADE_TODO: Uncomment and change the following line to return the collection enumerator. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="95F9AAD0-1319-4921-95F0-B9D3C4FF7F1C"'
		GetEnumerator = mCol.GetEnumerator
	End Function
	
	'%Remove: Elimina un elemento de la colección
	Public Sub Remove(ByRef vntIndexKey As Object)
		mCol.Remove(vntIndexKey)
	End Sub
	
	'%Class_Initialize: Controla la creación de la colección
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		mstrCertype = String.Empty
		mlngBranch = eRemoteDB.Constants.intNull
		mlngProduct = eRemoteDB.Constants.intNull
		mlngPolicy = eRemoteDB.Constants.intNull
		mlngCertif = eRemoteDB.Constants.intNull
		mdtmEffecdate = eRemoteDB.Constants.dtmNull
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'%Class_Terminate: Controla la destrucción de la colección
	'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Terminate_Renamed()
		'UPGRADE_NOTE: Object mCol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mCol = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
End Class






