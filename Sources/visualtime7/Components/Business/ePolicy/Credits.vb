Option Strict Off
Option Explicit On
Public Class Credits
	Implements System.Collections.IEnumerable
	'**+Objective: Collection that supports the class 'Credit'.
	'**+Version: $$Revision: 4 $
	'+Objetivo: Colección que le da soporte a la clase 'Credit'.
	'+Version: $$Revision: 4 $
	
	'**+Objective: Local variable to hold collection.
	'+Objetivo: Variable Local para almacenar la colección.
	Private mcolCredit As Collection
	
	'**%Objective: It adds an element to the collection.
	'**%Parameters:
	'**%    lclsCredit -
	'%Objetivo: Agrega un elemento a la colección.
	'%Parámetros:
	'%    lclsCredit -
	Public Function Add(ByRef lclsCredit As Credit) As Credit
		
		mcolCredit.Add(lclsCredit)
		
		'**-return the object created
		Add = lclsCredit


		Exit Function
	End Function
	
	'**%Objective: Function that makes the search in the table 'Credit'.
	'**%Parameters:
	'**%     scertype         -  type of registry
	'**%     nbranch          -  branch
	'**%     nproduct         -  product
	'**%     npolicy          -  i number of poliza
	'**%     ncertif          -  i number of certificate
	'**%     deffecdate       -  date of effect of the registry
	'%Objetivo: Función que realiza la busqueda en la tabla 'Credit'.
	'%Parámetros:
	'%     scertype        -   tipo de registro
	'%     nbranch         -   ramo
	'%     nproduct        -   producto
	'%     npolicy         -   numero de poliza
	'%     ncertif         -   numero de certificado
	'%     deffecdate      -   fecha de efecto del registro
	Public Function Find(ByVal sCertype As String, ByVal nBranch As Short, ByVal nProduct As Short, ByVal nPolicy As Integer, ByVal nCertif As Integer, ByVal dEffecdate As Date) As Boolean
		Dim lclsCredit As eRemoteDB.Execute
		Dim lclsCreditItem As Credit
		
		lclsCredit = New eRemoteDB.Execute
		
		'+ Define all parameters for the stored procedures 'insudb.reaCredit'. Generated on 21/07/2004 03:31:12 p.m.
		With lclsCredit
			.StoredProcedure = "reaCredit_a"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(True) Then
				Do While Not .EOF
					lclsCreditItem = New Credit
					lclsCreditItem.sCertype = sCertype
					lclsCreditItem.nBranch = nBranch
					lclsCreditItem.nProduct = nProduct
					lclsCreditItem.nPolicy = nPolicy
					lclsCreditItem.nCertif = nCertif
					lclsCreditItem.ninsmodality = .FieldToClass("ninsmodality")
					lclsCreditItem.nguar_type = .FieldToClass("nguar_type")
					lclsCreditItem.scontracnum = .FieldToClass("scontracnum")
					lclsCreditItem.dcontracdat = .FieldToClass("dcontracdat")
					lclsCreditItem.ntime_unit = .FieldToClass("ntime_unit")
					lclsCreditItem.dterm_date = .FieldToClass("dterm_date")
					lclsCreditItem.ntime_eject = .FieldToClass("ntime_eject")
					lclsCreditItem.ncredcau = .FieldToClass("ncredcau")
					lclsCreditItem.nindemper = .FieldToClass("nindemper")
					lclsCreditItem.nmoraallow = .FieldToClass("nmoraallow")
					lclsCreditItem.ntransmon1 = .FieldToClass("ntransmon1")
					lclsCreditItem.ntransmon2 = .FieldToClass("ntransmon2")
					lclsCreditItem.nindper1 = .FieldToClass("nindper1")
					lclsCreditItem.nindper2 = .FieldToClass("nindper2")
                    lclsCreditItem.sFollowUp = .FieldToClass("sfollowup")
                    lclsCreditItem.sContractObject = .FieldToClass("sContractObject")
                    lclsCreditItem.nBondstatus = .FieldToClass("nBondstatus")
                    lclsCreditItem.sInsurSector = .FieldToClass("sInsurSector")

					Call Add(lclsCreditItem)
					lclsCreditItem = Nothing
					.RNext()
				Loop 
				
				Find = True
				.RCloseRec()
				
			Else
				Find = False
			End If
		End With
		
		lclsCredit = Nothing
		lclsCreditItem = Nothing
		
		Exit Function
	End Function
	
	'**%Objective: This property is used when reference to an element becomes of the collection.
	'**%Parameters:
	'**%    vIndexKey   -
	'%Objetivo: Esta propiedad es usada cuando se hace referencia a un elemento de la colección.
	'%Parámetros:
	'%    vIndexKey -
	Public ReadOnly Property Item(ByVal vIndexKey As Object) As Credit
		Get
			
			Item = mcolCredit.Item(vIndexKey)
			
			Exit Property
		End Get
	End Property
	
	'**%Objective: It returns the amount of existing elements in the collection.
	'%Objetivo: Retorna la contidad de elementos existentes en la colección.
	Public ReadOnly Property Count() As Integer
		Get
			
			Count = mcolCredit.Count()
			
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
			'NewEnum = mcolCredit._NewEnum
			'
			'Exit Property
'ErrorHandler: '
			'ProcError("Credits.NewEnum()")
		'End Get
	'End Property
	
	Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
				GetEnumerator = mcolCredit.GetEnumerator
	End Function
	
	'**%Objective: It allows to remove an element of the collection.
	'**%Parameters:
	'**%    vIndexKey   -
	'%Objetivo: Permite eliminar un elemento de la colección.
	'%Parámetros:
	'%    vIndexKey -
	Public Sub Remove(ByRef vIndexKey As Object)
		
		mcolCredit.Remove(vIndexKey)
		
		Exit Sub
	End Sub
	
	'**%Objective: Creates the collection when this class is created.
	'%Objetivo: Crea la colección cuando se crea esta clase.
	Private Sub Class_Initialize_Renamed()
		
		mcolCredit = New Collection
		
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











