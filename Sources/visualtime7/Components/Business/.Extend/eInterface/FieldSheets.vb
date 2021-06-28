Option Strict Off
Option Explicit On
Public Class FieldSheets
	Implements System.Collections.IEnumerable
	
	Private mCol As Collection
	
	'%Add: Agrega un nuevo registro a la colección
	Public Function Add(ByVal lclsFieldSheet As FieldSheet) As FieldSheet
		mCol.Add(lclsFieldSheet)
		
		'+ Devolver el objeto creado
		Add = lclsFieldSheet
	End Function
	
	Public ReadOnly Property Item(ByVal vntIndexKey As Object) As FieldSheet
		Get
			'used when referencing an element in the collection
			'vntIndexKey contains either the Index or Key to the collection,
			'this is why it is declared as a Variant
			'Syntax: Set foo = x.Item(xyz) or Set foo = x.Item(5)
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	Public ReadOnly Property Count() As Integer
		Get
			'used when retrieving the number of elements in the
			'collection. Syntax: Debug.Print x.Count
			Count = mCol.Count()
		End Get
	End Property
	
	'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
	'Public ReadOnly Property NewEnum() As stdole.IUnknown
		'Get
			'this property allows you to enumerate
			'this collection with the For...Each syntax
			'NewEnum = mCol._NewEnum
		'End Get
	'End Property
	
	Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
		'UPGRADE_TODO: Uncomment and change the following line to return the collection enumerator. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="95F9AAD0-1319-4921-95F0-B9D3C4FF7F1C"'
		GetEnumerator = mCol.GetEnumerator
	End Function
	
	Public Sub Remove(ByRef vntIndexKey As Object)
		'used when removing an element from the collection
		'vntIndexKey contains either the Index or Key, which is why
		'it is declared as a Variant
		'Syntax: x.Remove(xyz)
		mCol.Remove(vntIndexKey)
	End Sub
	
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		'creates the collection when this class is created
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Terminate_Renamed()
		'destroys collection when this class is terminated
		'UPGRADE_NOTE: Object mCol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mCol = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
	
	'%Find : Esta función se encarga de de buscar la colección de datos de acuerdo a nSheet
	Public Function Find(ByVal nSheet As Integer, ByRef nFieldType As Integer) As Boolean
		Dim lrecreaFieldSheet As eRemoteDB.Execute
		Dim lclsRunRutine As eRemoteDB.Execute
		Dim sResult As String
		
		Dim lclsFieldSheet As FieldSheet
		
		On Error GoTo reaFieldSheet_Err
		
		lrecreaFieldSheet = New eRemoteDB.Execute
		
		With lrecreaFieldSheet
			.StoredProcedure = "reaFieldSheet"
			.Parameters.Add("nSheet", nSheet, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nFieldType", nFieldType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Do While Not .EOF
					lclsFieldSheet = New FieldSheet
					
					lclsFieldSheet.nSheet = nSheet
					lclsFieldSheet.nField = .FieldToClass("nField")
					lclsFieldSheet.nFieldType = nFieldType
					lclsFieldSheet.sTable = .FieldToClass("stable")
					lclsFieldSheet.nUsercode = .FieldToClass("nusercode")
					lclsFieldSheet.sFieldDesc = .FieldToClass("sfielddesc")
					lclsFieldSheet.sColumnName = .FieldToClass("sColumnname")
					lclsFieldSheet.sValue = .FieldToClass("svalue")
					lclsFieldSheet.sRutine = .FieldToClass("srutine")
					lclsFieldSheet.nRoworder = .FieldToClass("nroworder")
					lclsFieldSheet.nFieldOrder = .FieldToClass("nfieldorder")
					lclsFieldSheet.sValueslist = .FieldToClass("svalueslist")
					lclsFieldSheet.nDataType = .FieldToClass("nDataType")
					lclsFieldSheet.nFieldLarge = .FieldToClass("nfieldlarge")
					lclsFieldSheet.nObjtype = .FieldToClass("nobjtype")
					lclsFieldSheet.nTablehomo = .FieldToClass("ntablehomo")
					lclsFieldSheet.nOperator = .FieldToClass("noperator")
					lclsFieldSheet.nCondit = .FieldToClass("ncondit")
					lclsFieldSheet.sFieldCommen = .FieldToClass("sfieldcommen")
					lclsFieldSheet.sFieldrel = .FieldToClass("sfieldrel")
					lclsFieldSheet.sObligatory = .FieldToClass("sObligatory")
					lclsFieldSheet.sLastmove = .FieldToClass("sLastmove")
					lclsFieldSheet.nDecimal = .FieldToClass("nDecimal")
					
					If (nFieldType = 3) And (lclsFieldSheet.sValue = "") And (lclsFieldSheet.sRutine <> "") Then
						lclsRunRutine = New eRemoteDB.Execute
						With lclsRunRutine
							.StoredProcedure = "InsRunRutine"
							.Parameters.Add("sWord", lclsFieldSheet.sRutine, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 50, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
							.Parameters.Add("sResult", " ", eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 50, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
							
							If .Run(False) Then
								lclsFieldSheet.sValueRutine = .Parameters("sResult").Value
							Else
								lclsFieldSheet.sValueRutine = ""
							End If
						End With
						'UPGRADE_NOTE: Object lclsRunRutine may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
						lclsRunRutine = Nothing
					End If
					
					Call Add(lclsFieldSheet)
					'UPGRADE_NOTE: Object lclsFieldSheet may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lclsFieldSheet = Nothing
					.RNext()
				Loop 
				Find = True
			Else
				Find = False
			End If
		End With
		
reaFieldSheet_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecreaFieldSheet may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaFieldSheet = Nothing
	End Function
	
	
	'%Find2 : Esta función se encarga de de buscar la colección de datos de acuerdo a nSheet
	Public Function Find2(ByVal nSheet As Integer, ByRef nFieldType As Integer) As Boolean
		Dim lrecreaFieldSheet As eRemoteDB.Execute
		Dim lclsFieldSheet As FieldSheet
		
		On Error GoTo reaFieldSheet_Err
		
		lrecreaFieldSheet = New eRemoteDB.Execute
		
		With lrecreaFieldSheet
			.StoredProcedure = "reaFieldSheet"
			.Parameters.Add("nSheet", nSheet, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nFieldType", nFieldType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Do While Not .EOF
					lclsFieldSheet = New FieldSheet
					
					lclsFieldSheet.nSheet = nSheet
					lclsFieldSheet.nField = .FieldToClass("nField")
					lclsFieldSheet.nFieldType = nFieldType
					lclsFieldSheet.sTable = .FieldToClass("stable")
					lclsFieldSheet.nUsercode = .FieldToClass("nusercode")
					'% si es 3 busco el nombre del campo sin espacios
					If nFieldType = 3 Then
						lclsFieldSheet.sFieldDesc = Replace(.FieldToClass("sfielddesc"), " ", "")
					Else
						lclsFieldSheet.sFieldDesc = .FieldToClass("sfielddesc")
					End If
					lclsFieldSheet.sColumnName = .FieldToClass("sColumnname")
					lclsFieldSheet.sValue = .FieldToClass("svalue")
					lclsFieldSheet.sRutine = .FieldToClass("srutine")
					lclsFieldSheet.nRoworder = .FieldToClass("nroworder")
					lclsFieldSheet.nFieldOrder = .FieldToClass("nfieldorder")
					lclsFieldSheet.sValueslist = .FieldToClass("svalueslist")
					lclsFieldSheet.nDataType = .FieldToClass("nDataType")
					lclsFieldSheet.nFieldLarge = .FieldToClass("nfieldlarge")
					lclsFieldSheet.nObjtype = .FieldToClass("nobjtype")
					lclsFieldSheet.nTablehomo = .FieldToClass("ntablehomo")
					lclsFieldSheet.nOperator = .FieldToClass("noperator")
					lclsFieldSheet.nCondit = .FieldToClass("ncondit")
					lclsFieldSheet.sFieldCommen = .FieldToClass("sfieldcommen")
					lclsFieldSheet.sFieldrel = .FieldToClass("sfieldrel")
					lclsFieldSheet.sObligatory = .FieldToClass("sObligatory")
					lclsFieldSheet.sLastmove = .FieldToClass("sLastmove")
					
					Call Add(lclsFieldSheet)
					'UPGRADE_NOTE: Object lclsFieldSheet may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lclsFieldSheet = Nothing
					.RNext()
				Loop 
				Find2 = True
			Else
				Find2 = False
			End If
		End With
		
reaFieldSheet_Err: 
		If Err.Number Then
			Find2 = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecreaFieldSheet may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaFieldSheet = Nothing
    End Function

    '%Find_Dinamic_Table : Esta función se encarga de de buscar la colección de datos de acuerdo a nSheet para una tabla dinamica de certificados
    Public Function Find_Dinamic_Table(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nSheet As Integer, ByRef nFieldType As Integer, ByVal dEffecdate As Date) As Boolean
        Dim lrecreaFieldSheet As eRemoteDB.Execute
        Dim lclsRunRutine As eRemoteDB.Execute
        Dim lclsValues As eFunctions.Values
        Dim sResult As String

        Dim lclsFieldSheet As FieldSheet

        On Error GoTo reaFieldSheet_Dinamic_Table_Err

        lrecreaFieldSheet = New eRemoteDB.Execute

        With lrecreaFieldSheet
            .StoredProcedure = "reaFieldSheet_Dinamic_Table"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nSheet", nSheet, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nFieldType", nFieldType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run Then
                Do While Not .EOF
                    lclsFieldSheet = New FieldSheet

                    lclsFieldSheet.nSheet = nSheet
                    lclsFieldSheet.nField = .FieldToClass("nField")
                    lclsFieldSheet.nFieldType = nFieldType
                    lclsFieldSheet.sTable = .FieldToClass("stable")
                    lclsFieldSheet.nUsercode = .FieldToClass("nusercode")
                    lclsFieldSheet.sFieldDesc = .FieldToClass("sfielddesc")
                    lclsFieldSheet.sColumnName = .FieldToClass("sColumnname")
                    lclsFieldSheet.sValue = .FieldToClass("svalue")
                    lclsFieldSheet.sRutine = .FieldToClass("srutine")
                    lclsFieldSheet.nRoworder = .FieldToClass("nroworder")
                    lclsFieldSheet.nFieldOrder = .FieldToClass("nfieldorder")
                    lclsFieldSheet.sValueslist = .FieldToClass("svalueslist")
                    lclsFieldSheet.nDataType = .FieldToClass("nDataType")
                    lclsFieldSheet.nFieldLarge = .FieldToClass("nfieldlarge")
                    lclsFieldSheet.nObjtype = .FieldToClass("nobjtype")
                    lclsFieldSheet.nTablehomo = .FieldToClass("ntablehomo")
                    lclsFieldSheet.nOperator = .FieldToClass("noperator")
                    lclsFieldSheet.nCondit = .FieldToClass("ncondit")
                    lclsFieldSheet.sFieldCommen = .FieldToClass("sfieldcommen")
                    lclsFieldSheet.sFieldrel = .FieldToClass("sfieldrel")
                    lclsFieldSheet.sObligatory = .FieldToClass("sObligatory")
                    lclsFieldSheet.sLastmove = .FieldToClass("sLastmove")
                    lclsFieldSheet.nDecimal = .FieldToClass("nDecimal")
                    lclsFieldSheet.sValue2 = .FieldToClass("sValue2")
                    lclsFieldSheet.nValue = .FieldToClass("nValue")
                    lclsFieldSheet.dValue = .FieldToClass("dValue")

                    lclsValues = New eFunctions.Values
                    If lclsFieldSheet.sValue2 <> eRemoteDB.Constants.strNull Then
                        lclsFieldSheet.sValue = lclsFieldSheet.sValue2
                    End If

                    If lclsFieldSheet.nValue <> eRemoteDB.Constants.intNull Then
                        If lclsFieldSheet.nDecimal <> eRemoteDB.Constants.intNull Then
                            lclsFieldSheet.sValue = lclsValues.TypeToString(lclsFieldSheet.nValue, eFunctions.Values.eTypeData.etdDouble, False, lclsFieldSheet.nDecimal)
                        Else
                            lclsFieldSheet.sValue = lclsValues.TypeToString(lclsFieldSheet.nValue, eFunctions.Values.eTypeData.etdDouble, False)
                        End If
                    End If

                    If lclsFieldSheet.dValue <> eRemoteDB.Constants.dtmNull Then
                        lclsFieldSheet.sValue = lclsValues.TypeToString(lclsFieldSheet.dValue, eFunctions.Values.eTypeData.etdDate)
                    End If
                    If (nFieldType = 3) And (lclsFieldSheet.sValue = "") And (lclsFieldSheet.sRutine <> "") Then
                        lclsRunRutine = New eRemoteDB.Execute
                        With lclsRunRutine
                            .StoredProcedure = "InsRunRutine"
                            .Parameters.Add("sWord", lclsFieldSheet.sRutine, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 50, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                            .Parameters.Add("sResult", " ", eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 50, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

                            If .Run(False) Then
                                lclsFieldSheet.sValueRutine = .Parameters("sResult").Value
                            Else
                                lclsFieldSheet.sValueRutine = ""
                            End If
                        End With
                        'UPGRADE_NOTE: Object lclsRunRutine may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                        lclsRunRutine = Nothing
                    End If

                    Call Add(lclsFieldSheet)
                    'UPGRADE_NOTE: Object lclsFieldSheet may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                    lclsFieldSheet = Nothing
                    .RNext()
                Loop
                Find_Dinamic_Table = True
            Else
                Find_Dinamic_Table = False
            End If
        End With

reaFieldSheet_Dinamic_Table_Err:
        If Err.Number Then
            Find_Dinamic_Table = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lrecreaFieldSheet may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaFieldSheet = Nothing
        lclsValues = Nothing
    End Function
End Class






