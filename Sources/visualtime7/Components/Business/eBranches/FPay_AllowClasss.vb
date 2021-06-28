Option Strict Off
Option Explicit On
Public Class FPay_AllowClasss
	Implements System.Collections.IEnumerable
	'**+Objective: Collection that supports the class 'FPay_AllowClass'.
	'**+Version: $$Revision: 4 $
	'+Objetivo: Colección que le da soporte a la clase 'FPay_AllowClass'.
	'+Version: $$Revision: 4 $
	
	'**+Objective: Local variable to hold collection.
	'+Objetivo: Variable Local para almacenar la colección.
	Private mcolFPay_AllowClass As Collection
	
	'**%Objective: It adds an element to the collection.
	'**%Parameters:
	'**%    lclsFPay_AllowClass -
	'%Objetivo: Agrega un elemento a la colección.
	'%Parámetros:
	'%    lclsFPay_AllowClass -
	Public Function Add(ByRef lclsFPay_AllowClass As FPay_AllowClass) As FPay_AllowClass
		'On Error GoTo ErrorHandler
		
		'**-set the properties passed into the method
		mcolFPay_AllowClass.Add(lclsFPay_AllowClass)
		
		'**-return the object created
		Add = lclsFPay_AllowClass
		lclsFPay_AllowClass = Nothing
		
		Exit Function
    End Function
	
	'**%Objective: Function that makes the search in the table 'FPay_AllowClass'.
	'**%Parameters:
	'**%    nBranch     - Code of the line of business
	'**%    dEffecDate  - Date which from the record is valid.
	'**%    nProduct    - Code of the product
	'%Objetivo: Función que realiza la busqueda en la tabla 'FPay_AllowClass'.
	'%Parámetros:
	'%    nBranch     - Código del ramo comercial
	'%    dEffecDate  - Fecha de efecto del registro.
	'%    nProduct    - Código del producto
	Public Function Find(ByVal nBranch As Short, ByVal dEffecDate As Date, ByVal nProduct As Short) As Boolean
		Dim lclsFPay_AllowClass As eRemoteDB.Execute
		Dim lclsFPay_AllowClassItem As FPay_AllowClass
		
        On Error GoTo ErrorHandler
		
		lclsFPay_AllowClass = New eRemoteDB.Execute
		
		'+ Define all parameters for the stored procedures 'insudb.reaFPay_AllowClass'. Generated on 06/01/2005 08:26:55 a.m.
		With lclsFPay_AllowClass
			.StoredProcedure = "reaFPay_AllowClass_a"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecDate", dEffecDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(True) Then
				Do While Not .EOF
					lclsFPay_AllowClassItem = New FPay_AllowClass
					lclsFPay_AllowClassItem.nBranch = .FieldToClass("nBranch")
					lclsFPay_AllowClassItem.dEffecDate = .FieldToClass("dEffecDate")
					lclsFPay_AllowClassItem.nProduct = .FieldToClass("nProduct")
					lclsFPay_AllowClassItem.nPayFreq = .FieldToClass("nPayFreq")
					lclsFPay_AllowClassItem.nSOATClass = .FieldToClass("nSOATClass")
					lclsFPay_AllowClassItem.dNullDate = .FieldToClass("dNullDate")
					lclsFPay_AllowClassItem.bEditRecord = lclsFPay_AllowClassItem.dNullDate = dtmNull
					Call Add(lclsFPay_AllowClassItem)
					lclsFPay_AllowClassItem = Nothing
					.RNext()
				Loop 
				Find = True
				.RCloseRec()
			Else
				Find = False
			End If
		End With
		lclsFPay_AllowClass = Nothing
		
		Exit Function
ErrorHandler: 
        If Err.Number Then
            Find = False
        End If
	End Function
	
	'**%Objective: This property is used when reference to an element becomes of the collection.
	'**%Parameters:
	'**%    vIndexKey   -
	'%Objetivo: Esta propiedad es usada cuando se hace referencia a un elemento de la colección.
	'%Parámetros:
	'%    vIndexKey -
	Default Public ReadOnly Property Item(ByVal vIndexKey As Object) As FPay_AllowClass
		Get
			'On Error GoTo ErrorHandler
			
			Item = mcolFPay_AllowClass.Item(vIndexKey)
			
			Exit Property
        End Get
	End Property
	
	'**%Objective: It returns the amount of existing elements in the collection.
	'%Objetivo: Retorna la contidad de elementos existentes en la colección.
	Public ReadOnly Property Count() As Integer
		Get
			'On Error GoTo ErrorHandler
			
			Count = mcolFPay_AllowClass.Count()
			
			Exit Property
        End Get
	End Property
	
    Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
        GetEnumerator = mcolFPay_AllowClass.GetEnumerator
    End Function
	
	'**%Objective: It allows to remove an element of the collection.
	'**%Parameters:
	'**%    vIndexKey   -
	'%Objetivo: Permite eliminar un elemento de la colección.
	'%Parámetros:
	'%    vIndexKey -
	Public Sub Remove(ByRef vIndexKey As Object)
		'On Error GoTo ErrorHandler
		
		mcolFPay_AllowClass.Remove(vIndexKey)
		
		Exit Sub

	End Sub
	
	'**%Objective: Creates the collection when this class is created.
	'%Objetivo: Crea la colección cuando se crea esta clase.
	Private Sub Class_Initialize_Renamed()
		'On Error GoTo ErrorHandler
		
		mcolFPay_AllowClass = New Collection
		
		Exit Sub

	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'**%Objective: Destroys collection when this class is terminated.
	'%Objetivo: Destruye la colección cuando se termina esta clase.
	Private Sub Class_Terminate_Renamed()
		'On Error GoTo ErrorHandler
		
		mcolFPay_AllowClass = Nothing
		
		Exit Sub

	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
End Class






