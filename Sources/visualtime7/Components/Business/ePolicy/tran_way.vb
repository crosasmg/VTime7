Option Strict Off
Option Explicit On
Public Class tran_way
	'**+Objective: Class that supports the table Execute it's content is:
	'**+Version: $$Revision: 2 $
	'+Objetivo: Clase que le da soporte a la tabla Execute cuyo contenido es:
	'+Version: $$Revision: 2 $
	
	'**+Objective: Properties according to the table 'tran_way' in the system 06/07/2004 04:02:42 p.m.
	'+Objetivo: Propiedades según la tabla 'tran_way' en el sistema 06/07/2004 04:02:42 p.m.
	Public sCertype As String
	Public nBranch As Short
	Public nProduct As Short
	Public nPolicy As Integer
	Public nCertif As Integer
	Public nWay As Short
	Public sName_licen As String
	Public sDescript As String
	Public nNotenum As Integer
	
	
	
	'**%Objective: Add a record to the table "tran_way"
	'**%Parameters:
	'**%    nUsercode   -  code of user
	'**%    sCertype    -  type of registry
	'**%    nBranch     -  code of branch
	'**%    nProduct    -  code of product
	'**%    nPolicy     -  number of poliza
	'**%    nCertif     -  number of certificate
	'**%    dEffecdate  -  date of effect of the registry
	'**%    nWay        -  code of  the transportation mode
	'**%    sName_licen -  name or license plate
	'**%    sDescript   -  description of the transportation mode
	'**%    nNotenum    -  number of the note containing the comments.
	'%Objetivo: Agrega un registro a la tabla "tran_way"
	'%Parámetros:
	'%      nUsercode   -   código del usuario
	'%      sCertype    -   tipo de registro
	'%      nBranch     -   código del ramo
	'%      nProduct    -   código del producto
	'%      nPolicy     -   numero de poliza
	'%      nCertif     -   numero de certificado
	'%      dEffecdate  -   fecha de efecto del registro
	'%      nWay        -   código de tipo de transporte
	'%      sName_licen -   nombre o matrícula del medio de transporte.
	'%      sDescript   -   descripción el medio de transporte
	'%      nNotenum    -   número de la nota que contiene el texto libre.
	Private Function Add(ByVal nUsercode As Integer, ByVal sCertype As String, ByVal nBranch As Short, ByVal nProduct As Short, ByVal nPolicy As Integer, ByVal nCertif As Integer, ByVal dEffecdate As Date, ByVal nWay As Short, ByVal sName_licen As String, ByVal sDescript As String, ByVal nNotenum As Integer) As Boolean
		Dim lclstran_way As eRemoteDB.Execute
		

        lclstran_way = New eRemoteDB.Execute
		
		'+ Define all parameters for the stored procedures 'insudb.cretran_way'. Generated on 06/07/2004 04:02:42 p.m.
		
		With lclstran_way
'PENDING: Procedure not found
			.StoredProcedure = "cretran_way"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecDate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nWay", nWay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sName_licen", sName_licen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDescript", sDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNoteNum", nNotenum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Add = .Run(False)
		End With
		
		lclstran_way = Nothing
		
		Exit Function
	End Function
	
	'**%Objective: Updates a registry to the table "tran_way" using the key for this table.
	'**%Parameters:
	'**%    nUsercode   -  code of user
	'**%    sCertype    -  type of registry
	'**%    nBranch     -  code of branch
	'**%    nProduct    -  code of product
	'**%    nPolicy     -  number of poliza
	'**%    nCertif     -  number of certificate
	'**%    dEffecdate  -  date of effect of the registry
	'**%    nWay        -  code of  the transportation mode
	'**%    sName_licen -  name or license plate
	'**%    sDescript   -  description of the transportation mode
	'**%    nNotenum    -  number of the note containing the comments.
	'%Objetivo: Actualiza un registro a la tabla "tran_way" usando la clave para dicha tabla.
	'%Parámetros:
	'%      nUsercode   -   código del usuario
	'%      sCertype    -   tipo de registro
	'%      nBranch     -   código del ramo
	'%      nProduct    -   código del producto
	'%      nPolicy     -   numero de poliza
	'%      nCertif     -   numero de certificado
	'%      dEffecdate  -   fecha de efecto del registro
	'%      nWay        -   código de tipo de transporte
	'%      sName_licen -   nombre o matrícula del medio de transporte.
	'%      sDescript   -   descripción el medio de transporte
	'%      nNotenum    -   número de la nota que contiene el texto libre.
	Private Function Update(ByVal nUsercode As Integer, ByVal sCertype As String, ByVal nBranch As Short, ByVal nProduct As Short, ByVal nPolicy As Integer, ByVal nCertif As Integer, ByVal dEffecdate As Date, ByVal nWay As Short, ByVal sName_licen As String, ByVal sDescript As String, ByVal nNotenum As Integer) As Boolean
		Dim lclstran_way As eRemoteDB.Execute
		

        lclstran_way = New eRemoteDB.Execute
		
		'+ Define all parameters for the stored procedures 'insudb.updtran_way'. Generated on 06/07/2004 04:02:42 p.m.
		With lclstran_way
			.StoredProcedure = "insupdtran_way"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecDate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nWay", nWay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sName_licen", sName_licen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDescript", sDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNoteNum", nNotenum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Update = .Run(False)
		End With
		
		lclstran_way = Nothing
		
		Exit Function
	End Function
	
	'**%Objective: Delete a registry the table "tran_way" using the key for this table.
	'**%Parameters:
	'**%   sCertype    -  type of registry
	'**%   nBranch     -  code of branch
	'**%   nProduct    -  code of product
	'**%   nPolicy     -  number of poliza
	'**%   nCertif     -  number of certificate
	'**%   dEffecdate  -  date of effect of the registry
	'**%   nWay        -  code of  the transportation mode
	'**%   nUsercode   -  code of user
	'%Objetivo: Elimina un registro a la tabla "tran_way" usando la clave para dicha tabla.
	'%Parámetros:
	'%     sCertype    -   tipo de registro
	'%     nBranch     -   código del ramo
	'%     nProduct    -   código del producto
	'%     nPolicy     -   numero de poliza
	'%     nCertif     -   numero de certificado
	'%     dEffecdate  -   fecha de efecto del registro
	'%     nWay        -   código de tipo de transporte
	'%     nUsercode   -   código del usuario
	Private Function Delete(ByVal sCertype As String, ByVal nBranch As Short, ByVal nProduct As Short, ByVal nPolicy As Integer, ByVal nCertif As Integer, ByVal dEffecdate As Date, ByVal nWay As Short, ByVal nUsercode As Integer) As Boolean
		Dim lclstran_way As eRemoteDB.Execute
		

        lclstran_way = New eRemoteDB.Execute
		
		'+ Define all parameters for the stored procedures 'insudb.deltran_way'. Generated on 06/07/2004 04:02:42 p.m.
		With lclstran_way
			.StoredProcedure = "deltran_way"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecDate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nWay", nWay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Delete = .Run(False)
		End With
		
		lclstran_way = Nothing
		
		Exit Function
	End Function
	
	'**%Objective: It verifies the existence of a registry in table "tran_way" using the key of this table.
	'**%Parameters:
	'**%   sCertype    -  type of registry
	'**%   nBranch     -  code of branch
	'**%   nProduct    -  code of product
	'**%   nPolicy     -  number of poliza
	'**%   nCertif     -  number of certificate
	'**%   dEffecdate  -  date of effect of the registry
	'**%   nWay        -  code of  the transportation mode
	'%Objetivo: Verifica la existencia de un registro en la tabla "tran_way" usando la clave de dicha tabla.
	'%Parámetros:
	'%     sCertype    -   tipo de registro
	'%     nBranch     -   código del ramo
	'%     nProduct    -   código del producto
	'%     nPolicy     -   numero de poliza
	'%     nCertif     -   numero de certificado
	'%     dEffecdate  -   fecha de efecto del registro
	'%     nWay        -   código de tipo de transporte
	Private Function IsExist(ByVal sCertype As String, ByVal nBranch As Short, ByVal nProduct As Short, ByVal nPolicy As Integer, ByVal nCertif As Integer, ByVal dEffecdate As Date, ByVal nWay As Short) As Boolean
		Dim lclstran_way As eRemoteDB.Execute
		Dim lintExist As Short
		

        lclstran_way = New eRemoteDB.Execute
		lintExist = 0
		
		'+ Define all parameters for the stored procedures 'insudb.valtran_wayExist'. Generated on 06/07/2004 04:02:42 p.m.
		With lclstran_way
			.StoredProcedure = "reatran_way_v"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecDate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nWay", nWay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExist", lintExist, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				IsExist = (.Parameters("nExist").Value = 1)
			Else
				IsExist = False
			End If
		End With
		
		lclstran_way = Nothing
		
		Exit Function
	End Function
	
	'**%Objective: Validation of the data for the page details.
	'**%Parameters:
	'**%    sCodispl     - Logical code that identifies the transaction.
	'**%    nMainAction  - Action being executed on the transaction.
	'**%    sAction      - Action begin executed on the grid of the transaction
	'**%    sCertype     - Type of record
	'**%    nBranch      - Code of the line of business
	'**%    nProduct     - Code of the product
	'**%    nPolicy      - Number of the policy
	'**%    nCertif      - Number identifying the certificate
	'**%    dEffecdate   - Effective date of the record
	'**%    nWay         - code of  the transportation mode
	'**%    sName_licen  - name or license plate
	'**%    sDescript    - description of the transportation mode
	'**%    nNotenum     - number of the note containing the comments.
	'%Objetivo: Validación de los datos para la página detalle.
	'%Parámetros:
	'%    sCodispl     - Código lógico que identifica la transacción.
	'%    nMainAction  - Acción que se ejecuta sobre la transacción.
	'%    sAction      - Acción que se ejecuta sobre el grid de la transacción
	'%    sCertype     - Tipo de registro
	'%    nBranch      - Código de la línea del negocio
	'%    nProduct     - Código del producto
	'%    nPolicy      - Número de la póliza
	'%    nCertif      - Número que identifica el certificado
	'%    dEffecdate   - Fecha de efecto del registro
	'%    nWay         - código de tipo de transporte
	'%    sName_licen  - nombre o matrícula del medio de transporte.
	'%    sDescript    - descripción el medio de transporte
	'%    nNotenum     - número de la nota que contiene el texto libre.
    Public Function InsValTR004(ByVal sCodispl As String, _
                                ByVal nMainAction As Integer, _
                                ByVal sAction As String, _
                                ByVal sCertype As String, _
                                ByVal nBranch As Short, _
                                ByVal nProduct As Short, _
                                ByVal nPolicy As Integer, _
                                ByVal nCertif As Integer, _
                                ByVal dEffecdate As Date, _
                                ByVal nWay As Short, _
                                ByVal sName_licen As String, _
                                ByVal sDescript As String, _
                                ByVal nNotenum As Integer) As String

        Dim lclsErrors As eFunctions.Errors
        Dim lclstran_way As tran_way
        Dim lcolTran_ways As tran_ways


        lclsErrors = New eFunctions.Errors
        lclstran_way = New tran_way
        lcolTran_ways = New tran_ways

        If sName_licen = String.Empty Then
            Call lclsErrors.ErrorMessage(sCodispl, 38009)
        End If

        If sDescript = String.Empty Then
            Call lclsErrors.ErrorMessage(sCodispl, 10071)
        End If

        If lcolTran_ways.Find(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate) Then
            For Each lclstran_way In lcolTran_ways
                If Trim(UCase(lclstran_way.sName_licen)) = Trim(UCase(sName_licen)) And lclstran_way.nWay <> nWay Then
                    Call lclsErrors.ErrorMessage(sCodispl, 38011)
                    Exit For
                End If
            Next lclstran_way
        End If

        InsValTR004 = lclsErrors.Confirm

        lclsErrors = Nothing
        lclstran_way = Nothing
        lcolTran_ways = Nothing

        Exit Function
    End Function
	
	'**%Objective: Pass of the information introduced towards the layers of rules of business and access of data.
	'**%Parameters:
	'**%    nHeader      - Indicator of the zone (Header or detail)
	'**%    sCodispl     - Logical code that identifies the transaction.
	'**%    nMainAction  - Action being executed on the transaction.
	'**%    sAction      - Action begin executed on the grid of the transaction
	'**%    nUsercode    - Code of user
	'**%    sCertype     - Type of record
	'**%    nBranch      - Code of the line of business
	'**%    nProduct     - Code of the product
	'**%    nPolicy      - Number of the policy
	'**%    nCertif      - Number identifying the certificate
	'**%    dEffecdate   - Effective date of the record
	'**%    nWay         - code of  the transportation mode
	'**%    sName_licen  - name or license plate
	'**%    sDescript    - description of the transportation mode
	'**%    nNotenum     - number of the note containing the comments.
	'%Objetivo: Se encarga de llevar la información introducida hacia las capas de reglas de negocio y acceso de datos.
	'%Parámetros:
	'*    nHeader      - Indicador de zona de encabezado o detalle
	'%    sCodispl     - Código lógico que identifica la transacción.
	'%    nMainAction  - Acción que se ejecuta sobre la transacción.
	'%    sAction      - Acción que se ejecuta sobre el grid de la transacción
	'%    nUsercode   -  Código del usuario
	'%    sCertype     - Tipo de registro
	'%    nBranch      - Código de la línea del negocio
	'%    nProduct     - Código del producto
	'%    nPolicy      - Número de la póliza
	'%    nCertif      - Número que identifica el certificado
	'%    dEffecdate   - Fecha de efecto del registro
	'%    nWay         - código de tipo de transporte
	'%    sName_licen  - nombre o matrícula del medio de transporte.
	'%    sDescript    - descripción el medio de transporte
	'%    nNotenum     - número de la nota que contiene el texto libre.
	Public Function InsPostTR004(ByVal nHeader As Boolean, ByVal sCodispl As String, ByVal nMainAction As Integer, ByVal sAction As String, ByVal nUsercode As Integer, ByVal sCertype As String, ByVal nBranch As Short, ByVal nProduct As Short, ByVal nPolicy As Integer, ByVal nCertif As Integer, ByVal dEffecdate As Date, ByVal nWay As Short, ByVal sName_licen As String, ByVal sDescript As String, ByVal nNotenum As Integer) As Boolean
		
		Dim lclsPolicyWin As ePolicy.Policy_Win
		

        If sAction = "Del" Then
            InsPostTR004 = Delete(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nWay, nUsercode)
        Else
            InsPostTR004 = Update(nUsercode, sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nWay, sName_licen, sDescript, nNotenum)
        End If
		
		If InsPostTR004 Then
			lclsPolicyWin = New ePolicy.Policy_Win
			Call lclsPolicyWin.Add_PolicyWin(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, "TR004", "2")
			lclsPolicyWin = Nothing
		End If
		
		Exit Function
	End Function
End Class











