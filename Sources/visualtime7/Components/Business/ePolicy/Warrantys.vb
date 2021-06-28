Option Strict Off
Option Explicit On
Public Class Warrantys
	Implements System.Collections.IEnumerable
	'**+Objective: Collection that supports the class 'Warranty'.
	'**+Version: $$Revision: 4 $
	'+Objetivo: Colección que le da soporte a la clase 'Warranty'.
	'+Version: $$Revision: 4 $
	
	'**+Objective: Local variable to hold collection.
	'+Objetivo: Variable Local para almacenar la colección.
	Private mcolWarranty As Collection
	
	
	
	'**%Objective: It adds an element to the collection.
	'**%Parameters:
	'**%    lclsWarranty -
	'%Objetivo: Agrega un elemento a la colección.
	'%Parámetros:
	'%    lclsWarranty -
	Public Function Add(ByRef lclsWarranty As Warranty) As Warranty
		
		
		mcolWarranty.Add(lclsWarranty)
		
		'**-return the object created
		Add = lclsWarranty
		
		Exit Function
	End Function

	'**%Objective: Function that makes the search in the table 'Warranty'.
	'**%Parameters:
	'**%     scertype         -  type of registry
	'**%     nbranch          -  branch
	'**%     nproduct         -  product
	'**%     npolicy          -  i number of poliza
	'**%     ncertif          -  i number of certificate
	'**%     deffecdate       -  date of effect of the registry
	'%Objetivo: Función que realiza la busqueda en la tabla 'Warranty'.
	'%Parámetros:
	'%     scertype        -   tipo de registro
	'%     nbranch         -   ramo
	'%     nproduct        -   producto
	'%     npolicy         -   numero de poliza
	'%     ncertif         -   numero de certificado
	'%     deffecdate      -   fecha de efecto del registro
	Public Function Find(ByVal sCertype As String, ByVal nBranch As Short, ByVal nProduct As Short, ByVal nPolicy As Integer, ByVal nCertif As Integer, ByVal dEffecdate As Date) As Boolean
		Dim lclsWarranty As eRemoteDB.Execute
		Dim lclsWarrantyItem As Warranty
		
		lclsWarranty = New eRemoteDB.Execute
		
		'+ Define all parameters for the stored procedures 'insudb.reaWarranty'. Generated on 21/07/2004 12:11:57 p.m.
		With lclsWarranty
			.StoredProcedure = "reaWarranty_a"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(True) Then
				Do While Not .EOF
					lclsWarrantyItem = New Warranty
					lclsWarrantyItem.sCertype = sCertype
					lclsWarrantyItem.nBranch = nBranch
					lclsWarrantyItem.nProduct = nProduct
					lclsWarrantyItem.nPolicy = nPolicy
					lclsWarrantyItem.nCertif = nCertif
					lclsWarrantyItem.nWarrnumber = .FieldToClass("nWarrnumber")
					lclsWarrantyItem.nTypewarranty = .FieldToClass("nTypewarranty")
					lclsWarrantyItem.sDocwarranty = .FieldToClass("sDocwarranty")
					lclsWarrantyItem.nCurrency = .FieldToClass("nCurrency")
					lclsWarrantyItem.nCapacity = .FieldToClass("nCapacity")
					lclsWarrantyItem.nNotenum = .FieldToClass("nNotenum")
                    lclsWarrantyItem.dMaturity = .FieldToClass("dMaturity")
                    lclsWarrantyItem.sCliename = .FieldToClass("sCliename")
                    lclsWarrantyItem.sDescrole = .FieldToClass("sDescrole")
                    lclsWarrantyItem.nBondStatus = .FieldToClass("nBondStatus")

					Call Add(lclsWarrantyItem)
					lclsWarrantyItem = Nothing
					.RNext()
				Loop 
				Find = True
				.RCloseRec()
			Else
				Find = False
			End If
		End With
		
		lclsWarranty = Nothing
		lclsWarrantyItem = Nothing
		
		Exit Function
	End Function
	
	'**%Objective: This property is used when reference to an element becomes of the collection.
	'**%Parameters:
	'**%    vIndexKey   -
	'%Objetivo: Esta propiedad es usada cuando se hace referencia a un elemento de la colección.
	'%Parámetros:
	'%    vIndexKey -
	Public ReadOnly Property Item(ByVal vIndexKey As Object) As Warranty
		Get
			
			Item = mcolWarranty.Item(vIndexKey)
			
			Exit Property
		End Get
	End Property
	
	'**%Objective: It returns the amount of existing elements in the collection.
	'%Objetivo: Retorna la contidad de elementos existentes en la colección.
	Public ReadOnly Property Count() As Integer
		Get
			
			Count = mcolWarranty.Count()
			
			Exit Property
		End Get
	End Property
	
	'**%Objective: This property allows you to enumerate this collection with the "For...Each".
	'%Objetivo: Esta propiedad permite enumerar los elementos de la colección por medio de un "For...Each".
	'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
	'Public ReadOnly Property NewEnum() As stdole.IUnknown
		'Get
			'If Not IsIDEMode Then
			'End If
			'
			'NewEnum = mcolWarranty._NewEnum
			'
			'Exit Property
'ErrorHandler: '
			'ProcError("Warrantys.NewEnum()")
		'End Get
	'End Property
	
	Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
				GetEnumerator = mcolWarranty.GetEnumerator
	End Function
	
	'**%Objective: It allows to remove an element of the collection.
	'**%Parameters:
	'**%    vIndexKey   -
	'%Objetivo: Permite eliminar un elemento de la colección.
	'%Parámetros:
	'%    vIndexKey -
	Public Sub Remove(ByRef vIndexKey As Object)
		
		mcolWarranty.Remove(vIndexKey)
		
		Exit Sub
	End Sub
	
	'**%Objective: Creates the collection when this class is created.
	'%Objetivo: Crea la colección cuando se crea esta clase.
	Private Sub Class_Initialize_Renamed()
		
		mcolWarranty = New Collection
		
		Exit Sub
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'**%Objective: Destroys collection when this class is terminated.
	'%Objetivo: Destruye la colección cuando se termina esta clase.
	Private Sub Class_Terminate_Renamed()
		
		Exit Sub
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
End Class











