Option Strict Off
Option Explicit On
Public Class Tar_au_bons
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Tar_au_bons.cls                          $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 12:36p                               $%'
	'% $Revision:: 10                                       $%'
	'%-------------------------------------------------------%'
	
	Private mCol As Collection
	
	'**-Auxiliary variables
	'- Variables auxiliares
	
	Private mintBranch As Integer
	Private mintProduct As Integer
	
	'**% Add: add a new instance of the class Tar_au_bon to the collection
	'% Add: Añade una nueva instancia de la clase Tar_au_bon a la colección
	Public Function Add(ByRef objElement As Tar_au_bon) As Tar_au_bon
		mCol.Add(objElement)
		
		'**+ Return the created object
		'+ Retorna el objeto creado
		Add = objElement
	End Function
	
	'**% Find: restores the information of the Claim Discount
	'% Find: Devuelve la información de los Descuentos por Siniestralidad
	Public Function Find(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, Optional ByVal bFind As Boolean = False) As Boolean
		
		'**- Declare the variable that determinate the result of the function (True/False)
		'- Se declara la variable que determina el resultado de la funcion (True/False)
		
		Static lblnRead As Boolean
		Dim lrecreaTar_au_bonDP041 As eRemoteDB.Execute
		Dim lclsTar_au_bon As eProduct.Tar_au_bon
		
		On Error GoTo Find_Err
		
		lrecreaTar_au_bonDP041 = New eRemoteDB.Execute
		
		'**+Parameters definition for the stored procedure 'insudb.reaTar_au_bonDP041'
		'**+Data read on 05/14/2001 14:08:12
		'+Definición de parámetros para stored procedure 'insudb.reaTar_au_bonDP041'
		'+Información leída el 14/05/2001 14:08:12
		
		If mintBranch <> nBranch Or mintProduct <> nProduct Or bFind Then
			
			mintBranch = nBranch
			mintProduct = nProduct
			
			With lrecreaTar_au_bonDP041
				.StoredProcedure = "reaTar_au_bonDP041"
				.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				
				If .Run Then
					Do While Not .EOF
						lclsTar_au_bon = New eProduct.Tar_au_bon
						lclsTar_au_bon.nYear = .FieldToClass("nYear")
						lclsTar_au_bon.nClaimrat = .FieldToClass("nClaimrat")
						lclsTar_au_bon.nDiscount = .FieldToClass("nDiscount")
						
						Call Add(lclsTar_au_bon)
						
						'UPGRADE_NOTE: Object lclsTar_au_bon may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
						lclsTar_au_bon = Nothing
						.RNext()
					Loop 
					
					.RCloseRec()
					lblnRead = True
				Else
					lblnRead = False
				End If
			End With
		End If
		
Find_Err: 
		If Err.Number Then
			lblnRead = False
		End If
		Find = lblnRead
		'UPGRADE_NOTE: Object lrecreaTar_au_bonDP041 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaTar_au_bonDP041 = Nothing
	End Function
	
	'*** Item: restores one element of the collection (accourding ot the index)
	'* Item: Devuelve un elemento de la colección (segun índice)
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Tar_au_bon
		Get
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	'*** Count: restores the elements number that the collection owns
	'* Count: Devuelve el número de elementos que posee la colección
	Public ReadOnly Property Count() As Integer
		Get
			Count = mCol.Count()
		End Get
	End Property
	
	'*** NewEnum: Allows to enumerate the collection for using it in a cycle For Each... Next
	'* NewEnum: Permite enumerar la colección para utilizarla en un ciclo For Each... Next
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
	
	'**% Remove: deletes an element of the collection
	'% Remove: Elimina un elemento de la colección
	Public Sub Remove(ByRef vntIndexKey As Object)
		mCol.Remove(vntIndexKey)
	End Sub
	
	'**% Class_Initialize: controls the creation of an instance of the collection
	'% Class_Initialize: Controla la creación de una instancia de la colección
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'**% Class_Terminate: Controls the delete of an instance of the collection
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






