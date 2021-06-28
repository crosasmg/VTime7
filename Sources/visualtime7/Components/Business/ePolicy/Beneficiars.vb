Option Strict Off
Option Explicit On
Public Class Beneficiars
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Beneficiars.cls                          $%'
	'% $Author:: Nvaplat41                                  $%'
	'% $Date:: 9/10/03 19.01                                $%'
	'% $Revision:: 13                                       $%'
	'%-------------------------------------------------------%'
	
	'-Local variable to hold collection
	Private mCol As Collection
	
	'- Variable para almacenar el total de participación de los beneficiarios en la póliza
	Public nTotalParticip As Double
    Public nDuplicate As Double
    Public nTotalParticipCont As Double
    Public nTotalParticipDesign As Double
	
	'**%Add: Add a new instance of the benefit class to the collection
	'%Add: Añade una nueva instancia de la clase Beneficiar a la colección
	Public Function Add(ByVal lclsBeneficiar As Beneficiar) As Beneficiar
		With lclsBeneficiar
			mCol.Add(lclsBeneficiar, .sCertype & .nBranch & .nProduct & .nPolicy & .nCertif & .sClient & .dEffecdate.ToString("yyyyMMdd") & .nModulec & .nCover)
		End With
		
		Add = lclsBeneficiar
		'UPGRADE_NOTE: Object lclsBeneficiar may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsBeneficiar = Nothing
	End Function
	
	'**%Find: This method fills the collection with records from the table "Beneficiar" returning TRUE or FALSE
	'**%depending on the existence of the records
	'%Find: Este metodo carga la coleccion de elementos de la tabla "Beneficiar" devolviendo Verdadero o
	'%falso, dependiendo de la existencia de los registros.
	Public Function Find(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, Optional ByVal lblnFind As Boolean = False) As Boolean
		Dim lrecReaBeneficiar As eRemoteDB.Execute
		Dim lclsBeneficiar As Beneficiar
		Dim llngCover_old As Integer
		Dim lblnTotalParticip As Boolean
		Dim lblnTotalParticipCont As Boolean
        Dim lblnTotalParticipDesign As Boolean
        Dim llngIndex As Integer
		
		On Error GoTo Find_Err
		
		lrecReaBeneficiar = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'insudb.reaBeneficiar_a'
		'+Información leída el 27/03/2002
		
		lblnTotalParticip = False
		lblnTotalParticipCont = False
		
		With lrecReaBeneficiar
			.StoredProcedure = "reaBeneficiar_a"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				nTotalParticip = 0
				nTotalParticipCont = 0
				llngIndex = 0
				Do While Not .EOF
					lclsBeneficiar = New Beneficiar
					lclsBeneficiar.sClient = .FieldToClass("sClient")
					lclsBeneficiar.nParticip = .FieldToClass("nParticip")
					lclsBeneficiar.nRelation = .FieldToClass("nRelation")
					lclsBeneficiar.nModulec = .FieldToClass("nModulec")
					lclsBeneficiar.nCover = .FieldToClass("nCover")
					lclsBeneficiar.dDatedecla = .FieldToClass("dDatedecla")
					lclsBeneficiar.sIrrevoc = .FieldToClass("sIrrevoc")
                    lclsBeneficiar.sConting = .FieldToClass("sConting")
                    lclsBeneficiar.sDesign = .FieldToClass("sDesign")

                    If lclsBeneficiar.sConting = "1" And lclsBeneficiar.sDesign = "1" Then
                        nDuplicate = 2
                    End If
                    If lclsBeneficiar.sDesign = "1" Then
                        If llngCover_old = lclsBeneficiar.nCover Then
                            nTotalParticipDesign = nTotalParticipDesign + lclsBeneficiar.nParticip
                        Else
                            If nTotalParticipDesign <> 100 And llngIndex <> 0 Then
                                lblnTotalParticipDesign = True
                            End If
                            nTotalParticipDesign = 0
                            nTotalParticipDesign = nTotalParticipDesign + lclsBeneficiar.nParticip
                        End If
                        'se elimina end if y se agrega elseif
                    ElseIf lclsBeneficiar.sConting <> "1" Then
                        If llngCover_old = lclsBeneficiar.nCover Then
                            nTotalParticip = nTotalParticip + lclsBeneficiar.nParticip
                        Else
                            If nTotalParticip <> 100 And llngIndex <> 0 Then
                                lblnTotalParticip = True
                            End If
                            nTotalParticip = 0
                            nTotalParticip = nTotalParticip + lclsBeneficiar.nParticip
                        End If
                    Else
                        If llngCover_old = lclsBeneficiar.nCover Then
                            nTotalParticipCont = nTotalParticipCont + lclsBeneficiar.nParticip
                        Else
                            If nTotalParticipCont <> 100 And llngIndex <> 0 Then
                                lblnTotalParticipCont = True
                            End If
                            nTotalParticipCont = 0
                            nTotalParticipCont = nTotalParticipCont + lclsBeneficiar.nParticip
                        End If
                    End If
                    llngCover_old = lclsBeneficiar.nCover
                    Call Add(lclsBeneficiar)
                    'UPGRADE_NOTE: Object lclsBeneficiar may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                    lclsBeneficiar = Nothing
                    llngIndex = llngIndex + 1
                    .RNext()
                Loop
				.RCloseRec()
				Find = True
				If nTotalParticip = 100 Then
					If lblnTotalParticip Then
						nTotalParticip = 1
					End If
				End If
				
				If nTotalParticipCont = 100 And lblnTotalParticipCont Then
					nTotalParticipCont = 1
				End If
                If nTotalParticipDesign = 100 And lblnTotalParticipDesign Then
                    nTotalParticipDesign = 1
                End If

            End If
		End With
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecReaBeneficiar may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecReaBeneficiar = Nothing
		'UPGRADE_NOTE: Object lclsBeneficiar may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsBeneficiar = Nothing
	End Function
	
	'***Item: Returns a element of the collection (according Index)
	'*Item: Devuelve un elemento de la colección (segun índice)
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Beneficiar
		Get
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	'***Count: Returns the number of the element the collection has
	'*Count: Devuelve el número de elementos que posee la colección
	Public ReadOnly Property Count() As Integer
		Get
			Count = mCol.Count()
		End Get
	End Property
	
	'***NewEnum: Enumerates the collection for use in a For Each...Next loop
	'*NewEnum: Permite enumerar la colección para utilizarla en un ciclo For Each... Next
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
	
	'**%Remove: Delete the element of the collection
	'%Remove: Elimina un elemento de la colección
	Public Sub Remove(ByRef vntIndexKey As Object)
		mCol.Remove(vntIndexKey)
	End Sub
	
	'**% Class_Initialize: Control the creation of a collection instance
	'% Class_Initialize: Controla la creación de una instancia de la colección
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'**% Class_Terminate: Control the destruction of the collection instance
	'% Class_Terminate: Controla la destrucción de una instancia de la colección
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






