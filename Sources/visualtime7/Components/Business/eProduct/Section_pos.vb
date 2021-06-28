Option Strict Off
Option Explicit On
Public Class Section_pos
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Section_pos.cls                          $%'
	'% $Author:: Nvaplat11                                  $%'
	'% $Date:: 1/12/03 3:20p                                $%'
	'% $Revision:: 13                                       $%'
	'%-------------------------------------------------------%'
	
	Private mCol As Collection
	
	'% Add: Añade una nueva instancia de la clase Section_po a la colección
	Public Function Add(ByRef objSection_po As Section_po) As Section_po
		mCol.Add(objSection_po)
		
		Add = objSection_po
		'UPGRADE_NOTE: Object objSection_po may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		objSection_po = Nothing
	End Function
	
	'% Find: Este metodo carga los elementos de la tabla
	Public Function Find(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nTratypep As Integer, ByVal sPolitype As String, ByVal sCompon As String, ByVal dEffecdate As Date, Optional ByVal nType_amend As Integer = 0, Optional ByVal nOrigin As Integer = 0) As Boolean
		Dim lrecreaSection_po As eRemoteDB.Execute
		Dim lclsSection_po As eProduct.Section_po
		
		On Error GoTo Find_Err
		
		lrecreaSection_po = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'insudb.reaSection_po_a'
		'+ Información leída el 03/07/2002
		
		With lrecreaSection_po
			.StoredProcedure = "reaSection_po_a"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTratypep", nTratypep, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sPolitype", sPolitype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCompon", sCompon, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nType_amend", nType_amend, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOrigin", nOrigin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Do While Not .EOF
					lclsSection_po = New eProduct.Section_po
					lclsSection_po.nBranch = nBranch
					lclsSection_po.nProduct = nProduct
					lclsSection_po.sPolitype = sPolitype
					lclsSection_po.sCompon = sCompon
					lclsSection_po.sCodispl = .FieldToClass("sCodispl")
					lclsSection_po.sDescript = .FieldToClass("sDescript")
					lclsSection_po.nSequence = eRemoteDB.Constants.intNull
					If .FieldToClass("Seq_final") <> eRemoteDB.Constants.intNull Then
						lclsSection_po.nSequence = .FieldToClass("Seq_final")
                    End If

                    lclsSection_po.nTratypep = .FieldToClass("nTratypep")
                    lclsSection_po.sReport = .FieldToClass("sReport")
                    lclsSection_po.nOrder = .FieldToClass("nOrder")
                    lclsSection_po.sRoutine = .FieldToClass("sRoutine")
                    lclsSection_po.nId = .FieldToClass("nId")
					
					Call Add(lclsSection_po)
                    lclsSection_po = Nothing
					.RNext()
				Loop 
				
				.RCloseRec()
				Find = True
			End If
		End With
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
        lrecreaSection_po = Nothing
        lclsSection_po = Nothing
	End Function
	
	'* Item: Devuelve un elemento de la colección (segun índice)
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Section_po
		Get
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	'* Count: Devuelve el número de elementos que posee la colección
	Public ReadOnly Property Count() As Integer
		Get
			Count = mCol.Count()
		End Get
	End Property
	
    Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
        GetEnumerator = mCol.GetEnumerator
    End Function
	
	'% Remove: Elimina un elemento de la colección
	Public Sub Remove(ByRef vntIndexKey As Object)
		mCol.Remove(vntIndexKey)
	End Sub
	
	'* Class_Initialize: Controla la creación de una instancia de la colección
    Private Sub Class_Initialize_Renamed()
        mCol = New Collection
    End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'* Class_Terminate: Controla la destrucción de una instancia de la colección
    Private Sub Class_Terminate_Renamed()
        mCol = Nothing
    End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
End Class






