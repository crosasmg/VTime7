Option Strict Off
Option Explicit On
Public Class Civils
	Implements System.Collections.IEnumerable
	'**+Objective: Collection that supports the class 'Civil'.
	'**+Version: $$Revision: 2 $
	'+Objetivo: Colección que le da soporte a la clase 'Civil'.
	'+Version: $$Revision: 2 $
	
	'**+Objective: Local variable to hold collection.
	'+Objetivo: Variable Local para almacenar la colección.
	Private mcolCivil As Collection
	
	
	
	'**%Objective: It adds an element to the collection.
	'**%Parameters:
	'**%    lclsCivil -
	'%Objetivo: Agrega un elemento a la colección.
	'%Parámetros:
	'%    lclsCivil -
	Public Function Add(ByRef lclsCivil As Civil) As Civil
		
		'**-set the properties passed into the method

		mcolCivil.Add(lclsCivil)
		
		'**-return the object created
		Add = lclsCivil
		
		Exit Function
	End Function
	
	'**%Objective: Function that makes the search in the table 'Civil'.
	'**%Parameters:
	'**%    sCertype   - tipo de poliza/cotización/ propuesta.
	'**%    nBranch    - code of the branch
	'**%    nProduct   - code of the product
	'**%    nPolicy    - code of the policy
	'**%    nCertif    - code of the Certificat
	'**%    dEffecdate - effective date of the record
	'%Objetivo: Función que realiza la busqueda en la tabla 'Civil'.
	'%Parámetros:
	'%    sCertype   - tipo de poliza/cotización/ propuesta.
	'%    nBranch    - código del ramo
	'%    nProduct   - codigo del producto
	'%    nPolicy    - código de la poliza
	'%    nCertif    - código del certificado
	Public Function Find(ByVal sCertype As String, ByVal nBranch As Short, ByVal nProduct As Short, ByVal nPolicy As Integer, ByVal nCertif As Integer, ByVal dEffecDate As Date, ByVal sCodispl As String) As Boolean
		Dim lclsCivil As eRemoteDB.Execute
		Dim lclsCivilItem As Civil
		Dim lclsBusinessFun As Object
		
        lclsCivil = New eRemoteDB.Execute
		lclsBusinessFun = eRemoteDB.NetHelper.CreateClassInstance("eGeneral.Business_Functs")
		'+ Define all parameters for the stored procedures 'insudb.reaCivil'. Generated on 14/06/2004 11:08:40 a.m.
		With lclsCivil
			.StoredProcedure = "reacivil_a"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCodispl", sCodispl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(True) Then
				Do While Not .EOF
					lclsCivilItem = New Civil
					lclsCivilItem.sCertype = sCertype
					lclsCivilItem.nBranch = nBranch
					lclsCivilItem.nProduct = nProduct
					lclsCivilItem.nPolicy = nPolicy
					lclsCivilItem.nCertif = nCertif
					lclsCivilItem.nUnit_type = .FieldToClass("nUnit_type")
					lclsCivilItem.nUnit_quan = .FieldToClass("nUnit_quan")
					lclsCivilItem.sComplCod = .FieldToClass("sComplCod")
					lclsCivilItem.sDescBussi = .FieldToClass("sDescBussi")
					lclsCivilItem.nConstCat = .FieldToClass("nConstcat")
					'+ Si tiene giro de negocio recupera los valores de Tipo, Grupo y Giro a partir del código completo
					If lclsCivilItem.sComplCod <> String.Empty Then
						lclsCivilItem.nBusinessty = lclsBusinessFun.getBusinessty(lclsCivilItem.sComplCod)
						lclsCivilItem.nCommergrp = lclsBusinessFun.getCommergrp(lclsCivilItem.sComplCod)
						lclsCivilItem.nCodkind = lclsBusinessFun.getCodkind(lclsCivilItem.sComplCod)
					End If
					Call Add(lclsCivilItem)
					lclsCivilItem = Nothing
					.RNext()
				Loop 
				Find = True
				.RCloseRec()
			Else
				Find = False
			End If
		End With
		
		lclsBusinessFun = Nothing
		lclsCivil = Nothing
		lclsCivilItem = Nothing
		
		Exit Function
	End Function
	
	'**%Objective: This property is used when reference to an element becomes of the collection.
	'**%Parameters:
	'**%    vIndexKey   -
	'%Objetivo: Esta propiedad es usada cuando se hace referencia a un elemento de la colección.
	'%Parámetros:
	'%    vIndexKey -
	Default Public ReadOnly Property Item(ByVal vIndexKey As Object) As Civil
		Get

			Item = mcolCivil.Item(vIndexKey)
			
			Exit Property
		End Get
	End Property
	
	'**%Objective: It returns the amount of existing elements in the collection.
	'%Objetivo: Retorna la contidad de elementos existentes en la colección.
	Public ReadOnly Property Count() As Integer
		Get

			Count = mcolCivil.Count()
			
			Exit Property
		End Get
	End Property
	
	'**%Objective: This property allows you to enumerate this collection with the "For...Each".
	'%Objetivo: Esta propiedad permite enumerar los elementos de la colección por medio de un "For...Each".
	'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
	'Public ReadOnly Property NewEnum() As stdole.IUnknown
		'Get
    '
			'NewEnum = mcolCivil._NewEnum
			'
			'Exit Property
'ErrorHandler: '
			'ProcError("Civils.NewEnum()")
		'End Get
	'End Property
	
	Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
				GetEnumerator = mcolCivil.GetEnumerator
	End Function
	
	'**%Objective: It allows to remove an element of the collection.
	'**%Parameters:
	'**%    vIndexKey   -
	'%Objetivo: Permite eliminar un elemento de la colección.
	'%Parámetros:
	'%    vIndexKey -
	Public Sub Remove(ByRef vIndexKey As Object)

		mcolCivil.Remove(vIndexKey)
		
		Exit Sub
	End Sub
	
	'**%Objective: Creates the collection when this class is created.
	'%Objetivo: Crea la colección cuando se crea esta clase.
	Private Sub Class_Initialize_Renamed()

		mcolCivil = New Collection
		
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
