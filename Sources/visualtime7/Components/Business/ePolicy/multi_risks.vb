Option Strict Off
Option Explicit On
Public Class multi_risks
	Implements System.Collections.IEnumerable
	'**+Objective: Collection that supports the class 'multi_risk'.
	'**+Version: $$Revision: 4 $
	'+Objetivo: Colección que le da soporte a la clase 'multi_risk'.
	'+Version: $$Revision: 4 $
	
	'**+Objective: Local variable to hold collection.
	'+Objetivo: Variable Local para almacenar la colección.
	Private mcolmulti_risk As Collection
	
	'**%Objective: It adds an element to the collection.
	'**%Parameters:
	'**%    lclsmulti_risk -
	'%Objetivo: Agrega un elemento a la colección.
	'%Parámetros:
	'%    lclsmulti_risk -
	Public Function Add(ByRef lclsmulti_risk As multi_risk) As multi_risk

		'**-set the properties passed into the method
		mcolmulti_risk.Add(lclsmulti_risk)
		
		'**-return the object created
		Add = lclsmulti_risk
		lclsmulti_risk = Nothing
		
		Exit Function
	End Function
	
	'%Objetivo: Función que realiza la busqueda en la tabla 'multi_risk'.
	'%Parámetros:
	'%    sCertype -
	'%    nBranch -
	'%    nProduct -
	'%    nPolicy -
	'%    nCertif -
	Public Function Find(ByVal sCertype As String, ByVal nBranch As Short, ByVal nProduct As Short, ByVal nPolicy As Integer, ByVal nCertif As Integer, ByVal dEffecdate As Date, ByVal sCodispl As String) As Boolean
		Dim lclsmulti_risk As eRemoteDB.Execute
		Dim lclsmulti_riskItem As multi_risk
		Dim lclsPolicyFun As Object
		

		lclsmulti_risk = New eRemoteDB.Execute
		lclsPolicyFun = eRemoteDB.NetHelper.CreateClassInstance("eGeneral.Business_Functs")
		'+ Define all parameters for the stored procedures 'insudb.reamulti_risk'. Generated on 05/04/2005 05:23:00 p.m.
		With lclsmulti_risk
			.StoredProcedure = "reaMU001"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCodispl", sCodispl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(True) Then
				Do While Not .EOF
					lclsmulti_riskItem = New multi_risk
					lclsmulti_riskItem.sCertype = sCertype
					lclsmulti_riskItem.nBranch = nBranch
					lclsmulti_riskItem.nProduct = nProduct
					lclsmulti_riskItem.nPolicy = nPolicy
					lclsmulti_riskItem.nCertif = nCertif
					lclsmulti_riskItem.sComplCod = .FieldToClass("sComplCod")
					lclsmulti_riskItem.sDescBussi = .FieldToClass("sDescBussi")
					lclsmulti_riskItem.nConstcat = .FieldToClass("nConstcat")
					
					'Hallando las variables del giro de negocio
					If lclsmulti_riskItem.sComplCod <> String.Empty Then
						lclsmulti_riskItem.nBusinessty = lclsPolicyFun.getBusinessty(lclsmulti_riskItem.sComplCod)
						lclsmulti_riskItem.nCommergrp = lclsPolicyFun.getCommergrp(lclsmulti_riskItem.sComplCod)
						lclsmulti_riskItem.nCodkind = lclsPolicyFun.getCodkind(lclsmulti_riskItem.sComplCod)
					End If
					
					Call Add(lclsmulti_riskItem)
					lclsmulti_riskItem = Nothing
					.RNext()
				Loop 
				Find = True
				.RCloseRec()
			Else
				Find = False
			End If
		End With
		lclsmulti_risk = Nothing
		lclsPolicyFun = Nothing
		
		Exit Function
	End Function
	
	'**%Objective: This property is used when reference to an element becomes of the collection.
	'**%Parameters:
	'**%    vIndexKey   -
	'%Objetivo: Esta propiedad es usada cuando se hace referencia a un elemento de la colección.
	'%Parámetros:
	'%    vIndexKey -
	Default Public ReadOnly Property Item(ByVal vIndexKey As Object) As multi_risk
		Get

			Item = mcolmulti_risk.Item(vIndexKey)
			
			Exit Property
		End Get
	End Property
	
	'**%Objective: It returns the amount of existing elements in the collection.
	'%Objetivo: Retorna la contidad de elementos existentes en la colección.
	Public ReadOnly Property Count() As Integer
		Get

			Count = mcolmulti_risk.Count()
			
			Exit Property
		End Get
	End Property
	
	'**%Objective: This property allows you to enumerate this collection with the "For...Each".
	'%Objetivo: Esta propiedad permite enumerar los elementos de la colección por medio de un "For...Each".
	'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
	'Public ReadOnly Property NewEnum() As stdole.IUnknown
		'Get
    '
			'NewEnum = mcolmulti_risk._NewEnum
			'
			'Exit Property
'ErrorHandler: '
			'ProcError("multi_risks.NewEnum()")
		'End Get
	'End Property
	
	Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
				GetEnumerator = mcolmulti_risk.GetEnumerator
	End Function
	
	'**%Objective: It allows to remove an element of the collection.
	'**%Parameters:
	'**%    vIndexKey   -
	'%Objetivo: Permite eliminar un elemento de la colección.
	'%Parámetros:
	'%    vIndexKey -
	Public Sub Remove(ByRef vIndexKey As Object)

		mcolmulti_risk.Remove(vIndexKey)
		
		Exit Sub
	End Sub
	
	'**%Objective: Creates the collection when this class is created.
	'%Objetivo: Crea la colección cuando se crea esta clase.
	Private Sub Class_Initialize_Renamed()

		mcolmulti_risk = New Collection
		
		Exit Sub
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'**%Objective: Destroys collection when this class is terminated.
	'%Objetivo: Destruye la colección cuando se termina esta clase.
	Private Sub Class_Terminate_Renamed()

		mcolmulti_risk = Nothing
		
		Exit Sub
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
End Class











