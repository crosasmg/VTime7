Option Strict Off
Option Explicit On
Public Class Tran_stage
	'**+Objective: Class that supports the table tran_stage.
	'**+Version: $$Revision: 4 $
	'+Objetivo: Clase que le da soporte a la tabla tran_stage.
	'+Version: $$Revision: 4 $
	
	'**-Objective: Type of record
	'-Objetivo: Tipo de registro
	Public sCertype As String
	
	'**-Objective: Code of the line of business
	'-Objetivo: Código del ramo
	Public nBranch As Integer
	
	'**-Objective: Code of the product
	'-Objetivo: Código del producto
	Public nProduct As Integer
	
	'**-Objective: Number identifying the policy
	'-Objetivo: Número que identifica la póliza
	Public nPolicy As Double
	
	'**-Objective: Number identifying the certificate
	'-Objetivo: Número que identifica el certificado
	Public nCertif As Double
	
	'**-Objective: Number identifying the Stage
	'-Objetivo: Numero identificativo de la etapa
	Public nStage As Integer
	
	'**-Objective: Effective date of the record
	'-Objetivo: Fecha de efecto del registro
	Public dEffecdate As Date
	
	'**-Objective: Anullment date of the record
	'-Objetivo: Fecha de anulación del registro
	Public dNullDate As Date
	
	'**-Objective: Arrival date to the place of destination
	'-Objetivo: Fecha de llegada al lugar de destino
	Public dDestindat As Date
	
	'**-Objective: Arrival time to the place of destination
	'-Objetivo: Hora de llegada al lugar de destino
	Public sDestinhou As String
	
	'**-Objective: Departure date from the place of origin
	'-Objetivo: Fecha de salida del lugar de origen
	Public dOrigindat As Date
	
	'**-Objective: Departure time from the place of origin
	'-Objetivo: Hora de salida del lugar de origen
	Public sOriginhou As String
	
	'**-Objective: Type of route
	'-Objetivo: Tipo de ruta
	Public nRoute As Integer
	
	'**-Objective: Date which from the record is valid in the table Tran_route
	'-Objetivo: Fecha de efecto del registro en la tabla Tran_route
	Public dEfd_tran_route As Integer
	
	'**-Objective: Code of the user executing the transaction
	'-Objetivo: Código del usuario  que ejecuta la transacción
	Public nUsercode As Integer
	
	'**-Objective: Key identifying the Transportation Mode
	'-Objetivo: Nombre o matrícula del medio de transporte
	Public sName_licen As String
	
	'**-Objective: Assured amount  ot the transported  merchandise
	'-Objetivo: Monto asegurado de la mercancía transportada
	Public nAmount As Double
	
	'**-Objective: Amount of Franchise/Deductible of the transported merchandise
	'-Objetivo: Monto de franquicia/deducible de la mercancía transportada
	Public nFrandedi As Double
	
	'**-Objective: City of origin of the route.
	'-Objetivo: Ciudad de origen de la ruta
	Public sOrigen As String
	
	'**-Objective: City of destination of the route.
	'-Objetivo: Ciudad de Destino de la ruta
	Public sDestination As String
	
	'**-Objective: Type of route ensured
	'-Objetivo: Tipo de ruta asegurada
	Public nTypRoute As String
	
	'**-Objective: Type of transport
	'-Objetivo: Tipo de Transporte
    Public nTransptype As String

    Public sPurchase_Order As String
    Public sApplicationNumber As String
	
	
	'**%Objective: This method updates or adds a record into the table tran_stage
	'**%Parameters:
	'**%    sAction      - The type of action to be executed for the record ("Add" or "Update")
	'**%    sPolitype    - Policy type
	'**%    nUsercode    - Code of the user that creates or updates the record.
	'**%    sCertype     - Type of record
	'**%    nBranch      - Code of the line of business
	'**%    nProduct     - Code of the product
	'**%    nPolicy      - Number identifying the policy
	'**%    nCertif      - Number identifying the certificate
	'**%    nStage       - Number identifying the stage
	'**%    dEffecdate   - Effective date of the record
	'**%    dDestindat   - Arrival date to the place of destination
	'**%    dOrigindat   - Departure date from the place of origin
	'**%    nRoute       - type of coveraged route
	'**%    sName_licen  - Key identifying the Transportation Mode
	'**%    sOrigen      - City of origin of the route
	'**%    sDestination - City of destination of the route
	'%Objetivo: Este método permite agregar o actualizar un registro en la tabla tran_stage
	'%Parámetros:
	'%    sAction      - Indica el tipo de acción a ejecutar sobre el registro en la tabla ("Insertar" o "Actualizar").
	'%    sPolitype    - Tipo de póliza
	'%    nUsercode    - Código del usuario que crea o actualiza el registro.
	'%    sCertype     - Tipo de registro
	'%    nBranch      - Código del ramo
	'%    nProduct     - Código del producto
	'%    nPolicy      - Número que identifica la póliza
	'%    nCertif      - Número que identifica el certificado
	'%    nStage       - Número que identifica la etapa
	'%    dEffecdate   - Fecha efectiva del registro
	'%    dDestindat   - Fecha de llegada al lugar de destino
	'%    dOrigindat   - Fecha de salida del lugar de origen
	'%    nRoute       - Tipo de ruta asegurada
	'%    sName_licen  - Nombre o matrícula del medio de transporte
	'%    sOrigen      - Ciudad origen de la ruta
	'%    sDestination - Ciudad de destino de la ruta
    Private Function AddUpdate(ByVal sAction As String, ByVal sPolitype As String, ByVal nUsercode As Integer, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nStage As Integer, ByVal dEffecdate As Date, ByVal dDestindat As Date, ByVal dOrigindat As Date, ByVal nRoute As Integer, ByVal sName_licen As String, ByVal sOrigen As String, ByVal sDestination As String, ByVal sPurchase_Order As String, ByVal sApplicationNumber As String) As Boolean
        Dim lclsTran_stage As eRemoteDB.Execute


        lclsTran_stage = New eRemoteDB.Execute

        With lclsTran_stage
            .StoredProcedure = "insupdTran_stage"
            .Parameters.Add("sAction", sAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sPolitype", sPolitype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nStage", nStage, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dDestindat", dDestindat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dOrigindat", dOrigindat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("NROUTEAUX", nRoute, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sName_licen", sName_licen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sOrigen", sOrigen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sDestination", sDestination, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sPurchase_Order", sPurchase_Order, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sApplicationNumber", sApplicationNumber, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)


            AddUpdate = .Run(False)
        End With
        lclsTran_stage = Nothing

        Exit Function
    End Function
	
	'**%Objective: Deletes a record from the table Tran_stage by using the table's key.
	'**%Parameters:
	'**%    sCertype   - Type of record
	'**%    nBranch    - Code of the line of business
	'**%    nProduct   - Code of the product
	'**%    nPolicy    - Number identifying the policy
	'**%    nCertif    - Number identifying the certificate
	'**%    dEffecdate - Effective date of the record
	'**%    nStage     - Number identifying the Stage
	'**%    nUsercode  - Code of the user deleting the record
	'%Objetivo: Este método permite eliminar un registro de la tabla "Tran_stage" a través de la clave de dicha tabla.
	'%Parámetros:
	'%    sCertype   - Tipo de registro
	'%    nBranch    - Código del ramo
	'%    nProduct   - Código del producto
	'%    nPolicy    - Número que identifica la póliza
	'%    nCertif    - Número que identifica el certificado
	'%    dEffecdate - Fecha efectiva del registro
	'%    nStage     - Número que identifica la etapa
	'%    nUsercode  - Código del usuario que borra el registro
	Private Function Delete(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nStage As Integer, ByVal nUsercode As Integer) As Boolean
		Dim lclsTran_stage As eRemoteDB.Execute
		

        lclsTran_stage = New eRemoteDB.Execute
		
		With lclsTran_stage
			.StoredProcedure = "delTran_stage"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nStage", nStage, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Delete = .Run(False)
		End With
		
		lclsTran_stage = Nothing
		
		Exit Function
	End Function
	
	'**%Objective: This method obtains the information from the table Tran_stage.
	'**%Parameters:
	'**%    sCertype   - Type of record
	'**%    nBranch    - Code of the line of business
	'**%    nProduct   - Code of the product
	'**%    nPolicy    - Number identifying the policy
	'**%    nCertif    - Number identifying the certificate
	'**%    dEffecdate - Effective date of the record
	'%Objetivo: Este método realiza la lectura de la información de la tabla en tratamiento Tran_stage.
	'%Parámetros:
	'%    sCertype   - Tipo de registro
	'%    nBranch    - Código del ramo
	'%    nProduct   - Código del producto
	'%    nPolicy    - Número que identifica la póliza
	'%    nCertif    - Número que identifica el certificado
	'%    dEffecdate - Fecha efectiva del registro
    Public Function Find(ByVal sCertype As String, _
                         ByVal nBranch As Integer, _
                         ByVal nProduct As Integer, _
                         ByVal nPolicy As Double, _
                         ByVal nCertif As Double, _
                         ByVal nCurrency As Short, _
                         ByVal dEffecdate As Date) As Boolean

        Dim lclsTran_stage As eRemoteDB.Execute


        lclsTran_stage = New eRemoteDB.Execute

        With lclsTran_stage
            .StoredProcedure = "reaTran_stage_a"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(True) Then
                'Me.nStage = .FieldToClass("nStage")
                'Me.dDestindat = .FieldToClass("dDestindat")
                'Me.sDestinhou = .FieldToClass("sDestinhou")
                'Me.dOrigindat = .FieldToClass("dOrigindat")
                'Me.sOriginhou = .FieldToClass("sOriginhou")
                'Me.nRoute = .FieldToClass("nRoute")
                'Me.sName_licen = .FieldToClass("sName_licen")
                Find = True
                .RCloseRec()
            Else
                Find = False
            End If
        End With

        lclsTran_stage = Nothing

        Exit Function
    End Function
	'%Objetivo: Verifica la existencia de un registro en la tabla
	'%Parámetros:
	'%            GeographicZone1 - Código del primer nivel de distribución geográfica del país.
	Public Function IsExistTran_stagedet(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nStage As Integer, ByVal nCurrency As Integer) As Boolean
		Dim lclsTran_stage As eRemoteDB.Execute
		Dim lintExist As Short
		
		lclsTran_stage = New eRemoteDB.Execute
		lintExist = 0
		
		'+ Define all parameters for the stored procedures 'reatran_stagedet_v'
		'+ Define todos los parametros para el stored procedure 'reatran_stagedet_v'
		With lclsTran_stage
			.StoredProcedure = "reatran_stagedet_v"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nStage", nStage, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExist", lintExist, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				IsExistTran_stagedet = (.Parameters("nExist").Value = 1)
			Else
				IsExistTran_stagedet = False
			End If
		End With
		lclsTran_stage = Nothing
		
		Exit Function
	End Function
	
	'**%Objective: It verifies the existence of a registry in table "Civil" using the key of this table.
	'**%Parameters:
	'**%    sCertype   - tipo de poliza/cotización/ propuesta.
	'**%    nBranch    - code of the branch
	'**%    nProduct   - code of the product
	'**%    nPolicy    - code of the policy
	'**%    nCertif    - code of the Certificat
	'**%    dEffecdate - effective date of the record
	'%Objetivo: Verifica la existencia de un registro en la tabla "Civil" usando la clave de dicha tabla.
	'%Parámetros:
	'%    sCertype   - tipo de poliza/cotización/ propuesta.
	'%    nBranch    - código del ramo
	'%    nProduct   - codigo del producto
	'%    nPolicy    - código de la poliza
	'%    nCertif    - código del certificado
	'%    dEffecdate - fecha de efecto del registro
	Private Function IsExist_StageDet(ByVal sCertype As String, ByVal nBranch As Short, ByVal nProduct As Short, ByVal nPolicy As Integer, ByVal nCertif As Integer, ByVal dEffecdate As Date, ByVal nCurrency As Short) As Boolean
		Dim lclsTran_stage As eRemoteDB.Execute
		Dim lintExist As Short
		

        lclsTran_stage = New eRemoteDB.Execute
		lintExist = 0
		
		'+ Define all parameters for the stored procedures 'insudb.valCivilExist'. Generated on 14/06/2004 11:08:40 a.m.
		With lclsTran_stage
			.StoredProcedure = "insExistTR009"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExist", lintExist, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				IsExist_StageDet = (.Parameters("nExist").Value = 1)
			Else
				IsExist_StageDet = False
			End If
		End With
		
		lclsTran_stage = Nothing
		
		Exit Function
	End Function
	
	
	'**%Objective: Validates the data from the detail section of the page being processed.
	'**%Parameters:
	'**%    sAction      - It indicates the type of action to be applied in the table ("Add", "Update" o "Del")
	'**%    sPolitype    - Policy type
	'**%    sCodispl     - Code of the window (logical code)
	'**%    nCurrency    - Code of the currency
	'**%    sCertype     - Type of record
	'**%    nBranch      - Code of the line of business
	'**%    nProduct     - Code of the product
	'**%    nPolicy      - Number identifying the policy
	'**%    nCertif      - Number identifying the certificate
	'**%    nStage       - Number identifying the stage
	'**%    dEffecdate   - Effective date of the record
	'**%    dDestindat   - Arrival date to the place of destination
	'**%    dOrigindat   - Departure date from the place of origin
	'**%    nTyproute    - Type of route
	'**%    nTransptype  - Transport type
	'**%    sName_licen  - Key identifying the Transportation Mode
	'**%    sOrigen      - City of Origin of the Route
	'**%    sDestination - City of Destination of the Route
	'%Objetivo: Esta función permite validar los datos del detalle de la página en tratamiento.
	'%Parámetros:
	'%    sAction      - Indica el tipo de acción a ejecutar sobre los registros en la tabla ("Insertar", "Actualizar" o "Eliminar").
	'%    sPolitype    - Tipo de póliza
	'%    sCodispl     - Código de la ventana (lógico)
	'%    nCurrency    - Código de la moneda
	'%    sCertype     - Tipo de registro
	'%    nBranch      - Código del ramo
	'%    nProduct     - Código del producto
	'%    nPolicy      - Número que identifica la póliza
	'%    nCertif      - Número que identifica el certificado
	'%    nStage       - Número que identifica la etapa
	'%    dEffecdate   - Fecha efectiva del registro
	'%    dDestindat   - Fecha de llegada al lugar de destino
	'%    dOrigindat   - Fecha de salida del lugar de origen
	'%    nTyproute    - Tipo de ruta asegurada
	'%    nTransptype  - Tipo de transporte
	'%    sName_licen  - Nombre o matrícula del medio de transporte
	'%    sOrigen      - Ciudad origen de la ruta
	'%    sDestination - Ciudad destino de la ruta
	Public Function InsValTR009_Itin(ByVal sAction As String, ByVal sPolitype As String, ByVal sCodispl As String, ByVal nCurrency As Integer, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nStage As Integer, ByVal dEffecdate As Date, ByVal dDestindat As Date, ByVal dOrigindat As Date, ByVal nTypRoute As Integer, ByVal nTransptype As Short, ByVal sName_licen As String, ByVal sOrigen As String, ByVal sDestination As String) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lstrErrors As String
		

        lclsErrors = New eFunctions.Errors
		With lclsErrors
			
			lstrErrors = InsValTR009_ItinDB(sAction, sPolitype, sCodispl, nCurrency, sCertype, nBranch, nProduct, nPolicy, nCertif, nStage, dEffecdate, dDestindat, dOrigindat, nTypRoute, nTransptype, sName_licen, sOrigen, sDestination)
			If Len(lstrErrors) > 0 Then
                .ErrorMessage(sCodispl, , , , , , lstrErrors)
			End If
			
			InsValTR009_Itin = .Confirm
		End With
		
		lclsErrors = Nothing
		
		Exit Function
	End Function
	
	
	'**%Objective: Validation of the data for the page details.
	'**%Parameters:
	'**%    sCodispl     - Logical code that identifies the transaction.
	'**%    sCertype     - Type of record
	'**%    nBranch      - Code of the line of business
	'**%    nProduct     - Code of the product
	'**%    nPolicy      - Number of the policy
	'**%    nCertif      - Number identifying the certificate
	'**%    dEffecdate   - Effective date of the record
	'%Objetivo: Validación de los datos para la página detalle.
	'%Parámetros:
	'%    sCodispl     - Código lógico que identifica la transacción.
	'%    sCertype     - Tipo de registro
	'%    nBranch      - Código de la línea del negocio
	'%    nProduct     - Código del producto
	'%    nPolicy      - Número de la póliza
	'%    nCertif      - Número que identifica el certificado
	'%    dEffecdate   - Fecha de efecto del registro
	Public Function InsValTR009(ByVal sCodispl As String, ByVal sCertype As String, ByVal nBranch As Short, ByVal nProduct As Short, ByVal nPolicy As Integer, ByVal nCertif As Integer, ByVal dEffecdate As Date, ByVal nCurrency As Short, ByVal nUsercode As Short) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lclsPolicyWin As Policy_Win

        lclsErrors = New eFunctions.Errors
		
		If Me.Find(sCertype, nBranch, nProduct, nPolicy, nCertif, nCurrency, dEffecdate) Then
			If Not IsExist_StageDet(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nCurrency) Then
				Call lclsErrors.ErrorMessage(sCodispl, 80151)
			Else
				lclsPolicyWin = New ePolicy.Policy_Win
				Call lclsPolicyWin.Add_PolicyWin(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, "TR009", "2")
				lclsPolicyWin = Nothing
			End If
		End If
		InsValTR009 = lclsErrors.Confirm
		
		lclsErrors = Nothing
		
		Exit Function
	End Function
	
	'**%Objetivo: Performs validations by accessing to the database.
	'**%Parameters:
	'**%    sAction      - It indicates the type of action to be applied in the table ("Add", "Update" o "Del")
	'**%    sPolitype    - Policy type
	'**%    sCodispl     - Code of the window (logical code).
	'**%    nCurrency    - Code of the currency
	'**%    sCertype     - Type of record
	'**%    nBranch      - Code of the line of business
	'**%    nProduct     - Code of the product
	'**%    nPolicy      - Number identifying the policy
	'**%    nCertif      - Number identifying the certificate
	'**%    nStage       - Number identifying the stage
	'**%    dEffecdate   - Effective date of the record
	'**%    dDestindat   - Arrival date to the place of destination
	'**%    dOrigindat   - Departure date from the place of origin
	'**%    nTyproute    - Type of route
	'**%    nTransptype  - Transport type
	'**%    sName_licen  - Key identifying the Transportation Mode
	'**%    sOrigen      - City of Origin of the Route
	'**%    sDestination - City of Destination of the Route
	'%Objetivo: Esta función permite realizar validaciones con acceso a la base de datos.
	'%Parámetros:
	'%    sAction      - Indica el tipo de acción a ejecutar sobre los registros en la tabla ("Insertar", "Actualizar" o "Eliminar").
	'%    sPolitype    - Tipo de póliza
	'%    sCodispl     - Código de la ventana (lógico)
	'%    nCurrency    - Código de la moneda
	'%    sCertype     - Tipo de registro
	'%    nBranch      - Código del ramo
	'%    nProduct     - Código del producto
	'%    nPolicy      - Número que identifica la póliza
	'%    nCertif      - Número que identifica el certificado
	'%    nStage       - Número que identifica la etapa
	'%    dEffecdate   - Fecha efectiva del registro
	'%    dDestindat   - Fecha de llegada al lugar de destino
	'%    dOrigindat   - Fecha de salida del lugar de origen
	'%    nTyproute    - Tipo de ruta asegurada
	'%    nTransptype  - Tipo de transporte
	'%    sName_licen  - Nombre o matrícula del medio de transporte
	'%    sOrigen      - Ciudad origen de la ruta
	'%    sDestination - Ciudad destino de la ruta
	Private Function InsValTR009_ItinDB(ByVal sAction As String, ByVal sPolitype As String, ByVal sCodispl As String, ByVal nCurrency As Integer, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nStage As Integer, ByVal dEffecdate As Date, ByVal dDestindat As Date, ByVal dOrigindat As Date, ByVal nTypRoute As Integer, ByVal nTransptype As Short, ByVal sName_licen As String, ByVal sOrigen As String, ByVal sDestination As String) As String
		Dim lclsTran_stage As eRemoteDB.Execute
		


        InsValTR009_ItinDB = String.Empty

		lclsTran_stage = New eRemoteDB.Execute
		
		With lclsTran_stage
			.StoredProcedure = "valTransTR009_Itin"
			.Parameters.Add("sAction", sAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nStage", nStage, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dDestindat", dDestindat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dOrigindat", dOrigindat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTyproute", nTypRoute, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTransptype", nTransptype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sName_licen", sName_licen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sOrigen", sOrigen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDestination", sDestination, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sPolitype", sPolitype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sErrorList", " ", eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(False) Then
				InsValTR009_ItinDB = Trim(.Parameters("sErrorList").Value)
			End If
		End With
		
		lclsTran_stage = Nothing
		
		Exit Function
	End Function
	
	'**%Objective: Sends the information necessary to update the records in the database.
	'**%Parameters:
	'**%    sAction      - It indicates the type of action to be applied in the table ("Add", "Update" o "Del")
	'**%    sPolitype    - Policy type
	'**%    nUsercode    - Code of the user creating or updating the record
	'**%    nCurrency    - Code of the currency
	'**%    sCertype     - Type of record
	'**%    nBranch      - Code of the line of business
	'**%    nProduct     - Code of the product
	'**%    nPolicy      - Number identifying the policy
	'**%    nCertif      - Number identifying the certificate
	'**%    nStage       - Number identifying the stage
	'**%    dEffecdate   - Effective date of the record
	'**%    dDestindat   - Arrival date to the place of destination
	'**%    dOrigindat   - Departure date from the place of origin
	'**%    nRoute       - type of coveraged route
	'**%    sOrigen      - City of Origin of the Route
	'**%    sDestination - City of Destination of the Route
	'**%    sName_licen  - Key identifying the Transportation Mode
	'%Objetivo: Esta función permite enviar la información necesaria de los registros en tratamiento a la base de datos para su
	'% posterior actualización.
	'%Parámetros:
	'%    sAction      - Indica el tipo de acción a ejecutar sobre los registros en la tabla ("Insertar", "Actualizar" o "Eliminar").
	'%    sPolitype    - Tipo de póliza
	'%    nUsercode    - Código del usuario que crea o actualiza el registro.
	'%    nCurrency    - Código de la moneda
	'%    sCertype     - Tipo de registro
	'%    nBranch      - Código del ramo
	'%    nProduct     - Código del producto
	'%    nPolicy      - Número que identifica la póliza
	'%    nCertif      - Número que identifica el certificado
	'%    nStage       - Número que identifica la etapa
	'%    dEffecdate   - Fecha efectiva del registro
	'%    dDestindat   - Fecha de llegada al lugar de destino
	'%    dOrigindat   - Fecha de salida del lugar de origen
	'%    nRoute       - Tipo de ruta asegurada
	'%    sOrigen      - Ciudad origen de la ruta
	'%    sDestination - Ciudad destino de la ruta
	'%    sName_licen  - Nombre o matrícula del medio de transporte
    Public Function InsPostTR009_Itin(ByVal sAction As String, _
                                      ByVal sPolitype As String, _
                                      ByVal nUsercode As Integer, _
                                      ByVal nCurrency As Integer, _
                                      ByVal sCertype As String, _
                                      ByVal nBranch As Integer, _
                                      ByVal nProduct As Integer, _
                                      ByVal nPolicy As Double, _
                                      ByVal nCertif As Double, _
                                      ByVal nStage As Integer, _
                                      ByVal dEffecdate As Date, _
                                      ByVal dDestindat As Date, _
                                      ByVal dOrigindat As Date, _
                                      ByVal nRoute As Integer, _
                                      ByVal sOrigen As String, _
                                      ByVal sDestination As String, _
                                      ByVal sName_licen As String, _
                                      ByVal sPurchase_Order As String, _
                                      ByVal sApplicationNumber As String) As Boolean

        Dim lclsPolicyWin As Policy_Win



        lclsPolicyWin = New Policy_Win

        Select Case sAction
            Case "Add", "Update"
                InsPostTR009_Itin = AddUpdate(sAction, sPolitype, nUsercode, sCertype, nBranch, nProduct, nPolicy, nCertif, nStage, dEffecdate, dDestindat, dOrigindat, nRoute, sName_licen, sOrigen, sDestination, sPurchase_Order, sApplicationNumber)
            Case "Del"
                InsPostTR009_Itin = Delete(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nStage, nUsercode)

                Call lclsPolicyWin.Add_PolicyWin(sCertype, nBranch, nProduct, _
                                                 nPolicy, nCertif, dEffecdate, _
                                                 nUsercode, "TR009", "3", , , , False)

        End Select

        Call lclsPolicyWin.Add_PolicyWin(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, "CA014", "3", , , , False)

        lclsPolicyWin = Nothing

        Exit Function
    End Function
End Class











