Option Strict Off
Option Explicit On
Public Class Thefts
	Implements System.Collections.IEnumerable
	'**+Objective: Collection that supports the class 'Theft'.
	'**+Version: $$Revision: 2 $
	'+Objetivo: Colección que le da soporte a la clase 'Theft'.
	'+Version: $$Revision: 2 $
	
	'**+Objective: Local variable to hold collection.
	'+Objetivo: Variable Local para almacenar la colección.
	Private mcolTheft As Collection
	'**%Objective: It adds an element to the collection.
	'**%Parameters:
	'**%    lclsTheft - class theft
	'%Objetivo: Agrega un elemento a la colección.
	'%Parámetros:
	'%    lclsTheft - clase theft
	Public Function Add(ByRef lclsTheft As Theft) As Theft
		
		'**-set the properties passed into the method

		mcolTheft.Add(lclsTheft)
		
		'**-return the object created
		Add = lclsTheft
		
		Exit Function
	End Function
	
	'**%Objective: Function that makes the search in the table 'Theft'.
	'**%Parameters:
	'**%    scertype   - type of registry
	'**%    nbranch    - number of branch
	'**%    nproduct   - number of product
	'**%    npolicy    - number of policy
	'**%    ncertif    - number of certificate
	'**%    deffecdate - effect date
	'%Objetivo: Función que realiza la busqueda en la tabla 'Theft'.
	'%Parámetros:
	'%    scertype    - Tipo de registro
	'%    nbranch     - Código del ramo
	'%    nproduct    - Código del producto
	'%    npolicy     - Código de la poliza
	'%    ncertif     - Código del certificado
	'%    deffecdate  - Fecha de efecto del registro
	Public Function Find(ByVal sCertype As String, ByVal nBranch As Short, ByVal nProduct As Short, ByVal nPolicy As Integer, ByVal nCertif As Integer, ByVal dEffecDate As Date, ByVal sCodispl As String) As Boolean
		Dim lclsTheft As eRemoteDB.Execute
		Dim lclsTheftItem As Theft
        Dim lclsBusinessFun As Object
        Dim lclsRoles As New ePolicy.Roles
		

        lclsTheft = New eRemoteDB.Execute
        lclsBusinessFun = eRemoteDB.NetHelper.CreateClassInstance("eGeneral.Business_Functs")
		
		'+ Define all parameters for the stored procedures 'insudb.reaTheft'. Generated on 18/06/2004 01:51:53 p.m.
		With lclsTheft
			.StoredProcedure = "reaTheft_a"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCodispl", sCodispl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(True) Then
				Do While Not .EOF
					lclsTheftItem = New Theft
					lclsTheftItem.sCertype = sCertype
					lclsTheftItem.nBranch = nBranch
					lclsTheftItem.nProduct = nProduct
					lclsTheftItem.nPolicy = nPolicy
					lclsTheftItem.nCertif = nCertif
					lclsTheftItem.nInsured = .FieldToClass("nInsured")
					lclsTheftItem.nEmployees = .FieldToClass("nEmployees")
					lclsTheftItem.nArea = .FieldToClass("nArea")
					lclsTheftItem.nVigilance = .FieldToClass("nVigilance")
					lclsTheftItem.sComplCod = .FieldToClass("sComplCod")
					lclsTheftItem.sDescBussi = .FieldToClass("sDescBussi")
					lclsTheftItem.nConstCat = .FieldToClass("nConstcat")
                    If lclsTheftItem.sComplCod <> String.Empty Then
                        lclsTheftItem.nBusinessty = lclsBusinessFun.getBusinessty(lclsTheftItem.sComplCod)
                        lclsTheftItem.nCommergrp = lclsBusinessFun.getCommergrp(lclsTheftItem.sComplCod)
                        lclsTheftItem.nCodkind = lclsBusinessFun.getCodkind(lclsTheftItem.sComplCod)
                        'Else
                        '    If lclsRoles.Find(sCertype, nBranch, nProduct, nPolicy, nCertif, Roles.eRoles.eRolContratanting, "0", dEffecDate) Then
                        '        If lclsRoles.sComplCod <> String.Empty Then
                        '            lclsTheftItem.nBusinessty = lclsBusinessFun.getBusinessty(lclsRoles.sComplCod)
                        '            lclsTheftItem.nCommergrp = lclsBusinessFun.getCommergrp(lclsRoles.sComplCod)
                        '            lclsTheftItem.nCodkind = lclsBusinessFun.getCodkind(lclsRoles.sComplCod)
                        '        End If
                        '    End If
                    End If
                    Call Add(lclsTheftItem)
                    lclsTheftItem = Nothing
                    .RNext()
                Loop
				Find = True
				.RCloseRec()
			Else
				Find = False
			End If
		End With
		
		lclsBusinessFun = Nothing
		lclsTheft = Nothing
        lclsTheftItem = Nothing
        lclsRoles = Nothing
		
		Exit Function
	End Function
	
	'**%Objective: This property is used when reference to an element becomes of the collection.
	'**%Parameters:
	'**%    vIndexKey   -
	'%Objetivo: Esta propiedad es usada cuando se hace referencia a un elemento de la colección.
	'%Parámetros:
	'%    vIndexKey -
	Default Public ReadOnly Property Item(ByVal vIndexKey As Object) As Theft
		Get

			Item = mcolTheft.Item(vIndexKey)
			
			Exit Property
		End Get
	End Property
	
	'**%Objective: It returns the amount of existing elements in the collection.
	'%Objetivo: Retorna la contidad de elementos existentes en la colección.
	Public ReadOnly Property Count() As Integer
		Get

			Count = mcolTheft.Count()
			
			Exit Property
		End Get
	End Property
	
	'**%Objective: This property allows you to enumerate this collection with the "For...Each".
	'%Objetivo: Esta propiedad permite enumerar los elementos de la colección por medio de un "For...Each".
	'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
	'Public ReadOnly Property NewEnum() As stdole.IUnknown
		'Get
			'
			'NewEnum = mcolTheft._NewEnum
			'
			'Exit Property
'ErrorHandler: '
			'ProcError("Thefts.NewEnum()")
		'End Get
	'End Property
	
	Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
				GetEnumerator = mcolTheft.GetEnumerator
	End Function
	
	'**%Objective: It allows to remove an element of the collection.
	'**%Parameters:
	'**%    vIndexKey   -
	'%Objetivo: Permite eliminar un elemento de la colección.
	'%Parámetros:
	'%    vIndexKey -
	Public Sub Remove(ByRef vIndexKey As Object)

		mcolTheft.Remove(vIndexKey)
		
		Exit Sub
	End Sub
	
	'**%Objective: Creates the collection when this class is created.
	'%Objetivo: Crea la colección cuando se crea esta clase.
	Private Sub Class_Initialize_Renamed()

		mcolTheft = New Collection
		
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











