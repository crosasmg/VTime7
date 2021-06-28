Option Strict Off
Option Explicit On
Public Class Tran_stagedet
	'**+Objective: Class that supports the table Tran_stagedet.
	'**+Version: $$Revision: 4 $
	'+Objetivo: Clase que le da soporte a la tabla Tran_stagedet.
	'+Version: $$Revision: 4 $
	
	'**-Objective: Type of record
	'-Objetivo: Tipo de registro
	Public sCertype As String
	
	'**-Objective: Code of the line of business
	'-Objetivo: C�digo del ramo
	Public nBranch As Integer
	
	'**-Objective: Code of the product
	'-Objetivo: C�digo del producto
	Public nProduct As Integer
	
	'**-Objective: Number identifying the policy
	'-Objetivo: N�mero que identifica la p�liza
	Public nPolicy As Double
	
	'**-Objective: Number identifying the certificate
	'-Objetivo: N�mero que identifica el certificado
	Public nCertif As Double
	
	'**-Objective: Number identifying the Stage
	'-Objetivo: Numero identificativo de la etapa
	Public nStage As Integer
	
	'**-Objective: Classification of the merchandise
	'-Objetivo: Clasificaci�n de la mercanc�a
	Public nClassmerch As Integer
	
	'**-Objective: Merchandise classification Description
	'-Objetivo: Descripci�n de la Clasificaci�n de la mercanc�a
	Public sClassdesc As String
	
	'**-Objective: Type of packing associated to the merchandise
	'-Objetivo: Tipo de embalaje asociado a la mercanc�a
	Public nPacking As Integer
	
	'**-Objective: Packing type description associated to the merchandise
	'-Objetivo: Descripci�n del tipo de embalaje asociado a la mercanc�a
	Public sPackdesc As String
	
	'**-Objective: Code of the currency
	'-Objetivo: C�digo de la moneda
	Public nCurrency As Integer
	
	'**-Objective: Effectide date of the record
	'-Objetivo: Fecha efectiva del registro
	Public dEffecdate As Date
	
	'**-Objective: Date which from the record is valid in Tran_stage table
	'-Objetivo: Fecha de efecto del registro en la tabla Tran_stage
	Public dEfd_tran_stage As Date
	
	'**-Objective: Assured amount  ot the transported  merchandise
	'-Objetivo: Monto asegurado de la mercanc�a transportada
	Public nAmount As Double
	
	'**-Objective: Amount of Franchise/Deductible of the transported merchandise
	'-Objetivo: Monto de franquicia/deducible de la mercanc�a transportada
	Public nFrandedi As Double
	
	'**-Objective: Number of elements that are transported
	'-Objetivo: N�mero de elementos que se transportan
	Public nQuantrans As Integer
	
	'**-Objective: Unit of capacity or weight of the elements that are transported
	'-Objetivo: Unidad de capacidad o peso de los elementos que se transportan
	Public nUnit As Integer
	
	'**-Objective: Rate to apply to the merchandise
	'-Objetivo: Tasa a aplicar a la mercanc�a
    Public nMerchrate As Double

	
	'**-Objective: Value by unit of the merchandise
	'-Objetivo: Valor unitario de la mercanc�a
	Public nUnitvalue As Double
	
	'**-Objective: Number of the notes containing the comments
	'-Objetivo: N�mero de la nota que contiene los comentarios
	Public nNotenum As Double
	
	'**-Objective: Number of the image associated to the merchandise
	'-Objetivo: N�mero de la imagen asociada a la mercanc�a
	Public nImageNum As Double
	
	'**-Objective: Code o the user executing the transaction
	'-Objetivo: C�digo del usuario que ejecuta la transacci�n
	Public nUsercode As Integer
	
	'**%Objective: This method updates or adds a record into the table Tran_stagedet
	'**%Parameters:
	'**%    sAction         - The type of action to be executed for the record ("Add" or "Update")
	'**%    nUsercode       - Code of the user that creates or updates the record.
	'**%    sCertype        - Type of record
	'**%    nBranch         - Code of the line of business
	'**%    nProduct        - Code of the product
	'**%    nPolicy         - Number identifying the policy
	'**%    nCertif         - Number identifying the certificate
	'**%    nStage          - Number identifying the stage
	'**%    dEffecdate      - Effective date of the record
	'**%    nClassmerch     - Classification of the transported merchandise
	'**%    nPacking        - Type of packing associated to the merchandise
	'**%    nCurrency       - Code of the currency
	'**%    nAmount         - Assured amount  ot the transported  merchandise
	'**%    nFrandedi       - Type of packing associated to the merchandise
	'**%    nQuatrans       - Number of elements that are transported
	'**%    nUnit           - Unit of capacity or weight of the elements that are transported
	'**%    nMerchrate      - Rate to apply to the merchandise
	'**%    nUnitvalue      - Value by unit of the merchandise
	'**%    nNotenum        - Number of the note containing the comments
	'**%    nImagenum       - Number of the image associated to the merchandise
	'%Objetivo: Este m�todo permite agregar o actualizar un registro en la tabla Tran_stagedet
	'%Par�metros:
	'%    sAction         - Indica el tipo de acci�n a ejecutar sobre el registro en la tabla ("Insertar" o "Actualizar").
	'%    nUsercode       - C�digo del usuario que crea o actualiza el registro.
	'%    sCertype        - Tipo de registro
	'%    nBranch         - C�digo del ramo
	'%    nProduct        - C�digo del producto
	'%    nPolicy         - N�mero que identifica la p�liza
	'%    nCertif         - N�mero que identifica el certificado
	'%    nStage          - N�mero que identifica la etapa
	'%    dEffecdate      - Fecha efectiva del registro
	'%    nClassmerch     - Clasificaci�n de la mercanc�a transportada
	'%    nPacking        - Tipo de embalaje asociado a la mercanc�a transportada
	'%    nCurrency       - C�digo de la moneda
	'%    nAmount         - Monto asegurado de la mercanc�a transportada
	'%    nFrandedi       - Monto de franquicia/deducible de la mercanc�a transportada
	'%    nQuatrans       - N�mero de elementos trasnportados
	'%    nUnit           - Unidad de capacidad o peso de los elementos que se transportan
	'%    nMerchrate      - Tasa a aplicar a la mercanc�a
	'%    nUnitvalue      - Valor unitario de la mercanc�a
	'%    nNotenum        - N�mero de la nota que contiene los comentarios
	'%    nImagenum       - N�mero de la imagen asociada a la mercancia
	Private Function AddUpdate(ByVal sAction As String, ByVal nUsercode As Integer, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nStage As Integer, ByVal dEffecdate As Date, ByVal nClassmerch As Integer, ByVal nPacking As Integer, ByVal nCurrency As Integer, ByVal nAmount As Double, ByVal nFrandedi As Double, ByVal nQuantrans As Integer, ByVal nUnit As Integer, ByVal nMerchrate As Double, ByVal nUnitvalue As Double, ByVal nNotenum As Double, ByVal nImageNum As Double) As Boolean
		Dim lclsTran_stagedet As eRemoteDB.Execute
		

        lclsTran_stagedet = New eRemoteDB.Execute
		
		With lclsTran_stagedet
			.StoredProcedure = "insupdTran_stagedet"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nStage", nStage, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nClassmerch", nClassmerch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPacking", nPacking, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmount", nAmount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nFrandedi", nFrandedi, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nQuantrans", nQuantrans, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUnit", nUnit, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMerchrate", nMerchrate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 9, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUnitvalue", nUnitvalue, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNotenum", nNotenum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nImageNum", nImageNum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			AddUpdate = .Run(False)
		End With
		
		lclsTran_stagedet = Nothing
		
		Exit Function
	End Function
	
	'**%Objective: Deletes a record from the table Tran_stagedet by using the table's key.
	'**%Parameters:
	'**%    sCertype        - Type of record
	'**%    nBranch         - Code of the line of business
	'**%    nProduct        - Code of the product
	'**%    nPolicy         - Number identifying the policy
	'**%    nCertif         - Number identifying the certificate
	'**%    dEffecdate      - Effective date of the record
	'**%    nStage          - Number identifying the stage
	'**%    nClassmerch     - Classification of the transported merchandise
	'**%    nPacking        - Type of packing associated to the merchandise
	'**%    nCurrency       - Code of the currency
	'**%    nUsercode       - Code of the user that creates or updates the record
	'%Objetivo: Este m�todo permite eliminar un registro de la tabla Tran_stagedet a trav�s de la clave de dicha tabla.
	'%Par�metros:
	'%    sCertype        - Tipo de registro
	'%    nBranch         - C�digo del ramo
	'%    nProduct        - C�digo del producto
	'%    nPolicy         - N�mero que identifica la p�liza
	'%    nCertif         - N�mero que identifica el certificado
	'%    dEffecdate      - Fecha efectiva del registro
	'%    nStage          - N�mero que identifica la etapa
	'%    nClassmerch     - Clasificaci�n de la mercanc�a transportada
	'%    nPacking        - Tipo de embalaje asociado a la mercanc�a transportada
	'%    nCurrency       - C�digo de la moneda
	'%    nUsercode       - C�digo del usuario que crea o actualiza el registro
	Private Function Delete(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nStage As Integer, ByVal nClassmerch As Integer, ByVal nPacking As Integer, ByVal nCurrency As Integer, ByVal nUsercode As Integer) As Boolean
		Dim lclsTran_stagedet As eRemoteDB.Execute
		

        lclsTran_stagedet = New eRemoteDB.Execute
		
		With lclsTran_stagedet
			.StoredProcedure = "delTran_stagedet"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nStage", nStage, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nClassmerch", nClassmerch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPacking", nPacking, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Delete = .Run(False)
		End With
		
		lclsTran_stagedet = Nothing
		
		Exit Function
	End Function
	
	'**%Objective: Validates the data from the detail section of the page being processed.
	'**%Parameters:
	'**%    sCodispl        - Code of the window (logical code).
	'**%    sCertype        - Type of record
	'**%    nBranch         - Code of the line of business
	'**%    nProduct        - Code of the product
	'**%    nPolicy         - Number identifying the policy
	'**%    nCertif         - Number identifying the certificate
	'**%    nStage          - Number identifying the stage
	'**%    dEffecdate      - Effective date of the record
	'**%    nClassmerch     - Classification of the transported merchandise
	'**%    nPacking        - Type of packing associated to the merchandise
	'**%    nCurrency       - Code of the currency
	'**%    nAmount         - Assured amount  ot the transported  merchandise
	'**%    nMerchrate      - Rate to apply to the merchandise
	'**%    nUnitvalue      - Value by unit of the merchandise
	'%Objetivo: Esta funci�n permite validar los datos del detalle de la p�gina en tratamiento.
	'%Par�metros:
	'%    sCodispl        - C�digo de la ventana (l�gico).
	'%    sCertype        - Tipo de registro
	'%    nBranch         - C�digo del ramo
	'%    nProduct        - C�digo del producto
	'%    nPolicy         - N�mero que identifica la p�liza
	'%    nCertif         - N�mero que identifica el certificado
	'%    nStage          - N�mero que identifica la etapa
	'%    dEffecdate      - Fecha efectiva del registro
	'%    nClassmerch     - Clasificaci�n de la mercanc�a transportada
	'%    nPacking        - Tipo de embalaje asociado a la mercanc�a transportada
	'%    nCurrency       - C�digo de la moneda
	'%    nAmount         - Monto asegurado de la mercanc�a transportada
	'%    nMerchrate      - Tasa a aplicar a la mercanc�a
	'%    nUnitvalue      - Valor por unidad de la mercanc�a
	Public Function InsValTR009_Merch(ByVal sAction As String, ByVal sCodispl As String, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nStage As Integer, ByVal dEffecdate As Date, ByVal nClassmerch As Integer, ByVal nPacking As Integer, ByVal nCurrency As Integer, ByVal nAmount As Double, ByVal nMerchrate As Double, ByVal nUnitvalue As Double) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lstrErrors As String
		

        lclsErrors = New eFunctions.Errors
		With lclsErrors
			
			lstrErrors = InsValTR009_MerchDB(sAction, sCertype, nBranch, nProduct, nPolicy, nCertif, nStage, nCurrency, dEffecdate, nClassmerch, nPacking, nAmount, nFrandedi, nQuantrans, nUnit, nMerchrate, nUnitvalue)
			If Len(lstrErrors) > 0 Then
                .ErrorMessage(sCodispl, , , , , , lstrErrors)
			End If
			
			InsValTR009_Merch = .Confirm
		End With
		
		lclsErrors = Nothing
		
		Exit Function
	End Function
	
	'**%Objective: Sends the information necessary to update the records in the database.
	'**%Parameters:
	'**%    sAction         - The type of action to be executed for the record ("Add" or "Update")
	'**%    nUsercode       - Code of the user that creates or updates the record.
	'**%    sCertype        - Type of record
	'**%    nBranch         - Code of the line of business
	'**%    nProduct        - Code of the product
	'**%    nPolicy         - Number identifying the policy
	'**%    nCertif         - Number identifying the certificate
	'**%    nStage          - Number identifying the stage
	'**%    dEffecdate      - Effective date of the record
	'**%    nClassmerch     - Classification of the transported merchandise
	'**%    nPacking        - Type of packing associated to the merchandise
	'**%    nCurrency       - Code of the currency
	'**%    dEfd_tran_stage - Date which from the record is valid in Tran_stage table
	'**%    nAmount         - Assured amount  ot the transported  merchandise
	'**%    nFrandedi       - Type of packing associated to the merchandise
	'**%    nQuatrans       - Number of elements that are transported
	'**%    nUnit           - Unit of capacity or weight of the elements that are transported
	'**%    nMerchrate      - Rate to apply to the merchandise
	'**%    nUnitvalue      - Value by unit of the merchandise
	'**%    nNotenum        - Number of the note containing the comments
	'**%    nImagenum       - Number of the image associated to the merchandise
	'%Objetivo: Esta funci�n permite enviar la informaci�n necesaria de los registros en tratamiento a la base de datos para su
	'% posterior actualizaci�n.
	'%Par�metros:
	'%    sAction         - Indica el tipo de acci�n a ejecutar sobre el registro en la tabla ("Insertar" o "Actualizar").
	'%    nUsercode       - C�digo del usuario que crea o actualiza el registro.
	'%    sCertype        - Tipo de registro
	'%    nBranch         - C�digo del ramo
	'%    nProduct        - C�digo del producto
	'%    nPolicy         - N�mero que identifica la p�liza
	'%    nCertif         - N�mero que identifica el certificado
	'%    nStage          - N�mero que identifica la etapa
	'%    dEffecdate      - Fecha efectiva del registro
	'%    nClassmerch     - Clasificaci�n de la mercanc�a transportada
	'%    nPacking        - Tipo de embalaje asociado a la mercanc�a transportada
	'%    nCurrency       - C�digo de la moneda
	'%    dEfd_tran_stage - Fecha de efecto del registro en la tabla Tran_stage
	'%    nAmount         - Monto asegurado de la mercanc�a transportada
	'%    nFrandedi       - Monto de franquicia/deducible de la mercanc�a transportada
	'%    nQuatrans       - N�mero de elementos trasnportados
	'%    nUnit           - Unidad de capacidad o peso de los elementos que se transportan
	'%    nMerchrate      - Tasa a aplicar a la mercanc�a
	'%    nUnitvalue      - Valor unitario de la mercanc�a
	'%    nNotenum        - N�mero de la nota que contiene los comentarios
	'%    nImagenum       - N�mero de la imagen asociada a la mercancia
	Public Function InsPostTR009_Merch(ByVal sAction As String, ByVal nUsercode As Integer, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nStage As Integer, ByVal dEffecdate As Date, ByVal nClassmerch As Integer, ByVal nPacking As Integer, ByVal nCurrency As Integer, ByVal nAmount As Double, ByVal nFrandedi As Double, ByVal nQuantrans As Integer, ByVal nUnit As Integer, ByVal nMerchrate As Double, ByVal nUnitvalue As Double, ByVal nNotenum As Double, ByVal nImageNum As Double) As Boolean
		Dim lclsPolicyWin As Policy_Win
		

        lclsPolicyWin = New Policy_Win
		
		Select Case sAction
			Case "Add", "Update"
				InsPostTR009_Merch = AddUpdate(sAction, nUsercode, sCertype, nBranch, nProduct, nPolicy, nCertif, nStage, dEffecdate, nClassmerch, nPacking, nCurrency, nAmount, nFrandedi, nQuantrans, nUnit, nMerchrate, nUnitvalue, nNotenum, nImageNum)
				
			Case "Del"
                InsPostTR009_Merch = Delete(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nStage, nClassmerch, nPacking, nCurrency, nUsercode)

                Call lclsPolicyWin.Add_PolicyWin(sCertype, nBranch, nProduct, _
                                                 nPolicy, nCertif, dEffecdate, _
                                                 nUsercode, "TR009", "3", , , , False)

		End Select
		
        Call lclsPolicyWin.Add_PolicyWin(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, "CA014", "3", , , , False)
		
		lclsPolicyWin = Nothing
		
		Exit Function
	End Function
	
	'**%Objective: Validates the data from the detail section of the page being processed.
	'**%Parameters:
	'**%    sCodispl        - Code of the window (logical code).
	'**%    sCertype        - Type of record
	'**%    nBranch         - Code of the line of business
	'**%    nProduct        - Code of the product
	'**%    nPolicy         - Number identifying the policy
	'**%    nCertif         - Number identifying the certificate
	'**%    nStage          - Number identifying the stage
	'**%    nCurrency       - Code of the currency
	'**%    dEffecdate      - Effective date of the record
	'**%    nClassmerch     - Classification of the transported merchandise
	'**%    nPacking        - Type of packing associated to the merchandise
	'**%    nAmount         - Assured amount  ot the transported  merchandise
	'**%    nFrandedi       - Type of packing associated to the merchandise
	'**%    nQuatrans       - Number of elements that are transported
	'**%    nUnit           - Unit of capacity or weight of the elements that are transported
	'**%    nMerchrate      - Rate to apply to the merchandise
	'**%    nUnitvalue      - Value by unit of the merchandise
	'%Objetivo: Esta funci�n permite validar los datos del detalle de la p�gina en tratamiento.
	'%Par�metros:
	'%    sAction         - Acci�n que se ejecuta en la transacci�n
	'%    sCertype        - Tipo de registro
	'%    nBranch         - C�digo del ramo
	'%    nProduct        - C�digo del producto
	'%    nPolicy         - N�mero que identifica la p�liza
	'%    nCertif         - N�mero que identifica el certificado
	'%    nStage          - N�mero que identifica la etapa
	'%    nCurrency       - C�digo de la moneda
	'%    dEffecdate      - Fecha efectiva del registro
	'%    nClassmerch     - Clasificaci�n de la mercanc�a transportada
	'%    nPacking        - Tipo de embalaje asociado a la mercanc�a transportada
	'%    nAmount         - Monto asegurado de la mercanc�a transportada
	'%    nFrandedi       - Monto de franquicia/deducible de la mercanc�a transportada
	'%    nQuatrans       - N�mero de elementos trasnportados
	'%    nUnit           - Unidad de capacidad o peso de los elementos que se transportan
	'%    nMerchrate      - Tasa a aplicar a la mercanc�a
	'%    nUnitvalue      - Valor unitario de la mercanc�a
	Private Function InsValTR009_MerchDB(ByVal sAction As String, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nStage As Integer, ByVal nCurrency As Integer, ByVal dEffecdate As Date, ByVal nClassmerch As Integer, ByVal nPacking As Integer, ByVal nAmount As Double, ByVal nFrandedi As Double, ByVal nQuantrans As Integer, ByVal nUnit As Integer, ByVal nMerchrate As Double, ByVal nUnitvalue As Double) As String
		Dim lclsTran_stagedet As eRemoteDB.Execute
		


        InsValTR009_MerchDB = String.Empty

		lclsTran_stagedet = New eRemoteDB.Execute
		
		With lclsTran_stagedet
			.StoredProcedure = "valTransTR009_Merch"
			.Parameters.Add("sAction", sAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nStage", nStage, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nClassmerch", nClassmerch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPacking", nPacking, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmount", nAmount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nFrandedi", nFrandedi, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nQuantrans", nQuantrans, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUnit", nUnit, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMerchrate", nMerchrate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 9, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUnitvalue", nUnitvalue, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sErrorList", " ", eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(False) Then
				InsValTR009_MerchDB = Trim(.Parameters("sErrorList").Value)
			End If
		End With
		
		lclsTran_stagedet = Nothing
		
		Exit Function
	End Function
End Class











