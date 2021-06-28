Option Strict Off
Option Explicit On
Public Class LocateTar_Soats
	Implements System.Collections.IEnumerable
	'**+Objective: Collection that supports the class 'LocateTar_Soat'.
	'**+Version: $$Revision: 4 $
	'+Objetivo: Colección que le da soporte a la clase 'LocateTar_Soat'.
	'+Version: $$Revision: 4 $
	
	'**+Objective: Local variable to hold collection.
	'+Objetivo: Variable Local para almacenar la colección.
	Private mcolLocateTar_Soat As Collection
	
	'**%Objective: It adds an element to the collection.
	'**%Parameters:
	'**%    lclsLocateTar_Soat -
	'%Objetivo: Agrega un elemento a la colección.
	'%Parámetros:
	'%    lclsLocateTar_Soat -
	Public Function Add(ByRef lclsLocateTar_Soat As LocateTar_Soat) As LocateTar_Soat

		'**-set the properties passed into the method
		mcolLocateTar_Soat.Add(lclsLocateTar_Soat)
		
		'**-return the object created
		Add = lclsLocateTar_Soat
		lclsLocateTar_Soat = Nothing
		
		Exit Function

	End Function
	
	'**%Objective: Function that makes the search in the table 'LocateTar_Soat'.
	'**%Parameters:
	'**%    Pending   -
	'%Objetivo: Función que realiza la busqueda en la tabla 'LocateTar_Soat'.
	'%Parámetros:
	'%    Pendiente -
	Public Function Find(ByVal dEffecDate As Date) As Boolean
		Dim lclsLocateTar_Soat As eRemoteDB.Execute
		Dim lclsLocateTar_SoatItem As LocateTar_Soat
		
        On Error GoTo ErrorHandler
		
		lclsLocateTar_Soat = New eRemoteDB.Execute
		
		'+ Define all parameters for the stored procedures 'insudb.reaLocateTar_Soat'. Generated on 04/01/2005 11:42:26 AM
		With lclsLocateTar_Soat
			.StoredProcedure = "reaLocateTar_Soat_a"
			.Parameters.Add("dEffecDate", dEffecDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(True) Then
				Do While Not .EOF
					lclsLocateTar_SoatItem = New LocateTar_Soat
					lclsLocateTar_SoatItem.dEffecDate = .FieldToClass("dEffecDate")
					lclsLocateTar_SoatItem.nLocal_Type = .FieldToClass("nLocat_Type")
					lclsLocateTar_SoatItem.nZipCode_Ini = .FieldToClass("nZipCode_Ini")
					lclsLocateTar_SoatItem.nZipCode_End = .FieldToClass("nZipCode_End")
					lclsLocateTar_SoatItem.sDescript = .FieldToClass("sDescript")
					lclsLocateTar_SoatItem.dNullDate = .FieldToClass("dNullDate")
					lclsLocateTar_SoatItem.bEditRecord = IIf(.FieldToClass("dNullDate") = dtmNull, True, False)
					Call Add(lclsLocateTar_SoatItem)
					lclsLocateTar_SoatItem = Nothing
					.RNext()
				Loop 
				Find = True
				.RCloseRec()
			Else
				Find = False
			End If
		End With
		lclsLocateTar_Soat = Nothing
		
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
	Public ReadOnly Property Item(ByVal vIndexKey As Object) As LocateTar_Soat
		Get

			Item = mcolLocateTar_Soat.Item(vIndexKey)
			
			Exit Property

        End Get
	End Property
	
	'**%Objective: It returns the amount of existing elements in the collection.
	'%Objetivo: Retorna la contidad de elementos existentes en la colección.
	Public ReadOnly Property Count() As Integer
		Get

			Count = mcolLocateTar_Soat.Count()
			
			Exit Property

        End Get
	End Property
	
	Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
				GetEnumerator = mcolLocateTar_Soat.GetEnumerator
	End Function
	
	'**%Objective: It allows to remove an element of the collection.
	'**%Parameters:
	'**%    vIndexKey   -
	'%Objetivo: Permite eliminar un elemento de la colección.
	'%Parámetros:
	'%    vIndexKey -
	Public Sub Remove(ByRef vIndexKey As Object)

		mcolLocateTar_Soat.Remove(vIndexKey)
		
		Exit Sub

    End Sub
	
	'**%Objective: Creates the collection when this class is created.
	'%Objetivo: Crea la colección cuando se crea esta clase.
	Private Sub Class_Initialize_Renamed()
		
		mcolLocateTar_Soat = New Collection
		
		Exit Sub

    End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'**%Objective: Destroys collection when this class is terminated.
	'%Objetivo: Destruye la colección cuando se termina esta clase.
	Private Sub Class_Terminate_Renamed()

		mcolLocateTar_Soat = Nothing
		
		Exit Sub

    End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
End Class






